using System;
using System.IO;

namespace EXO_Engine
{
	// Token: 0x02000002 RID: 2
	internal class Cleaner
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal static void RemoveSteamAPI()
		{
			string filePath = Environment.CurrentDirectory + "\\VRChat_Data\\Plugins\\x86_64\\steam_api64.dll";
			bool flag = File.Exists(filePath);
			if (flag)
			{
				CLog.L("Deleting steam_api64.dll...", ConsoleColor.White, ConsoleColor.DarkRed);
				try
				{
					File.Delete(filePath);
					CLog.L("steam_api64.dll deleted successfully.", ConsoleColor.White, ConsoleColor.DarkRed);
				}
				catch (IOException ioExp)
				{
					CLog.E("An error occurred: ", ioExp);
				}
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020C4 File Offset: 0x000002C4
		internal static void EACacheClean()
		{
			string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData\\Roaming\\EasyAntiCheat");
			bool flag = Directory.Exists(folderPath);
			if (flag)
			{
				CLog.L("Deleting EAC cache...", ConsoleColor.White, ConsoleColor.DarkRed);
				try
				{
					Directory.Delete(folderPath, true);
					CLog.L("EAC cache deleted successfully.", ConsoleColor.White, ConsoleColor.DarkRed);
				}
				catch (IOException ioExp)
				{
					CLog.E("An error occurred: ", ioExp);
				}
			}
		}
	}
}
