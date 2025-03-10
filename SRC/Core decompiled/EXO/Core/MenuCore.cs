using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using CoreRuntime.Manager;
using EXO.Functions.InputManager;
using EXO.Functions.MenuOverrides;
using EXO.Functions.PlayerFunc;
using EXO.LogTools;
using EXO.Menus;
using EXO.Modules;
using EXO.Modules.Utilities;
using EXO.Patches;
using EXO_Base;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace EXO.Core
{
	// Token: 0x020000B2 RID: 178
	internal class MenuCore
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x000264EC File Offset: 0x000246EC
		private static IEnumerable<MenuModule> EModule
		{
			get
			{
				IEnumerable<MenuModule> enumerable;
				if ((enumerable = MenuCore.Module) == null)
				{
					enumerable = (MenuCore.Module = Enumerable.ToList<MenuModule>(Enumerable.Select<Type, MenuModule>(Enumerable.Where<Type>(Assembly.GetExecutingAssembly().GetTypesSafe(), (Type o) => o.IsSubclassOf(typeof(MenuModule))), (Type a) => (MenuModule)Activator.CreateInstance(a))));
				}
				return enumerable;
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00026560 File Offset: 0x00024760
		public static void OnUpdate()
		{
			bool flag = !Keyboard.current.insertKey.wasPressedThisFrame;
			if (!flag)
			{
				CLog.L("Force Loading Menus...", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Core\\MenuCore.cs", 54);
				MenuCore.LoadMenus();
			}
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0002659F File Offset: 0x0002479F
		public static void LoadMenus()
		{
			CLog.L("Waiting for UI...", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Core\\MenuCore.cs", 60);
			CoroutineManager.RunCoroutine(MenuCore.<LoadMenus>g__WaitForQM|7_0());
			CoroutineManager.RunCoroutine(MenuCore.<LoadMenus>g__WaitForQMLate|7_1());
			CoroutineManager.RunCoroutine(MenuCore.<LoadMenus>g__WaitForMM|7_2());
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x000265DC File Offset: 0x000247DC
		internal static void LateInit()
		{
			DashOverride.PatchQM();
			bool qmconsole = Config.cfg.QMConsole;
			if (qmconsole)
			{
				DashOverride.ApplyQMOverride();
			}
			bool menuRecolor = Config.cfg.MenuRecolor;
			if (menuRecolor)
			{
				ColorMenus.Paint();
			}
			Zoom.AfterMenuLoad();
			KeybindInputs.ShowKeyBinds();
			Camera.main.GetComponent<HighlightsFXStandalone>().highlightColor = Color.red;
			MenuCore.SetReticle(BaseImages.FromBase(BaseImages.ReticleIcon));
			LogReader.Init();
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00026650 File Offset: 0x00024850
		internal static void SetReticle(Sprite sprite)
		{
			UtilFunc.WaitForObj("UserInterface/UnscaledUI/HudContent/Canvas/ReticleParent/Reticle", delegate
			{
				GameObject obj = GameObject.Find("UserInterface/UnscaledUI/HudContent/Canvas/ReticleParent/Reticle");
				bool flag = obj.GetComponent<Image>() == null;
				if (flag)
				{
					CLog.E("Image is Null", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Core\\MenuCore.cs", 162);
				}
				else
				{
					obj.GetComponent<Image>().overrideSprite = sprite;
				}
			});
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00026684 File Offset: 0x00024884
		internal static void MenuLogs(string msg)
		{
			CLog.Tag();
			"[".WriteToConsole(15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Core\\MenuCore.cs", 170).WriteToConsole("Menu", 12, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Core\\MenuCore.cs", 171).WriteToConsole("] ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Core\\MenuCore.cs", 172)
				.WriteLineToConsole(msg, 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Core\\MenuCore.cs", 173);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00026714 File Offset: 0x00024914
		[CompilerGenerated]
		internal static IEnumerator <LoadMenus>g__WaitForQM|7_0()
		{
			for (;;)
			{
				VRCUiManager field_Private_Static_VRCUiManager_ = VRCUiManager.field_Private_Static_VRCUiManager_0;
				Object @object;
				if (field_Private_Static_VRCUiManager_ == null)
				{
					@object = null;
				}
				else
				{
					GameObject gameObject = field_Private_Static_VRCUiManager_.gameObject;
					@object = ((gameObject != null) ? gameObject.FindObject("Canvas_QuickMenu(Clone)") : null);
				}
				if (!(@object == null))
				{
					break;
				}
				yield return new WaitForFixedUpdate();
			}
			MenuCore.MenuLogs("Building Menus...");
			foreach (MenuModule menu in MenuCore.EModule)
			{
				try
				{
					MethodInfo isOverriding = menu.GetType().GetMethod("LoadMenu");
					bool flag = isOverriding.DeclaringType == menu.GetType();
					if (flag)
					{
						MenuCore.MenuLogs("Building " + menu.GetType().Name + "...");
						MenuModule menuModule = menu;
						if (menuModule != null)
						{
							menuModule.LoadMenu();
						}
					}
					isOverriding = null;
				}
				catch (Exception ex2)
				{
					Exception ex = ex2;
					string text = "Error! ";
					string fullName = menu.GetType().FullName;
					string text2 = "\n";
					Exception ex3 = ex;
					MenuCore.MenuLogs(text + fullName + text2 + ((ex3 != null) ? ex3.ToString() : null));
				}
				menu = null;
			}
			IEnumerator<MenuModule> enumerator = null;
			ColorPaletteOverride.InitPalettes();
			TabExtender.OnMenuBuilt();
			VRCUiManager field_Private_Static_VRCUiManager_2 = VRCUiManager.field_Private_Static_VRCUiManager_0;
			if (field_Private_Static_VRCUiManager_2 != null)
			{
				field_Private_Static_VRCUiManager_2.FindObject("Canvas_MainMenu(Clone)/Container/PageButtons/HorizontalLayoutGroup/Page_VRCPlusHighlight").GetComponent<Button>().onClick.AddListener(MenuCore.VRCPlus);
			}
			yield break;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0002672C File Offset: 0x0002492C
		[CompilerGenerated]
		internal static IEnumerator <LoadMenus>g__WaitForQMLate|7_1()
		{
			while (GameObject.Find("Canvas_QuickMenu(Clone)") == null)
			{
				yield return new WaitForFixedUpdate();
			}
			foreach (MenuModule menu in MenuCore.EModule)
			{
				try
				{
					MethodInfo isOverriding = menu.GetType().GetMethod("LoadLate");
					bool flag = isOverriding.DeclaringType == menu.GetType();
					if (flag)
					{
						MenuCore.MenuLogs("Building " + menu.GetType().Name + "...");
						MenuModule menuModule = menu;
						if (menuModule != null)
						{
							menuModule.LoadLate();
						}
					}
					isOverriding = null;
				}
				catch (Exception ex2)
				{
					Exception ex = ex2;
					string text = "Error! ";
					string fullName = menu.GetType().FullName;
					string text2 = "\n";
					Exception ex3 = ex;
					MenuCore.MenuLogs(text + fullName + text2 + ((ex3 != null) ? ex3.ToString() : null));
				}
				menu = null;
			}
			IEnumerator<MenuModule> enumerator = null;
			MenuCore.LateInit();
			MenuCore.MenuLogs("Menus Menu Has Been Assembled!");
			yield break;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00026744 File Offset: 0x00024944
		[CompilerGenerated]
		internal static IEnumerator <LoadMenus>g__WaitForMM|7_2()
		{
			while (GameObject.Find("Canvas_MainMenu(Clone)/Container/MMParent/Menu_VRChat+/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup") == null)
			{
				yield return new WaitForFixedUpdate();
			}
			ColorPaletteOverride.EnableVRCPlus();
			yield break;
		}

		// Token: 0x04000318 RID: 792
		private static Action onOpenVRCPlus;

		// Token: 0x04000319 RID: 793
		private static Action VRCPlus = delegate
		{
			Action action = MenuCore.onOpenVRCPlus;
			if (action != null)
			{
				action.Invoke();
			}
			QuickMenuPatch.OnQuickMenuOpen = (Action)Delegate.Combine(QuickMenuPatch.OnQuickMenuOpen, delegate
			{
				Action action2 = MenuCore.onOpenVRCPlus;
				if (action2 != null)
				{
					action2.Invoke();
				}
			});
			ColorPaletteOverride.EnableVRCPlus();
		};

		// Token: 0x0400031A RID: 794
		private static IEnumerable<MenuModule> Module = null;

		// Token: 0x0400031B RID: 795
		public static GameObject quickMenu;
	}
}
