using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using EXO.Core;
using EXO.Functions.MenuOverrides;
using WCv2.Components.WorldPatches;

namespace EXO.Patches
{
	// Token: 0x02000039 RID: 57
	internal class ActionWheelPatch : PatchModule
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x0000AF88 File Offset: 0x00009188
		public override void LoadPatch()
		{
			MethodInfo method = typeof(ActionMenu).GetMethod("OnEnable");
			Action action;
			if ((action = ActionWheelPatch.<>O.<0>__OnOpen) == null)
			{
				action = (ActionWheelPatch.<>O.<0>__OnOpen = new Action(ActionWheelPatch.OnOpen));
			}
			PatchHandler.Detour(method, action);
			MethodInfo method2 = typeof(ActionMenu).GetMethod("OnDisable");
			Action action2;
			if ((action2 = ActionWheelPatch.<>O.<1>__OnClose) == null)
			{
				action2 = (ActionWheelPatch.<>O.<1>__OnClose = new Action(ActionWheelPatch.OnClose));
			}
			PatchHandler.Detour(method2, action2);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000B000 File Offset: 0x00009200
		private static void OnOpen()
		{
			ActionMenuOverride.Init();
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000B009 File Offset: 0x00009209
		private static void OnClose()
		{
			ActionMenuOverride.Init();
		}

		// Token: 0x020000EF RID: 239
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400037F RID: 895
			public static Action <0>__OnOpen;

			// Token: 0x04000380 RID: 896
			public static Action <1>__OnClose;
		}
	}
}
