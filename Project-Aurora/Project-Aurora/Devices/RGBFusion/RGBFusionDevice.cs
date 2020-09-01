using Aurora;
using Aurora.Devices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using CSScriptLibrary;
using Aurora.Settings;
using System.ComponentModel;
using Aurora.Utils;
using Mono.CSharp;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml;
using System.Security.Cryptography;
using IronPython.Runtime;

namespace Aurora.Devices.RGBFusion
{
    public class RGBFusionDevice : Device
    {
        private string _devicename = "RGB Fusion";
        private bool _isConnected = false;
        private long _lastUpdateTime = 0;
        private Stopwatch _ellapsedTimeWatch = new Stopwatch();
        private VariableRegistry _variableRegistry = null;
        private DeviceKeys _commitKey;
        private string _RGBFusionDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\GIGABYTE\\RGBFusion\\";
        private Dictionary<DeviceKeys, DeviceMapState> _deviceMap;
        private Color _initialColor = Color.Black;
        private string _ignoreLedsParam = string.Empty;
        private string _customArgs = string.Empty;
        private byte[] _setColorCommandDataPacket = new byte[1024];
        private List<string> _RGBFusionBridgeFiles = new List<string>();
        private const string _RGBFusionExeName = "RGBFusion.exe";
        private const string _RGBFusionBridgeExeName = "RGBFusionAuroraListener.exe";
        private const string _defaultProfileFileName = "pro1.xml";
        private const string _defaultExtProfileFileName = "ExtPro1.xml";
        private const int _maxConnectRetryCountLeft = 50;
        private int _connectRetryCountLeft = _maxConnectRetryCountLeft;
        private bool _starting = false;
        private const int _ConnectRetryTimeOut = 100;

        private HashSet<byte> _rgbFusionLedIndexes;

        public bool Initialize()
        {
            _starting = true;
            try
            {
                if (!TestRGBFusionBridgeListener(1))
                    Shutdown();

                if (!IsRGBFusionInstalled())
                {
                    Global.logger.Error("RGBFusion is not installed.");
                    return false;
                }
                if (IsRGBFusionRunning())
                {
                    Global.logger.Error("RGBFusion should be closed before run RGBFusion Bridge.");
                    return false;
                }
                if (!IsRGBFusinMainProfileCreated())
                {
                    Global.logger.Error("RGBFusion main profile file is not created. Run RGBFusion for at least one time.");
                    return false;
                }
                if (!IsRGBFusionBridgeInstalled())
                {
                    Global.logger.Warn("RGBFusion Bridge is not installed. Installing. Installing.");
                    try
                    {
                        InstallRGBFusionBridge();
                    }
                    catch (Exception ex)
                    {
                        Global.logger.Error("RGBFusion Bridge cannot be initialized. Error: " + ex.Message);
                        return false;
                    }
                    return false;
                }

                //Start RGBFusion Bridge
                Global.logger.Info("Starting RGBFusion Bridge.");
                if (!StartListenerForDevice())
                {
                    _isConnected = false;
                    _starting = false;
                    return false;
                }

                Global.logger.Info("RGBFusion bridge is listening");
                //If device is restarted, re-send last color command.
                if (_setColorCommandDataPacket[0] != 0)
                {
                    SendCommandToRGBFusion(_setColorCommandDataPacket);
                }

                UpdateDeviceMap();
                _isConnected = true;
                _starting = false;
                _connectRetryCountLeft = _maxConnectRetryCountLeft;
                return true;
            }
            catch (Exception ex)
            {
                Global.logger.Error("RGBFusion Bridge cannot be initialized. Error: " + ex.Message);
                _isConnected = false;
                _starting = false;
                return false;
            }
            finally
            {
                _starting = false;
            }
        }

        private bool StartListenerForDevice()
        {
            try
            {
                _starting = true;
                //GetRegisteredVariables();
                string pStart = _RGBFusionDirectory + _RGBFusionBridgeExeName;
                string pArgs = _customArgs + " " + (ValidateIgnoreLedParam() ? "--ignoreled:" + _ignoreLedsParam : "");
                Process.Start(pStart, pArgs);
                if (!TestRGBFusionBridgeListener(110))
                {
                    Global.logger.Error("RGBFusion bridge listener didn't start on " + _RGBFusionDirectory + _RGBFusionBridgeExeName);
                    _starting = false;
                    return false;
                }
                else
                {
                    _starting = false;
                    return true;
                }
            }
            catch
            {
                _starting = false;
                return false;
            }
            finally
            {
                _starting = false;
            }
        }

        public void KillProcessByName(string processName)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = Environment.SystemDirectory + @"\taskkill.exe";
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.StartInfo.Arguments = string.Format(@"/f /im {0}", processName);
            cmd.Start();
            cmd.WaitForExit();
            cmd.Dispose();
        }

        public bool SendCommandToRGBFusion(byte[] args)
        {
            try
            {
                using (var pipe = new NamedPipeClientStream(".", "RGBFusionAuroraListener", PipeDirection.Out))
                using (var stream = new BinaryWriter(pipe))
                {
                    pipe.Connect(100);
                    stream.Write(args);
                    return true;
                }
            }
            catch
            {
                Thread.Sleep(100);
                return false;
            }
        }

        public void Reset()
        {
            if (_starting)
            {
                return;
            }

            if (IsRGBFusionBridgeRunning())
            {
                KillProcessByName(_RGBFusionBridgeExeName);
            }
            StartListenerForDevice();
        }

        public void Shutdown()
        {
            if (IsRGBFusionBridgeRunning())
            {
                SendCommandToRGBFusion(new byte[] { 1, 5, 0, 0, 0, 0, 0 });
                Thread.Sleep(1000);
                KillProcessByName(_RGBFusionBridgeExeName);
            }
            _isConnected = false;
        }

        private struct DeviceMapState
        {
            public byte led;
            public Color color;
            public DeviceMapState(byte led, Color color)
            {
                this.led = led;
                this.color = color;
            }
        }

        private void UpdateDeviceMap()
        {
            _deviceMap = new Dictionary<DeviceKeys, DeviceMapState>();
            _deviceMap.Add(DeviceKeys.MBAREA_4, new DeviceMapState(0, _initialColor)); //Aorus LOGO
            _deviceMap.Add(DeviceKeys.MBAREA_3, new DeviceMapState(2, _initialColor)); // SB
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_1, new DeviceMapState(10, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_2, new DeviceMapState(11, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_3, new DeviceMapState(12, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_4, new DeviceMapState(13, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_5, new DeviceMapState(14, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_6, new DeviceMapState(15, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_7, new DeviceMapState(16, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_8, new DeviceMapState(17, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_9, new DeviceMapState(18, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_10, new DeviceMapState(19, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_11, new DeviceMapState(20, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_12, new DeviceMapState(21, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_13, new DeviceMapState(22, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_14, new DeviceMapState(23, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_15, new DeviceMapState(24, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_16, new DeviceMapState(25, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_17, new DeviceMapState(26, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_18, new DeviceMapState(27, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_19, new DeviceMapState(28, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_20, new DeviceMapState(29, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_21, new DeviceMapState(30, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_22, new DeviceMapState(31, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_23, new DeviceMapState(32, _initialColor));
            _deviceMap.Add(DeviceKeys.DLEDSTRIP_24, new DeviceMapState(33, _initialColor));
            _commitKey = _deviceMap.Keys.Max();
        }

        bool _deviceChanged = true;

        public VariableRegistry GetRegisteredVariables()
        {
            if (_rgbFusionLedIndexes == null)
            {
                _rgbFusionLedIndexes = GetLedIndexes();
            }

            if (_variableRegistry == null)
            {
                var devKeysEnumAsEnumerable = System.Enum.GetValues(typeof(DeviceKeys)).Cast<DeviceKeys>();
                _variableRegistry = new VariableRegistry();
                _variableRegistry.Register($"{_devicename}_ignore_leds", "", "Area index to be ignored by RGBFusion Bridge", null, null, "Comma separated. Require Aurora restart.");
                _variableRegistry.Register($"{_devicename}_custom_args", "", "Custom command line arguments", null, null, "Just for advanced users.");
                foreach (byte ledIndex in _rgbFusionLedIndexes)
                {
                    _variableRegistry.Register($"{_devicename}_area_" + ledIndex.ToString(), DeviceKeys.ESC, "Key to Use for area index " + ledIndex.ToString(), devKeysEnumAsEnumerable.Max(), devKeysEnumAsEnumerable.Min(), "Require Aurora restart.");
                }
            }
            _ignoreLedsParam = Global.Configuration.VarRegistry.GetVariable<string>($"{_devicename}_ignore_leds");
            _customArgs = Global.Configuration.VarRegistry.GetVariable<string>($"{_devicename}_custom_args");
            return _variableRegistry;
        }

        private bool ValidateIgnoreLedParam()
        {
            if (_ignoreLedsParam == null)
                return false;

            string[] ignoreLedsParam = _ignoreLedsParam.Split(',');

            foreach (string s in ignoreLedsParam)
            {
                if (!byte.TryParse(s, out _))
                {
                    Global.logger.Warn("RGBFusion Bridge --ignoreled bad param {0}. Running Bridge in default mode.", s);
                    return false;
                }
            }
            return true;
        }

        public string GetDeviceName()
        {
            return _devicename;
        }

        public string GetDeviceDetails()
        {
            return _devicename + (_isConnected ? ": Connected" : ": Not connected");
        }

        public string GetDeviceUpdatePerformance()
        {
            return (IsConnected() ? _lastUpdateTime + " ms" : "");
        }

        public bool Reconnect()
        {
            Shutdown();
            return Initialize();
        }

        public bool IsInitialized()
        {
            return IsConnected();
        }

        public bool IsConnected()
        {
            return _isConnected;
        }

        public bool IsKeyboardConnected()
        {
            return false;
        }

        public bool IsPeripheralConnected()
        {
            return true;
        }

        public bool UpdateDevice(Dictionary<DeviceKeys, Color> keyColors, DoWorkEventArgs e, bool forced = false)
        {
            if (e.Cancel)
            {
                return false;
            }
            if (_starting)
            {
                Global.logger.Warn("RGBFusion Bridge starting. Ignoring command.");
                return false;
            }

            byte commandIndex = 0;
            try
            {
                foreach (KeyValuePair<DeviceKeys, Color> key in keyColors)
                {
                    if (!_deviceMap.TryGetValue(key.Key, out DeviceMapState deviceMapState))
                        continue;
                    byte led = deviceMapState.led;

                    if (key.Value != _deviceMap[key.Key].color)
                    {
                        if (led < 8) // MB
                        {
                            commandIndex++;
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 1] = 1;
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 2] = 10;
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 3] = (byte)(key.Value.R * key.Value.A / 255.0f);
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 4] = (byte)(key.Value.G * key.Value.A / 255.0f);
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 5] = (byte)(key.Value.B * key.Value.A / 255.0f);
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 6] = led;
                        }
                        if (led == 8) // GPU
                        {
                            commandIndex++;
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 1] = 1;
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 2] = 40;
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 3] = (byte)(key.Value.R * key.Value.A / 255.0f);
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 4] = (byte)(key.Value.G * key.Value.A / 255.0f);
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 5] = (byte)(key.Value.B * key.Value.A / 255.0f);
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 6] = 0;
                        }
                        else if (led == 9) // RAM
                        {
                            commandIndex++;
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 1] = 1;
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 2] = 30;
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 3] = (byte)(key.Value.R * key.Value.A / 255.0f);
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 4] = (byte)(key.Value.G * key.Value.A / 255.0f);
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 5] = (byte)(key.Value.B * key.Value.A / 255.0f);
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 6] = 0;
                        }
                        else if (led >= 10) // DLED PIN HEADER																																								
                        {
                            commandIndex++;
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 1] = 1;
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 2] = 20;
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 3] = (byte)(key.Value.R * key.Value.A / 255.0f);
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 4] = (byte)(key.Value.G * key.Value.A / 255.0f);
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 5] = (byte)(key.Value.B * key.Value.A / 255.0f);
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 6] = (byte)(led - 10);
                        }
                        _deviceMap[key.Key] = new DeviceMapState(led, key.Value);
                        _deviceChanged = true;
                    }

                    if (key.Key == _commitKey)
                    {
                        if (_deviceChanged)
                        {
                            commandIndex++;
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 1] = 2;
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 2] = 0;
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 3] = 0;
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 4] = 0;
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 5] = 0;
                            _setColorCommandDataPacket[(commandIndex - 1) * 6 + 6] = 0;
                            _setColorCommandDataPacket[0] = commandIndex;
                            SendCommandToRGBFusion(_setColorCommandDataPacket);
                        }
                        commandIndex = 0;
                        _deviceChanged = false;
                    }
                }
                if (e.Cancel)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Global.logger.Error(string.Format("Error sendind commands to RGBFusion Bridge. Error: {0}", ex.Message));
                return false;
            }
        }

        public bool UpdateDevice(DeviceColorComposition colorComposition, DoWorkEventArgs e, bool forced = false)
        {
            _ellapsedTimeWatch.Restart();
            bool update_result = UpdateDevice(colorComposition.keyColors, e, forced);
            _ellapsedTimeWatch.Stop();
            _lastUpdateTime = _ellapsedTimeWatch.ElapsedMilliseconds;
            if (_lastUpdateTime > _ConnectRetryTimeOut && _connectRetryCountLeft > 0)
            {
                Reset();
                _connectRetryCountLeft--;
                Global.logger.Warn(string.Format("{0} device reseted automatically.", _devicename));
            }
            else
            {
                _connectRetryCountLeft = _maxConnectRetryCountLeft;
            }
            return update_result;
        }

        #region RGBFusion Specific Methods

        private HashSet<byte> GetLedIndexes()
        {
            HashSet<byte> rgbFusionLedIndexes = new HashSet<byte>();

            string mainProfileFilePath = _RGBFusionDirectory + _defaultProfileFileName;
            if (!IsRGBFusinMainProfileCreated())
            {
                Global.logger.Error(string.Format("Main profile file not found at {0}. Launch RGBFusion at least one time.", mainProfileFilePath));
            }
            else
            {
                XmlDocument mainProfileXml = new XmlDocument();
                mainProfileXml.Load(mainProfileFilePath);
                XmlNode ledInfoNode = mainProfileXml.DocumentElement.SelectSingleNode("/LED_info");
                foreach (XmlNode node in ledInfoNode.ChildNodes)
                {
                    rgbFusionLedIndexes.Add(Convert.ToByte(node.Attributes["Area_index"]?.InnerText));
                }
            }
            string extMainProfileFilePath = _RGBFusionDirectory + _defaultExtProfileFileName;
            if (!IsRGBFusinMainExtProfileCreated())
            {
                Global.logger.Error(string.Format("Main external devices profile file not found at {0}. Launch RGBFusion at least one time.", mainProfileFilePath));
            }
            else
            {
                XmlDocument extMainProfileXml = new XmlDocument();
                extMainProfileXml.Load(extMainProfileFilePath);
                XmlNode extLedInfoNode = extMainProfileXml.DocumentElement.SelectSingleNode("/LED_info");
                foreach (XmlNode node in extLedInfoNode.ChildNodes)
                {
                    rgbFusionLedIndexes.Add(Convert.ToByte(node.Attributes["Area_index"]?.InnerText));
                }
            }
            return rgbFusionLedIndexes;
        }
        private bool IsRGBFusionInstalled()
        {
            string RGBFusionDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\GIGABYTE\\RGBFusion\\";
            bool result = (Directory.Exists(RGBFusionDirectory));
            return result;
        }

        private bool IsRGBFusionRunning()
        {
            return Process.GetProcessesByName(Path.GetFileNameWithoutExtension(_RGBFusionExeName)).Length > 0;
        }

        private bool IsRGBFusionBridgeRunning()
        {
            return Process.GetProcessesByName(Path.GetFileNameWithoutExtension(_RGBFusionBridgeExeName)).Length > 0;
        }

        private bool IsRGBFusinMainProfileCreated()
        {
            string defaulprofileFullpath = _RGBFusionDirectory + _defaultProfileFileName;
            bool result = (File.Exists(defaulprofileFullpath));
            return result;
        }

        private bool IsRGBFusinMainExtProfileCreated()
        {

            string defaulprofileFullpath = _RGBFusionDirectory + _defaultExtProfileFileName;
            bool result = (File.Exists(defaulprofileFullpath));
            return result;
        }

        private bool IsRGBFusionBridgeInstalled()
        {
            bool error = false;
            foreach (string file in _RGBFusionBridgeFiles)
            {
                if (!File.Exists(_RGBFusionDirectory + file))
                {
                    Global.logger.Warn(String.Format("File {0} not installed.", file));
                    error = true;
                }
                else if (CalculateMD5(_RGBFusionDirectory + file).ToLower() != CalculateMD5("RGBFusionBridge\\" + file).ToLower())
                {
                    Global.logger.Warn(String.Format("File {0} MD5 incorrect.", file));
                    error = true;
                }
            }
            return !error;
        }

        static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                if (!File.Exists(filename))
                    return string.Empty;
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    var md5String = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    return md5String;
                }
            }
        }

        private bool TestRGBFusionBridgeListener(byte secondsTimeOut)
        {
            bool result = false;
            for (int i = 0; i < secondsTimeOut; i++)
            {
                if (SendCommandToRGBFusion(new byte[] { 1, 255, 0, 0, 0, 0, 0 }))
                    return true;
                if (!IsRGBFusionBridgeRunning())
                    return false;
                //Test listener every 500ms until pipe is up or timeout
                Thread.Sleep(1000);
            }
            return result;
        }

        private bool InstallRGBFusionBridge()
        {
            Shutdown();
            foreach (string fileName in _RGBFusionBridgeFiles)
            {
                try
                {
                    File.Copy("RGBFusionBridge\\" + fileName, _RGBFusionDirectory + fileName, true);
                    Global.logger.Info(String.Format("RGBFusion file {0} install  OK.", fileName));
                }
                catch (Exception ex)
                {
                    Global.logger.Error(String.Format("RGBFusion file {0} install error: {1}", fileName, ex.Message));
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}