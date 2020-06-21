﻿using Aurora;
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
    public class NZXTHUE2 : Device
    {
        public string _devicename = string.Empty;
        private Stopwatch _watch = new Stopwatch();
        private bool _isConnected;
        private long _lastUpdateTime = 0;
        private DeviceKeys _commitKey;
        private Color _initialColor = Color.Black;
        private int _deviceIndex = -1;
        private int _connectRetryCount = 0;
        private int _maxConnectRetryCount = 3;
        private Dictionary<DeviceKeys, List<DeviceMapState>> _deviceMap;
        public bool Initialize()
        {
            try
            {
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

        public NZXTHUE2(string deviceName, int deviceIndex, Dictionary<DeviceKeys, List<DeviceMapState>> deviceMap)
        {
            _devicename = string.Format("NZXT HUE2 Ambient - ({0})", deviceName);
            _deviceIndex = deviceIndex;
            _deviceMap = deviceMap;
            _commitKey = _deviceMap.Keys.Max();
        }

        private void StartListenerForDevice()
        {
            //TODO Move to registry key
            Process.Start(@"D:\Warez\Utiles\NZXTHUEAmbientListener\NZXTHUEAmbientListener.exe", "--dev:" + _deviceIndex);
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
            using (var pipe = new NamedPipeClientStream(".", "HUE2AmbientDeviceController" + _deviceIndex, PipeDirection.Out))
            using (var stream = new BinaryWriter(pipe))
            {
                pipe.Connect(timeout: 10);
                stream.Write(args);
            }
        }

        public static void KillProcessByName(string processName)
        {
            Process cmd = new Process();
            //TODO: Get Weindows path

            cmd.StartInfo.FileName = Environment.SystemDirectory + @"\taskkill.exe";
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

            _isConnected = false;
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