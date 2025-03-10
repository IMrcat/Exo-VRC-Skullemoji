using System;
using EXO.Core;
using EXO.Functions.Render;
using EXO.LogTools;
using EXO.Menus.SubMenus;
using EXO.Wrappers;
using UnityEngine;
using VRC;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Groups;

namespace EXO.Menus.SelectMenus
{
	// Token: 0x02000065 RID: 101
	internal class ListenerSelect : MenuModule
	{
		// Token: 0x0600037B RID: 891 RVA: 0x00013073 File Offset: 0x00011273
		internal static void AddAction()
		{
			VRCPage vrcpage = ListenerSelect.selectPage;
			vrcpage.OnMenuOpen = (Action)Delegate.Combine(vrcpage.OnMenuOpen, delegate
			{
				SelectMenu.UpdateDisplayTarget(ListenerSelect.target, ListenerSelect.selectPage);
			});
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000130B0 File Offset: 0x000112B0
		public override void LoadMenu()
		{
			ListenerSelect.selectPage = new VRCPage("Listener Menu", false, true, false, null, "", null, false);
			CollapsibleButtonGroup listenerSelGrp = new CollapsibleButtonGroup(ListenerSelect.selectPage, "Listener Menu", true);
			ListenerSelect.AddAction();
			ListenerSelect.capsuleToggleSel = new VRCToggle(listenerSelGrp, "Capsule ESP", delegate(bool val)
			{
				if (val)
				{
					VisualsMenu.capsuleToggle.State = false;
					ListenerSelect.capsuleTarget = PlayerWrapper.GetSelectedUser;
				}
				bool flag = ListenerSelect.capsuleTarget == null;
				if (flag)
				{
					ListenerSelect.capsuleTarget = ListenerSelect.target;
				}
				ESPs.CapsuleHighlight(ListenerSelect.capsuleTarget, val);
				if (val)
				{
					CLog.L("Enabled Capsule ESP on " + ListenerSelect.capsuleTarget.DisplayName(), true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\ListenerSelect.cs", 51);
				}
				else
				{
					CLog.L("Disabled Capsule ESP on " + ListenerSelect.capsuleTarget.DisplayName(), true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\ListenerSelect.cs", 53);
				}
			}, false, "Off", "On", null, null, false);
			ListenerSelect.lineToggleSel = new VRCToggle(listenerSelGrp, "Line ESP", delegate(bool val)
			{
				if (val)
				{
					VisualsMenu.lineToggle.State = false;
					ListenerSelect.lineTarget = PlayerWrapper.GetSelectedUser;
				}
				bool flag2 = ListenerSelect.lineTarget == null;
				if (flag2)
				{
					ListenerSelect.lineTarget = ListenerSelect.target;
				}
				ListenerSelect.lineSelectESP = val;
				bool flag3 = ListenerSelect.lineSelectESP;
				if (flag3)
				{
					LineESP.EnableListenerLine(ListenerSelect.lineTarget);
					CLog.L("Enabled Line ESP on " + ListenerSelect.lineTarget.DisplayName(), true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\ListenerSelect.cs", 73);
				}
				else
				{
					LineESP.DisableLines();
					CLog.L("Disabled Line ESP on " + ListenerSelect.lineTarget.DisplayName(), true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\ListenerSelect.cs", 78);
				}
			}, false, "Off", "On", null, null, false);
			ListenerSelect.boneToggleSel = new VRCToggle(listenerSelGrp, "Bone ESP", delegate(bool val)
			{
				if (val)
				{
					VisualsMenu.boneToggle.State = false;
					BoneESP.listenerTarget = PlayerWrapper.GetSelectedUser;
				}
				bool flag4 = BoneESP.listenerTarget == null;
				if (flag4)
				{
					BoneESP.listenerTarget = ListenerSelect.target;
				}
				ListenerSelect.boneSelectESP = val;
				bool flag5 = !ListenerSelect.boneSelectESP && BoneESP.listenerTarget.gameObject.GetComponent<LineRenderer>();
				if (flag5)
				{
					BoneESP.listenerTarget.gameObject.GetComponent<LineRenderer>().enabled = false;
				}
				bool flag6 = ListenerSelect.boneSelectESP;
				if (flag6)
				{
					CLog.L("Enabled Line ESP on " + BoneESP.listenerTarget.DisplayName(), true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\ListenerSelect.cs", 100);
				}
				else
				{
					CLog.L("Disabled Line ESP on " + BoneESP.listenerTarget.DisplayName(), true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\ListenerSelect.cs", 102);
				}
			}, false, "Off", "On", null, null, false);
			new VRCToggle(listenerSelGrp, "Hear Voice", delegate(bool val)
			{
				if (val)
				{
					ListenerSelect.voiceTarget = PlayerWrapper.GetSelectedUser;
					ListenerSelect.voiceTarget.GetVRCPlayerApi().SetVoiceDistanceFar(1000f);
					ListenerSelect.voiceTarget.GetVRCPlayerApi().SetVoiceDistanceNear(999f);
					CLog.L("Listening to " + ListenerSelect.voiceTarget.DisplayName() + "...", true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\ListenerSelect.cs", 113);
				}
				else
				{
					bool flag7 = ListenerSelect.voiceTarget == null;
					if (flag7)
					{
						ListenerSelect.voiceTarget = ListenerSelect.target;
					}
					ListenerSelect.voiceTarget.GetVRCPlayerApi().SetVoiceDistanceFar(40f);
					ListenerSelect.voiceTarget.GetVRCPlayerApi().SetVoiceDistanceNear(0f);
					CLog.L("Stopped listening to " + ListenerSelect.voiceTarget.DisplayName(), true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\ListenerSelect.cs", 122);
				}
				bool flag8 = ListenerSelect.voiceTarget == null;
				if (flag8)
				{
					ListenerSelect.voiceTarget = ListenerSelect.target;
				}
			}, false, "Off", "On", null, null, false);
		}

		// Token: 0x040001AF RID: 431
		internal static Player target;

		// Token: 0x040001B0 RID: 432
		public static VRCPage selectPage;

		// Token: 0x040001B1 RID: 433
		internal static VRCToggle capsuleToggleSel;

		// Token: 0x040001B2 RID: 434
		internal static VRCToggle lineToggleSel;

		// Token: 0x040001B3 RID: 435
		internal static VRCToggle boneToggleSel;

		// Token: 0x040001B4 RID: 436
		internal static bool lineSelectESP;

		// Token: 0x040001B5 RID: 437
		internal static bool boneSelectESP;

		// Token: 0x040001B6 RID: 438
		internal static Player capsuleTarget;

		// Token: 0x040001B7 RID: 439
		internal static Player lineTarget;

		// Token: 0x040001B8 RID: 440
		internal static Player voiceTarget;
	}
}
