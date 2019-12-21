using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardApi
{
    public class VK
    {
        public const int WM_SYSKEYDOWN = 0x0104;
        public const int WM_SYSKEYUP = 0x0105;

        public const int VK_LButton = 0x1;    // 鼠标左键
        public const int VK_RButton = 0x2;    // 鼠标右键
        public const int VK_Cancel = 0x3;     // CANCEL 键
        public const int VK_MButton = 0x4;    // 鼠标中键
        public const int VK_Back = 0x8;       // BACKSPACE 键
        public const int VK_Tab = 0x9;        // TAB 键
        public const int VK_Clear = 0xC;      // CLEAR 键
        public const int VK_Enter = 0xD;     // ENTER 键
        public const int VK_Shift = 0x10;     // SHIFT 键
        public const int VK_Ctrl = 0x11;   // CTRL 键
        public const int VK_Alt = 0x12;       // Alt 键 
        public const int VK_Pause = 0x13;     // PAUSE 键
        public const int VK_Capital = 0x14;   // CAPS LOCK 键
        public const int VK_Escape = 0x1B;    // ESC 键
        public const int VK_Space = 0x20;     // SPACEBAR 键
        public const int VK_PageUp = 0x21;    // PAGE UP 键
        public const int VK_PageDown = 0x22;    // PAGE UP 键
        public const int VK_End = 0x23;       // End 键
        public const int VK_Home = 0x24;      // HOME 键
        public const int VK_Left = 0x25;      // LEFT ARROW 键
        public const int VK_Up = 0x26;        // UP ARROW 键
        public const int VK_Right = 0x27;     // RIGHT ARROW 键
        public const int VK_Down = 0x28;      // DOWN ARROW 键
        public const int VK_Select = 0x29;    // Select 键
        public const int VK_Print = 0x2A;     // PRINT SCREEN 键
        public const int VK_Execute = 0x2B;   // EXECUTE 键
        public const int VK_Tiled = 0xC0;   // EXECUTE 键

        //常用键 字母键A到Z
        public const int VK_A = 65;
        public const int VK_B = 66;
        public const int VK_C = 67;
        public const int VK_D = 68;
        public const int VK_E = 69;
        public const int VK_F = 70;
        public const int VK_G = 71;
        public const int VK_H = 72;
        public const int VK_I = 73;
        public const int VK_J = 74;
        public const int VK_K = 75;
        public const int VK_L = 76;
        public const int VK_M = 77;
        public const int VK_N = 78;
        public const int VK_O = 79;
        public const int VK_P = 80;
        public const int VK_Q = 81;
        public const int VK_R = 82;
        public const int VK_S = 83;
        public const int VK_T = 84;
        public const int VK_U = 85;
        public const int VK_V = 86;
        public const int VK_W = 87;
        public const int VK_X = 88;
        public const int VK_Y = 89;
        public const int VK_Z = 90;

        //数字键盘0到9

        public const int VK_0 = 0x30;    // 0 键
        public const int VK_1 = 0x31;    // 1 键
        public const int VK_2 = 0x32;    // 2 键
        public const int VK_3 = 0x33;    // 3 键
        public const int VK_4 = 0x34;    // 4 键
        public const int VK_5 = 0x35;    // 5 键
        public const int VK_6 = 0x36;    // 6 键
        public const int VK_7 = 0x37;    // 7 键
        public const int VK_8 = 0x38;    // 8 键
        public const int VK_9 = 0x39;    // 9 键

        public const int VK_Numpad0 = 0x60;    //0 键
        public const int VK_Numpad1 = 0x61;    //1 键
        public const int VK_Numpad2 = 0x62;    //2 键
        public const int VK_Numpad3 = 0x63;    //3 键
        public const int VK_Numpad4 = 0x64;    //4 键
        public const int VK_Numpad5 = 0x65;    //5 键
        public const int VK_Numpad6 = 0x66;    //6 键
        public const int VK_Numpad7 = 0x67;    //7 键
        public const int VK_Numpad8 = 0x68;    //8 键
        public const int VK_Numpad9 = 0x69;    //9 键
        public const int VK_Multiply = 0x6A;   // MULTIPLICATIONSIGN(*)键
        public const int VK_Add = 0x6B;        // PLUS SIGN(+) 键
        public const int VK_Separator = 0x6C;  // ENTER 键
        public const int VK_Subtract = 0x6D;   // MINUS SIGN(-) 键
        public const int VK_Decimal = 0x6E;    // DECIMAL POINT(.) 键
        public const int VK_Divide = 0x6F;     // DIVISION SIGN(/) 键

        //F1到F12按键
        public const int VK_F1 = 0x70;   //F1 键
        public const int VK_F2 = 0x71;   //F2 键
        public const int VK_F3 = 0x72;   //F3 键
        public const int VK_F4 = 0x73;   //F4 键
        public const int VK_F5 = 0x74;   //F5 键
        public const int VK_F6 = 0x75;   //F6 键
        public const int VK_F7 = 0x76;   //F7 键
        public const int VK_F8 = 0x77;   //F8 键
        public const int VK_F9 = 0x78;   //F9 键
        public const int VK_F10 = 0x79;  //F10 键
        public const int VK_F11 = 0x7A;  //F11 键
        public const int VK_F12 = 0x7B;  //F12 键
        
    }
}
