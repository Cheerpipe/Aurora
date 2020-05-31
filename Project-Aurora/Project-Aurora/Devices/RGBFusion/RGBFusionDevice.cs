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

namespace Aurora.Devices.RGBFusion
{
    public class RGBFusionDevice : Device
    {
        private string devicename = "RGB Fusion";
        private bool isConnected;
        private long lastUpdateTime = 0;
        private System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

        public bool Initialize()
        {
            try
            {
                //TODO: Check if RGBFusionsetcolor is up and fire if off
                try
                {
                    Shutdown();
                }
                catch { }
                Process.Start(@"C:\Program Files (x86)\GIGABYTE\RGBFusion\RGBFusionAuroraListener.exe", @"--kingstondriver --aorusvgadriver --dleddriver --ignoreled:0,4,5,7,8,9");
                isConnected = true;
                return true;
            }
            catch (Exception exc)
            {
                isConnected = false;
                return false;
            }
        }

        public void SendArgs(byte[] args)
        {
            using (var pipe = new NamedPipeClientStream(".", "RGBFusionAuroraListener", PipeDirection.Out))
            using (var stream = new BinaryWriter(pipe))
            {
                pipe.Connect(timeout: 10);
                stream.Write(args);
            }
        }

        public void Reset()
        {
            Shutdown();
            Thread.Sleep(1000);
            Initialize();
        }

        public void Shutdown()
        {
            SendArgs(new byte[] { 5, 0, 0, 0, 0, 0 });
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
		//           MB Area/LEd			   Aurora DeviceKey
		new DeviceMapState(1, _initialColor, DeviceKeys.MBAREA_6),
        new DeviceMapState(2, _initialColor, DeviceKeys.MBAREA_3),
        new DeviceMapState(3, _initialColor, DeviceKeys.MBAREA_2),
        new DeviceMapState(6, _initialColor, DeviceKeys.MBAREA_4),
        new DeviceMapState(8, _initialColor, DeviceKeys.MBAREA_1),
        new DeviceMapState(9, _initialColor, DeviceKeys.MBAREA_5),
        new DeviceMapState(10, _initialColor, DeviceKeys.DLEDSTRIP_1),
        new DeviceMapState(11, _initialColor, DeviceKeys.DLEDSTRIP_2),
        new DeviceMapState(12, _initialColor, DeviceKeys.DLEDSTRIP_3),
        new DeviceMapState(13, _initialColor, DeviceKeys.DLEDSTRIP_4),
        new DeviceMapState(14, _initialColor, DeviceKeys.DLEDSTRIP_5),
        new DeviceMapState(15, _initialColor, DeviceKeys.DLEDSTRIP_6),
        new DeviceMapState(16, _initialColor, DeviceKeys.DLEDSTRIP_7),
        new DeviceMapState(17, _initialColor, DeviceKeys.DLEDSTRIP_8),
        new DeviceMapState(18, _initialColor, DeviceKeys.DLEDSTRIP_9),
        new DeviceMapState(19, _initialColor, DeviceKeys.DLEDSTRIP_10),
        new DeviceMapState(20, _initialColor, DeviceKeys.DLEDSTRIP_11),
        new DeviceMapState(21, _initialColor, DeviceKeys.DLEDSTRIP_12),
        new DeviceMapState(22, _initialColor, DeviceKeys.DLEDSTRIP_13),
        new DeviceMapState(23, _initialColor, DeviceKeys.DLEDSTRIP_14),
        new DeviceMapState(24, _initialColor, DeviceKeys.DLEDSTRIP_15),
        new DeviceMapState(25, _initialColor, DeviceKeys.DLEDSTRIP_16),
        new DeviceMapState(26, _initialColor, DeviceKeys.DLEDSTRIP_17),
        new DeviceMapState(27, _initialColor, DeviceKeys.DLEDSTRIP_18)
    };

        bool _deviceChanged = true;

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
            if (e.Cancel)
            {
                return false;
            }

            try
            {
                foreach (KeyValuePair<DeviceKeys, Color> key in keyColors)
                {
                    //FELIPE
                    //Check range of RGBFusion.
                    if ((int)key.Key < 800 || (int)key.Key > 828)
                        continue;

                    if (key.Key == DeviceKeys.MBAREA_6 || key.Key == DeviceKeys.DLEDSTRIP_18 )
                    {
                        if (_deviceChanged)
                            SendArgs(new byte[] { 2, 0, 0, 0, 0, 0 });

                        _deviceChanged = false;
                    }

                    for (byte d = 0; d < deviceMap.Count; d++)
                    {
                        if ((deviceMap[d].deviceKey == key.Key) && (key.Value != deviceMap[d].color))
                        {
                            if (deviceMap[d].led < 8) // MB
                            {
                                SendArgs(new byte[]
                                {
                                1,
                                10, //Motherboard device ID
								Convert.ToByte(key.Value.R * key.Value.R / 255),
                                Convert.ToByte(key.Value.G * key.Value.G / 255),
                                Convert.ToByte(key.Value.B * key.Value.B / 255),
                                Convert.ToByte(deviceMap[d].led) //number between 0 and 9. 8 can also be VGA and 9 RAM if you don't use specific driver for devices.																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																																		
								});
                            }
                            if (deviceMap[d].led == 8) // GPU
                            {
                                SendArgs(new byte[]
                                {
                                1,
                                40,
                                Convert.ToByte(key.Value.R * key.Value.R / 255),
                                Convert.ToByte(key.Value.G * key.Value.G / 255),
                                Convert.ToByte(key.Value.B * key.Value.B / 255),
                                Convert.ToByte(0)
                                });
                            }
                            else if (deviceMap[d].led == 9) // RAM
                            {
                                SendArgs(new byte[]
                                {
                                1,
                                30, //RAM device ID
								Convert.ToByte(key.Value.R * key.Value.R / 255),
                                Convert.ToByte(key.Value.G * key.Value.G / 255),
                                Convert.ToByte(key.Value.B * key.Value.B / 255),
                                Convert.ToByte(0) // ALways 0 for now. Working in DIM and single LED control
								});
                            }
                            else if (deviceMap[d].led >= 10) // DLED PIN HEADER																																								
                            {
                                SendArgs(new byte[]
                                {
                                1, // COmmand Set
								20, // Device ID for DLED pin header
								Convert.ToByte(key.Value.R * key.Value.R / 255),
                                Convert.ToByte(key.Value.G * key.Value.G / 255),
                                Convert.ToByte(key.Value.B * key.Value.B / 255),
                                Convert.ToByte(deviceMap[d].led-10) // LED ID 0-17
								});
                            }

                            if ((deviceMap[d].deviceKey == key.Key) && (key.Value != deviceMap[d].color))
                            {
                                deviceMap[d] = new DeviceMapState(deviceMap[d].led, key.Value, deviceMap[d].deviceKey);
                                _deviceChanged = true;
                            }
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