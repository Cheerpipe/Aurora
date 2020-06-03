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

namespace Aurora.Devices.NZXTHUE2Ambient
{
    public class NZXTHUE2AmbientDesktopDevice : Device
    {
        public string _devicename = "NZXT HUE Ambient Desktop";
        private System.Diagnostics.Stopwatch _watch = new System.Diagnostics.Stopwatch();
        private bool _isConnected;
        private long _lastUpdateTime = 0;
        private DeviceKeys _commitKey;
        private Color _initialColor = Color.FromArgb(0, 0, 0);
        private List<DeviceMapState> _deviceMap;
        public bool Initialize()
        {
            try
            {
                UpdateDeviceMap();
                KillProcessByName("NZXT CAM.exe");
                KillProcessByName("NZXTHUEAmbientListener.exe");
                Thread.Sleep(500);
                Process.Start(@"D:\Warez\Utiles\NZXTHUEAmbientListener\NZXTHUEAmbientListener.exe");
                _isConnected = true;
                return true;
            }
            catch (Exception)
            {
                _isConnected = false;
                return false;
            }
        }

        NamedPipeClientStream pipe;
        BinaryWriter stream;
        public void SendArgs(byte[] args)
        {
            using (var pipe = new NamedPipeClientStream(".", "HUE2AmbientDeviceController1", PipeDirection.Out))
            using (var stream = new BinaryWriter(pipe))
            {
                pipe.Connect(timeout: 10);
                stream.Write(args);
            }
        }

        public static void KillProcessByName(string processName)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = @"C:\Windows\System32\taskkill.exe";
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.StartInfo.Arguments = string.Format(@"/f /im {0}", processName);
            cmd.Start();
            cmd.Dispose();
        }

        public void Reset()
        {
            Shutdown();
            Thread.Sleep(1000);
            Initialize();
        }

        public void Shutdown()
        {
            try
            {
                SendArgs(new byte[] { 1, 5, 0, 0, 0, 0 }); // Operatin code 5 set all leds to black and close the listener application.
            }
            catch
            {
                //Just in case Bridge is not responding or already closed
            }

            Thread.Sleep(1000); // Time to shutdown leds and close listener application.
            _isConnected = false;
        }

        private struct DeviceMapState
        {
            public byte led;
            public Color color;
            public DeviceKeys deviceKey;
            public DeviceMapState(byte led, Color color, DeviceKeys deviceKeys)
            {
                this.led = led;
                this.color = color;
                this.deviceKey = deviceKeys;
            }
        }
        private void UpdateDeviceMap()
        {
            _deviceMap = new List<DeviceMapState>
            {

              new DeviceMapState(25, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_9),
              new DeviceMapState(24, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_8),
              new DeviceMapState(23, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_7),
              new DeviceMapState(22, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_6),
              new DeviceMapState(21, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_5),
              new DeviceMapState(20, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_4),
              new DeviceMapState(19, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_3),
              new DeviceMapState(18, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_2),
              new DeviceMapState(17, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_1),
              new DeviceMapState(16, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_0),

              new DeviceMapState(15, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_55),
              new DeviceMapState(14, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_55),
              new DeviceMapState(13, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_54),
              new DeviceMapState(12, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_54),
              new DeviceMapState(11, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_53),
              new DeviceMapState(10, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_53),
              new DeviceMapState(9, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_52),
              new DeviceMapState(8, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_52),
              new DeviceMapState(7, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_51),
              new DeviceMapState(6, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_51),
              new DeviceMapState(5, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_50),
              new DeviceMapState(4, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_50),
              new DeviceMapState(3, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_49),
              new DeviceMapState(2, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_49),
              new DeviceMapState(1, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_48),
              new DeviceMapState(0, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_47),

              new DeviceMapState(51, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_46),
              new DeviceMapState(50, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_45),
              new DeviceMapState(49, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_44),
              new DeviceMapState(48, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_44),
              new DeviceMapState(47, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_43),
              new DeviceMapState(46, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_43),
              new DeviceMapState(45, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_42),
              new DeviceMapState(44, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_42),
              new DeviceMapState(43, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_41),
              new DeviceMapState(42, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_41),
              new DeviceMapState(41, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_40),
              new DeviceMapState(40, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_40),
              new DeviceMapState(39, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_39),
              new DeviceMapState(38, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_39),
              new DeviceMapState(37, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_39),
              new DeviceMapState(36, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_38),

              new DeviceMapState(35, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_37),
              new DeviceMapState(34, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_36),
              new DeviceMapState(33, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_35),
              new DeviceMapState(32, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_34),
              new DeviceMapState(31, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_33),
              new DeviceMapState(30, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_32),
              new DeviceMapState(29, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_31),
              new DeviceMapState(28, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_30),
              new DeviceMapState(27, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_29),
              new DeviceMapState(26, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_28)
            };
            _commitKey = _deviceMap.Max(k => k.deviceKey);
        }

        bool _deviceChanged = true;

        //Custom method to send the color to the device

        public VariableRegistry GetRegisteredVariables()
        {
            return new VariableRegistry();
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
            throw new NotImplementedException();
        }

        public bool IsPeripheralConnected()
        {
            throw new NotImplementedException();
        }

        public bool UpdateDevice(Dictionary<DeviceKeys, Color> keyColors, DoWorkEventArgs e, bool forced = false)
        {
            byte commandIndex = 0;
            try
            {
                foreach (KeyValuePair<DeviceKeys, Color> key in keyColors)
                {
                    if ((int)key.Key < 600 || (int)key.Key > 655)
                        continue;
                    for (byte d = 0; d < _deviceMap.Count; d++)
                    {
                        if ((_deviceMap[d].deviceKey == key.Key) && (key.Value != _deviceMap[d].color))
                        {
                            commandIndex++;
                            _commandDataPacket[(commandIndex - 1) * 5 + 1] = 1;
                            _commandDataPacket[(commandIndex - 1) * 5 + 2] = Convert.ToByte(key.Value.R * key.Value.A / 255);
                            _commandDataPacket[(commandIndex - 1) * 5 + 3] = Convert.ToByte(key.Value.G * key.Value.A / 255);
                            _commandDataPacket[(commandIndex - 1) * 5 + 4] = Convert.ToByte(key.Value.B * key.Value.A / 255);
                            _commandDataPacket[(commandIndex - 1) * 5 + 5] = _deviceMap[d].led;
                            _deviceMap[d] = new DeviceMapState(_deviceMap[d].led, key.Value, _deviceMap[d].deviceKey);
                            _deviceChanged = true;
                            // Can't break because map bind more than one light to one key.
                           // if (key.Key != _commitKey)
                           //     break;
                        }
                    }

                    if (key.Key == _commitKey)
                    {
                        if (_deviceChanged)
                        {
                            commandIndex++;
                            _commandDataPacket[(commandIndex - 1) * 5 + 1] = 4;
                            _commandDataPacket[(commandIndex - 1) * 5 + 2] = 0;
                            _commandDataPacket[(commandIndex - 1) * 5 + 3] = 0;
                            _commandDataPacket[(commandIndex - 1) * 5 + 4] = 0;
                            _commandDataPacket[(commandIndex - 1) * 5 + 5] = 0;
                            _commandDataPacket[0] = commandIndex;
                            SendArgs(_commandDataPacket);
                        }

                        commandIndex = 0;
                        _deviceChanged = false;
                    }

                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private byte[] _commandDataPacket = new byte[512];
        public bool UpdateDevice(DeviceColorComposition colorComposition, DoWorkEventArgs e, bool forced = false)
        {
            _watch.Restart();

            bool update_result = UpdateDevice(colorComposition.keyColors, e, forced);

            _watch.Stop();
            _lastUpdateTime = _watch.ElapsedMilliseconds;

            return update_result;
        }
    }
}