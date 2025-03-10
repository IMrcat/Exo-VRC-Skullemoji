using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Extras;

namespace WorldAPI.ButtonAPI.Buttons
{
	// Token: 0x0200001C RID: 28
	public class VRCToggle : ToggleControl
	{
		// Token: 0x0600010B RID: 267 RVA: 0x000063DC File Offset: 0x000045DC
		public VRCToggle(Transform menu, string text, Action<bool, VRCToggle> listener, bool DefaultState = false, string OffTooltip = null, string OnToolTip = null, Sprite onimage = null, Sprite offimage = null, bool half = false)
		{
			bool flag = !APIBase.IsReady();
			if (flag)
			{
				throw new NullReferenceException("Object Search had FAILED!");
			}
			if (OffTooltip == null)
			{
				OffTooltip = "Turn On " + text.Replace("\n", string.Empty);
			}
			if (OnToolTip == null)
			{
				OnToolTip = "Turn Off " + text.Replace("\n", string.Empty);
			}
			bool flag2 = VRCToggle.ToggleTemplate == null;
			if (flag2)
			{
				VRCButton btn = new VRCButton(APIBase.ButtonGrp.transform, text, string.Empty, delegate
				{
				}, false, false, null, ExtentedControl.HalfType.Normal, false);
				VRCToggle.ToggleTemplate = btn.transform;
				Object.DestroyImmediate(VRCToggle.ToggleTemplate.GetComponent<Button>());
				Object.DestroyImmediate(VRCToggle.ToggleTemplate.Find("Icons/Icon_Secondary"));
				Object.DestroyImmediate(VRCToggle.ToggleTemplate.Find("Badge_Close"));
				Object.DestroyImmediate(VRCToggle.ToggleTemplate.Find("Badge_MMJump"));
				VRCToggle.ToggleTemplate.gameObject.AddComponent<Toggle>();
				Transform defaultImageObj = btn.ImgCompnt.transform;
				defaultImageObj.name = "Icon_Off";
				Transform onImge = Object.Instantiate<Transform>(defaultImageObj, defaultImageObj.parent);
				onImge.name = "Icon_On";
				defaultImageObj.gameObject.active = true;
				onImge.gameObject.active = true;
				defaultImageObj.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
				VRCToggle.ToggleTemplate.gameObject.SetActive(false);
			}
			base.gameObject = (base.transform = Object.Instantiate<Transform>(VRCToggle.ToggleTemplate, menu)).gameObject;
			base.gameObject.SetActive(true);
			base.gameObject.name = text;
			base.ToggleCompnt = base.transform.GetOrAddComponent<Toggle>();
			base.TMProCompnt = base.transform.GetComponentInChildren<TextMeshProUGUI>();
			base.Text = text;
			base.TMProCompnt.text = text;
			base.TMProCompnt.richText = true;
			base.OnImage = base.gameObject.transform.Find("Icons/Icon_On").GetComponent<Image>();
			base.OffImage = base.gameObject.transform.Find("Icons/Icon_Off").GetComponent<Image>();
			base.OffImage.transform.localPosition = new Vector3(-46f, 0f, 0f);
			base.OnImage.transform.localPosition = new Vector3(49f, 0f, 0f);
			base.Listener = listener;
			base.SoftSetState(DefaultState);
			base.SetImages(true, onimage, offimage);
			this.SetToolTip(DefaultState ? OnToolTip : OffTooltip);
			if (half)
			{
				base.TurnHalf(24f);
			}
			this.inst = this;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000066D8 File Offset: 0x000048D8
		public VRCToggle RSetActive(bool val)
		{
			this.SetActive(val);
			return this;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000066F4 File Offset: 0x000048F4
		public VRCToggle(ButtonGroupControl buttonGroup, string text, Action<bool, VRCToggle> stateChanged, bool DefaultState = false, string OffTooltip = "Off", string OnToolTip = "On", Sprite onimage = null, Sprite offimage = null, bool half = false)
			: this(buttonGroup.GroupContents.transform, text, stateChanged, DefaultState, OffTooltip, OnToolTip, onimage, offimage, half)
		{
			buttonGroup._toggles.Add(this);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006730 File Offset: 0x00004930
		public VRCToggle(ButtonGroupControl buttonGroup, string text, Action<bool> stateChanged, bool DefaultState = false, string OffTooltip = "Off", string OnToolTip = "On", Sprite onimage = null, Sprite offimage = null, bool half = false)
			: this(buttonGroup, text, delegate(bool val, VRCToggle _)
			{
				stateChanged.Invoke(val);
			}, DefaultState, OffTooltip, OnToolTip, onimage, offimage, half)
		{
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000676C File Offset: 0x0000496C
		public VRCToggle(Transform menu, string text, Action<bool> stateChanged, bool DefaultState = false, string OffTooltip = "Off", string OnToolTip = "On", Sprite onimage = null, Sprite offimage = null, bool half = false)
			: this(menu, text, delegate(bool val, VRCToggle _)
			{
				stateChanged.Invoke(val);
			}, DefaultState, OffTooltip, OnToolTip, onimage, offimage, half)
		{
		}

		// Token: 0x04000075 RID: 117
		private static Transform ToggleTemplate;
	}
}
