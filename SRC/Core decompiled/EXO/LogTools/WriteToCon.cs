using System;
using System.Runtime.CompilerServices;
using HexedTools.HookUtils;

namespace EXO.LogTools
{
	// Token: 0x0200008A RID: 138
	internal static class WriteToCon
	{
		// Token: 0x0600057F RID: 1407 RVA: 0x0001C688 File Offset: 0x0001A888
		public static string WriteToConsole(this string text, ConsoleColor color, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
		{
			Logs.WriteOut(text, color);
			Console.ResetColor();
			return text;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001C6AC File Offset: 0x0001A8AC
		public static string WriteToConsole(this string text, string AddText, ConsoleColor color, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
		{
			Logs.WriteOut(AddText ?? "", color);
			Console.ResetColor();
			return text;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0001C6D8 File Offset: 0x0001A8D8
		public static string WriteLineToConsole(this string text, ConsoleColor color, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
		{
			Console.ResetColor();
			Logs.WriteOutLine(text, color);
			Console.ResetColor();
			Console.ResetColor();
			return text;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0001C708 File Offset: 0x0001A908
		public static string WriteLineToConsole(this string text, string AddText, ConsoleColor color, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
		{
			Console.ResetColor();
			Logs.WriteOutLine(AddText ?? "", color);
			Console.ResetColor();
			Console.ResetColor();
			return text;
		}
	}
}
