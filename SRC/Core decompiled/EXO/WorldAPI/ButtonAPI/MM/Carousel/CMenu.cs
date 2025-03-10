using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.Localization;
using VRC.UI.Elements.Controls;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Extras;

namespace WorldAPI.ButtonAPI.MM.Carousel
{
	// Token: 0x02000025 RID: 37
	public class CMenu : Root
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00007D3F File Offset: 0x00005F3F
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00007D47 File Offset: 0x00005F47
		internal List<GameObject> ChlidrenObjects { get; set; } = new List<GameObject>();

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00007D50 File Offset: 0x00005F50
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00007D58 File Offset: 0x00005F58
		public Action OnClick { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00007D61 File Offset: 0x00005F61
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00007D69 File Offset: 0x00005F69
		public Button Button { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00007D72 File Offset: 0x00005F72
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00007D7A File Offset: 0x00005F7A
		public Image ImageComp { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00007D83 File Offset: 0x00005F83
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00007D8B File Offset: 0x00005F8B
		public MMCarousel RootMenu { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00007D94 File Offset: 0x00005F94
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00007D9C File Offset: 0x00005F9C
		public ToolTip ToolTip { get; private set; }

		// Token: 0x06000168 RID: 360 RVA: 0x00007DA8 File Offset: 0x00005FA8
		public CMenu(Transform transform, Action onClick, string buttonText, string toolTip = "", Sprite Icon = null)
		{
			bool flag = !APIBase.IsReady();
			if (flag)
			{
				throw new Exception();
			}
			bool flag2 = Icon == null;
			if (flag2)
			{
				Icon = APIBase.DefaultButtonSprite;
			}
			transform = (base.gameObject = Object.Instantiate<GameObject>(APIBase.MMMCarouselButtonTemplate, transform)).transform;
			base.gameObject.name = buttonText;
			base.TMProCompnt = base.gameObject.transform.Find("Mask/Text_Name").GetComponent<TextMeshProUGUI>();
			base.TMProCompnt.text = buttonText;
			base.TMProCompnt.richText = true;
			bool flag3 = onClick != null;
			if (flag3)
			{
				this.OnClick = (Action)Delegate.Combine(this.OnClick, onClick);
			}
			((this.Button = base.gameObject.GetComponent<Button>()).onClick = new Button.ButtonClickedEvent()).AddListener(delegate
			{
				Action onClick2 = this.OnClick;
				if (onClick2 != null)
				{
					onClick2.Invoke();
				}
			});
			this.ImageComp = base.gameObject.transform.Find("Icon").GetComponent<Image>();
			bool flag4 = Icon != null;
			if (flag4)
			{
				this.ImageComp.sprite = Icon;
			}
			else
			{
				this.ImageComp.gameObject.SetActive(false);
			}
			(this.ToolTip = base.gameObject.GetComponent<ToolTip>())._localizableString = toolTip.Localize(null, null, null);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00007F28 File Offset: 0x00006128
		public CMenu(MMCarousel ph, string buttonText, string toolTip = "", Sprite Icon = null)
			: this(ph.BarContents, null, buttonText, toolTip, Icon)
		{
			CMenu <>4__this = this;
			this.OnClick = (Action)Delegate.Combine(this.OnClick, delegate
			{
				ph.MenuContents.GetChildren().ForEach(delegate(GameObject a)
				{
					a.SetActive(false);
				});
				<>4__this.ChlidrenObjects.ForEach(delegate(GameObject a)
				{
					a.SetActive(true);
				});
			});
			this.RootMenu = ph;
		}
	}
}
