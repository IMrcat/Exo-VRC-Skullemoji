using System;
using System.Runtime.CompilerServices;
using EXO.Core;
using EXO.LogTools;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Buttons.Groups;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Groups;

namespace EXO.Menus.SubMenus
{
	// Token: 0x02000053 RID: 83
	internal class Ghost : MenuModule
	{
		// Token: 0x0600033C RID: 828 RVA: 0x0000ED30 File Offset: 0x0000CF30
		public override void LoadMenu()
		{
			Ghost.subMenu = new VRCPage("Ghost", false, true, false, null, "", null, false);
			ButtonGroup mainGrp = new ButtonGroup(Ghost.subMenu, "Ghost", false, TextAnchor.UpperCenter);
			new VRCToggle(mainGrp, "Semi Auto", delegate(bool val)
			{
				Ghost.semiAuto = val;
			}, false, "Off", "On", null, null, false);
			new VRCButton(mainGrp, "Force Start", "Force starts the game", delegate
			{
				GameObject.Find("LobbyManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_ReadyStartGame");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "Bring Keys", "Brings 3 Keys", delegate
			{
				try
				{
					VRCPlayer instance = VRCPlayer.field_Internal_Static_VRCPlayer_0;
					VRCPlayerApi localPlayer = VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0;
					GameObject flag = GameObject.Find("PoliceStation_A/Functions/KeySpawn/Keys/Key");
					flag.gameObject.SetActive(true);
					Networking.SetOwner(localPlayer, flag.gameObject);
					flag.transform.position = instance.transform.position + new Vector3(0f, 0.5f, 0f);
					GameObject flag2 = GameObject.Find("PoliceStation_A/Functions/KeySpawn/Keys/Key (1)");
					flag2.gameObject.SetActive(true);
					Networking.SetOwner(localPlayer, flag2.gameObject);
					flag2.transform.position = instance.transform.position + new Vector3(0f, 0.7f, 0f);
					GameObject flag3 = GameObject.Find("PoliceStation_A/Functions/KeySpawn/Keys/Key (2)");
					flag3.gameObject.SetActive(true);
					Networking.SetOwner(localPlayer, flag3.gameObject);
					flag3.transform.position = instance.transform.position + new Vector3(0f, 0.9f, 0f);
				}
				catch
				{
					CLog.L("You are not in Ghost", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\WorldEx\\Ghost.cs", 61);
				}
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new DuoButtons(mainGrp, "Ghost Win", "", delegate
			{
				GameObject.Find("LobbyManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_GhostWin");
			}, "Human Win", "", delegate
			{
				GameObject.Find("LobbyManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_HumanWin");
			});
			new DuoButtons(mainGrp, "Sabotage Ghost", "", delegate
			{
				GameObject.Find("GameManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "OnGhostKill");
			}, "+ Money", "", delegate
			{
				GameObject.Find("GameManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "OnSuspiciousKill");
			});
			new VRCButton(mainGrp, "Craft All", "", delegate
			{
				string[] basePaths = new string[] { "PoliceStation_A/Functions/WeaponWorkShops/T1-M1911", "PoliceStation_A/Functions/WeaponWorkShops/T2_MP7", "PoliceStation_A/Functions/WeaponWorkShops/T3_Vector", "PoliceStation_A/Functions/WeaponWorkShops/T2_M500" };
				foreach (string basePath in basePaths)
				{
					for (int i = 0; i < 10; i++)
					{
						string text = basePath;
						string text2;
						if (i <= 0)
						{
							text2 = "";
						}
						else
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
							defaultInterpolatedStringHandler..ctor(3, 1);
							defaultInterpolatedStringHandler.AppendLiteral(" (");
							defaultInterpolatedStringHandler.AppendFormatted<int>(i);
							defaultInterpolatedStringHandler.AppendLiteral(")");
							text2 = defaultInterpolatedStringHandler.ToStringAndClear();
						}
						string path = text + text2;
						GameObject flag4 = GameObject.Find(path);
						if (flag4 != null)
						{
							flag4.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Local_StartCraft");
						}
					}
				}
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
		}

		// Token: 0x04000178 RID: 376
		public static VRCPage subMenu;

		// Token: 0x04000179 RID: 377
		internal static bool semiAuto;
	}
}
