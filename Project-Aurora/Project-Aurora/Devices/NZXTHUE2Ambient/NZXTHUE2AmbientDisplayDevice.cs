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
    public class NZXTHUE2AmbientDisplayDevice : Device
    {
        public string devicename = "NZXT HUE Ambient Display";
        private System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
        private bool isConnected;
        private long lastUpdateTime = 0;
        public bool Initialize()
        {
            try
            {
                KillProcessByName("NZXT CAM.exe");
                KillProcessByName("NZXTHUEAmbientListener.exe");
                Thread.Sleep(500);
                Process.Start(@"D:\Warez\Utiles\NZXTHUEAmbientListener\NZXTHUEAmbientListener.exe");
                isConnected = true;
                return true;
            }
            catch (Exception)
            {
                isConnected = false;
                return false;
            }
        }

        public void SendArgs(byte[] args)
        {
            using (var pipe = new NamedPipeClientStream(".", "HUE2AmbientDeviceController0", PipeDirection.Out))
            using (var stream = new BinaryWriter(pipe))
            {
                pipe.Connect(100);
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
            KillProcessByName("NZXTHUEAmbientListener.exe");
            isConnected = false;
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

        private static Color _initialColor = Color.FromArgb(0, 0, 0);
        private List<DeviceMapState> deviceMap = new List<DeviceMapState>
    {
	  //             To Area/Key				   From DeviceKey		
      new DeviceMapState(0, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_0),
      new DeviceMapState(1, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_1),
      new DeviceMapState(2, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_2),
      new DeviceMapState(3, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_3),
      new DeviceMapState(4, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_4),
      new DeviceMapState(5, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_5),
      new DeviceMapState(6, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_6),
      new DeviceMapState(7, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_7),
      new DeviceMapState(8, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_8),
      new DeviceMapState(9, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_9),
      new DeviceMapState(10, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_10),
      new DeviceMapState(11, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_11),
      new DeviceMapState(12, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_12),
      new DeviceMapState(13, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_13),
      new DeviceMapState(14, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_14),
      new DeviceMapState(15, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_15),
      new DeviceMapState(16, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_16),
      new DeviceMapState(17, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_17),
      new DeviceMapState(18, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_18),
      new DeviceMapState(19, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_19),
      new DeviceMapState(20, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_20),
      new DeviceMapState(21, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_21),
      new DeviceMapState(22, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_22),
      new DeviceMapState(23, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_23),
      new DeviceMapState(24, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_24),
      new DeviceMapState(25, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_25),
      new DeviceMapState(26, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_26),
      new DeviceMapState(27, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_27),
      new DeviceMapState(28, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_28),
      new DeviceMapState(29, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_29),
      new DeviceMapState(30, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_30),
      new DeviceMapState(31, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_31),
      new DeviceMapState(32, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_32),
      new DeviceMapState(33, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_33),
      new DeviceMapState(34, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_34),
      new DeviceMapState(35, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_35),
      new DeviceMapState(36, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_36),
      new DeviceMapState(37, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_37),
      new DeviceMapState(38, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_38),
      new DeviceMapState(39, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_39),
      new DeviceMapState(40, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_40),
      new DeviceMapState(41, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_41),
      new DeviceMapState(42, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_42),
      new DeviceMapState(43, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_43),
      new DeviceMapState(44, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_44),
      new DeviceMapState(45, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_45),
      new DeviceMapState(46, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_46),
      new DeviceMapState(47, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_47),
      new DeviceMapState(48, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_48),
      new DeviceMapState(49, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_49),
      new DeviceMapState(50, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_50),
      new DeviceMapState(51, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_51),
      new DeviceMapState(52, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_52),
      new DeviceMapState(53, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_53),
      new DeviceMapState(54, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_54),
      new DeviceMapState(55, _initialColor, DeviceKeys.LEDSTRIPLIGHT1_55)
    };

        bool _deviceChanged = true;

        //Custom method to send the color to the device


        public VariableRegistry GetRegisteredVariables()
        {
            return new VariableRegistry();
        }

        public string GetDeviceName()
        {
            return devicename;
        }

        public string GetDeviceDetails()
        {
            return devicename + (isConnected ? ": Connected" : ": Not connected");
        }

        public string GetDeviceUpdatePerformance()
        {
            return (IsConnected() ? lastUpdateTime + " ms" : "");
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
            return isConnected;
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
                    for (byte d = 0; d < deviceMap.Count; d++)
                    {
                        if ((deviceMap[d].deviceKey == key.Key) && (key.Value != deviceMap[d].color))
                        {
                            commandIndex++;
                            _commandDataPacket[(commandIndex - 1) * 5 + 1] = 1;
                            _commandDataPacket[(commandIndex - 1) * 5 + 2] = Convert.ToByte(key.Value.R * key.Value.A / 255);
                            _commandDataPacket[(commandIndex - 1) * 5 + 3] = Convert.ToByte(key.Value.G * key.Value.A / 255);
                            _commandDataPacket[(commandIndex - 1) * 5 + 4] = Convert.ToByte(key.Value.B * key.Value.A / 255);
                            _commandDataPacket[(commandIndex - 1) * 5 + 5] = deviceMap[d].led;
                            deviceMap[d] = new DeviceMapState(deviceMap[d].led, key.Value, deviceMap[d].deviceKey);
                            _deviceChanged = true;
                            break;
                        }
                    }

                    if (key.Key == deviceMap.Max(k => k.deviceKey))
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
            watch.Restart();

            bool update_result = UpdateDevice(colorComposition.keyColors, e, forced);

            watch.Stop();
            lastUpdateTime = watch.ElapsedMilliseconds;

            return update_result;
        }
    }
}