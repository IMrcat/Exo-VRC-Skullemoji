using System;
using EXO.Core;
using VRC.SDKBase.Validation.Performance;
using WCv2.Components.WorldPatches;

namespace EXO.Patches
{
	// Token: 0x02000044 RID: 68
	internal class ScannerPatch : PatchModule
	{
		// Token: 0x06000307 RID: 775 RVA: 0x0000C314 File Offset: 0x0000A514
		public override void LoadPatch()
		{
			PatchHandler.Detour(typeof(AvatarPerformance).GetMethod("GetPerformanceScannerSet"), (bool _) => false);
		}
	}
}
