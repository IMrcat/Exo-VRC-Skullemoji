using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.Localization;
using VRC.UI.Element;
using VRC.UI.Elements.Controls;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.MM.Carousel.Items;

namespace WorldAPI.ButtonAPI.QM.Extras
{
	// Token: 0x02000012 RID: 18
	public class VRCSlider
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000046DD File Offset: 0x000028DD
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000046E5 File Offset: 0x000028E5
		public GameObject gameObject { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000046EE File Offset: 0x000028EE
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000046F6 File Offset: 0x000028F6
		public TextMeshProUGUI TextMeshPro { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000046FF File Offset: 0x000028FF
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00004707 File Offset: 0x00002907
		public Transform transform { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004710 File Offset: 0x00002910
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00004718 File Offset: 0x00002918
		public Transform slider { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004721 File Offset: 0x00002921
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00004729 File Offset: 0x00002929
		public SnapSliderExtendedCallbacks snapSlider { get; private set; }

		// Token: 0x060000A5 RID: 165 RVA: 0x00004734 File Offset: 0x00002934
		public VRCSlider(Transform menu, string text, string tooltip, Action<float, VRCSlider> listener, float defaultValue = 0f, float minValue = 0f, float maxValue = 100f)
		{
			VRCSlider <>4__this = this;
			bool flag = !APIBase.IsReady();
			if (flag)
			{
				throw new NullReferenceException("Object Search had FAILED!");
			}
			this.transform = Object.Instantiate<Transform>(APIBase.Slider, menu);
			this.gameObject = this.transform.gameObject;
			this.gameObject.name = text;
			(this.TextMeshPro = this.gameObject.transform.Find("LeftItemContainer/Title").GetComponent<TextMeshProUGUI>()).text = text;
			this.TextMeshPro.richText = true;
			(this.slider = this.gameObject.transform.Find("RightItemContainer/Slider")).GetComponent<ToolTip>()._localizableString = tooltip.Localize(null, null, null);
			this.snapSlider = this.slider.GetComponent<SnapSliderExtendedCallbacks>();
			this.snapSlider.field_Private_UnityEvent_0 = null;
			this.snapSlider.onValueChanged = new global::UnityEngine.UI.Slider.SliderEvent();
			this.snapSlider.minValue = minValue;
			this.snapSlider.maxValue = maxValue;
			this.snapSlider.value = defaultValue;
			this.snapSlider.onValueChanged.AddListener(delegate(float va)
			{
				listener.Invoke(va, <>4__this);
			});
			this.slider.parent.Find("Text_MM_H3").gameObject.active = false;
			this.gameObject.GetComponent<SettingComponent>().enabled = false;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000048C0 File Offset: 0x00002AC0
		public VRCSlider PercentEnding(string ending = "%")
		{
			TextMeshProUGUI perst = this.slider.parent.Find("Text_MM_H3").GetComponent<TextMeshProUGUI>();
			perst.gameObject.active = true;
			this.snapSlider.onValueChanged.AddListener(delegate(float va)
			{
				perst.text = va.ToString("0.00") + ending;
			});
			return this;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004934 File Offset: 0x00002B34
		public VRCSlider Button(Action onClick, string toolTip = "", Sprite Icon = null)
		{
			return this.Button(delegate(VRCSlider tgl)
			{
				Action onClick2 = onClick;
				if (onClick2 != null)
				{
					onClick2.Invoke();
				}
			}, toolTip, Icon);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004964 File Offset: 0x00002B64
		public VRCSlider Button(Action<VRCSlider> onClick, string toolTip = "", Sprite Icon = null)
		{
			Transform Btn = this.slider.parent.Find("Button");
			Btn.GetComponent<ToolTip>()._localizableString = toolTip.Localize(null, null, null);
			(Btn.GetComponent<Button>().onClick = new Button.ButtonClickedEvent()).AddListener(delegate
			{
				Action<VRCSlider> onClick2 = onClick;
				if (onClick2 != null)
				{
					onClick2.Invoke(this);
				}
			});
			Btn.gameObject.active = true;
			bool flag = Icon != null;
			if (flag)
			{
				Btn.Find("Icon").GetComponent<Image>().sprite = Icon;
			}
			return this;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004A14 File Offset: 0x00002C14
		public VRCToggle Toggle(string text, Action<bool> listenr, bool defaultState = false, string OffTooltip = null, string OnToolTip = null, Sprite onimage = null, Sprite offimage = null)
		{
			VRCToggle tgl = new VRCToggle(this.slider.parent, text, listenr, defaultState, OffTooltip, OnToolTip, onimage, offimage, false);
			tgl.OnImage.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			tgl.OnImage.transform.localPosition = new Vector3(21.5f, 35f, 0f);
			tgl.OffImage.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
			tgl.OffImage.transform.localPosition = new Vector3(-22.5f, 24.5f, 0f);
			tgl.TMProCompnt.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			tgl.TMProCompnt.transform.localPosition = new Vector3(0f, -35f, 0f);
			tgl.GetTransform().Find("Background").localScale = new Vector3(1f, 0.5f, 1f);
			return tgl;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004B4C File Offset: 0x00002D4C
		public VRCSlider(VRCPage menu, string text, string tooltip, Action<float> listener, float defaultValue = 0f, float minValue = 0f, float maxValue = 100f)
			: this(menu.MenuContents, text, tooltip, delegate(float va, VRCSlider _)
			{
				listener.Invoke(va);
			}, defaultValue, minValue, maxValue)
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004B89 File Offset: 0x00002D89
		public VRCSlider(VRCPage menu, string text, string tooltip, Action<float, VRCSlider> listener, float defaultValue = 0f, float minValue = 0f, float maxValue = 100f)
			: this(menu.MenuContents, text, tooltip, listener, defaultValue, minValue, maxValue)
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004BA4 File Offset: 0x00002DA4
		public VRCSlider(CGrp menu, string text, string tooltip, Action<float> listener, float defaultValue = 0f, float minValue = 0f, float maxValue = 100f)
			: this(menu.MenuContents, text, tooltip, delegate(float va, VRCSlider _)
			{
				listener.Invoke(va);
			}, defaultValue, minValue, maxValue)
		{
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004BE1 File Offset: 0x00002DE1
		public VRCSlider(CGrp menu, string text, string tooltip, Action<float, VRCSlider> listener, float defaultValue = 0f, float minValue = 0f, float maxValue = 100f)
			: this(menu.MenuContents, text, tooltip, listener, defaultValue, minValue, maxValue)
		{
		}
	}
}
