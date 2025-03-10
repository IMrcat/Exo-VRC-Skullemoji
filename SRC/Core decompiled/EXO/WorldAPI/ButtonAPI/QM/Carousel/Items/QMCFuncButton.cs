using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.Localization;
using VRC.UI.Elements.Controls;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Extras;
using WorldAPI.ButtonAPI.Groups;

namespace WorldAPI.ButtonAPI.QM.Carousel.Items
{
	// Token: 0x02000015 RID: 21
	public class QMCFuncButton : ExtentedControl
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004DAD File Offset: 0x00002FAD
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00004DB4 File Offset: 0x00002FB4
		public static Button AdditionalButton { get; private set; }

		// Token: 0x060000C1 RID: 193 RVA: 0x00004DBC File Offset: 0x00002FBC
		public QMCFuncButton(Transform parent, string text, string tooltip, Action listener)
		{
			bool flag = !APIBase.IsReady();
			if (flag)
			{
				throw new NullReferenceException("Object Search had FAILED!");
			}
			base.transform = Object.Instantiate<GameObject>(APIBase.QMCarouselFuncButtonTemplate, parent).transform;
			base.gameObject = base.transform.gameObject;
			base.gameObject.name = text + "_ControlContainer";
			base.transform.Find("LeftItemContainer").gameObject.DestroyChildren(null);
			base.transform.Find("TitleMainContainer").gameObject.DestroyChildren(null);
			Transform button = Object.Instantiate<Transform>(APIBase.QMCarouselFuncButtonTemplate.transform.Find("LeftItemContainer/Button (1)"), base.transform.Find("LeftItemContainer"));
			button.name = text;
			base.TMProCompnt = button.Find("Text_MM_H3").GetComponent<TextMeshProUGUI>();
			base.TMProCompnt.text = text;
			base.TMProCompnt.richText = true;
			button.GetComponent<ToolTip>()._localizableString = tooltip.Localize(null, null, null);
			base.ButtonCompnt = button.GetComponent<Button>();
			base.ButtonCompnt.onClick = new Button.ButtonClickedEvent();
			base.ButtonCompnt.onClick.AddListener(listener);
			button.gameObject.SetActive(true);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004F1C File Offset: 0x0000311C
		public void AddButton(string text, string tooltip, Action listener)
		{
			Transform newButton = Object.Instantiate<Transform>(APIBase.QMCarouselFuncButtonTemplate.transform.Find("LeftItemContainer/Button (1)"), base.transform.Find("LeftItemContainer"));
			newButton.name = text;
			base.TMProCompnt = newButton.Find("Text_MM_H3").GetComponent<TextMeshProUGUI>();
			base.TMProCompnt.text = text;
			base.TMProCompnt.richText = true;
			newButton.GetComponent<ToolTip>()._localizableString = tooltip.Localize(null, null, null);
			QMCFuncButton.AdditionalButton = newButton.GetComponent<Button>();
			QMCFuncButton.AdditionalButton.onClick = new Button.ButtonClickedEvent();
			QMCFuncButton.AdditionalButton.onClick.AddListener(listener);
			newButton.gameObject.SetActive(true);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004FE1 File Offset: 0x000031E1
		public QMCFuncButton(QMCGroup group, string text, string tooltip, Action listener)
			: this(group.GetTransform().Find("QM_Settings_Panel/VerticalLayoutGroup").transform, text, tooltip, listener)
		{
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005004 File Offset: 0x00003204
		public QMCFuncButton(CollapsibleButtonGroup buttonGroup, string text, string tooltip, Action listener)
			: this(buttonGroup.QMCParent, text, tooltip, listener)
		{
		}
	}
}
