using System;
using EXO.Core;
using VRC.SDKBase;
using WCv2.Components.WorldPatches;

namespace EXO.Patches
{
	// Token: 0x0200003D RID: 61
	internal class InteractablePatch : PatchModule
	{
		// Token: 0x060002D5 RID: 725 RVA: 0x0000B669 File Offset: 0x00009869
		public override void LoadPatch()
		{
			PatchHandler.Detour(typeof(VRC_Interactable).GetMethod("Awake"), delegate(VRC_Interactable instance)
			{
				bool interactReach = Config.cfg.InteractReach;
				if (interactReach)
				{
					instance.proximity = float.MaxValue;
				}
			});
		}
	}
}
