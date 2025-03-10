using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using HexedTools.HookUtils;

namespace EXO_Engine
{
	// Token: 0x02000004 RID: 4
	[NullableContext(1)]
	[Nullable(0)]
	internal class CLog
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002418 File Offset: 0x00000618
		internal static void Tag()
		{
			Console.ResetColor();
			Logs.WriteOut("[", ConsoleColor.White);
			Console.ResetColor();
			Logs.WriteOut("EXO Engine", ConsoleColor.DarkRed);
			Console.ResetColor();
			Logs.WriteOut("] ", ConsoleColor.White);
			Console.ResetColor();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002464 File Offset: 0x00000664
		internal static void L(string MessageToLog, ConsoleColor TextColor = ConsoleColor.White, ConsoleColor MidColor = ConsoleColor.DarkRed)
		{
			CLog.Tag();
			Console.ResetColor();
			Logs.WriteOut("[", ConsoleColor.White);
			Console.ResetColor();
			Logs.WriteOut("~>", MidColor);
			Console.ResetColor();
			Logs.WriteOut("] ", ConsoleColor.White);
			Console.ResetColor();
			Logs.WriteOutLine(MessageToLog, TextColor);
			Console.ResetColor();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000024C4 File Offset: 0x000006C4
		internal static void R(string MessageToLog, ConsoleColor TextColor = ConsoleColor.DarkRed, ConsoleColor MidColor = ConsoleColor.DarkRed)
		{
			CLog.Tag();
			Console.ResetColor();
			Logs.WriteOut("[", ConsoleColor.White);
			Console.ResetColor();
			Logs.WriteOut("~>", MidColor);
			Console.ResetColor();
			Logs.WriteOut("] ", ConsoleColor.White);
			Console.ResetColor();
			Logs.WriteOutLine(MessageToLog, TextColor);
			Console.ResetColor();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002524 File Offset: 0x00000724
		internal static void E(string MessageToLog, ConsoleColor TextColor = ConsoleColor.Red, ConsoleColor MidColor = ConsoleColor.Blue)
		{
			CLog.Tag();
			Logs.WriteOut("[", ConsoleColor.White);
			Console.ResetColor();
			Logs.WriteOut("~>", MidColor);
			Console.ResetColor();
			Logs.WriteOut("] ", ConsoleColor.White);
			Console.ResetColor();
			Logs.WriteOutLine(MessageToLog, TextColor);
			Console.ResetColor();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002580 File Offset: 0x00000780
		public static void E(Exception ex)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(122, 4);
			defaultInterpolatedStringHandler.AppendLiteral("\n============ERROR============ \nTIME: ");
			defaultInterpolatedStringHandler.AppendFormatted(DateTime.Now.ToString("HH:mm.fff", CultureInfo.InvariantCulture));
			defaultInterpolatedStringHandler.AppendLiteral(" \nERROR MESSAGE: ");
			defaultInterpolatedStringHandler.AppendFormatted(ex.Message);
			defaultInterpolatedStringHandler.AppendLiteral(" \nLAST INSTRUCTIONS: ");
			defaultInterpolatedStringHandler.AppendFormatted(ex.StackTrace);
			defaultInterpolatedStringHandler.AppendLiteral(" \nFULL ERROR: ");
			defaultInterpolatedStringHandler.AppendFormatted<Exception>(ex);
			defaultInterpolatedStringHandler.AppendLiteral(" \n=============END=============\n");
			CLog.E(defaultInterpolatedStringHandler.ToStringAndClear(), ConsoleColor.Red, ConsoleColor.Blue);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002630 File Offset: 0x00000830
		public static void E(string msg, Exception ex)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(123, 5);
			defaultInterpolatedStringHandler.AppendLiteral("\n============ERROR============ \n");
			defaultInterpolatedStringHandler.AppendFormatted(msg);
			defaultInterpolatedStringHandler.AppendLiteral("\nTIME: ");
			defaultInterpolatedStringHandler.AppendFormatted(DateTime.Now.ToString("HH:mm.fff", CultureInfo.InvariantCulture));
			defaultInterpolatedStringHandler.AppendLiteral(" \nERROR MESSAGE: ");
			defaultInterpolatedStringHandler.AppendFormatted(ex.Message);
			defaultInterpolatedStringHandler.AppendLiteral(" \nLAST INSTRUCTIONS: ");
			defaultInterpolatedStringHandler.AppendFormatted(ex.StackTrace);
			defaultInterpolatedStringHandler.AppendLiteral(" \nFULL ERROR: ");
			defaultInterpolatedStringHandler.AppendFormatted<Exception>(ex);
			defaultInterpolatedStringHandler.AppendLiteral(" \n=============END=============\n");
			CLog.E(defaultInterpolatedStringHandler.ToStringAndClear(), ConsoleColor.Red, ConsoleColor.Blue);
		}
	}
}
