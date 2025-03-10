using System;
using HexedTools.HookUtils;

namespace WorldAPI
{
	// Token: 0x02000008 RID: 8
	internal static class Logs
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021CB File Offset: 0x000003CB
		internal static void Log(string message, ConsoleColor color = 15)
		{
			Logs.Log(message, "", color, 5, "WorldAPI");
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021E0 File Offset: 0x000003E0
		internal static void Error(Exception e, string message)
		{
			Logs.Error(message, e);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021EA File Offset: 0x000003EA
		internal static void Error(string message, Exception e = null)
		{
			Logs.Error(message, e, "WorldAPI");
		}
	}
}
