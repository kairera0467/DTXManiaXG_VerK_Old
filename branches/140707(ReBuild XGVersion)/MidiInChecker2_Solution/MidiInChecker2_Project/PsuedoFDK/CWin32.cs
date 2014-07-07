﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace MidiInChecker2
{
	public class CWin32
	{
		#region [ Win32 定数 ]
		//-----------------
		public const int BROADCAST_QUERY_DENY = 0x424d5144;
		public const uint CALLBACK_FUNCTION = 0x30000;
		public const uint ES_CONTINUOUS = 0x80000000;
		public const uint ES_DISPLAY_REQUIRED = 2;
		public const uint ES_SYSTEM_REQUIRED = 1;
		public const uint ES_USER_PRESENT = 4;
		public const int GWL_EXSTYLE = -20;
		public const int GWL_HINSTANCE = -6;
		public const int GWL_HWNDPARENT = -8;
		public const int GWL_ID = -12;
		public const int GWL_STYLE = -16;
		public const int GWL_USERDATA = -21;
		public const int GWL_WNDPROC = -4;
		public static readonly IntPtr HWND_NOTOPMOST = new IntPtr( -2 );
		public static readonly IntPtr HWND_TOPMOST = new IntPtr( -1 );
		public const uint MAXPNAMELEN = 0x20;
		public const uint MIM_CLOSE = 0x3c2;
		public const uint MIM_DATA = 0x3c3;
		public const uint MIM_ERROR = 0x3c5;
		public const uint MIM_LONGDATA = 0x3c4;
		public const uint MIM_LONGERROR = 0x3c6;
		public const uint MIM_OPEN = 0x3c1;
		public const int MONITOR_DEFAULTTOPRIMARY = 1;
		public const int PBT_APMQUERYSTANDBY = 1;
		public const int PBT_APMQUERYSUSPEND = 0;
		public const int SC_MONITORPOWER = 0xf170;
		public const int SC_SCREENSAVE = 0xf140;
		public const int SIZE_MAXIMIZED = 2;
		public const int SIZE_MINIMIZED = 1;
		public const int SIZE_RESTORED = 0;
		public const uint SWP_FRAMECHANGED = 0x20;
		public const uint SWP_HIDEWINDOW = 0x80;
		public const uint SWP_NOACTIVATE = 0x10;
		public const uint SWP_NOCOPYBITS = 0x100;
		public const uint SWP_NOMOVE = 2;
		public const uint SWP_NOOWNERZORDER = 0x200;
		public const uint SWP_NOREDRAW = 8;
		public const uint SWP_NOSENDCHANGING = 0x400;
		public const uint SWP_NOSIZE = 1;
		public const uint SWP_NOZORDER = 4;
		public const uint SWP_SHOWWINDOW = 0x40;
		public const int WM_ACTIVATEAPP = 0x1c;
		public const int WM_POWERBROADCAST = 0x218;
		public const int WM_SIZE = 5;
		public const int WM_SYSCOMMAND = 0x112;
		public const int WM_SYSKEYDOWN = 260;
		public const int WPF_RESTORETOMAXIMIZED = 2;
		public const long WS_BORDER = 0x800000L;
		public const long WS_CAPTION = 0xc00000L;
		public const long WS_CHILD = 0x40000000L;
		public const long WS_CHILDWINDOW = 0x40000000L;
		public const long WS_CLIPCHILDREN = 0x2000000L;
		public const long WS_CLIPSIBLINGS = 0x4000000L;
		public const long WS_DISABLED = 0x8000000L;
		public const long WS_DLGFRAME = 0x400000L;
		public const long WS_EX_ACCEPTFILES = 0x10L;
		public const long WS_EX_APPWINDOW = 0x40000L;
		public const long WS_EX_CLIENTEDGE = 0x200L;
		public const long WS_EX_COMPOSITED = 0x2000000L;
		public const long WS_EX_CONTEXTHELP = 0x400L;
		public const long WS_EX_CONTROLPARENT = 0x10000L;
		public const long WS_EX_DLGMODALFRAME = 1L;
		public const long WS_EX_LAYERED = 0x80000L;
		public const long WS_EX_LAYOUTRTL = 0x400000L;
		public const long WS_EX_LEFT = 0L;
		public const long WS_EX_LEFTSCROLLBAR = 0x4000L;
		public const long WS_EX_LTRREADING = 0L;
		public const long WS_EX_MDICHILD = 0x40L;
		public const long WS_EX_NOACTIVATE = 0x8000000L;
		public const long WS_EX_NOINHERITLAYOUT = 0x100000L;
		public const long WS_EX_NOPARENTNOTIFY = 4L;
		public const long WS_EX_OVERLAPPEDWINDOW = 0x300L;
		public const long WS_EX_PALETTEWINDOW = 0x188L;
		public const long WS_EX_RIGHT = 0x1000L;
		public const long WS_EX_RIGHTSCROLLBAR = 0L;
		public const long WS_EX_RTLREADING = 0x2000L;
		public const long WS_EX_STATICEDGE = 0x20000L;
		public const long WS_EX_TOOLWINDOW = 0x80L;
		public const long WS_EX_TOPMOST = 8L;
		public const long WS_EX_TRANSPARENT = 0x20L;
		public const long WS_EX_WINDOWEDGE = 0x100L;
		public const long WS_GROUP = 0x20000L;
		public const long WS_HSCROLL = 0x100000L;
		public const long WS_ICONIC = 0x20000000L;
		public const long WS_MAXIMIZE = 0x1000000L;
		public const long WS_MAXIMIZEBOX = 0x10000L;
		public const long WS_MINIMIZE = 0x20000000L;
		public const long WS_MINIMIZEBOX = 0x20000L;
		public const long WS_OVERLAPPED = 0L;
		public const long WS_OVERLAPPEDWINDOW = 0xcf0000L;
		public const long WS_POPUP = 0x80000000L;
		public const long WS_POPUPWINDOW = 0x80880000L;
		public const long WS_SIZEBOX = 0x40000L;
		public const long WS_SYSMENU = 0x80000L;
		public const long WS_TABSTOP = 0x10000L;
		public const long WS_THICKFRAME = 0x40000L;
		public const long WS_TILED = 0L;
		public const long WS_TILEDWINDOW = 0xcf0000L;
		public const long WS_VISIBLE = 0x10000000L;
		public const long WS_VSCROLL = 0x200000L;

		public enum EShowWindow
		{
			ForceMinimize = 11,
			Hide = 0,
			Maximize = 3,
			Minimize = 6,
			Normal = 1,
			Restore = 9,
			Show = 5,
			ShowDefault = 10,
			ShowMaximized = 3,
			ShowMinimized = 2,
			ShowMinNoActive = 7,
			ShowNA = 8,
			ShowNoActivate = 4
		}
		public enum MMSYSERR
		{
			NOERROR,
			ERROR,
			BADDEVICEID,
			NOTENABLED,
			ALLOCATED,
			INVALHANDLE,
			NODRIVER,
			NOMEM,
			NOTSUPPORTED,
			BADERRNUM,
			INVALFLAG,
			INVALPARAM,
			HANDLEBUSY,
			INVALIDALIAS,
			BADDB,
			KEYNOTFOUND,
			READERROR,
			WRITEERROR,
			DELETEERROR,
			VALNOTFOUND,
			NODRIVERCB,
			MOREDATA
		}
		[FlagsAttribute]
		internal enum ExecutionState : uint
		{
			Null = 0,					// 関数が失敗した時の戻り値
			SystemRequired = 1,			// スタンバイを抑止
			DisplayRequired = 2,		// 画面OFFを抑止
			Continuous = 0x80000000,	// 効果を永続させる。ほかオプションと併用する。
		}
		//-----------------
		#endregion

		#region [ Win32 関数 ]
		//-----------------
		[return: MarshalAs( UnmanagedType.Bool )]
		[DllImport( "user32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		public static extern bool AdjustWindowRect( ref RECT lpRect, uint dwStyle, [MarshalAs( UnmanagedType.Bool )] bool bMenu );
		[return: MarshalAs( UnmanagedType.Bool )]
		[DllImport( "user32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		public static extern bool GetClientRect( IntPtr hWnd, out RECT lpRect );
		[DllImport( "user32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		public static extern uint GetWindowLong( IntPtr hWnd, int nIndex );
		[return: MarshalAs( UnmanagedType.Bool )]
		[DllImport( "user32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		public static extern bool GetWindowPlacement( IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl );
		[return: MarshalAs( UnmanagedType.Bool )]
		[DllImport( "user32.dll", CharSet = CharSet.Auto )]
		public static extern bool IsIconic( IntPtr hWnd );
		[return: MarshalAs( UnmanagedType.Bool )]
		[DllImport( "user32.dll", CharSet = CharSet.Auto )]
		public static extern bool IsWindowVisible( IntPtr hWnd );
		[return: MarshalAs( UnmanagedType.Bool )]
		[DllImport( "user32.dll", CharSet = CharSet.Auto )]
		public static extern bool IsZoomed( IntPtr hWnd );
		[DllImport( "winmm.dll" )]
		public static extern uint midiInClose( uint hMidiIn );
		[DllImport( "winmm.dll" )]
		public static extern uint midiInGetDevCaps( uint uDeviceID, ref MIDIINCAPS lpMidiInCaps, uint cbMidiInCaps );
		[DllImport( "winmm.dll" )]
		public static extern uint midiInGetID( uint hMidiIn, ref uint puDeviceID );
		[DllImport( "winmm.dll" )]
		public static extern uint midiInGetNumDevs();
		[DllImport( "winmm.dll" )]
		public static extern uint midiInOpen( ref uint phMidiIn, uint uDeviceID, MidiInProc dwCallback, int dwInstance, int fdwOpen );
		[DllImport( "winmm.dll" )]
		public static extern uint midiInReset( uint hMidiIn );
		[DllImport( "winmm.dll" )]
		public static extern uint midiInStart( uint hMidiIn );
		[DllImport( "winmm.dll" )]
		public static extern uint midiInStop( uint hMidiIn );
		[DllImport( "user32.dll", CharSet = CharSet.Auto )]
		public static extern IntPtr MonitorFromWindow( IntPtr hwnd, uint dwFlags );
		[return: MarshalAs( UnmanagedType.Bool )]
		[DllImport( "user32.dll", CharSet = CharSet.Auto )]
		public static extern bool PeekMessage( out WindowMessage message, IntPtr hwnd, uint messageFilterMin, uint messageFilterMax, uint flags );
		[DllImport( "kernel32.dll", CharSet = CharSet.Auto )]
		public static extern uint SetThreadExecutionState( uint esFlags );
		[DllImport( "user32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		public static extern uint SetWindowLong( IntPtr hWnd, int nIndex, uint dwNewLong );
		[return: MarshalAs( UnmanagedType.Bool )]
		[DllImport( "user32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		public static extern bool SetWindowPlacement( IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl );
		[return: MarshalAs( UnmanagedType.Bool )]
		[DllImport( "user32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		public static extern bool SetWindowPos( IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags );
		[return: MarshalAs( UnmanagedType.Bool )]
		[DllImport( "user32.dll", CharSet = CharSet.Auto )]
		public static extern bool ShowWindow( IntPtr hWnd, EShowWindow nCmdShow );
		[return: MarshalAs( UnmanagedType.Bool )]
		[DllImport( "user32.dll", CharSet = CharSet.Auto, SetLastError = true )]
		public static extern bool SystemParametersInfo( uint uiAction, uint uiParam, IntPtr pvParam, uint fWinIni );
		[DllImport( "kernel32.dll" )]
		public static extern void GetSystemInfo( ref SYSTEM_INFO ptmpsi );
		[DllImport( "kernel32.dll" )]
		internal static extern ExecutionState SetThreadExecutionState( ExecutionState esFlags );
		//-----------------
		#endregion

		#region [ Win32 構造体 ]
		//-----------------
		[StructLayout( LayoutKind.Sequential )]
		private struct FILTERKEYS
		{
			public int cbSize;
			public int dwFlags;
		}

		[StructLayout( LayoutKind.Sequential )]
		public struct MIDIINCAPS
		{
			public ushort wMid;
			public ushort wPid;
			public uint vDriverVersion;
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 0x20 )]
			public string szPname;
			public uint dwSupport;
		}

		[StructLayout( LayoutKind.Sequential )]
		public struct RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;
		}

		[StructLayout( LayoutKind.Sequential )]
		private struct STICKYKEYS
		{
			public int cbSize;
			public int dwFlags;
		}

		[StructLayout( LayoutKind.Sequential )]
		private struct TOGGLEKEYS
		{
			public int cbSize;
			public int dwFlags;
		}

		[StructLayout( LayoutKind.Sequential )]
		public struct WAVEFORMATEX
		{
			public ushort wFormatTag;
			public ushort nChannels;
			public uint nSamplesPerSec;
			public uint nAvgBytesPerSec;
			public ushort nBlockAlign;
			public ushort wBitsPerSample;
			public ushort cbSize;
		}

		[StructLayout( LayoutKind.Sequential )]
		public struct WindowMessage
		{
			public IntPtr hWnd;
			public uint msg;
			public IntPtr wParam;
			public IntPtr lParam;
			public uint time;
			public Point p;
		}

		[StructLayout( LayoutKind.Sequential )]
		public struct WINDOWPLACEMENT
		{
			public int length;
			public int flags;
			public CWin32.EShowWindow showCmd;
			public Point ptMinPosition;
			public Point ptMaxPosition;
			public CWin32.RECT rcNormalPosition;
			public static int Length
			{
				get
				{
					return Marshal.SizeOf( typeof( CWin32.WINDOWPLACEMENT ) );
				}
			}
		}
		[StructLayout( LayoutKind.Sequential )]
		public struct SYSTEM_INFO
		{
			public uint dwOemId;
			public uint dwPageSize;
			public uint lpMinimumApplicationAddress;
			public uint lpMaximumApplicationAddress;
			public uint dwActiveProcessorMask;
			public uint dwNumberOfProcessors;
			public uint dwProcessorType;
			public uint dwAllocationGranularity;
			public uint dwProcessorLevel;
			public uint dwProcessorRevision;
		}
		//-----------------
		#endregion


		// プロパティ

		public static bool bアプリがIdle状態である
		{
			get
			{
				WindowMessage message;
				return !PeekMessage( out message, IntPtr.Zero, 0, 0, 0 );
			}
		}


		// キーボードの特殊機能の制御

		public static class Cトグルキー機能
		{
			public static void t無効化する()
			{
				if ( ( stored.dwFlags & 1L ) == 0L )
				{
					CWin32.TOGGLEKEYS structure = new CWin32.TOGGLEKEYS();
					structure.dwFlags = stored.dwFlags;
					structure.cbSize = stored.cbSize;
					structure.dwFlags &= -5;
					structure.dwFlags &= -9;
					int cb = Marshal.SizeOf( structure );
					IntPtr ptr = Marshal.AllocCoTaskMem( cb );
					Marshal.StructureToPtr( structure, ptr, false );
					CWin32.SystemParametersInfo( 0x35, (uint) cb, ptr, 0 );
					Marshal.FreeCoTaskMem( ptr );
				}
			}
			public static void t復元する()
			{
				int cb = Marshal.SizeOf( stored );
				IntPtr ptr = Marshal.AllocCoTaskMem( cb );
				Marshal.StructureToPtr( stored, ptr, false );
				CWin32.SystemParametersInfo( 0x35, (uint) cb, ptr, 0 );
				Marshal.FreeCoTaskMem( ptr );
			}

			#region [ private ]
			//-----------------
			static Cトグルキー機能()
			{
				int cb = Marshal.SizeOf( stored );
				IntPtr ptr = Marshal.AllocCoTaskMem( cb );
				Marshal.StructureToPtr( stored, ptr, false );
				CWin32.SystemParametersInfo( 0x34, (uint) cb, ptr, 0 );
				stored = (CWin32.TOGGLEKEYS) Marshal.PtrToStructure( ptr, typeof( CWin32.TOGGLEKEYS ) );
				Marshal.FreeCoTaskMem( ptr );
			}

			private const uint SPI_GETTOGGLEKEYS = 0x34;
			private const uint SPI_SETTOGGLEKEYS = 0x35;
			private static CWin32.TOGGLEKEYS stored = new CWin32.TOGGLEKEYS();
			private const uint TKF_CONFIRMHOTKEY = 8;
			private const uint TKF_HOTKEYACTIVE = 4;
			private const uint TKF_TOGGLEKEYSON = 1;
			//-----------------
			#endregion
		}
		public static class Cフィルタキー機能
		{
			public static void t無効化する()
			{
				if ( ( stored.dwFlags & 1L ) == 0L )
				{
					CWin32.FILTERKEYS structure = new CWin32.FILTERKEYS();
					structure.dwFlags = stored.dwFlags;
					structure.cbSize = stored.cbSize;
					structure.dwFlags &= -5;
					structure.dwFlags &= -9;
					int cb = Marshal.SizeOf( structure );
					IntPtr ptr = Marshal.AllocCoTaskMem( cb );
					Marshal.StructureToPtr( structure, ptr, false );
					CWin32.SystemParametersInfo( 0x3b, (uint) cb, ptr, 0 );
					Marshal.FreeCoTaskMem( ptr );
				}
			}
			public static void t復元する()
			{
				int cb = Marshal.SizeOf( stored );
				IntPtr ptr = Marshal.AllocCoTaskMem( cb );
				Marshal.StructureToPtr( stored, ptr, false );
				CWin32.SystemParametersInfo( 0x3b, (uint) cb, ptr, 0 );
				Marshal.FreeCoTaskMem( ptr );
			}

			#region [ private ]
			//-----------------
			static Cフィルタキー機能()
			{
				stored.cbSize = 0;
				stored.dwFlags = 0;
				int cb = Marshal.SizeOf( stored );
				IntPtr ptr = Marshal.AllocCoTaskMem( cb );
				Marshal.StructureToPtr( stored, ptr, false );
				CWin32.SystemParametersInfo( 50, (uint) cb, ptr, 0 );
				stored = (CWin32.FILTERKEYS) Marshal.PtrToStructure( ptr, typeof( CWin32.FILTERKEYS ) );
				Marshal.FreeCoTaskMem( ptr );
			}

			private const uint FKF_CONFIRMHOTKEY = 8;
			private const uint FKF_FILTERKEYSON = 1;
			private const uint FKF_HOTKEYACTIVE = 4;
			private const uint SPI_GETFILTERKEYS = 50;
			private const uint SPI_SETFILTERKEYS = 0x3b;
			private static CWin32.FILTERKEYS stored = new CWin32.FILTERKEYS();
			//-----------------
			#endregion
		}
		public static class C固定キー機能
		{
			public static void t無効化する()
			{
				if ( ( stored.dwFlags & 1L ) == 0L )
				{
					CWin32.STICKYKEYS structure = new CWin32.STICKYKEYS();
					structure.dwFlags = stored.dwFlags;
					structure.cbSize = stored.cbSize;
					structure.dwFlags &= -5;
					structure.dwFlags &= -9;
					int cb = Marshal.SizeOf( structure );
					IntPtr ptr = Marshal.AllocCoTaskMem( cb );
					Marshal.StructureToPtr( structure, ptr, false );
					CWin32.SystemParametersInfo( 0x3b, (uint) cb, ptr, 0 );
					Marshal.FreeCoTaskMem( ptr );
				}
			}
			public static void t復元する()
			{
				int cb = Marshal.SizeOf( stored );
				IntPtr ptr = Marshal.AllocCoTaskMem( cb );
				Marshal.StructureToPtr( stored, ptr, false );
				CWin32.SystemParametersInfo( 0x3b, (uint) cb, ptr, 0 );
				Marshal.FreeCoTaskMem( ptr );
			}

			#region [ private ]
			//-----------------
			static C固定キー機能()
			{
				stored.cbSize = 0;
				stored.dwFlags = 0;
				int cb = Marshal.SizeOf( stored );
				IntPtr ptr = Marshal.AllocCoTaskMem( cb );
				Marshal.StructureToPtr( stored, ptr, false );
				CWin32.SystemParametersInfo( 0x3a, (uint) cb, ptr, 0 );
				stored = (CWin32.STICKYKEYS) Marshal.PtrToStructure( ptr, typeof( CWin32.STICKYKEYS ) );
				Marshal.FreeCoTaskMem( ptr );
			}

			private const uint SKF_CONFIRMHOTKEY = 8;
			private const uint SKF_HOTKEYACTIVE = 4;
			private const uint SKF_STICKYKEYSON = 1;
			private const uint SPI_GETSTICKYKEYS = 0x3a;
			private const uint SPI_SETSTICKYKEYS = 0x3b;
			private static CWin32.STICKYKEYS stored = new CWin32.STICKYKEYS();
			//-----------------
			#endregion
		}


		// Win32 メッセージ処理デリゲート

		public delegate void MidiInProc( uint hMidiIn, uint wMsg, int dwInstance, int dwParam1, int dwParam2 );
	}
}
