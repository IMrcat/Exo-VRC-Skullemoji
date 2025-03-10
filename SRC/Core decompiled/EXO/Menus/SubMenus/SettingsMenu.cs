using System;
using EXO.Core;
using EXO.LogTools;
using EXO.Menus.SubMenus.Settings;
using EXO.Patches;
using EXO_Base;
using UnityEngine;
using VRC.SDKBase;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Groups;

namespace EXO.Menus.SubMenus
{
	// Token: 0x0200004D RID: 77
	internal class SettingsMenu : MenuModule
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0000DEF8 File Offset: 0x0000C0F8
		public override void LoadMenu()
		{
			SettingsMenu.subMenu = new VRCPage("Settings", false, true, false, null, "", null, false);
			ButtonGroup settingGrp = new ButtonGroup(SettingsMenu.subMenu, "Settings", false, TextAnchor.UpperCenter);
			new VRCButton(settingGrp, "KeyBinds", "Open KeyBinds Menu", delegate
			{
				BindsMenu.subMenu.OpenMenu();
			}, false, true, BaseImages.FromBase(BaseImages.IconDefault), ExtentedControl.HalfType.Normal, false);
			new VRCButton(settingGrp, "Logs", "Open Logs Menu", delegate
			{
				LogMenu.subMenu.OpenMenu();
			}, false, true, BaseImages.FromBase(BaseImages.IconSub), ExtentedControl.HalfType.Normal, false);
			new VRCButton(settingGrp, "Menu Customs", "Open Menu Customs Menu", delegate
			{
				MenuMenu.subMenu.OpenMenu();
			}, false, true, BaseImages.FromBase(BaseImages.IconSub), ExtentedControl.HalfType.Normal, false);
			new VRCToggle(settingGrp, "HWID Spoof", delegate(bool val)
			{
				Config.cfg.IdSpoof = val;
			}, Config.cfg.IdSpoof, "Need to Restart Game", "On", null, null, false);
			new VRCToggle(settingGrp, "Anti Block", delegate(bool val)
			{
				CLog.L(Networking.LocalPlayer.GetAvatarEyeHeightAsMeters().ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\SettingsMenu.cs", 45);
				Config.cfg.AntiBlock = val;
			}, Config.cfg.AntiBlock, "Off", "On", null, null, false);
			SettingsMenu.antiUdonBtn = new VRCToggle(settingGrp, "Anti Udon", delegate(bool val)
			{
				UdonSyncPatch.antiUdon = val;
			}, false, "Off", "On", null, null, false);
		}

		// Token: 0x04000169 RID: 361
		public static VRCPage subMenu;

		// Token: 0x0400016A RID: 362
		internal static VRCToggle antiUdonBtn;
	}
}
