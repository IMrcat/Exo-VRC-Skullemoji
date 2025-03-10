using System;
using System.IO;
using System.Net;
using EXO.Core;
using EXO.LogTools;

namespace EXO.Functions.External
{
	// Token: 0x020000AB RID: 171
	internal class GetDependencies : FunctionModule
	{
		// Token: 0x0600065B RID: 1627 RVA: 0x00024348 File Offset: 0x00022548
		public override void OnInject()
		{
			string filePath = Path.Combine(AppStart.HexedDirectory.FullName, "Runtime", "EXO Base.dll");
			CLog.L("[Dependencies] Downloading EXO Base Lib...", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\External\\GetDependencies.cs", 20);
			File.WriteAllBytes(filePath, GetDependencies.web.DownloadData("https://github.com/Cyconi/EXO-Resources/raw/main/EXO%20Base.dll"));
			CLog.L("[Dependencies] EXO Base Lib Downloaded!", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\External\\GetDependencies.cs", 22);
			bool flag = !File.Exists(filePath = Path.Combine(AppStart.HexedDirectory.FullName, "Runtime", "ConsoleTool.dll")) || Config.cfg.ForceUpdate16;
			if (flag)
			{
				CLog.L("[Dependencies] Downloading DiscordRPC Lib...", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\External\\GetDependencies.cs", 26);
				File.WriteAllBytes(filePath, GetDependencies.web.DownloadData("https://github.com/Cyconi/EXO-Resources/raw/main/ConsoleTool.dll"));
				CLog.L("[Dependencies] DiscordRPC Lib Downloaded!", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\External\\GetDependencies.cs", 28);
			}
			bool flag2 = !File.Exists(filePath = Path.Combine(AppStart.HexedDirectory.FullName, "Runtime", "DiscordRPC.dll")) || Config.cfg.ForceUpdate16;
			if (flag2)
			{
				CLog.L("[Dependencies] Downloading DiscordRPC Lib...", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\External\\GetDependencies.cs", 32);
				File.WriteAllBytes(filePath, GetDependencies.web.DownloadData("https://github.com/Cyconi/EXO-Resources/raw/main/DiscordRPC.dll"));
				CLog.L("[Dependencies] DiscordRPC Lib Downloaded!", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\External\\GetDependencies.cs", 34);
			}
			bool flag3 = !File.Exists(filePath = Path.Combine(AppStart.HexedDirectory.FullName, "Runtime", "Newtonsoft.Json.dll")) || Config.cfg.ForceUpdate16;
			if (flag3)
			{
				CLog.L("[Dependencies] Downloading Newtonsoft.Json Lib...", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\External\\GetDependencies.cs", 38);
				File.WriteAllBytes(filePath, GetDependencies.web.DownloadData("https://github.com/Cyconi/EXO-Resources/raw/main/Newtonsoft.Json.dll"));
				CLog.L("[Dependencies] Newtonsoft.Json Lib Downloaded!", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\External\\GetDependencies.cs", 40);
			}
			bool flag4 = !File.Exists(filePath = Path.Combine(AppStart.HexedDirectory.FullName, "Runtime", "NotHarmony.dll")) || Config.cfg.ForceUpdate16;
			if (flag4)
			{
				CLog.L("[Dependencies] Downloading NotHarmony Lib...", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\External\\GetDependencies.cs", 44);
				File.WriteAllBytes(filePath, GetDependencies.web.DownloadData("https://github.com/Cyconi/EXO-Resources/raw/main/NotHarmony.dll"));
				CLog.L("[Dependencies] NotHarmony Lib Downloaded!", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\External\\GetDependencies.cs", 46);
			}
		}

		// Token: 0x040002FF RID: 767
		private static WebClient web = new WebClient();
	}
}
