using System;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements.Tooltips;
using WorldAPI.ButtonAPI.Buttons;

namespace WorldAPI.ButtonAPI.Controls
{
	// Token: 0x02000011 RID: 17
	public class ToggleControl : Root
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00004161 File Offset: 0x00002361
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00004169 File Offset: 0x00002369
		public bool IsHalf { get; internal set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00004172 File Offset: 0x00002372
		// (set) Token: 0x06000084 RID: 132 RVA: 0x0000417A File Offset: 0x0000237A
		public Image OnImage { get; internal set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00004183 File Offset: 0x00002383
		// (set) Token: 0x06000086 RID: 134 RVA: 0x0000418B File Offset: 0x0000238B
		public Image OffImage { get; internal set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00004194 File Offset: 0x00002394
		// (set) Token: 0x06000088 RID: 136 RVA: 0x0000419C File Offset: 0x0000239C
		public Toggle ToggleCompnt { get; internal set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000041A5 File Offset: 0x000023A5
		// (set) Token: 0x0600008A RID: 138 RVA: 0x000041AD File Offset: 0x000023AD
		public UiToggleTooltip TipComp { get; internal set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000041B6 File Offset: 0x000023B6
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000041BE File Offset: 0x000023BE
		internal Action<bool, VRCToggle> Listener { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000041C7 File Offset: 0x000023C7
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000041D4 File Offset: 0x000023D4
		public bool State
		{
			get
			{
				return this.ToggleCompnt.isOn;
			}
			set
			{
				this.ToggleCompnt.isOn = value;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000041E4 File Offset: 0x000023E4
		public void SetAction(Action<bool> newAction)
		{
			this.Listener = delegate(bool val, VRCToggle _)
			{
				newAction.Invoke(val);
			};
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004211 File Offset: 0x00002411
		public void SetAction(Action<bool, VRCToggle> newAction)
		{
			this.Listener = newAction;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000421C File Offset: 0x0000241C
		public void SoftSetState(bool value)
		{
			this.ToggleCompnt.onValueChanged = new Toggle.ToggleEvent();
			this.State = value;
			this.ToggleCompnt.onValueChanged.AddListener(delegate(bool val)
			{
				APIBase.SafelyInvolk(val, delegate(bool va)
				{
					this.Listener.Invoke(va, this.inst);
				}, base.Text);
				Action<VRCToggle, bool> onVRCToggleValChange = APIBase.Events.onVRCToggleValChange;
				if (onVRCToggleValChange != null)
				{
					onVRCToggleValChange.Invoke(this.inst, val);
				}
				this.OnImage.color = new Color(this.OnImage.color.r, this.OnImage.color.g, this.OnImage.color.b, val ? 1f : 0.17f);
				this.OffImage.color = new Color(this.OnImage.color.r, this.OnImage.color.g, this.OnImage.color.b, val ? 0.17f : 1f);
				bool isHalf = this.IsHalf;
				if (isHalf)
				{
					this.OffImage.gameObject.active = !val;
					this.OnImage.gameObject.active = val;
				}
			});
			this.OnImage.color = new Color(this.OnImage.color.r, this.OnImage.color.g, this.OnImage.color.b, value ? 1f : 0.17f);
			this.OffImage.color = new Color(this.OnImage.color.r, this.OnImage.color.g, this.OnImage.color.b, value ? 0.17f : 1f);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004308 File Offset: 0x00002508
		public ValueTuple<Sprite, Sprite> SetImages(Sprite onSprite = null, Sprite offSprite = null)
		{
			this.OffImage.sprite = offSprite;
			this.OnImage.sprite = onSprite;
			return new ValueTuple<Sprite, Sprite>(onSprite, offSprite);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000433B File Offset: 0x0000253B
		public void SetInteractable(bool val)
		{
			this.ToggleCompnt.interactable = val;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000434C File Offset: 0x0000254C
		public ValueTuple<Sprite, Sprite> SetImages(bool checkForNull, Sprite onSprite = null, Sprite offSprite = null)
		{
			if (checkForNull)
			{
				if (onSprite == null)
				{
					onSprite = APIBase.OnSprite;
				}
				if (offSprite == null)
				{
					offSprite = APIBase.OffSprite;
				}
			}
			bool flag = offSprite != null;
			if (flag)
			{
				this.OffImage.sprite = offSprite;
			}
			bool flag2 = onSprite != null;
			if (flag2)
			{
				this.OnImage.sprite = onSprite;
			}
			return new ValueTuple<Sprite, Sprite>(onSprite, offSprite);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000043B0 File Offset: 0x000025B0
		public void TurnHalf(Vector3 TogglePoz, float FontSize = 24f)
		{
			base.gameObject.transform.localPosition = TogglePoz;
			this.TurnHalf(FontSize);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000043D0 File Offset: 0x000025D0
		public void TurnHalf(float FontSize = 24f)
		{
			bool isHalf = this.IsHalf;
			if (isHalf)
			{
				throw new Exception("Toggle is ALREADY half!");
			}
			this.IsHalf = true;
			this.OnImage.transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
			this.OnImage.transform.localPosition = new Vector3(-52f, -5f, 0f);
			this.OnImage.gameObject.SetActive(this.State);
			this.OffImage.transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
			this.OffImage.transform.localPosition = new Vector3(-52f, -5f, 0f);
			this.OffImage.gameObject.SetActive(!this.State);
			base.TMProCompnt.fontSize = FontSize;
			base.TMProCompnt.transform.localPosition = new Vector3(34.5f, 43f, 0f);
			base.TMProCompnt.transform.parent.GetComponent<LayoutElement>().enabled = false;
			base.TMProCompnt.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 50f);
			base.gameObject.transform.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(0f, -80f);
			this.Listener = (Action<bool, VRCToggle>)Delegate.Combine(this.Listener, delegate(bool val, VRCToggle _)
			{
				this.OffImage.gameObject.active = !val;
				this.OnImage.gameObject.active = val;
			});
		}

		// Token: 0x04000049 RID: 73
		internal VRCToggle inst;
	}
}
