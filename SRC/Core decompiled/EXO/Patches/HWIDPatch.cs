using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using CoreRuntime.Manager;
using EXO.Core;
using Il2CppInterop.Runtime;
using Il2CppSystem;
using UnityEngine;
using VRC.Core;

namespace EXO.Patches
{
	// Token: 0x0200003C RID: 60
	internal class HWIDPatch : PatchModule
	{
		// Token: 0x060002D1 RID: 721 RVA: 0x0000B484 File Offset: 0x00009684
		public override void LoadPatch()
		{
			bool flag = !Config.cfg.IdSpoof;
			if (flag)
			{
				PatchCore.PatchLogs("[HWID] Disabled");
			}
			else
			{
				PatchCore.PatchLogs("[HWID] Enabled");
				HWIDPatch.<>c__DisplayClass2_0 CS$<>8__locals1;
				CS$<>8__locals1.currentHWID = SystemInfo.deviceUniqueIdentifier;
				string currentHWID = CS$<>8__locals1.currentHWID;
				bool flag2 = currentHWID != null && currentHWID.Length == 0;
				if (flag2)
				{
					PatchCore.PatchLogs("[HWID] Failed to get HWID");
				}
				else
				{
					bool flag3 = APIUser.CurrentUser != null;
					if (flag3)
					{
						PatchCore.PatchLogs("[HWID] Already logged in, skipping HWID Spoof");
					}
					else
					{
						HWIDPatch.FakeHWID = new global::Il2CppSystem.Object(IL2CPP.ManagedStringToIl2Cpp(HWIDPatch.<LoadPatch>g__GetHWID|2_1(ref CS$<>8__locals1)));
						HookManager.Detour<HWIDPatch._deviceUniqueIdentifierDelegate>(IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetDeviceUniqueIdentifier"), () => HWIDPatch.FakeHWID.Pointer);
					}
				}
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000B55C File Offset: 0x0000975C
		[CompilerGenerated]
		internal static string <LoadPatch>g__GetHWID|2_1(ref HWIDPatch.<>c__DisplayClass2_0 A_0)
		{
			string hwidInfoPath = AppStart.HexedDirectory.FullName + "\\EXO\\HWID_DELETE_IF_BAN.txt";
			bool flag = !File.Exists(hwidInfoPath);
			if (!flag)
			{
				string[] hwidInfo = File.ReadAllText(hwidInfoPath).Split(":", 0);
				string _lastHWID = hwidInfo[0];
				string _lastSpoofHWID = hwidInfo[1];
				bool flag2 = _lastHWID != A_0.currentHWID;
				if (!flag2)
				{
					PatchCore.PatchLogs("[HWID] Using HWID " + _lastSpoofHWID + "..");
					return _lastSpoofHWID;
				}
			}
			File.WriteAllText(hwidInfoPath, A_0.currentHWID + ":" + HWIDPatch.<LoadPatch>g__GenerateHWID|2_2(ref A_0));
			return HWIDPatch.<LoadPatch>g__GetHWID|2_1(ref A_0);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000B604 File Offset: 0x00009804
		[CompilerGenerated]
		internal static string <LoadPatch>g__GenerateHWID|2_2(ref HWIDPatch.<>c__DisplayClass2_0 A_0)
		{
			byte[] bytes = new byte[A_0.currentHWID.Length / 2];
			new global::System.Random(Environment.TickCount).NextBytes(bytes);
			return string.Join("", Enumerable.Select<byte, string>(bytes, (byte it) => it.ToString("x2")));
		}

		// Token: 0x0400013F RID: 319
		private static global::Il2CppSystem.Object FakeHWID;

		// Token: 0x020000F5 RID: 245
		// (Invoke) Token: 0x06000743 RID: 1859
		private delegate IntPtr _deviceUniqueIdentifierDelegate();
	}
}
