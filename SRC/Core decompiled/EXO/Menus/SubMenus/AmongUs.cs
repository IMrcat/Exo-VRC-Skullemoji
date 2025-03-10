using System;
using EXO.Core;
using EXO.LogTools;
using EXO.Modules.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.Core;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Buttons.Groups;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Groups;

namespace EXO.Menus.SubMenus
{
	// Token: 0x02000051 RID: 81
	internal class AmongUs : MenuModule
	{
		// Token: 0x06000338 RID: 824 RVA: 0x0000EA4C File Offset: 0x0000CC4C
		public override void LoadMenu()
		{
			AmongUs.subMenu = new VRCPage("Among Us", false, true, false, null, "", null, false);
			ButtonGroup mainGrp = new ButtonGroup(AmongUs.subMenu, "Among Us", false, TextAnchor.UpperCenter);
			new VRCButton(mainGrp, "Task EarRape", "Loud", delegate
			{
				UdonUtils.SendUdonEventsWithName("CompleteTask");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "Assign All Tasks", "Gives everyone all tasks", delegate
			{
				UdonUtils.SendUdonEventsWithName("AssignTask");
				UdonUtils.SendUdonEventsWithName("AssignWiring");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "Force Start", "Force starts the game", delegate
			{
				GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Btn_Start");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new DuoButtons(mainGrp, "Self Impostor", "Sets your role as Impostor", delegate
			{
				for (int i = 0; i < 30; i++)
				{
					string playerEntryPath = "Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + i.ToString() + ")/Player Name Text";
					Transform playerEntryTransform = GameObject.Find("Game Logic").FindObject(playerEntryPath);
					GameObject playerEntry = ((playerEntryTransform != null) ? playerEntryTransform.gameObject : null);
					bool flag = playerEntry != null;
					if (flag)
					{
						TMP_Text tmpTextComponent = playerEntry.GetComponent<TMP_Text>();
						Text textComponent = playerEntry.GetComponent<Text>();
						string playerName = APIUser.CurrentUser.displayName.ToString();
						bool flag2 = (tmpTextComponent != null && tmpTextComponent.text.Equals(playerName)) || (tmpTextComponent == null && textComponent != null && textComponent.text.Equals(playerName));
						if (flag2)
						{
							GameObject.Find("Player Node (" + i.ToString() + ")").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncAssignM");
							break;
						}
					}
					else
					{
						CLog.D("Player Entry not found for index: " + i.ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\WorldEx\\AmongUs.cs", 68);
					}
				}
			}, "Self Crewmate", "Sets your role as Crewmate", delegate
			{
				for (int j = 0; j < 30; j++)
				{
					string playerEntryPath2 = "Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + j.ToString() + ")/Player Name Text";
					Transform playerEntryTransform2 = GameObject.Find("Game Logic").FindObject(playerEntryPath2);
					GameObject playerEntry2 = ((playerEntryTransform2 != null) ? playerEntryTransform2.gameObject : null);
					bool flag3 = playerEntry2 != null;
					if (flag3)
					{
						TMP_Text tmpTextComponent2 = playerEntry2.GetComponent<TMP_Text>();
						Text textComponent2 = playerEntry2.GetComponent<Text>();
						string playerName2 = APIUser.CurrentUser.displayName.ToString();
						bool flag4 = (tmpTextComponent2 != null && tmpTextComponent2.text.Equals(playerName2)) || (tmpTextComponent2 == null && textComponent2 != null && textComponent2.text.Equals(playerName2));
						if (flag4)
						{
							GameObject.Find("Player Node (" + j.ToString() + ")").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncAssignB");
							break;
						}
					}
					else
					{
						CLog.D("Player Entry not found for index: " + j.ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\WorldEx\\AmongUs.cs", 93);
					}
				}
			});
			new DuoButtons(mainGrp, "Everyone Impostor", "Makes everyone a Impostor", delegate
			{
				for (int k = 0; k < 30; k++)
				{
					GameObject flag5 = GameObject.Find("Player Node (" + k.ToString() + ")");
					flag5.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncAssignM");
				}
			}, "Everyone Crewmate", "Makes everyone a Crewmate", delegate
			{
				for (int l = 0; l < 30; l++)
				{
					GameObject flag6 = GameObject.Find("Player Node (" + l.ToString() + ")");
					flag6.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncAssignB");
				}
			});
			new DuoButtons(mainGrp, "Spam Skip", "Spam Skip", delegate
			{
				UdonUtils.SendUdonEventsWithName("Btn_SkipVoting");
			}, "End Vote", "End Vote", delegate
			{
				GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncCloseVoting");
			});
			new DuoButtons(mainGrp, "Sabotage All", "Sabotage All", delegate
			{
				GameObject obj = GameObject.Find("Game Logic");
				bool flag7 = obj;
				if (flag7)
				{
					obj.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncDoSabotageDoorsStorage");
					obj.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncDoSabotageDoorsCafeteria");
					obj.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncDoSabotageDoorsSecurity");
					obj.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncDoSabotageDoorsElectrical");
					obj.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncDoSabotageDoorsMedbay");
					obj.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncDoSabotageDoorsLower");
					obj.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncDoSabotageDoorsUpper");
					obj.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncDoSabotageOxygen");
					obj.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncDoSabotageLights");
					obj.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncDoSabotageReactor");
				}
			}, "Fix All", "Fix All", delegate
			{
				UdonUtils.SendUdonEventsWithName("CancelAllSabotage");
			});
			new DuoButtons(mainGrp, "Kill All", "Kill All", delegate
			{
				GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "KillLocalPlayer");
			}, "All to Space", "Sends everyone out into space", delegate
			{
				for (int m = 0; m < 30; m++)
				{
					GameObject flag8 = GameObject.Find("Player Node (" + m.ToString() + ")");
					bool flag9 = flag8;
					if (flag9)
					{
						flag8.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncVotedOut");
					}
				}
			});
		}

		// Token: 0x04000176 RID: 374
		public static VRCPage subMenu;
	}
}
