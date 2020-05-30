using Aurora;
using Aurora.Devices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Diagnostics;
using System.Timers;

public class ExternalPipesCommandListenerDeviceScript
{
    public string devicename = "External Pipes Command Listener";
    public bool enabled = true; //Switch to True, to enable it in Aurora
	private bool Initialized = false;
	//private Timer InitializeTimer = new System.Timers.Timer(3000);
	
    public bool Initialize()
    {
        try
        {
			if (!Initialized)
			{
				Global.net_listener.CommandRecieved += NetworkListener_ExternalCommandRecieved;
				Initialized = true;	
				enabled = false;
			}

            return false;
        }
        catch(Exception exc)
        {
            return false;
        }
    }
	
	private static void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        //Global.logger.Error("Global.dev_manager.Initialize();");
		//Global.dev_manager.Initialize();
    }
	
    private void NetworkListener_ExternalCommandRecieved(string command, string args)
    {
        switch (command)
        {
            case "restart_devices":
				Global.dev_manager.Shutdown();
                break;
            case "close":
				System.Windows.Application.Current.Shutdown();
                break;
				
        }
    }
    
    public void Reset()
    {
    }
    
    public void Shutdown()
    {

    }
    
    public bool UpdateDevice(Dictionary<DeviceKeys, Color> keyColors, bool forced)
    {
        return true;
    }
    
    private void SendColorToDevice(Color color, bool forced)
    {

	}
}