using System;
using EXO.Core;
using EXO.LogTools;
using TMPro;
using UnityEngine;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Groups;

namespace EXO.Menus.SubMenus.WorldEx
{
	// Token: 0x0200005B RID: 91
	internal class Sunset : MenuModule
	{
		// Token: 0x0600035D RID: 861 RVA: 0x00011088 File Offset: 0x0000F288
		public override void LoadMenu()
		{
			Sunset.subMenu = new VRCPage("Sunset Bar", false, true, false, null, "", null, false);
			ButtonGroup mainGrp = new ButtonGroup(Sunset.subMenu, "Sunset Bar", false, TextAnchor.UpperCenter);
			new VRCButton(mainGrp, "Self Bouncer", "", delegate
			{
				int player = Sunset.GetPlayer(VRCPlayer.field_Internal_Static_VRCPlayer_0);
				GameObject.Find("Udon Scripts/Manager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SelectPlayer" + player.ToString());
				GameObject.Find("Udon Scripts/Manager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "ToggleBouncer");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "Self Manager", "", delegate
			{
				int player2 = Sunset.GetPlayer(VRCPlayer.field_Internal_Static_VRCPlayer_0);
				GameObject.Find("Udon Scripts/Manager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SelectPlayer" + player2.ToString());
				GameObject.Find("Udon Scripts/Manager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "ToggleManager");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "Self General Manager", "", delegate
			{
				int player3 = Sunset.GetPlayer(VRCPlayer.field_Internal_Static_VRCPlayer_0);
				GameObject.Find("Udon Scripts/Manager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SelectPlayer" + player3.ToString());
				GameObject.Find("Udon Scripts/Manager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "GiveGeneralManager");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "Self Bartender", "", delegate
			{
				int player4 = Sunset.GetPlayer(VRCPlayer.field_Internal_Static_VRCPlayer_0);
				GameObject.Find("Udon Scripts/Manager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SelectPlayer" + player4.ToString());
				GameObject.Find("Udon Scripts/Manager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "ToggleBartender");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "Self VIP", "", delegate
			{
				int player5 = Sunset.GetPlayer(VRCPlayer.field_Internal_Static_VRCPlayer_0);
				GameObject.Find("Udon Scripts/Manager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SelectPlayer" + player5.ToString());
				GameObject.Find("Udon Scripts/Manager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "ToggleVIPVerified");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "Self Performer", "", delegate
			{
				int player6 = Sunset.GetPlayer(VRCPlayer.field_Internal_Static_VRCPlayer_0);
				GameObject.Find("Udon Scripts/Manager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SelectPlayer" + player6.ToString());
				GameObject.Find("Udon Scripts/Manager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "TogglePerformer");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "Self DJ", "", delegate
			{
				int player7 = Sunset.GetPlayer(VRCPlayer.field_Internal_Static_VRCPlayer_0);
				GameObject.Find("Udon Scripts/Manager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SelectPlayer" + player7.ToString());
				GameObject.Find("Udon Scripts/Manager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "ToggleDJ");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00011235 File Offset: 0x0000F435
		public static void SunsetBouncerTarget(VRCPlayer target)
		{
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00011238 File Offset: 0x0000F438
		public static int GetPlayer(VRCPlayer selection)
		{
			string name = selection.field_Private_VRCPlayerApi_0.displayName;
			for (int index = 0; index < 59; index++)
			{
				string number = index.ToString() + "/Player Label Text";
				bool flag = GameObject.Find("Udon Scripts/The Brand New 2024 UI by Rineycat/Foreground/Scroll View/Viewport/Content/" + number).GetComponent<TextMeshProUGUI>().text.Contains(name);
				if (flag)
				{
					return index;
				}
			}
			CLog.L("Player not found in list", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\WorldEx\\Sunset.cs", 104);
			return -1;
		}

		// Token: 0x04000195 RID: 405
		public static VRCPage subMenu;
	}
}
