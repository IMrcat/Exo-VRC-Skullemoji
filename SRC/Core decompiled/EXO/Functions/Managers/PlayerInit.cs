using System;
using System.Collections;
using System.Diagnostics;
using CoreRuntime.Manager;
using EXO.Core;
using EXO.LogTools;
using EXO.Wrappers;
using UnityEngine;

namespace EXO.Functions.Managers
{
	// Token: 0x020000A2 RID: 162
	internal class PlayerInit : FunctionModule
	{
		// Token: 0x0600062C RID: 1580 RVA: 0x000228D0 File Offset: 0x00020AD0
		public override void OnUpdate()
		{
			bool isPlayerNotNull = PlayerWrapper.LocalVRCPlayer != null && PlayerWrapper.LocalVRCPlayer.gameObject != null;
			bool flag = PlayerInit.playerInit != isPlayerNotNull;
			if (flag)
			{
				PlayerInit.playerInit = isPlayerNotNull;
				CLog.D("Player Init " + PlayerInit.playerInit.ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\Managers\\PlayerInit.cs", 30);
				bool flag2 = PlayerInit.playerInit;
				if (flag2)
				{
					PlayerInit.playerTimer.Stop();
					CLog.L("Player created in " + PlayerInit.playerTimer.Elapsed.TotalSeconds.ToString("F5") + " seconds.", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\Managers\\PlayerInit.cs", 35);
					CoroutineManager.RunCoroutine(this.WaitSecond());
				}
				else
				{
					PlayerInit.playerTimer.Restart();
					AppStart.OnPlayerDestroy();
				}
			}
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000229AF File Offset: 0x00020BAF
		private IEnumerator WaitSecond()
		{
			while (!SceneLoadDetector.sceneChange)
			{
				yield return new WaitForFixedUpdate();
			}
			yield return new WaitForSeconds(1f);
			CLog.D("Running On Player Init", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\Managers\\PlayerInit.cs", 53);
			AppStart.OnPlayerInit();
			SceneLoadDetector.sceneChange = false;
			yield break;
		}

		// Token: 0x040002E3 RID: 739
		internal static bool playerInit = false;

		// Token: 0x040002E4 RID: 740
		internal static Stopwatch playerTimer = new Stopwatch();
	}
}
