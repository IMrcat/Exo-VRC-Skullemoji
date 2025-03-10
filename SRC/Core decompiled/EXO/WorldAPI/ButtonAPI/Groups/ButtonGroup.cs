using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Extras;
using WorldAPI.ButtonAPI.QM.Controls;

namespace WorldAPI.ButtonAPI.Groups
{
	// Token: 0x02000020 RID: 32
	public class ButtonGroup : ButtonGroupControl
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00006D68 File Offset: 0x00004F68
		public ButtonGroup(Transform parent, string text, bool NoText = false, TextAnchor ButtonAlignment = TextAnchor.UpperCenter)
		{
			bool flag = !APIBase.IsReady();
			if (flag)
			{
				throw new NullReferenceException("Object Search had FAILED!");
			}
			base.WasNoText = NoText;
			bool flag2 = !NoText;
			if (flag2)
			{
				base.headerGameObject = Object.Instantiate<GameObject>(APIBase.ButtonGrpText, parent);
				base.TMProCompnt = base.headerGameObject.GetComponentInChildren<TextMeshProUGUI>(true);
				base.TMProCompnt.enableWordWrapping = false;
				base.TMProCompnt.overflowMode = TextOverflowModes.Overflow;
				base.headerGameObject.GetComponentInChildren<TextMeshProUGUI>().text = text;
				base.TMProCompnt.text = text;
				base.TMProCompnt.richText = true;
				base.Text = text;
			}
			base.gameObject = Object.Instantiate<GameObject>(APIBase.ButtonGrp, parent);
			base.gameObject.name = text;
			base.gameObject.transform.DestroyChildren(null);
			base.GroupContents = base.gameObject;
			base.transform = base.gameObject.transform;
			this.Layout = base.gameObject.GetComponent<GridLayoutGroup>();
			this.Layout.childAlignment = ButtonAlignment;
			base.parentMenuMask = parent.parent.GetComponent<RectMask2D>();
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006E9C File Offset: 0x0000509C
		public void ChangeChildAlignment(TextAnchor ButtonAlignment = TextAnchor.UpperCenter)
		{
			this.Layout.childAlignment = ButtonAlignment;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006EAB File Offset: 0x000050AB
		public ButtonGroup(WorldPage page, string text, bool NoText = false, TextAnchor ButtonAlignment = TextAnchor.UpperCenter)
			: this(page.MenuContents, text, NoText, ButtonAlignment)
		{
		}

		// Token: 0x04000080 RID: 128
		private readonly GridLayoutGroup Layout;
	}
}
