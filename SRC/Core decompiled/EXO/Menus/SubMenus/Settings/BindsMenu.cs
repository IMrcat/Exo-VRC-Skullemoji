using System;
using System.Runtime.CompilerServices;
using EXO.Core;
using UnityEngine;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.QM.Carousel;
using WorldAPI.ButtonAPI.QM.Carousel.Items;

namespace EXO.Menus.SubMenus.Settings
{
	// Token: 0x02000060 RID: 96
	internal class BindsMenu : MenuModule
	{
		// Token: 0x0600036F RID: 879 RVA: 0x00011F34 File Offset: 0x00010134
		public override void LoadMenu()
		{
			BindsMenu.subMenu = new VRCPage("Keybinds", false, true, false, null, "", null, false);
			QMCGroup keybindsGrp = new QMCGroup(BindsMenu.subMenu, "Desktop Keybinds", TextAnchor.UpperLeft);
			QMCGroup qmcgroup = keybindsGrp;
			string text = "List Keybinds";
			string text2 = "Lists all you keybinds in the console";
			Action action;
			if ((action = BindsMenu.<>O.<0>__WriteConfig) == null)
			{
				action = (BindsMenu.<>O.<0>__WriteConfig = new Action(Keybindings.WriteConfig));
			}
			new QMCFuncButton(qmcgroup, text, text2, action);
			new QMCToggle(keybindsGrp, "Anti Udon", delegate(bool val)
			{
				Config.cfg.AntiUdonBind = val;
			}, Config.cfg.AntiUdonBind, "", false);
			new QMCToggle(keybindsGrp, "Double Jump Fly", delegate(bool val)
			{
				Config.cfg.MinecraftFly = val;
			}, Config.cfg.MinecraftFly, "", false);
			new QMCToggle(keybindsGrp, "Fly", delegate(bool val)
			{
				Config.cfg.FlyBind = val;
			}, Config.cfg.FlyBind, "", false);
			new QMCToggle(keybindsGrp, "No Clip", delegate(bool val)
			{
				Config.cfg.NoClipBind = val;
			}, Config.cfg.NoClipBind, "", false);
			new QMCToggle(keybindsGrp, "Speed", delegate(bool val)
			{
				Config.cfg.SpeedBind = val;
			}, Config.cfg.SpeedBind, "", false);
			new QMCToggle(keybindsGrp, "Zoom", delegate(bool val)
			{
				Config.cfg.ZoomBind = val;
			}, Config.cfg.ZoomBind, "", false);
			new QMCToggle(keybindsGrp, "3rd Person", delegate(bool val)
			{
				Config.cfg.ThirdPersonBind = val;
			}, Config.cfg.ThirdPersonBind, "", false);
			new QMCToggle(keybindsGrp, "Mouse TP", delegate(bool val)
			{
				Config.cfg.MouseTpBind = val;
			}, Config.cfg.MouseTpBind, "", false);
			new QMCToggle(keybindsGrp, "Serialization", delegate(bool val)
			{
				Config.cfg.SerializationBind = val;
			}, Config.cfg.SerializationBind, "", false);
			new QMCToggle(keybindsGrp, "Head Break", delegate(bool val)
			{
				Config.cfg.HeadFlipper = val;
			}, Config.cfg.HeadFlipper, "", false);
			QMCGroup vrKeybindsGrp = new QMCGroup(BindsMenu.subMenu, "VR Keybinds", TextAnchor.UpperLeft);
			new QMCToggle(vrKeybindsGrp, "VR Fly", delegate(bool val)
			{
				Config.cfg.FlyVRBind = val;
			}, Config.cfg.FlyVRBind, "", false);
			new QMCToggle(vrKeybindsGrp, "VR No Clip", delegate(bool val)
			{
				Config.cfg.NoClipVRBind = val;
			}, Config.cfg.NoClipVRBind, "", false);
			new QMCToggle(vrKeybindsGrp, "VR Speed", delegate(bool val)
			{
				Config.cfg.SpeedVRBind = val;
			}, Config.cfg.SpeedVRBind, "", false);
		}

		// Token: 0x0400019F RID: 415
		public static VRCPage subMenu;

		// Token: 0x02000123 RID: 291
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040004F5 RID: 1269
			public static Action <0>__WriteConfig;
		}
	}
}
