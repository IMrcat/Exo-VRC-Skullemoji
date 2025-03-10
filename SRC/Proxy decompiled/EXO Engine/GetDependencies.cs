using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;

namespace EXO_Engine
{
	// Token: 0x02000007 RID: 7
	internal class GetDependencies
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002928 File Offset: 0x00000B28
		internal static void OnLoad()
		{
			string filePath = Path.Combine(Client.HexDir.FullName, "Runtime", "ConsoleTool.dll");
			bool flag = !File.Exists(filePath);
			if (flag)
			{
				File.WriteAllBytes(filePath, GetDependencies.web.DownloadData("https://github.com/Cyconi/EXO-Resources/raw/main/ConsoleTool.dll"));
			}
			filePath = Path.Combine(Client.HexDir.FullName, "Runtime", "EXO Base.dll");
			CLog.L("[Dependencies] Downloading EXO Base...", ConsoleColor.White, ConsoleColor.DarkRed);
			File.WriteAllBytes(filePath, GetDependencies.web.DownloadData("https://github.com/Cyconi/EXO-Resources/raw/main/EXO%20Base.dll"));
			CLog.L("[Dependencies] EXO Base Downloaded!", ConsoleColor.White, ConsoleColor.DarkRed);
			bool flag2 = !File.Exists(filePath = Path.Combine(Client.HexDir.FullName, "Runtime", "DiscordRPC.dll"));
			if (flag2)
			{
				CLog.L("[Dependencies] Downloading DiscordRPC...", ConsoleColor.White, ConsoleColor.DarkRed);
				File.WriteAllBytes(filePath, GetDependencies.web.DownloadData("https://github.com/Cyconi/EXO-Resources/raw/main/DiscordRPC.dll"));
				CLog.L("[Dependencies] DiscordRPC Downloaded!", ConsoleColor.White, ConsoleColor.DarkRed);
			}
			bool flag3 = !File.Exists(filePath = Path.Combine(Client.HexDir.FullName, "Runtime", "Newtonsoft.Json.dll"));
			if (flag3)
			{
				CLog.L("[Dependencies] Downloading Newtonsoft.Json...", ConsoleColor.White, ConsoleColor.DarkRed);
				File.WriteAllBytes(filePath, GetDependencies.web.DownloadData("https://github.com/Cyconi/EXO-Resources/raw/main/Newtonsoft.Json.dll"));
				CLog.L("[Dependencies] Newtonsoft.Json Downloaded!", ConsoleColor.White, ConsoleColor.DarkRed);
			}
			bool flag4 = !File.Exists(filePath = Path.Combine(Client.HexDir.FullName, "Runtime", "NotHarmony.dll"));
			if (flag4)
			{
				CLog.L("[Dependencies] Downloading NotHarmony...", ConsoleColor.White, ConsoleColor.DarkRed);
				File.WriteAllBytes(filePath, GetDependencies.web.DownloadData("https://github.com/Cyconi/EXO-Resources/raw/main/NotHarmony.dll"));
				CLog.L("[Dependencies] NotHarmony Downloaded!", ConsoleColor.White, ConsoleColor.DarkRed);
			}
		}

		// Token: 0x0400000D RID: 13
		[Nullable(1)]
		private static WebClient web = new WebClient();
	}
}
