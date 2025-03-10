using System;
using System.Runtime.CompilerServices;

namespace EXO_Engine
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	internal class Msg
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002C64 File Offset: 0x00000E64
		internal static void Authorized(string username)
		{
			Console.WriteLine();
			CLog.R("[====== EXO Engine ======]", ConsoleColor.DarkRed, ConsoleColor.DarkRed);
			CLog.L("                          ", ConsoleColor.White, ConsoleColor.DarkRed);
			CLog.L("        Authorized        ", ConsoleColor.White, ConsoleColor.DarkRed);
			CLog.L("      Welcome " + username + "  ", ConsoleColor.White, ConsoleColor.DarkRed);
			CLog.L("                          ", ConsoleColor.White, ConsoleColor.DarkRed);
			CLog.R("[====== EXO Engine ======]", ConsoleColor.DarkRed, ConsoleColor.DarkRed);
			Console.WriteLine();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002CDC File Offset: 0x00000EDC
		internal static void NotAuthorized(string message)
		{
			Console.WriteLine();
			CLog.R("[====== EXO Engine ======]", ConsoleColor.DarkRed, ConsoleColor.DarkRed);
			CLog.L("                         ", ConsoleColor.White, ConsoleColor.DarkRed);
			CLog.L("      " + message + "          ", ConsoleColor.White, ConsoleColor.DarkRed);
			CLog.L("                         ", ConsoleColor.White, ConsoleColor.DarkRed);
			CLog.R("[====== EXO Engine ======]", ConsoleColor.DarkRed, ConsoleColor.DarkRed);
			Console.WriteLine();
		}
	}
}
