using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.Localization;
using VRC.UI.Elements.Controls;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Groups;

namespace WorldAPI.ButtonAPI.QM.Carousel.Items
{
	// Token: 0x02000016 RID: 22
	public class QMCSelector : ExtentedControl
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00005018 File Offset: 0x00003218
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00005020 File Offset: 0x00003220
		public ToolTip ContainerTooltip { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00005029 File Offset: 0x00003229
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00005031 File Offset: 0x00003231
		public ToolTip SelectionBoxTextTooltip { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000503A File Offset: 0x0000323A
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00005042 File Offset: 0x00003242
		public TextMeshProUGUI TMProSelectionBoxText { get; set; }

		// Token: 0x060000CB RID: 203 RVA: 0x0000504C File Offset: 0x0000324C
		public QMCSelector(Transform parent, string text, string containerTooltip, bool separator = false)
		{
			bool flag = !APIBase.IsReady();
			if (flag)
			{
				throw new NullReferenceException("Object Search had FAILED!");
			}
			base.gameObject = Object.Instantiate<GameObject>(APIBase.QMCarouselSelectorTemplate, parent);
			base.transform = base.gameObject.transform;
			base.gameObject.name = text;
			base.TMProCompnt = base.transform.Find("LeftItemContainer/Title").GetComponent<TextMeshProUGUI>();
			base.TMProCompnt.text = text;
			base.TMProCompnt.richText = true;
			base.Text = text;
			if (separator)
			{
				GameObject seB = Object.Instantiate<GameObject>(APIBase.QMCarouselSeparator, parent);
				seB.name = "Separator";
			}
			(this.ContainerTooltip = base.transform.Find("LeftItemContainer").GetComponent<ToolTip>())._localizableString = containerTooltip.Localize(null, null, null);
			Button buttonLeft = base.transform.Find("RightItemContainer/ButtonLeft").transform.GetComponent<Button>();
			buttonLeft.onClick.AddListener(delegate
			{
				this.ScrollLeft();
			});
			Button buttonRight = base.transform.Find("RightItemContainer/ButtonRight").transform.GetComponent<Button>();
			buttonRight.onClick.AddListener(delegate
			{
				this.ScrollRight();
			});
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000051BC File Offset: 0x000033BC
		public void AddSetting(string name, string tooltip, Action listener)
		{
			this.settings.Add(new QMCSelector.Setting
			{
				Name = name,
				Tooltip = tooltip,
				Listener = listener
			});
			bool flag = this.settings.Count == 1;
			if (flag)
			{
				this.UpdateDisplayedSetting(0);
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005210 File Offset: 0x00003410
		private void ScrollLeft()
		{
			bool flag = this.settings.Count == 0;
			if (!flag)
			{
				this.currentIndex = (this.currentIndex - 1 + this.settings.Count) % this.settings.Count;
				this.UpdateDisplayedSetting(this.currentIndex);
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005268 File Offset: 0x00003468
		private void ScrollRight()
		{
			bool flag = this.settings.Count == 0;
			if (!flag)
			{
				this.currentIndex = (this.currentIndex + 1) % this.settings.Count;
				this.UpdateDisplayedSetting(this.currentIndex);
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000052B4 File Offset: 0x000034B4
		private void UpdateDisplayedSetting(int index)
		{
			QMCSelector.Setting setting = this.settings[index];
			this.TMProSelectionBoxText = base.transform.Find("RightItemContainer/OptionSelectionBox/Text_MM_H3").GetComponent<TextMeshProUGUI>();
			this.TMProSelectionBoxText.text = setting.Name;
			(this.SelectionBoxTextTooltip = base.transform.Find("RightItemContainer/OptionSelectionBox").GetComponent<ToolTip>())._localizableString = setting.Tooltip.Localize(null, null, null);
			Action listener = setting.Listener;
			if (listener != null)
			{
				listener.Invoke();
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005342 File Offset: 0x00003542
		public QMCSelector(QMCGroup group, string text, string containerTooltip, bool separator = false)
			: this(group.GetTransform().Find("QM_Settings_Panel/VerticalLayoutGroup").transform, text, containerTooltip, separator)
		{
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005365 File Offset: 0x00003565
		public QMCSelector(CollapsibleButtonGroup buttonGroup, string text, string containerTooltip, bool separator = false)
			: this(buttonGroup.QMCParent, text, containerTooltip, separator)
		{
		}

		// Token: 0x0400005A RID: 90
		private List<QMCSelector.Setting> settings = new List<QMCSelector.Setting>();

		// Token: 0x0400005B RID: 91
		private int currentIndex = 0;

		// Token: 0x020000CD RID: 205
		private class Setting
		{
			// Token: 0x17000165 RID: 357
			// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00027124 File Offset: 0x00025324
			// (set) Token: 0x060006D4 RID: 1748 RVA: 0x0002712C File Offset: 0x0002532C
			public string Name { get; set; }

			// Token: 0x17000166 RID: 358
			// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00027135 File Offset: 0x00025335
			// (set) Token: 0x060006D6 RID: 1750 RVA: 0x0002713D File Offset: 0x0002533D
			public string Tooltip { get; set; }

			// Token: 0x17000167 RID: 359
			// (get) Token: 0x060006D7 RID: 1751 RVA: 0x00027146 File Offset: 0x00025346
			// (set) Token: 0x060006D8 RID: 1752 RVA: 0x0002714E File Offset: 0x0002534E
			public Action Listener { get; set; }
		}
	}
}
