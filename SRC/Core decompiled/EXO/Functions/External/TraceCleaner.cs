using System;
using System.IO;
using EXO.Core;
using EXO.LogTools;

namespace EXO.Functions.External
{
	// Token: 0x020000AC RID: 172
	internal class TraceCleaner : FunctionModule
	{
		// Token: 0x0600065E RID: 1630 RVA: 0x00024587 File Offset: 0x00022787
		public override void OnInject()
		{
			TraceCleaner.RemoveSteamAPI();
			TraceCleaner.EACacheClean();
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00024598 File Offset: 0x00022798
		internal static void RemoveSteamAPI()
		{
			string filePath = Environment.CurrentDirectory + "\\VRChat_Data\\Plugins\\x86_64\\steam_api64.dll";
			bool flag = File.Exists(filePath);
			if (flag)
			{
				CLog.L("Deleting steam_api64.dll...", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\External\\TraceCleaner.cs", 25);
				try
				{
					File.Delete(filePath);
					CLog.L("steam_api64.dll deleted successfully.", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\External\\TraceCleaner.cs", 29);
				}
				catch (IOException ioExp)
				{
					CLog.E("An error occurred: ", ioExp);
				}
			}
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00024614 File Offset: 0x00022814
		internal static void EACacheClean()
		{
			string folderPath = Path.Combine(Environment.GetFolderPath(40), "AppData\\Roaming\\EasyAntiCheat");
			bool flag = Directory.Exists(folderPath);
			if (flag)
			{
				CLog.L("Deleting EAC cache...", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\External\\TraceCleaner.cs", 40);
				try
				{
					Directory.Delete(folderPath, true);
					CLog.L("EAC cache deleted successfully.", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\External\\TraceCleaner.cs", 44);
				}
				catch (IOException ioExp)
				{
					CLog.E("An error occurred: ", ioExp);
				}
			}
		}
	}
}
