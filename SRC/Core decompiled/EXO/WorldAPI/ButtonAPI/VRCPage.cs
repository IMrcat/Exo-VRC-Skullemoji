using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CoreRuntime.Manager;
using Il2CppSystem.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.Localization;
using VRC.UI.Core.Styles;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using WorldAPI.ButtonAPI.Extras;
using WorldAPI.ButtonAPI.QM.Controls;

namespace WorldAPI.ButtonAPI
{
	// Token: 0x0200000C RID: 12
	public class VRCPage : WorldPage
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002F51 File Offset: 0x00001151
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002F59 File Offset: 0x00001159
		public bool IsRoot { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002F62 File Offset: 0x00001162
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002F69 File Offset: 0x00001169
		public static VRCPage lastOpenedPage { get; private set; }

		// Token: 0x06000031 RID: 49 RVA: 0x00002F74 File Offset: 0x00001174
		public VRCPage(string pageTitle, bool root = false, bool backButton = true, bool expandButton = false, Action expandButtonAction = null, string expandButtonTooltip = "", Sprite expandButtonSprite = null, bool preserveColor = false)
		{
			bool flag = !APIBase.IsReady();
			if (flag)
			{
				throw new Exception();
			}
			bool flag2 = APIBase.MenuPage == null;
			if (flag2)
			{
				Logs.Error("Fatal Error: ButtonAPI.menuPageBase Is Null!", null);
			}
			else
			{
				int region = 0;
				base.MenuName = "WorldMenu_" + pageTitle + Guid.NewGuid().ToString();
				this.IsRoot = root;
				try
				{
					Transform gameObject = Object.Instantiate<Transform>(APIBase.MenuPage, APIBase.MenuPage.transform.parent);
					gameObject.name = base.MenuName;
					gameObject.transform.SetSiblingIndex(9);
					gameObject.gameObject.active = false;
					region++;
					Object.DestroyImmediate(gameObject.GetOrAddComponent<CameraMenu>());
					base.Page = gameObject.gameObject.AddComponent<UIPage>();
					region++;
					base.Page.field_Public_String_0 = base.MenuName;
					base.Page.field_Private_Boolean_1 = true;
					base.Page.field_Protected_MenuStateController_0 = QMUtils.GetMenuStateControllerInstance;
					base.Page.field_Private_List_1_UIPage_0 = new List<UIPage>();
					base.Page.field_Private_List_1_UIPage_0.Add(base.Page);
					region++;
					QMUtils.GetMenuStateControllerInstance.field_Private_Dictionary_2_String_UIPage_0.Add(base.MenuName, base.Page);
					if (root)
					{
						List<UIPage> list = Enumerable.ToList<UIPage>(QMUtils.GetMenuStateControllerInstance.field_Public_Il2CppReferenceArray_1_UIPage_0);
						list.Add(base.Page);
						QMUtils.GetMenuStateControllerInstance.field_Public_Il2CppReferenceArray_1_UIPage_0 = list.ToArray();
					}
					region++;
					base.MenuContents = gameObject.transform.Find("Scrollrect/Viewport/VerticalLayoutGroup");
					base.MenuContents.GetComponent<HorizontalOrVerticalLayoutGroup>().childControlHeight = true;
					base.MenuContents.DestroyChildren(null);
					region++;
					this.pageTitleText = gameObject.Find("Header_Camera/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>();
					this.pageTitleText.fontSize = 54.7f;
					this.pageTitleText.text = pageTitle;
					this.pageTitleText.richText = true;
					region++;
					GameObject backButtonGameObject = gameObject.transform.GetChild(0).Find("LeftItemContainer/Button_Back").gameObject;
					backButtonGameObject.SetActive(backButton);
					(backButtonGameObject.GetComponentInChildren<Button>().onClick = new Button.ButtonClickedEvent()).AddListener(delegate
					{
						bool isRoot = this.IsRoot;
						if (isRoot)
						{
							QMUtils.GetMenuStateControllerInstance.Method_Public_Void_String_UIContext_Boolean_TransitionType_0("QuickMenuDashboard", null, false, UIPage.TransitionType.Right);
						}
						else
						{
							base.Page.Method_Protected_Virtual_New_Void_0();
						}
						Action backButtonPress = this.BackButtonPress;
						if (backButtonPress != null)
						{
							backButtonPress.Invoke();
						}
					});
					region++;
					this.extButtonGameObject = gameObject.transform.GetChild(0).Find("RightItemContainer/Button_QM_Expand").gameObject;
					this.extButtonGameObject.SetActive(expandButton);
					this.extButtonGameObject.GetComponentInChildren<Button>().onClick = new Button.ButtonClickedEvent();
					bool flag3 = expandButtonAction != null;
					if (flag3)
					{
						this.extButtonGameObject.GetComponentInChildren<Button>().onClick.AddListener(expandButtonAction);
					}
					bool flag4 = expandButtonSprite != null;
					if (flag4)
					{
						this.extButtonGameObject.GetComponentInChildren<Image>().sprite = expandButtonSprite;
						this.extButtonGameObject.GetComponentInChildren<Image>().overrideSprite = expandButtonSprite;
						if (preserveColor)
						{
							this.extButtonGameObject.GetComponentInChildren<Image>().color = Color.white;
							this.extButtonGameObject.GetComponentInChildren<StyleElement>(true).enabled = false;
						}
					}
					region++;
					this.menuMask = base.MenuContents.parent.gameObject.GetOrAddComponent<VRCRectMask2D>();
					this.menuMask.enabled = true;
					gameObject.transform.Find("Scrollrect").GetOrAddComponent<VRCScrollRect>().enabled = true;
					gameObject.transform.Find("Scrollrect").GetOrAddComponent<ScrollRect>().verticalScrollbar = gameObject.transform.Find("Scrollrect/Scrollbar").GetOrAddComponent<Scrollbar>();
					gameObject.transform.Find("Scrollrect").GetOrAddComponent<ScrollRect>().verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHide;
					gameObject.DestroyChildren((Transform where) => where.name != "Scrollrect" && where.name != "Header_Camera");
					region++;
					gameObject.transform.Find("Scrollrect/Viewport").GetComponent<VRCRectMask2D>().prop_Boolean_0 = true;
					gameObject.transform.Find("Scrollrect").GetComponent<VRCScrollRect>().field_Public_Boolean_0 = true;
					region++;
					base.Page.GetComponent<Canvas>().enabled = true;
					base.Page.GetComponent<CanvasGroup>().enabled = true;
					base.Page.GetComponent<UIPage>().enabled = true;
					base.Page.GetComponent<GraphicRaycaster>().enabled = true;
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
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000345C File Offset: 0x0000165C
		public void AddExtButton(Action onClick, string tooltip, Sprite icon)
		{
			GameObject obj = Object.Instantiate<GameObject>(this.extButtonGameObject, this.extButtonGameObject.transform.parent);
			obj.transform.SetSiblingIndex(0);
			obj.SetActive(true);
			obj.GetComponentInChildren<Button>().onClick = new Button.ButtonClickedEvent();
			obj.GetComponentInChildren<Button>().onClick.AddListener(onClick);
			obj.GetComponent<ToolTip>()._localizableString = tooltip.Localize(null, null, null);
			obj.GetComponentInChildren<Image>().sprite = icon;
			obj.GetComponentInChildren<Image>().overrideSprite = icon;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000034F4 File Offset: 0x000016F4
		public void OpenMenu()
		{
			base.Page.gameObject.active = true;
			QMUtils.GetMenuStateControllerInstance.Method_Public_Void_String_UIContext_Boolean_TransitionType_0(base.Page.field_Public_String_0, null, false, UIPage.TransitionType.Right);
			Action onMenuOpen = base.OnMenuOpen;
			if (onMenuOpen != null)
			{
				onMenuOpen.Invoke();
			}
			VRCPage.lastOpenedPage = this;
			CoroutineManager.RunCoroutine(this.IWillKrillSomeone());
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003556 File Offset: 0x00001756
		private IEnumerator IWillKrillSomeone()
		{
			yield return new WaitForSeconds(0.3f);
			base.Page.transform.Find("Header_Camera").gameObject.active = false;
			yield return new WaitForSeconds(0.1f);
			base.Page.transform.Find("Header_Camera").gameObject.active = true;
			yield break;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003565 File Offset: 0x00001765
		public void SetTitle(string text)
		{
			this.pageTitleText.text = text;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003574 File Offset: 0x00001774
		public void CloseMenu()
		{
			base.Page.Method_Protected_Virtual_New_Void_0();
		}

		// Token: 0x0400002B RID: 43
		public Action BackButtonPress;

		// Token: 0x0400002C RID: 44
		public TextMeshProUGUI pageTitleText;

		// Token: 0x0400002D RID: 45
		public RectMask2D menuMask;

		// Token: 0x0400002E RID: 46
		private GameObject extButtonGameObject;
	}
}
