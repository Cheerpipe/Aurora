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
        private string _RGBFusionExeName = "RGBFusion.exe";
        private string _RGBFusionBridgeExeName = "RGBFusionAuroraListener.exe";
        private Dictionary<DeviceKeys, DeviceMapState> _deviceMap;
        private Color _initialColor = Color.FromArgb(0, 0, 0);
        private string _defaultProfileFileName = "pro1.xml";
        private string[] _RGBFusionBridgeFiles = new string[]
        {
            "RGBFusionAuroraListener.exe",
            "RGBFusionBridge.dll"
        };
        private const string _RGBFusionExeName = "RGBFusion.exe";
        private const string _RGBFusionBridgeExeName = "RGBFusionAuroraListener.exe";
        private const string _defaultProfileFileName = "pro1.xml";
        private const string _defaultExtProfileFileName = "ExtPro1.xml";

        public bool Initialize()
        {
            try
            {
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
                    Global.logger.Warn("RGBFusion Bridge is not installed. Installing.");
                    try
                    {
                        //InstallRGBFusionBridge();
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
                Process.Start(_RGBFusionDirectory + _RGBFusionBridgeExeName, @"--kingstondriver --aorusvgadriver --dleddriver --ignoreled:0,4,5,7,8,9");
                UpdateDeviceMap();
                _isConnected = true;
                return true;
            }
            catch (Exception ex)
            {
                Global.logger.Error("RGBFusion Bridge cannot be initialized. Error: " + ex.Message);
                _isConnected = false;
                return false;
            }
        }

        public void SendCommandToRGBFusion(byte[] args)
        {
            try
            {
                pipe.Connect(timeout: 10);
                stream.Write(args);
            }
            catch (Exception ex)
            {
                Global.logger.Error(string.Format("RGBFusion Device has encountered an error while sending the command {0} to RGBFusion Bridge. Error: {1}", BitConverter.ToString(args), ex.Message));
                return false;
            }
        }

        public void Reset()
        {
            Shutdown();
            Initialize();
        }

        public void Shutdown()
        {
            if (IsRGBFusionBridgeRunning())
            {
                if (SendCommandToRGBFusion(new byte[] { 1, 5, 0, 0, 0, 0, 0 })) // Operatin code 5 set all leds to black and close the listener application.
                    Thread.Sleep(1000); // Time to shutdown leds and close listener application.
                KillProcessByName("RGBFusionAuroraListener"); //Just in case RGBFusionAuroraListener did not close
            }
            catch
            {
                //Just in case Bridge is not responding or already closed
            }

            Thread.Sleep(1000); // Time to shutdown leds and close listener application.
            _isConnected = false;
        }

        public void KillProcessByName(string processName)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = @"C:\Windows\System32\taskkill.exe";
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.StartInfo.Arguments = string.Format(@"/f /im {0}", processName);
            cmd.Start();
            cmd.Dispose();
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
            _deviceMap.Add(DeviceKeys.MBAREA_6, new DeviceMapState(1, _initialColor));
            _deviceMap.Add(DeviceKeys.MBAREA_3, new DeviceMapState(2, _initialColor));
            _deviceMap.Add(DeviceKeys.MBAREA_2, new DeviceMapState(3, _initialColor));
            _deviceMap.Add(DeviceKeys.MBAREA_4, new DeviceMapState(6, _initialColor));
            _deviceMap.Add(DeviceKeys.MBAREA_1, new DeviceMapState(8, _initialColor));
            _deviceMap.Add(DeviceKeys.MBAREA_5, new DeviceMapState(9, _initialColor));
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
            _commitKey = _deviceMap.Keys.Max();
        }

        bool _deviceChanged = true;

        public VariableRegistry GetRegisteredVariables()
        {
            if (_variableRegistry == null)
            {
                var devKeysEnumAsEnumerable = System.Enum.GetValues(typeof(DeviceKeys)).Cast<DeviceKeys>();
                _variableRegistry = new VariableRegistry();
                _variableRegistry.Register($"{_devicename}_devicekey", DeviceKeys.Peripheral_Logo, "Key to Use", devKeysEnumAsEnumerable.Max(), devKeysEnumAsEnumerable.Min());
            }
            return _variableRegistry;
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
                            _commandDataPacket[(commandIndex - 1) * 6 + 1] = 1;
                            _commandDataPacket[(commandIndex - 1) * 6 + 2] = 10;
                            _commandDataPacket[(commandIndex - 1) * 6 + 3] = (byte)(key.Value.R * key.Value.A / 255.0f);
                            _commandDataPacket[(commandIndex - 1) * 6 + 4] = (byte)(key.Value.G * key.Value.A / 255.0f);
                            _commandDataPacket[(commandIndex - 1) * 6 + 5] = (byte)(key.Value.B * key.Value.A / 255.0f);
                            _commandDataPacket[(commandIndex - 1) * 6 + 6] = led;
                        }
                        if (led == 8) // GPU
                        {
                            commandIndex++;
                            _commandDataPacket[(commandIndex - 1) * 6 + 1] = 1;
                            _commandDataPacket[(commandIndex - 1) * 6 + 2] = 40;
                            _commandDataPacket[(commandIndex - 1) * 6 + 3] = (byte)(key.Value.R * key.Value.A / 255.0f);
                            _commandDataPacket[(commandIndex - 1) * 6 + 4] = (byte)(key.Value.G * key.Value.A / 255.0f);
                            _commandDataPacket[(commandIndex - 1) * 6 + 5] = (byte)(key.Value.B * key.Value.A / 255.0f);
                            _commandDataPacket[(commandIndex - 1) * 6 + 6] = 0;
                        }
                        else if (led == 9) // RAM
                        {
                            commandIndex++;
                            _commandDataPacket[(commandIndex - 1) * 6 + 1] = 1;
                            _commandDataPacket[(commandIndex - 1) * 6 + 2] = 30;
                            _commandDataPacket[(commandIndex - 1) * 6 + 3] = (byte)(key.Value.R * key.Value.A / 255.0f);
                            _commandDataPacket[(commandIndex - 1) * 6 + 4] = (byte)(key.Value.G * key.Value.A / 255.0f);
                            _commandDataPacket[(commandIndex - 1) * 6 + 5] = (byte)(key.Value.B * key.Value.A / 255.0f);
                            _commandDataPacket[(commandIndex - 1) * 6 + 6] = 0;
                        }
                        else if (led >= 10) // DLED PIN HEADER																																								
                        {
                            commandIndex++;
                            _commandDataPacket[(commandIndex - 1) * 6 + 1] = 1;
                            _commandDataPacket[(commandIndex - 1) * 6 + 2] = 20;
                            _commandDataPacket[(commandIndex - 1) * 6 + 3] = (byte)(key.Value.R * key.Value.A / 255.0f);
                            _commandDataPacket[(commandIndex - 1) * 6 + 4] = (byte)(key.Value.G * key.Value.A / 255.0f);
                            _commandDataPacket[(commandIndex - 1) * 6 + 5] = (byte)(key.Value.B * key.Value.A / 255.0f);
                            _commandDataPacket[(commandIndex - 1) * 6 + 6] = (byte)(led - 10);
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
                            Debug.WriteLine("Set color");
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
                _setColorCommandDataPacket[0] = 0; //Invalidate bad command
                Global.logger.Warn(string.Format("RGBFusion device error while updatind device. Error: {0}", ex.Message));
                return false;
            }
        }
        private byte[] _commandDataPacket = new byte[512];

        public bool UpdateDevice(DeviceColorComposition colorComposition, DoWorkEventArgs e, bool forced = false)
        {
            //UpdateDeviceMap();
            _ellapsedTimeWatch.Restart();
            bool update_result = UpdateDevice(colorComposition.keyColors, e, forced);
            _ellapsedTimeWatch.Stop();
            _lastUpdateTime = _ellapsedTimeWatch.ElapsedMilliseconds;
            return update_result;
        }
        #region RGBFusion Specific Methods
        private bool IsRGBFusionInstalled()
        {
            string RGBFusionDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\GIGABYTE\\RGBFusion\\";
            bool result = (Directory.Exists(RGBFusionDirectory));
            return result;
        }

        private bool IsRGBFusionRunning()
        {
            return Process.GetProcessesByName(_RGBFusionExeName).Length > 0;
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

        private bool IsRGBFusionBridgeInstalled()
        {
            string rgbFusionBridgeFullpath = _RGBFusionDirectory + _RGBFusionBridgeExeName;
            bool result = (File.Exists(rgbFusionBridgeFullpath));
            return result;
        }

        private bool InstallRGBFusionBridge()
        {
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