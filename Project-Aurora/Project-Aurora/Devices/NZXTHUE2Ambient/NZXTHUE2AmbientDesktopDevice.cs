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

namespace Aurora.Devices.NZXTHUE2Ambient
{
    public class NZXTHUE2AmbientDesktopDevice : Device
    {
        public string _devicename = "NZXT HUE Ambient Desktop";
        private Stopwatch _watch = new Stopwatch();
        private bool _isConnected;
        private long _lastUpdateTime = 0;
        private DeviceKeys _commitKey;
        private Color _initialColor = Color.FromArgb(0, 0, 0);
        private int deviceIndex = 1;
        private Dictionary<DeviceKeys, List<DeviceMapState>> _deviceMap;
        public bool Initialize()
        {
            try
            {
                UpdateDeviceMap();
                KillProcessByName("NZXT CAM.exe");
                if (!ListenerRunning())
                {
                    StartListenerForDevice();
                }
                _isConnected = true;
                return true;
            }
            catch (Exception)
            {
                _isConnected = false;
                return false;
            }
        }

        private void StartListenerForDevice()
        {
            Process.Start(@"D:\Warez\Utiles\NZXTHUEAmbientListener\NZXTHUEAmbientListener.exe", "--dev:" + deviceIndex);
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

        NamedPipeClientStream pipe;
        BinaryWriter stream;
        public void SendArgs(byte[] args)
        {
            using (var pipe = new NamedPipeClientStream(".", "HUE2AmbientDeviceController" + deviceIndex, PipeDirection.Out))
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
            try
            {
                SendArgs(new byte[] { 1, 6, 0, 0, 0, 0 }); // Operatin code 5 set all leds to black and close the listener application.
            }
            catch
            {
                StartListenerForDevice();
            }
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

            Thread.Sleep(100); // Time to shutdown leds and close listener application.
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
            _deviceMap = new Dictionary<DeviceKeys, List<DeviceMapState>>
            {
                { DeviceKeys.LEDSTRIPLIGHT1_9, new List<DeviceMapState>
                    { new DeviceMapState(25,_initialColor)}
                },
                { DeviceKeys.LEDSTRIPLIGHT1_8, new List<DeviceMapState>
                    { new DeviceMapState(24,_initialColor)}
                },
                { DeviceKeys.LEDSTRIPLIGHT1_7, new List<DeviceMapState>
                    { new DeviceMapState(23,_initialColor)}
                },
                { DeviceKeys.LEDSTRIPLIGHT1_6, new List<DeviceMapState>
                    { new DeviceMapState(22,_initialColor)}
                },
                { DeviceKeys.LEDSTRIPLIGHT1_5, new List<DeviceMapState>
                    { new DeviceMapState(21,_initialColor)}
                },
                { DeviceKeys.LEDSTRIPLIGHT1_4, new List<DeviceMapState>
                    { new DeviceMapState(20,_initialColor)}
                },
                { DeviceKeys.LEDSTRIPLIGHT1_3, new List<DeviceMapState>
                    { new DeviceMapState(19,_initialColor)}
                },
                { DeviceKeys.LEDSTRIPLIGHT1_2, new List<DeviceMapState>
                    { new DeviceMapState(18,_initialColor)}
                },
                { DeviceKeys.LEDSTRIPLIGHT1_1, new List<DeviceMapState>
                    { new DeviceMapState(17,_initialColor)}
                },
                { DeviceKeys.LEDSTRIPLIGHT1_0, new List<DeviceMapState>
                    { new DeviceMapState(16,_initialColor)}
                },
                { DeviceKeys.LEDSTRIPLIGHT1_55, new List<DeviceMapState>
                    {
                        { new DeviceMapState(15,_initialColor)},
                        { new DeviceMapState(14,_initialColor)}
                    }
                },
                { DeviceKeys.LEDSTRIPLIGHT1_54, new List<DeviceMapState>
                    {
                        { new DeviceMapState(13,_initialColor)},
                        { new DeviceMapState(12,_initialColor)}
                    }
                },
                { DeviceKeys.LEDSTRIPLIGHT1_53, new List<DeviceMapState>
                    {
                        { new DeviceMapState(11,_initialColor)},
                        { new DeviceMapState(10,_initialColor)}
                    }
                },
                { DeviceKeys.LEDSTRIPLIGHT1_52, new List<DeviceMapState>
                    {
                        { new DeviceMapState(9,_initialColor)},
                        { new DeviceMapState(8,_initialColor)}
                    }
                },
                { DeviceKeys.LEDSTRIPLIGHT1_51, new List<DeviceMapState>
                    {
                        { new DeviceMapState(7,_initialColor)},
                        { new DeviceMapState(6,_initialColor)}
                    }
                },
                { DeviceKeys.LEDSTRIPLIGHT1_50, new List<DeviceMapState>
                    {
                        { new DeviceMapState(5,_initialColor)},
                        { new DeviceMapState(4,_initialColor)}
                    }
                },
                { DeviceKeys.LEDSTRIPLIGHT1_49, new List<DeviceMapState>
                    {
                        { new DeviceMapState(3,_initialColor)},
                        { new DeviceMapState(2,_initialColor)}
                    }
                },
                { DeviceKeys.LEDSTRIPLIGHT1_48, new List<DeviceMapState>
                    {
                        { new DeviceMapState(1,_initialColor)}
                    }
                },
                { DeviceKeys.LEDSTRIPLIGHT1_47, new List<DeviceMapState>
                    {
                        { new DeviceMapState(0,_initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_46, new List<DeviceMapState>
                 {
                    {
                         new DeviceMapState(51,_initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_45, new List<DeviceMapState>
                    {
                        { new DeviceMapState(50,_initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_44, new List<DeviceMapState>
                    {
                        { new DeviceMapState(48,_initialColor)},
                        { new DeviceMapState(49,_initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_43, new List<DeviceMapState>
                    {
                        { new DeviceMapState(47,_initialColor)},
                        { new DeviceMapState(46,_initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_42, new List<DeviceMapState>
                    {
                        { new DeviceMapState(45,_initialColor)},
                        { new DeviceMapState(44,_initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_41, new List<DeviceMapState>
                    {
                        { new DeviceMapState(43,_initialColor)},
                        { new DeviceMapState(42,_initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_40, new List<DeviceMapState>
                    {
                        { new DeviceMapState(41,_initialColor)},
                        { new DeviceMapState(40,_initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_39, new List<DeviceMapState>
                    {
                        { new DeviceMapState(39,_initialColor)},
                        { new DeviceMapState(38,_initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_38, new List<DeviceMapState>
                    {
                        { new DeviceMapState(37,_initialColor)},
                        { new DeviceMapState(36,_initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_37, new List<DeviceMapState>
                    {
                        { new DeviceMapState(35,_initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_36, new List<DeviceMapState>
                    {
                        { new DeviceMapState(34,_initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_35, new List<DeviceMapState>
                    {
                        { new DeviceMapState(33,_initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_34, new List<DeviceMapState>
                    {
                        { new DeviceMapState(32,_initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_33, new List<DeviceMapState>
                    {
                        { new DeviceMapState(31,_initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_32, new List<DeviceMapState>
                    {
                        { new DeviceMapState(30,_initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_31, new List<DeviceMapState>
                    {
                        { new DeviceMapState(29,_initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_30, new List<DeviceMapState>
                    {
                        { new DeviceMapState(28,_initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_29, new List<DeviceMapState>
                    {
                        { new DeviceMapState(27,_initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_28, new List<DeviceMapState>
                    {
                        { new DeviceMapState(26,_initialColor)}
                    }
                },
            };

            _commitKey = _deviceMap.Keys.Max();
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
                    if (!_deviceMap.TryGetValue(key.Key, out List<DeviceMapState> deviceMapStateList))
                        continue;

                    for (int d = 0; d < deviceMapStateList.Count; d++)
                    {
                        if (key.Value != deviceMapStateList[d].color)
                        {
                            commandIndex++;
                            _commandDataPacket[(commandIndex - 1) * 5 + 1] = 1;
                            _commandDataPacket[(commandIndex - 1) * 5 + 2] = (byte)(key.Value.R * key.Value.A / 255.0f);
                            _commandDataPacket[(commandIndex - 1) * 5 + 3] = (byte)(key.Value.G * key.Value.A / 255.0f);
                            _commandDataPacket[(commandIndex - 1) * 5 + 4] = (byte)(key.Value.B * key.Value.A / 255.0f);
                            _commandDataPacket[(commandIndex - 1) * 5 + 5] = deviceMapStateList[d].led;
                            deviceMapStateList[d] = new DeviceMapState(deviceMapStateList[d].led, key.Value);
                            _deviceChanged = true;
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