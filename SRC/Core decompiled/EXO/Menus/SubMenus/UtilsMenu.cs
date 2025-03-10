using System;
using EXO.Core;
using EXO.Functions.Render;
using EXO.LogTools;
using EXO.Modules.Utilities;
using EXO.Wrappers;
using UnityEngine;
using VRC;
using VRC.Networking;
using VRC.SDKBase;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Buttons.Groups;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Groups;

namespace EXO.Menus.SubMenus
{
	// Token: 0x0200004E RID: 78
	internal class UtilsMenu : MenuModule
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000E0B6 File Offset: 0x0000C2B6
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000E0BD File Offset: 0x0000C2BD
		internal static bool FuckUspeak { get; set; }

		// Token: 0x06000332 RID: 818 RVA: 0x0000E0C8 File Offset: 0x0000C2C8
		public override void LoadMenu()
		{
			UtilsMenu.subMenu = new VRCPage("Utils", false, true, false, null, "", null, false);
			ButtonGroup utilsGrp = new ButtonGroup(UtilsMenu.subMenu, "Utils", false, TextAnchor.UpperCenter);
			new VRCButton(utilsGrp, "Copy World ID", "Copys world id to your clipboard", delegate
			{
				CLog.L(RoomManager.field_Internal_Static_ApiWorld_0.id + ":" + WorldWrapper.ApiWorldInstance.instanceId, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\UtilsMenu.cs", 42);
				UtilFunc.Clipboard = RoomManager.field_Internal_Static_ApiWorld_0.id + ":" + WorldWrapper.ApiWorldInstance.instanceId;
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(utilsGrp, "Join World\nBy ID", "Join world by world id from your clipboard", delegate
			{
				string fix = UtilFunc.Clipboard.Replace("&instanceId=", ":");
				bool flag2 = UtilFunc.CheckWorldID(fix, default(int?));
				if (flag2)
				{
					Networking.GoToRoom(fix);
				}
				else
				{
					CLog.L("World ID Is Invalid", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\UtilsMenu.cs", 51);
				}
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(utilsGrp, "Play Video\nBy URL", "Play a video from you clipboard", delegate
			{
				try
				{
					UtilFunc.PlayVideo(UtilFunc.Clipboard);
				}
				catch (Exception)
				{
					CLog.L("Failed to play video", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\UtilsMenu.cs", 55);
				}
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new DuoToggles(utilsGrp, "Right Pointer", "Turn On Controller Tracer", "Turn Off Controller Tracer", delegate(bool val)
			{
				ControllerPointer.isRightLineVisible = val;
			}, "Left Pointer", "Turn On Controller Tracer", "Turn Off Controller Tracer", delegate(bool val)
			{
				ControllerPointer.isLeftLineVisible = val;
			}, null, null, 24f, 24f, false, false);
			ButtonGroup serialGrp = new ButtonGroup(UtilsMenu.subMenu, "Serialization", false, TextAnchor.UpperCenter);
			UtilsMenu.serialToggle = new VRCToggle(serialGrp, "Serialization", delegate(bool val)
			{
				if (val)
				{
					UtilsMenu.clone = PlayerWrapper.LocalPlayer.Clone(true);
				}
				else
				{
					bool flag3 = UtilsMenu.returnToBody;
					if (flag3)
					{
						PlayerWrapper.GetLocalPlayer().transform.position = UtilsMenu.clone.transform.position;
					}
					Object.Destroy(UtilsMenu.clone);
				}
				Player.prop_Player_0.GetComponent<FlatBufferNetworkSerializer>().enabled = !val;
			}, false, "Off", "On", null, null, false);
			new VRCToggle(serialGrp, "Return to Body", delegate(bool val)
			{
				UtilsMenu.returnToBody = val;
			}, false, "Off", "On", null, null, false);
			bool flag = !AppStart.devMode;
			if (flag)
			{
			}
		}

		// Token: 0x0400016B RID: 363
		public static VRCPage subMenu;

		// Token: 0x0400016C RID: 364
		internal static VRCToggle serialToggle;

		// Token: 0x0400016E RID: 366
		internal static GameObject clone;

		// Token: 0x0400016F RID: 367
		internal static bool returnToBody;
	}
}
