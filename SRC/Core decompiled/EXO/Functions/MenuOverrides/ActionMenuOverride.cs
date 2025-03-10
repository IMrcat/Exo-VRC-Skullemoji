using System;
using EXO.LogTools;
using EXO.Modules.Utilities;
using EXO_Base;
using UnityEngine;
using UnityEngine.UI;

namespace EXO.Functions.MenuOverrides
{
	// Token: 0x02000099 RID: 153
	internal class ActionMenuOverride
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x00020CA4 File Offset: 0x0001EEA4
		internal static void Init()
		{
			UtilFunc.WaitForObj("UserInterface/UnscaledUI/ActionMenu/Container/MenuR/ActionMenu/Main", delegate
			{
				ActionMenuOverride.ApplyOverride(GameObject.Find("UserInterface/UnscaledUI/ActionMenu/Container/MenuR/ActionMenu/Main"));
			});
			UtilFunc.WaitForObj("UserInterface/UnscaledUI/ActionMenu/Container/MenuL/ActionMenu/Main", delegate
			{
				ActionMenuOverride.ApplyOverride(GameObject.Find("UserInterface/UnscaledUI/ActionMenu/Container/MenuL/ActionMenu/Main"));
			});
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00020D08 File Offset: 0x0001EF08
		private static void ApplyOverride(GameObject actionMenu)
		{
			try
			{
				PedalGraphic pedgraphic = actionMenu.FindObject("Center").GetComponent<PedalGraphic>();
				pedgraphic.field_Public_Single_3 = 360f;
				pedgraphic.color = Color.black;
			}
			catch (Exception e)
			{
				CLog.E("Center Error", e);
			}
			try
			{
				actionMenu.FindObject("Background").GetComponent<PedalGraphic>().color = Color.black;
			}
			catch (Exception e2)
			{
				CLog.E("Background Error", e2);
			}
			try
			{
				actionMenu.FindObject("Cursor").GetComponent<Image>().overrideSprite = BaseImages.FromBase(BaseImages.JoyStickIcon);
			}
			catch (Exception e3)
			{
				CLog.E("Cursor Error", e3);
			}
		}
	}
}
