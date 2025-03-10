using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.Localization;
using VRC.UI.Core.Styles;
using VRC.UI.Element;
using VRC.UI.Elements.Controls;
using VRC.UI.Elements.Tooltips;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Extras;

namespace WorldAPI.ButtonAPI.QM.Carousel.Items
{
	// Token: 0x02000017 RID: 23
	public class QMCSlider : Root
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000538B File Offset: 0x0000358B
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00005393 File Offset: 0x00003593
		public Action<float, QMCSlider> Listener { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000539C File Offset: 0x0000359C
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x000053A4 File Offset: 0x000035A4
		public TextMeshProUGUI TextMeshPro { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000053AD File Offset: 0x000035AD
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x000053B5 File Offset: 0x000035B5
		private Transform body { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000053BE File Offset: 0x000035BE
		// (set) Token: 0x060000DB RID: 219 RVA: 0x000053C6 File Offset: 0x000035C6
		public Transform valDisplay { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000053CF File Offset: 0x000035CF
		// (set) Token: 0x060000DD RID: 221 RVA: 0x000053D7 File Offset: 0x000035D7
		public Transform slider { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000053E0 File Offset: 0x000035E0
		// (set) Token: 0x060000DF RID: 223 RVA: 0x000053E8 File Offset: 0x000035E8
		public float DefaultValue { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000053F1 File Offset: 0x000035F1
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000053F9 File Offset: 0x000035F9
		public SnapSliderExtendedCallbacks snapSlider { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00005402 File Offset: 0x00003602
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x0000540A File Offset: 0x0000360A
		public Action<bool> ToggleListener { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005413 File Offset: 0x00003613
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x0000541B File Offset: 0x0000361B
		public Transform Handle { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00005424 File Offset: 0x00003624
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x0000542C File Offset: 0x0000362C
		public Toggle ToggleCompnt { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00005435 File Offset: 0x00003635
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x0000543D File Offset: 0x0000363D
		public bool ToggleValue { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00005446 File Offset: 0x00003646
		// (set) Token: 0x060000EB RID: 235 RVA: 0x0000544E File Offset: 0x0000364E
		public Transform ResetButton { get; private set; }

		// Token: 0x060000EC RID: 236 RVA: 0x00005458 File Offset: 0x00003658
		public QMCSlider(QMCGroup group, string text, string tooltip, Action<float, QMCSlider> listener, float defaultValue = 0f, float minValue = 0f, float maxValue = 100f, bool isDecimal = false, string ending = "%", bool separator = false)
		{
			QMCSlider.<>c__DisplayClass51_0 CS$<>8__locals1 = new QMCSlider.<>c__DisplayClass51_0();
			CS$<>8__locals1.listener = listener;
			CS$<>8__locals1.ending = ending;
			base..ctor();
			CS$<>8__locals1.<>4__this = this;
			bool flag = !APIBase.IsReady();
			if (flag)
			{
				throw new NullReferenceException("Object Search had FAILED!");
			}
			string figures = "0";
			base.transform = Object.Instantiate<Transform>(APIBase.Slider, group.GetTransform().Find("QM_Settings_Panel/VerticalLayoutGroup").transform);
			base.gameObject = base.transform.gameObject;
			base.gameObject.name = text;
			(this.body = base.transform.Find("LeftItemContainer")).GetComponent<ToolTip>()._localizableString = tooltip.Localize(null, null, null);
			(this.TextMeshPro = this.body.Find("Title").GetComponent<TextMeshProUGUI>()).text = text;
			this.TextMeshPro.richText = true;
			(this.slider = base.gameObject.transform.Find("RightItemContainer/Slider")).GetComponent<ToolTip>()._localizableString = tooltip.Localize(null, null, null);
			this.valDisplay = this.slider.parent.Find("Text_MM_H3");
			this.snapSlider = this.slider.GetComponent<SnapSliderExtendedCallbacks>();
			this.snapSlider.field_Private_UnityEvent_0 = null;
			this.snapSlider.onValueChanged = new global::UnityEngine.UI.Slider.SliderEvent();
			this.snapSlider.minValue = minValue;
			this.snapSlider.maxValue = maxValue;
			defaultValue = (this.DefaultValue = (this.snapSlider.value = defaultValue));
			this.Listener = delegate(float value, QMCSlider slider)
			{
				bool toggleValue = CS$<>8__locals1.<>4__this.ToggleValue;
				if (toggleValue)
				{
					CS$<>8__locals1.listener.Invoke(value, CS$<>8__locals1.<>4__this);
				}
			};
			this.snapSlider.onValueChanged.AddListener(delegate(float value)
			{
				Action<float, QMCSlider> listener2 = CS$<>8__locals1.<>4__this.Listener;
				if (listener2 != null)
				{
					listener2.Invoke(value, CS$<>8__locals1.<>4__this);
				}
			});
			if (separator)
			{
				GameObject seB = Object.Instantiate<GameObject>(APIBase.QMCarouselSeparator, group.GetTransform().Find("QM_Settings_Panel/VerticalLayoutGroup").transform);
				seB.name = "Separator";
			}
			if (isDecimal)
			{
				figures = "0.0";
			}
			TextMeshProUGUI perst = this.valDisplay.GetComponent<TextMeshProUGUI>();
			perst.gameObject.active = true;
			this.snapSlider.onValueChanged.AddListener(delegate(float va)
			{
				perst.text = va.ToString(figures) + CS$<>8__locals1.ending;
			});
			perst.text = defaultValue.ToString() + CS$<>8__locals1.ending;
			base.gameObject.GetComponent<SettingComponent>().enabled = false;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005728 File Offset: 0x00003928
		public void AddResetButton()
		{
			this.ResetButton = base.transform.Find("RightItemContainer/Button");
			this.ResetButton.gameObject.SetActive(true);
			this.ResetButton.GetComponent<ToolTip>()._localizableString = "Reset to the default value".Localize(null, null, null);
			this.ResetButton.GetOrAddComponent<CanvasGroup>().alpha = 1f;
			Button button = this.ResetButton.GetComponent<Button>();
			button.onClick.AddListener(delegate
			{
				this.ResetValue();
			});
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000057BC File Offset: 0x000039BC
		private void ResetValue()
		{
			bool toggleValue = this.ToggleValue;
			if (toggleValue)
			{
				this.snapSlider.value = this.DefaultValue;
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000057E8 File Offset: 0x000039E8
		public void AddMuteButton(Action<bool> stateChange, bool defaultState = true)
		{
			Transform muteButton = base.transform.Find("RightItemContainer/Cell_MM_ToggleButton");
			muteButton.gameObject.SetActive(true);
			muteButton.SetSiblingIndex(4);
			this.ToggleValue = defaultState;
			this.ToggleCompnt = muteButton.GetComponent<Toggle>();
			this.ToggleListener = stateChange;
			this.ToggleCompnt.onValueChanged = new Toggle.ToggleEvent();
			this.ToggleCompnt.isOn = defaultState;
			this.SetVisualComponents(defaultState);
			this.ToggleCompnt.onValueChanged.AddListener(delegate(bool val)
			{
				bool flag = this.shouldInvoke;
				if (flag)
				{
					APIBase.SafelyInvolk(val, this.ToggleListener, "SliderMuteToggle");
				}
				Action<QMCSlider, bool> onQMCSliderToggleValChange = APIBase.Events.onQMCSliderToggleValChange;
				if (onQMCSliderToggleValChange != null)
				{
					onQMCSliderToggleValChange.Invoke(this, val);
				}
				this.ToggleValue = val;
				this.SetVisualComponents(val);
			});
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005884 File Offset: 0x00003A84
		public void AddToggle(Action<bool> stateChange, bool defaultState = true)
		{
			Transform toggleButton = base.transform.Find("RightItemContainer/Cell_MM_ToggleSwitch");
			toggleButton.gameObject.SetActive(true);
			toggleButton.SetSiblingIndex(4);
			this.ToggleValue = defaultState;
			RadioButton button = toggleButton.Find("Cell_MM_OnOffSwitch").GetComponent<RadioButton>();
			button.Method_Public_Void_Boolean_0(defaultState);
			(this.Handle = button._handle).transform.localPosition = (defaultState ? QMCSlider.onPos : QMCSlider.offPos);
			this.ToggleCompnt = toggleButton.transform.GetComponent<Toggle>();
			this.ToggleListener = stateChange;
			this.ToggleCompnt.onValueChanged = new Toggle.ToggleEvent();
			this.ToggleCompnt.isOn = defaultState;
			this.SetVisualComponents(defaultState);
			this.ToggleCompnt.onValueChanged.AddListener(delegate(bool val)
			{
				bool flag = this.shouldInvoke;
				if (flag)
				{
					APIBase.SafelyInvolk(val, this.ToggleListener, "SliderToggle");
				}
				Action<QMCSlider, bool> onQMCSliderToggleValChange = APIBase.Events.onQMCSliderToggleValChange;
				if (onQMCSliderToggleValChange != null)
				{
					onQMCSliderToggleValChange.Invoke(this, val);
				}
				button.Method_Public_Void_Boolean_0(val);
				this.Handle.localPosition = (val ? QMCSlider.onPos : QMCSlider.offPos);
				this.ToggleValue = val;
				this.SetVisualComponents(val);
			});
			toggleButton.gameObject.GetComponent<UiToggleTooltip>()._localizableString = "Enable / disable this setting".Localize(null, null, null);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000599F File Offset: 0x00003B9F
		public void SoftSetState(bool value)
		{
			this.shouldInvoke = false;
			this.ToggleCompnt.isOn = value;
			this.shouldInvoke = true;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000059C0 File Offset: 0x00003BC0
		private void SetVisualComponents(bool defaultState)
		{
			bool flag = !defaultState;
			if (flag)
			{
				this.slider.GetComponent<Selectable>().m_GroupsAllowInteraction = false;
				this.slider.GetComponent<CanvasGroup>().alpha = 0.25f;
				this.slider.parent.Find("Text_MM_H3").GetComponent<CanvasGroup>().alpha = 0.25f;
				bool active = this.ResetButton.gameObject.active;
				if (active)
				{
					StyleElement RBStyle = this.ResetButton.GetComponent<StyleElement>();
					this.ResetButton.GetComponent<CanvasGroup>().alpha = 0.25f;
					RBStyle.field_Private_Boolean_0 = false;
					RBStyle.field_Private_Boolean_1 = true;
				}
			}
			else
			{
				this.slider.GetComponent<Selectable>().m_GroupsAllowInteraction = true;
				this.slider.GetComponent<CanvasGroup>().alpha = 1f;
				this.slider.parent.Find("Text_MM_H3").GetComponent<CanvasGroup>().alpha = 1f;
				bool active2 = this.ResetButton.gameObject.active;
				if (active2)
				{
					StyleElement RBStyle2 = this.ResetButton.GetComponent<StyleElement>();
					this.ResetButton.GetComponent<CanvasGroup>().alpha = 1f;
					RBStyle2.field_Private_Boolean_0 = true;
					RBStyle2.field_Private_Boolean_1 = false;
				}
			}
		}

		// Token: 0x04000067 RID: 103
		private bool shouldInvoke = true;

		// Token: 0x04000068 RID: 104
		private static Vector3 onPos = new Vector3(93f, 0f, 0f);

		// Token: 0x04000069 RID: 105
		private static Vector3 offPos = new Vector3(30f, 0f, 0f);
	}
}
