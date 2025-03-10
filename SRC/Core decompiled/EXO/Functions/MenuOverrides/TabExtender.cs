using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CoreRuntime.Manager;
using EXO.Core;
using EXO.LogTools;
using EXO.Modules.Utilities;
using EXO.Patches;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI;

namespace EXO.Functions.MenuOverrides
{
	// Token: 0x020000A0 RID: 160
	internal class TabExtender : FunctionModule
	{
		// Token: 0x0600061D RID: 1565 RVA: 0x00022710 File Offset: 0x00020910
		internal static void OnMenuBuilt()
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
			if (!flag2)
			{
				try
				{
					Delegate onQuickMenuOpen = QuickMenuPatch.OnQuickMenuOpen;
					Action action;
					if ((action = TabExtender.<>O.<0>__RecalculateLayout) == null)
					{
						action = (TabExtender.<>O.<0>__RecalculateLayout = new Action(TabExtender.RecalculateLayout));
					}
					QuickMenuPatch.OnQuickMenuOpen = (Action)Delegate.Combine(onQuickMenuOpen, action);
					Delegate onQuickMenuClose = QuickMenuPatch.OnQuickMenuClose;
					Action action2;
					if ((action2 = TabExtender.<>O.<0>__RecalculateLayout) == null)
					{
						action2 = (TabExtender.<>O.<0>__RecalculateLayout = new Action(TabExtender.RecalculateLayout));
					}
					QuickMenuPatch.OnQuickMenuClose = (Action)Delegate.Combine(onQuickMenuClose, action2);
				}
				catch (Exception e)
				{
					CLog.E("Failed to add QM Listener Menu Build", e);
				}
				CoroutineManager.RunCoroutine(TabExtender.Init());
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x000227F8 File Offset: 0x000209F8
		internal static IEnumerator Init()
		{
			while (APIBase.QuickMenu.FindObject("CanvasGroup/Container/Window/QMParent") == null)
			{
				yield return null;
			}
			try
			{
				TabExtender.quickMenu = APIBase.QuickMenu.transform;
				TabExtender.pageButtonsQM = TabExtender.quickMenu.FindObject("CanvasGroup/Container/Window/Page_Buttons_QM");
				TabExtender.layout = TabExtender.quickMenu.FindObject("CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup");
				Object.Destroy(TabExtender.layout.GetComponent<HorizontalLayoutGroup>());
				HorizontalLayoutGroupAdjuster.Transform = TabExtender.layout;
				HorizontalLayoutGroupAdjuster.TooltipRect = TabExtender.quickMenu.FindObject("CanvasGroup/Container/Window/ToolTipPanel").GetComponent<RectTransform>();
				try
				{
					Delegate onQuickMenuOpen = QuickMenuPatch.OnQuickMenuOpen;
					Action action;
					if ((action = TabExtender.<>O.<1>__OnEnable) == null)
					{
						action = (TabExtender.<>O.<1>__OnEnable = new Action(HorizontalLayoutGroupAdjuster.OnEnable));
					}
					QuickMenuPatch.OnQuickMenuOpen = (Action)Delegate.Combine(onQuickMenuOpen, action);
					Delegate onQuickMenuClose = QuickMenuPatch.OnQuickMenuClose;
					Action action2;
					if ((action2 = TabExtender.<>O.<2>__OnDisable) == null)
					{
						action2 = (TabExtender.<>O.<2>__OnDisable = new Action(HorizontalLayoutGroupAdjuster.OnDisable));
					}
					QuickMenuPatch.OnQuickMenuClose = (Action)Delegate.Combine(onQuickMenuClose, action2);
				}
				catch (Exception ex)
				{
					Exception e = ex;
					CLog.E("Failed to add QM Listener Init", e);
				}
			}
			catch (Exception ex)
			{
				Exception e2 = ex;
				CLog.E("Error At Tab Extender..", e2);
			}
			CLog.L("Loading TabExtender...", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\MenuOverrides\\TabExtender.cs", 61);
			Transform newBG = Object.Instantiate<Transform>(TabExtender.pageButtonsQM.FindObject("HorizontalLayoutGroup/Page_Here/Background"), TabExtender.pageButtonsQM);
			newBG.SetAsFirstSibling();
			Image image = newBG.GetComponent<Image>();
			image.sprite = UtilFunc.GeVRCSprite("Page_Tab_Backdrop_hover");
			image.color = new Color(0.0667f, 0.0667f, 0.0667f, 0.9f);
			yield break;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00022800 File Offset: 0x00020A00
		internal static void RecalculateLayout()
		{
			HorizontalLayoutGroupAdjuster.OnEnable();
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00022814 File Offset: 0x00020A14
		// Note: this type is marked as 'beforefieldinit'.
		unsafe static TabExtender()
		{
			int num = 3;
			List<string> list = new List<string>(num);
			CollectionsMarshal.SetCount<string>(list, num);
			Span<string> span = CollectionsMarshal.AsSpan<string>(list);
			int num2 = 0;
			*span[num2] = "tabextension";
			num2++;
			*span[num2] = "tabextend";
			num2++;
			*span[num2] = "wcv2";
			num2++;
			TabExtender.assemblyNames = list;
		}

		// Token: 0x040002DB RID: 731
		internal static Transform quickMenu;

		// Token: 0x040002DC RID: 732
		internal static Transform layout;

		// Token: 0x040002DD RID: 733
		internal static Transform background;

		// Token: 0x040002DE RID: 734
		internal static Transform pageButtonsQM;

		// Token: 0x040002DF RID: 735
		internal static readonly List<string> assemblyNames;

		// Token: 0x02000192 RID: 402
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040006FB RID: 1787
			public static Action <0>__RecalculateLayout;

			// Token: 0x040006FC RID: 1788
			public static Action <1>__OnEnable;

			// Token: 0x040006FD RID: 1789
			public static Action <2>__OnDisable;
		}
	}
}
