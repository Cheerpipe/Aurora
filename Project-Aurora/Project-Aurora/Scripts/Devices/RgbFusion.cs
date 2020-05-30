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

public class RGBFusion
{
    public string devicename = "RGB Fusion";
    public bool enabled = true; //Switch to True, to enable it in Aurora

    public bool Initialize()
    {
        try
        {
            //TODO: Check if RGBFusionsetcolor is up and fire if off
			try 
			{
				Shutdown(); 
			} 
			catch {} 
            Process.Start(@"C:\Program Files (x86)\GIGABYTE\RGBFusion\RGBFusionAuroraListener.exe", @"--kingstondriver --aorusvgadriver --dleddriver --ignoreled:0,4,5,7,8,9");
            return true;
        }
        catch (Exception exc)
        {
            return false;
        }
    }

	public  void SendArgs(byte[] args)
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
    public bool UpdateDevice(Dictionary<DeviceKeys, Color> keyColors, bool forced)
    {
        try
        {
            foreach (KeyValuePair<DeviceKeys, Color> key in keyColors)
            {
                if (key.Key == DeviceKeys.MBAREA_6 || key.Key == DeviceKeys.DLEDSTRIP_18)
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
						if (deviceMap[d].led==8) // GPU
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
						else if (deviceMap[d].led==9) // RAM
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
						else if (deviceMap[d].led>=10) // DLED PIN HEADER																																								
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
}