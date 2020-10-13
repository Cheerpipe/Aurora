using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Devices.NZXTHUE2Ambient
{
    public struct DeviceMapState
    {
        public byte led;
        public Color color;
        public DeviceMapState(byte led, Color color)
        {
            this.led = led;
            this.color = color;
        }
    }
}
