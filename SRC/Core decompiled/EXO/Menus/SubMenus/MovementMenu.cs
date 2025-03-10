using System;
using EXO.Core;
using EXO.Functions.PlayerFunc;
using EXO.LogTools;
using UnityEngine;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Groups;
using WorldAPI.ButtonAPI.QM.Extras;

namespace EXO.Menus.SubMenus
{
	// Token: 0x0200004C RID: 76
	internal class MovementMenu : MenuModule
	{
		// Token: 0x0600032C RID: 812 RVA: 0x0000DC88 File Offset: 0x0000BE88
		public override void LoadMenu()
		{
			MovementMenu.subMenu = new VRCPage("Movement", false, true, false, null, "", null, false);
			ButtonGroup movementGrp = new ButtonGroup(MovementMenu.subMenu, "Movement", false, TextAnchor.UpperCenter);
			new VRCSlider(MovementMenu.subMenu, "Fly Speed : 3.0", "Changes your fly speed modifier", delegate(float val, VRCSlider s)
			{
				MoveFunc.flySpeed = val;
				s.TextMeshPro.text = "Fly Speed : " + val.ToString("F1");
			}, 3f, 0f, 20f).Button(delegate(VRCSlider s)
			{
				MoveFunc.flySpeed = 3f;
				s.snapSlider.value = 3f;
				CLog.L("Reset fly speed to " + MoveFunc.flySpeed.ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\MovementMenu.cs", 50);
			}, "Reset to 3", null);
			new VRCSlider(MovementMenu.subMenu, "Speed : 3.0", "Changes your speed modifier", delegate(float val, VRCSlider s)
			{
				MoveFunc.speedVal = val;
				s.TextMeshPro.text = "Speed : " + val.ToString("F1");
			}, 3f, 0f, 10f).Button(delegate(VRCSlider s)
			{
				MoveFunc.speedVal = 3f;
				s.snapSlider.value = 3f;
				CLog.L("Reset speed to " + MoveFunc.speedVal.ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\MovementMenu.cs", 61);
			}, "Reset to 3", null);
			MovementMenu.speedBtn = new VRCToggle(movementGrp, "Speed", delegate(bool val)
			{
				Config.cfg.SpeedToggle = val;
			}, Config.cfg.SpeedToggle, "Off", "On", null, null, false);
			MovementMenu.noClipBtn = new VRCToggle(movementGrp, "No Clip", delegate(bool val)
			{
				bool flag = !Config.cfg.FlyToggle && val;
				if (flag)
				{
					MovementMenu.flyBtn.State = true;
				}
				MoveFunc.noClipToggle = val;
				MoveFunc.NoClip();
			}, MoveFunc.noClipToggle, "Off", "On", null, null, false);
			MovementMenu.flyBtn = new VRCToggle(movementGrp, "Fly", delegate(bool val)
			{
				bool noClipToggle = MoveFunc.noClipToggle;
				if (noClipToggle)
				{
					MovementMenu.noClipBtn.State = false;
				}
				Physics.gravity = ((AttachToPlayer.isAttached || val) ? new Vector3(0f, 0f, 0f) : new Vector3(0f, -9.81f, 0f));
				Config.cfg.FlyToggle = val;
			}, Config.cfg.FlyToggle, "Off", "On", null, null, false);
			new VRCToggle(movementGrp, "JetPack", delegate(bool val)
			{
				Config.cfg.JetPackToggle = val;
			}, Config.cfg.JetPackToggle, "Off", "On", null, null, false);
			new VRCToggle(movementGrp, "Auto Force Jump", delegate(bool val)
			{
				Config.cfg.AutoJumpState = val;
				MoveFunc.ForceJump();
			}, Config.cfg.AutoJumpState, "Off", "On", null, null, false);
		}

		// Token: 0x04000165 RID: 357
		public static VRCPage subMenu;

		// Token: 0x04000166 RID: 358
		internal static VRCToggle speedBtn;

		// Token: 0x04000167 RID: 359
		internal static VRCToggle noClipBtn;

		// Token: 0x04000168 RID: 360
		internal static VRCToggle flyBtn;
	}
}
