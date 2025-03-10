using System;
using EXO.Core;
using EXO.Modules.Utilities;
using UnityEngine;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Buttons.Groups;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Groups;

namespace EXO.Menus.SubMenus.WorldEx
{
	// Token: 0x0200005C RID: 92
	internal class TerrorsNowhere : MenuModule
	{
		// Token: 0x06000361 RID: 865 RVA: 0x000112C8 File Offset: 0x0000F4C8
		public override void LoadMenu()
		{
			TerrorsNowhere.subMenu = new VRCPage("Terrors Of Nowhere", false, true, false, null, "", null, false);
			ButtonGroup mainGrp = new ButtonGroup(TerrorsNowhere.subMenu, "Terrors Of Nowhere", false, TextAnchor.UpperCenter);
			new VRCToggle(mainGrp, "tog", delegate(bool val)
			{
			}, false, "Off", "On", null, null, false);
			new VRCButton(mainGrp, "KillPlayerSuperKillThem", "SpamGuns", delegate
			{
				UdonUtils.SendUdonEventsWithName("KillPlayerSuperKillThem");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "EndRound", "End Game", delegate
			{
				UdonUtils.SendUdonEventsWithName("EndRound");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new DuoButtons(mainGrp, "StartReboot", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("StartReboot");
			}, "Reboot", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("Reboot");
			});
			new DuoButtons(mainGrp, "UpdateOwner", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("UpdateOwner");
			}, "UpdatePool", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("UpdatePool");
			});
			new DuoButtons(mainGrp, "StartGame", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("StartGame");
			}, "StartingText", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("StartingText");
			});
			new DuoButtons(mainGrp, "KillThisPlayerAll", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("KillThisPlayerAll");
			}, "KillThisPlayer", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("KillThisPlayer");
			});
			new DuoButtons(mainGrp, "PlayerAppearAll", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("PlayerAppearAll");
			}, "PlayerAppear", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("PlayerAppear");
			});
			new DuoButtons(mainGrp, "PlayerIgnoreAll", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("PlayerIgnoreAll");
			}, "PlayerIgnore", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("PlayerIgnore");
			});
			new DuoButtons(mainGrp, "ForceOutOfRoundAll", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("ForceOutOfRoundAll");
			}, "ForceOutOfRound", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("ForceOutOfRound");
			});
			new DuoButtons(mainGrp, "GiveKnife", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("GiveKnife");
			}, "ForceUpdateOwnerShipPerms", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("ForceUpdateOwnerShipPerms");
			});
			new DuoButtons(mainGrp, "UpdateItemPerPlayer", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("UpdateItemPerPlayer");
			}, "MoveItemToPoolLocation", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("MoveItemToPoolLocation");
			});
			new DuoButtons(mainGrp, "AwardPointsIfAliveFire", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("AwardPointsIfAliveFire");
			}, "AwardPointsIfDeadFire", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("AwardPointsIfDeadFire");
			});
			new DuoButtons(mainGrp, "AwardPointsIfAlive", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("AwardPointsIfAlive");
			}, "OniEffect", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("OniEffect");
			});
			new DuoButtons(mainGrp, "SendSignalToRaiseReward", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("SendSignalToRaiseReward");
			}, "TeleportPlayer", "", delegate
			{
				UdonUtils.SendUdonEventsWithName("TeleportPlayer");
			});
		}

		// Token: 0x04000196 RID: 406
		public static VRCPage subMenu;
	}
}
