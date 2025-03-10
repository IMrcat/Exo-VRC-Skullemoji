using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CoreRuntime.Manager;
using EXO.Modules.Utilities;
using UnityEngine;

namespace EXO.Functions.MenuOverrides
{
	// Token: 0x020000A1 RID: 161
	public class HorizontalLayoutGroupAdjuster
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00022875 File Offset: 0x00020A75
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x0002287C File Offset: 0x00020A7C
		internal static RectTransform TooltipRect { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00022884 File Offset: 0x00020A84
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x0002288B File Offset: 0x00020A8B
		private static BoxCollider MenuCollider { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00022893 File Offset: 0x00020A93
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x0002289A File Offset: 0x00020A9A
		internal static Transform Transform { get; set; }

		// Token: 0x06000628 RID: 1576 RVA: 0x000228A2 File Offset: 0x00020AA2
		internal static void OnEnable()
		{
			CoroutineManager.RunCoroutine(HorizontalLayoutGroupAdjuster.RecalculateLayout());
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x000228AF File Offset: 0x00020AAF
		internal static void OnDisable()
		{
			CoroutineManager.RunCoroutine(HorizontalLayoutGroupAdjuster.RecalculateLayout());
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000228BC File Offset: 0x00020ABC
		public static IEnumerator RecalculateLayout()
		{
			bool flag;
			if (Config.cfg.TabExtension)
			{
				flag = Enumerable.Any<Assembly>(AppDomain.CurrentDomain.GetAssemblies(), (Assembly x) => TabExtender.assemblyNames.Contains(x.GetName().Name.ToLower().Replace(" ", "")));
			}
			else
			{
				flag = true;
			}
			bool flag2 = flag;
			if (flag2)
			{
				yield break;
			}
			bool flag3 = HorizontalLayoutGroupAdjuster.MenuCollider == null;
			int num;
			if (flag3)
			{
				for (int i = 0; i < 10; i = num + 1)
				{
					yield return null;
					num = i;
				}
				HorizontalLayoutGroupAdjuster.MenuCollider = TabExtender.quickMenu.FindObject("CanvasGroup/Container/Window/Page_Buttons_QM").GetComponent<BoxCollider>();
			}
			List<Transform> childs = new List<Transform>();
			for (int j = 0; j < HorizontalLayoutGroupAdjuster.Transform.childCount; j = num + 1)
			{
				Transform child = HorizontalLayoutGroupAdjuster.Transform.GetChild(j);
				bool flag4 = child.gameObject.activeSelf && child.gameObject.name != "Background_QM_PagePanel";
				if (flag4)
				{
					childs.Add(child);
				}
				child = null;
				num = j;
			}
			int pivotX = 0;
			for (int k = 0; k < childs.Count; k = num + 1)
			{
				int y = k / Config.cfg.TabsPerRow;
				int x2 = k - y * Config.cfg.TabsPerRow;
				bool flag5 = x2 == 0;
				if (flag5)
				{
					pivotX = -(((childs.Count - k >= Config.cfg.TabsPerRow) ? Config.cfg.TabsPerRow : (childs.Count - k)) * 64) + 64;
				}
				bool flag6 = childs.Count > k;
				if (flag6)
				{
					RectTransform rect = childs[k].transform.GetComponent<RectTransform>();
					rect.anchoredPosition = new Vector2((float)(pivotX + x2 * 128), (float)(-(float)(y * 128)));
					rect.pivot = new Vector2(0.5f, 1f);
					rect = null;
				}
				num = k;
			}
			HorizontalLayoutGroupAdjuster.MenuCollider.size = new Vector3(1100f, (float)(128 + (childs.Count - 1) / Config.cfg.TabsPerRow * 128), 1f);
			HorizontalLayoutGroupAdjuster.MenuCollider.center = new Vector3(0f, (float)(-64 - (childs.Count - 1) / Config.cfg.TabsPerRow * 64), 0f);
			HorizontalLayoutGroupAdjuster.TooltipRect.anchoredPosition = new Vector2(0f, (float)(-140 - (childs.Count - 1) / Config.cfg.TabsPerRow * 128 + 10));
			yield break;
		}
	}
}
