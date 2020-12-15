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

namespace Aurora.Devices.MagicHomeDevice
{
    public class MagicHomeDevice : IDevice
    {
        public string _devicename = "MagicHome";
        public string ipAddress = "192.168.1.46";
        public bool enabled = true;
        private Color device_color = Color.Black;
        private MagicHomeController _magicHomeController;
        private bool _isConnected;
        private long _lastUpdateTime = 0;
        private Stopwatch _ellapsedTimeWatch = new Stopwatch();
        private VariableRegistry _variableRegistry = null;

        public Color CurrentColor { get { return currentColor; } }

        public string DeviceName => _devicename;

        public string DeviceDetails => _devicename + (_isConnected ? ": Connected" : ": Not connected");

        public string DeviceUpdatePerformance => (IsConnected() ? _lastUpdateTime + " ms" : "");

        public bool IsInitialized => IsConnected();


        public bool Initialize()
        {
            try
            {
                _magicHomeController = new MagicHomeController();
                _magicHomeController.Init(ipAddress);
                _isConnected = true;
                return true;
            }
            catch (Exception exc)
            {
                _isConnected = false;
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
            _magicHomeController.Stop();
            _isConnected = false;
        }

        bool _deviceChanged = true;

        public VariableRegistry RegisteredVariables
        {
            get
            {
                if (_variableRegistry == null)
                {
                    var devKeysEnumAsEnumerable = System.Enum.GetValues(typeof(DeviceKeys)).Cast<DeviceKeys>();
                    _variableRegistry = new VariableRegistry();
                 //   _variableRegistry.Register($"{_devicename}_devicekey", DeviceKeys.Peripheral_Logo, "Key to Use", devKeysEnumAsEnumerable.Max(), devKeysEnumAsEnumerable.Min());
                }
                return _variableRegistry;
            }
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

        public bool UpdateDevice(Dictionary<DeviceKeys, Color> keyColors, DoWorkEventArgs e, bool forced = false)
        {
            try
            {
                foreach (KeyValuePair<DeviceKeys, Color> key in keyColors)
                {
                    //Iterate over each key and color and send them to your device
                    if (key.Key == DeviceKeys.BACKTABLE)
                    {
                        //For example if we're basing our device color on Peripheral colors
                        SendColorToDevice(key.Value, forced);
                    }
                }

                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }

        private void SendColorToDevice(Color color, bool forced)
        {
            if (!device_color.Equals(color) || forced)
            {
                device_color = color;
                color = Color.FromArgb(Convert.ToByte(color.R * color.A / 255), Convert.ToByte(color.G * color.A / 255), Convert.ToByte(color.B * color.A / 255));
                _magicHomeController.SetColor(color);

            }
        }

        public bool UpdateDevice(DeviceColorComposition colorComposition, DoWorkEventArgs e, bool forced = false)
        {
            _ellapsedTimeWatch.Restart();
            bool update_result = UpdateDevice(colorComposition.keyColors, e, forced);
            _ellapsedTimeWatch.Stop();
            _lastUpdateTime = _ellapsedTimeWatch.ElapsedMilliseconds;
            return update_result;
        }
    }
}