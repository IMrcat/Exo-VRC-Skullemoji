using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppInterop.Runtime;
using Il2CppSystem;
using UnityEngine;
using VRC.Localization;

namespace WorldAPI.ButtonAPI.AM
{
	// Token: 0x0200002A RID: 42
	public class ActionMenuPage
	{
		// Token: 0x06000180 RID: 384 RVA: 0x00008580 File Offset: 0x00006780
		public ActionMenuPage(ActionMenuBaseMenu baseMenu, string buttonText, [Nullable(2)] Texture2D buttonIcon = null)
		{
			bool flag = baseMenu != ActionMenuBaseMenu.MainMenu;
			if (!flag)
			{
				this.menuEntryButton = new ActionMenuButton(ActionMenuBaseMenu.MainMenu, buttonText, new Action(this.OpenMenu), buttonIcon);
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000085C8 File Offset: 0x000067C8
		public ActionMenuPage(ActionMenuPage basePage, string buttonText, [Nullable(2)] Texture2D buttonIcon = null)
		{
			this.previousPage = basePage;
			this.menuEntryButton = new ActionMenuButton(this.previousPage, buttonText, new Action(this.OpenMenu), buttonIcon);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00008604 File Offset: 0x00006804
		public void OpenMenu()
		{
			Action func = delegate
			{
				foreach (ActionMenuButton actionMenuButton in this.buttons)
				{
					ActionMenuOpener actionMenuOpener2 = ActionWheelAPI.GetActionMenuOpener();
					PedalOption pedalOption = ((actionMenuOpener2 != null && actionMenuOpener2.field_Public_ActionMenu_0 != null) ? actionMenuOpener2.field_Public_ActionMenu_0.Method_Public_PedalOption_0() : null);
					bool flag2 = pedalOption == null;
					if (!flag2)
					{
						pedalOption.prop_LocalizableString_0 = actionMenuButton.buttonText.Localize(null, null, null);
						pedalOption.field_Public_Func_1_Boolean_0 = DelegateSupport.ConvertDelegate<Func<bool>>(actionMenuButton.buttonAction);
						bool flag3 = actionMenuButton.buttonIcon != null;
						if (flag3)
						{
							pedalOption.Method_Public_Virtual_Final_New_Void_Texture2D_0(actionMenuButton.buttonIcon);
						}
						actionMenuButton.currentPedalOption = pedalOption;
					}
				}
			};
			ActionMenuOpener actionMenuOpener = ActionWheelAPI.GetActionMenuOpener();
			bool flag = actionMenuOpener != null && actionMenuOpener.field_Public_ActionMenu_0 != null;
			if (flag)
			{
				actionMenuOpener.field_Public_ActionMenu_0.Method_Public_ObjectNPublicAcTeAcLoGaUnique_Action_Action_Texture2D_LocalizableString_0(func, func, null, "it dies without this lol".Localize(null, null, null));
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00008669 File Offset: 0x00006869
		public void AddButton(string buttonText, Action buttonAction, [Nullable(2)] Texture2D buttonIcon = null)
		{
			new ActionMenuButton(this, buttonText, buttonAction, buttonIcon);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00008676 File Offset: 0x00006876
		public void AddToggle(string buttonText, Action<bool> buttonAction, [Nullable(2)] Texture2D onIcon = null, [Nullable(2)] Texture2D offIcon = null)
		{
			new ActionMenuToggle(this, buttonText, buttonAction, false, onIcon, offIcon);
		}

		// Token: 0x040000A8 RID: 168
		public readonly List<ActionMenuButton> buttons = new List<ActionMenuButton>();

		// Token: 0x040000A9 RID: 169
		[Nullable(2)]
		public readonly ActionMenuPage previousPage;

		// Token: 0x040000AA RID: 170
		[Nullable(2)]
		public ActionMenuButton menuEntryButton;
	}
}
