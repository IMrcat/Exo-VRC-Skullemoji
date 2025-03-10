using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI.ButtonAPI.Extras;
using WorldAPI.ButtonAPI.QM.Controls;

namespace WorldAPI.ButtonAPI.MM.Carousel.Items
{
	// Token: 0x02000026 RID: 38
	public class CGrp : WorldPage
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00007FA5 File Offset: 0x000061A5
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00007FAD File Offset: 0x000061AD
		public Toggle Togl { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00007FB6 File Offset: 0x000061B6
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00007FBE File Offset: 0x000061BE
		public bool IsOpen { get; private set; }

		// Token: 0x0600016F RID: 367 RVA: 0x00007FC8 File Offset: 0x000061C8
		public CGrp(CMenu menu, string text, bool expandable = true, bool defaultState = true)
		{
			bool flag = !APIBase.IsReady();
			if (flag)
			{
				throw new Exception();
			}
			base.transform = Object.Instantiate<GameObject>(APIBase.MMBtnGRP, menu.RootMenu.MenuContents).transform;
			base.transform.name = "BtnGrp_" + text;
			base.gameObject = base.transform.gameObject;
			(base.MenuContents = base.transform.Find("Settings_Panel_1/VerticalLayoutGroup")).DestroyChildren((Transform a) => a.name != "Background_Info");
			base.TMProCompnt = base.transform.Find("MM_Foldout/Label").GetComponent<TextMeshProUGUI>();
			base.TMProCompnt.text = text;
			base.TMProCompnt.richText = true;
			this.Togl = base.transform.Find("MM_Foldout/Background_Button").GetComponent<Toggle>();
			this.Togl.onValueChanged = new Toggle.ToggleEvent();
			this.Togl.isOn = defaultState;
			this.Togl.onValueChanged.AddListener(delegate(bool val)
			{
				base.MenuContents.gameObject.SetActive(val);
				this.IsOpen = val;
				Action onMenuOpen = base.OnMenuOpen;
				if (onMenuOpen != null)
				{
					onMenuOpen.Invoke();
				}
			});
			this.Togl.gameObject.active = expandable;
			menu.ChlidrenObjects.Add(base.transform.gameObject);
			base.MenuName = text;
		}
	}
}
