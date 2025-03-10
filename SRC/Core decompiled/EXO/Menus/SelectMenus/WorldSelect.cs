using System;
using System.Collections;
using CoreRuntime.Manager;
using EXO.Core;
using EXO.Menus.SubMenus;
using EXO.Modules.Utilities;
using EXO.Patches;
using EXO.Wrappers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Buttons.Groups;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Groups;

namespace EXO.Menus.SelectMenus
{
	// Token: 0x02000069 RID: 105
	internal class WorldSelect : MenuModule
	{
		// Token: 0x06000387 RID: 903 RVA: 0x0001396C File Offset: 0x00011B6C
		internal static void AddActions()
		{
			VRCPage vrcpage = WorldSelect.selectPage;
			vrcpage.OnMenuOpen = (Action)Delegate.Combine(vrcpage.OnMenuOpen, delegate
			{
				SelectMenu.UpdateDisplayTarget(WorldSelect.target, WorldSelect.selectPage);
			});
			JoinLeavePatch.OnPlayerLeave = (Action<Player>)Delegate.Combine(JoinLeavePatch.OnPlayerLeave, delegate(Player player)
			{
				bool flag = player == WorldSelect.target;
				if (flag)
				{
					WorldSelect.killLoopToggle.State = false;
					WorldSelect.explodeLoopToggle.State = false;
					WorldSelect.homingTrapToggle.State = false;
					WorldSelect.pointStackToggle.State = false;
				}
			});
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000139E8 File Offset: 0x00011BE8
		public override void LoadMenu()
		{
			WorldSelect.selectPage = new VRCPage("World Cheats", false, true, false, null, "", null, false);
			CollapsibleButtonGroup worldSelGrp = new CollapsibleButtonGroup(WorldSelect.selectPage, "Murder | Among Us", true);
			WorldSelect.AddActions();
			new DuoButtons(worldSelGrp, "Set Murder", "Sets selected player as Murderer", delegate
			{
				WorldSelect.target = PlayerWrapper.GetSelectedUser;
				WorldSelect.SendJarEvent(WorldSelect.target._vrcplayer.DisplayName(), "SyncAssignM");
			}, "Set Bystander", "Sets selected player as Bystander", delegate
			{
				WorldSelect.target = PlayerWrapper.GetSelectedUser;
				WorldSelect.SendJarEvent(WorldSelect.target._vrcplayer.DisplayName(), "SyncAssignB");
			});
			new VRCButton(worldSelGrp, "Explode", "Explodes the selected player", delegate
			{
				WorldSelect.target = PlayerWrapper.GetSelectedUser;
				GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)/Intact").active = true;
				GameObject flag = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)");
				Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, flag);
				flag.transform.position = WorldSelect.target.transform.position + new Vector3(0f, 0.1f, 0f);
				GameObject flag2 = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)");
				flag2.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Explode");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(worldSelGrp, "Kill", "Kills the selected player", delegate
			{
				WorldSelect.target = PlayerWrapper.GetSelectedUser;
				WorldSelect.SendJarEvent(WorldSelect.target._vrcplayer.DisplayName(), "SyncKill");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			WorldSelect.killLoopToggle = new VRCToggle(worldSelGrp, "Kill Loop", delegate(bool val)
			{
				WorldSelect.target = PlayerWrapper.GetSelectedUser;
				WorldSelect.killState = val;
				CoroutineManager.RunCoroutine(WorldSelect.KillLoop());
			}, false, "Off", "On", null, null, false);
			WorldSelect.explodeLoopToggle = new VRCToggle(worldSelGrp, "Explode Loop", delegate(bool val)
			{
				WorldSelect.target = PlayerWrapper.GetSelectedUser;
				WorldSelect.explodeState = val;
				CoroutineManager.RunCoroutine(WorldSelect.ExplodeLoop());
			}, false, "Off", "On", null, null, false);
			WorldSelect.homingTrapToggle = new VRCToggle(worldSelGrp, "Homing Trap", delegate(bool val)
			{
				WorldSelect.target = PlayerWrapper.GetSelectedUser;
				WorldSelect.trapState = val;
				if (val)
				{
					CoroutineManager.RunCoroutine(WorldSelect.HomingTrap(WorldSelect.target));
				}
			}, false, "Places a trap under the selected player that follows them around", "On", null, null, false);
			new DuoButtons(worldSelGrp, "Homing Grenade", "Spawns a grenade that will follow the selected player", delegate
			{
				WorldSelect.target = PlayerWrapper.GetSelectedUser;
				CoroutineManager.RunCoroutine(WorldSelect.HomingGrenade(WorldSelect.target));
			}, "Detonate", "Detonates Homing Grenade", delegate
			{
				WorldSelect.homingExplodeState = true;
			});
			CollapsibleButtonGroup prisonSelGrp = new CollapsibleButtonGroup(WorldSelect.selectPage, "Prison Escape", false);
			new DuoButtons(prisonSelGrp, "+ Points", "Add Points to selected player", delegate
			{
				PrisonEsc.EventOnPlayer(WorldSelect.target._vrcplayer, "__0__AddPoints");
			}, "Clear Scorecard", "Clear Scorecard for selected player", delegate
			{
				PrisonEsc.EventOnPlayer(WorldSelect.target._vrcplayer, "_ClearScorecard");
			});
			new DuoButtons(prisonSelGrp, "+ Guard Kill", "Add Guard Kill to selected player", delegate
			{
				PrisonEsc.EventOnPlayer(WorldSelect.target._vrcplayer, "_AddGuardKill");
			}, "+ Prisoner Kill", "Add Prisoner Kill to selected player", delegate
			{
				PrisonEsc.EventOnPlayer(WorldSelect.target._vrcplayer, "__0__AddPrisKill");
			});
			new DuoButtons(prisonSelGrp, "+ Escape Win", "Add Escape Win to selected player", delegate
			{
				PrisonEsc.EventOnPlayer(WorldSelect.target._vrcplayer, "_AddEscape");
			}, "+ Guard Win", "Add Escape Win to selected player", delegate
			{
				PrisonEsc.EventOnPlayer(WorldSelect.target._vrcplayer, "_AddWin");
			});
			WorldSelect.pointStackToggle = new VRCToggle(prisonSelGrp, "+ Points Loop", delegate(bool val)
			{
				WorldSelect.target = PlayerWrapper.GetSelectedUser;
				WorldSelect.pointState = val;
				if (val)
				{
					PrisonEsc.EventOnPlayer(WorldSelect.target._vrcplayer, "_AddEscape");
				}
				CoroutineManager.RunCoroutine(WorldSelect.PrisonPointsLoop(WorldSelect.target));
			}, false, "Off", "On", null, null, false);
			bool devMode = AppStart.devMode;
			if (devMode)
			{
				new VRCButton(prisonSelGrp, "get_IsEmpty", "get_IsEmpty", delegate
				{
					WorldSelect.target = PlayerWrapper.GetSelectedUser;
					PrisonEsc.EventOnPlayer(WorldSelect.target._vrcplayer, "get_IsEmpty");
				}, false, false, null, ExtentedControl.HalfType.Normal, false);
				new VRCButton(prisonSelGrp, "Kill Prisoner", "Beta Feature, may not work as intended. Prison Escape Only!", delegate
				{
					WorldSelect.target = PlayerWrapper.GetSelectedUser;
					PrisonEsc.EventOnPlayer(WorldSelect.target._vrcplayer, "Damage250");
					PrisonEsc.GetPlayerData(WorldSelect.target.DisplayName()).GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Damage250");
				}, false, false, null, ExtentedControl.HalfType.Normal, false);
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00013DC8 File Offset: 0x00011FC8
		internal static IEnumerator KillLoop()
		{
			bool flag;
			do
			{
				WorldSelect.SendJarEvent(WorldSelect.target._vrcplayer.DisplayName(), "SyncAssignB");
				yield return new WaitForSeconds(0f);
				WorldSelect.SendJarEvent(WorldSelect.target._vrcplayer.DisplayName(), "SyncKill");
				yield return new WaitForSeconds(0f);
				flag = !WorldSelect.killState;
			}
			while (!flag);
			yield break;
			yield break;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00013DD0 File Offset: 0x00011FD0
		internal static IEnumerator PrisonPointsLoop(Player player)
		{
			bool flag;
			do
			{
				PrisonEsc.EventOnPlayer(player._vrcplayer, "__0__AddPoints");
				yield return new WaitForSeconds(0.1f);
				flag = !WorldSelect.pointState;
			}
			while (!flag);
			yield break;
			yield break;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00013DDF File Offset: 0x00011FDF
		internal static IEnumerator ExplodeLoop()
		{
			for (;;)
			{
				GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)/Intact").active = true;
				GameObject flag = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)");
				Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, flag);
				flag.transform.position = WorldSelect.target.transform.position + new Vector3(0f, 0.1f, 0f);
				GameObject flag2 = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)");
				flag2.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Explode");
				yield return new WaitForSeconds(0f);
				bool flag3 = !WorldSelect.explodeState;
				if (flag3)
				{
					break;
				}
				flag = null;
				flag2 = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00013DE7 File Offset: 0x00011FE7
		internal static IEnumerator HomingTrap(Player target)
		{
			GameObject obj = GameObject.Find("Game Logic/Weapons/Bear Trap (1)");
			bool flag = obj;
			if (flag)
			{
				obj.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncDeploy");
				Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, obj);
			}
			bool flag3;
			do
			{
				bool flag2 = obj;
				if (flag2)
				{
					obj.transform.position = target.transform.position + new Vector3(0f, 0.1f, 0f);
				}
				yield return new WaitForSeconds(0.1f);
				flag3 = !WorldSelect.trapState;
			}
			while (!flag3);
			yield break;
			yield break;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00013DF6 File Offset: 0x00011FF6
		internal static IEnumerator HomingGrenade(Player target)
		{
			GameObject obj = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)");
			bool flag = obj;
			if (flag)
			{
				GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)/Intact").active = true;
				Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, obj);
			}
			float syncArmTimer = 0f;
			while (!WorldSelect.homingExplodeState)
			{
				bool flag2 = obj;
				if (flag2)
				{
					bool flag3 = syncArmTimer >= 1f;
					if (flag3)
					{
						obj.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncArm");
						syncArmTimer = 0f;
					}
					obj.transform.position = target.transform.position + new Vector3(0f, 0.5f, 0f);
				}
				yield return new WaitForSeconds(0.05f);
				syncArmTimer += 0.05f;
			}
			int num;
			for (int i = 0; i < 60; i = num + 1)
			{
				bool flag4 = obj;
				if (flag4)
				{
					obj.transform.position = target.transform.position + new Vector3(0f, 0.5f, 0f);
				}
				yield return new WaitForSeconds(0.05f);
				num = i;
			}
			WorldSelect.homingExplodeState = false;
			yield break;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00013E08 File Offset: 0x00012008
		internal static void SendJarEvent(string userDisplayName, string uEvent)
		{
			for (int i = 0; i < 30; i++)
			{
				string playerEntryPath = "Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + i.ToString() + ")/Player Name Text";
				Transform playerEntryTransform = GameObject.Find("Game Logic").FindObject(playerEntryPath);
				GameObject playerEntry = ((playerEntryTransform != null) ? playerEntryTransform.gameObject : null);
				bool flag = playerEntry != null;
				if (flag)
				{
					TMP_Text tmpTextComponent = playerEntry.GetComponent<TMP_Text>();
					TextMeshProUGUI textMeshProUGUIComponent = playerEntry.GetComponent<TextMeshProUGUI>();
					Text textComponent = playerEntry.GetComponent<Text>();
					bool flag2 = (tmpTextComponent != null && tmpTextComponent.text.Equals(userDisplayName)) || (textMeshProUGUIComponent != null && textMeshProUGUIComponent.text.Equals(userDisplayName)) || (tmpTextComponent == null && textComponent != null && textComponent.text.Equals(userDisplayName));
					if (flag2)
					{
						GameObject.Find("Player Node (" + i.ToString() + ")").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, uEvent);
						break;
					}
				}
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00013F20 File Offset: 0x00012120
		internal static GameObject GetPlayerNode(string userDisplayName)
		{
			for (int i = 0; i < 30; i++)
			{
				string playerEntryPath = "Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + i.ToString() + ")/Player Name Text";
				Transform playerEntryTransform = GameObject.Find("Game Logic").FindObject(playerEntryPath);
				GameObject playerEntry = ((playerEntryTransform != null) ? playerEntryTransform.gameObject : null);
				bool flag = playerEntry != null;
				if (flag)
				{
					TMP_Text tmpTextComponent = playerEntry.GetComponent<TMP_Text>();
					Text textComponent = playerEntry.GetComponent<Text>();
					bool flag2 = (tmpTextComponent != null && tmpTextComponent.text.Equals(userDisplayName)) || (tmpTextComponent == null && textComponent != null && textComponent.text.Equals(userDisplayName));
					if (flag2)
					{
						return GameObject.Find("Player Node (" + i.ToString() + ")");
					}
				}
			}
			return null;
		}

		// Token: 0x040001C3 RID: 451
		internal static Player target;

		// Token: 0x040001C4 RID: 452
		public static VRCPage selectPage;

		// Token: 0x040001C5 RID: 453
		internal static bool pointState;

		// Token: 0x040001C6 RID: 454
		internal static bool killState;

		// Token: 0x040001C7 RID: 455
		internal static bool explodeState;

		// Token: 0x040001C8 RID: 456
		internal static bool trapState;

		// Token: 0x040001C9 RID: 457
		internal static bool homingExplodeState;

		// Token: 0x040001CA RID: 458
		internal static VRCToggle killLoopToggle;

		// Token: 0x040001CB RID: 459
		internal static VRCToggle explodeLoopToggle;

		// Token: 0x040001CC RID: 460
		internal static VRCToggle homingTrapToggle;

		// Token: 0x040001CD RID: 461
		internal static VRCToggle pointStackToggle;
	}
}
