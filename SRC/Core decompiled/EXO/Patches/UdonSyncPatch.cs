using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using EXO.Core;
using EXO.LogTools;
using EXO.Menus.SelectMenus;
using EXO.Menus.SubMenus;
using EXO.Modules.Utilities;
using EXO.Wrappers;
using UnityEngine;
using VRC;
using VRC.Networking;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using WCv2.Components.WorldPatches;

namespace EXO.Patches
{
	// Token: 0x02000045 RID: 69
	internal class UdonSyncPatch : PatchModule
	{
		// Token: 0x06000309 RID: 777 RVA: 0x0000C358 File Offset: 0x0000A558
		public override void LoadPatch()
		{
			MethodInfo method = typeof(UdonSync).GetMethod("UdonSyncRunProgramAsRPC");
			Func<UdonSync, string, Player, bool> func;
			if ((func = UdonSyncPatch.<>O.<0>__UdonSyncRPCPatch) == null)
			{
				func = (UdonSyncPatch.<>O.<0>__UdonSyncRPCPatch = new Func<UdonSync, string, Player, bool>(UdonSyncPatch.UdonSyncRPCPatch));
			}
			PatchHandler.Detour(method, func);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000C390 File Offset: 0x0000A590
		private static bool UdonSyncRPCPatch(UdonSync __instance, string __0, Player __1)
		{
			bool udonLogger = Config.cfg.UdonLogger;
			if (udonLogger)
			{
				UdonSyncPatch.LogUdon(__instance, __0, __1);
			}
			bool flag5 = UdonSyncPatch.antiUdon;
			bool flag6;
			if (flag5)
			{
				flag6 = false;
			}
			else
			{
				bool flag7 = __0.Equals("GrantControl");
				if (flag7)
				{
					flag6 = false;
				}
				else
				{
					bool localPlr = __1.UserID() == PlayerWrapper.LocalUserID;
					bool flag8 = (__0.StartsWith('_') || __0 == "Trigger") && !localPlr;
					if (flag8)
					{
						CLog.L(__1.DisplayName() + " Called An Internal Event!", true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 49);
					}
					bool flag9 = WorldSelect.trapState && __instance.name.ContiansIgnoreCase("Bear Trap (1)") && __0 == "SyncTrigger";
					if (flag9)
					{
						WorldSelect.homingTrapToggle.State = false;
					}
					bool flag10 = __0 == "SyncAbort" && !localPlr;
					if (flag10)
					{
						CLog.L(__1.DisplayName() + " Called Abort!", true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 55);
					}
					bool godMode = WorldCheats.godMode;
					if (godMode)
					{
						bool flag11 = __0.ContiansIgnoreCase("damageself") || (__0.ContiansIgnoreCase("taggedplayer") && localPlr);
						if (flag11)
						{
							bool godModeLogger = Config.cfg.GodModeLogger;
							if (godModeLogger)
							{
								UdonSyncPatch.GodModeLog(__1.field_Private_APIUser_0.displayName);
							}
							return false;
						}
						bool flag12 = __0.ContiansIgnoreCase("kill") || __0.ContiansIgnoreCase("killed") || __0.ContiansIgnoreCase("trap") || __0.ContiansIgnoreCase("damage") || __0.ContiansIgnoreCase("stun") || __0.ContiansIgnoreCase("backstab") || __0.ContiansIgnoreCase("beentagged") || (__0.ContiansIgnoreCase("hurt") && !localPlr);
						if (flag12)
						{
							bool godModeLogger2 = Config.cfg.GodModeLogger;
							if (godModeLogger2)
							{
								UdonSyncPatch.GodModeLog(__1.field_Private_APIUser_0.displayName);
							}
							return false;
						}
						bool flag13 = __instance.name.ContiansIgnoreCase("killplayer") || __0.ContiansIgnoreCase("killthisplayer") || __0.ContiansIgnoreCase("die");
						if (flag13)
						{
							bool godModeLogger3 = Config.cfg.GodModeLogger;
							if (godModeLogger3)
							{
								UdonSyncPatch.GodModeLog(__1.field_Private_APIUser_0.displayName);
							}
							return false;
						}
					}
					bool flag14 = Murder4.goldGunGlobal && __0 == "NonPatronSkin";
					if (flag14)
					{
						__instance.gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "PatronSkin");
						flag6 = false;
					}
					else
					{
						bool flag15 = Murder4.goldGun && __0 == "NonPatronSkin" && localPlr;
						if (flag15)
						{
							__instance.gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "PatronSkin");
							flag6 = false;
						}
						else
						{
							bool flag16 = Murder4.semiAuto && __0 == "SyncDryFire" && localPlr;
							if (flag16)
							{
								__instance.gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.Owner, "Fire");
								flag6 = false;
							}
							else
							{
								bool flag17 = Murder4.semiAutoGlobal && __0 == "SyncDryFire";
								if (flag17)
								{
									__instance.gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.Owner, "Fire");
									flag6 = false;
								}
								else
								{
									bool flag18 = Murder4.explosiveRounds && __0 == "SyncFire" && localPlr;
									if (flag18)
									{
										Ray ray = new Ray(__instance.gameObject.transform.position, __instance.gameObject.transform.forward);
										RaycastHit raycastHit;
										bool flag19 = Physics.Raycast(ray, out raycastHit);
										if (flag19)
										{
											GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)/Intact").active = true;
											GameObject flag = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)");
											Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, flag);
											flag.transform.position = raycastHit.point;
											GameObject flag2 = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)");
											flag2.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Explode");
										}
									}
									bool flag20 = Murder4.explosiveRoundsGlobal && __0 == "SyncFire";
									if (flag20)
									{
										Ray ray2 = new Ray(__instance.gameObject.transform.position, __instance.gameObject.transform.forward);
										RaycastHit raycastHit2;
										bool flag21 = Physics.Raycast(ray2, out raycastHit2);
										if (flag21)
										{
											GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)/Intact").active = true;
											GameObject flag3 = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)");
											Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, flag3);
											flag3.transform.position = raycastHit2.point;
											GameObject flag4 = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)");
											flag4.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Explode");
										}
									}
									bool flag22 = Ghost.semiAuto && __0.ContiansIgnoreCase("local_endfiring") && localPlr;
									if (flag22)
									{
										bool flag23 = __instance.gameObject.name.ContiansIgnoreCase("T1-M1911");
										if (flag23)
										{
											__instance.gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_FireOneShot");
										}
										bool flag24 = __instance.gameObject.name.ContiansIgnoreCase("T2-DesertEagle");
										if (flag24)
										{
											__instance.gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_FireOneShot");
										}
										bool flag25 = __instance.gameObject.name.ContiansIgnoreCase("T2-m500");
										if (flag25)
										{
											__instance.gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_FireOneShot");
										}
										bool flag26 = __instance.gameObject.name.ContiansIgnoreCase("T4-M107");
										if (flag26)
										{
											__instance.gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_FireOneShot");
										}
										bool flag27 = __instance.gameObject.name.ContiansIgnoreCase("T2-MP7");
										if (flag27)
										{
											__instance.gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "InitializeWeapon");
											__instance.gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartFiring");
										}
										bool flag28 = __instance.gameObject.name.ContiansIgnoreCase("T3-Vector");
										if (flag28)
										{
											__instance.gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "InitializeWeapon");
											__instance.gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartFiring");
										}
										bool flag29 = __instance.gameObject.name.ContiansIgnoreCase("T3-P90");
										if (flag29)
										{
											__instance.gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "InitializeWeapon");
											__instance.gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartFiring");
										}
										bool flag30 = __instance.gameObject.name.ContiansIgnoreCase("T4-M249");
										if (flag30)
										{
											__instance.gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "InitializeWeapon");
											__instance.gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartFiring");
										}
										flag6 = false;
									}
									else
									{
										flag6 = true;
									}
								}
							}
						}
					}
				}
			}
			return flag6;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000CA7C File Offset: 0x0000AC7C
		private static void LogUdon(UdonSync instance, string Event, Player player)
		{
			CLog.Tag();
			"[".WriteToConsole(15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 175).WriteToConsole("UdonSync", 4, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 176).WriteToConsole("] ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 177)
				.WriteToConsole("Obj ", 6, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 178)
				.WriteToConsole("- ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 179)
				.WriteToConsole(instance.name + " ", 6, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 180)
				.WriteToConsole("| ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 181)
				.WriteToConsole("Event ", 11, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 182)
				.WriteToConsole("- ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 183)
				.WriteToConsole(Event + " ", 11, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 184)
				.WriteToConsole("| ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 185)
				.WriteToConsole("User ", 13, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 186)
				.WriteToConsole("- ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 187)
				.WriteLineToConsole(player.field_Private_APIUser_0.displayName + " ", 13, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 188);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000CBE4 File Offset: 0x0000ADE4
		private static void GodModeLog(string player)
		{
			CLog.Tag();
			"[".WriteToConsole(15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 193).WriteToConsole("UdonSync", 4, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 194).WriteToConsole("] ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 195)
				.WriteToConsole("[", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 196)
				.WriteToConsole("GodMode", 6, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 197)
				.WriteToConsole("] ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 198)
				.WriteToConsole("Prevented death from ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 199)
				.WriteLineToConsole(player + " ", 13, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\UdonSyncPatch.cs", 200);
		}

		// Token: 0x04000159 RID: 345
		internal static bool antiUdon;

		// Token: 0x02000103 RID: 259
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040003A6 RID: 934
			public static Func<UdonSync, string, Player, bool> <0>__UdonSyncRPCPatch;
		}
	}
}
