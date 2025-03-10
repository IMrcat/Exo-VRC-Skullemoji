using System;
using System.Text;
using EXO.Core;
using EXO.Modules.Utilities;
using EXO.Patches;
using EXO.Wrappers;
using Microsoft.Win32;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EXO.Functions.MenuOverrides
{
	// Token: 0x0200009A RID: 154
	internal class ColorPaletteOverride
	{
		// Token: 0x060005FC RID: 1532 RVA: 0x00020DF0 File Offset: 0x0001EFF0
		private static void MakePalette()
		{
			try
			{
				using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\VRChat\\VRChat", true))
				{
					string exoval = "{\"name\":\"exo\",\"id\":\"pal_00005\",\"highlights\":\"#9E0000\",\"icons\":\"#FFFFFF\",\"buttons\":\"#0A0A0A\",\"backgrounds\":\"#000000\",\"text\":\"#FFFFFF\",\"subtext\":\"#FF0000\"}";
					byte[] specifiedValue = Encoding.UTF8.GetBytes(exoval);
					key.SetValue("COLOR_PALETTES_CURRENT_" + PlayerWrapper.LocalAPIUser.id + "_exo", specifiedValue, 3);
					MenuCore.MenuLogs("Made Palette exo");
				}
			}
			catch (Exception ex)
			{
				MenuCore.MenuLogs("Error while creating palette: " + ex.Message);
			}
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00020E98 File Offset: 0x0001F098
		internal static void Palettes()
		{
			using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\VRChat\\VRChat", true))
			{
				foreach (string valueName in key.GetValueNames())
				{
					bool flag = !valueName.Contains("COLOR_PALETTES_" + PlayerWrapper.LocalAPIUser.id) || !valueName.Contains(PlayerWrapper.LocalAPIUser.id);
					if (!flag)
					{
						string stringValue = Encoding.UTF8.GetString(key.GetValue(valueName) as byte[]);
						string skip = stringValue.Substring(stringValue.IndexOf(':'));
						string name = skip.Substring(2, skip.IndexOf(',') - 3);
						key.SetValue(valueName, valueName, 3);
						break;
					}
				}
			}
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00020F7C File Offset: 0x0001F17C
		internal static void InitPalettes()
		{
			Transform mmpar = UtilFunc.UserInterface.FindObject("Canvas_MainMenu(Clone)/Container/MMParent");
			bool isSupporter = PlayerWrapper.LocalAPIUser.isSupporter;
			if (!isSupporter)
			{
				using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\VRChat\\VRChat", true))
				{
					string[] valueNames = key.GetValueNames();
					for (int i = 0; i < valueNames.Length; i++)
					{
						string valueName = valueNames[i];
						bool flag = !valueName.Contains("COLOR_PALETTES_CURRENT_" + PlayerWrapper.LocalAPIUser.id) || !valueName.Contains(PlayerWrapper.LocalAPIUser.id);
						if (!flag)
						{
							string stringValue = Encoding.UTF8.GetString(key.GetValue(valueName) as byte[]);
							string skip = stringValue.Substring(stringValue.IndexOf(':'));
							string name = skip.Substring(2, skip.IndexOf(',') - 3);
							MenuCore.MenuLogs("Enabling Color Palette " + name + "...");
							ColorPaletteOverride.applyAction = (Action)Delegate.Combine(ColorPaletteOverride.applyAction, delegate
							{
								MenuCore.MenuLogs("EXO Color Palette\n\n#9E0000,#FFFFFF,#0A0A0A,#000000,#FFFFFF,#FF0000\n");
								MenuCore.MenuLogs("Applying " + name + "...");
								Transform menuObj = mmpar.FindObject("Menu_VRChat+");
								menuObj.SetActive(true);
								MonoBehaviour1PublicTrVoGaVoOb01TVo89Unique objComp = mmpar.FindObject("Menu_VRChat+/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/Page_MM_UIColorPalettes");
								objComp.Method_Protected_Virtual_Void_0();
								objComp.SetActive(true);
								UtilFunc.UpdateClamp(0.3f, delegate
								{
									string palettePath = "Menu_VRChat+/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/Page_MM_UIColorPalettes";
									Transform paletteObj = mmpar.Find(palettePath);
									foreach (GameObject child in paletteObj.GetChildrenAsList())
									{
										bool flag2 = child.name != "Cell_MM_Gallery_UIColorPalette(Clone)";
										if (!flag2)
										{
											bool flag3 = child.FindObject("Selected Outline/Bottom Border/Text (TMP)").text != name;
											if (!flag3)
											{
												child.GetComponent<Button>().Press();
												break;
											}
										}
									}
									objComp.Method_Protected_Virtual_Void_0();
									objComp.SetActive(true);
								});
								QuickMenuPatch.OnQuickMenuOpen = (Action)Delegate.Remove(QuickMenuPatch.OnQuickMenuOpen, ColorPaletteOverride.applyAction);
							});
							break;
						}
					}
					QuickMenuPatch.OnQuickMenuOpen = (Action)Delegate.Combine(QuickMenuPatch.OnQuickMenuOpen, ColorPaletteOverride.applyAction);
				}
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00021108 File Offset: 0x0001F308
		internal static void EnableVRCPlus()
		{
			bool isSupporter = PlayerWrapper.LocalAPIUser.isSupporter;
			if (!isSupporter)
			{
				UtilFunc.Delay(0.1f, delegate
				{
					GameObject vrcPlus = GameObject.Find("Canvas_MainMenu(Clone)/Container/MMParent/Menu_VRChat+/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup");
					vrcPlus.transform.Find("Page_MM_VRChatPlus_Account").gameObject.SetActive(false);
					vrcPlus.transform.Find("Page_MM_Backgrounds").gameObject.SetActive(true);
					vrcPlus.transform.Find("Page_MM_UIColorPalettes").gameObject.SetActive(true);
					GameObject.Find("Canvas_MainMenu(Clone)/Container/MMParent/Menu_VRChat+/Menu_MM_DynamicSidePanel/Panel_SectionList/DynamicSidePanel_Header/TitleContainer").SetActive(false);
				});
			}
		}

		// Token: 0x040002BF RID: 703
		private static Action applyAction;
	}
}
