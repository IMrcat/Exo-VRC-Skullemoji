using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EXO.Core;
using EXO.Patches;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EXO.Functions.MenuOverrides
{
	// Token: 0x0200009B RID: 155
	internal class DashOverride
	{
		// Token: 0x06000601 RID: 1537 RVA: 0x0002115C File Offset: 0x0001F35C
		internal static void PatchQM()
		{
			bool flag = (MenuCore.quickMenu = GameObject.Find("Canvas_QuickMenu(Clone)")) == null;
			if (!flag)
			{
				ScrollRect componentInChildren = MenuCore.quickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Dashboard").GetComponentInChildren<ScrollRect>();
				componentInChildren.content.GetComponent<VerticalLayoutGroup>().childControlHeight = true;
				componentInChildren.enabled = true;
				componentInChildren.verticalScrollbar = componentInChildren.transform.Find("Scrollbar").GetComponent<Scrollbar>();
				componentInChildren.viewport.GetComponent<RectMask2D>().enabled = true;
				Transform carousel = MenuCore.quickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners");
				Transform banner = MenuCore.quickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/VRC+_Banners");
				carousel.localScale = Vector3.zero;
				banner.localScale = Vector3.zero;
				carousel.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
				banner.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
				carousel.gameObject.active = false;
				banner.gameObject.active = false;
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0002126C File Offset: 0x0001F46C
		internal static void ApplyQMOverride()
		{
			MenuCore.quickMenu = GameObject.Find("Canvas_QuickMenu(Clone)");
			bool flag = MenuCore.quickMenu == null;
			if (!flag)
			{
				Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();
				string text = "Button_Worlds";
				Transform transform = MenuCore.quickMenu.transform.Find(DashOverride.linksButtons + "Button_Worlds");
				dictionary.Add(text, (transform != null) ? transform.gameObject : null);
				string text2 = "Button_Avatars";
				Transform transform2 = MenuCore.quickMenu.transform.Find(DashOverride.linksButtons + "Button_Avatars");
				dictionary.Add(text2, (transform2 != null) ? transform2.gameObject : null);
				string text3 = "Button_Social";
				Transform transform3 = MenuCore.quickMenu.transform.Find(DashOverride.linksButtons + "Button_Social");
				dictionary.Add(text3, (transform3 != null) ? transform3.gameObject : null);
				string text4 = "Button_ViewGroups";
				Transform transform4 = MenuCore.quickMenu.transform.Find(DashOverride.linksButtons + "Button_ViewGroups");
				dictionary.Add(text4, (transform4 != null) ? transform4.gameObject : null);
				string text5 = "Button_GoHome";
				Transform transform5 = MenuCore.quickMenu.transform.Find(DashOverride.actionsButtons + "Button_GoHome");
				dictionary.Add(text5, (transform5 != null) ? transform5.gameObject : null);
				string text6 = "Button_Respawn";
				Transform transform6 = MenuCore.quickMenu.transform.Find(DashOverride.actionsButtons + "Button_Respawn");
				dictionary.Add(text6, (transform6 != null) ? transform6.gameObject : null);
				string text7 = "Button_SelectUser";
				Transform transform7 = MenuCore.quickMenu.transform.Find(DashOverride.actionsButtons + "Button_SelectUser");
				dictionary.Add(text7, (transform7 != null) ? transform7.gameObject : null);
				string text8 = "Button_Safety";
				Transform transform8 = MenuCore.quickMenu.transform.Find(DashOverride.actionsButtons + "Button_Safety");
				dictionary.Add(text8, (transform8 != null) ? transform8.gameObject : null);
				DashOverride.qmButtons = dictionary;
				DashOverride.headerText = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/Header_H1/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>();
				DashOverride.headerText.richText = true;
				DashOverride.headerText.text = "<color=#c00000>EXO</color>";
				DashOverride.headerText.transform.localPosition = new Vector3(55f, 0f, 0f);
				Transform transform9 = MenuCore.quickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickLinks");
				if (transform9 != null)
				{
					transform9.gameObject.SetActive(false);
				}
				Transform transform10 = MenuCore.quickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickActions");
				if (transform10 != null)
				{
					transform10.gameObject.SetActive(false);
				}
				Delegate onQuickMenuOpen = QuickMenuPatch.OnQuickMenuOpen;
				Action action;
				if ((action = DashOverride.<>O.<0>__FormateDash) == null)
				{
					action = (DashOverride.<>O.<0>__FormateDash = new Action(DashOverride.FormateDash));
				}
				QuickMenuPatch.OnQuickMenuOpen = (Action)Delegate.Combine(onQuickMenuOpen, action);
				Delegate onQuickMenuClose = QuickMenuPatch.OnQuickMenuClose;
				Action action2;
				if ((action2 = DashOverride.<>O.<0>__FormateDash) == null)
				{
					action2 = (DashOverride.<>O.<0>__FormateDash = new Action(DashOverride.FormateDash));
				}
				QuickMenuPatch.OnQuickMenuClose = (Action)Delegate.Combine(onQuickMenuClose, action2);
				DashOverride.FormateDash();
				MenuCore.quickMenu.transform.Find("CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup").GetComponent<VerticalLayoutGroup>().enabled = false;
				bool flag2 = QMConsole.logObject != null;
				if (flag2)
				{
					QMConsole.logObject.transform.localPosition = new Vector3(-200f, 0f, 0f);
				}
				Transform actionGroup = MenuCore.quickMenu.transform.Find(DashOverride.actionsButtons);
				bool flag3 = actionGroup != null;
				if (flag3)
				{
					actionGroup.localPosition = new Vector3(0f, -150f, 0f);
				}
				Transform linkGroup = MenuCore.quickMenu.transform.Find(DashOverride.linksButtons);
				bool flag4 = linkGroup != null;
				if (flag4)
				{
					linkGroup.localPosition = new Vector3(0f, -50f, 0f);
				}
				foreach (GameObject button in DashOverride.qmButtons.Values)
				{
					bool flag5 = button == null;
					if (!flag5)
					{
						DashOverride.SetButtonProperties(button);
					}
				}
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0002168C File Offset: 0x0001F88C
		private static void SetButtonProperties(GameObject button)
		{
			Transform buttonBackground = button.transform.Find("Background");
			bool flag = buttonBackground != null;
			if (flag)
			{
				buttonBackground.transform.localScale = new Vector3(1f, 0.5f, 1f);
			}
			foreach (TextMeshProUGUI textMesh in button.GetComponentsInChildren<TextMeshProUGUI>())
			{
				textMesh.enableAutoSizing = false;
				textMesh.transform.localScale = Vector3.one;
			}
			Transform text = button.transform.Find("TextLayoutParent/Text_H4");
			bool flag2 = text != null;
			if (flag2)
			{
				TextMeshProUGUI component = text.GetComponent<TextMeshProUGUI>();
				bool flag3 = component != null;
				if (flag3)
				{
					component.enableAutoSizing = false;
					component.rectTransform.localScale = Vector3.one;
					component.rectTransform.sizeDelta = new Vector2(200f, 50f);
					TextMeshProUGUI textMeshProUGUI = component;
					string name = button.name;
					if (!true)
					{
					}
					uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
					string text2;
					if (num <= 2034525333U)
					{
						if (num <= 548756159U)
						{
							if (num != 112140175U)
							{
								if (num == 548756159U)
								{
									if (name == "Button_Worlds")
									{
										text2 = "World";
										goto IL_027C;
									}
								}
							}
							else if (name == "Button_GoHome")
							{
								text2 = "Home";
								goto IL_027C;
							}
						}
						else if (num != 860866672U)
						{
							if (num == 2034525333U)
							{
								if (name == "Button_SelectUser")
								{
									text2 = "Select";
									goto IL_027C;
								}
							}
						}
						else if (name == "Button_Safety")
						{
							text2 = "Safety";
							goto IL_027C;
						}
					}
					else if (num <= 2424950392U)
					{
						if (num != 2066866337U)
						{
							if (num == 2424950392U)
							{
								if (name == "Button_Avatars")
								{
									text2 = "Avatar";
									goto IL_027C;
								}
							}
						}
						else if (name == "Button_Social")
						{
							text2 = "Social";
							goto IL_027C;
						}
					}
					else if (num != 2566887506U)
					{
						if (num == 4200933131U)
						{
							if (name == "Button_ViewGroups")
							{
								text2 = "Group";
								goto IL_027C;
							}
						}
					}
					else if (name == "Button_Respawn")
					{
						text2 = "Respawn";
						goto IL_027C;
					}
					text2 = "die";
					IL_027C:
					if (!true)
					{
					}
					textMeshProUGUI.text = text2;
				}
			}
			Transform closeIcon = button.transform.Find("Badge_Close");
			bool flag4 = closeIcon != null;
			if (flag4)
			{
				closeIcon.gameObject.SetActive(true);
				closeIcon.localPosition = new Vector3(85f, 35f, 0f);
				Image img = closeIcon.GetComponent<Image>();
				bool flag5 = img != null;
				if (flag5)
				{
					img.color = Color.white;
					img.overrideSprite = button.transform.Find("Icons/Icon").GetComponent<Image>().sprite;
					closeIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(25f, 25f);
				}
			}
			Transform transform = button.transform.Find("Badge_MMJump");
			if (transform != null)
			{
				transform.gameObject.SetActive(false);
			}
			Transform transform2 = button.transform.Find("Icons");
			if (transform2 != null)
			{
				transform2.gameObject.SetActive(false);
			}
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00021A28 File Offset: 0x0001FC28
		internal static void FormateDash()
		{
			bool flag = QMConsole.logObject != null;
			if (flag)
			{
				QMConsole.logObject.transform.localPosition = new Vector3(-200f, 0f, 0f);
			}
			DashOverride.headerText.transform.localPosition = new Vector3(55f, 0f, 0f);
			foreach (GameObject button in DashOverride.qmButtons.Values)
			{
				bool flag2 = button == null;
				if (!flag2)
				{
					Transform transform = button.transform;
					string name = button.name;
					if (!true)
					{
					}
					Vector3 vector;
					if (!(name == "Button_Avatars"))
					{
						if (!(name == "Button_Respawn"))
						{
							if (!(name == "Button_Social"))
							{
								if (!(name == "Button_SelectUser"))
								{
									if (!(name == "Button_ViewGroups"))
									{
										if (!(name == "Button_Safety"))
										{
											vector = button.transform.localPosition;
										}
										else
										{
											vector = new Vector3(-348f, -600f, 0f);
										}
									}
									else
									{
										vector = new Vector3(-348f, -600f, 0f);
									}
								}
								else
								{
									vector = new Vector3(-348f, -400f, 0f);
								}
							}
							else
							{
								vector = new Vector3(-348f, -400f, 0f);
							}
						}
						else
						{
							vector = new Vector3(-348f, -200f, 0f);
						}
					}
					else
					{
						vector = new Vector3(-348f, -200f, 0f);
					}
					if (!true)
					{
					}
					transform.localPosition = vector;
				}
			}
		}

		// Token: 0x040002C0 RID: 704
		private static Dictionary<string, GameObject> qmButtons;

		// Token: 0x040002C1 RID: 705
		private static string linksButtons = "CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickLinks/";

		// Token: 0x040002C2 RID: 706
		private static string actionsButtons = "CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/";

		// Token: 0x040002C3 RID: 707
		internal static VerticalLayoutGroup layoutGroup;

		// Token: 0x040002C4 RID: 708
		internal static TextMeshProUGUI headerText;

		// Token: 0x0200018B RID: 395
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040006DC RID: 1756
			public static Action <0>__FormateDash;
		}
	}
}
