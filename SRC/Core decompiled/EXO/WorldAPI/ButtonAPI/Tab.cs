using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements.Controls;
using WorldAPI.ButtonAPI.Controls;

namespace WorldAPI.ButtonAPI
{
	// Token: 0x0200000B RID: 11
	public class Tab : ExtentedControl
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002D87 File Offset: 0x00000F87
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002D8F File Offset: 0x00000F8F
		public GameObject badgeGameObject { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002D98 File Offset: 0x00000F98
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002DA0 File Offset: 0x00000FA0
		public TextMeshProUGUI badgeText { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002DA9 File Offset: 0x00000FA9
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002DB1 File Offset: 0x00000FB1
		public Image tabIcon { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002DBA File Offset: 0x00000FBA
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002DC2 File Offset: 0x00000FC2
		public Button Button { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002DCB File Offset: 0x00000FCB
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002DD3 File Offset: 0x00000FD3
		public VRCPage Menu { get; private set; }

		// Token: 0x0600002C RID: 44 RVA: 0x00002DDC File Offset: 0x00000FDC
		public Tab(VRCPage menu, string tooltip, Sprite icon = null, Transform parent = null, bool openMenu = true)
		{
			Tab <>4__this = this;
			bool flag = !APIBase.IsReady();
			if (flag)
			{
				throw new Exception();
			}
			bool flag2 = parent == null;
			if (flag2)
			{
				parent = APIBase.Tab.parent;
			}
			this.Menu = menu;
			(base.gameObject = Object.Instantiate<GameObject>(APIBase.Tab.gameObject, parent)).name = menu.MenuName + "_Tab";
			(this.tabIcon = base.gameObject.transform.Find("Icon").GetComponent<Image>()).overrideSprite = icon;
			(this.element = base.gameObject.GetComponent<StyleElement>()).field_Private_Selectable_0 = (this.Button = base.gameObject.GetComponent<Button>());
			(this.Button.onClick = new Button.ButtonClickedEvent()).AddListener(delegate
			{
				<>4__this.gameObject.GetComponent<StyleElement>().field_Private_Selectable_0 = <>4__this.Button;
				bool openMenu2 = openMenu;
				if (openMenu2)
				{
					VRCPage menu2 = <>4__this.Menu;
					if (menu2 != null)
					{
						menu2.OpenMenu();
					}
				}
				Action onClickAction = <>4__this.onClickAction;
				if (onClickAction != null)
				{
					onClickAction.Invoke();
				}
			});
			this.Button.GetComponent<MenuTab>()._controlName = menu.MenuName;
			this.badgeGameObject = base.gameObject.transform.GetChild(0).gameObject;
			this.badgeText = this.badgeGameObject.GetComponentInChildren<TextMeshProUGUI>();
			this.SetToolTip(tooltip);
			base.gameObject.active = true;
		}

		// Token: 0x04000028 RID: 40
		private StyleElement element;
	}
}
