using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using CoreRuntime.Manager;
using EXO.Core;
using EXO.Functions.Managers;
using EXO.LogTools;
using EXO.Modules.Utilities;
using EXO.Wrappers;
using Il2CppSystem.Collections.Generic;
using UnityEngine;

namespace EXO.Modules
{
	// Token: 0x0200006F RID: 111
	internal class FriendLogger : ModsModule
	{
		// Token: 0x060003BE RID: 958 RVA: 0x000151D6 File Offset: 0x000133D6
		public override void OnInject()
		{
			CoroutineManager.RunCoroutine(FriendLogger.Wait());
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000151E3 File Offset: 0x000133E3
		public override void OnPlayerWasInit()
		{
			CoroutineManager.RunCoroutine(FriendLogger.LogFriends());
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000151F0 File Offset: 0x000133F0
		public override void OnPlayerWasDestroyed()
		{
			CoroutineManager.RunCoroutine(FriendLogger.LogFriends());
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000151FD File Offset: 0x000133FD
		internal static IEnumerator Wait()
		{
			while (!PlayerInit.playerInit || !PlayerWrapper.LocalVRCPlayer || !PlayerWrapper.LocalPlayer)
			{
				yield return new WaitForSeconds(1f);
			}
			float num = 1f;
			Action action;
			if ((action = FriendLogger.<>O.<0>__MakeFile) == null)
			{
				action = (FriendLogger.<>O.<0>__MakeFile = new Action(FriendLogger.MakeFile));
			}
			UtilFunc.Delay(num, action);
			yield break;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00015208 File Offset: 0x00013408
		internal static void MakeFile()
		{
			try
			{
				FriendLogger.path = Path.Combine(Directory.CreateDirectory(AppStart.HexedDirectory.FullName + "\\EXO\\Friends").FullName, PlayerWrapper.LocalUserID + ".json");
				bool flag = !File.Exists(FriendLogger.path);
				if (flag)
				{
					CLog.L("Friends File Doesn't Exist...", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\FriendLogger.cs", 42);
					File.Create(FriendLogger.path);
					CLog.L("Friends File Has Been Made", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\FriendLogger.cs", 44);
				}
			}
			catch (Exception e)
			{
				CLog.E(e);
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000152B4 File Offset: 0x000134B4
		internal static IEnumerator LogFriends()
		{
			CoroutineManager.RunCoroutine(FriendLogger.Wait());
			if (FriendLogger.path != null)
			{
				try
				{
					IEnumerable<string> fileData = File.ReadLines(FriendLogger.path);
					foreach (string text in PlayerWrapper.LocalAPIUser.friendIDs)
					{
						bool flag = !Enumerable.Contains<string>(fileData, text);
						if (flag)
						{
							File.AppendAllText(FriendLogger.path, text + "\n");
							CLog.L("Added " + text + " to list.", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\FriendLogger.cs", 65);
						}
						text = null;
					}
					List<string>.Enumerator enumerator = null;
					fileData = null;
					yield break;
				}
				catch (Exception ex)
				{
					Exception e = ex;
					CLog.E("Error Logging Friends to File", e);
					yield break;
				}
				yield break;
			}
			yield break;
		}

		// Token: 0x040001E9 RID: 489
		internal static string path;

		// Token: 0x0200014A RID: 330
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040005D7 RID: 1495
			public static Action <0>__MakeFile;
		}
	}
}
