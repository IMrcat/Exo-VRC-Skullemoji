using System;
using EXO.Core;
using EXO.Functions.PlayerFunc;
using EXO.Wrappers;
using UnityEngine;
using VRC;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Buttons.Groups;
using WorldAPI.ButtonAPI.Groups;
using WorldAPI.ButtonAPI.QM.Extras;

namespace EXO.Menus.SelectMenus
{
	// Token: 0x02000063 RID: 99
	internal class AttachSelect : MenuModule
	{
		// Token: 0x06000375 RID: 885 RVA: 0x00012AB5 File Offset: 0x00010CB5
		internal static void AddAction()
		{
			VRCPage vrcpage = AttachSelect.selectPage;
			vrcpage.OnMenuOpen = (Action)Delegate.Combine(vrcpage.OnMenuOpen, delegate
			{
				SelectMenu.UpdateDisplayTarget(AttachSelect.target, AttachSelect.selectPage);
			});
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00012AF4 File Offset: 0x00010CF4
		public override void LoadMenu()
		{
			AttachSelect.selectPage = new VRCPage("Player Attach", false, true, false, null, "", null, false);
			CollapsibleButtonGroup attachSelGrp = new CollapsibleButtonGroup(AttachSelect.selectPage, "Player Attach", true);
			AttachSelect.AddAction();
			AttachSelect.attachToggle = new VRCToggle(attachSelGrp, "Attach To Player", delegate(bool val)
			{
				AttachToPlayer.isAttached = val;
				AttachSelect.target = PlayerWrapper.GetSelectedUser;
				AttachToPlayer.target = AttachSelect.target;
				Physics.gravity = (AttachToPlayer.isAttached ? new Vector3(0f, 0f, 0f) : new Vector3(0f, -9.81f, 0f));
			}, false, "Off", "On", null, null, false);
			new DuoButtons(attachSelGrp, "Set To Head", "Set To Head", delegate
			{
				AttachSelect.target = PlayerWrapper.GetSelectedUser;
				AttachToPlayer.attachLocation = "head";
			}, "Set To Lap", "Set To Lap", delegate
			{
				AttachSelect.target = PlayerWrapper.GetSelectedUser;
				AttachToPlayer.attachLocation = "hip";
			});
			new DuoButtons(attachSelGrp, "Set To Right Hand", "Set To Right Hand", delegate
			{
				AttachSelect.target = PlayerWrapper.GetSelectedUser;
				AttachToPlayer.attachLocation = "rightHand";
			}, "Set To Left Hand", "Set To Left Hand", delegate
			{
				AttachSelect.target = PlayerWrapper.GetSelectedUser;
				AttachToPlayer.attachLocation = "leftHand";
			});
			new DuoButtons(attachSelGrp, "Set To Right Foot", "Set To Right Foot", delegate
			{
				AttachSelect.target = PlayerWrapper.GetSelectedUser;
				AttachToPlayer.attachLocation = "rightFoot";
			}, "Set To Left Foot", "Set To Left Foot", delegate
			{
				AttachSelect.target = PlayerWrapper.GetSelectedUser;
				AttachToPlayer.attachLocation = "leftFoot";
			});
			AttachSelect.xSlider = new VRCSlider(AttachSelect.selectPage, "X Offset : 0", "Adjust X Offset", delegate(float val, VRCSlider s)
			{
				s.TextMeshPro.text = "X Offset : " + val.ToString("F2");
				AttachToPlayer.xOffset = val;
			}, 0f, -1f, 1f).Button(delegate(VRCSlider s)
			{
				s.snapSlider.value = 0f;
				AttachToPlayer.xOffset = 0f;
			}, "Reset to 0", null);
			AttachSelect.ySlider = new VRCSlider(AttachSelect.selectPage, "Y Offset : 0", "Adjust Y Offset", delegate(float val, VRCSlider s)
			{
				s.TextMeshPro.text = "Y Offset : " + val.ToString("F2");
				AttachToPlayer.yOffset = val;
			}, 0f, -1f, 1f).Button(delegate(VRCSlider s)
			{
				s.snapSlider.value = 0f;
				AttachToPlayer.yOffset = 0f;
			}, "Reset to 0", null);
			AttachSelect.zSlider = new VRCSlider(AttachSelect.selectPage, "Z Offset : 0", "Adjust Z Offset", delegate(float val, VRCSlider s)
			{
				s.TextMeshPro.text = "Z Offset : " + val.ToString("F2");
				AttachToPlayer.zOffset = val;
			}, 0f, -1f, 1f).Button(delegate(VRCSlider s)
			{
				s.snapSlider.value = 0f;
				AttachToPlayer.zOffset = 0f;
			}, "Reset to 0", null);
		}

		// Token: 0x040001A5 RID: 421
		internal static Player target;

		// Token: 0x040001A6 RID: 422
		public static VRCPage selectPage;

		// Token: 0x040001A7 RID: 423
		internal static VRCSlider xSlider;

		// Token: 0x040001A8 RID: 424
		internal static VRCSlider ySlider;

		// Token: 0x040001A9 RID: 425
		internal static VRCSlider zSlider;

		// Token: 0x040001AA RID: 426
		internal static VRCToggle attachToggle;
	}
}
