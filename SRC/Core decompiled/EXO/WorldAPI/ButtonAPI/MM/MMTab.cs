using System;
using UnityEngine;
using UnityEngine.UI;
using VRC.Localization;
using VRC.UI.Core.Styles;
using VRC.UI.Elements.Controls;
using WorldAPI.ButtonAPI.Controls;

namespace WorldAPI.ButtonAPI.MM
{
	// Token: 0x02000024 RID: 36
	public class MMTab : Root
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00007B77 File Offset: 0x00005D77
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00007B7E File Offset: 0x00005D7E
		public static Action OnClick { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00007B86 File Offset: 0x00005D86
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00007B8E File Offset: 0x00005D8E
		public Image Image { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00007B97 File Offset: 0x00005D97
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00007B9F File Offset: 0x00005D9F
		public ToolTip ToolTip { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00007BA8 File Offset: 0x00005DA8
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00007BB0 File Offset: 0x00005DB0
		public Button MenuTab { get; private set; }

		// Token: 0x06000158 RID: 344 RVA: 0x00007BBC File Offset: 0x00005DBC
		private void Make(Action method, string toolTip, Sprite sprite)
		{
			bool flag = !APIBase.IsReady();
			if (flag)
			{
				throw new NullReferenceException("Object Search had FAILED!");
			}
			base.gameObject = Object.Instantiate<GameObject>(APIBase.MMMTabTemplate, APIBase.MMMTabTemplate.transform.parent);
			bool flag2 = sprite != null;
			if (flag2)
			{
				(this.Image = base.gameObject.transform.Find("Icon").GetComponent<Image>()).sprite = sprite;
			}
			else
			{
				base.gameObject.transform.Find("Icon").gameObject.active = false;
			}
			base.gameObject.GetComponent<StyleElement>().field_Private_Selectable_0 = base.gameObject.GetComponent<Button>();
			base.gameObject.GetComponent<Button>().onClick.AddListener(delegate
			{
				base.gameObject.SetActive(true);
			});
			(this.ToolTip = base.gameObject.GetComponent<ToolTip>())._localizableString = toolTip.Localize(null, null, null);
			((this.MenuTab = base.gameObject.GetComponent<Button>()).onClick = new Button.ButtonClickedEvent()).AddListener(method);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00007CF4 File Offset: 0x00005EF4
		public MMTab(MMPage page, string toolTip = "", Sprite sprite = null)
		{
			this.Make(new Action(page.OpenMenu), toolTip, sprite);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00007D12 File Offset: 0x00005F12
		public MMTab(MMCarousel page, string toolTip = "", Sprite sprite = null)
		{
			this.Make(new Action(page.OpenMenu), toolTip, sprite);
		}
	}
}
