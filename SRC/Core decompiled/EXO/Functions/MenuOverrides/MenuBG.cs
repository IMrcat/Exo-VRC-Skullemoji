using System;
using System.IO;
using System.Net;
using EXO.Core;
using EXO.LogTools;
using EXO.Modules.Utilities;
using EXO.Patches;
using UnityEngine;
using UnityEngine.UI;

namespace EXO.Functions.MenuOverrides
{
	// Token: 0x0200009D RID: 157
	internal class MenuBG : FunctionModule
	{
		// Token: 0x06000610 RID: 1552 RVA: 0x00021E34 File Offset: 0x00020034
		public override void OnInject()
		{
			QuickMenuPatch.OnQuickMenuOpen = (Action)Delegate.Combine(QuickMenuPatch.OnQuickMenuOpen, delegate
			{
				UtilFunc.Delay(0.3f, delegate
				{
					MenuBG.ApplyMenuImages();
					MenuBG.UpdateImageAlpha();
				});
			});
			bool flag = !File.Exists(MenuBG.qmPath) || Config.cfg.ForceUpdate16;
			if (flag)
			{
				CLog.L("QM Background Downloading...", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\MenuOverrides\\MenuBG.cs", 37);
				Directory.CreateDirectory(Path.GetDirectoryName(MenuBG.qmPath));
				File.WriteAllBytes(MenuBG.qmPath, MenuBG.web.DownloadData("https://github.com/Cyconi/EXO-Resources/blob/main/qmImage.png?raw=true"));
				CLog.L("QM Background Downloaded", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\MenuOverrides\\MenuBG.cs", 41);
			}
			bool flag2 = !File.Exists(MenuBG.mmPath) || Config.cfg.ForceUpdate16;
			if (flag2)
			{
				CLog.L("MM Background Downloading...", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\MenuOverrides\\MenuBG.cs", 45);
				Directory.CreateDirectory(Path.GetDirectoryName(MenuBG.mmPath));
				File.WriteAllBytes(MenuBG.mmPath, MenuBG.web.DownloadData("https://github.com/Cyconi/EXO-Resources/blob/main/mmImage.png?raw=true"));
				CLog.L("MM Background Downloaded", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\MenuOverrides\\MenuBG.cs", 49);
			}
			MenuBG.ApplyMenuImages();
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00021F5C File Offset: 0x0002015C
		internal static void UpdateImageAlpha()
		{
			Color tempColor = MenuBG.mmImage.color;
			tempColor.a = Config.cfg.BackgroundTransparency;
			MenuBG.mmImage.color = tempColor;
			tempColor = MenuBG.qmImage.color;
			tempColor.a = Config.cfg.BackgroundTransparency;
			MenuBG.qmImage.color = tempColor;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00021FBA File Offset: 0x000201BA
		public static void ApplyMenuImages()
		{
			UtilFunc.WaitForObj("Canvas_QuickMenu(Clone)", delegate
			{
				MenuBG.qmImage = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/BackgroundLayer01").GetComponent<Image>();
				MenuBG.mmImage = GameObject.Find("UserInterface/Canvas_MainMenu(Clone)/Container/MMParent/BackgroundLayer01").GetComponent<Image>();
				bool qmbackground = Config.cfg.QMBackground;
				if (qmbackground)
				{
					MenuBG.qmImage.color = new Color(1f, 1f, 1f, Config.cfg.BackgroundTransparency);
					MenuBG.qmImage.overrideSprite = ImageHandleing.FromBytes(File.ReadAllBytes(MenuBG.qmPath));
				}
				else
				{
					MenuBG.qmImage.color = new Color(0f, 0f, 0f, Config.cfg.BackgroundTransparency);
				}
				bool mmbackground = Config.cfg.MMBackground;
				if (mmbackground)
				{
					MenuBG.mmImage.color = new Color(1f, 1f, 1f, Config.cfg.BackgroundTransparency);
					MenuBG.mmImage.overrideSprite = ImageHandleing.FromBytes(File.ReadAllBytes(MenuBG.mmPath));
				}
				else
				{
					MenuBG.mmImage.color = new Color(0f, 0f, 0f, Config.cfg.BackgroundTransparency);
				}
			});
		}

		// Token: 0x040002CC RID: 716
		internal static string qmPath = Path.Combine(Directory.CreateDirectory(AppStart.HexedDirectory.FullName + "\\EXO\\Images").FullName, "qmImage.png");

		// Token: 0x040002CD RID: 717
		internal static string mmPath = Path.Combine(Directory.CreateDirectory(AppStart.HexedDirectory.FullName + "\\EXO\\Images").FullName, "mmImage.png");

		// Token: 0x040002CE RID: 718
		private static WebClient web = new WebClient();

		// Token: 0x040002CF RID: 719
		private static Image mmImage;

		// Token: 0x040002D0 RID: 720
		private static Image qmImage;
	}
}
