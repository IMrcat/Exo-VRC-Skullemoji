using System;
using System.Collections;
using System.Collections.Generic;
using CoreRuntime.Manager;
using EXO.Core;
using EXO.Menus.SubMenus.Settings;
using EXO.Modules.Utilities;
using UnityEngine;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Buttons.Groups;
using WorldAPI.ButtonAPI.Groups;

namespace EXO.Menus.SubMenus.WorldEx
{
	// Token: 0x0200005D RID: 93
	internal class ZombieSurvival : MenuModule
	{
		// Token: 0x06000363 RID: 867 RVA: 0x000117DC File Offset: 0x0000F9DC
		public override void LoadMenu()
		{
			ZombieSurvival.subMenu = new VRCPage("Zombie Survival", false, true, false, null, "", null, false);
			ButtonGroup mainGrp = new ButtonGroup(ZombieSurvival.subMenu, "Zombie Survival", false, TextAnchor.UpperCenter);
			new VRCToggle(mainGrp, "Kill Loop", delegate(bool val)
			{
				ZombieSurvival.killState = val;
				SettingsMenu.antiUdonBtn.State = !val;
				LogMenu.logUdonBtn.State = !val;
				CoroutineManager.RunCoroutine(ZombieSurvival.KillLoop());
			}, false, "Off", "On", null, null, false);
			new VRCToggle(mainGrp, "Spam Guns", delegate(bool val)
			{
				ZombieSurvival.shootState = val;
				SettingsMenu.antiUdonBtn.State = !val;
				LogMenu.logUdonBtn.State = !val;
				CoroutineManager.RunCoroutine(ZombieSurvival.SpamGuns());
			}, false, "Off", "On", null, null, false);
			new DuoButtons(mainGrp, "WeaponHolsterAll", "WeaponHolsterAll", delegate
			{
				List<UdonBehaviour> weaponSpawner = GameObject.Find("Weapon Spawner").GetAllComponentsInChildren<UdonBehaviour>();
				List<UdonBehaviour> lobbyWeapons = GameObject.Find("Lobby Weapons").GetAllComponentsInChildren<UdonBehaviour>();
				foreach (UdonBehaviour udon in weaponSpawner)
				{
					udon.SendCustomNetworkEvent(NetworkEventTarget.All, "WeaponHolsterAll");
				}
				foreach (UdonBehaviour udon2 in lobbyWeapons)
				{
					udon2.SendCustomNetworkEvent(NetworkEventTarget.All, "WeaponHolsterAll");
				}
			}, "WeaponDropAll", "WeaponDropAll", delegate
			{
				List<UdonBehaviour> weaponSpawner2 = GameObject.Find("Weapon Spawner").GetAllComponentsInChildren<UdonBehaviour>();
				List<UdonBehaviour> lobbyWeapons2 = GameObject.Find("Lobby Weapons").GetAllComponentsInChildren<UdonBehaviour>();
				foreach (UdonBehaviour udon3 in weaponSpawner2)
				{
					udon3.SendCustomNetworkEvent(NetworkEventTarget.All, "WeaponDropAll");
				}
				foreach (UdonBehaviour udon4 in lobbyWeapons2)
				{
					udon4.SendCustomNetworkEvent(NetworkEventTarget.All, "WeaponDropAll");
				}
			});
			new DuoButtons(mainGrp, "DieMaster", "DieMaster", delegate
			{
				UdonUtils.SendUdonEventsWithName("DieMaster");
			}, "VerifyPlayers", "VerifyPlayers", delegate
			{
				UdonUtils.SendUdonEventsWithName("VerifyPlayers");
			});
			new DuoButtons(mainGrp, "ClipReloadAll", "ClipReloadAll", delegate
			{
				List<UdonBehaviour> weaponSpawner3 = GameObject.Find("Weapon Spawner").GetAllComponentsInChildren<UdonBehaviour>();
				List<UdonBehaviour> lobbyWeapons3 = GameObject.Find("Lobby Weapons").GetAllComponentsInChildren<UdonBehaviour>();
				foreach (UdonBehaviour udon5 in weaponSpawner3)
				{
					udon5.SendCustomNetworkEvent(NetworkEventTarget.All, "ClipReloadAll");
				}
				foreach (UdonBehaviour udon6 in lobbyWeapons3)
				{
					udon6.SendCustomNetworkEvent(NetworkEventTarget.All, "ClipReloadAll");
				}
			}, "ShootEffect", "ShootEffect", delegate
			{
				UdonUtils.SendUdonEventsWithName("ShootEffect");
			});
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00011993 File Offset: 0x0000FB93
		internal static IEnumerator SpamGuns()
		{
			List<UdonBehaviour> weaponSpawner = GameObject.Find("Weapon Spawner").GetAllComponentsInChildren<UdonBehaviour>();
			List<UdonBehaviour> lobbyWeapons = GameObject.Find("Lobby Weapons").GetAllComponentsInChildren<UdonBehaviour>();
			bool flag;
			do
			{
				foreach (UdonBehaviour udon in weaponSpawner)
				{
					udon.SendCustomNetworkEvent(NetworkEventTarget.All, "ShootEffect");
					udon = null;
				}
				List<UdonBehaviour>.Enumerator enumerator = default(List<UdonBehaviour>.Enumerator);
				foreach (UdonBehaviour udon2 in lobbyWeapons)
				{
					udon2.SendCustomNetworkEvent(NetworkEventTarget.All, "ShootEffect");
					udon2 = null;
				}
				List<UdonBehaviour>.Enumerator enumerator2 = default(List<UdonBehaviour>.Enumerator);
				yield return new WaitForSeconds(0.1f);
				flag = !ZombieSurvival.shootState;
			}
			while (!flag);
			yield break;
			yield break;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0001199B File Offset: 0x0000FB9B
		internal static IEnumerator KillLoop()
		{
			UdonBehaviour udon = GameObject.Find("Player Object Assigner").GetComponent<UdonBehaviour>();
			bool flag;
			do
			{
				try
				{
					udon.SendCustomNetworkEvent(NetworkEventTarget.All, "Die");
				}
				catch
				{
				}
				yield return new WaitForSeconds(0.1f);
				flag = !ZombieSurvival.killState;
			}
			while (!flag);
			yield break;
			yield break;
		}

		// Token: 0x04000197 RID: 407
		public static VRCPage subMenu;

		// Token: 0x04000198 RID: 408
		internal static bool killState;

		// Token: 0x04000199 RID: 409
		internal static bool shootState;
	}
}
