using System;
using System.Runtime.CompilerServices;
using EXO.Core;
using EXO.LogTools;
using EXO.Modules.Utilities;
using EXO.Wrappers;
using TMPro;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.SDKBase;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Buttons.Groups;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Groups;
using WorldAPI.ButtonAPI.QM.Extras;

namespace EXO.Menus.SubMenus
{
	// Token: 0x0200004B RID: 75
	internal class AvatarMenu : MenuModule
	{
		// Token: 0x0600032A RID: 810 RVA: 0x0000DA9C File Offset: 0x0000BC9C
		public override void LoadMenu()
		{
			AvatarMenu.subMenu = new VRCPage("Avatar", false, true, false, null, "", null, false);
			ButtonGroup mainGrp = new ButtonGroup(AvatarMenu.subMenu, "Avatar", false, TextAnchor.UpperCenter);
			new VRCButton(mainGrp, "Avatar\nBy ID", "Changes to the avatar on your clipboard", delegate
			{
				bool flag = UtilFunc.Clipboard.StartsWith("avtr");
				if (flag)
				{
					CLog.L("Changing into " + UtilFunc.Clipboard, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\AvatarMenu.cs", 33);
					try
					{
						PlayerWrapper.ChangeAvatar(UtilFunc.Clipboard);
					}
					catch
					{
						CLog.E("Avatar ID Is Invalid", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\AvatarMenu.cs", 34);
					}
				}
				else
				{
					CLog.L("Clipboard Doesn't hold and avatar id", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\AvatarMenu.cs", 37);
				}
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "Copy\nAvatar ID", "Copy User's Avatar ID", delegate
			{
				ApiAvatar avatar = PlayerWrapper.LocalPlayer.GetAPIAvatar();
				bool flag2 = avatar != null;
				if (flag2)
				{
					CLog.L(avatar.id, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\AvatarMenu.cs", 44);
					UtilFunc.Clipboard = avatar.id;
				}
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "Reload Avatars", "Reloads all avatars", delegate
			{
				foreach (Player Player in PlayerWrapper.GetAllPlayers)
				{
					Player.ReloadAvatar();
				}
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new DuoButtons(mainGrp, "Set to 100m", "Set Avatar height to 100m", delegate
			{
				Networking.LocalPlayer.SetAvatarEyeHeightByMeters(100f);
			}, "Set to 0.1m", "Set Avatar height to 0.1m", delegate
			{
				Networking.LocalPlayer.SetAvatarEyeHeightByMeters(0.1f);
			});
			new VRCSlider(AvatarMenu.subMenu, "Height Override", "Avatar Height Override", delegate(float val, VRCSlider s)
			{
				TMP_Text textMeshPro = s.TextMeshPro;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(19, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Height Override : ");
				defaultInterpolatedStringHandler.AppendFormatted<float>(val, "F1");
				defaultInterpolatedStringHandler.AppendLiteral("m");
				textMeshPro.text = defaultInterpolatedStringHandler.ToStringAndClear();
				Networking.LocalPlayer.SetAvatarEyeHeightByMeters(val);
			}, 1f, 0.1f, 25f).Button(delegate(VRCSlider s)
			{
				float originalHeight = PlayerWrapper.LocalPlayer.GetVRCAvatarManager().prop_VRCAvatarDescriptor_0.ViewPosition.y;
				Networking.LocalPlayer.SetAvatarEyeHeightByMeters(originalHeight);
				s.snapSlider.value = originalHeight;
			}, "Reset", null);
			new VRCSlider(AvatarMenu.subMenu, "Extended Slider", "Allows adjustment between 25m and 50m", delegate(float val, VRCSlider s)
			{
				TMP_Text textMeshPro2 = s.TextMeshPro;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2;
				defaultInterpolatedStringHandler2..ctor(19, 1);
				defaultInterpolatedStringHandler2.AppendLiteral("Extended Slider : ");
				defaultInterpolatedStringHandler2.AppendFormatted<float>(val, "F1");
				defaultInterpolatedStringHandler2.AppendLiteral("m");
				textMeshPro2.text = defaultInterpolatedStringHandler2.ToStringAndClear();
				Networking.LocalPlayer.SetAvatarEyeHeightByMeters(val);
			}, 0f, 25f, 50f);
		}

		// Token: 0x04000164 RID: 356
		public static VRCPage subMenu;
	}
}
