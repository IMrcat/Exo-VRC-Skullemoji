using System;
using TMPro;
using UnityEngine;
using WorldAPI.ButtonAPI.Controls;

namespace WorldAPI.ButtonAPI.QM.Carousel.Items
{
	// Token: 0x02000018 RID: 24
	public class QMCTitle : Root
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00005B9C File Offset: 0x00003D9C
		public QMCTitle(Transform parent, string text, bool separator = false)
		{
			bool flag = !APIBase.IsReady();
			if (flag)
			{
				throw new NullReferenceException("Object Search had FAILED!");
			}
			base.transform = Object.Instantiate<GameObject>(APIBase.QMCarouselTitleTemplate, parent).transform;
			base.gameObject = base.transform.gameObject;
			base.gameObject.name = text;
			base.TMProCompnt = base.transform.Find("LeftItemContainer/Text_MM_H3").GetComponent<TextMeshProUGUI>();
			base.TMProCompnt.text = text;
			base.TMProCompnt.richText = true;
			if (separator)
			{
				GameObject seB = Object.Instantiate<GameObject>(APIBase.QMCarouselSeparator, parent);
				seB.name = "Separator";
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005C53 File Offset: 0x00003E53
		public QMCTitle(QMCGroup group, string text, bool separator = false)
			: this(group.GetTransform().Find("QM_Settings_Panel/VerticalLayoutGroup"), text, separator)
		{
		}
	}
}
