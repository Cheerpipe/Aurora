using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Devices.NZXTHUE2Ambient.DeviceMaps
{
    public static class HUE2DesktopDeviceMap
    {
        public static Dictionary<DeviceKeys, List<DeviceMapState>> GetDeviceMap(Color initialColor)
        {
            Dictionary<DeviceKeys, List<DeviceMapState>> deviceMap = new Dictionary<DeviceKeys, List<DeviceMapState>>
            {
                { DeviceKeys.LEDSTRIPLIGHT1_9, new List<DeviceMapState>
                    { new DeviceMapState(25,initialColor)}
                },
                { DeviceKeys.LEDSTRIPLIGHT1_8, new List<DeviceMapState>
                    { new DeviceMapState(24,initialColor)}
                },
                { DeviceKeys.LEDSTRIPLIGHT1_7, new List<DeviceMapState>
                    { new DeviceMapState(23,initialColor)}
                },
                { DeviceKeys.LEDSTRIPLIGHT1_6, new List<DeviceMapState>
                    { new DeviceMapState(22,initialColor)}
                },
                { DeviceKeys.LEDSTRIPLIGHT1_5, new List<DeviceMapState>
                    { new DeviceMapState(21,initialColor)}
                },
                { DeviceKeys.LEDSTRIPLIGHT1_4, new List<DeviceMapState>
                    { new DeviceMapState(20,initialColor)}
                },
                { DeviceKeys.LEDSTRIPLIGHT1_3, new List<DeviceMapState>
                    { new DeviceMapState(19,initialColor)}
                },
                { DeviceKeys.LEDSTRIPLIGHT1_2, new List<DeviceMapState>
                    { new DeviceMapState(18,initialColor)}
                },
                { DeviceKeys.LEDSTRIPLIGHT1_1, new List<DeviceMapState>
                    { new DeviceMapState(17,initialColor)}
                },
                { DeviceKeys.LEDSTRIPLIGHT1_0, new List<DeviceMapState>
                    { new DeviceMapState(16,initialColor)}
                },
                { DeviceKeys.LEDSTRIPLIGHT1_55, new List<DeviceMapState>
                    {
                        { new DeviceMapState(15,initialColor)},
                        { new DeviceMapState(14,initialColor)}
                    }
                },
                { DeviceKeys.LEDSTRIPLIGHT1_54, new List<DeviceMapState>
                    {
                        { new DeviceMapState(13,initialColor)},
                        { new DeviceMapState(12,initialColor)}
                    }
                },
                { DeviceKeys.LEDSTRIPLIGHT1_53, new List<DeviceMapState>
                    {
                        { new DeviceMapState(11,initialColor)},
                        { new DeviceMapState(10,initialColor)}
                    }
                },
                { DeviceKeys.LEDSTRIPLIGHT1_52, new List<DeviceMapState>
                    {
                        { new DeviceMapState(9,initialColor)},
                        { new DeviceMapState(8,initialColor)}
                    }
                },
                { DeviceKeys.LEDSTRIPLIGHT1_51, new List<DeviceMapState>
                    {
                        { new DeviceMapState(7,initialColor)},
                        { new DeviceMapState(6,initialColor)}
                    }
                },
                { DeviceKeys.LEDSTRIPLIGHT1_50, new List<DeviceMapState>
                    {
                        { new DeviceMapState(5,initialColor)},
                        { new DeviceMapState(4,initialColor)}
                    }
                },
                { DeviceKeys.LEDSTRIPLIGHT1_49, new List<DeviceMapState>
                    {
                        { new DeviceMapState(3,initialColor)},
                        { new DeviceMapState(2,initialColor)}
                    }
                },
                { DeviceKeys.LEDSTRIPLIGHT1_48, new List<DeviceMapState>
                    {
                        { new DeviceMapState(1,initialColor)}
                    }
                },
                { DeviceKeys.LEDSTRIPLIGHT1_47, new List<DeviceMapState>
                    {
                        { new DeviceMapState(0,initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_46, new List<DeviceMapState>
                 {
                    {
                         new DeviceMapState(51,initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_45, new List<DeviceMapState>
                    {
                        { new DeviceMapState(50,initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_44, new List<DeviceMapState>
                    {
                        { new DeviceMapState(48,initialColor)},
                        { new DeviceMapState(49,initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_43, new List<DeviceMapState>
                    {
                        { new DeviceMapState(47,initialColor)},
                        { new DeviceMapState(46,initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_42, new List<DeviceMapState>
                    {
                        { new DeviceMapState(45,initialColor)},
                        { new DeviceMapState(44,initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_41, new List<DeviceMapState>
                    {
                        { new DeviceMapState(43,initialColor)},
                        { new DeviceMapState(42,initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_40, new List<DeviceMapState>
                    {
                        { new DeviceMapState(41,initialColor)},
                        { new DeviceMapState(40,initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_39, new List<DeviceMapState>
                    {
                        { new DeviceMapState(39,initialColor)},
                        { new DeviceMapState(38,initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_38, new List<DeviceMapState>
                    {
                        { new DeviceMapState(37,initialColor)},
                        { new DeviceMapState(36,initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_37, new List<DeviceMapState>
                    {
                        { new DeviceMapState(35,initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_36, new List<DeviceMapState>
                    {
                        { new DeviceMapState(34,initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_35, new List<DeviceMapState>
                    {
                        { new DeviceMapState(33,initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_34, new List<DeviceMapState>
                    {
                        { new DeviceMapState(32,initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_33, new List<DeviceMapState>
                    {
                        { new DeviceMapState(31,initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_32, new List<DeviceMapState>
                    {
                        { new DeviceMapState(30,initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_31, new List<DeviceMapState>
                    {
                        { new DeviceMapState(29,initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_30, new List<DeviceMapState>
                    {
                        { new DeviceMapState(28,initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_29, new List<DeviceMapState>
                    {
                        { new DeviceMapState(27,initialColor)}
                    }
                },
                 { DeviceKeys.LEDSTRIPLIGHT1_28, new List<DeviceMapState>
                    {
                        { new DeviceMapState(26,initialColor)}
                    }
                },
            };
            return deviceMap;
        }
    }
}
