using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using CoreRuntime.Interfaces;
using CoreRuntime.Manager;
using EXO.Core;
using EXO.Functions.External;
using EXO.Functions.Managers;
using EXO.LogTools;
using EXO.Modules;
using EXO.Modules.Utilities;
using EXO.Wrappers;
using UnityEngine;

namespace EXO
{
	// Token: 0x0200002D RID: 45
	public class AppStart : HexedCheat
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00008A4A File Offset: 0x00006C4A
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00008A51 File Offset: 0x00006C51
		internal static DirectoryInfo HexedDirectory { get; private set; }

		// Token: 0x06000194 RID: 404 RVA: 0x00008A59 File Offset: 0x00006C59
		public override void OnLoad(string[] args)
		{
			AppStart.CallOnLoad(this, false);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00008A64 File Offset: 0x00006C64
		public static void CallOnLoad(HexedCheat instance, bool consoleLaunched = false)
		{
			AppStart.HexedDirectory = new FileInfo(instance.Path).Directory.Parent;
			ConfigManager.LoadConfig();
			ConsoleApp.wasLaunched = consoleLaunched;
			ConsoleApp.Launch();
			CLog.R("[====== EXO Loaded ======]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\AppStart.cs", 45);
			PlayerInit.playerTimer.Start();
			DiscordClient.Init();
			GetUser.Init();
			MonoManager.PatchUpdate(typeof(VRCApplication).GetMethod("Update"));
			MonoManager.PatchOnApplicationQuit(typeof(VRCApplicationSetup).GetMethod("OnApplicationQuit"));
			UpdateManager.StartFixedUpdate();
			StartMsgs.Watermark();
			CoroutineManager.RunCoroutine(AppStart.<CallOnLoad>g__WaitForId|10_0());
			PatchCore.LoadPatches();
			FunctionCore.OnLoad();
			ModuleCore.OnLoad();
			WorldWrapper.AddAction();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00008B2D File Offset: 0x00006D2D
		public override void OnApplicationQuit()
		{
			CLog.L("Game Closing", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\AppStart.cs", 91);
			Config.cfg.ForceUpdate16 = false;
			ConsoleApp.OnClose();
			UpdateManager.StopFixedUpdate();
			ConfigManager.Save();
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00008B61 File Offset: 0x00006D61
		public override void OnUpdate()
		{
			FunctionCore.OnUpdate();
			ModuleCore.OnUpdate();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00008B70 File Offset: 0x00006D70
		public static void FixedUpdate()
		{
			FunctionCore.OnFixedUpdate();
			ModuleCore.OnFixedUpdate();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00008B7F File Offset: 0x00006D7F
		public static void OnPlayerInit()
		{
			FunctionCore.OnPlayerWasInit();
			ModuleCore.OnPlayerWasInit();
			WorldWrapper.Init();
			WorldWrapper.FindItems();
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00008B9A File Offset: 0x00006D9A
		public static void OnPlayerDestroy()
		{
			FunctionCore.OnPlayerWasDestroyed();
			ModuleCore.OnPlayerWasDestroyed();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00008BCC File Offset: 0x00006DCC
		[CompilerGenerated]
		internal static IEnumerator <CallOnLoad>g__WaitForId|10_0()
		{
			while (DiscordClient.discordAccount == null || !GetUser.isReady)
			{
				yield return new WaitForSeconds(1f);
			}
			bool flag = !GetUser.Auth(DiscordClient.discordAccount.ID.ToString());
			if (flag)
			{
				StartMsgs.UnAuthorized();
				Environment.Exit(0);
				yield break;
			}
			AppStart.username = DiscordClient.discordAccount.DisplayName;
			AppStart.license = GetUser.GetDetails(DiscordClient.discordAccount.ID.ToString());
			StartMsgs.Authorized(DiscordClient.discordAccount.Username, AppStart.license);
			AppStart.devMode = DiscordClient.discordAccount.ID.ToString() == "964947247286616074";
			MenuCore.LoadMenus();
			yield break;
		}

		// Token: 0x040000B3 RID: 179
		internal static string release = "1.6";

		// Token: 0x040000B4 RID: 180
		internal static bool debugMode = false;

		// Token: 0x040000B5 RID: 181
		internal static bool devMode = false;

		// Token: 0x040000B6 RID: 182
		internal static string username;

		// Token: 0x040000B7 RID: 183
		internal static string license;
	}
}
