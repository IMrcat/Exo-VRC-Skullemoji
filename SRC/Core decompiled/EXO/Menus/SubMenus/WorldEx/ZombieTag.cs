using System;
using EXO.Core;
using UnityEngine;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Buttons.Groups;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Groups;

namespace EXO.Menus.SubMenus.WorldEx
{
	// Token: 0x0200005E RID: 94
	internal class ZombieTag : MenuModule
	{
		// Token: 0x06000367 RID: 871 RVA: 0x000119AC File Offset: 0x0000FBAC
		public override void LoadMenu()
		{
			ZombieTag.subMenu = new VRCPage("Zombie Tag", false, true, false, null, "", null, false);
			ButtonGroup mainGrp = new ButtonGroup(ZombieTag.subMenu, "Zombie Tag", false, TextAnchor.UpperCenter);
			new VRCToggle(mainGrp, "Zombie Door Locks", delegate(bool val)
			{
				if (val)
				{
					GameObject.Find("Game Controller").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "UnlockZombieDoors");
				}
				else
				{
					GameObject.Find("Game Controller").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "LockZombieDoors");
				}
			}, false, "Off", "On", null, null, false);
			new DuoButtons(mainGrp, "Force Start", "Force starts the game", delegate
			{
				GameObject.Find("Game Controller").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "StartGame");
			}, "Force End", "Force ends the game", delegate
			{
				GameObject.Find("Game Controller").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "EndGame");
			});
			new DuoButtons(mainGrp, "Zombie Win", "Makes Zombies win", delegate
			{
				GameObject.Find("Game Controller").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "ZombiesWin");
			}, "Humans Win", "Makes Humans win", delegate
			{
				GameObject.Find("Game Controller").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "HumansWin");
			});
			new DuoButtons(mainGrp, "Unlock Doors", "Unlock All Doors", delegate
			{
				GameObject.Find("Game Controller").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "UnlockAllDoors");
			}, "Close Doors", "Closes all the doors", delegate
			{
				GameObject.Find("Game Controller").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "CloseDoors");
			});
			new VRCButton(mainGrp, "All Lobby Spawn", "Teleports everyone to zombie spawn", delegate
			{
				GameObject.Find("Game Controller").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "TeleportToLobbySpawn");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new DuoButtons(mainGrp, "Zombie Spawn All", "Teleports everyone to zombie spawn", delegate
			{
				GameObject.Find("Game Controller").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "TeleportToZombieSpawn");
			}, "Zombie Spawn All", "Zombie Spawn All", delegate
			{
				GameObject.Find("Game Controller").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "TeleportToHumanSpawn");
			});
			new VRCButton(mainGrp, "Break Game", "Breaks the game", delegate
			{
				GameObject.Find("Game Controller").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "GameBroke");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "Toggle Colliders?", "Toggles Colliders (Not sure what this does)", delegate
			{
				GameObject.Find("Game Controller").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "ToggleColliders");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
		}

		// Token: 0x0400019A RID: 410
		public static VRCPage subMenu;
	}
}
