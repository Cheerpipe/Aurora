﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Devices.NZXTHUE2Ambient.DeviceMaps
{
    public static class HUE2TVDeviceMap
    {
        public static Dictionary<DeviceKeys, List<DeviceMapState>> GetDeviceMap(Color initialColor)
        {
            Dictionary<DeviceKeys, List<DeviceMapState>> deviceMap = new Dictionary<DeviceKeys, List<DeviceMapState>>
            {

                { DeviceKeys.LEDSTRIPLIGHT1_18, new List<DeviceMapState>{ new DeviceMapState(0,initialColor), new DeviceMapState(1,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_17, new List<DeviceMapState>{ new DeviceMapState(2,initialColor), new DeviceMapState(3,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_16, new List<DeviceMapState>{ new DeviceMapState(4,initialColor), new DeviceMapState(5,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_15, new List<DeviceMapState>{ new DeviceMapState(6,initialColor), new DeviceMapState(7,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_14, new List<DeviceMapState>{ new DeviceMapState(8,initialColor), new DeviceMapState(9,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_13, new List<DeviceMapState>{ new DeviceMapState(10,initialColor), new DeviceMapState(11,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_12, new List<DeviceMapState>{ new DeviceMapState(12,initialColor), new DeviceMapState(13,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_11, new List<DeviceMapState>{ new DeviceMapState(14,initialColor), new DeviceMapState(15,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_10, new List<DeviceMapState>{ new DeviceMapState(16,initialColor), new DeviceMapState(17,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_9, new List<DeviceMapState>{ new DeviceMapState(18,initialColor), new DeviceMapState(19,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_8, new List<DeviceMapState>{ new DeviceMapState(20,initialColor), new DeviceMapState(21,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_7, new List<DeviceMapState>{ new DeviceMapState(22,initialColor), new DeviceMapState(23,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_6, new List<DeviceMapState>{ new DeviceMapState(24,initialColor), new DeviceMapState(25,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_5, new List<DeviceMapState>{ new DeviceMapState(26,initialColor), new DeviceMapState(27,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_4, new List<DeviceMapState>{ new DeviceMapState(28,initialColor), new DeviceMapState(29,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_3, new List<DeviceMapState>{ new DeviceMapState(30,initialColor), new DeviceMapState(31,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_2, new List<DeviceMapState>{ new DeviceMapState(32,initialColor), new DeviceMapState(33,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_1, new List<DeviceMapState>{ new DeviceMapState(34,initialColor), new DeviceMapState(35,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_0, new List<DeviceMapState>{ new DeviceMapState(36,initialColor), new DeviceMapState(37,initialColor)}},

                { DeviceKeys.LEDSTRIPLIGHT1_19, new List<DeviceMapState>{ new DeviceMapState(75,initialColor), new DeviceMapState(74,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_20, new List<DeviceMapState>{ new DeviceMapState(73,initialColor), new DeviceMapState(72,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_21, new List<DeviceMapState>{ new DeviceMapState(71,initialColor), new DeviceMapState(70,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_22, new List<DeviceMapState>{ new DeviceMapState(69,initialColor), new DeviceMapState(68,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_23, new List<DeviceMapState>{ new DeviceMapState(67,initialColor), new DeviceMapState(66,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_24, new List<DeviceMapState>{ new DeviceMapState(65,initialColor), new DeviceMapState(64,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_25, new List<DeviceMapState>{ new DeviceMapState(63,initialColor), new DeviceMapState(62,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_26, new List<DeviceMapState>{ new DeviceMapState(61,initialColor), new DeviceMapState(60,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_27, new List<DeviceMapState>{ new DeviceMapState(59,initialColor), new DeviceMapState(58,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_28, new List<DeviceMapState>{ new DeviceMapState(57,initialColor), new DeviceMapState(56,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_29, new List<DeviceMapState>{ new DeviceMapState(55,initialColor), new DeviceMapState(54,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_30, new List<DeviceMapState>{ new DeviceMapState(53,initialColor), new DeviceMapState(52,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_31, new List<DeviceMapState>{ new DeviceMapState(51,initialColor), new DeviceMapState(50,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_32, new List<DeviceMapState>{ new DeviceMapState(49,initialColor), new DeviceMapState(48,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_33, new List<DeviceMapState>{ new DeviceMapState(47,initialColor), new DeviceMapState(46,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_34, new List<DeviceMapState>{ new DeviceMapState(45,initialColor), new DeviceMapState(44,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_35, new List<DeviceMapState>{ new DeviceMapState(43,initialColor), new DeviceMapState(42,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_36, new List<DeviceMapState>{ new DeviceMapState(41,initialColor), new DeviceMapState(40,initialColor)}},
                { DeviceKeys.LEDSTRIPLIGHT1_37, new List<DeviceMapState>{ new DeviceMapState(39,initialColor), new DeviceMapState(38,initialColor)}}

              };
            return deviceMap;
        }
    }
}
