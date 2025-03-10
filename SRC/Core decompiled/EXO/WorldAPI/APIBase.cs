using System;
using Il2CppInterop.Runtime;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using UnityEngine;
using VRC.UI.Core.Styles;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.MM.Carousel.Items;
using WorldAPI.ButtonAPI.QM.Carousel.Items;

namespace WorldAPI
{
	// Token: 0x02000009 RID: 9
	public class APIBase
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021F9 File Offset: 0x000003F9
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002200 File Offset: 0x00000400
		public static Action<Exception, string> ErrorCallBack { get; set; } = delegate(Exception er, string str)
		{
			Logs.Error("The ButtonAPI had an Error At " + str, er);
		};

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002208 File Offset: 0x00000408
		internal static Transform UserInterface
		{
			get
			{
				VRCUiManager field_Private_Static_VRCUiManager_ = VRCUiManager.field_Private_Static_VRCUiManager_0;
				return (field_Private_Static_VRCUiManager_ != null) ? field_Private_Static_VRCUiManager_.transform : null;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000221C File Offset: 0x0000041C
		public static bool IsReady()
		{
			bool hasChecked = APIBase.HasChecked;
			bool flag;
			if (hasChecked)
			{
				flag = true;
			}
			else
			{
				Transform transform = APIBase.UserInterface.Find("Canvas_QuickMenu(Clone)");
				bool flag2 = (APIBase.QuickMenu = ((transform != null) ? transform.gameObject : null)) == null;
				if (flag2)
				{
					throw new NullReferenceException("QuickMenu Is Null!");
				}
				GameObject quickMenu = APIBase.QuickMenu;
				bool flag3 = (APIBase.Button = ((quickMenu != null) ? quickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Here/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_WorldActions/Button_NewInstance") : null)) == null;
				if (flag3)
				{
					throw new NullReferenceException("Button Is Null!");
				}
				GameObject quickMenu2 = APIBase.QuickMenu;
				bool flag4 = (APIBase.MenuPage = ((quickMenu2 != null) ? quickMenu2.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Camera") : null)) == null;
				if (flag4)
				{
					throw new NullReferenceException("MenuTab Is Null!");
				}
				GameObject quickMenu3 = APIBase.QuickMenu;
				bool flag5 = (APIBase.Tab = ((quickMenu3 != null) ? quickMenu3.transform.Find("CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_DevTools") : null)) == null;
				if (flag5)
				{
					throw new NullReferenceException("Tab Is Null!");
				}
				GameObject quickMenu4 = APIBase.QuickMenu;
				GameObject gameObject;
				if (quickMenu4 == null)
				{
					gameObject = null;
				}
				else
				{
					Transform transform2 = quickMenu4.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Camera/Scrollrect/Viewport/VerticalLayoutGroup/Buttons");
					gameObject = ((transform2 != null) ? transform2.gameObject : null);
				}
				bool flag6 = (APIBase.ButtonGrp = gameObject) == null;
				if (flag6)
				{
					throw new NullReferenceException("ButtonGrp Is Null!");
				}
				GameObject quickMenu5 = APIBase.QuickMenu;
				GameObject gameObject2;
				if (quickMenu5 == null)
				{
					gameObject2 = null;
				}
				else
				{
					Transform transform3 = quickMenu5.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Camera/Scrollrect/Viewport/VerticalLayoutGroup/Header_H3");
					gameObject2 = ((transform3 != null) ? transform3.gameObject : null);
				}
				bool flag7 = (APIBase.ButtonGrpText = gameObject2) == null;
				if (flag7)
				{
					throw new NullReferenceException("ButtonGrpText Is Null!");
				}
				GameObject quickMenu6 = APIBase.QuickMenu;
				GameObject gameObject3;
				if (quickMenu6 == null)
				{
					gameObject3 = null;
				}
				else
				{
					Transform transform4 = quickMenu6.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_QM_GeneralSettings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/UIElements");
					gameObject3 = ((transform4 != null) ? transform4.gameObject : null);
				}
				bool flag8 = (APIBase.ColpButtonGrp = gameObject3) == null;
				if (flag8)
				{
					throw new NullReferenceException("ColpButtonGrp Is Null!");
				}
				GameObject quickMenu7 = APIBase.QuickMenu;
				bool flag9 = (APIBase.Slider = ((quickMenu7 != null) ? quickMenu7.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_QM_GeneralSettings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/DisplayAndVisualAdjustments/QM_Settings_Panel/VerticalLayoutGroup/ScreenBrightness") : null)) == null;
				if (flag9)
				{
					throw new NullReferenceException("Slider Is Null!");
				}
				GameObject quickMenu8 = APIBase.QuickMenu;
				GameObject gameObject4;
				if (quickMenu8 == null)
				{
					gameObject4 = null;
				}
				else
				{
					Transform transform5 = quickMenu8.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_QM_GeneralSettings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/UIElements");
					gameObject4 = ((transform5 != null) ? transform5.gameObject : null);
				}
				bool flag10 = (APIBase.QMCarouselPageTemplate = gameObject4) == null;
				if (flag10)
				{
					throw new NullReferenceException("QuickMenu Carousel Template Is Null!");
				}
				GameObject quickMenu9 = APIBase.QuickMenu;
				GameObject gameObject5;
				if (quickMenu9 == null)
				{
					gameObject5 = null;
				}
				else
				{
					Transform transform6 = quickMenu9.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_QM_GeneralSettings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/UIElements/QM_Settings_Panel/VerticalLayoutGroup/Separator");
					gameObject5 = ((transform6 != null) ? transform6.gameObject : null);
				}
				bool flag11 = (APIBase.QMCarouselSeparator = gameObject5) == null;
				if (flag11)
				{
					throw new NullReferenceException("QuickMenu Carousel Separator Is Null!");
				}
				GameObject quickMenu10 = APIBase.QuickMenu;
				GameObject gameObject6;
				if (quickMenu10 == null)
				{
					gameObject6 = null;
				}
				else
				{
					Transform transform7 = quickMenu10.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_QM_GeneralSettings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/UIElements/QM_Settings_Panel/VerticalLayoutGroup/ShowGroupBanner");
					gameObject6 = ((transform7 != null) ? transform7.gameObject : null);
				}
				bool flag12 = (APIBase.QMCarouselToggleTemplate = gameObject6) == null;
				if (flag12)
				{
					throw new NullReferenceException("QuickMenu Carousel Toggle Template Is Null!");
				}
				GameObject quickMenu11 = APIBase.QuickMenu;
				GameObject gameObject7;
				if (quickMenu11 == null)
				{
					gameObject7 = null;
				}
				else
				{
					Transform transform8 = quickMenu11.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_QM_GeneralSettings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/UIElements/QM_Settings_Panel/VerticalLayoutGroup/NameplateVisibility");
					gameObject7 = ((transform8 != null) ? transform8.gameObject : null);
				}
				bool flag13 = (APIBase.QMCarouselSelectorTemplate = gameObject7) == null;
				if (flag13)
				{
					throw new NullReferenceException("QuickMenu Carousel Selector Template Is Null!");
				}
				GameObject quickMenu12 = APIBase.QuickMenu;
				GameObject gameObject8;
				if (quickMenu12 == null)
				{
					gameObject8 = null;
				}
				else
				{
					Transform transform9 = quickMenu12.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_QM_GeneralSettings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Chatbox/QM_Settings_Panel/VerticalLayoutGroup/ChatboxFunctionTitle");
					gameObject8 = ((transform9 != null) ? transform9.gameObject : null);
				}
				bool flag14 = (APIBase.QMCarouselTitleTemplate = gameObject8) == null;
				if (flag14)
				{
					throw new NullReferenceException("QuickMenu Carousel Title Template Is Null!");
				}
				GameObject quickMenu13 = APIBase.QuickMenu;
				GameObject gameObject9;
				if (quickMenu13 == null)
				{
					gameObject9 = null;
				}
				else
				{
					Transform transform10 = quickMenu13.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_QM_GeneralSettings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Chatbox/QM_Settings_Panel/VerticalLayoutGroup/ChatboxFunctionButtons");
					gameObject9 = ((transform10 != null) ? transform10.gameObject : null);
				}
				bool flag15 = (APIBase.QMCarouselFuncButtonTemplate = gameObject9) == null;
				if (flag15)
				{
					throw new NullReferenceException("QuickMenu Carousel Function Button Template Is Null!");
				}
				GameObject quickMenu14 = APIBase.QuickMenu;
				GameObject gameObject10;
				if (quickMenu14 == null)
				{
					gameObject10 = null;
				}
				else
				{
					Transform transform11 = quickMenu14.transform.Find("CanvasGroup/Container/Window/Wing_Left/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup");
					gameObject10 = ((transform11 != null) ? transform11.gameObject : null);
				}
				bool flag16 = (APIBase.WingParentL = gameObject10) == null;
				if (flag16)
				{
					throw new NullReferenceException("WingParentL Is Null!");
				}
				GameObject quickMenu15 = APIBase.QuickMenu;
				GameObject gameObject11;
				if (quickMenu15 == null)
				{
					gameObject11 = null;
				}
				else
				{
					Transform transform12 = quickMenu15.transform.Find("CanvasGroup/Container/Window/Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup");
					gameObject11 = ((transform12 != null) ? transform12.gameObject : null);
				}
				bool flag17 = (APIBase.WingParentR = gameObject11) == null;
				if (flag17)
				{
					throw new NullReferenceException("WingParentR Is Null!");
				}
				Transform transform13 = APIBase.UserInterface.Find("Canvas_MainMenu(Clone)");
				bool flag18 = (APIBase.MMM = ((transform13 != null) ? transform13.gameObject : null)) == null;
				if (flag18)
				{
					throw new NullReferenceException("MainMenu Is Null!");
				}
				GameObject mmm = APIBase.MMM;
				GameObject gameObject12;
				if (mmm == null)
				{
					gameObject12 = null;
				}
				else
				{
					Transform transform14 = mmm.transform.Find("Container/MMParent/Menu_MM_Profile");
					gameObject12 = ((transform14 != null) ? transform14.gameObject : null);
				}
				bool flag19 = (APIBase.MMMpageTemplate = gameObject12) == null;
				if (flag19)
				{
					throw new NullReferenceException("Init Menu Template Is Null!");
				}
				GameObject mmm2 = APIBase.MMM;
				GameObject gameObject13;
				if (mmm2 == null)
				{
					gameObject13 = null;
				}
				else
				{
					Transform transform15 = mmm2.transform.Find("Container/MMParent/Menu_Settings");
					gameObject13 = ((transform15 != null) ? transform15.gameObject : null);
				}
				bool flag20 = (APIBase.MMMCarouselPageTemplate = gameObject13) == null;
				if (flag20)
				{
					throw new NullReferenceException("Menu_Settings Is Null!");
				}
				GameObject mmm3 = APIBase.MMM;
				GameObject gameObject14;
				if (mmm3 == null)
				{
					gameObject14 = null;
				}
				else
				{
					Transform transform16 = mmm3.transform.Find("Container/PageButtons/HorizontalLayoutGroup/Page_Profile");
					gameObject14 = ((transform16 != null) ? transform16.gameObject : null);
				}
				bool flag21 = (APIBase.MMMTabTemplate = gameObject14) == null;
				if (flag21)
				{
					throw new NullReferenceException("Init Menu Tab Is Null!");
				}
				GameObject mmm4 = APIBase.MMM;
				GameObject gameObject15;
				if (mmm4 == null)
				{
					gameObject15 = null;
				}
				else
				{
					Transform transform17 = mmm4.transform.Find("Container/MMParent/Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Navigation/Viewport/VerticalLayoutGroup/Cell_MM_Audio & Voice");
					gameObject15 = ((transform17 != null) ? transform17.gameObject : null);
				}
				bool flag22 = (APIBase.MMMCarouselButtonTemplate = gameObject15) == null;
				if (flag22)
				{
					throw new NullReferenceException("MMMCarouselButtonTemplate Is Null!");
				}
				GameObject mmm5 = APIBase.MMM;
				GameObject gameObject16;
				if (mmm5 == null)
				{
					gameObject16 = null;
				}
				else
				{
					Transform transform18 = mmm5.transform.Find("Container/MMParent/Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/Debug/ManageCachedData");
					gameObject16 = ((transform18 != null) ? transform18.gameObject : null);
				}
				bool flag23 = (APIBase.MMBtnGRP = gameObject16) == null;
				if (flag23)
				{
					throw new NullReferenceException("MMBtnGRP Is Null!");
				}
				GameObject mmm6 = APIBase.MMM;
				GameObject gameObject17;
				if (mmm6 == null)
				{
					gameObject17 = null;
				}
				else
				{
					Transform transform19 = mmm6.transform.Find("Container/MMParent/Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/Mirrors/PersonalMirror/Settings_Panel_1/VerticalLayoutGroup/PersonalMirror");
					gameObject17 = ((transform19 != null) ? transform19.gameObject : null);
				}
				bool flag24 = (APIBase.MMCTgl = gameObject17) == null;
				if (flag24)
				{
					throw new NullReferenceException("MMCTgl Is Null!");
				}
				APIBase.GetToglSprites();
				flag = (APIBase.HasChecked = true);
			}
			return flag;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000281C File Offset: 0x00000A1C
		private static void GetToglSprites()
		{
			StyleEngine styleEngine = APIBase.QuickMenu.GetComponent<StyleEngine>();
			List<StyleResource.Resource> resources = styleEngine.field_Public_StyleResource_0.resources;
			for (int i = 0; i < resources.Count; i++)
			{
				StyleResource.Resource resource = resources[i];
				Type type;
				if (resource == null)
				{
					type = null;
				}
				else
				{
					global::UnityEngine.Object obj = resource.obj;
					type = ((obj != null) ? obj.GetIl2CppType() : null);
				}
				bool flag = type == null;
				if (!flag)
				{
					bool flag2 = resources[i].obj.GetIl2CppType() != Il2CppType.Of<Sprite>();
					if (!flag2)
					{
						bool flag3 = resources[i].obj.name.Equals("Decline");
						if (flag3)
						{
							APIBase.OffSprite = resources[i].obj.Cast<Sprite>();
						}
						bool flag4 = resources[i].obj.name.Equals("Checkmark");
						if (flag4)
						{
							APIBase.OnSprite = resources[i].obj.Cast<Sprite>();
						}
						bool flag5 = APIBase.OffSprite != null && APIBase.OnSprite != null;
						if (flag5)
						{
							break;
						}
					}
				}
			}
			bool flag6 = APIBase.OffSprite == null;
			if (flag6)
			{
				throw new NullReferenceException("OffSprite Is Null!");
			}
			bool flag7 = APIBase.OnSprite == null;
			if (flag7)
			{
				throw new NullReferenceException("OnSprite Is Null!");
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002980 File Offset: 0x00000B80
		internal static void SafelyInvolk(Action action, string name)
		{
			try
			{
				action.Invoke();
			}
			catch (Exception e)
			{
				APIBase.ErrorCallBack.Invoke(e, name);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000029BC File Offset: 0x00000BBC
		internal static void SafelyInvolk(bool state, Action<bool> action, string name)
		{
			try
			{
				action.Invoke(state);
			}
			catch (Exception e)
			{
				APIBase.ErrorCallBack.Invoke(e, name);
			}
		}

		// Token: 0x04000006 RID: 6
		public static Sprite DefaultButtonSprite;

		// Token: 0x04000007 RID: 7
		public static Sprite OffSprite;

		// Token: 0x04000008 RID: 8
		public static Sprite OnSprite;

		// Token: 0x04000009 RID: 9
		public static GameObject QuickMenu;

		// Token: 0x0400000A RID: 10
		public static GameObject ColpButtonGrp;

		// Token: 0x0400000B RID: 11
		public static GameObject ButtonGrp;

		// Token: 0x0400000C RID: 12
		public static GameObject ButtonGrpText;

		// Token: 0x0400000D RID: 13
		public static Transform Button;

		// Token: 0x0400000E RID: 14
		public static Transform Tab;

		// Token: 0x0400000F RID: 15
		public static Transform MenuPage;

		// Token: 0x04000010 RID: 16
		public static Transform Slider;

		// Token: 0x04000011 RID: 17
		public static GameObject QMCarouselPageTemplate;

		// Token: 0x04000012 RID: 18
		public static GameObject QMCarouselSeparator;

		// Token: 0x04000013 RID: 19
		public static GameObject QMCarouselToggleTemplate;

		// Token: 0x04000014 RID: 20
		public static GameObject QMCarouselSliderTemplate;

		// Token: 0x04000015 RID: 21
		public static GameObject QMCarouselSelectorTemplate;

		// Token: 0x04000016 RID: 22
		public static GameObject QMCarouselTitleTemplate;

		// Token: 0x04000017 RID: 23
		public static GameObject QMCarouselFuncButtonTemplate;

		// Token: 0x04000018 RID: 24
		public static GameObject WingParentL;

		// Token: 0x04000019 RID: 25
		public static GameObject WingParentR;

		// Token: 0x0400001A RID: 26
		public static GameObject MMM;

		// Token: 0x0400001B RID: 27
		public static GameObject MMMpageTemplate;

		// Token: 0x0400001C RID: 28
		public static GameObject MMMCarouselPageTemplate;

		// Token: 0x0400001D RID: 29
		public static GameObject MMMCarouselButtonTemplate;

		// Token: 0x0400001E RID: 30
		public static GameObject MMMTabTemplate;

		// Token: 0x0400001F RID: 31
		public static GameObject MMCTgl;

		// Token: 0x04000020 RID: 32
		public static GameObject MMBtnGRP;

		// Token: 0x04000021 RID: 33
		private static bool HasChecked;

		// Token: 0x020000BB RID: 187
		public class Events
		{
			// Token: 0x04000320 RID: 800
			public static Action<VRCToggle, bool> onVRCToggleValChange = delegate(VRCToggle er, bool str)
			{
			};

			// Token: 0x04000321 RID: 801
			public static Action<CToggle, bool> onCToggleValChange = delegate(CToggle er, bool str)
			{
			};

			// Token: 0x04000322 RID: 802
			public static Action<QMCToggle, bool> onQMCToggleValChange = delegate(QMCToggle er, bool str)
			{
			};

			// Token: 0x04000323 RID: 803
			public static Action<QMCSlider, bool> onQMCSliderToggleValChange = delegate(QMCSlider er, bool str)
			{
			};
		}
	}
}
