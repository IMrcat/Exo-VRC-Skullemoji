using System;
using EXO.Core;
using EXO.Modules.Utilities;
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
	// Token: 0x02000059 RID: 89
	internal class MagicFreeze : MenuModule
	{
		// Token: 0x06000358 RID: 856 RVA: 0x00010C98 File Offset: 0x0000EE98
		public override void LoadMenu()
		{
			MagicFreeze.subMenu = new VRCPage("Magic Freeze Tag", false, true, false, null, "", null, false);
			ButtonGroup mainGrp = new ButtonGroup(MagicFreeze.subMenu, "Magic Freeze", false, TextAnchor.UpperCenter);
			new VRCToggle(mainGrp, "All Patreon", delegate(bool val)
			{
				if (val)
				{
					GameObject.Find("Patreon Data").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "GetPatronTier");
				}
			}, false, "Off", "On", null, null, false);
			new VRCToggle(mainGrp, "Remove Obstacles", delegate(bool val)
			{
				GameObject.Find("Game Logic/Obstacle Set (Gates)").SetActive(!val);
				GameObject.Find("Game Logic/Obstacle Set (Lasers)").SetActive(!val);
			}, false, "Off", "On", null, null, false);
			new DuoButtons(mainGrp, "Force Start", "Force starts the game", delegate
			{
				GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Btn_Start");
			}, "Force End", "Force Ends The Game", delegate
			{
				GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "NobodyVictory");
			});
			new DuoButtons(mainGrp, "Taggers Wins", "Makes Taggers Win", delegate
			{
				GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "TaggerVictory");
			}, "Runners Win", "Makes Runners Win", delegate
			{
				GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "RunnerVictory");
			});
			new DuoButtons(mainGrp, "All Taggers", "All Taggers", delegate
			{
				UdonUtils.SendUdonEventsWithName("SpawnAsTagger");
				GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SpawnAsTagger");
			}, "All Runners", "All Runners", delegate
			{
				UdonUtils.SendUdonEventsWithName("SpawnAsRunner");
				GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SpawnAsRunner");
			});
			new VRCButton(mainGrp, "Tp All Random", "Teleports Everyone to a Random spot on the map (or Spawn after a round)", delegate
			{
				GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "TeleportToRandom");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "Master Lock", "Toggles the start game lock", delegate
			{
				GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Tgl_MasterLock");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "Unfreeze All", "Unfreeze Everyone", delegate
			{
				UdonUtils.SendUdonEventsWithName("Unfreeze");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
		}

		// Token: 0x04000192 RID: 402
		public static VRCPage subMenu;
	}
}
