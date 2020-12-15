using System;
using System.Net;
using System.Threading;
using System.Drawing;

namespace Aurora.Devices.MagicHomeDevice
{
    public class MagicHomeController
    {
        private MagicHome.MagicHome _strips;
        private string _ipAddress;
        private Thread _newSetColorThread;
        private Color _color;
        private readonly ManualResetEvent _commandEvent = new ManualResetEvent(initialState: false);

        public bool IsInitialized()
        {
            return _strips != null && _strips.IsConnected;
        }

        public void Init(string ipAddress)
        {
            timerDisconnect.AutoReset = false;
            timerDisconnect.Enabled = false;
            timerDisconnect.Elapsed += TimerDisconnect_Elapsed;
            _ipAddress = ipAddress;
            var initThread = new Thread(DoInit);
            initThread.SetApartmentState(ApartmentState.STA);
            initThread.Start();
            initThread.Join();

            _newSetColorThread = new Thread(SetColor);
            _newSetColorThread.SetApartmentState(ApartmentState.STA);
            _newSetColorThread.Start();
        }

        private void TimerDisconnect_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Disconnect();
        }

        public void Stop()
        {
            if (_strips.IsConnected)
                _strips.Disconnect();

            while (_strips.IsConnected)
            {
                Thread.Sleep(50);
            }

            if (_strips != null)
                _strips.Dispose();
            _newSetColorThread.Abort();
        }

        private void SetColor()
        {
            while (_newSetColorThread.IsAlive)
            {
                _commandEvent.WaitOne();
                if (_strips.IsConnected)
                    _strips.SetColor(_color);
                _commandEvent.Reset();
            }
        }

        public void SetColor(Color color)
        {
            if (!_strips.IsConnected)
            {
                Connect();
            }
            _color = color;
            _commandEvent.Set();
            timerDisconnect.Stop();
            timerDisconnect.Start();
        }

        private void Disconnect()
        {
            if (_strips != null)
                if (_strips.IsConnected)
                    _strips.Disconnect();
        }

        System.Timers.Timer timerDisconnect = new System.Timers.Timer(5000);

        private void Connect()
        {
            if (_strips != null)
                if (!_strips.IsConnected)
                    _strips.Connect();

        }

        private void DoInit()
        {
            _strips = new MagicHome.MagicHome();
            if (!IPAddress.TryParse(_ipAddress, out _strips.Ip))
            {
                Console.WriteLine("Invalid IP Address");
            }

            Connect();
            _strips.OnConnectFail += _strips_OnConnectFail;
            _strips.OnConnectionLost += _strips_OnConnectionLost;
            _strips.TurnOnWhenConnected = false;
            System.Timers.Timer timeout = new System.Timers.Timer(5000);
            timeout.Elapsed += Timeout_Elapsed;
            timeout.Start();
            while (!_strips.IsConnected && timeout.Enabled)
            {
                Thread.Sleep(50);
            }
            if (!_strips.IsConnected)
            {
                Console.WriteLine("Controller not found.");
            }
        }

        private void _strips_OnConnectionLost(object sender, EventArgs e)
        {
            if (_strips != null)
                if (!_strips.IsConnected)
                    _strips.Connect();
        }

        private void _strips_OnConnectFail(object sender, EventArgs e)
        {

        }
        private static void Timeout_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ((System.Timers.Timer)sender).Enabled = false;
        }
    }
}