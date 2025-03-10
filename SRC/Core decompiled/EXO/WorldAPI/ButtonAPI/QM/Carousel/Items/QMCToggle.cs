using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.Localization;
using VRC.UI.Element;
using VRC.UI.Elements.Controls;
using VRC.UI.Elements.Tooltips;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Groups;

namespace WorldAPI.ButtonAPI.QM.Carousel.Items
{
	// Token: 0x02000019 RID: 25
	public class QMCToggle : ToggleControl
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005C6F File Offset: 0x00003E6F
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00005C77 File Offset: 0x00003E77
		public Action<bool> ListenerC { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005C80 File Offset: 0x00003E80
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00005C88 File Offset: 0x00003E88
		public UiToggleTooltip ToolTip { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00005C91 File Offset: 0x00003E91
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00005C99 File Offset: 0x00003E99
		public Transform Handle { get; private set; }

		// Token: 0x060000FE RID: 254 RVA: 0x00005CA4 File Offset: 0x00003EA4
		public QMCToggle(Transform parent, string text, Action<bool> stateChange, bool defaultState = false, string tooltip = "", bool separator = false)
		{
			QMCToggle <>4__this = this;
			bool flag = !APIBase.IsReady();
			if (flag)
			{
				throw new NullReferenceException("Object Search had FAILED!");
			}
			base.gameObject = Object.Instantiate<GameObject>(APIBase.QMCarouselToggleTemplate, parent);
			base.transform = base.gameObject.transform;
			base.gameObject.name = text;
			base.TMProCompnt = base.transform.Find("LeftItemContainer/Title").GetComponent<TextMeshProUGUI>();
			base.TMProCompnt.text = text;
			base.TMProCompnt.richText = true;
			base.Text = text;
			(this.ToolTip = base.gameObject.GetComponent<UiToggleTooltip>())._localizableString = tooltip.Localize(null, null, null);
			if (separator)
			{
				GameObject seB = Object.Instantiate<GameObject>(APIBase.QMCarouselSeparator, parent);
				seB.name = "Separator";
			}
			this.toggleSwitch = base.transform.Find("RightItemContainer/Cell_MM_OnOffSwitch").GetComponent<RadioButton>();
			this.toggleSwitch.Method_Public_Void_Boolean_0(defaultState);
			(this.Handle = this.toggleSwitch._handle).transform.localPosition = (defaultState ? QMCToggle.onPos : QMCToggle.offPos);
			base.ToggleCompnt = base.gameObject.GetComponent<Toggle>();
			base.ToggleCompnt.onValueChanged = new Toggle.ToggleEvent();
			base.ToggleCompnt.isOn = defaultState;
			this.ListenerC = stateChange;
			base.ToggleCompnt.onValueChanged.AddListener(delegate(bool val)
			{
				bool flag2 = <>4__this.shouldInvoke;
				if (flag2)
				{
					APIBase.SafelyInvolk(val, <>4__this.ListenerC, text);
				}
				Action<QMCToggle, bool> onQMCToggleValChange = APIBase.Events.onQMCToggleValChange;
				if (onQMCToggleValChange != null)
				{
					onQMCToggleValChange.Invoke(<>4__this, val);
				}
				<>4__this.toggleSwitch.Method_Public_Void_Boolean_0(val);
				<>4__this.Handle.localPosition = (val ? QMCToggle.onPos : QMCToggle.offPos);
			});
			base.gameObject.GetComponent<SettingComponent>().enabled = false;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005E78 File Offset: 0x00004078
		public new void SoftSetState(bool value)
		{
			this.shouldInvoke = false;
			base.ToggleCompnt.isOn = value;
			this.shouldInvoke = true;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005E96 File Offset: 0x00004096
		public QMCToggle(QMCGroup group, string text, Action<bool> stateChange, bool defaultState = false, string tooltip = "", bool separator = false)
			: this(group.GetTransform().Find("QM_Settings_Panel/VerticalLayoutGroup").transform, text, stateChange, defaultState, tooltip, separator)
		{
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005EBD File Offset: 0x000040BD
		public QMCToggle(CollapsibleButtonGroup buttonGroup, string text, Action<bool> stateChange, bool defaultState = false, string tooltip = "", bool separator = false)
			: this(buttonGroup.QMCParent, text, stateChange, defaultState, tooltip, separator)
		{
		}

		// Token: 0x0400006E RID: 110
		private RadioButton toggleSwitch;

		// Token: 0x0400006F RID: 111
		private bool shouldInvoke = true;

		// Token: 0x04000070 RID: 112
		private static Vector3 onPos = new Vector3(93f, 0f, 0f);

		// Token: 0x04000071 RID: 113
		private static Vector3 offPos = new Vector3(30f, 0f, 0f);
	}
}
