using System;
using System.Linq;
using System.Reflection;
using EXO.Core;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon.Wrapper.Modules;
using WCv2.Components.WorldPatches;

namespace EXO.Patches
{
	// Token: 0x02000042 RID: 66
	internal class PickUpPatch : PatchModule
	{
		// Token: 0x060002FB RID: 763 RVA: 0x0000C058 File Offset: 0x0000A258
		public override void LoadPatch()
		{
			PatchHandler.Detour(typeof(ExternVRCSDK3ComponentsVRCPickup).GetMethod("__set_DisallowTheft__SystemBoolean"), () => !Config.cfg.ForceGrab);
			PatchHandler.Detour(typeof(ExternVRCSDK3ComponentsVRCPickup).GetMethod("__set_pickupable__SystemBoolean"), () => !Config.cfg.ForceGrab);
			PatchHandler.Detour(typeof(ExternVRCSDK3ComponentsVRCPickup).GetMethod("__set_allowManipulationWhenEquipped__SystemBoolean"), () => !Config.cfg.ForceGrab);
			PatchHandler.Detour(typeof(ExternVRCSDK3ComponentsVRCPickup).GetMethod("__set_proximity__SystemSingle"), () => !Config.cfg.ForceGrab);
			PatchHandler.Detour(Enumerable.First<MethodInfo>(typeof(VRCPickup).GetMethods(), (MethodInfo x) => x.Name == "Drop" && x.GetParameters().Length == 1), () => !Config.cfg.AntiDrop);
			PatchHandler.Detour(typeof(VRC_Pickup).GetMethod("Awake"), delegate(VRC_Pickup instance)
			{
				bool flag = instance.name == "MirrorPickup";
				if (!flag)
				{
					bool forceGrab = Config.cfg.ForceGrab;
					if (forceGrab)
					{
						instance.DisallowTheft = false;
						instance.pickupable = true;
						instance.allowManipulationWhenEquipped = true;
					}
					bool itemReach = Config.cfg.ItemReach;
					if (itemReach)
					{
						instance.proximity = float.MaxValue;
					}
				}
			});
		}
	}
}
