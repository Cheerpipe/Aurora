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

namespace Aurora.Devices.HassioLightDevice
{
    public class HassioLightDevice : Device
    {
        private string _devicename = "Hassio";
        private bool _isConnected;
        private long _lastUpdateTime = 0;
        private Stopwatch _ellapsedTimeWatch = new Stopwatch();
        private VariableRegistry _variableRegistry = null;
        private HassioClient hassioClient;


        public Color CurrentColor { get { return currentColor; } }
        public bool Initialize()
        {
            try
            {
                hassioClient = new HassioClient();
                _isConnected = true;
                return true;
            }
            catch
            {
                _isConnected = false;
                return false;
            }
        }


        public void Reset()
        {
            Shutdown();
            Initialize();
            hassioClient.SetColor(currentColor);
        }

        public void Shutdown()
        {
            try
            {
                hassioClient.SetColor(Color.Black);
            }
            catch
            {
                //Just in case Bridge is not responding or already closed
            }
            _isConnected = false;
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
        private Color currentColor = Color.Black;

        private bool AmbientLightEnabled()
        {
            var win_reg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Aurora");
            return (string)win_reg.GetValue("AmbientLightEnabled") == "1";
        }

        public bool UpdateDevice(Dictionary<DeviceKeys, Color> keyColors, DoWorkEventArgs e, bool forced = false)
        {
            if (e.Cancel)
            {
                return false;
            }

            try
            {
                foreach (KeyValuePair<DeviceKeys, Color> key in keyColors)
                {
                    if (key.Key == Global.Configuration.VarRegistry.GetVariable<DeviceKeys>($"{_devicename}_devicekey"))
                    {
                        if (key.Value != currentColor)
                        {
                            hassioClient.SetColor(key.Value);
                            currentColor = key.Value;
                        }
                    }
                }

                if (e.Cancel)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private byte[] _commandDataPacket = new byte[512];

        public bool UpdateDevice(DeviceColorComposition colorComposition, DoWorkEventArgs e, bool forced = false)
        {
            if (!AmbientLightEnabled())
            {
                _lastUpdateTime = 0;
                return true;
            }

            _ellapsedTimeWatch.Restart();
            bool update_result = UpdateDevice(colorComposition.keyColors, e, forced);
            _ellapsedTimeWatch.Stop();
            _lastUpdateTime = _ellapsedTimeWatch.ElapsedMilliseconds;
            return update_result;
        }
    }
}