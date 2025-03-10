using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using CoreRuntime.Manager;
using EXO.Core;
using EXO.Functions.Avatar;
using EXO.Functions.MenuOverrides;
using EXO.LogTools;
using EXO.Modules.API;
using EXO.Modules.Utilities;
using EXO.Patches;
using EXO.Wrappers;
using EXO_Base;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC;
using VRC.Core;
using VRC.Localization;
using VRC.UI.Elements.Controls;
using VRC.UI.Elements.Menus;
using WorldAPI;
using WorldAPI.ButtonAPI.Extras;

namespace EXO.Menus.BigMenu
{
	// Token: 0x0200006A RID: 106
	internal class AvatarFavMenu : MenuModule
	{
		// Token: 0x06000391 RID: 913 RVA: 0x00014017 File Offset: 0x00012217
		public override void LoadMenu()
		{
			AvatarFavMenu.AddButtonListener("Canvas_MainMenu(Clone)/Container/PageButtons/HorizontalLayoutGroup/Page_Avatars", AvatarFavMenu.OnOpen);
			AvatarFavMenu.AddButtonListener("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/Button_Avatars", AvatarFavMenu.OnOpen);
			AviFavs.Init();
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00014040 File Offset: 0x00012240
		[Nullable(2)]
		private static AvatarContentSection AvatarContentSection
		{
			[NullableContext(2)]
			get
			{
				object obj;
				if ((obj = AvatarFavMenu._avatarContent) == null)
				{
					obj = (AvatarFavMenu._avatarContent = GameObject.Find("Container/MMParent/Menu_Avatars/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/Cell_MM_AvatarContentSection").GetComponent<AvatarContentSection>());
				}
				return obj as AvatarContentSection;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00014065 File Offset: 0x00012265
		[Nullable(2)]
		private static TMP_Text Header
		{
			[NullableContext(2)]
			get
			{
				object obj;
				if ((obj = AvatarFavMenu._header) == null)
				{
					obj = (AvatarFavMenu._header = APIBase.MMM.FindObject("Container/MMParent/Menu_Avatars/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Header_MM_H2/LeftItemContainer/Text_Title").GetComponent<TMP_Text>());
				}
				return obj as TMP_Text;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0001408F File Offset: 0x0001228F
		[Nullable(2)]
		private static Transform FavBtn
		{
			[NullableContext(2)]
			get
			{
				object obj;
				if ((obj = AvatarFavMenu._favBtn) == null)
				{
					obj = (AvatarFavMenu._favBtn = APIBase.MMM.FindObject("Container/MMParent/Menu_Avatars/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Panel_SelectedAvatar/ScrollRect/Viewport/VerticalLayoutGroup/Button_AvatarDetails"));
				}
				return obj as Transform;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000395 RID: 917 RVA: 0x000140B4 File Offset: 0x000122B4
		[Nullable(2)]
		private static Transform Banner
		{
			[NullableContext(2)]
			get
			{
				object obj;
				if ((obj = AvatarFavMenu._banner) == null)
				{
					obj = (AvatarFavMenu._banner = APIBase.MMM.FindObject("Container/MMParent/Menu_Avatars/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/VRC+ Upsell/Image"));
				}
				return obj as Transform;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000396 RID: 918 RVA: 0x000140D9 File Offset: 0x000122D9
		private static bool InFavMenu
		{
			get
			{
				return Enumerable.Any<string>(AvatarFavMenu.Menus, new Func<string, bool>(AvatarFavMenu.Header.text.Contains));
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000140FA File Offset: 0x000122FA
		private static void Clear()
		{
			AvatarFavMenu.AvatarContentSection.field_Private_Object1PublicTYBoTYUnique_1_IList_1.field_Protected_TYPE_0.Clear();
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00014111 File Offset: 0x00012311
		private static void DisplayAvatar(ApiAvatar avtr)
		{
			AvatarFavMenu.AvatarContentSection.field_Private_Object1PublicTYBoTYUnique_1_IList_1.field_Protected_TYPE_0.Add(new Object1PublicOb1BoObStBoLiStDaBoUnique
			{
				field_Protected_TYPE_0 = avtr
			});
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00014138 File Offset: 0x00012338
		private static void UpdateFavBtnText(ApiAvatar avtr)
		{
			AvatarFavMenu.FavBtn.Find("Text_ButtonName").GetComponent<TMP_Text>().text = (Enumerable.Any<AvatarObject>(AvatarFavMenu.Data.FavAvis, (AvatarObject x) => x.id == avtr.id) ? "Unfavorite Avatar" : "Favorite Avatar");
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00014190 File Offset: 0x00012390
		private static void AddButtonListener(string path, Action action)
		{
			UtilFunc.UserInterface.FindObject(path).GetComponent<Button>().onClick.AddListener(action);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000141B3 File Offset: 0x000123B3
		private static void SetDropdownValue(string path, int value)
		{
			APIBase.MMM.FindObject(path).GetComponent<TMP_Dropdown>().SetValue(value, true);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000141D0 File Offset: 0x000123D0
		private static void SetButtonProperties(Transform objToOverride, Action click, string text, string tooltip)
		{
			objToOverride.Find("Mask/Text_Name").GetComponent<TextMeshProUGUI>().text = text;
			objToOverride.FindObject("Icon").GetComponent<Image>().sprite = BaseImages.FromBase(BaseImages.IconEXO);
			objToOverride.GetComponent<ToolTip>()._localizableString = tooltip.Localize(null, null, null);
			objToOverride.GetComponent<Button>().onClick.AddListener(click);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00014244 File Offset: 0x00012444
		private static void SetFavBtnIcon()
		{
			try
			{
				AvatarFavMenu.FavBtn.Find("Text_ButtonName/Icon").GetComponent<Image>().overrideSprite = BaseImages.FromBase(BaseImages.IconEXO);
			}
			catch
			{
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00014290 File Offset: 0x00012490
		private static void MakeBanner()
		{
			AvatarFavMenu.Banner.GetOrAddComponent<Button>().onClick.AddListener(new Action(ColorPaletteOverride.EnableVRCPlus));
			AvatarFavMenu.Banner.GetComponent<Image>().overrideSprite = BaseImages.FromBase(BaseImages.EXOBanner);
			AvatarFavMenu.Banner.GetComponent<ToolTip>()._localizableString = "EXO VRC Plus".ConvertToLocalized();
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000142F8 File Offset: 0x000124F8
		private static void MakeFavBtn()
		{
			Button favBtnComponent = AvatarFavMenu.FavBtn.GetComponent<Button>();
			favBtnComponent.onClick.RemoveAllListeners();
			favBtnComponent.onClick.AddListener(delegate
			{
				ApiAvatar ped = APIBase.MMM.FindObject("Container/MMParent/Menu_Avatars/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Panel_SelectedAvatar/Panel_MM_AvatarViewer/Avatar").GetComponent<SimpleAvatarPedestal>().field_Internal_ApiAvatar_0;
				AvatarFavMenu.AddOrRemoveAvatar(ped);
				AvatarFavMenu.UpdateFavBtnText(ped);
				AvatarFavMenu.SetDropdownValue("Container/MMParent/Menu_Avatars/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Header_MM_H2/RightItemContainer/Field_MM_SortBy", 0);
				AvatarFavMenu.PopulateList(AvatarFavMenu.Data.FavAvis);
			});
			AvatarFavMenu.UpdateFavBtnText(PlayerWrapper.LocalPlayer.GetAPIAvatar());
			AvatarFavMenu.SetFavBtnIcon();
			AvatarFavMenu.FavBtn.GetComponent<ToolTip>()._localizableString = "EXO Avatar Favorites".ConvertToLocalized();
			CoroutineManager.RunCoroutine(AvatarFavMenu.FixToolTip());
			CoroutineManager.RunCoroutine(AvatarFavMenu.CheckAvatar());
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00014394 File Offset: 0x00012594
		private static void MakeBtn(Transform objToOverride, Action click, string text, string header, string tooltip, int siblingIndex = 3)
		{
			Transform par = APIBase.MMM.FindObject("Container/MMParent/Menu_Avatars/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Navigation/Viewport/VerticalLayoutGroup/VerticalLayoutGroup User");
			click = (Action)Delegate.Combine(click, delegate
			{
				CoroutineManager.RunCoroutine(AvatarFavMenu.FixShit(objToOverride, header, tooltip));
			});
			AvatarFavMenu.SetButtonProperties(objToOverride, click, text, tooltip);
			AvatarFavMenu.OnOpen = (Action)Delegate.Combine(AvatarFavMenu.OnOpen, delegate
			{
				objToOverride.Find("Mask/Text_Name").GetComponent<TextMeshProUGUI>().text = text;
				objToOverride.parent = par;
			});
			AvatarFavMenu.OnOpen.Invoke();
			objToOverride.SetSiblingIndex(siblingIndex);
			AvatarFavMenu.Menus.Add(header);
			AvatarFavMenu.EXOFav = objToOverride.gameObject;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00014468 File Offset: 0x00012668
		internal static void PopulateList(List<AvatarObject> list)
		{
			UtilFunc.Delay(0.5f, delegate
			{
				AvatarFavMenu.FavBtn.SetActive(true);
				bool flag = !AvatarFavMenu.InFavMenu || !AvatarFavMenu.UpdateNeeded;
				if (!flag)
				{
					AvatarFavMenu.Clear();
					list.ForEach(delegate(AvatarObject x)
					{
						AvatarFavMenu.DisplayAvatar(x.ToApiAvatar());
					});
					AvatarFavMenu.SetDropdownValue("Container/MMParent/Menu_Avatars/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Header_MM_H2/RightItemContainer/Field_MM_SortBy", 0);
					AvatarFavMenu.UpdateNeeded = false;
				}
			});
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001449C File Offset: 0x0001269C
		internal static void AddAvatar(ApiAvatar avtr)
		{
			bool flag = !Enumerable.Any<AvatarObject>(AvatarFavMenu.Data.FavAvis, (AvatarObject x) => x.id == avtr.id);
			if (flag)
			{
				CLog.L(avtr.name + " Has been added from your favorites list", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\BigMenu\\AvatarFavMenu.cs", 193);
				GUILog.DisplayOnScreen(avtr.name + " added from your favorites list");
				AvatarFavMenu.Data.FavAvis.Insert(0, new AvatarObject(avtr, false, false));
			}
			File.WriteAllText(AvatarFavMenu.Data.FavsPath, JsonConvert.SerializeObject(AvatarFavMenu.Data.FavAvis, Formatting.Indented));
			AvatarFavMenu.UpdateNeeded = true;
			AvatarFavMenu.PopulateList(AvatarFavMenu.Data.FavAvis);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00014558 File Offset: 0x00012758
		internal static void RemoveAvatar(ApiAvatar avtr)
		{
			bool flag = Enumerable.Any<AvatarObject>(AvatarFavMenu.Data.FavAvis, (AvatarObject x) => x.id == avtr.id);
			if (flag)
			{
				CLog.L(avtr.name + " Has been removed from your favorites list", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\BigMenu\\AvatarFavMenu.cs", 206);
				GUILog.DisplayOnScreen(avtr.name + " removed from your favorites list");
				AvatarFavMenu.Data.FavAvis.RemoveWhere((AvatarObject x) => x.id == avtr.id);
			}
			File.WriteAllText(AvatarFavMenu.Data.FavsPath, JsonConvert.SerializeObject(AvatarFavMenu.Data.FavAvis, Formatting.Indented));
			AvatarFavMenu.UpdateNeeded = true;
			AvatarFavMenu.PopulateList(AvatarFavMenu.Data.FavAvis);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00014610 File Offset: 0x00012810
		internal static void AddOrRemoveAvatar(ApiAvatar avtr)
		{
			bool flag = !Enumerable.Any<AvatarObject>(AvatarFavMenu.Data.FavAvis, (AvatarObject x) => x.id == avtr.id);
			if (flag)
			{
				AvatarFavMenu.AddAvatar(avtr);
			}
			else
			{
				AvatarFavMenu.RemoveAvatar(avtr);
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00014662 File Offset: 0x00012862
		private static IEnumerator CheckAvatar()
		{
			SimpleAvatarPedestal ped = APIBase.MMM.FindObject("Container/MMParent/Menu_Avatars/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Panel_SelectedAvatar/Panel_MM_AvatarViewer/Avatar").GetComponent<SimpleAvatarPedestal>();
			string lastAvatar = ped.field_Internal_ApiAvatar_0.id;
			for (;;)
			{
				yield return new WaitForSeconds(0.5f);
				bool flag = ped.field_Internal_ApiAvatar_0.id != lastAvatar;
				if (flag)
				{
					lastAvatar = ped.field_Internal_ApiAvatar_0.id;
					AvatarFavMenu.UpdateFavBtnText(ped.field_Internal_ApiAvatar_0);
				}
			}
			yield break;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0001466A File Offset: 0x0001286A
		private static IEnumerator FixShit(Transform objToOverride, string header, string tooltip)
		{
			UtilFunc.Delay(0.2f, delegate
			{
				AvatarFavMenu.Header.text = header;
			});
			bool flag = AvatarFavMenu.fixStarted;
			if (flag)
			{
				yield break;
			}
			AvatarFavMenu.fixStarted = true;
			for (;;)
			{
				yield return new WaitForSeconds(1f);
				TMP_Text header2 = AvatarFavMenu.Header;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(3, 2);
				defaultInterpolatedStringHandler.AppendFormatted(header);
				defaultInterpolatedStringHandler.AppendLiteral(" (");
				defaultInterpolatedStringHandler.AppendFormatted<int>(AvatarFavMenu.Data.FavAvis.Count);
				defaultInterpolatedStringHandler.AppendLiteral(")");
				header2.text = defaultInterpolatedStringHandler.ToStringAndClear();
				objToOverride.SetSiblingIndex(3);
				objToOverride.Find("Count_BG").SetActive(true);
				objToOverride.Find("Count_BG/Text_Number").GetComponentInChildren<TextMeshProUGUI>().text = AvatarFavMenu.Data.FavAvis.Count.ToString();
				objToOverride.Find("Count_BG/Text_Number").GetComponentInChildren<TextMeshProUGUIEx>()._localizableString = AvatarFavMenu.Data.FavAvis.Count.ToString().Localize(null, null, null);
				objToOverride.GetComponent<ToolTip>()._localizableString = tooltip.Localize(null, null, null);
				bool inFavMenu = AvatarFavMenu.InFavMenu;
				if (inFavMenu)
				{
					AvatarFavMenu.PopulateList(AvatarFavMenu.Data.FavAvis);
				}
			}
			yield break;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00014687 File Offset: 0x00012887
		private static IEnumerator FixToolTip()
		{
			bool flag = AvatarFavMenu.toolTipFixStarted;
			if (flag)
			{
				yield break;
			}
			AvatarFavMenu.toolTipFixStarted = true;
			for (;;)
			{
				yield return new WaitForSeconds(0.5f);
				AvatarFavMenu.FavBtn.GetComponent<ToolTip>()._localizableString = "EXO Avatar Favorites".ConvertToLocalized();
			}
			yield break;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00014698 File Offset: 0x00012898
		// Note: this type is marked as 'beforefieldinit'.
		static AvatarFavMenu()
		{
			List<string> list = new List<string>();
			list.Add("Public");
			list.Add("Fallbacks");
			AvatarFavMenu.Menus = list;
			AvatarFavMenu.check = false;
			AvatarFavMenu.UpdateNeeded = true;
			AvatarFavMenu.OnOpen = delegate
			{
				UtilFunc.Delay(0.3f, delegate
				{
					Action onOpen = AvatarFavMenu.OnOpen;
					if (onOpen != null)
					{
						onOpen.Invoke();
					}
					bool made = AvatarFavMenu.Made;
					if (made)
					{
						bool flag = !AvatarFavMenu.check;
						if (flag)
						{
							GameObject exofav = AvatarFavMenu.EXOFav;
							if (exofav != null)
							{
								exofav.GetComponent<Button>().onClick.Invoke();
							}
							UtilFunc.Delay(0.5f, delegate
							{
								APIBase.MMM.FindObject("Container/MMParent/Menu_Avatars/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Navigation/Viewport/VerticalLayoutGroup/VerticalLayoutGroup User/Cell_MM_SidebarListItem (Favorites)").GetComponent<Button>().onClick.Invoke();
							});
							UtilFunc.Delay(0.6f, delegate
							{
								GameObject exofav2 = AvatarFavMenu.EXOFav;
								if (exofav2 != null)
								{
									exofav2.GetComponent<Button>().onClick.Invoke();
								}
							});
							AvatarFavMenu.check = true;
						}
					}
					else
					{
						try
						{
							QuickMenuPatch.OnQuickMenuOpen = (Action)Delegate.Combine(QuickMenuPatch.OnQuickMenuOpen, delegate
							{
								Action onOpen2 = AvatarFavMenu.OnOpen;
								if (onOpen2 != null)
								{
									onOpen2.Invoke();
								}
							});
							MainMenuPatch.OnMainMenuOpen = (Action)Delegate.Combine(MainMenuPatch.OnMainMenuOpen, delegate
							{
								Action onOpen3 = AvatarFavMenu.OnOpen;
								if (onOpen3 != null)
								{
									onOpen3.Invoke();
								}
							});
						}
						catch (Exception e)
						{
							CLog.E("Failed to add QM Listener", e);
						}
						Transform par = APIBase.MMM.FindObject("Container/MMParent/Menu_Avatars/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Navigation/Viewport/VerticalLayoutGroup/VerticalLayoutGroup User");
						AvatarFavMenu.MakeFavBtn();
						AvatarFavMenu.MakeBtn(par.parent.Find("VerticalLayoutGroup Dynamic").GetChildren()[0].transform, delegate
						{
							AvatarFavMenu.PopulateList(AvatarFavMenu.Data.FavAvis);
						}, "EXO Avatars", "EXO", "Unlimited Avatar Favorites", 3);
						AvatarFavMenu.MakeBanner();
						AvatarFavMenu.Made = true;
					}
				});
			};
		}

		// Token: 0x040001CE RID: 462
		private static GameObject EXOFav;

		// Token: 0x040001CF RID: 463
		private static List<string> Menus;

		// Token: 0x040001D0 RID: 464
		private static bool Made;

		// Token: 0x040001D1 RID: 465
		private static bool check;

		// Token: 0x040001D2 RID: 466
		private static bool toolTipFixStarted;

		// Token: 0x040001D3 RID: 467
		private static bool fixStarted;

		// Token: 0x040001D4 RID: 468
		private static bool UpdateNeeded;

		// Token: 0x040001D5 RID: 469
		private static object _avatarContent;

		// Token: 0x040001D6 RID: 470
		private static object _favBtn;

		// Token: 0x040001D7 RID: 471
		private static object _header;

		// Token: 0x040001D8 RID: 472
		private static object _banner;

		// Token: 0x040001D9 RID: 473
		private static Action OnOpen;

		// Token: 0x02000137 RID: 311
		private static class Data
		{
			// Token: 0x17000188 RID: 392
			// (get) Token: 0x0600097B RID: 2427 RVA: 0x0002F48D File Offset: 0x0002D68D
			internal static string FavsPath
			{
				get
				{
					return AppStart.HexedDirectory.FullName + "\\EXO\\FavoritesPlus.json";
				}
			}

			// Token: 0x17000189 RID: 393
			// (get) Token: 0x0600097C RID: 2428 RVA: 0x0002F4A3 File Offset: 0x0002D6A3
			internal static string SeenPath
			{
				get
				{
					return AppStart.HexedDirectory.FullName + "\\EXO\\SeenAvatars.json";
				}
			}

			// Token: 0x1700018A RID: 394
			// (get) Token: 0x0600097D RID: 2429 RVA: 0x0002F4BC File Offset: 0x0002D6BC
			internal static List<AvatarObject> FavAvis
			{
				get
				{
					bool flag = AvatarFavMenu.Data._favAvis == null;
					if (flag)
					{
						bool flag2 = !File.Exists(AvatarFavMenu.Data.FavsPath);
						if (flag2)
						{
							string favsPath = AvatarFavMenu.Data.FavsPath;
							List<AvatarObject> list = new List<AvatarObject>();
							list.Add(AvatarObject.NullrobotAvatar);
							File.WriteAllText(favsPath, JsonConvert.SerializeObject(list));
						}
						AvatarFavMenu.Data._favAvis = JsonConvert.DeserializeObject<List<AvatarObject>>(File.ReadAllText(AvatarFavMenu.Data.FavsPath));
					}
					return AvatarFavMenu.Data._favAvis;
				}
			}

			// Token: 0x0600097E RID: 2430 RVA: 0x0002F528 File Offset: 0x0002D728
			internal static List<AvatarObject> SeenAvis()
			{
				bool flag = AvatarFavMenu.Data._seenAvis == null;
				if (flag)
				{
					bool flag2 = !File.Exists(AvatarFavMenu.Data.SeenPath);
					if (flag2)
					{
						string seenPath = AvatarFavMenu.Data.SeenPath;
						List<AvatarObject> list = new List<AvatarObject>();
						list.Add(AvatarObject.NullrobotAvatar);
						File.WriteAllText(seenPath, JsonConvert.SerializeObject(list));
					}
					AvatarFavMenu.Data._seenAvis = JsonConvert.DeserializeObject<List<AvatarObject>>(File.ReadAllText(AvatarFavMenu.Data.SeenPath));
				}
				bool flag3 = AvatarFavMenu.Data._seenAvis.Count > 30;
				if (flag3)
				{
					AvatarFavMenu.Data._seenAvis = Enumerable.ToList<AvatarObject>(Enumerable.Skip<AvatarObject>(AvatarFavMenu.Data._seenAvis, AvatarFavMenu.Data._seenAvis.Count - 30));
				}
				return AvatarFavMenu.Data._seenAvis;
			}

			// Token: 0x04000598 RID: 1432
			internal static List<AvatarObject> _favAvis;

			// Token: 0x04000599 RID: 1433
			internal static List<AvatarObject> _seenAvis;
		}
	}
}
