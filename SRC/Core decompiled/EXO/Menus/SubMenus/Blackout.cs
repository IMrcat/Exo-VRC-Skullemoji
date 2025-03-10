using System;
using EXO.Core;
using UnityEngine;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Groups;

namespace EXO.Menus.SubMenus
{
	// Token: 0x02000052 RID: 82
	internal class Blackout : MenuModule
	{
		// Token: 0x0600033A RID: 826 RVA: 0x0000ECEC File Offset: 0x0000CEEC
		public override void LoadMenu()
		{
			Blackout.subMenu = new VRCPage("Blackout", false, true, false, null, "", null, false);
			ButtonGroup mainGrp = new ButtonGroup(Blackout.subMenu, "Blackout", false, TextAnchor.UpperCenter);
		}

		// Token: 0x04000177 RID: 375
		public static VRCPage subMenu;
	}
}
