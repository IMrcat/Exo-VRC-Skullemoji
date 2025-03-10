using System;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Buttons;

namespace WorldAPI.ButtonAPI.Controls
{
	// Token: 0x0200000F RID: 15
	public class ExtentedControl : Root
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003B2C File Offset: 0x00001D2C
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00003B34 File Offset: 0x00001D34
		public Button ButtonCompnt { get; internal set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003B3D File Offset: 0x00001D3D
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00003B45 File Offset: 0x00001D45
		public Image ImgCompnt { get; internal set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003B4E File Offset: 0x00001D4E
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00003B56 File Offset: 0x00001D56
		public Action onClickAction { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003B5F File Offset: 0x00001D5F
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00003B67 File Offset: 0x00001D67
		public string ToolTip { get; internal set; }

		// Token: 0x06000062 RID: 98 RVA: 0x00003B70 File Offset: 0x00001D70
		public void SetSprite1(Sprite sprite)
		{
			this.ImgCompnt.sprite = sprite;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003B7F File Offset: 0x00001D7F
		public void SetSprite(Sprite sprite)
		{
			this.ImgCompnt.overrideSprite = sprite;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003B8E File Offset: 0x00001D8E
		public Sprite GetSprite()
		{
			return this.ImgCompnt.sprite;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003B9B File Offset: 0x00001D9B
		public void ShowSubMenuIcon(bool state)
		{
			base.gameObject.transform.Find("Badge_MMJump").gameObject.SetActive(state);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003BBE File Offset: 0x00001DBE
		public void SetIconColor(Color color)
		{
			this.ImgCompnt.color = color;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003BD0 File Offset: 0x00001DD0
		public override string SetToolTip(string tip)
		{
			this.ToolTip = tip;
			return base.SetToolTip(tip);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003BF4 File Offset: 0x00001DF4
		public void SetAction(Action newAction)
		{
			this.SetAction(delegate(VRCButton _)
			{
				newAction.Invoke();
			});
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003C24 File Offset: 0x00001E24
		public void SetAction(Action<VRCButton> newAction)
		{
			this.ButtonCompnt.onClick = new Button.ButtonClickedEvent();
			this.onClickAction = delegate
			{
				newAction.Invoke(this.inst);
			};
			this.ButtonCompnt.onClick.AddListener(delegate
			{
				APIBase.SafelyInvolk(this.onClickAction, this.Text);
			});
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003C8C File Offset: 0x00001E8C
		public void SetBackgroundImage(Sprite newImg)
		{
			base.gameObject.transform.Find("Background").GetComponent<Image>().sprite = newImg;
			base.gameObject.transform.Find("Background").GetComponent<Image>().overrideSprite = newImg;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003CDC File Offset: 0x00001EDC
		internal void ResetTextPox()
		{
			base.TMProCompnt.transform.localPosition = new Vector3(0f, 0f, 0f);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003D04 File Offset: 0x00001F04
		public void TurnHalf(ExtentedControl.HalfType Type, bool IsGroup)
		{
			this.ImgCompnt.gameObject.active = false;
			base.TMProCompnt.fontSize = 22f;
			Transform Jmp = base.gameObject.transform.Find("Badge_MMJump");
			Jmp.localPosition = new Vector3(Jmp.localPosition.x, Jmp.localPosition.y - 34f, Jmp.localPosition.z);
			if (IsGroup)
			{
				base.gameObject.transform.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(0f, -115f);
			}
			else
			{
				base.gameObject.transform.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(0f, -80f);
			}
			switch (Type)
			{
			case ExtentedControl.HalfType.Top:
				this.ImgCompnt.transform.localPosition = new Vector3(0f, 0f, 0f);
				base.TMProCompnt.transform.localPosition = new Vector3(0f, 90f, 0f);
				base.gameObject.transform.Find("Background").localPosition = new Vector3(0f, 53f, 0f);
				break;
			case ExtentedControl.HalfType.Normal:
				this.ImgCompnt.transform.localPosition = new Vector3(0f, 0f, 0f);
				base.TMProCompnt.transform.localPosition = new Vector3(0f, 43f, 0f);
				break;
			case ExtentedControl.HalfType.Bottom:
				this.ImgCompnt.transform.localPosition = new Vector3(0f, 0f, 0f);
				base.TMProCompnt.transform.localPosition = new Vector3(0f, -5f, 0f);
				base.gameObject.transform.Find("Background").localPosition = new Vector3(0f, -53f, 0f);
				break;
			}
		}

		// Token: 0x0400003F RID: 63
		internal VRCButton inst;

		// Token: 0x020000C3 RID: 195
		public enum HalfType
		{
			// Token: 0x04000334 RID: 820
			Top,
			// Token: 0x04000335 RID: 821
			Normal,
			// Token: 0x04000336 RID: 822
			Bottom
		}
	}
}
