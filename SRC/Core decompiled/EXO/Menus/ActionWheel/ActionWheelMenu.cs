using System;
using EXO.Core;
using EXO.Modules.Utilities;
using EXO_Base;
using UnityEngine;
using WorldAPI.ButtonAPI.AM;

namespace EXO.Menus.ActionWheel
{
	// Token: 0x0200006C RID: 108
	internal class ActionWheelMenu : MenuModule
	{
		// Token: 0x060003AC RID: 940 RVA: 0x00014AE3 File Offset: 0x00012CE3
		public override void LoadLate()
		{
			UtilFunc.WaitForObj("UserInterface/UnscaledUI/ActionWheelMenu/Container/MenuR/ActionWheelMenu", delegate
			{
				ActionMenu AWM = GameObject.Find("UserInterface/UnscaledUI/ActionWheelMenu/Container/MenuR/ActionWheelMenu").GetComponent<ActionMenu>();
				ActionWheelAPI.OpenMainPage(AWM);
				new ActionMenuButton(ActionMenuBaseMenu.MainMenu, "EXO", delegate
				{
					ActionWheelMenu.page.OpenMenu();
				}, BaseImages.FromBase(BaseImages.IconEXO).texture);
			});
		}

		// Token: 0x040001DD RID: 477
		internal static ActionMenuPage page;
	}
}
