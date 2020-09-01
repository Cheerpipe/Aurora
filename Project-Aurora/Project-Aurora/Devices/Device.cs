using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Devices
{
    /// <summary>
    /// Enum definition, representing everysingle supported device key
    /// </summary>
    public enum DeviceKeys
    {
        /// <summary>
        /// Peripheral Device
        /// <note type="note">Setting this key will make entire peripheral device one color</note>
        /// </summary>
        [Description("All Peripheral Devices")]
        Peripheral = 0,

        /// <summary>
        /// Escape key
        /// </summary>
        [Description("Escape")]
        ESC = 1,

        /// <summary>
        /// F1 key
        /// </summary>
        [Description("F1")]
        F1 = 2,

        /// <summary>
        /// F2 key
        /// </summary>
        [Description("F2")]
        F2 = 3,

        /// <summary>
        /// F3 key
        /// </summary>
        [Description("F3")]
        F3 = 4,

        /// <summary>
        /// F4 key
        /// </summary>
        [Description("F4")]
        F4 = 5,

        /// <summary>
        /// F5 key
        /// </summary>
        [Description("F5")]
        F5 = 6,

        /// <summary>
        /// F6 key
        /// </summary>
        [Description("F6")]
        F6 = 7,

        /// <summary>
        /// F7 key
        /// </summary>
        [Description("F7")]
        F7 = 8,

        /// <summary>
        /// F8 key
        /// </summary>
        [Description("F8")]
        F8 = 9,

        /// <summary>
        /// F9 key
        /// </summary>
        [Description("F9")]
        F9 = 10,

        /// <summary>
        /// F10 key
        /// </summary>
        [Description("F10")]
        F10 = 11,

        /// <summary>
        /// F11 key
        /// </summary>
        [Description("F11")]
        F11 = 12,

        /// <summary>
        /// F12 key
        /// </summary>
        [Description("F12")]
        F12 = 13,

        /// <summary>
        /// Print Screen key
        /// </summary>
        [Description("Print Screen")]
        PRINT_SCREEN = 14,

        /// <summary>
        /// Scroll Lock key
        /// </summary>
        [Description("Scroll Lock")]
        SCROLL_LOCK = 15,

        /// <summary>
        /// Pause/Break key
        /// </summary>
        [Description("Pause")]
        PAUSE_BREAK = 16,


        /// <summary>
        /// Half/Full width (Japanese layout) key
        /// </summary>
        [Description("Half/Full width")]
        JPN_HALFFULLWIDTH = 152,

        /// <summary>
        /// OEM 5 key
        /// </summary>
        [Description("OEM 5")]
        OEM5 = 156,

        /// <summary>
        /// Tilde key
        /// </summary>
        [Description("Tilde")]
        TILDE = 17,

        /// <summary>
        /// One key
        /// </summary>
        [Description("1")]
        ONE = 18,

        /// <summary>
        /// Two key
        /// </summary>
        [Description("2")]
        TWO = 19,

        /// <summary>
        /// Three key
        /// </summary>
        [Description("3")]
        THREE = 20,

        /// <summary>
        /// Four key
        /// </summary>
        [Description("4")]
        FOUR = 21,

        /// <summary>
        /// Five key
        /// </summary>
        [Description("5")]
        FIVE = 22,

        /// <summary>
        /// Six key
        /// </summary>
        [Description("6")]
        SIX = 23,

        /// <summary>
        /// Seven key
        /// </summary>
        [Description("7")]
        SEVEN = 24,

        /// <summary>
        /// Eight key
        /// </summary>
        [Description("8")]
        EIGHT = 25,

        /// <summary>
        /// Nine key
        /// </summary>
        [Description("9")]
        NINE = 26,

        /// <summary>
        /// Zero key
        /// </summary>
        [Description("0")]
        ZERO = 27,

        /// <summary>
        /// Minus key
        /// </summary>
        [Description("-")]
        MINUS = 28,

        /// <summary>
        /// Equals key
        /// </summary>
        [Description("=")]
        EQUALS = 29,

        /// <summary>
        /// OEM 6 key
        /// </summary>
        [Description("OEM 6")]
        OEM6 = 169,

        /// <summary>
        /// Backspace key
        /// </summary>
        [Description("Backspace")]
        BACKSPACE = 30,

        /// <summary>
        /// Insert key
        /// </summary>
        [Description("Insert")]
        INSERT = 31,

        /// <summary>
        /// Home key
        /// </summary>
        [Description("Home")]
        HOME = 32,

        /// <summary>
        /// Page up key
        /// </summary>
        [Description("Page Up")]
        PAGE_UP = 33,

        /// <summary>
        /// Numpad Lock key
        /// </summary>
        [Description("Numpad Lock")]
        NUM_LOCK = 34,

        /// <summary>
        /// Numpad divide key
        /// </summary>
        [Description("Numpad /")]
        NUM_SLASH = 35,

        /// <summary>
        /// Numpad multiply key
        /// </summary>
        [Description("Numpad *")]
        NUM_ASTERISK = 36,

        /// <summary>
        /// Numpad minus key
        /// </summary>
        [Description("Numpad -")]
        NUM_MINUS = 37,


        /// <summary>
        /// Tab key
        /// </summary>
        [Description("Tab")]
        TAB = 38,

        /// <summary>
        /// Q key
        /// </summary>
        [Description("Q")]
        Q = 39,

        /// <summary>
        /// W key
        /// </summary>
        [Description("W")]
        W = 40,

        /// <summary>
        /// E key
        /// </summary>
        [Description("E")]
        E = 41,

        /// <summary>
        /// R key
        /// </summary>
        [Description("R")]
        R = 42,

        /// <summary>
        /// T key
        /// </summary>
        [Description("T")]
        T = 43,

        /// <summary>
        /// Y key
        /// </summary>
        [Description("Y")]
        Y = 44,

        /// <summary>
        /// U key
        /// </summary>
        [Description("U")]
        U = 45,

        /// <summary>
        /// I key
        /// </summary>
        [Description("I")]
        I = 46,

        /// <summary>
        /// O key
        /// </summary>
        [Description("O")]
        O = 47,

        /// <summary>
        /// P key
        /// </summary>
        [Description("P")]
        P = 48,

        /// <summary>
        /// OEM 1 key
        /// </summary>
        [Description("OEM 1")]
        OEM1 = 170,

        /// <summary>
        /// Open Bracket key
        /// </summary>
        [Description("{")]
        OPEN_BRACKET = 49,

        /// <summary>
        /// OEM Plus key
        /// </summary>
        [Description("OEM Plus")]
        OEMPlus = 171,

        /// <summary>
        /// Close Bracket key
        /// </summary>
        [Description("}")]
        CLOSE_BRACKET = 50,

        /// <summary>
        /// Backslash key
        /// </summary>
        [Description("\\")]
        BACKSLASH = 51,

        /// <summary>
        /// Delete key
        /// </summary>
        [Description("Delete")]
        DELETE = 52,

        /// <summary>
        /// End key
        /// </summary>
        [Description("End")]
        END = 53,

        /// <summary>
        /// Page down key
        /// </summary>
        [Description("Page Down")]
        PAGE_DOWN = 54,

        /// <summary>
        /// Numpad seven key
        /// </summary>
        [Description("Numpad 7")]
        NUM_SEVEN = 55,

        /// <summary>
        /// Numpad eight key
        /// </summary>
        [Description("Numpad 8")]
        NUM_EIGHT = 56,

        /// <summary>
        /// Numpad nine key
        /// </summary>
        [Description("Numpad 9")]
        NUM_NINE = 57,

        /// <summary>
        /// Numpad add key
        /// </summary>
        [Description("Numpad +")]
        NUM_PLUS = 58,


        /// <summary>
        /// Caps Lock key
        /// </summary>
        [Description("Caps Lock")]
        CAPS_LOCK = 59,

        /// <summary>
        /// A key
        /// </summary>
        [Description("A")]
        A = 60,

        /// <summary>
        /// S key
        /// </summary>
        [Description("S")]
        S = 61,

        /// <summary>
        /// D key
        /// </summary>
        [Description("D")]
        D = 62,

        /// <summary>
        /// F key
        /// </summary>
        [Description("F")]
        F = 63,

        /// <summary>
        /// G key
        /// </summary>
        [Description("G")]
        G = 64,

        /// <summary>
        /// H key
        /// </summary>
        [Description("H")]
        H = 65,

        /// <summary>
        /// J key
        /// </summary>
        [Description("J")]
        J = 66,

        /// <summary>
        /// K key
        /// </summary>
        [Description("K")]
        K = 67,

        /// <summary>
        /// L key
        /// </summary>
        [Description("L")]
        L = 68,

        /// <summary>
        /// OEM Tilde key
        /// </summary>
        [Description("OEM Tilde")]
        OEMTilde = 157,

        /// <summary>
        /// Semicolon key
        /// </summary>
        [Description("Semicolon")]
        SEMICOLON = 69,

        /// <summary>
        /// Apostrophe key
        /// </summary>
        [Description("Apostrophe")]
        APOSTROPHE = 70,

        /// <summary>
        /// Hashtag key
        /// </summary>
        [Description("#")]
        HASHTAG = 71,

        /// <summary>
        /// Enter key
        /// </summary>
        [Description("Enter")]
        ENTER = 72,

        /// <summary>
        /// Numpad four key
        /// </summary>
        [Description("Numpad 4")]
        NUM_FOUR = 73,

        /// <summary>
        /// Numpad five key
        /// </summary>
        [Description("Numpad 5")]
        NUM_FIVE = 74,

        /// <summary>
        /// Numpad six key
        /// </summary>
        [Description("Numpad 6")]
        NUM_SIX = 75,


        /// <summary>
        /// Left Shift key
        /// </summary>
        [Description("Left Shift")]
        LEFT_SHIFT = 76,

        /// <summary>
        /// Non-US Backslash key
        /// </summary>
        [Description("Non-US Backslash")]
        BACKSLASH_UK = 77,

        /// <summary>
        /// Z key
        /// </summary>
        [Description("Z")]
        Z = 78,

        /// <summary>
        /// X key
        /// </summary>
        [Description("X")]
        X = 79,

        /// <summary>
        /// C key
        /// </summary>
        [Description("C")]
        C = 80,

        /// <summary>
        /// V key
        /// </summary>
        [Description("V")]
        V = 81,

        /// <summary>
        /// B key
        /// </summary>
        [Description("B")]
        B = 82,

        /// <summary>
        /// N key
        /// </summary>
        [Description("N")]
        N = 83,

        /// <summary>
        /// M key
        /// </summary>
        [Description("M")]
        M = 84,

        /// <summary>
        /// Comma key
        /// </summary>
        [Description("Comma")]
        COMMA = 85,

        /// <summary>
        /// Period key
        /// </summary>
        [Description("Period")]
        PERIOD = 86,

        /// <summary>
        /// Forward Slash key
        /// </summary>
        [Description("Forward Slash")]
        FORWARD_SLASH = 87,

        /// <summary>
        /// OEM 8 key
        /// </summary>
        [Description("OEM 8")]
        OEM8 = 158,

        /// <summary>
        /// OEM 102 key
        /// </summary>
        [Description("OEM 102")]
        OEM102 = 159,

        /// <summary>
        /// Right Shift key
        /// </summary>
        [Description("Right Shift")]
        RIGHT_SHIFT = 88,

        /// <summary>
        /// Arrow Up key
        /// </summary>
        [Description("Arrow Up")]
        ARROW_UP = 89,

        /// <summary>
        /// Numpad one key
        /// </summary>
        [Description("Numpad 1")]
        NUM_ONE = 90,

        /// <summary>
        /// Numpad two key
        /// </summary>
        [Description("Numpad 2")]
        NUM_TWO = 91,

        /// <summary>
        /// Numpad three key
        /// </summary>
        [Description("Numpad 3")]
        NUM_THREE = 92,

        /// <summary>
        /// Numpad enter key
        /// </summary>
        [Description("Numpad Enter")]
        NUM_ENTER = 93,


        /// <summary>
        /// Left Control key
        /// </summary>
        [Description("Left Control")]
        LEFT_CONTROL = 94,

        /// <summary>
        /// Left Windows key
        /// </summary>
        [Description("Left Windows Key")]
        LEFT_WINDOWS = 95,

        /// <summary>
        /// Left Alt key
        /// </summary>
        [Description("Left Alt")]
        LEFT_ALT = 96,

        /// <summary>
        /// Non-conversion (Japanese layout) key
        /// </summary>
        [Description("Non-conversion")]
        JPN_MUHENKAN = 153,

        /// <summary>
        /// Spacebar key
        /// </summary>
        [Description("Spacebar")]
        SPACE = 97,

        /// <summary>
        /// Conversion (Japanese layout) key
        /// </summary>
        [Description("Conversion")]
        JPN_HENKAN = 154,

        /// <summary>
        /// Hiragana/Katakana (Japanese layout) key
        /// </summary>
        [Description("Hiragana/Katakana")]
        JPN_HIRAGANA_KATAKANA = 155,

        /// <summary>
        /// Right Alt key
        /// </summary>
        [Description("Right Alt")]
        RIGHT_ALT = 98,

        /// <summary>
        /// Right Windows key
        /// </summary>
        [Description("Right Windows Key")]
        RIGHT_WINDOWS = 99,

        /// <summary>
        /// Application Select key
        /// </summary>
        [Description("Application Select Key")]
        APPLICATION_SELECT = 100,

        /// <summary>
        /// Right Control key
        /// </summary>
        [Description("Right Control")]
        RIGHT_CONTROL = 101,

        /// <summary>
        /// Arrow Left key
        /// </summary>
        [Description("Arrow Left")]
        ARROW_LEFT = 102,

        /// <summary>
        /// Arrow Down key
        /// </summary>
        [Description("Arrow Down")]
        ARROW_DOWN = 103,

        /// <summary>
        /// Arrow Right key
        /// </summary>
        [Description("Arrow Right")]
        ARROW_RIGHT = 104,

        /// <summary>
        /// Numpad zero key
        /// </summary>
        [Description("Numpad 0")]
        NUM_ZERO = 105,

        /// <summary>
        /// Numpad period key
        /// </summary>
        [Description("Numpad Period")]
        NUM_PERIOD = 106,


        /// <summary>
        /// Function key
        /// </summary>
        [Description("FN Key")]
        FN_Key = 107,


        /// <summary>
        /// Macrokey 1 key
        /// </summary>
        [Description("G1")]
        G1 = 108,

        /// <summary>
        /// Macrokey 2 key
        /// </summary>
        [Description("G2")]
        G2 = 109,

        /// <summary>
        /// Macrokey 3 key
        /// </summary>
        [Description("G3")]
        G3 = 110,

        /// <summary>
        /// Macrokey 4 key
        /// </summary>
        [Description("G4")]
        G4 = 111,

        /// <summary>
        /// Macrokey 5 key
        /// </summary>
        [Description("G5")]
        G5 = 112,

        /// <summary>
        /// Macrokey 6 key
        /// </summary>
        [Description("G6")]
        G6 = 113,

        /// <summary>
        /// Macrokey 7 key
        /// </summary>
        [Description("G7")]
        G7 = 114,

        /// <summary>
        /// Macrokey 8 key
        /// </summary>
        [Description("G8")]
        G8 = 115,

        /// <summary>
        /// Macrokey 9 key
        /// </summary>
        [Description("G9")]
        G9 = 116,

        /// <summary>
        /// Macrokey 10 key
        /// </summary>
        [Description("G10")]
        G10 = 117,

        /// <summary>
        /// Macrokey 11 key
        /// </summary>
        [Description("G11")]
        G11 = 118,

        /// <summary>
        /// Macrokey 12 key
        /// </summary>
        [Description("G12")]
        G12 = 119,

        /// <summary>
        /// Macrokey 13 key
        /// </summary>
        [Description("G13")]
        G13 = 120,

        /// <summary>
        /// Macrokey 14 key
        /// </summary>
        [Description("G14")]
        G14 = 121,

        /// <summary>
        /// Macrokey 15 key
        /// </summary>
        [Description("G15")]
        G15 = 122,

        /// <summary>
        /// Macrokey 16 key
        /// </summary>
        [Description("G16")]
        G16 = 123,

        /// <summary>
        /// Macrokey 17 key
        /// </summary>
        [Description("G17")]
        G17 = 124,

        /// <summary>
        /// Macrokey 18 key
        /// </summary>
        [Description("G18")]
        G18 = 125,

        /// <summary>
        /// Macrokey 19 key
        /// </summary>
        [Description("G19")]
        G19 = 126,

        /// <summary>
        /// Macrokey 20 key
        /// </summary>
        [Description("G20")]
        G20 = 127,


        /// <summary>
        /// Brand Logo
        /// </summary>
        [Description("Brand Logo")]
        LOGO = 128,

        /// <summary>
        /// Brand Logo #2
        /// </summary>
        [Description("Brand Logo #2")]
        LOGO2 = 129,

        /// <summary>
        /// Brand Logo #3
        /// </summary>
        [Description("Brand Logo #3")]
        LOGO3 = 130,

        /// <summary>
        /// Brightness Switch
        /// </summary>
        [Description("Brightness Switch")]
        BRIGHTNESS_SWITCH = 131,

        /// <summary>
        /// Lock Switch
        /// </summary>
        [Description("Lock Switch")]
        LOCK_SWITCH = 132,


        /// <summary>
        /// Multimedia Play/Pause
        /// </summary>
        [Description("Media Play/Pause")]
        MEDIA_PLAY_PAUSE = 133,

        /// <summary>
        /// Multimedia Play
        /// </summary>
        [Description("Media Play")]
        MEDIA_PLAY = 134,

        /// <summary>
        /// Multimedia Pause
        /// </summary>
        [Description("Media Pause")]
        MEDIA_PAUSE = 135,

        /// <summary>
        /// Multimedia Stop
        /// </summary>
        [Description("Media Stop")]
        MEDIA_STOP = 136,

        /// <summary>
        /// Multimedia Previous
        /// </summary>
        [Description("Media Previous")]
        MEDIA_PREVIOUS = 137,

        /// <summary>
        /// Multimedia Next
        /// </summary>
        [Description("Media Next")]
        MEDIA_NEXT = 138,


        /// <summary>
        /// Volume Mute
        /// </summary>
        [Description("Volume Mute")]
        VOLUME_MUTE = 139,

        /// <summary>
        /// Volume Down
        /// </summary>
        [Description("Volume Down")]
        VOLUME_DOWN = 140,

        /// <summary>
        /// Volume Up
        /// </summary>
        [Description("Volume Up")]
        VOLUME_UP = 141,


        /// <summary>
        /// Additional Light 1
        /// </summary>
        [Description("Additional Light 1")]
        ADDITIONALLIGHT1 = 142,

        /// <summary>
        /// Additional Light 2
        /// </summary>
        [Description("Additional Light 2")]
        ADDITIONALLIGHT2 = 143,

        /// <summary>
        /// Additional Light 3
        /// </summary>
        [Description("Additional Light 3")]
        ADDITIONALLIGHT3 = 144,

        /// <summary>
        /// Additional Light 4
        /// </summary>
        [Description("Additional Light 4")]
        ADDITIONALLIGHT4 = 145,

        /// <summary>
        /// Additional Light 5
        /// </summary>
        [Description("Additional Light 5")]
        ADDITIONALLIGHT5 = 146,

        /// <summary>
        /// Additional Light 6
        /// </summary>
        [Description("Additional Light 6")]
        ADDITIONALLIGHT6 = 147,

        /// <summary>
        /// Additional Light 7
        /// </summary>
        [Description("Additional Light 7")]
        ADDITIONALLIGHT7 = 148,

        /// <summary>
        /// Additional Light 8
        /// </summary>
        [Description("Additional Light 8")]
        ADDITIONALLIGHT8 = 149,

        /// <summary>
        /// Additional Light 9
        /// </summary>
        [Description("Additional Light 9")]
        ADDITIONALLIGHT9 = 150,

        /// <summary>
        /// Additional Light 10
        /// </summary>
        [Description("Additional Light 10")]
        ADDITIONALLIGHT10 = 151,

        /// <summary>
        /// Peripheral Logo
        /// </summary>
        [Description("Peripheral Logo")]
        Peripheral_Logo = 160,

        /// <summary>
        /// Peripheral Scroll Wheel
        /// </summary>
        [Description("Peripheral Scroll Wheel")]
        Peripheral_ScrollWheel = 161,

        /// <summary>
        /// Peripheral Front-facing lights
        /// </summary>
        [Description("Peripheral Front Lights")]
        Peripheral_FrontLight = 162,

        /// <summary>
        /// Profile key 1
        /// </summary>
        [Description("Profile Key 1")]
        Profile_Key1 = 163,

        /// <summary>
        /// Profile key 2
        /// </summary>
        [Description("Profile Key 2")]
        Profile_Key2 = 164,

        /// <summary>
        /// Profile key 3
        /// </summary>
        [Description("Profile Key 3")]
        Profile_Key3 = 165,

        /// <summary>
        /// Profile key 4
        /// </summary>
        [Description("Profile Key 4")]
        Profile_Key4 = 166,

        /// <summary>
        /// Profile key 5
        /// </summary>
        [Description("Profile Key 5")]
        Profile_Key5 = 167,

        /// <summary>
        /// Profile key 6
        /// </summary>
        [Description("Profile Key 6")]
        Profile_Key6 = 168,

        /// <summary>
        /// Numpad 00
        /// </summary>
        [Description("Numpad 00")]
        NUM_ZEROZERO = 169,

        /// <summary>
        /// Macrokey 0 key
        /// </summary>
        [Description("G0")]
        G0 = 170,

        /// <summary>
        /// Macrokey 0 key
        /// </summary>
        [Description("Left FN")]
        LEFT_FN = 171,

        /// <summary>
        /// Additional Light 11
        /// </summary>
        [Description("Additional Light 11")]
        ADDITIONALLIGHT11 = 172,

        /// <summary>
        /// Additional Light 12
        /// </summary>
        [Description("Additional Light 12")]
        ADDITIONALLIGHT12 = 173,

        /// <summary>
        /// Additional Light 13
        /// </summary>
        [Description("Additional Light 13")]
        ADDITIONALLIGHT13 = 174,

        /// <summary>
        /// Additional Light 14
        /// </summary>
        [Description("Additional Light 14")]
        ADDITIONALLIGHT14 = 175,

        /// <summary>
        /// Additional Light 15
        /// </summary>
        [Description("Additional Light 15")]
        ADDITIONALLIGHT15 = 176,

        /// <summary>
        /// Additional Light 16
        /// </summary>
        [Description("Additional Light 16")]
        ADDITIONALLIGHT16 = 177,

        /// <summary>
        /// Additional Light 17
        /// </summary>
        [Description("Additional Light 17")]
        ADDITIONALLIGHT17 = 178,

        /// <summary>
        /// Additional Light 18
        /// </summary>
        [Description("Additional Light 18")]
        ADDITIONALLIGHT18 = 179,

        /// <summary>
        /// Additional Light 19
        /// </summary>
        [Description("Additional Light 19")]
        ADDITIONALLIGHT19 = 180,

        /// <summary>
        /// Additional Light 20
        /// </summary>
        [Description("Additional Light 20")]
        ADDITIONALLIGHT20 = 181,

        /// <summary>
        /// Additional Light 21
        /// </summary>
        [Description("Additional Light 21")]
        ADDITIONALLIGHT21 = 182,

        /// <summary>
        /// Additional Light 22
        /// </summary>
        [Description("Additional Light 22")]
        ADDITIONALLIGHT22 = 183,

        /// <summary>
        /// Additional Light 23
        /// </summary>
        [Description("Additional Light 23")]
        ADDITIONALLIGHT23 = 184,

        /// <summary>
        /// Additional Light 24
        /// </summary>
        [Description("Additional Light 24")]
        ADDITIONALLIGHT24 = 185,

        /// <summary>
        /// Additional Light 25
        /// </summary>
        [Description("Additional Light 25")]
        ADDITIONALLIGHT25 = 186,

        /// <summary>
        /// Additional Light 26
        /// </summary>
        [Description("Additional Light 26")]
        ADDITIONALLIGHT26 = 187,

        /// <summary>
        /// Additional Light 27
        /// </summary>
        [Description("Additional Light 27")]
        ADDITIONALLIGHT27 = 188,

        /// <summary>
        /// Additional Light 28
        /// </summary>
        [Description("Additional Light 28")]
        ADDITIONALLIGHT28 = 189,

        /// <summary>
        /// Additional Light 29
        /// </summary>
        [Description("Additional Light 29")]
        ADDITIONALLIGHT29 = 190,

        /// <summary>
        /// Additional Light 30
        /// </summary>
        [Description("Additional Light 30")]
        ADDITIONALLIGHT30 = 191,

        /// <summary>
        /// Additional Light 31
        /// </summary>
        [Description("Additional Light 31")]
        ADDITIONALLIGHT31 = 192,

        /// <summary>
        /// Additional Light 32
        /// </summary>
        [Description("Additional Light 32")]
        ADDITIONALLIGHT32 = 193,

        /// <summary>
        /// Mousepad Light 1
        /// </summary>
        [Description("Mousepad Light 1")]
        MOUSEPADLIGHT1 = 201,

        /// <summary>
        /// Mousepad Light 2
        /// </summary>
        [Description("Mousepad Light 2")]
        MOUSEPADLIGHT2 = 202,

        /// <summary>
        /// Mousepad Light 1
        /// </summary>
        [Description("Mousepad Light 3")]
        MOUSEPADLIGHT3 = 203,

        /// <summary>
        /// Mousepad Light 2
        /// </summary>
        [Description("Mousepad Light 4")]
        MOUSEPADLIGHT4 = 204,

        /// <summary>
        /// Mousepad Light 1
        /// </summary>
        [Description("Mousepad Light 5")]
        MOUSEPADLIGHT5 = 205,

        /// <summary>
        /// Mousepad Light 2
        /// </summary>
        [Description("Mousepad Light 6")]
        MOUSEPADLIGHT6 = 206,

        /// <summary>
        /// Mousepad Light 1
        /// </summary>
        [Description("Mousepad Light 7")]
        MOUSEPADLIGHT7 = 207,

        /// <summary>
        /// Mousepad Light 2
        /// </summary>
        [Description("Mousepad Light 8")]
        MOUSEPADLIGHT8 = 208,

        /// <summary>
        /// Mousepad Light 1
        /// </summary>
        [Description("Mousepad Light 9")]
        MOUSEPADLIGHT9 = 209,

        /// <summary>
        /// Mousepad Light 2
        /// </summary>
        [Description("Mousepad Light 10")]
        MOUSEPADLIGHT10 = 210,

        /// <summary>
        /// Mousepad Light 2
        /// </summary>
        [Description("Mousepad Light 11")]
        MOUSEPADLIGHT11 = 211,

        /// <summary>
        /// Mousepad Light 1
        /// </summary>
        [Description("Mousepad Light 12")]
        MOUSEPADLIGHT12 = 212,

        /// <summary>
        /// Mousepad Light 2
        /// </summary>
        [Description("Mousepad Light 13")]
        MOUSEPADLIGHT13 = 213,

        /// <summary>
        /// Mousepad Light 1
        /// </summary>
        [Description("Mousepad Light 14")]
        MOUSEPADLIGHT14 = 214,

        /// <summary>
        /// Mousepad Light 2
        /// </summary>
        [Description("Mousepad Light 15")]
        MOUSEPADLIGHT15 = 215,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Calculator")]
        CALC = 216,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("G560 Front Left")]
        G560_FRONT_LEFT = 300,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("G560 Front Right")]
        G560_FRONT_RIGHT = 301,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("G560 Rear Left")]
        G560_REAR_LEFT = 302,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("G560 Rear Right")]
        G560_REAR_RIGHT = 303,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 0")]
        LEDSTRIPLIGHT1_0 = 600,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 1")]
        LEDSTRIPLIGHT1_1 = 601,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 2")]
        LEDSTRIPLIGHT1_2 = 602,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 3")]
        LEDSTRIPLIGHT1_3 = 603,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 4")]
        LEDSTRIPLIGHT1_4 = 604,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 5")]
        LEDSTRIPLIGHT1_5 = 605,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 6")]
        LEDSTRIPLIGHT1_6 = 606,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 7")]
        LEDSTRIPLIGHT1_7 = 607,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 8")]
        LEDSTRIPLIGHT1_8 = 608,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 9")]
        LEDSTRIPLIGHT1_9 = 609,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 10")]
        LEDSTRIPLIGHT1_10 = 610,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 11")]
        LEDSTRIPLIGHT1_11 = 611,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 12")]
        LEDSTRIPLIGHT1_12 = 612,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 13")]
        LEDSTRIPLIGHT1_13 = 613,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 14")]
        LEDSTRIPLIGHT1_14 = 614,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 15")]
        LEDSTRIPLIGHT1_15 = 615,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 16")]
        LEDSTRIPLIGHT1_16 = 616,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 17")]
        LEDSTRIPLIGHT1_17 = 617,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 18")]
        LEDSTRIPLIGHT1_18 = 618,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 19")]
        LEDSTRIPLIGHT1_19 = 619,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 20")]
        LEDSTRIPLIGHT1_20 = 620,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 21")]
        LEDSTRIPLIGHT1_21 = 621,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 22")]
        LEDSTRIPLIGHT1_22 = 622,


        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 23")]
        LEDSTRIPLIGHT1_23 = 623,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 24")]
        LEDSTRIPLIGHT1_24 = 624,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 25")]
        LEDSTRIPLIGHT1_25 = 625,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 26")]
        LEDSTRIPLIGHT1_26 = 626,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 27")]
        LEDSTRIPLIGHT1_27 = 627,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 28")]
        LEDSTRIPLIGHT1_28 = 628,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 29")]
        LEDSTRIPLIGHT1_29 = 629,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 30")]
        LEDSTRIPLIGHT1_30 = 630,


        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 31")]
        LEDSTRIPLIGHT1_31 = 631,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 32")]
        LEDSTRIPLIGHT1_32 = 632,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 33")]
        LEDSTRIPLIGHT1_33 = 633,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 34")]
        LEDSTRIPLIGHT1_34 = 634,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 35")]
        LEDSTRIPLIGHT1_35 = 635,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 36")]
        LEDSTRIPLIGHT1_36 = 636,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 37")]
        LEDSTRIPLIGHT1_37 = 637,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 38")]
        LEDSTRIPLIGHT1_38 = 638,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 39")]
        LEDSTRIPLIGHT1_39 = 639,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 40")]
        LEDSTRIPLIGHT1_40 = 640,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 41")]
        LEDSTRIPLIGHT1_41 = 641,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 42")]
        LEDSTRIPLIGHT1_42 = 642,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 43")]
        LEDSTRIPLIGHT1_43 = 643,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 44")]
        LEDSTRIPLIGHT1_44 = 644,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 45")]
        LEDSTRIPLIGHT1_45 = 645,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 46")]
        LEDSTRIPLIGHT1_46 = 646,


        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 47")]
        LEDSTRIPLIGHT1_47 = 647,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 48")]
        LEDSTRIPLIGHT1_48 = 648,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 49")]
        LEDSTRIPLIGHT1_49 = 649,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 50")]
        LEDSTRIPLIGHT1_50 = 650,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 51")]
        LEDSTRIPLIGHT1_51 = 651,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 52")]
        LEDSTRIPLIGHT1_52 = 652,


        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 53")]
        LEDSTRIPLIGHT1_53 = 653,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 54")]
        LEDSTRIPLIGHT1_54 = 654,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 55")]
        LEDSTRIPLIGHT1_55 = 655,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 56")]
        LEDSTRIPLIGHT1_56 = 656,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 57")]
        LEDSTRIPLIGHT1_57 = 657,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 58")]
        LEDSTRIPLIGHT1_58 = 658,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 59")]

        LEDSTRIPLIGHT1_59 = 659,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 60")]
        LEDSTRIPLIGHT1_60 = 660,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 61")]
        LEDSTRIPLIGHT1_61 = 661,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 62")]
        LEDSTRIPLIGHT1_62 = 662,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 1 LED 63")]
        LEDSTRIPLIGHT1_63 = 663,
       

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 0")]
        LEDSTRIPLIGHT2_0 = 700,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 1")]
        LEDSTRIPLIGHT2_1 = 701,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 2")]
        LEDSTRIPLIGHT2_2 = 702,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 3")]
        LEDSTRIPLIGHT2_3 = 703,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 4")]
        LEDSTRIPLIGHT2_4 = 704,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 5")]
        LEDSTRIPLIGHT2_5 = 705,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 6")]
        LEDSTRIPLIGHT2_6 = 706,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 7")]
        LEDSTRIPLIGHT2_7 = 707,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 8")]
        LEDSTRIPLIGHT2_8 = 708,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 9")]
        LEDSTRIPLIGHT2_9 = 709,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 10")]
        LEDSTRIPLIGHT2_10 = 710,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 11")]
        LEDSTRIPLIGHT2_11 = 711,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 12")]
        LEDSTRIPLIGHT2_12 = 712,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 13")]
        LEDSTRIPLIGHT2_13 = 713,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 14")]
        LEDSTRIPLIGHT2_14 = 714,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 15")]
        LEDSTRIPLIGHT2_15 = 715,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 16")]
        LEDSTRIPLIGHT2_16 = 716,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 17")]
        LEDSTRIPLIGHT2_17 = 717,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 18")]
        LEDSTRIPLIGHT2_18 = 718,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 19")]
        LEDSTRIPLIGHT2_19 = 719,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 20")]
        LEDSTRIPLIGHT2_20 = 720,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 21")]
        LEDSTRIPLIGHT2_21 = 721,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 22")]
        LEDSTRIPLIGHT2_22 = 722,


        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 23")]
        LEDSTRIPLIGHT2_23 = 723,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 24")]
        LEDSTRIPLIGHT2_24 = 724,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 25")]
        LEDSTRIPLIGHT2_25 = 725,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 26")]
        LEDSTRIPLIGHT2_26 = 726,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 27")]
        LEDSTRIPLIGHT2_27 = 727,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 28")]
        LEDSTRIPLIGHT2_28 = 728,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 29")]
        LEDSTRIPLIGHT2_29 = 729,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 30")]
        LEDSTRIPLIGHT2_30 = 730,


        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 31")]
        LEDSTRIPLIGHT2_31 = 731,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 32")]
        LEDSTRIPLIGHT2_32 = 732,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 33")]
        LEDSTRIPLIGHT2_33 = 733,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 34")]
        LEDSTRIPLIGHT2_34 = 734,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 35")]
        LEDSTRIPLIGHT2_35 = 735,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 36")]
        LEDSTRIPLIGHT2_36 = 736,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 37")]
        LEDSTRIPLIGHT2_37 = 737,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 38")]
        LEDSTRIPLIGHT2_38 = 738,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 39")]
        LEDSTRIPLIGHT2_39 = 739,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 40")]
        LEDSTRIPLIGHT2_40 = 740,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 41")]
        LEDSTRIPLIGHT2_41 = 741,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 42")]
        LEDSTRIPLIGHT2_42 = 742,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 43")]
        LEDSTRIPLIGHT2_43 = 743,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 44")]
        LEDSTRIPLIGHT2_44 = 744,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 45")]
        LEDSTRIPLIGHT2_45 = 745,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 46")]
        LEDSTRIPLIGHT2_46 = 746,


        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 47")]
        LEDSTRIPLIGHT2_47 = 747,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 48")]
        LEDSTRIPLIGHT2_48 = 748,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 49")]
        LEDSTRIPLIGHT2_49 = 749,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 50")]
        LEDSTRIPLIGHT2_50 = 750,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 51")]
        LEDSTRIPLIGHT2_51 = 751,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 52")]
        LEDSTRIPLIGHT2_52 = 752,


        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 53")]
        LEDSTRIPLIGHT2_53 = 753,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 54")]
        LEDSTRIPLIGHT2_54 = 754,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 55")]
        LEDSTRIPLIGHT2_55 = 755,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 56")]
        LEDSTRIPLIGHT2_56 = 756,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 57")]
        LEDSTRIPLIGHT2_57 = 757,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 58")]
        LEDSTRIPLIGHT2_58 = 758,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 59")]

        LEDSTRIPLIGHT2_59 = 759,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 60")]
        LEDSTRIPLIGHT2_60 = 760,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 61")]
        LEDSTRIPLIGHT2_61 = 761,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 62")]
        LEDSTRIPLIGHT2_62 = 762,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 63")]
        LEDSTRIPLIGHT2_63 = 763,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 64")]
        LEDSTRIPLIGHT2_64 = 764,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 65")]
        LEDSTRIPLIGHT2_65 = 765,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 66")]
        LEDSTRIPLIGHT2_66 = 766,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 67")]
        LEDSTRIPLIGHT2_67 = 767,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 68")]
        LEDSTRIPLIGHT2_68 = 768,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 69")]
        LEDSTRIPLIGHT2_69= 769,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 70")]
        LEDSTRIPLIGHT2_70 = 770,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 71")]
        LEDSTRIPLIGHT2_71 = 771,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 72")]
        LEDSTRIPLIGHT2_72 = 772,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 73")]
        LEDSTRIPLIGHT2_73 = 773,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 74")]
        LEDSTRIPLIGHT2_74 = 774,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 75")]
        LEDSTRIPLIGHT2_75 = 775,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Led strip 2 LED 76")]
        LEDSTRIPLIGHT2_76 = 776,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Mainboard area 0")]
        MBAREA_0 = 800,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Mainboard area 1")]
        MBAREA_1 = 801,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Mainboard area 2")]
        MBAREA_2 = 802,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Mainboard area 3")]
        MBAREA_3 = 803,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Mainboard area 4")]
        MBAREA_4 = 804,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Mainboard area 5")]
        MBAREA_5 = 805,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Mainboard area 6")]
        MBAREA_6 = 806,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Mainboard area 7")]
        MBAREA_7 = 807,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Mainboard area 8")]
        MBAREA_8 = 808,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Mainboard area 9")]
        MBAREA_9 = 809,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Mainboard area 10")]
        MBAREA_10 = 810,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 1")]
        DLEDSTRIP_1 = 811,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 2")]
        DLEDSTRIP_2 = 812,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 3")]
        DLEDSTRIP_3 = 813,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 4")]
        DLEDSTRIP_4 = 814,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 5")]
        DLEDSTRIP_5 = 815,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 6")]
        DLEDSTRIP_6 = 816,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 7")]
        DLEDSTRIP_7 = 817,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 8")]
        DLEDSTRIP_8 = 818,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 9")]
        DLEDSTRIP_9 = 819,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 10")]
        DLEDSTRIP_10 = 820,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 11")]
        DLEDSTRIP_11 = 821,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 12")]
        DLEDSTRIP_12 = 822,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 13")]
        DLEDSTRIP_13 = 823,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 14")]
        DLEDSTRIP_14 = 824,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 15")]
        DLEDSTRIP_15 = 825,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 16")]
        DLEDSTRIP_16 = 826,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 17")]
        DLEDSTRIP_17 = 827,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 18")]
        DLEDSTRIP_18 = 828,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 18")]
        DLEDSTRIP_19 = 829,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 18")]
        DLEDSTRIP_20 = 830,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 18")]
        DLEDSTRIP_21 = 831,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 18")]
        DLEDSTRIP_22 = 832,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 18")]
        DLEDSTRIP_23 = 833,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 18")]
        DLEDSTRIP_24 = 834,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 18")]
        DLEDSTRIP_25 = 835,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 18")]
        DLEDSTRIP_26 = 836,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 18")]
        DLEDSTRIP_27 = 837,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 18")]
        DLEDSTRIP_28 = 838,
        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("RGBFusion DLED 18")]
        DLEDSTRIP_29 = 839,

        ///<summary>
        /// Calculator Key
        /// </summary>
        [Description("Ambiente")]
        AMBIENT = 890,

        /// <summary>
        /// None
        /// </summary>
        [Description("None")]
        NONE = -1,
    };

    /// <summary>
    /// Struct representing color settings being sent to devices
    /// </summary>
    public class DeviceColorComposition
    {
        public readonly object bitmapLock = new object();
        public Dictionary<DeviceKeys, Color> keyColors;
        public Bitmap keyBitmap;
    }

    /// <summary>
    /// An interface for a device class.
    /// </summary>
    public interface Device
    {
        /// <summary>
        /// Gets registered variables by this device.
        /// </summary>
        /// <returns>Registered Variables</returns>
        Settings.VariableRegistry GetRegisteredVariables();

        /// <summary>
        /// Gets the device name.
        /// </summary>
        /// <returns>Device name</returns>
        string GetDeviceName();

        /// <summary>
        /// Gets specific details about the device instance.
        /// </summary>
        /// <returns>Details about the device instance</returns>
        string GetDeviceDetails();

        /// <summary>
        /// Gets the device update performance.
        /// </summary>
        /// <returns>Details about device's update performance</returns>
        string GetDeviceUpdatePerformance();

        /// <summary>
        /// Attempts to initialize the device instance.
        /// </summary>
        /// <returns>A boolean value representing the success of this call</returns>
        bool Initialize();

        /// <summary>
        /// Shuts down the device instance.
        /// </summary>
        void Shutdown();

        /// <summary>
        /// Resets the device instance.
        /// </summary>
        void Reset();

        /// <summary>
        /// Attempts to reconnect the device. [NOT IMPLEMENTED]
        /// </summary>
        /// <returns>A boolean value representing the success of this call</returns>
        bool Reconnect();

        /// <summary>
        /// Gets the initialization status of this device instance.
        /// </summary>
        /// <returns>A boolean value representing the initialization status of this device</returns>
        bool IsInitialized();

        /// <summary>
        /// Gets the connection status of this device instance. [NOT IMPLEMENTED]
        /// </summary>
        /// <returns>A boolean value representing the connection status of this device</returns>
        bool IsConnected();

        /// <summary>
        /// Gets the keyboard connection status for this device instance.
        /// </summary>
        /// <returns>A boolean value representing the keyboard connection status of this device</returns>
        bool IsKeyboardConnected();

        /// <summary>
        /// Gets the peripheral connection status for this device instance.
        /// </summary>
        /// <returns>A boolean value representing the peripheral connection status of this device</returns>
        bool IsPeripheralConnected();

        /// <summary>
        /// Updates the device with a specified color arrangement.
        /// </summary>
        /// <param name="keyColors">A dictionary of DeviceKeys their corresponding Colors</param>
        /// <param name="forced">A boolean value indicating whether or not to forcefully update this device</param>
        /// <returns></returns>
        bool UpdateDevice(Dictionary<DeviceKeys, Color> keyColors, DoWorkEventArgs e, bool forced = false);

        /// <summary>
        /// Updates the device with a specified color composition.
        /// </summary>
        /// <param name="colorComposition">A struct containing a dictionary of colors as well as the resulting bitmap</param>
        /// <param name="forced">A boolean value indicating whether or not to forcefully update this device</param>
        /// <returns></returns>
        bool UpdateDevice(DeviceColorComposition colorComposition, DoWorkEventArgs e, bool forced = false);
    }
}
