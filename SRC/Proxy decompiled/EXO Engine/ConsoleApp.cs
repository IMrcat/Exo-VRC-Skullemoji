using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;

namespace EXO_Engine
{
	// Token: 0x02000005 RID: 5
	internal class ConsoleApp
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002700 File Offset: 0x00000900
		internal static void Launch()
		{
			string processName = "ConsoleLogs";
			string fileName = "ConsoleLogs.exe";
			bool flag = Process.GetProcessesByName(processName).Length == 0;
			if (flag)
			{
				CLog.L(processName + " is not running. Attempting to start...", ConsoleColor.White, ConsoleColor.DarkRed);
				try
				{
					string filePath = Path.Combine(Client.HexDir.FullName, "EXO", fileName);
					CLog.L("File path: " + filePath, ConsoleColor.White, ConsoleColor.DarkRed);
					bool flag2 = !File.Exists(filePath);
					if (flag2)
					{
						CLog.L("[Dependencies] Downloading ConsoleLogs.exe...", ConsoleColor.White, ConsoleColor.DarkRed);
						using (WebClient client = new WebClient())
						{
							client.DownloadFile("https://github.com/Cyconi/EXO-Resources/raw/main/ConsoleLogs.exe", filePath);
							CLog.L("Downloaded to: " + filePath, ConsoleColor.White, ConsoleColor.DarkRed);
						}
						CLog.L("[Dependencies] ConsoleLogs.exe Downloaded!", ConsoleColor.White, ConsoleColor.DarkRed);
					}
					ConsoleApp.consoleLogsProcess = Process.Start(filePath);
					CLog.L(processName + " started successfully.", ConsoleColor.White, ConsoleColor.DarkRed);
					ConsoleApp.wasLaunched = true;
				}
				catch (Exception ex)
				{
					CLog.L("Failed to start " + processName + ". Error: " + ex.Message, ConsoleColor.White, ConsoleColor.DarkRed);
					CLog.L("Stack Trace: " + ex.StackTrace, ConsoleColor.White, ConsoleColor.DarkRed);
				}
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002858 File Offset: 0x00000A58
		internal static void OnClose()
		{
			bool flag = ConsoleApp.wasLaunched && ConsoleApp.consoleLogsProcess != null;
			if (flag)
			{
				ConsoleApp.consoleLogsProcess.Close();
			}
		}

		// Token: 0x04000009 RID: 9
		internal static bool wasLaunched;

		// Token: 0x0400000A RID: 10
		[Nullable(1)]
		internal static Process consoleLogsProcess;
	}
}
