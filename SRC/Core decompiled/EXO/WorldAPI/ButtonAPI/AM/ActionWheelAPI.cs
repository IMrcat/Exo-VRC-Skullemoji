using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EXO.LogTools;
using EXO.Modules.Utilities;
using Il2CppInterop.Runtime;
using Il2CppSystem;
using VRC.Localization;

namespace WorldAPI.ButtonAPI.AM
{
	// Token: 0x0200002C RID: 44
	public static class ActionWheelAPI
	{
		// Token: 0x0600018E RID: 398 RVA: 0x000089AC File Offset: 0x00006BAC
		public static bool IsOpen(this ActionMenuOpener actionMenuOpener)
		{
			return actionMenuOpener.field_Private_Boolean_0;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000089C4 File Offset: 0x00006BC4
		[NullableContext(2)]
		public static ActionMenuOpener GetActionMenuOpener()
		{
			ActionMenuOpener left = ActionMenuController.field_Public_Static_ActionMenuController_0.field_Public_ActionMenuOpener_0;
			ActionMenuOpener right = ActionMenuController.field_Public_Static_ActionMenuController_0.field_Public_ActionMenuOpener_1;
			return (left.IsOpen() && !right.IsOpen()) ? left : (right.IsOpen() ? right : null);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008A0C File Offset: 0x00006C0C
		public static void OpenMainPage(ActionMenu menu)
		{
			UtilFunc.Delay(1f, delegate
			{
				bool flag = menu == null;
				if (flag)
				{
					CLog.E("menu is null", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\WorldAPI\\ButtonAPI\\AM\\ActionWheelAPI.cs", 32);
				}
				else
				{
					ActionWheelAPI.activeActionMenu = menu;
					bool flag2 = ActionWheelAPI.mainMenuButtons == null;
					if (flag2)
					{
						CLog.E("mainMenuButtons is null", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\WorldAPI\\ButtonAPI\\AM\\ActionWheelAPI.cs", 40);
					}
					else
					{
						int i = 0;
						foreach (ActionMenuButton actionMenuButton in ActionWheelAPI.mainMenuButtons)
						{
							i++;
							bool flag3 = actionMenuButton == null;
							if (flag3)
							{
								CLog.E("actionMenuButton is null on iteration " + i.ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\WorldAPI\\ButtonAPI\\AM\\ActionWheelAPI.cs", 50);
							}
							else
							{
								bool bFail = false;
								bool flag4 = ActionWheelAPI.activeActionMenu.field_Private_List_1_PedalOption_0 == null;
								if (flag4)
								{
									CLog.E("actionMenuButton is null on iteration " + i.ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\WorldAPI\\ButtonAPI\\AM\\ActionWheelAPI.cs", 58);
								}
								else
								{
									foreach (PedalOption existingPedalOption in ActionWheelAPI.activeActionMenu.field_Private_List_1_PedalOption_0)
									{
										bool flag5 = existingPedalOption == null || existingPedalOption.prop_LocalizableString_0 == null;
										if (flag5)
										{
											CLog.E("actionMenuButton is null", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\WorldAPI\\ButtonAPI\\AM\\ActionWheelAPI.cs", 66);
										}
										else
										{
											bool flag6 = existingPedalOption.prop_LocalizableString_0.FallbackText == actionMenuButton.buttonText;
											if (flag6)
											{
												actionMenuButton.currentPedalOption = existingPedalOption;
												CLog.L("out early", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\WorldAPI\\ButtonAPI\\AM\\ActionWheelAPI.cs", 73);
												return;
											}
											string fallbackText = existingPedalOption.prop_LocalizableString_0.FallbackText;
											bool flag7 = fallbackText == "Emojis" || fallbackText == "Tools";
											bool flag8 = flag7;
											if (flag8)
											{
												CLog.L("is Emojis or Tools", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\WorldAPI\\ButtonAPI\\AM\\ActionWheelAPI.cs", 79);
												bFail = true;
											}
										}
									}
									bool flag9 = !bFail;
									if (flag9)
									{
										CLog.L("Intentional return instead of continue.", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\WorldAPI\\ButtonAPI\\AM\\ActionWheelAPI.cs", 86);
										break;
									}
									ActionMenuOpener actionMenuOpener = ActionWheelAPI.GetActionMenuOpener();
									PedalOption pedalOption;
									if (actionMenuOpener == null)
									{
										pedalOption = null;
									}
									else
									{
										ActionMenu field_Public_ActionMenu_ = actionMenuOpener.field_Public_ActionMenu_0;
										pedalOption = ((field_Public_ActionMenu_ != null) ? field_Public_ActionMenu_.Method_Public_PedalOption_0() : null);
									}
									PedalOption newPedalOption = pedalOption;
									bool flag10 = newPedalOption == null;
									if (flag10)
									{
										CLog.E("newPedalOption is null", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\WorldAPI\\ButtonAPI\\AM\\ActionWheelAPI.cs", 95);
									}
									else
									{
										newPedalOption.prop_LocalizableString_0 = actionMenuButton.buttonText.Localize(null, null, null);
										newPedalOption.field_Public_Func_1_Boolean_0 = DelegateSupport.ConvertDelegate<Func<bool>>(actionMenuButton.buttonAction);
										bool flag11 = actionMenuButton.buttonIcon != null;
										if (flag11)
										{
											CLog.L("actionMenuButton.buttonIcon exists", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\WorldAPI\\ButtonAPI\\AM\\ActionWheelAPI.cs", 104);
											newPedalOption.Method_Public_Virtual_Final_New_Void_Texture2D_0(actionMenuButton.buttonIcon);
										}
										actionMenuButton.currentPedalOption = newPedalOption;
									}
								}
							}
						}
					}
				}
			});
		}

		// Token: 0x040000B1 RID: 177
		public static readonly List<ActionMenuButton> mainMenuButtons = new List<ActionMenuButton>();

		// Token: 0x040000B2 RID: 178
		[Nullable(2)]
		public static ActionMenu activeActionMenu;
	}
}
