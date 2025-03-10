using System;
using System.Runtime.CompilerServices;
using EXO.Core;
using EXO.Functions.MenuOverrides;
using UnityEngine;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Groups;
using WorldAPI.ButtonAPI.QM.Extras;

namespace EXO.Menus.SubMenus.Settings
{
	// Token: 0x02000062 RID: 98
	internal class MenuMenu : MenuModule
	{
		// Token: 0x06000373 RID: 883 RVA: 0x000126CC File Offset: 0x000108CC
		public override void LoadMenu()
		{
			MenuMenu.subMenu = new VRCPage("Menus", false, true, false, null, "", null, false);
			ButtonGroup menuGrp = new ButtonGroup(MenuMenu.subMenu, "Settings", false, TextAnchor.UpperCenter);
			new VRCToggle(menuGrp, "QM Console", delegate(bool val)
			{
				Config.cfg.QMConsole = val;
			}, Config.cfg.QMConsole, "Off", "On", null, null, false);
			new VRCToggle(menuGrp, "Bold QM Console", delegate(bool val)
			{
				Config.cfg.BoldQMConsole = val;
			}, Config.cfg.BoldQMConsole, "Off", "On", null, null, false);
			new VRCToggle(menuGrp, "QM Image", delegate(bool val)
			{
				Config.cfg.QMBackground = val;
				MenuBG.ApplyMenuImages();
			}, Config.cfg.QMBackground, "Off", "On", null, null, false);
			new VRCToggle(menuGrp, "MM Image", delegate(bool val)
			{
				Config.cfg.MMBackground = val;
				MenuBG.ApplyMenuImages();
			}, Config.cfg.MMBackground, "Off", "On", null, null, false);
			new VRCToggle(menuGrp, "Tab Extender", delegate(bool val)
			{
				Config.cfg.TabExtension = val;
			}, Config.cfg.TabExtension, "Off", "On", null, null, false);
			VRCPage vrcpage = MenuMenu.subMenu;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(15, 1);
			defaultInterpolatedStringHandler.AppendLiteral("Tabs Per Row : ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(Config.cfg.TabsPerRow);
			new VRCSlider(vrcpage, defaultInterpolatedStringHandler.ToStringAndClear(), "Adjust the tabs per row", delegate(float val, VRCSlider s)
			{
				Config.cfg.TabsPerRow = (int)val;
				TabExtender.RecalculateLayout();
				s.TextMeshPro.text = "Tabs Per Row : " + Config.cfg.TabsPerRow.ToString();
			}, (float)Config.cfg.TabsPerRow, 1f, 8f).Button(delegate(VRCSlider s)
			{
				Config.cfg.TabsPerRow = 7;
				TabExtender.RecalculateLayout();
				s.snapSlider.value = 7f;
			}, "Reset", null);
			VRCPage vrcpage2 = MenuMenu.subMenu;
			defaultInterpolatedStringHandler..ctor(19, 1);
			defaultInterpolatedStringHandler.AppendLiteral("Background Alpha : ");
			defaultInterpolatedStringHandler.AppendFormatted<float>(Config.cfg.BackgroundTransparency);
			new VRCSlider(vrcpage2, defaultInterpolatedStringHandler.ToStringAndClear(), "Changes the transparency on the menu backgrounds", delegate(float val, VRCSlider s)
			{
				float fVal = val / 100f;
				Config.cfg.BackgroundTransparency = fVal;
				MenuBG.UpdateImageAlpha();
				s.TextMeshPro.text = "Background Alpha : " + Config.cfg.BackgroundTransparency.ToString("F2");
			}, Config.cfg.BackgroundTransparency * 100f, 0f, 100f).Button(delegate(VRCSlider s)
			{
				Config.cfg.BackgroundTransparency = 95f;
				s.snapSlider.value = 95f;
			}, "Reset", null);
			VRCPage vrcpage3 = MenuMenu.subMenu;
			defaultInterpolatedStringHandler..ctor(18, 1);
			defaultInterpolatedStringHandler.AppendLiteral("QM Music Volume : ");
			defaultInterpolatedStringHandler.AppendFormatted<float>(Config.cfg.MenuMusic);
			new VRCSlider(vrcpage3, defaultInterpolatedStringHandler.ToStringAndClear(), "QM Music Volume", delegate(float val, VRCSlider s)
			{
				float volume = val / 100f;
				MenuMusic.audioSource.volume = volume;
				Config.cfg.MenuMusic = volume;
				s.TextMeshPro.text = "QM Music Volume : " + MenuMusic.audioSource.volume.ToString("F2");
			}, Config.cfg.MenuMusic * 100f, 0f, 50f).Button(delegate(VRCSlider s)
			{
				s.snapSlider.value = 50f;
				MenuMusic.audioSource.volume = 0.5f;
				Config.cfg.MenuMusic = 0.5f;
				s.TextMeshPro.text = "QM Music Volume " + MenuMusic.audioSource.volume.ToString("F2");
			}, "Reset", null);
			new VRCSlider(MenuMenu.subMenu, "Loading Volume : 0.2", "Loading Music Volume", delegate(float val, VRCSlider s)
			{
				float volume2 = val / 100f;
				LoadingScreen.audioSource.volume = volume2;
				LoadingScreen.audioSource1.volume = volume2;
				Config.cfg.LoadingMusic = volume2;
				s.TextMeshPro.text = "Loading Volume : " + LoadingScreen.audioSource.volume.ToString("F2");
			}, Config.cfg.LoadingMusic * 100f, 0f, 50f).Button(delegate(VRCSlider s)
			{
				s.snapSlider.value = 20f;
				LoadingScreen.audioSource.volume = 0.2f;
				LoadingScreen.audioSource1.volume = 0.2f;
				Config.cfg.LoadingMusic = 0.2f;
				s.TextMeshPro.text = "Loading Volume " + LoadingScreen.audioSource.volume.ToString("F2");
			}, "Reset", null);
		}

		// Token: 0x040001A4 RID: 420
		public static VRCPage subMenu;
	}
}
