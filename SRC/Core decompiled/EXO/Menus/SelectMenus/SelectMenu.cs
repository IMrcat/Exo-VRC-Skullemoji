using System;
using EXO.Core;
using EXO.LogTools;
using EXO.Wrappers;
using UnityEngine;
using VRC;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons.Groups;
using WorldAPI.ButtonAPI.Groups;

namespace EXO.Menus.SelectMenus
{
	// Token: 0x02000067 RID: 103
	internal class SelectMenu : MenuModule
	{
		// Token: 0x06000381 RID: 897 RVA: 0x000134FC File Offset: 0x000116FC
		internal static void UpdateDisplayTarget(Player selectedTarget, VRCPage page)
		{
			bool flag = selectedTarget != null && page != null;
			if (flag)
			{
				page.pageTitleText.text = "Target: " + selectedTarget.DisplayName();
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001353C File Offset: 0x0001173C
		public override void LoadLate()
		{
			SelectMenu.parent = GameObject.Find("Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup").transform;
			SelectMenu.mainSelPage = GameObject.Find("Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions").gameObject;
			SelectMenu.selectPage = new VRCPage("<color=#9b0000>EXO</color>", false, true, false, null, "", null, false);
			SelectMenu.playerSelGrp = new CollapsibleButtonGroup(SelectMenu.parent, "<color=#9b0000>EXO</color>", true);
			SelectMenu.playerSelGrp.headerObj.gameObject.transform.SetAsFirstSibling();
			SelectMenu.playerSelGrp.gameObject.transform.SetAsFirstSibling();
			new DuoButtons(SelectMenu.playerSelGrp, "Force Clone", "Force Clone User's Avatar", delegate
			{
				SelectMenu.target = PlayerWrapper.GetSelectedUser;
				CLog.L(PlayerWrapper.GetSelectedUser.ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\SelectMenu.cs", 101);
				string activeAvatarId = SelectMenu.target.GetAPIAvatar().id;
				bool flag = SelectMenu.target.GetAPIAvatar().releaseStatus != "private";
				if (flag)
				{
					CLog.L("Changing into " + activeAvatarId, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\SelectMenu.cs", 105);
					GUILog.DisplayOnScreen("Changing into " + SelectMenu.target.GetAPIAvatar().name);
					PlayerWrapper.ChangeAvatar(activeAvatarId);
				}
				else
				{
					CLog.L("Avatar ID " + activeAvatarId + " is private!", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\SelectMenu.cs", 110);
					GUILog.DisplayOnScreen(SelectMenu.target.GetAPIAvatar().name + " is private");
				}
			}, "Attach Menu", "Opens Player Attach Menu", delegate
			{
				AttachSelect.target = PlayerWrapper.GetSelectedUser;
				AttachSelect.selectPage.OpenMenu();
			});
			new DuoButtons(SelectMenu.playerSelGrp, "Utils Menu", "Opens Utils Menu", delegate
			{
				UtilsSelect.target = PlayerWrapper.GetSelectedUser;
				UtilsSelect.selectPage.OpenMenu();
			}, "Orbit Menu", "Opens Item Orbit Menu", delegate
			{
				OrbitSelect.target = PlayerWrapper.GetSelectedUser;
				OrbitSelect.selectPage.OpenMenu();
			});
			new DuoButtons(SelectMenu.playerSelGrp, "Listener Menu", "Opens Listener Menu", delegate
			{
				ListenerSelect.target = PlayerWrapper.GetSelectedUser;
				ListenerSelect.selectPage.OpenMenu();
			}, "Bring Menu", "Opens Bring Items Menu", delegate
			{
				BringItems.target = PlayerWrapper.GetSelectedUser;
				BringItems.MakeMenu();
			});
			new DuoButtons(SelectMenu.playerSelGrp, "Teleport", "Teleports you to User", delegate
			{
				SelectMenu.target = PlayerWrapper.GetSelectedUser;
				PlayerWrapper.GetLocalPlayer().transform.position = SelectMenu.target.transform.position;
			}, "World Cheats", "Open World Cheats Menu", delegate
			{
				WorldSelect.target = PlayerWrapper.GetSelectedUser;
				WorldSelect.selectPage.OpenMenu();
			});
		}

		// Token: 0x040001BC RID: 444
		internal static Player target;

		// Token: 0x040001BD RID: 445
		internal static Transform parent;

		// Token: 0x040001BE RID: 446
		internal static CollapsibleButtonGroup playerSelGrp;

		// Token: 0x040001BF RID: 447
		internal static GameObject mainSelPage;

		// Token: 0x040001C0 RID: 448
		internal static VRCPage selectPage;
	}
}
