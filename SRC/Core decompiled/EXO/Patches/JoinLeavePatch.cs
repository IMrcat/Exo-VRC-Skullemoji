using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using CoreRuntime.Manager;
using EXO.Core;
using EXO.LogTools;
using EXO.Wrappers;
using VRC;
using VRC.SDK3.Components;
using WCv2.Components.WorldPatches;

namespace EXO.Patches
{
	// Token: 0x0200003E RID: 62
	internal class JoinLeavePatch : PatchModule
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000B6AD File Offset: 0x000098AD
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000B6B4 File Offset: 0x000098B4
		internal static Action<Player> OnPlayerJoin { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000B6BC File Offset: 0x000098BC
		// (set) Token: 0x060002DA RID: 730 RVA: 0x0000B6C3 File Offset: 0x000098C3
		internal static Action<Player> OnPlayerLeave { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000B6CB File Offset: 0x000098CB
		// (set) Token: 0x060002DC RID: 732 RVA: 0x0000B6D2 File Offset: 0x000098D2
		internal static Action<Player> OnLocalPlayerJoin { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000B6DA File Offset: 0x000098DA
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0000B6E1 File Offset: 0x000098E1
		internal static Action<Player> OnLocalPlayerLeave { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000B6E9 File Offset: 0x000098E9
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x0000B6F0 File Offset: 0x000098F0
		internal static VRCPickup[] curItems { get; set; }

		// Token: 0x060002E1 RID: 737 RVA: 0x0000B6F8 File Offset: 0x000098F8
		public override void LoadPatch()
		{
			MethodInfo method = typeof(VRCPlayer).GetMethod("Awake");
			Action<VRCPlayer> action;
			if ((action = JoinLeavePatch.<>O.<0>__OnPlayerJoin_Internal) == null)
			{
				action = (JoinLeavePatch.<>O.<0>__OnPlayerJoin_Internal = new Action<VRCPlayer>(JoinLeavePatch.OnPlayerJoin_Internal));
			}
			PatchHandler.Detour(method, action);
			MethodInfo method2 = typeof(Player).GetMethod("OnDestroy");
			Action<Player> action2;
			if ((action2 = JoinLeavePatch.<>O.<1>__OnPlayerLeave_Internal) == null)
			{
				action2 = (JoinLeavePatch.<>O.<1>__OnPlayerLeave_Internal = new Action<Player>(JoinLeavePatch.OnPlayerLeave_Internal));
			}
			PatchHandler.Detour(method2, action2);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000B770 File Offset: 0x00009970
		private static void OnPlayerJoin_Internal(VRCPlayer __instance)
		{
			JoinLeavePatch.<>c__DisplayClass21_0 CS$<>8__locals1 = new JoinLeavePatch.<>c__DisplayClass21_0();
			CS$<>8__locals1.__instance = __instance;
			CoroutineManager.RunCoroutine(CS$<>8__locals1.<OnPlayerJoin_Internal>g__RunMe|0());
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000B798 File Offset: 0x00009998
		private static void OnPlayerLeave_Internal(Player __instance)
		{
			bool flag = __instance.DisplayName() == PlayerWrapper.LocalAPIUser.displayName;
			if (flag)
			{
				Action<Player> onLocalPlayerLeave = JoinLeavePatch.OnLocalPlayerLeave;
				if (onLocalPlayerLeave != null)
				{
					onLocalPlayerLeave.Invoke(__instance);
				}
			}
			else
			{
				Action<Player> onPlayerLeave = JoinLeavePatch.OnPlayerLeave;
				if (onPlayerLeave != null)
				{
					onPlayerLeave.Invoke(__instance);
				}
			}
			GUILog.DisplayOnScreen("<color=#ffffff>[</color><color=#ff0000>Left</color><color=#ffffff>]</color> " + __instance.DisplayName());
			JoinLeavePatch.LeaveLogs(__instance.DisplayName(), __instance.prop_APIUser_0.id);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000B814 File Offset: 0x00009A14
		internal static void JoinLogs(string user, string id)
		{
			CLog.Tag();
			"[".WriteToConsole(15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 85).WriteToConsole("Join", 10, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 86).WriteToConsole("] ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 87)
				.WriteToConsole(user + " ", 6, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 88)
				.WriteToConsole("| ", 10, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 89)
				.WriteLineToConsole(id + " ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 90);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000B8A8 File Offset: 0x00009AA8
		internal static void LeaveLogs(string user, string id)
		{
			CLog.Tag();
			"[".WriteToConsole(15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 95).WriteToConsole("Left", 12, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 96).WriteToConsole("] ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 97)
				.WriteToConsole(user + " ", 6, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 98)
				.WriteToConsole("| ", 12, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 99)
				.WriteLineToConsole(id + " ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\JoinLeavePatch.cs", 100);
		}

		// Token: 0x020000F9 RID: 249
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000390 RID: 912
			public static Action<VRCPlayer> <0>__OnPlayerJoin_Internal;

			// Token: 0x04000391 RID: 913
			public static Action<Player> <1>__OnPlayerLeave_Internal;
		}
	}
}
