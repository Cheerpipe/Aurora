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
using NAudio.CoreAudioApi;
using System.Management;

namespace Aurora.Devices.NZXTHUE2Ambient
{
    public class NZXTHUE2 : IDevice
    {
        public string _devicename = string.Empty;
        private Stopwatch _watch = new Stopwatch();
        private VariableRegistry _variableRegistry = null;
        private bool _isConnected;
        private long _lastUpdateTime = 0;
        byte _commandIndex = 0;
        private Color _initialColor = Color.Black;
        private int _deviceIndex = -1;
        private const int _maxConnectRetryCountLeft = 10;
        private int _connectRetryCountLeft = _maxConnectRetryCountLeft;
        private Dictionary<DeviceKeys, List<DeviceMapState>> _deviceMap;
        private bool _useFastStartup = false;
        private const int _ConnectRetryTimeOut = 190;
        private const string NZXTHUEAmbientListenerExeName = "NZXTHUEAmbientListener.exe";

        public bool Initialize()
        {
            try
            {
                _starting = true;
                KillProcessByName("NZXT CAM.exe");
                UpdateConfigVariables();
                if (!ListenerRunning())
                {
                    Global.logger.Warn(string.Format("{0} device listener not running. Starting.", _devicename));
                    StartListenerForDevice(_useFastStartup ? "--uselastsetting" : "");
                }
                _isConnected = true;
                return true;
            }
            catch (Exception)
            {
                _isConnected = false;
                return false;
            }
            finally
            {
                _starting = false;
            }
        }

        public NZXTHUE2(string deviceName, int deviceIndex, Dictionary<DeviceKeys, List<DeviceMapState>> deviceMap)
        {
            _devicename = string.Format("NZXT HUE2 Ambient - ({0})", deviceName);
            _deviceIndex = deviceIndex;
            _deviceMap = deviceMap;
        }

        private void StartListenerForDevice(string customArgs = "")
        {
            //TODO Move to registry key
            Process.Start(@"D:\Warez\Utiles\NZXTHUEAmbientListener\NZXTHUEAmbientListener.exe", "--dev:" + _deviceIndex + " " + customArgs);
        }

        private bool ListenerRunning()
        {
            try
            {
                SendArgs(new byte[] { 1, 255, 0, 0, 0, 0 });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void SendArgs(byte[] args)
        {
            using (var pipe = new NamedPipeClientStream(".", "HUE2AmbientDeviceController" + _deviceIndex, PipeDirection.Out))
            using (var stream = new BinaryWriter(pipe))
            {
                pipe.Connect(timeout: 200);
                stream.Write(args);
            }
        }

        public void KillProcessByName(string processName, string args = "")
        {
            processName = Path.GetFileNameWithoutExtension(processName);
            Process[] process = Process.GetProcessesByName(processName);

            foreach (Process p in process)
            {
                if (args != "")
                    if (GetCommandLine(p).Contains(args))
                    {
                        try { p.Kill(); }
                        catch { }
                    }

            }
        }

        private string GetCommandLine(Process process)
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))
            using (ManagementObjectCollection objects = searcher.Get())
            {
                string args = objects.Cast<ManagementBaseObject>().SingleOrDefault()?["CommandLine"]?.ToString();
                return args != null ? args : "";
            }
        }
        bool _starting = false;
        public void Reset()
        {
            if (_starting)
            {
                return;
            }

            try
            {
                _starting = true;
                SendArgs(new byte[] { 1, 6, 0, 0, 0, 0 }); // Operatin code 5 set all leds to black and close the listener application.
                Global.logger.Warn(string.Format("Soft reseting {0}.", _devicename));
            }
            catch
            {
                Global.logger.Warn(string.Format("Soft reseting {0} didn't work. Using hard reset", _devicename));
                KillProcessByName(NZXTHUEAmbientListenerExeName, "--dev:" + _deviceIndex);
                StartListenerForDevice(_useFastStartup ? "--uselastsetting" : "");
            }
            finally
            {
                _starting = false;
            }
        }

        public void Shutdown()
        {
            _isConnected = false;
            try
            {
                SendArgs(new byte[] { 1, 5, 0, 0, 0, 0 }); // Operatin code 5 set all leds to black and close the listener application.
            }
            catch
            {
                Thread.Sleep(1000);
                KillProcessByName(NZXTHUEAmbientListenerExeName, "--dev:" + _deviceIndex);
            }

        }

        bool _deviceChanged = true;

        //Custom method to send the color to the device

        public VariableRegistry RegisteredVariables
        {
            get
            {
                if (_variableRegistry == null)
                {
                    _variableRegistry = new VariableRegistry();
                    _variableRegistry.Register($"{_devicename}_use_fast_startup", false, "Use fast startup", null, null, "Use last led detection to avoid detection routine to speedup device initialization.");
                    _variableRegistry.Register($"{_devicename}_device_index", 0, "Device index", 255, 0, "First detected device has index 0.");
                }
                return _variableRegistry;
            }
        }
        private void UpdateConfigVariables()
        {
            _useFastStartup = Global.Configuration.VarRegistry.GetVariable<bool>($"{_devicename}_use_fast_startup");
            _deviceIndex = Global.Configuration.VarRegistry.GetVariable<int>($"{_devicename}_device_index");
        }

        private byte[] _dataPacket = new byte[256];

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

        public bool IsConnected()
        {
            return _isConnected;
        }

        public bool IsKeyboardConnected()
        {
            throw new NotImplementedException();
        }

        public bool IsPeripheralConnected()
        {
            throw new NotImplementedException();
        }

        public bool UpdateDevice(Dictionary<DeviceKeys, Color> keyColors, DoWorkEventArgs e, bool forced = false)
        {

            try
            {
                _commandIndex = 0;
                _deviceChanged = false;
                foreach (KeyValuePair<DeviceKeys, Color> key in keyColors)
                {
                    if (!_deviceMap.TryGetValue(key.Key, out List<DeviceMapState> deviceMapStateList))
                        continue;

                    for (int d = 0; d < deviceMapStateList.Count; d++)
                    {
                        if (key.Value != deviceMapStateList[d].color)
                        {
                            _commandIndex++;
                            _commandDataPacket[(_commandIndex - 1) * 5 + 1] = 1;
                            _commandDataPacket[(_commandIndex - 1) * 5 + 2] = (byte)(key.Value.R * key.Value.A / 255.0f);
                            _commandDataPacket[(_commandIndex - 1) * 5 + 3] = (byte)(key.Value.G * key.Value.A / 255.0f);
                            _commandDataPacket[(_commandIndex - 1) * 5 + 4] = (byte)(key.Value.B * key.Value.A / 255.0f);
                            _commandDataPacket[(_commandIndex - 1) * 5 + 5] = deviceMapStateList[d].led;
                            deviceMapStateList[d] = new DeviceMapState(deviceMapStateList[d].led, key.Value);
                            _deviceChanged = true;
                        }
                    }
                }

                if (_deviceChanged)
                {
                    _commandIndex++;
                    _commandDataPacket[(_commandIndex - 1) * 5 + 1] = 4;
                    _commandDataPacket[(_commandIndex - 1) * 5 + 2] = 0;
                    _commandDataPacket[(_commandIndex - 1) * 5 + 3] = 0;
                    _commandDataPacket[(_commandIndex - 1) * 5 + 4] = 0;
                    _commandDataPacket[(_commandIndex - 1) * 5 + 5] = 0;
                    _commandDataPacket[0] = _commandIndex;
                    SendArgs(_commandDataPacket);
                }

                return true;
            }
            catch (Exception ex)
            {
                Global.logger.Warn(string.Format("{0} error while sending commands. Error: {1}", _devicename, ex.Message));
                return false;
            }
        }

        private byte[] _commandDataPacket = new byte[512];

        public string DeviceName => _devicename;

        public string DeviceDetails => _devicename + (_isConnected ? ": Connected" : ": Not connected");

        public string DeviceUpdatePerformance => (IsConnected() ? _lastUpdateTime + " ms" : "");

        public bool IsInitialized => IsConnected();


        public bool UpdateDevice(DeviceColorComposition colorComposition, DoWorkEventArgs e, bool forced = false)
        {
            _watch.Restart();
            bool update_result = UpdateDevice(colorComposition.keyColors, e, forced);
            _watch.Stop();
            _lastUpdateTime = _watch.ElapsedMilliseconds;
            if (_lastUpdateTime > _ConnectRetryTimeOut && _isConnected && _connectRetryCountLeft > 0)
            {
                _connectRetryCountLeft--;
            }
            else if (_lastUpdateTime > _ConnectRetryTimeOut && _connectRetryCountLeft == 0)
            {
                _connectRetryCountLeft = _maxConnectRetryCountLeft;
                Global.logger.Warn(string.Format("{0} device reseted automatically.", _devicename));
                Reset();
            }
            else
            {
                _connectRetryCountLeft = _maxConnectRetryCountLeft;
            }

            if (_connectRetryCountLeft <= 0 && _isConnected)
            {
                Reset();
                Global.logger.Warn(string.Format("{0} device reseted automatically.", _devicename));
                Thread.Sleep(2000);
            }
            return update_result;
        }
    }
}
