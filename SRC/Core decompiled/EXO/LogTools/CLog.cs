using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using EXO.Core;
using HexedTools.HookUtils;
using Il2CppSystem.IO;

namespace EXO.LogTools
{
	// Token: 0x02000086 RID: 134
	internal class CLog
	{
		// Token: 0x0600056B RID: 1387 RVA: 0x0001BA0C File Offset: 0x00019C0C
		internal static void Tag()
		{
			Console.ResetColor();
			Logs.WriteOut("[", 15);
			Console.ResetColor();
			Logs.WriteOut("EXO", 4);
			Console.ResetColor();
			Logs.WriteOut("] ", 15);
			Console.ResetColor();
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001BA58 File Offset: 0x00019C58
		internal static void L(string MessageToLog, bool logToGUI = false, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
		{
			string fileName = Path.GetFileName(filePath);
			CLog.Tag();
			bool debugMode = AppStart.debugMode;
			if (debugMode)
			{
				Logs.WriteOut("[", 15);
				Console.ResetColor();
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(1, 2);
				defaultInterpolatedStringHandler.AppendFormatted(fileName);
				defaultInterpolatedStringHandler.AppendLiteral(":");
				defaultInterpolatedStringHandler.AppendFormatted<int>(lineNumber);
				Logs.WriteOut(defaultInterpolatedStringHandler.ToStringAndClear(), 14);
				Console.ResetColor();
				Logs.WriteOut("] ", 15);
			}
			Console.ResetColor();
			Logs.WriteOut("[", 15);
			Console.ResetColor();
			Logs.WriteOut("~>", 4);
			Console.ResetColor();
			Logs.WriteOut("] ", 15);
			Console.ResetColor();
			Logs.WriteOutLine(MessageToLog, 15);
			Console.ResetColor();
			bool flag = logToGUI && MenuCore.quickMenu != null;
			if (flag)
			{
				GUILog.DisplayOnScreen(MessageToLog);
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001BB44 File Offset: 0x00019D44
		internal static void D(string MessageToLog, bool logToGUI = false, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
		{
			bool flag = !AppStart.debugMode;
			if (!flag)
			{
				string fileName = Path.GetFileName(filePath);
				CLog.Tag();
				Logs.WriteOut("[", 15);
				Console.ResetColor();
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(1, 2);
				defaultInterpolatedStringHandler.AppendFormatted(fileName);
				defaultInterpolatedStringHandler.AppendLiteral(":");
				defaultInterpolatedStringHandler.AppendFormatted<int>(lineNumber);
				Logs.WriteOut(defaultInterpolatedStringHandler.ToStringAndClear(), 14);
				Console.ResetColor();
				Logs.WriteOut("] ", 15);
				Console.ResetColor();
				Logs.WriteOut("[", 15);
				Console.ResetColor();
				Logs.WriteOut("~>", 4);
				Console.ResetColor();
				Logs.WriteOut("] ", 15);
				Console.ResetColor();
				Logs.WriteOutLine(MessageToLog, 13);
				Console.ResetColor();
				if (logToGUI)
				{
					GUILog.DisplayOnScreen(MessageToLog);
				}
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001BC28 File Offset: 0x00019E28
		internal static void R(string MessageToLog, bool logToGUI = false, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
		{
			string fileName = Path.GetFileName(filePath);
			CLog.Tag();
			bool debugMode = AppStart.debugMode;
			if (debugMode)
			{
				Logs.WriteOut("[", 15);
				Console.ResetColor();
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(1, 2);
				defaultInterpolatedStringHandler.AppendFormatted(fileName);
				defaultInterpolatedStringHandler.AppendLiteral(":");
				defaultInterpolatedStringHandler.AppendFormatted<int>(lineNumber);
				Logs.WriteOut(defaultInterpolatedStringHandler.ToStringAndClear(), 14);
				Console.ResetColor();
				Logs.WriteOut("] ", 15);
			}
			Console.ResetColor();
			Logs.WriteOut("[", 15);
			Console.ResetColor();
			Logs.WriteOut("~>", 4);
			Console.ResetColor();
			Logs.WriteOut("] ", 15);
			Console.ResetColor();
			Logs.WriteOutLine(MessageToLog, 4);
			Console.ResetColor();
			if (logToGUI)
			{
				GUILog.DisplayOnScreen(MessageToLog);
			}
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001BD04 File Offset: 0x00019F04
		internal static void E(string MessageToLog, bool logToGUI = false, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
		{
			string fileName = Path.GetFileName(filePath);
			CLog.Tag();
			bool debugMode = AppStart.debugMode;
			if (debugMode)
			{
				Logs.WriteOut("[", 15);
				Console.ResetColor();
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(1, 2);
				defaultInterpolatedStringHandler.AppendFormatted(fileName);
				defaultInterpolatedStringHandler.AppendLiteral(":");
				defaultInterpolatedStringHandler.AppendFormatted<int>(lineNumber);
				Logs.WriteOut(defaultInterpolatedStringHandler.ToStringAndClear(), 14);
				Console.ResetColor();
				Logs.WriteOut("] ", 15);
			}
			Logs.WriteOut("[", 15);
			Console.ResetColor();
			Logs.WriteOut("~>", 4);
			Console.ResetColor();
			Logs.WriteOut("] ", 15);
			Console.ResetColor();
			Logs.WriteOutLine(MessageToLog, 12);
			Console.ResetColor();
			if (logToGUI)
			{
				GUILog.DisplayOnScreen("[ERROR] " + MessageToLog);
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0001BDE4 File Offset: 0x00019FE4
		public static void E(Exception ex)
		{
			string stack = ex.StackTrace;
			string message = ex.Message;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(122, 4);
			defaultInterpolatedStringHandler.AppendLiteral("\n============ERROR============ \nTIME: ");
			defaultInterpolatedStringHandler.AppendFormatted(DateTime.Now.ToString("HH:mm.fff", CultureInfo.InvariantCulture));
			defaultInterpolatedStringHandler.AppendLiteral(" \nERROR MESSAGE: ");
			defaultInterpolatedStringHandler.AppendFormatted(message);
			defaultInterpolatedStringHandler.AppendLiteral(" \nLAST INSTRUCTIONS: ");
			defaultInterpolatedStringHandler.AppendFormatted(stack);
			defaultInterpolatedStringHandler.AppendLiteral(" \nFULL ERROR: ");
			defaultInterpolatedStringHandler.AppendFormatted<Exception>(ex);
			defaultInterpolatedStringHandler.AppendLiteral(" \n=============END=============\n");
			CLog.E(defaultInterpolatedStringHandler.ToStringAndClear(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\CLog.cs", 133);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0001BEA0 File Offset: 0x0001A0A0
		public static void E(string msg, Exception ex)
		{
			string stack = ex.StackTrace;
			string message = ex.Message;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(123, 5);
			defaultInterpolatedStringHandler.AppendLiteral("\n============ERROR============ \n");
			defaultInterpolatedStringHandler.AppendFormatted(msg);
			defaultInterpolatedStringHandler.AppendLiteral("\nTIME: ");
			defaultInterpolatedStringHandler.AppendFormatted(DateTime.Now.ToString("HH:mm.fff", CultureInfo.InvariantCulture));
			defaultInterpolatedStringHandler.AppendLiteral(" \nERROR MESSAGE: ");
			defaultInterpolatedStringHandler.AppendFormatted(message);
			defaultInterpolatedStringHandler.AppendLiteral(" \nLAST INSTRUCTIONS: ");
			defaultInterpolatedStringHandler.AppendFormatted(stack);
			defaultInterpolatedStringHandler.AppendLiteral(" \nFULL ERROR: ");
			defaultInterpolatedStringHandler.AppendFormatted<Exception>(ex);
			defaultInterpolatedStringHandler.AppendLiteral(" \n=============END=============\n");
			CLog.E(defaultInterpolatedStringHandler.ToStringAndClear(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\LogTools\\CLog.cs", 140);
		}
	}
}
