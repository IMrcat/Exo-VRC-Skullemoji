using System;
using EXO.Core;
using EXO.LogTools;
using EXO.Modules.Utilities;
using EXO.Wrappers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI;

namespace EXO.Menus
{
	// Token: 0x02000047 RID: 71
	internal class ColorMenus
	{
		// Token: 0x06000312 RID: 786 RVA: 0x0000CE0C File Offset: 0x0000B00C
		public static void Paint()
		{
			Transform mmpar = UtilFunc.UserInterface.FindObject("Canvas_MainMenu(Clone)/Container/MMParent");
			mmpar.FindObject("Page_MM_Backgrounds").SetActive(true);
			Button[] buttons = Resources.FindObjectsOfTypeAll<Button>();
			Button button = null;
			foreach (Button btn in buttons)
			{
				bool flag = btn.gameObject.name == "Filigree" && btn.gameObject.transform.parent.name == "Page_MM_Backgrounds";
				if (flag)
				{
					button = btn;
					break;
				}
			}
			bool flag2 = button != null;
			if (flag2)
			{
				button.onClick.Invoke();
			}
			else
			{
				CLog.E("Button not found", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\ColorMenus.cs", 54);
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000CEDC File Offset: 0x0000B0DC
		internal static Color HexToColor(string hexColor)
		{
			bool flag = hexColor.IndexOf('#') != -1;
			if (flag)
			{
				hexColor = hexColor.Replace("#", "");
			}
			float r = (float)int.Parse(hexColor.Substring(0, 2), 512) / 255f;
			float g = (float)int.Parse(hexColor.Substring(2, 2), 512) / 255f;
			float b = (float)int.Parse(hexColor.Substring(4, 2), 512) / 255f;
			return new Color(r, g, b);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000CF6C File Offset: 0x0000B16C
		internal static string ColorToHex(Color baseColor, bool hash = false)
		{
			int num = Convert.ToInt32(baseColor.r * 255f);
			int num2 = Convert.ToInt32(baseColor.g * 255f);
			int num3 = Convert.ToInt32(baseColor.b * 255f);
			string text = num.ToString("X2") + num2.ToString("X2") + num3.ToString("X2");
			if (hash)
			{
				text = "#" + text;
			}
			return text;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000CFF4 File Offset: 0x0000B1F4
		internal static void RecolorText(string hex)
		{
			foreach (Text text in MenuCore.quickMenu.GetComponentsInChildren<Text>(true))
			{
				text.color = ColorMenus.HexToColor(hex);
			}
			foreach (TextMeshProUGUI textMeshProUGUI in MenuCore.quickMenu.GetComponentsInChildren<TextMeshProUGUI>(true))
			{
				textMeshProUGUI.color = ColorMenus.HexToColor(hex);
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000D098 File Offset: 0x0000B298
		internal static void RecolorButton(string hex)
		{
			Color clr = ColorMenus.HexToColor(hex);
			foreach (Toggle toggle in MenuCore.quickMenu.GetComponentsInChildren<Toggle>(true))
			{
				try
				{
					bool flag = ColorMenus.HasParentWithName(toggle.gameObject, "HorizontalLayoutGroup");
					if (!flag)
					{
						ColorMenus.RecolorBackGrn(toggle.transform.Find("Background").gameObject, clr);
					}
				}
				catch (Exception e)
				{
				}
			}
			foreach (Button button2 in MenuCore.quickMenu.GetComponentsInChildren<Button>(true))
			{
				try
				{
					bool flag2 = ColorMenus.HasParentWithName(button2.gameObject, "HorizontalLayoutGroup");
					if (flag2)
					{
						continue;
					}
					ColorMenus.RecolorBackGrn(button2.transform.Find("Background").gameObject, clr);
				}
				catch (Exception e2)
				{
				}
				try
				{
					bool flag3 = ColorMenus.HasParentWithName(button2.gameObject, "HorizontalLayoutGroup");
					if (flag3)
					{
						continue;
					}
					bool flag4 = button2.transform.Find("Background") == null;
					if (flag4)
					{
						ColorMenus.RecolorBackGrn(button2.transform.Find("Background (1)").gameObject, clr);
					}
				}
				catch (Exception e3)
				{
				}
				try
				{
					bool flag5 = ColorMenus.HasParentWithName(button2.gameObject, "HorizontalLayoutGroup");
					if (flag5)
					{
						continue;
					}
					bool flag6 = button2.transform.Find("Background") == null;
					if (flag6)
					{
						ColorMenus.RecolorBackGrn(button2.transform.Find("Background (2)").gameObject, clr);
					}
				}
				catch (Exception e4)
				{
				}
				try
				{
					bool flag7 = ColorMenus.HasParentWithName(button2.gameObject, "HorizontalLayoutGroup");
					if (!flag7)
					{
						ColorMenus.RecolorBackGrn(button2.transform.Find("Container/Background").gameObject, clr);
					}
				}
				catch (Exception e5)
				{
				}
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000D34C File Offset: 0x0000B54C
		private static bool HasParentWithName(GameObject obj, string parentName)
		{
			Transform currentParent = obj.transform.parent;
			while (currentParent != null)
			{
				bool flag = currentParent.gameObject.name.Equals(parentName);
				if (flag)
				{
					return true;
				}
				currentParent = currentParent.parent;
			}
			return false;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000D39A File Offset: 0x0000B59A
		internal static void RecolorBackGrn(Transform transform, Color color)
		{
			ColorMenus.RecolorBackGrn(transform.gameObject, color);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000D3AC File Offset: 0x0000B5AC
		internal static void RecolorBackGrn(GameObject bg, Color color)
		{
			bool flag = bg.transform.parent.Find("Bg") == null;
			if (flag)
			{
				GameObject Btn = Object.Instantiate<GameObject>(bg.gameObject, bg.transform.parent);
				Btn.name = "Bg";
				Btn.GetComponent<RectTransform>().SetSiblingIndex(1);
				bg.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 0f);
				bg.active = false;
			}
			Image component3 = bg.transform.parent.Find("Bg").GetComponent<Image>();
			component3.color = color;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000D454 File Offset: 0x0000B654
		internal static void RecolorPointer(int side, string hex)
		{
			bool flag = !PlayerWrapper.IsInVR();
			if (!flag)
			{
				Color csdlr = UtilFunc.HexToColor(hex);
				Color clr = new Color(csdlr.r, csdlr.g, csdlr.b, 0.8944f);
				bool flag2 = ColorMenus.VRUiCursorL == null;
				if (flag2)
				{
					ColorMenus.VRUiCursorL = GameObject.Find("CursorManager").FindObject("DotLeftHand").GetComponent<VRCUiCursor>();
					ColorMenus.VRUiCursorR = GameObject.Find("CursorManager").FindObject("DotRightHand").GetComponent<VRCUiCursor>();
				}
				bool flag3 = side == -1;
				if (flag3)
				{
					ColorMenus.VRUiCursorL.field_Public_Color_0 = clr;
					ColorMenus.VRUiCursorL.field_Public_Color_1 = clr;
				}
				else
				{
					ColorMenus.VRUiCursorR.field_Public_Color_0 = clr;
					ColorMenus.VRUiCursorR.field_Public_Color_1 = clr;
				}
				if (ColorMenus.VRCUICursorIcon == null)
				{
					ColorMenus.VRCUICursorIcon = VRCApplication.prop_VRCApplication_0.FindObject("CursorManager/MouseArrow/VRCUICursorIcon").GetComponent<SpriteRenderer>();
				}
				ColorMenus.VRCUICursorIcon.color = csdlr;
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000D554 File Offset: 0x0000B754
		internal static void ChangeQMBG()
		{
			Transform obj = APIBase.QuickMenu.FindObject("CanvasGroup/Container/Window/QMParent/BackgroundLayer01");
			if (ColorMenus.QMImage == null)
			{
				ColorMenus.QMImage = obj.GetComponent<Image>();
			}
			ColorMenus.UpdateQMColor();
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000D58A File Offset: 0x0000B78A
		internal static void UpdateQMColor()
		{
			ColorMenus.QMImage.color = new Color(0f, 0f, 0f, 0f);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000D5B0 File Offset: 0x0000B7B0
		internal static void ColorWing(int side, string hex)
		{
			Color clr = UtilFunc.HexToColor(hex);
			string Txside = ((side == 1) ? "Right" : "Left");
			Transform obj = APIBase.QuickMenu.FindObject("CanvasGroup/Container/Window/Wing_" + Txside + "/Container/InnerContainer/Background");
			obj.GetComponent<Image>().color = clr;
		}

		// Token: 0x0400015A RID: 346
		private static VRCUiCursor VRUiCursorL;

		// Token: 0x0400015B RID: 347
		private static VRCUiCursor VRUiCursorR;

		// Token: 0x0400015C RID: 348
		private static SpriteRenderer VRCUICursorIcon;

		// Token: 0x0400015D RID: 349
		private static Image QMImage;
	}
}
