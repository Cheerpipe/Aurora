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
        public string devicename = "NZXT HUE Ambient Desktop";
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
            SendArgs(new byte[] { 5, 0, 0, 0, 0 });
            Thread.Sleep(1000);
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
	  //             To Area/Key				   From DeviceKey		
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

        bool _deviceChanged = true;
        
        //Custom method to send the color to the device
        private void SendColorToDevice(Color color, byte led = 0)
        {
            //Check if device's current color is the same, no need to update if they are the same		
            //0 nothing
            //1 setledtrx
            //2 setledall
            //3 start trx
            //4 commit trx 
            //6 shutdown

            SendArgs(new byte[5] { 1, Convert.ToByte(color.R * color.A / 255), Convert.ToByte(color.G * color.A / 255), Convert.ToByte(color.B * color.A / 255), led });
        }

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
            try
            {
                foreach (KeyValuePair<DeviceKeys, Color> key in keyColors)
                {
                    if ((int)key.Key < 600 || (int)key.Key > 699)
                        continue;
                    if (key.Key == DeviceKeys.LEDSTRIPLIGHT1_55)
                    {
                        if (_deviceChanged)
                            SendArgs(new byte[] { 4, 0, 0, 0, 0 });
                        SendArgs(new byte[] { 3, 0, 0, 0, 0 });
                        _deviceChanged = false;
                    }
                    for (byte d = 0; d < deviceMap.Count; d++)
                    {
                        if ((deviceMap[d].deviceKey == key.Key) && (key.Value != deviceMap[d].color))
                        {
                            SendColorToDevice(key.Value, deviceMap[d].led);
                            deviceMap[d] = new DeviceMapState(deviceMap[d].led, key.Value, deviceMap[d].deviceKey);
                            _deviceChanged = true;
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

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