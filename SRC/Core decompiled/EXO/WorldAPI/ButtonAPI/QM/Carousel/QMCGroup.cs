using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Extras;
using WorldAPI.ButtonAPI.QM.Carousel.Items;
using WorldAPI.ButtonAPI.QM.Controls;

namespace WorldAPI.ButtonAPI.QM.Carousel
{
	// Token: 0x02000014 RID: 20
	public class QMCGroup : ButtonGroupControl
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004C48 File Offset: 0x00002E48
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00004C50 File Offset: 0x00002E50
		public Transform MenuContents { get; internal set; }

		// Token: 0x060000B9 RID: 185 RVA: 0x00004C5C File Offset: 0x00002E5C
		public QMCGroup(Transform parent, string text, TextAnchor ButtonAlignment = TextAnchor.UpperLeft)
		{
			bool flag = !APIBase.IsReady();
			if (flag)
			{
				throw new NullReferenceException("Object Search had FAILED!");
			}
			base.gameObject = Object.Instantiate<GameObject>(APIBase.QMCarouselPageTemplate, parent);
			base.gameObject.name = text;
			GameObject bg = APIBase.QMCarouselPageTemplate.transform.Find("QM_Settings_Panel/VerticalLayoutGroup/Background_Info").gameObject;
			Transform label = base.gameObject.transform.Find("QM_Foldout/Label");
			label.GetComponent<TextMeshProUGUI>().text = text;
			Transform settingsPanel = base.gameObject.transform.Find("QM_Settings_Panel/VerticalLayoutGroup");
			settingsPanel.DestroyChildren(null);
			GameObject newBG = Object.Instantiate<GameObject>(bg, settingsPanel);
			newBG.name = "Background_Info";
			this.Layout = base.gameObject.GetComponent<VerticalLayoutGroup>();
			this.Layout.childAlignment = ButtonAlignment;
			base.parentMenuMask = parent.parent.GetComponent<RectMask2D>();
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004D48 File Offset: 0x00002F48
		public void ChangeChildAlignment(TextAnchor ButtonAlignment = TextAnchor.UpperLeft)
		{
			this.Layout.childAlignment = ButtonAlignment;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004D57 File Offset: 0x00002F57
		public QMCGroup(WorldPage page, string text, TextAnchor ButtonAlignment = TextAnchor.UpperLeft)
			: this(page.MenuContents, text, ButtonAlignment)
		{
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004D69 File Offset: 0x00002F69
		public void AddToggle(string text, Action<bool> stateChange, bool defaultState = false, string toolTip = "", bool separator = false)
		{
			new QMCToggle(this, text, stateChange, defaultState, toolTip, separator);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004D7C File Offset: 0x00002F7C
		public void AddSlider(string text, string tooltip, Action<float, QMCSlider> listener, float defaultValue = 0f, float minValue = 0f, float maxValue = 100f, bool isDecimal = false, string ending = "%", bool separator = false)
		{
			new QMCSlider(this, text, tooltip, listener, defaultValue, minValue, maxValue, isDecimal, ending, separator);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004DA0 File Offset: 0x00002FA0
		public void AddSelector(string text, string containerTooltip)
		{
			new QMCSelector(this, text, containerTooltip, false);
		}

		// Token: 0x04000055 RID: 85
		private readonly VerticalLayoutGroup Layout;
	}
}
