using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.Localization;
using VRC.UI.Element;
using VRC.UI.Elements.Controls;
using VRC.UI.Elements.Tooltips;
using WorldAPI.ButtonAPI.Controls;

namespace WorldAPI.ButtonAPI.MM.Carousel.Items
{
	// Token: 0x02000027 RID: 39
	public class CToggle : Root
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000816B File Offset: 0x0000636B
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00008173 File Offset: 0x00006373
		public Action<bool> Listener { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000817C File Offset: 0x0000637C
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00008184 File Offset: 0x00006384
		public Toggle ToggleCompnt { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000818D File Offset: 0x0000638D
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00008195 File Offset: 0x00006395
		public UiToggleTooltip ToolTip { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000819E File Offset: 0x0000639E
		// (set) Token: 0x06000178 RID: 376 RVA: 0x000081A6 File Offset: 0x000063A6
		public Transform Handle { get; private set; }

		// Token: 0x06000179 RID: 377 RVA: 0x000081B0 File Offset: 0x000063B0
		public CToggle(CGrp grp, string text, Action<bool> stateChange, bool defaultState = false, string toolTip = "", string toolTip2 = "")
		{
			CToggle <>4__this = this;
			bool flag = !APIBase.IsReady();
			if (flag)
			{
				throw new NullReferenceException("Object Search had FAILED!");
			}
			base.gameObject = Object.Instantiate<GameObject>(APIBase.MMCTgl, grp.MenuContents.transform);
			base.transform = base.gameObject.transform;
			base.gameObject.name = text;
			base.TMProCompnt = base.transform.Find("LeftItemContainer/Title").GetComponent<TextMeshProUGUI>();
			base.TMProCompnt.text = text;
			base.TMProCompnt.richText = true;
			base.Text = text;
			(this.ToolTip = base.gameObject.GetComponent<UiToggleTooltip>())._localizableString = toolTip.Localize(null, null, null);
			this.toggleSwitch = base.transform.Find("RightItemContainer/Cell_MM_OnOffSwitch").GetComponent<RadioButton>();
			this.toggleSwitch.Method_Public_Void_Boolean_0(defaultState);
			(this.Handle = this.toggleSwitch._handle).transform.localPosition = (defaultState ? CToggle.onPos : CToggle.offPos);
			this.ToggleCompnt = base.gameObject.GetComponent<Toggle>();
			this.ToggleCompnt.onValueChanged = new Toggle.ToggleEvent();
			this.ToggleCompnt.isOn = defaultState;
			this.Listener = stateChange;
			this.ToggleCompnt.onValueChanged.AddListener(delegate(bool val)
			{
				bool flag2 = <>4__this.shouldInvoke;
				if (flag2)
				{
					APIBase.SafelyInvolk(val, <>4__this.Listener, text);
				}
				Action<CToggle, bool> onCToggleValChange = APIBase.Events.onCToggleValChange;
				if (onCToggleValChange != null)
				{
					onCToggleValChange.Invoke(<>4__this, val);
				}
				<>4__this.toggleSwitch.Method_Public_Void_Boolean_0(val);
				<>4__this.Handle.localPosition = (val ? CToggle.onPos : CToggle.offPos);
			});
			base.gameObject.GetComponent<SettingComponent>().enabled = false;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000836A File Offset: 0x0000656A
		public void SoftSetState(bool value)
		{
			this.shouldInvoke = false;
			this.ToggleCompnt.isOn = value;
			this.shouldInvoke = true;
		}

		// Token: 0x0400009E RID: 158
		private RadioButton toggleSwitch;

		// Token: 0x0400009F RID: 159
		private bool shouldInvoke = true;

		// Token: 0x040000A0 RID: 160
		private static Vector3 onPos = new Vector3(93f, 0f, 0f);

		// Token: 0x040000A1 RID: 161
		private static Vector3 offPos = new Vector3(30f, 0f, 0f);
	}
}
