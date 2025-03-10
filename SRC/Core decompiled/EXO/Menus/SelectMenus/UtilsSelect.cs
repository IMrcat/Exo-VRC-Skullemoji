using System;
using System.Linq;
using EXO.Core;
using EXO.Functions.Avatar;
using EXO.LogTools;
using EXO.Modules.API;
using EXO.Modules.Utilities;
using EXO.Wrappers;
using VRC;
using VRC.Core;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Buttons.Groups;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Groups;

namespace EXO.Menus.SelectMenus
{
	// Token: 0x02000068 RID: 104
	internal class UtilsSelect : MenuModule
	{
		// Token: 0x06000384 RID: 900 RVA: 0x0001374D File Offset: 0x0001194D
		internal static void AddAction()
		{
			VRCPage vrcpage = UtilsSelect.selectPage;
			vrcpage.OnMenuOpen = (Action)Delegate.Combine(vrcpage.OnMenuOpen, delegate
			{
				SelectMenu.UpdateDisplayTarget(UtilsSelect.target, UtilsSelect.selectPage);
			});
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001378C File Offset: 0x0001198C
		public override void LoadMenu()
		{
			UtilsSelect.selectPage = new VRCPage("Utils Menu", false, true, false, null, "", null, false);
			CollapsibleButtonGroup avatarSelGrp = new CollapsibleButtonGroup(UtilsSelect.selectPage, "Utils", true);
			UtilsSelect.AddAction();
			new DuoButtons(avatarSelGrp, "Favorite", "Favorite User's Avatar", delegate
			{
				UtilsSelect.target = PlayerWrapper.GetSelectedUser;
				ApiAvatar avi = UtilsSelect.target.GetAPIAvatar();
				bool flag = avi.releaseStatus.ToLower() == "public";
				if (flag)
				{
					bool flag2 = AviFavs.FavAvis != null;
					if (flag2)
					{
						bool flag3 = !Enumerable.Any<AvatarObject>(AviFavs.FavAvis, (AvatarObject a) => a.id == avi.id);
						if (flag3)
						{
							AviFavs.FavAvi(avi);
							CLog.L(avi.id + " Has been Added to your favorites list", true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\UtilsSelect.cs", 40);
						}
						else
						{
							CLog.L("Avatar " + avi.id + " is already in the favorites list", true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\UtilsSelect.cs", 43);
						}
					}
					else
					{
						CLog.L("FavAvis is null", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\UtilsSelect.cs", 46);
					}
				}
				else
				{
					CLog.L("Warning; Avatar is private (Not Added)", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\UtilsSelect.cs", 50);
					GUILog.DisplayOnScreen(avi.id + " is private");
				}
			}, "UnFavorite", "Unfavorite User's Avatar", delegate
			{
				UtilsSelect.target = PlayerWrapper.GetSelectedUser;
				ApiAvatar avi = UtilsSelect.target.GetAPIAvatar();
				bool flag4 = avi.releaseStatus.ToLower() == "public";
				if (flag4)
				{
					bool flag5 = AviFavs.FavAvis != null;
					if (flag5)
					{
						bool flag6 = Enumerable.Any<AvatarObject>(AviFavs.FavAvis, (AvatarObject a) => a.id == avi.id);
						if (flag6)
						{
							AviFavs.UnFavAvi(avi);
							CLog.L(avi.id + " Has been removed from your favorites list", true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\UtilsSelect.cs", 66);
						}
						else
						{
							CLog.L("Avatar " + avi.id + " is not in the favorites list", true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\UtilsSelect.cs", 69);
						}
					}
					else
					{
						CLog.L("FavAvis is null", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\UtilsSelect.cs", 73);
					}
				}
			});
			new VRCButton(avatarSelGrp, "Force Clone", "Force Clone User's Avatar", delegate
			{
				UtilsSelect.target = PlayerWrapper.GetSelectedUser;
				CLog.L(PlayerWrapper.GetSelectedUser.ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\UtilsSelect.cs", 79);
				string activeAvatarId = UtilsSelect.target.GetAPIAvatar().id;
				bool flag7 = UtilsSelect.target.GetAPIAvatar().releaseStatus != "private";
				if (flag7)
				{
					CLog.L("Changing into " + UtilsSelect.target.GetAPIAvatar().name + " | " + activeAvatarId, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\UtilsSelect.cs", 83);
					GUILog.DisplayOnScreen("Changing into " + UtilsSelect.target.GetAPIAvatar().name);
					PlayerWrapper.ChangeAvatar(activeAvatarId);
				}
				else
				{
					CLog.L("Avatar ID " + activeAvatarId + " is private!", true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\UtilsSelect.cs", 88);
				}
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(avatarSelGrp, "Reload Avatar", "Reload User's Avatar", delegate
			{
				UtilsSelect.target = PlayerWrapper.GetSelectedUser;
				UtilsSelect.target.ReloadAvatar();
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(avatarSelGrp, "Teleport", "Teleports you to User", delegate
			{
				UtilsSelect.target = PlayerWrapper.GetSelectedUser;
				PlayerWrapper.GetLocalPlayer().transform.position = UtilsSelect.target.transform.position;
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(avatarSelGrp, "Copy Avi ID", "Copy User's Avatar ID", delegate
			{
				UtilsSelect.target = PlayerWrapper.GetSelectedUser;
				ApiAvatar avatar = UtilsSelect.target.GetAPIAvatar();
				bool flag8 = avatar != null;
				if (flag8)
				{
					CLog.L(avatar.id, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\UtilsSelect.cs", 106);
					UtilFunc.Clipboard = avatar.id;
					bool flag9 = avatar.releaseStatus == "private";
					if (flag9)
					{
						CLog.L("Avatar id is private", true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\UtilsSelect.cs", 109);
					}
				}
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(avatarSelGrp, "Copy User ID", "Copy User's ID", delegate
			{
				UtilsSelect.target = PlayerWrapper.GetSelectedUser;
				string id = UtilsSelect.target.UserID();
				bool flag10 = id != null;
				if (flag10)
				{
					CLog.L("Copied " + id + " to clipboard", true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\UtilsSelect.cs", 119);
					UtilFunc.Clipboard = id;
				}
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(avatarSelGrp, "Copy Username", "Copy User's Username", delegate
			{
				UtilsSelect.target = PlayerWrapper.GetSelectedUser;
				string name = UtilsSelect.target.DisplayName();
				bool flag11 = name != null;
				if (flag11)
				{
					CLog.L("Copied " + name + " to clipboard", true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SelectMenus\\UtilsSelect.cs", 129);
					UtilFunc.Clipboard = name;
				}
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
		}

		// Token: 0x040001C1 RID: 449
		internal static Player target;

		// Token: 0x040001C2 RID: 450
		public static VRCPage selectPage;
	}
}
