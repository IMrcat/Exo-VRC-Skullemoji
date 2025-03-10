using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using EXO.LogTools;

namespace EXO.Functions.External
{
	// Token: 0x020000AA RID: 170
	internal class ConsoleApp
	{
		// Token: 0x06000657 RID: 1623 RVA: 0x000241B8 File Offset: 0x000223B8
		internal static void Launch()
		{
			bool flag = Process.GetProcessesByName(ConsoleApp.processName).Length == 0;
			if (flag)
			{
				CLog.L(ConsoleApp.processName + " is not running. Attempting to start...", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\External\\ConsoleApp.cs", 24);
				try
				{
					string filePath = Path.Combine(AppStart.HexedDirectory.FullName, "EXO", ConsoleApp.fileName);
					bool flag2 = !File.Exists(filePath) || Config.cfg.ForceUpdate16;
					if (flag2)
					{
						CLog.L("[Dependencies] Downloading ConsoleLogs.exe...", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\External\\ConsoleApp.cs", 32);
						using (WebClient client = new WebClient())
						{
							client.DownloadFile("https://github.com/Cyconi/EXO-Resources/raw/main/ConsoleLogs.exe", filePath);
						}
						CLog.L("[Dependencies] ConsoleLogs.exe Downloaded!", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\External\\ConsoleApp.cs", 37);
					}
					Process.Start(filePath);
					CLog.L(ConsoleApp.processName + " started successfully.", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\External\\ConsoleApp.cs", 41);
					ConsoleApp.wasLaunched = true;
				}
				catch (Exception ex)
				{
					CLog.E("Failed to start " + ConsoleApp.processName, ex);
				}
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x000242DC File Offset: 0x000224DC
		internal static void OnClose()
		{
			bool flag = ConsoleApp.wasLaunched;
			if (flag)
			{
				Process[] processes = Process.GetProcessesByName(ConsoleApp.processName);
				bool flag2 = processes.Length != 0;
				if (flag2)
				{
					bool flag3 = !processes[0].CloseMainWindow();
					if (flag3)
					{
						processes[0].Kill();
					}
				}
			}
		}

		// Token: 0x040002FC RID: 764
		internal static bool wasLaunched = false;

		// Token: 0x040002FD RID: 765
		internal static string processName = "ConsoleLogs";

		// Token: 0x040002FE RID: 766
		internal static string fileName = "ConsoleLogs.exe";
	}
}
