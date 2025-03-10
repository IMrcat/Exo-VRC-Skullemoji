using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Il2CppSystem.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.Localization;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using WorldAPI.ButtonAPI.Extras;
using WorldAPI.ButtonAPI.QM.Controls;

namespace WorldAPI.ButtonAPI.MM
{
	// Token: 0x02000022 RID: 34
	public class MMCarousel : WorldPage
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000070A8 File Offset: 0x000052A8
		// (set) Token: 0x0600013D RID: 317 RVA: 0x000070B0 File Offset: 0x000052B0
		public int Pageint { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000070B9 File Offset: 0x000052B9
		// (set) Token: 0x0600013F RID: 319 RVA: 0x000070C1 File Offset: 0x000052C1
		public Image ImageComp { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000070CA File Offset: 0x000052CA
		// (set) Token: 0x06000141 RID: 321 RVA: 0x000070D2 File Offset: 0x000052D2
		public Transform LogOutBtn { get; private set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000070DB File Offset: 0x000052DB
		// (set) Token: 0x06000143 RID: 323 RVA: 0x000070E3 File Offset: 0x000052E3
		public Transform ExitBtn { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000070EC File Offset: 0x000052EC
		// (set) Token: 0x06000145 RID: 325 RVA: 0x000070F4 File Offset: 0x000052F4
		public Transform BarContents { get; private set; }

		// Token: 0x06000146 RID: 326 RVA: 0x00007100 File Offset: 0x00005300
		private static void PrePrepMenu()
		{
			MMCarousel.Preped = true;
			APIBase.MMMCarouselPageTemplate.GetComponent<UIPage>().Method_Public_Void_Boolean_TransitionType_0(true, UIPage.TransitionType.None);
			APIBase.MMMCarouselPageTemplate.GetComponent<Canvas>().enabled = false;
			APIBase.MMMCarouselPageTemplate.GetComponent<CanvasGroup>().enabled = false;
			APIBase.MMMCarouselPageTemplate.GetComponent<GraphicRaycaster>().enabled = false;
			VRCUiManager field_Private_Static_VRCUiManager_ = VRCUiManager.field_Private_Static_VRCUiManager_0;
			if (field_Private_Static_VRCUiManager_ != null)
			{
				field_Private_Static_VRCUiManager_.transform.Find("Canvas_MainMenu(Clone)/Container/PageButtons/HorizontalLayoutGroup/Page_Settings").GetComponent<Button>().onClick.AddListener(delegate
				{
					APIBase.MMMCarouselPageTemplate.GetComponent<Canvas>().enabled = true;
					APIBase.MMMCarouselPageTemplate.GetComponent<CanvasGroup>().enabled = true;
					APIBase.MMMCarouselPageTemplate.GetComponent<GraphicRaycaster>().enabled = true;
				});
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000071A8 File Offset: 0x000053A8
		public MMCarousel(string menuName, string HeaderText, Sprite Icon = null)
		{
			bool flag = !APIBase.IsReady();
			if (flag)
			{
				throw new NullReferenceException("Object Search had FAILED!");
			}
			bool flag2 = !MMCarousel.Preped;
			if (flag2)
			{
				MMCarousel.PrePrepMenu();
			}
			int region = 0;
			try
			{
				base.transform = (base.gameObject = Object.Instantiate<GameObject>(APIBase.MMMCarouselPageTemplate, APIBase.MMMCarouselPageTemplate.transform.parent)).transform;
				base.gameObject.name = menuName;
				base.MenuName = menuName;
				base.Page = base.gameObject.GetComponent<UIPage>();
				string GuidName = menuName + Guid.NewGuid().ToString();
				base.Page.field_Public_String_0 = GuidName;
				(base.Page.field_Private_List_1_UIPage_0 = new List<UIPage>()).Add(base.Page);
				region++;
				QMUtils.GetMainMenuStateControllerInstance.field_Private_Dictionary_2_String_UIPage_0.Add(GuidName, base.Page);
				List<UIPage> list = Enumerable.ToList<UIPage>(QMUtils.GetMainMenuStateControllerInstance.field_Public_Il2CppReferenceArray_1_UIPage_0);
				list.Add(base.Page);
				QMUtils.GetMainMenuStateControllerInstance.field_Public_Il2CppReferenceArray_1_UIPage_0 = list.ToArray();
				this.Pageint = QMUtils.GetMainMenuStateControllerInstance.field_Public_Il2CppReferenceArray_1_UIPage_0.Count;
				region++;
				base.Page.GetComponent<Canvas>().enabled = true;
				base.Page.GetComponent<CanvasGroup>().enabled = true;
				base.Page.GetComponent<UIPage>().enabled = true;
				base.Page.GetComponent<GraphicRaycaster>().enabled = true;
				region++;
				Transform scrolNav = base.gameObject.transform.Find("Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Navigation");
				region++;
				scrolNav.GetComponent<VRCScrollRect>().field_Public_Boolean_0 = true;
				scrolNav.transform.Find("Viewport/VerticalLayoutGroup").GetComponent<Canvas>().enabled = true;
				scrolNav.transform.Find("Viewport/VerticalLayoutGroup").GetComponent<GraphicRaycaster>().enabled = true;
				region++;
				scrolNav.transform.parent.Find("ScrollRect_Content").GetComponent<VRCScrollRect>().field_Public_Boolean_0 = true;
				scrolNav.transform.parent.Find("ScrollRect_Content/Header_MM_H2/RightItemContainer/Field_MM_TextSearchField").gameObject.active = false;
				region++;
				Transform DynamicSidePanelHeader = base.gameObject.transform.Find("Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/DynamicSidePanel_Header");
				this.ImageComp = DynamicSidePanelHeader.Find("TitleContainer/Icon").GetComponent<Image>();
				this.ImageComp.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
				this.ImageComp.transform.localPosition = new Vector3(88.5f, -50f, 0f);
				bool flag3 = Icon != null;
				if (flag3)
				{
					this.ImageComp.sprite = Icon;
					this.ImageComp.overrideSprite = Icon;
				}
				else
				{
					this.ImageComp.gameObject.SetActive(false);
				}
				region++;
				TextMeshProUGUI textt = DynamicSidePanelHeader.Find("TitleContainer/Text_Name").GetComponent<TextMeshProUGUI>();
				textt.text = HeaderText;
				textt.overflowMode = TextOverflowModes.Overflow;
				textt.autoSizeTextContainer = true;
				textt.enableAutoSizing = true;
				textt.richText = true;
				region++;
				(base.MenuContents = scrolNav.parent.transform.Find("ScrollRect_Content/Viewport/VerticalLayoutGroup")).DestroyChildren(null);
				(this.BarContents = scrolNav.transform.Find("Viewport/VerticalLayoutGroup")).DestroyChildren(null);
				region++;
				this.LogOutBtn = DynamicSidePanelHeader.Find("Button_Logout");
				this.ExitBtn = DynamicSidePanelHeader.Find("Button_Exit");
				this.LogOutBtn.gameObject.active = false;
				this.ExitBtn.gameObject.active = false;
				base.gameObject.SetActive(false);
			}
			catch (Exception ex)
			{
				string text = "Exception Caught When Making Page At Region: ";
				string text2 = region.ToString();
				string text3 = "\n\n";
				Exception ex2 = ex;
				throw new Exception(text + text2 + text3 + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000075E4 File Offset: 0x000057E4
		public void SetExtraButtons(string text1, Action listener1, string toolTip1, string text2, Action listener2, string toolTip2, Sprite sprite1 = null, Sprite sprite2 = null)
		{
			if (sprite1 == null)
			{
				sprite1 = APIBase.DefaultButtonSprite;
			}
			if (sprite2 == null)
			{
				sprite2 = APIBase.DefaultButtonSprite;
			}
			(this.LogOutBtn.GetComponent<Button>().onClick = new Button.ButtonClickedEvent()).AddListener(listener1);
			this.LogOutBtn.GetComponent<ToolTip>()._localizableString = toolTip1.Localize(null, null, null);
			this.LogOutBtn.gameObject.active = true;
			MMCarousel.<SetExtraButtons>g__Apply|23_0(this.LogOutBtn, text1, sprite1);
			(this.ExitBtn.GetComponent<Button>().onClick = new Button.ButtonClickedEvent()).AddListener(listener2);
			this.ExitBtn.GetComponent<ToolTip>()._localizableString = toolTip2.Localize(null, null, null);
			this.ExitBtn.gameObject.active = true;
			MMCarousel.<SetExtraButtons>g__Apply|23_0(this.ExitBtn, text2, sprite2);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000076CC File Offset: 0x000058CC
		public void OpenMenu()
		{
			base.gameObject.SetActive(true);
			QMUtils.GetMainMenuStateControllerInstance.Method_Public_Void_String_UIContext_Boolean_TransitionType_0(base.Page.field_Public_String_0, null, true, UIPage.TransitionType.Right);
			Action onMenuOpen = base.OnMenuOpen;
			if (onMenuOpen != null)
			{
				onMenuOpen.Invoke();
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000770C File Offset: 0x0000590C
		[CompilerGenerated]
		internal static void <SetExtraButtons>g__Apply|23_0(Transform btn, string text, Sprite icom)
		{
			foreach (TextMeshProUGUI obj in btn.GetComponentsInChildren<TextMeshProUGUI>())
			{
				obj.text = text;
				obj.richText = true;
			}
			foreach (Image obj2 in btn.GetComponentsInChildren<Image>())
			{
				bool flag = obj2.name != "Icon";
				if (!flag)
				{
					obj2.sprite = icom;
				}
			}
		}

		// Token: 0x0400008B RID: 139
		private static bool Preped;
	}
}
