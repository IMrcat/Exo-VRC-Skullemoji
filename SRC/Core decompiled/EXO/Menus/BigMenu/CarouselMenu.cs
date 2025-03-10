using System;
using EXO.Core;
using EXO.LogTools;
using EXO.Modules.Utilities;
using EXO_Base;
using UnityEngine;
using WorldAPI.ButtonAPI.MM;
using WorldAPI.ButtonAPI.MM.Carousel;
using WorldAPI.ButtonAPI.MM.Carousel.Items;
using WorldAPI.ButtonAPI.QM.Extras;

namespace EXO.Menus.BigMenu
{
	// Token: 0x0200006B RID: 107
	internal class CarouselMenu : MenuModule
	{
		// Token: 0x060003AA RID: 938 RVA: 0x000146E8 File Offset: 0x000128E8
		public override void LoadMenu()
		{
			bool flag = !AppStart.devMode;
			if (!flag)
			{
				VRCUiManager field_Private_Static_VRCUiManager_ = VRCUiManager.field_Private_Static_VRCUiManager_0;
				if (field_Private_Static_VRCUiManager_ != null)
				{
					GameObject gameObject = field_Private_Static_VRCUiManager_.gameObject;
					if (gameObject != null)
					{
						gameObject.FindObject("Canvas_MainMenu(Clone)/Container/PageButtons/HorizontalLayoutGroup/Page_Help&Info").SetActive(false);
					}
				}
				VRCUiManager field_Private_Static_VRCUiManager_2 = VRCUiManager.field_Private_Static_VRCUiManager_0;
				if (field_Private_Static_VRCUiManager_2 != null)
				{
					GameObject gameObject2 = field_Private_Static_VRCUiManager_2.gameObject;
					if (gameObject2 != null)
					{
						gameObject2.FindObject("Canvas_MainMenu(Clone)/Container/PageButtons/HorizontalLayoutGroup/Page_Marketplace").SetActive(false);
					}
				}
				CarouselMenu.carousel = new MMCarousel("EXO_Root", "EXO", BaseImages.FromBase(BaseImages.IconEXO));
				CarouselMenu.carousel.SetExtraButtons("Name 1", delegate
				{
				}, "text 1", "Name 2", delegate
				{
				}, "text 2", BaseImages.FromBase(BaseImages.IconDefault), BaseImages.FromBase(BaseImages.IconDefault));
				new MMTab(CarouselMenu.carousel, "EXO Tab", BaseImages.FromBase(BaseImages.IconEXO));
				CarouselMenu.rootMenu = new CMenu(CarouselMenu.carousel, "Init Menu", "", BaseImages.FromBase(BaseImages.IconKatana));
				CarouselMenu.secondMenu = new CMenu(CarouselMenu.carousel, "Second Menu", "", BaseImages.FromBase(BaseImages.IconKatana));
				CGrp mainGrp = new CGrp(CarouselMenu.rootMenu, "EXO Group", true, true);
				new VRCSlider(mainGrp, "Slider", "Slider Tool tip", delegate(float val)
				{
					CLog.L(val.ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\BigMenu\\CarouselMenu.cs", 49);
				}, 0f, 0f, 100f).Button(delegate
				{
					CLog.L("Slider Button", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\BigMenu\\CarouselMenu.cs", 52);
				}, "Slider Toggle", null).Toggle("Slider Toggle", delegate(bool val)
				{
					CLog.L(val.ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\BigMenu\\CarouselMenu.cs", 55);
				}, false, null, null, null, null);
				new CToggle(mainGrp, "Test Toggle", delegate(bool val)
				{
					CLog.L(val.ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\BigMenu\\CarouselMenu.cs", 60);
				}, false, "", "");
				new CToggle(mainGrp, "Test Toggle default state", delegate(bool val)
				{
					CLog.L(val.ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\BigMenu\\CarouselMenu.cs", 65);
				}, true, "", "");
				new CToggle(mainGrp, "Test Toggle long string test ------------", delegate(bool val)
				{
					CLog.L(val.ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\BigMenu\\CarouselMenu.cs", 69);
				}, true, "", "");
				CGrp secondGrp = new CGrp(CarouselMenu.secondMenu, "Section 2", true, true);
				new VRCSlider(secondGrp, "Default Slider", "Tool tip", delegate(float v)
				{
				}, 0f, 0f, 100f).Button(delegate
				{
				}, "Test Button", null).Toggle("Test Toggle", delegate(bool va)
				{
				}, false, null, null, null, null);
				new CToggle(secondGrp, "Test Toggle", delegate(bool val)
				{
					CLog.L(val.ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\BigMenu\\CarouselMenu.cs", 84);
				}, false, "", "");
				new CToggle(secondGrp, "Test Toggle default state", delegate(bool val)
				{
					CLog.L(val.ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\BigMenu\\CarouselMenu.cs", 85);
				}, true, "", "");
				new CToggle(secondGrp, "Test Toggle long string test ------------", delegate(bool val)
				{
					CLog.L(val.ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\BigMenu\\CarouselMenu.cs", 86);
				}, true, "", "");
			}
		}

		// Token: 0x040001DA RID: 474
		internal static MMCarousel carousel;

		// Token: 0x040001DB RID: 475
		internal static CMenu rootMenu;

		// Token: 0x040001DC RID: 476
		internal static CMenu secondMenu;
	}
}
