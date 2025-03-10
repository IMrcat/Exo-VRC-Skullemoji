using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using EXO.Core;
using EXO.Functions.MenuOverrides;
using VRC.UI.Elements;
using WCv2.Components.WorldPatches;

namespace EXO.Patches
{
	// Token: 0x0200003F RID: 63
	internal class MainMenuPatch : PatchModule
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000B943 File Offset: 0x00009B43
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x0000B94A File Offset: 0x00009B4A
		internal static Action OnMainMenuOpen { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000B952 File Offset: 0x00009B52
		// (set) Token: 0x060002EA RID: 746 RVA: 0x0000B959 File Offset: 0x00009B59
		internal static Action OnMainMenuClose { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000B961 File Offset: 0x00009B61
		// (set) Token: 0x060002EC RID: 748 RVA: 0x0000B968 File Offset: 0x00009B68
		internal static bool IsOpen { get; private set; }

		// Token: 0x060002ED RID: 749 RVA: 0x0000B970 File Offset: 0x00009B70
		public override void LoadPatch()
		{
			MethodInfo method = typeof(MainMenu).GetMethod("OnEnable");
			Action action;
			if ((action = MainMenuPatch.<>O.<0>__OnMMOpen) == null)
			{
				action = (MainMenuPatch.<>O.<0>__OnMMOpen = new Action(MainMenuPatch.OnMMOpen));
			}
			PatchHandler.Detour(method, action);
			MethodInfo method2 = typeof(MainMenu).GetMethod("OnDisable");
			Action action2;
			if ((action2 = MainMenuPatch.<>O.<1>__OnMMClosed) == null)
			{
				action2 = (MainMenuPatch.<>O.<1>__OnMMClosed = new Action(MainMenuPatch.OnMMClosed));
			}
			PatchHandler.Detour(method2, action2);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000B9E8 File Offset: 0x00009BE8
		private static void OnMMOpen()
		{
			ColorPaletteOverride.EnableVRCPlus();
			bool flag = !MainMenuPatch.openFix;
			if (flag)
			{
				MainMenuPatch.openFix = true;
			}
			else
			{
				MainMenuPatch.IsOpen = true;
				Action onMainMenuOpen = MainMenuPatch.OnMainMenuOpen;
				if (onMainMenuOpen != null)
				{
					onMainMenuOpen.Invoke();
				}
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000BA2C File Offset: 0x00009C2C
		private static void OnMMClosed()
		{
			bool flag = !MainMenuPatch.closeFix;
			if (flag)
			{
				MainMenuPatch.closeFix = true;
			}
			else
			{
				MainMenuPatch.IsOpen = false;
				Action onMainMenuClose = MainMenuPatch.OnMainMenuClose;
				if (onMainMenuClose != null)
				{
					onMainMenuClose.Invoke();
				}
			}
		}

		// Token: 0x04000147 RID: 327
		private static bool openFix;

		// Token: 0x04000148 RID: 328
		private static bool closeFix;

		// Token: 0x020000FB RID: 251
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000393 RID: 915
			public static Action <0>__OnMMOpen;

			// Token: 0x04000394 RID: 916
			public static Action <1>__OnMMClosed;
		}
	}
}
