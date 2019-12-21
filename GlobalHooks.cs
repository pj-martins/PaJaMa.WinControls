using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaJaMa.WinControls
{
	public class GlobalHooks : IDisposable
	{
		private const int WH_KEYBOARD_LL = 13;
		private const int WH_MOUSE_LL = 14;

		private const int WM_KEYDOWN = 0x0100;
		private const int HC_ACTION = 0;

		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
		public static extern short GetKeyState(int keyCode);

		[DllImport("user32")]
		private static extern int GetKeyboardState(byte[] pbKeyState);

		[DllImport("user32")]
		private static extern int ToAscii(
			int uVirtKey,
			int uScanCode,
			byte[] lpbKeyState,
			byte[] lpwTransKey,
			int fuState);

		//Modifier key vkCode constants 
		private const int VK_SHIFT = 0x10;
		private const int VK_CONTROL = 0x11;
		private const int VK_MENU = 0x12;
		private const int VK_CAPITAL = 0x14;

		private Hook _keyboardHook;
		private Hook _mouseHook;

		public event System.Windows.Forms.KeyEventHandler KeyDown;
		public event KeyPressEventHandler KeyPress;

		public event System.Windows.Forms.MouseEventHandler MouseMove;
		public event System.Windows.Forms.MouseEventHandler LeftMouseDown;
		public event System.Windows.Forms.MouseEventHandler LeftMouseUp;
		public event System.Windows.Forms.MouseEventHandler RightMouseDown;
		public event System.Windows.Forms.MouseEventHandler RightMouseUp;
		//public event System.Windows.Forms.MouseEventHandler MouseWheel;

		public GlobalHooks()
		{
			_keyboardHook = new Hook(WH_KEYBOARD_LL);
			_keyboardHook.HookFired += keyboardHook_HookFired;


		}

		public void AttachMouseHook()
		{
			_mouseHook = new Hook(WH_MOUSE_LL);
			_mouseHook.HookFired += mouseHook_HookFired;
		}

		public void DeatchMouseHook()
		{
			_mouseHook.HookFired -= mouseHook_HookFired;
			_mouseHook.Dispose();
			_mouseHook = null;
		}

		private void mouseHook_HookFired(object sender, HookFiredEventArgs e)
		{
			MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(e.LParam, typeof(MSLLHOOKSTRUCT));
			// TODO: double click, mouse wheel
			CancelMouseEventArgs args = null;
			switch ((MouseMessages)e.WParam)
			{
				case MouseMessages.WM_LBUTTONDOWN:
					if (LeftMouseDown != null) LeftMouseDown(this, args = new CancelMouseEventArgs(MouseButtons.Left, 1, hookStruct.pt.x, hookStruct.pt.y, 0));
					break;
				case MouseMessages.WM_LBUTTONUP:
					if (LeftMouseUp != null) LeftMouseUp(this, args = new CancelMouseEventArgs(MouseButtons.Left, 1, hookStruct.pt.x, hookStruct.pt.y, 0));
					break;
				case MouseMessages.WM_RBUTTONDOWN:
					if (RightMouseDown != null) RightMouseDown(this, args = new CancelMouseEventArgs(MouseButtons.Right, 0, hookStruct.pt.x, hookStruct.pt.y, 0));
					break;
				case MouseMessages.WM_RBUTTONUP:
					if (RightMouseUp != null) RightMouseUp(this, args = new CancelMouseEventArgs(MouseButtons.Right, 0, hookStruct.pt.x, hookStruct.pt.y, 0));
					break;
				case MouseMessages.WM_MOUSEMOVE:
					if (MouseMove != null) MouseMove(this, args = new CancelMouseEventArgs(MouseButtons.None, 0, hookStruct.pt.x, hookStruct.pt.y, 0));
					break;
			}
			if (args != null && args.Cancel)
				e.Cancel = true;
		}

		private Keys AddModifiers(Keys key)
		{
			//CapsLock 
			if ((GetKeyState(VK_CAPITAL) & 0x0001) != 0) key = key | Keys.CapsLock;

			//Shift 
			if ((GetKeyState(VK_SHIFT) & 0x8000) != 0) key = key | Keys.Shift;

			//Ctrl 
			if ((GetKeyState(VK_CONTROL) & 0x8000) != 0) key = key | Keys.Control;

			//Alt 
			if ((GetKeyState(VK_MENU) & 0x8000) != 0) key = key | Keys.Alt;
			return key;

		}

		private void keyboardHook_HookFired(object sender, HookFiredEventArgs e)
		{
			if (e.NCode >= 0 && e.WParam == (IntPtr)WM_KEYDOWN)
			{
				int vkCode = Marshal.ReadInt32(e.LParam);
				if (KeyDown != null)
				{
					var keys = AddModifiers((Keys)vkCode);
					var args = new CancelKeyEventArgs(keys);
					KeyDown(this, args);
					if (args.Cancel) e.Cancel = true;
				}

				if (KeyPress != null)
				{
					bool isDownShift = ((GetKeyState(VK_SHIFT) & 0x80) == 0x80 ? true : false);
					bool isDownCapslock = (GetKeyState(VK_CAPITAL) != 0 ? true : false);

					KeyboardHookStruct khs = (KeyboardHookStruct)Marshal.PtrToStructure(e.LParam, typeof(KeyboardHookStruct));

					byte[] keyState = new byte[256];
					GetKeyboardState(keyState);
					byte[] inBuffer = new byte[2];
					if (ToAscii(khs.VirtualKeyCode,
							  khs.ScanCode,
							  keyState,
							  inBuffer,
							  khs.Flags) == 1)
					{
						char key = (char)inBuffer[0];
						if ((isDownCapslock ^ isDownShift) && Char.IsLetter(key)) key = Char.ToUpper(key);
						CancelKeyPressEventArgs args = new CancelKeyPressEventArgs(key);
						KeyPress(this, args);
						if (args.Cancel) e.Cancel = true;
					}
				}
			}
		}


		public void Dispose()
		{
			if (_mouseHook != null)
				_mouseHook.Dispose();

			_keyboardHook.Dispose();
		}

		private enum MouseMessages
		{
			WM_LBUTTONDOWN = 0x0201,
			WM_LBUTTONUP = 0x0202,
			WM_MOUSEMOVE = 0x0200,
			WM_MOUSEWHEEL = 0x020A,
			WM_RBUTTONDOWN = 0x0204,
			WM_RBUTTONUP = 0x0205
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct POINT
		{
			public int x;
			public int y;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct MSLLHOOKSTRUCT
		{
			public POINT pt;
			public uint mouseData;
			public uint flags;
			public uint time;
			public IntPtr dwExtraInfo;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct KeyboardHookStruct
		{
			public int VirtualKeyCode;
			public int ScanCode;
			public int Flags;
			public int Time;
			public int ExtraInfo;
		}
	}

	public class Hook : IDisposable
	{
		private IntPtr _hookID = IntPtr.Zero;
		private LowLevelProc _hookProc;

		public event HookFiredEventHandler HookFired;

		public Hook(int hook)
		{
			_hookProc = new LowLevelProc(HookCallback);
			_hookID = SetHook(_hookProc, hook);
		}

		private IntPtr SetHook(LowLevelProc proc, int hook)
		{
			using (Process curProcess = Process.GetCurrentProcess())
			using (ProcessModule curModule = curProcess.MainModule)
			{
				return SetWindowsHookEx(hook, proc,
					GetModuleHandle(curModule.ModuleName), 0);
			}
		}

		private delegate IntPtr LowLevelProc(
			int nCode, IntPtr wParam, IntPtr lParam);

		private IntPtr HookCallback(
			int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode >= 0)
			{
				var args = new HookFiredEventArgs() { NCode = nCode, LParam = lParam, WParam = wParam };
				if (HookFired != null)
					HookFired(this, args);
				if (args.Cancel) return new IntPtr(1);
			}
			return CallNextHookEx(_hookID, nCode, wParam, lParam);
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr SetWindowsHookEx(int idHook,
			LowLevelProc lpfn, IntPtr hMod, uint dwThreadId);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool UnhookWindowsHookEx(IntPtr hhk);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
			IntPtr wParam, IntPtr lParam);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string lpModuleName);

		public void Dispose()
		{
			UnhookWindowsHookEx(_hookID);
		}
	}

	public delegate void HookFiredEventHandler(object sender, HookFiredEventArgs e);
	public class HookFiredEventArgs : EventArgs
	{
		public int NCode { get; set; }
		public IntPtr WParam { get; set; }
		public IntPtr LParam { get; set; }
		public bool Cancel { get; set; }
	}

	public class CancelMouseEventArgs : MouseEventArgs
	{
		public bool Cancel { get; set; }
		public CancelMouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta) : base(button, clicks, x, y, delta) { }
	}

	public class CancelKeyEventArgs : KeyEventArgs
	{
		public bool Cancel { get; set; }
		public CancelKeyEventArgs(Keys keyData) : base(keyData) { }
	}

	public class CancelKeyPressEventArgs : KeyPressEventArgs
	{
		public bool Cancel { get; set; }
		public CancelKeyPressEventArgs(char keyChar) : base(keyChar) { }
	}
}
