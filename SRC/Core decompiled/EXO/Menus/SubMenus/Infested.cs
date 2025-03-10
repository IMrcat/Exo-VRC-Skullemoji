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

namespace EXO.Menus.SubMenus
{
	// Token: 0x02000054 RID: 84
	internal class Infested : MenuModule
	{
		// Token: 0x0600033E RID: 830 RVA: 0x0000EF00 File Offset: 0x0000D100
		public override void LoadMenu()
		{
			Infested.subMenu = new VRCPage("Infested", false, true, false, null, "", null, false);
			ButtonGroup mainGrp = new ButtonGroup(Infested.subMenu, "Infested", false, TextAnchor.UpperCenter);
			new VRCToggle(mainGrp, "Semi Auto", delegate(bool val)
			{
				Infested.semiAuto = val;
			}, false, "Off", "On", null, null, false);
			new VRCButton(mainGrp, "Force Start", "Force starts the game", delegate
			{
				GameObject.Find("LobbyManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_ReadyStartGame");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "Kill All", "Kill All", delegate
			{
				GameObject.Find("DamageSync").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "BackStab");
				GameObject.Find("DamageSync").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "BackStabDamage");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new DuoButtons(mainGrp, "Infested Win", "", delegate
			{
				GameObject.Find("LobbyManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_GhostWin");
			}, "Human Win", "", delegate
			{
				GameObject.Find("LobbyManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_HumanWin");
			});
		}

		// Token: 0x0400017A RID: 378
		public static VRCPage subMenu;

		// Token: 0x0400017B RID: 379
		internal static bool semiAuto;
	}
}
