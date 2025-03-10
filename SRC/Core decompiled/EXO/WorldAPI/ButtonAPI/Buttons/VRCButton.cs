using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using WorldAPI.ButtonAPI.Controls;

namespace WorldAPI.ButtonAPI.Buttons
{
	// Token: 0x0200001B RID: 27
	public class VRCButton : ExtentedControl
	{
		// Token: 0x06000107 RID: 263 RVA: 0x000060B8 File Offset: 0x000042B8
		public VRCButton(Transform menu, string text, string tooltip, Action<VRCButton> listener, bool Half = false, bool SubMenuIcon = false, Sprite Icon = null, ExtentedControl.HalfType Type = ExtentedControl.HalfType.Normal, bool IsGroup = false)
		{
			bool flag = !APIBase.IsReady();
			if (flag)
			{
				throw new NullReferenceException("Object Search had FAILED!");
			}
			if (Icon == null)
			{
				Icon = APIBase.DefaultButtonSprite;
			}
			(base.transform = Object.Instantiate<Transform>(APIBase.Button, menu)).name = "Button_" + text;
			base.gameObject = base.transform.gameObject;
			base.gameObject.SetActive(true);
			base.TMProCompnt = base.transform.GetComponentInChildren<TextMeshProUGUI>();
			bool flag2 = base.TMProCompnt == null;
			if (flag2)
			{
				throw new NullReferenceException("Unable to grab Text Object");
			}
			base.TMProCompnt.text = text;
			base.TMProCompnt.richText = true;
			base.Text = text;
			base.ButtonCompnt = base.transform.GetComponent<Button>();
			bool flag3 = base.ButtonCompnt == null;
			if (flag3)
			{
				throw new NullReferenceException("Unable to grab Button Componet");
			}
			base.ButtonCompnt.onClick = new Button.ButtonClickedEvent();
			bool flag4 = listener != null;
			if (flag4)
			{
				base.SetAction(listener);
			}
			else
			{
				base.ButtonCompnt.interactable = false;
			}
			base.ImgCompnt = base.transform.transform.Find("Icons/Icon").GetComponent<Image>();
			StyleElement elemetn = base.ImgCompnt.gameObject.GetComponent<StyleElement>();
			bool flag5 = elemetn != null;
			if (flag5)
			{
				elemetn.enabled = false;
			}
			bool flag6 = base.ImgCompnt.color == Color.black;
			if (flag6)
			{
				base.ImgCompnt.color = Color.white;
			}
			Object.Destroy(base.transform.transform.Find("Icons/Icon_Secondary").gameObject);
			bool flag7 = Icon != null;
			if (flag7)
			{
				base.SetSprite1(Icon);
				base.SetSprite(Icon);
			}
			else
			{
				base.transform.transform.Find("Icons/Icon").gameObject.active = false;
				base.ResetTextPox();
			}
			base.ShowSubMenuIcon(SubMenuIcon);
			this.SetToolTip(tooltip);
			if (Half)
			{
				base.TurnHalf(Type, IsGroup);
			}
			this.inst = this;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000062F4 File Offset: 0x000044F4
		public VRCButton(Transform menu, string text, string tooltip, Action click, bool Half = false, bool subMenuIcon = false, Sprite icon = null, ExtentedControl.HalfType Type = ExtentedControl.HalfType.Normal, bool IsGroup = false)
			: this(menu, text, tooltip, delegate(VRCButton _)
			{
				click.Invoke();
			}, Half, subMenuIcon, icon, Type, IsGroup)
		{
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00006330 File Offset: 0x00004530
		public VRCButton(ButtonGroupControl buttonGroup, string text, string tooltip, Action click, bool Half = false, bool subMenuIcon = false, Sprite icon = null, ExtentedControl.HalfType Type = ExtentedControl.HalfType.Normal, bool IsGroup = false)
			: this(buttonGroup, text, tooltip, delegate(VRCButton _)
			{
				click.Invoke();
			}, Half, subMenuIcon, icon, Type, IsGroup)
		{
			bool flag = !Half && icon == null;
			if (flag)
			{
				base.transform.transform.Find("Icons").gameObject.active = false;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000063A0 File Offset: 0x000045A0
		public VRCButton(ButtonGroupControl buttonGroup, string text, string tooltip, Action<VRCButton> click, bool Half = false, bool subMenuIcon = false, Sprite icon = null, ExtentedControl.HalfType Type = ExtentedControl.HalfType.Normal, bool IsGroup = false)
			: this(buttonGroup.GroupContents.transform, text, tooltip, click, Half, subMenuIcon, icon, Type, IsGroup)
		{
			buttonGroup._buttons.Add(this);
		}
	}
}
