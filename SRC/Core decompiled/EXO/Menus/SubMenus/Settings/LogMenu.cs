using System;
using EXO.Core;
using EXO.LogTools;
using EXO.Patches;
using UnityEngine;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.QM.Carousel;
using WorldAPI.ButtonAPI.QM.Carousel.Items;

namespace EXO.Menus.SubMenus.Settings
{
	// Token: 0x02000061 RID: 97
	internal class LogMenu : MenuModule
	{
		// Token: 0x06000371 RID: 881 RVA: 0x000122B4 File Offset: 0x000104B4
		public override void LoadMenu()
		{
			LogMenu.subMenu = new VRCPage("Logging", false, true, false, null, "", null, false);
			QMCGroup logGrp = new QMCGroup(LogMenu.subMenu, "Logs", TextAnchor.UpperLeft);
			LogMenu.logGodModeBtn = new QMCToggle(logGrp, "God Mode Logs", delegate(bool val)
			{
				Config.cfg.GodModeLogger = val;
			}, Config.cfg.GodModeLogger, "", false);
			LogMenu.logUdonBtn = new QMCToggle(logGrp, "Log Udon", delegate(bool val)
			{
				Config.cfg.UdonLogger = val;
			}, Config.cfg.UdonLogger, "", false);
			new QMCToggle(logGrp, "Screen Logs", delegate(bool val)
			{
				Config.cfg.ScreenLogger = val;
				if (val)
				{
					GUILog.logText.alpha = 1f;
				}
				else
				{
					GUILog.logText.alpha = 0f;
				}
			}, Config.cfg.ScreenLogger, "", false);
			new QMCToggle(logGrp, "Fade Screen Logs", delegate(bool val)
			{
				Config.cfg.ScreenLogFade = val;
			}, Config.cfg.ScreenLogFade, "", false);
			new QMCToggle(logGrp, "Bold Screen Logs", delegate(bool val)
			{
				Config.cfg.BoldScreenLogs = val;
			}, Config.cfg.BoldScreenLogs, "", false);
			QMCGroup logFriendsGrp = new QMCGroup(LogMenu.subMenu, "Friends", TextAnchor.UpperLeft);
			new QMCToggle(logFriendsGrp, "Log To HUD", delegate(bool val)
			{
				Config.cfg.LogFriendsToHUD = val;
			}, Config.cfg.LogFriendsToHUD, "", false);
			new QMCToggle(logFriendsGrp, "Log Activity", delegate(bool val)
			{
				Config.cfg.LogFriendActivity = val;
			}, Config.cfg.LogFriendActivity, "", false);
			new QMCToggle(logFriendsGrp, "Log Locations", delegate(bool val)
			{
				Config.cfg.LogFriendLocations = val;
			}, Config.cfg.LogFriendLocations, "", false);
			new QMCToggle(logFriendsGrp, "Log Instance Info", delegate(bool val)
			{
				Config.cfg.LogFriendInstanceInfo = val;
			}, Config.cfg.LogFriendInstanceInfo, "", false);
			QMCGroup eventLogGrp = new QMCGroup(LogMenu.subMenu, "Events", TextAnchor.UpperLeft);
			LogMenu.logEventsBtn = new QMCToggle(eventLogGrp, "Log Events", delegate(bool val)
			{
				EventPatch.EventLog = val;
			}, EventPatch.EventLog, "", false);
			new QMCToggle(eventLogGrp, "Log E1", delegate(bool val)
			{
				LogEvent.e1 = val;
			}, false, "", false);
			new QMCToggle(eventLogGrp, "Log E4", delegate(bool val)
			{
				LogEvent.e4 = val;
			}, false, "", false);
			new QMCToggle(eventLogGrp, "Log E6", delegate(bool val)
			{
				LogEvent.e6 = val;
			}, false, "", false);
			new QMCToggle(eventLogGrp, "Log E7", delegate(bool val)
			{
				LogEvent.e7 = val;
			}, false, "", false);
			new QMCToggle(eventLogGrp, "Log E12", delegate(bool val)
			{
				LogEvent.e12 = val;
			}, false, "", false);
			new QMCToggle(eventLogGrp, "Log E209", delegate(bool val)
			{
				LogEvent.e209 = val;
			}, false, "", false);
			new QMCToggle(eventLogGrp, "Log E210", delegate(bool val)
			{
				LogEvent.e210 = val;
			}, false, "", false);
		}

		// Token: 0x040001A0 RID: 416
		public static VRCPage subMenu;

		// Token: 0x040001A1 RID: 417
		internal static QMCToggle logUdonBtn;

		// Token: 0x040001A2 RID: 418
		internal static QMCToggle logGodModeBtn;

		// Token: 0x040001A3 RID: 419
		internal static QMCToggle logEventsBtn;
	}
}
