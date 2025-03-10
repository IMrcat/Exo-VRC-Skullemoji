using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using EXO.Core;
using EXO.Functions.MenuOverrides;
using VRC.UI.Elements;
using WCv2.Components.WorldPatches;

namespace EXO.Patches
{
	// Token: 0x02000043 RID: 67
	internal class QuickMenuPatch : PatchModule
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000C1E4 File Offset: 0x0000A3E4
		// (set) Token: 0x060002FE RID: 766 RVA: 0x0000C1EB File Offset: 0x0000A3EB
		internal static Action OnQuickMenuOpen { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000C1F3 File Offset: 0x0000A3F3
		// (set) Token: 0x06000300 RID: 768 RVA: 0x0000C1FA File Offset: 0x0000A3FA
		internal static Action OnQuickMenuClose { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000C202 File Offset: 0x0000A402
		// (set) Token: 0x06000302 RID: 770 RVA: 0x0000C209 File Offset: 0x0000A409
		internal static bool IsOpen { get; private set; }

		// Token: 0x06000303 RID: 771 RVA: 0x0000C214 File Offset: 0x0000A414
		public override void LoadPatch()
		{
			MethodInfo method = typeof(QuickMenu).GetMethod("OnEnable");
			Action action;
			if ((action = QuickMenuPatch.<>O.<0>__OnQMOpen) == null)
			{
				action = (QuickMenuPatch.<>O.<0>__OnQMOpen = new Action(QuickMenuPatch.OnQMOpen));
			}
			PatchHandler.Detour(method, action);
			MethodInfo method2 = typeof(QuickMenu).GetMethod("OnDisable");
			Action action2;
			if ((action2 = QuickMenuPatch.<>O.<1>__OnQMClosed) == null)
			{
				action2 = (QuickMenuPatch.<>O.<1>__OnQMClosed = new Action(QuickMenuPatch.OnQMClosed));
			}
			PatchHandler.Detour(method2, action2);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000C28C File Offset: 0x0000A48C
		private static void OnQMOpen()
		{
			ColorPaletteOverride.EnableVRCPlus();
			bool flag = !QuickMenuPatch.openFix;
			if (flag)
			{
				QuickMenuPatch.openFix = true;
			}
			else
			{
				QuickMenuPatch.IsOpen = true;
				Action onQuickMenuOpen = QuickMenuPatch.OnQuickMenuOpen;
				if (onQuickMenuOpen != null)
				{
					onQuickMenuOpen.Invoke();
				}
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000C2D0 File Offset: 0x0000A4D0
		private static void OnQMClosed()
		{
			bool flag = !QuickMenuPatch.closeFix;
			if (flag)
			{
				QuickMenuPatch.closeFix = true;
			}
			else
			{
				QuickMenuPatch.IsOpen = false;
				Action onQuickMenuClose = QuickMenuPatch.OnQuickMenuClose;
				if (onQuickMenuClose != null)
				{
					onQuickMenuClose.Invoke();
				}
			}
		}

		// Token: 0x04000156 RID: 342
		private static bool openFix;

		// Token: 0x04000157 RID: 343
		private static bool closeFix;

		// Token: 0x02000101 RID: 257
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040003A2 RID: 930
			public static Action <0>__OnQMOpen;

			// Token: 0x040003A3 RID: 931
			public static Action <1>__OnQMClosed;
		}
	}
}
