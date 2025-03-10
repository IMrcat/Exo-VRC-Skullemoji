using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;

namespace WorldAPI.ButtonAPI.Extras
{
	// Token: 0x0200000D RID: 13
	public static class QMUtils
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000035D3 File Offset: 0x000017D3
		public static QuickMenu GetQuickMenuInstance
		{
			get
			{
				QuickMenu quickMenu;
				if ((quickMenu = QMUtils._Qm) == null)
				{
					quickMenu = (QMUtils._Qm = Enumerable.FirstOrDefault<QuickMenu>(Resources.FindObjectsOfTypeAll<QuickMenu>()));
				}
				return quickMenu;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000035EE File Offset: 0x000017EE
		public static MainMenu GetMainMenuInstance
		{
			get
			{
				MainMenu mainMenu;
				if ((mainMenu = QMUtils._MM) == null)
				{
					mainMenu = (QMUtils._MM = Enumerable.FirstOrDefault<MainMenu>(Resources.FindObjectsOfTypeAll<MainMenu>(), (MainMenu x) => x.name == "Canvas_MainMenu(Clone)"));
				}
				return mainMenu;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00003628 File Offset: 0x00001828
		public static MenuStateController GetMenuStateControllerInstance
		{
			get
			{
				MenuStateController menuStateController;
				if ((menuStateController = QMUtils.QuickMenuController) == null)
				{
					menuStateController = (QMUtils.QuickMenuController = QMUtils.GetQuickMenuInstance.GetComponent<MenuStateController>());
				}
				return menuStateController;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00003643 File Offset: 0x00001843
		public static MenuStateController GetMainMenuStateControllerInstance
		{
			get
			{
				MenuStateController menuStateController;
				if ((menuStateController = QMUtils.MainMenuController) == null)
				{
					menuStateController = (QMUtils.MainMenuController = QMUtils.GetMainMenuInstance.GetComponent<MenuStateController>());
				}
				return menuStateController;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000365E File Offset: 0x0000185E
		public static MenuStateController GetWngLMenuStateControllerInstance
		{
			get
			{
				MenuStateController menuStateController;
				if ((menuStateController = QMUtils.WLcontroller) == null)
				{
					menuStateController = (QMUtils.WLcontroller = QMUtils.GetQuickMenuInstance.transform.Find("CanvasGroup/Container/Window/Wing_Left").GetComponent<MenuStateController>());
				}
				return menuStateController;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003688 File Offset: 0x00001888
		public static MenuStateController GetWngRMenuStateControllerInstance
		{
			get
			{
				MenuStateController menuStateController;
				if ((menuStateController = QMUtils.WRcontroller) == null)
				{
					menuStateController = (QMUtils.WRcontroller = QMUtils.GetQuickMenuInstance.transform.Find("CanvasGroup/Container/Window/Wing_Right").GetComponent<MenuStateController>());
				}
				return menuStateController;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000036B2 File Offset: 0x000018B2
		public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
		{
			return gameObject.transform.GetOrAddComponent<T>();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000036C0 File Offset: 0x000018C0
		public static T GetOrAddComponent<T>(this Transform transform) where T : Component
		{
			T component = transform.GetComponent<T>();
			bool flag = component == null;
			T t;
			if (flag)
			{
				t = transform.gameObject.AddComponent<T>();
			}
			else
			{
				t = component;
			}
			return t;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000036F8 File Offset: 0x000018F8
		public static void DestroyChildren(this Transform transform, Func<Transform, bool> exclude = null)
		{
			for (int childcount = transform.childCount - 1; childcount >= 0; childcount--)
			{
				bool flag = exclude == null || exclude.Invoke(transform.GetChild(childcount));
				if (flag)
				{
					Object.DestroyImmediate(transform.GetChild(childcount).gameObject);
				}
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003749 File Offset: 0x00001949
		public static void DestroyChildren(this GameObject gameObj, Func<Transform, bool> exclude = null)
		{
			gameObj.transform.DestroyChildren(exclude);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003758 File Offset: 0x00001958
		public static List<GameObject> GetChildren(this Transform transform)
		{
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < transform.childCount; i++)
			{
				GameObject gameObject = transform.GetChild(i).gameObject;
				list.Add(gameObject);
			}
			return list;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000037A0 File Offset: 0x000019A0
		internal static bool IsObfuscated(this string str)
		{
			for (int i = 0; i < str.Length; i++)
			{
				char it = str.get_Chars(i);
				bool flag = !char.IsDigit(it) && (it < 'a' || it > 'z') && (it < 'A' || it > 'Z') && it != '_' && it != '`' && it != '.' && it != '<' && it != '>';
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003818 File Offset: 0x00001A18
		public static void ResetTransform(Transform transform)
		{
			transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			transform.localScale = new Vector3(1f, 1f, 1f);
			transform.localPosition = Vector3.zero;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003868 File Offset: 0x00001A68
		public static void RemoveUnknownComps(GameObject gameObject, Action<string> callBackOnDestroy = null)
		{
			Component[] components = gameObject.GetComponents<Component>();
			for (int D = 0; D < components.Length; D++)
			{
				string name = components[D].GetIl2CppType().Name;
				bool flag = name.IsObfuscated() && components[D].GetIl2CppType().BaseType.Name != "TextMeshProUGUI";
				if (flag)
				{
					Object.Destroy(components[D]);
					if (callBackOnDestroy != null)
					{
						callBackOnDestroy.Invoke(name);
					}
				}
			}
		}

		// Token: 0x0400002F RID: 47
		private static QuickMenu _Qm;

		// Token: 0x04000030 RID: 48
		private static MainMenu _MM;

		// Token: 0x04000031 RID: 49
		internal static MenuStateController QuickMenuController;

		// Token: 0x04000032 RID: 50
		internal static MenuStateController MainMenuController;

		// Token: 0x04000033 RID: 51
		internal static MenuStateController WLcontroller;

		// Token: 0x04000034 RID: 52
		internal static MenuStateController WRcontroller;
	}
}
