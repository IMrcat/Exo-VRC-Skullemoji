using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Controls;

namespace WorldAPI.ButtonAPI.Buttons
{
	// Token: 0x0200001A RID: 26
	public class VRCLable : Root
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00005F09 File Offset: 0x00004109
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00005F11 File Offset: 0x00004111
		public Button ButtonCompnt { get; internal set; }

		// Token: 0x06000105 RID: 261 RVA: 0x00005F1C File Offset: 0x0000411C
		public VRCLable(Transform menu, string text, string LowerText, Action onClick = null, bool Bg = true)
		{
			bool flag = !APIBase.IsReady();
			if (flag)
			{
				throw new NullReferenceException("Object Search had FAILED!");
			}
			base.gameObject = (this.SButton = new VRCButton(menu, text, null, onClick, false, false, null, ExtentedControl.HalfType.Normal, false)).gameObject;
			this.ButtonCompnt = this.SButton.ButtonCompnt;
			this.SButton.ImgCompnt.enabled = false;
			base.TMProCompnt = this.SButton.TMProCompnt;
			base.TMProCompnt.richText = true;
			base.TMProCompnt.transform.localPosition = new Vector3(0f, 2f, 0f);
			base.TMProCompnt.fontSize = 38f;
			base.TMProCompnt.enableAutoSizing = true;
			GameObject Text2 = Object.Instantiate<GameObject>(this.SButton.TMProCompnt.gameObject, new Vector3(0f, -54.75f, 0f), Quaternion.Euler(0f, 0f, 0f), this.SButton.transform);
			Text2.transform.localPosition = new Vector3(0f, -54.75f, 0f);
			Text2.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			this.LowerTextUgui = Text2.GetComponent<TextMeshProUGUI>();
			this.LowerTextUgui.text = LowerText;
			base.Text = text;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000609C File Offset: 0x0000429C
		public VRCLable(ButtonGroupControl grp, string text, string LowerText, Action onClick = null, bool Bg = false)
			: this(grp.GroupContents.transform, text, LowerText, onClick, Bg)
		{
		}

		// Token: 0x04000073 RID: 115
		public readonly VRCButton SButton;

		// Token: 0x04000074 RID: 116
		public readonly TextMeshProUGUI LowerTextUgui;
	}
}
