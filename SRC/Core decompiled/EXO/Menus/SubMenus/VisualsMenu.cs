using System;
using CoreRuntime.Manager;
using EXO.Core;
using EXO.Functions.Render;
using EXO.Menus.SelectMenus;
using EXO.Menus.SubMenus.Visuals;
using EXO.Modules;
using EXO_Base;
using UnityEngine;
using VRC;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Groups;
using WorldAPI.ButtonAPI.QM.Carousel;
using WorldAPI.ButtonAPI.QM.Carousel.Items;

namespace EXO.Menus.SubMenus
{
	// Token: 0x0200004F RID: 79
	internal class VisualsMenu : MenuModule
	{
		// Token: 0x06000334 RID: 820 RVA: 0x0000E2B4 File Offset: 0x0000C4B4
		public override void LoadMenu()
		{
			VisualsMenu.subMenu = new VRCPage("Visuals", false, true, false, null, "", null, false);
			CollapsibleButtonGroup visualsGrp = new CollapsibleButtonGroup(VisualsMenu.subMenu, "Visuals", false);
			new VRCButton(visualsGrp, "Camera", "Open Camera Menu", delegate
			{
				CamMenu.subMenu.OpenMenu();
			}, false, true, BaseImages.FromBase(BaseImages.EyeIcon), ExtentedControl.HalfType.Normal, false);
			new VRCToggle(visualsGrp, "NamePlates", delegate(bool val)
			{
				Config.cfg.NamePlates = val;
				if (val)
				{
					NamePlates.EnableTags();
					bool flag = !NamePlates.isUpdating;
					if (flag)
					{
						CoroutineManager.RunCoroutine(NamePlates.UpdateNamePlate());
					}
				}
				else
				{
					NamePlates.DisableTags();
					bool isUpdating = NamePlates.isUpdating;
					if (isUpdating)
					{
						CoroutineManager.StopCoroutine(NamePlates.UpdateNamePlate());
					}
				}
			}, Config.cfg.NamePlates, "Off", "On", null, null, false);
			new VRCToggle(visualsGrp, "HUD FPS", delegate(bool val)
			{
				Config.cfg.DisplayHUD = val;
				if (val)
				{
					bool flag2 = !HudDisplay.isUpdating;
					if (flag2)
					{
						CoroutineManager.RunCoroutine(HudDisplay.UpdateDisplay());
					}
					HudDisplay.display.alpha = 0.7f;
				}
				else
				{
					bool isUpdating2 = HudDisplay.isUpdating;
					if (isUpdating2)
					{
						CoroutineManager.StopCoroutine(HudDisplay.UpdateDisplay());
					}
					HudDisplay.display.alpha = 0f;
					HudDisplay.isUpdating = false;
				}
			}, Config.cfg.DisplayHUD, "Off", "On", null, null, false);
			QMCGroup espGrp = new QMCGroup(VisualsMenu.subMenu, "ESP", TextAnchor.UpperLeft);
			new QMCToggle(espGrp, "Use Rank Colors", delegate(bool val)
			{
				VisualsMenu.capsuleToggle.State = false;
				Config.cfg.TrustColors = val;
			}, Config.cfg.TrustColors, "", false);
			VisualsMenu.capsuleToggle = new QMCToggle(espGrp, "Capsule ESP", delegate(bool val)
			{
				ListenerSelect.capsuleToggleSel.State = false;
				ESPs.playerCapsuleESP = val;
				foreach (Player player in PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0)
				{
					ESPs.CapsuleHighlight(player, ESPs.playerCapsuleESP);
				}
			}, false, "", false);
			VisualsMenu.lineToggle = new QMCToggle(espGrp, "Line ESP", delegate(bool val)
			{
				ListenerSelect.lineToggleSel.State = false;
				LineESP.ESP = val;
				bool esp = LineESP.ESP;
				if (esp)
				{
					LineESP.EnableLines();
				}
				else
				{
					LineESP.DisableLines();
				}
			}, false, "", false);
			VisualsMenu.boneToggle = new QMCToggle(espGrp, "Bone ESP", delegate(bool val)
			{
				ListenerSelect.boneToggleSel.State = false;
				BoneESP.render = val;
				bool flag3 = !val;
				if (flag3)
				{
					BoneESP.DisableRenders();
				}
			}, false, "", false);
			new QMCToggle(espGrp, "Item ESP", delegate(bool val)
			{
				ESPs.itemESP = val;
				CoroutineManager.RunCoroutine(ESPs.ItemHighlight());
			}, false, "", false);
			new QMCToggle(espGrp, "Trigger ESP", delegate(bool val)
			{
				ESPs.triggerESP = val;
				CoroutineManager.RunCoroutine(ESPs.TriggerHighlight());
			}, false, "", false);
			new QMCToggle(espGrp, "Interactable ESP", delegate(bool val)
			{
				ESPs.interactESP = val;
				CoroutineManager.RunCoroutine(ESPs.IntractableHighlight());
			}, false, "", false);
			new QMCToggle(espGrp, "Box Collider ESP", delegate(bool val)
			{
				ESPs.boxColESP = val;
				CoroutineManager.RunCoroutine(ESPs.BoxColliderHighlight());
			}, false, "", false);
			new QMCToggle(espGrp, "Rigidbody ESP", delegate(bool val)
			{
				ESPs.rigidbodyESP = val;
				CoroutineManager.RunCoroutine(ESPs.RigidbodyHighlight());
			}, false, "", false);
			new QMCToggle(espGrp, "Udon Sync ESP", delegate(bool val)
			{
				ESPs.udonESP = val;
				CoroutineManager.RunCoroutine(ESPs.UdonHighlight());
			}, false, "", false);
		}

		// Token: 0x04000170 RID: 368
		public static VRCPage subMenu;

		// Token: 0x04000171 RID: 369
		internal static QMCToggle capsuleToggle;

		// Token: 0x04000172 RID: 370
		internal static QMCToggle lineToggle;

		// Token: 0x04000173 RID: 371
		internal static QMCToggle boneToggle;
	}
}
