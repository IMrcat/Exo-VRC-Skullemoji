using System;
using System.Collections.Generic;
using System.Linq;
using Il2CppSystem.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using WorldAPI.ButtonAPI.Extras;
using WorldAPI.ButtonAPI.QM.Controls;

namespace WorldAPI.ButtonAPI.MM
{
	// Token: 0x02000023 RID: 35
	public class MMPage : WorldPage
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000077C0 File Offset: 0x000059C0
		// (set) Token: 0x0600014C RID: 332 RVA: 0x000077C8 File Offset: 0x000059C8
		public int Pageint { get; private set; }

		// Token: 0x0600014D RID: 333 RVA: 0x000077D4 File Offset: 0x000059D4
		private static void PrePrepMenu()
		{
			MMPage.Preped = true;
			APIBase.MMMpageTemplate.GetComponent<UIPage>().Method_Public_Void_Boolean_TransitionType_0(true, UIPage.TransitionType.None);
			APIBase.MMMpageTemplate.GetComponent<Canvas>().enabled = false;
			APIBase.MMMpageTemplate.GetComponent<CanvasGroup>().enabled = false;
			APIBase.MMMpageTemplate.GetComponent<GraphicRaycaster>().enabled = false;
			VRCUiManager field_Private_Static_VRCUiManager_ = VRCUiManager.field_Private_Static_VRCUiManager_0;
			if (field_Private_Static_VRCUiManager_ != null)
			{
				field_Private_Static_VRCUiManager_.transform.Find("Canvas_MainMenu(Clone)/Container/PageButtons/HorizontalLayoutGroup/Page_Profile").GetComponent<Button>().onClick.AddListener(delegate
				{
					APIBase.MMMpageTemplate.GetComponent<Canvas>().enabled = true;
					APIBase.MMMpageTemplate.GetComponent<CanvasGroup>().enabled = true;
					APIBase.MMMpageTemplate.GetComponent<GraphicRaycaster>().enabled = true;
				});
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000787C File Offset: 0x00005A7C
		public MMPage(string menuName, bool root = false)
		{
			bool flag = !APIBase.IsReady();
			if (flag)
			{
				throw new NullReferenceException("Object Search had FAILED!");
			}
			bool flag2 = !MMPage.Preped;
			if (flag2)
			{
				MMPage.PrePrepMenu();
			}
			int region = 0;
			try
			{
				base.gameObject = Object.Instantiate<GameObject>(APIBase.MMMpageTemplate, APIBase.MMMpageTemplate.transform.parent);
				base.transform = base.gameObject.transform;
				base.gameObject.name = menuName;
				base.gameObject.transform.Find("Loading_Display").gameObject.active = false;
				base.MenuName = menuName;
				TextMeshProUGUI ttext = base.gameObject.transform.Find("Header_MM_UserName/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>();
				ttext.text = base.MenuName;
				ttext.richText = true;
				region++;
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
				bool flag3 = !root;
				if (flag3)
				{
					base.transform.Find("Header_MM_UserName/LeftItemContainer/Button_Back").gameObject.active = true;
				}
				region++;
				base.Page.GetComponent<Canvas>().enabled = true;
				base.Page.GetComponent<CanvasGroup>().enabled = true;
				base.Page.GetComponent<UIPage>().enabled = true;
				base.Page.GetComponent<GraphicRaycaster>().enabled = true;
				region++;
				base.Page.transform.Find("ScrollRect").GetComponent<VRCScrollRect>().field_Public_Boolean_0 = true;
				(base.MenuContents = base.gameObject.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup")).DestroyChildren(null);
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

		// Token: 0x0600014F RID: 335 RVA: 0x00007B38 File Offset: 0x00005D38
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

		// Token: 0x0400008D RID: 141
		private static bool Preped;
	}
}
