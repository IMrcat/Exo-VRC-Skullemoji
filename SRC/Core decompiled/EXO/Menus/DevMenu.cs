using System;
using System.Collections;
using System.IO;
using CoreRuntime.Manager;
using EXO.Core;
using EXO.Functions.InputManager;
using EXO.Functions.Item;
using EXO.Menus.SubMenus.Settings;
using EXO.Modules;
using EXO.Modules.Utilities;
using EXO.Patches;
using EXO.Wrappers;
using Il2CppSystem.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC;
using VRC.Core;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using WorldAPI.ButtonAPI;
using WorldAPI.ButtonAPI.Buttons;
using WorldAPI.ButtonAPI.Controls;
using WorldAPI.ButtonAPI.Groups;

namespace EXO.Menus
{
	// Token: 0x02000049 RID: 73
	internal class DevMenu : MenuModule
	{
		// Token: 0x06000320 RID: 800 RVA: 0x0000D610 File Offset: 0x0000B810
		public override void LoadMenu()
		{
			DevMenu.AddAction();
			DevMenu.subMenu = new VRCPage("Dev Menu", false, true, false, null, "", null, false);
			ButtonGroup devGrp = new ButtonGroup(DevMenu.subMenu, "Beta", false, TextAnchor.UpperCenter);
			new VRCToggle(devGrp, "Anti Theft", delegate(bool val)
			{
				AntiTheft.Enabled = val;
			}, false, "Anti Theft", "On", null, null, false);
			new VRCButton(devGrp, "Log Udon", "Log Udon", delegate
			{
				CoroutineManager.RunCoroutine(DevMenu.LogUdon());
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCToggle(devGrp, "Log Keys", delegate(bool val)
			{
				KeybindInputs.logKeys = val;
			}, KeybindInputs.logKeys, "Off", "On", null, null, false);
			new VRCButton(devGrp, "Runtime Buttons", "Build Runtime buttons", delegate
			{
				RuntimeBtns.CreateButtons();
			}, false, true, null, ExtentedControl.HalfType.Normal, false);
			DevMenu.killAllToggle = new VRCToggle(devGrp, "Kill Loop All", delegate(bool val)
			{
				LogMenu.logGodModeBtn.State = !val;
				LogMenu.logUdonBtn.State = !val;
				LogMenu.logEventsBtn.State = false;
				int localNode = -1;
				GameObject gameLogic = GameObject.Find("Game Logic");
				for (int i = 0; i < 24; i++)
				{
					string playerPath = "Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + i.ToString() + ")/Player Name Text";
					Transform playerTransform = gameLogic.FindObject(playerPath);
					GameObject player = ((playerTransform != null) ? playerTransform.gameObject : null);
					bool flag = player != null;
					if (flag)
					{
						TMP_Text tmpText = player.GetComponent<TMP_Text>();
						Text text = player.GetComponent<Text>();
						string playerName = APIUser.CurrentUser.displayName.ToString();
						bool flag2 = (tmpText != null && tmpText.text.Equals(playerName)) || (tmpText == null && text != null && text.text.Equals(playerName));
						if (flag2)
						{
							localNode = i;
							break;
						}
					}
					else
					{
						for (int j = 0; j < 24; j++)
						{
							GameObject currentNode = GameObject.Find("Player Node (" + j.ToString() + ")");
							bool flag3 = currentNode.GetComponent<CapsuleCollider>().radius != 0.5f && currentNode.GetComponent<CapsuleCollider>().center != Vector3.zero;
							if (flag3)
							{
								localNode = j;
								break;
							}
						}
					}
				}
				DevMenu.killLoopState = val;
				CoroutineManager.RunCoroutine(DevMenu.KillLoopAll(localNode));
			}, false, "Off", "On", null, null, false);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000D76D File Offset: 0x0000B96D
		internal static void AddAction()
		{
			JoinLeavePatch.OnLocalPlayerLeave = (Action<Player>)Delegate.Combine(JoinLeavePatch.OnLocalPlayerLeave, delegate(Player plr)
			{
				bool state = DevMenu.killAllToggle.State;
				if (state)
				{
					DevMenu.killAllToggle.State = false;
				}
			});
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000D7A3 File Offset: 0x0000B9A3
		internal static IEnumerator LogUdon()
		{
			string directoryPath = AppStart.HexedDirectory.FullName + "\\EXO\\UdonDump";
			Directory.CreateDirectory(directoryPath);
			string filePath = Path.Combine(directoryPath, WorldWrapper.ApiWorld.name + "_Dump.txt");
			bool flag = !File.Exists(filePath);
			if (flag)
			{
				using (StreamWriter writer = new StreamWriter(filePath))
				{
					foreach (UdonBehaviour udonObj in WorldWrapper.udonBehaviours)
					{
						foreach (string name in udonObj._eventTable.Keys)
						{
							writer.WriteLine("Obj: " + UtilFunc.GetGameObjectPath(udonObj.transform) + " | Udon: " + name);
							name = null;
						}
						Dictionary<string, List<uint>>.KeyCollection.Enumerator enumerator = null;
						udonObj = null;
					}
					UdonBehaviour[] array = null;
				}
				StreamWriter writer = null;
			}
			yield return new WaitForSeconds(0.1f);
			yield break;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000D7AB File Offset: 0x0000B9AB
		internal static IEnumerator KillLoopAll(int localNode)
		{
			GameObject gameLogic = GameObject.Find("Game Logic");
			UdonBehaviour gameLogicBehaviour = gameLogic.GetComponent<UdonBehaviour>();
			GameObject[] playerNodes = new GameObject[24];
			int num;
			for (int i = 0; i < 24; i = num + 1)
			{
				bool flag = i == localNode;
				if (!flag)
				{
					playerNodes[i] = GameObject.Find("Player Node (" + i.ToString() + ")");
				}
				num = i;
			}
			bool flag3;
			do
			{
				for (int j = 0; j < 24; j = num + 1)
				{
					bool flag2 = j == localNode || playerNodes[j] == null;
					if (!flag2)
					{
						playerNodes[j].GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncAssignB");
					}
					num = j;
				}
				gameLogicBehaviour.SendCustomNetworkEvent(NetworkEventTarget.All, "KillLocalPlayer");
				yield return new WaitForSeconds(0.1f);
				flag3 = !DevMenu.killLoopState;
			}
			while (!flag3);
			yield break;
			yield break;
		}

		// Token: 0x0400015E RID: 350
		public static VRCPage subMenu;

		// Token: 0x0400015F RID: 351
		internal static VRCToggle killAllToggle;

		// Token: 0x04000160 RID: 352
		internal static bool killLoopState;
	}
}
