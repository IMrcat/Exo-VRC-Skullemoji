using System;
using EXO.Core;
using EXO.Menus.SubMenus;
using EXO.Modules.Utilities;
using EXO.WorldAPI.ButtonAPI.QM.Wing;
using EXO_Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Groups;

namespace EXO.Menus
{
	// Token: 0x0200004A RID: 74
	internal class MainMenu : MenuModule
	{
		// Token: 0x06000325 RID: 805 RVA: 0x0000D7C4 File Offset: 0x0000B9C4
		public override void LoadMenu()
		{
			MainMenu.mainPage = new VRCPage("<color=#c00000>EXO</color>", true, false, false, null, "", null, false);
			new Tab(MainMenu.mainPage, "~ EXO ~", BaseImages.FromBase(BaseImages.IconEXO), null, true);
			string grpString = (AppStart.license.IsNullOrEmpty() ? ("User : " + AppStart.username) : ("User : " + AppStart.username + " : " + AppStart.license));
			MainMenu.mainGrp = new ButtonGroup(MainMenu.mainPage, grpString, false, TextAnchor.UpperCenter);
			new VRCButton(MainMenu.mainGrp, "World Cheats", "Open World Cheats Menu", delegate
			{
				WorldCheats.subMenu.OpenMenu();
			}, false, true, BaseImages.FromBase(BaseImages.IconEXO), ExtentedControl.HalfType.Normal, false);
			new VRCButton(MainMenu.mainGrp, "Utils", "Open Utils Menu", delegate
			{
				UtilsMenu.subMenu.OpenMenu();
			}, false, true, BaseImages.FromBase(BaseImages.UtilsIcon), ExtentedControl.HalfType.Normal, false);
			new VRCButton(MainMenu.mainGrp, "Movement", "Open Movement Menu", delegate
			{
				MovementMenu.subMenu.OpenMenu();
			}, false, true, BaseImages.FromBase(BaseImages.MoveIcon), ExtentedControl.HalfType.Normal, false);
			new VRCButton(MainMenu.mainGrp, "Visuals", "Open Visuals Menu", delegate
			{
				VisualsMenu.subMenu.OpenMenu();
			}, false, true, BaseImages.FromBase(BaseImages.EyeIcon), ExtentedControl.HalfType.Normal, false);
			new VRCButton(MainMenu.mainGrp, "Settings", "Open Settings Menu", delegate
			{
				SettingsMenu.subMenu.OpenMenu();
			}, false, true, BaseImages.FromBase(BaseImages.IconDefault), ExtentedControl.HalfType.Normal, false);
			new VRCButton(MainMenu.mainGrp, "Avatar", "Open Avatar Menu", delegate
			{
				AvatarMenu.subMenu.OpenMenu();
			}, false, true, BaseImages.FromBase(BaseImages.IconSub), ExtentedControl.HalfType.Normal, false);
			bool flag = !AppStart.devMode;
			if (!flag)
			{
				new VRCButton(MainMenu.mainGrp, "Dev Menu", "Open Dev Menu", delegate
				{
					DevMenu.subMenu.OpenMenu();
				}, false, true, BaseImages.FromBase(BaseImages.IconEXO), ExtentedControl.HalfType.Normal, false);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000DA2D File Offset: 0x0000BC2D
		// (set) Token: 0x06000327 RID: 807 RVA: 0x0000DA34 File Offset: 0x0000BC34
		internal static VRCPage _lastPage { get; private set; }

		// Token: 0x06000328 RID: 808 RVA: 0x0000DA3C File Offset: 0x0000BC3C
		public override void LoadLate()
		{
			Transform par = UtilFunc.UserInterface.FindObject("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window");
			UtilFunc.WaitForObj("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emoji", delegate
			{
				new WingButton(par, "Open Last Menu", delegate
				{
					VRCPage lastOpenedPage = VRCPage.lastOpenedPage;
					if (lastOpenedPage != null)
					{
						lastOpenedPage.OpenMenu();
					}
					MainMenu._lastPage = VRCPage.lastOpenedPage;
				}, true, BaseImages.FromBase(BaseImages.IconKatana));
				Transform button = par.FindObject("Wing_Left/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emoji");
				GameObject inst = Object.Instantiate<Transform>(button, button.parent).gameObject;
				inst.transform.SetAsFirstSibling();
				TextMeshProUGUI txt = inst.GetComponentInChildren<TextMeshProUGUI>();
				txt.richText = true;
				txt.text = "<color=#c00000>EXO</color>";
				try
				{
					inst.transform.Find("Container/Icon").GetComponent<Image>().overrideSprite = BaseImages.FromBase(BaseImages.IconEXO);
				}
				catch
				{
				}
				Button btn = inst.GetComponent<Button>();
				btn.onClick.RemoveAllListeners();
				btn.onClick.AddListener(delegate
				{
					MainMenu.mainPage.OpenMenu();
				});
				MenuCore.MenuLogs("Left Wing ~> button has been initiated");
			});
			UtilFunc.WaitForObj("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emoji", delegate
			{
				new WingButton(par, "Open Last Menu", delegate
				{
					VRCPage lastOpenedPage2 = VRCPage.lastOpenedPage;
					if (lastOpenedPage2 != null)
					{
						lastOpenedPage2.OpenMenu();
					}
					MainMenu._lastPage = VRCPage.lastOpenedPage;
				}, false, BaseImages.FromBase(BaseImages.IconKatana));
				Transform button2 = par.FindObject("Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emoji");
				GameObject inst2 = Object.Instantiate<Transform>(button2, button2.parent).gameObject;
				inst2.transform.SetAsFirstSibling();
				TextMeshProUGUI txt2 = inst2.GetComponentInChildren<TextMeshProUGUI>();
				txt2.richText = true;
				txt2.text = "<color=#c00000>EXO</color>";
				try
				{
					inst2.transform.Find("Container/Icon").GetComponent<Image>().overrideSprite = BaseImages.FromBase(BaseImages.IconEXO);
				}
				catch
				{
				}
				Button btn2 = inst2.GetComponent<Button>();
				btn2.onClick.RemoveAllListeners();
				btn2.onClick.AddListener(delegate
				{
					MainMenu.mainPage.OpenMenu();
				});
				MenuCore.MenuLogs("Right Wing ~> button has been initiated");
			});
		}

		// Token: 0x04000161 RID: 353
		public static VRCPage mainPage;

		// Token: 0x04000162 RID: 354
		public static ButtonGroup mainGrp;
	}
}
