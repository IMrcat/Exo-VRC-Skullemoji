using System;
using System.Collections;
using System.Runtime.CompilerServices;
using CoreRuntime.Manager;
using EXO.Core;
using EXO.LogTools;
using EXO.Modules.Utilities;
using EXO.Patches;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC;
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
	// Token: 0x02000056 RID: 86
	internal class PrisonEsc : MenuModule
	{
		// Token: 0x06000347 RID: 839 RVA: 0x0000FB4C File Offset: 0x0000DD4C
		public override void LoadMenu()
		{
			PrisonEsc.subMenu = new VRCPage("Prison Escape", false, true, false, null, "", null, false);
			CollapsibleButtonGroup mainGrp = new CollapsibleButtonGroup(PrisonEsc.subMenu, "Prison Escape", false);
			PrisonEsc.AddAction();
			new VRCButton(mainGrp, "Force Start", "Force starts the game", delegate
			{
				GameObject.Find("Game State").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "StartGameCountdown");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			PrisonEsc.prisonDoorsToggle = new VRCToggle(mainGrp, "Remove Doors", delegate(bool val)
			{
				GameObject.Find("Prison/Doors").SetActive(!val);
				GameObject.Find("Prison/Cells").SetActive(!val);
			}, false, "Off", "On", null, null, false);
			new VRCButton(mainGrp, "Kill All", "Kill All", delegate
			{
				GameObject.Find("Scripts/Player Objects/PlayerData").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Damage250");
				for (int i = 1; i < 34; i++)
				{
					GameObject.Find("Scripts/Player Objects/PlayerData (" + i.ToString() + ")").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "Damage250");
				}
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new DuoButtons(mainGrp, "+ Points", "Add Points", delegate
			{
				PrisonEsc.EventOnPlayer(VRCPlayer.field_Internal_Static_VRCPlayer_0, "__0__AddPoints");
			}, "Clear Scorecard", "Clear Scorecard", delegate
			{
				PrisonEsc.EventOnPlayer(VRCPlayer.field_Internal_Static_VRCPlayer_0, "_ClearScorecard");
			});
			new DuoButtons(mainGrp, "+ Guard Kill", "Add Guard Kill", delegate
			{
				PrisonEsc.EventOnPlayer(VRCPlayer.field_Internal_Static_VRCPlayer_0, "_AddGuardKill");
			}, "+ Prisoner Kill", "Add Prisoner Kill", delegate
			{
				PrisonEsc.EventOnPlayer(VRCPlayer.field_Internal_Static_VRCPlayer_0, "__0__AddPrisKill");
			});
			new DuoButtons(mainGrp, "+ Escape Win", "Add Escape Win", delegate
			{
				PrisonEsc.EventOnPlayer(VRCPlayer.field_Internal_Static_VRCPlayer_0, "_AddEscape");
			}, "+ Guard Win", "Add Guard Win", delegate
			{
				PrisonEsc.EventOnPlayer(VRCPlayer.field_Internal_Static_VRCPlayer_0, "_AddWin");
			});
			new VRCButton(mainGrp, "Clear All Scorecards", "Clear All Scorecards", delegate
			{
				PrisonEsc.EventOnAll("_ClearScorecard");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "+ Points All", "Add Points All", delegate
			{
				PrisonEsc.EventOnAll("__0__AddPoints");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new DuoButtons(mainGrp, "+ Guard Kill All", "Add Guard Kill All", delegate
			{
				PrisonEsc.EventOnAll("_AddGuardKill");
			}, "+ Prisoner Kill All", "Add Prisoner Kill All", delegate
			{
				PrisonEsc.EventOnAll("__0__AddPrisKill");
			});
			new DuoButtons(mainGrp, "+ Escape Win All", "Add Escape Win All", delegate
			{
				PrisonEsc.EventOnAll("_AddEscape");
			}, "+ Guard Win All", "Add Guard Win All", delegate
			{
				PrisonEsc.EventOnAll("_AddWin");
			});
			bool devMode = AppStart.devMode;
			if (devMode)
			{
				PrisonEsc.loopToggle = new VRCToggle(mainGrp, "Point Loop All", delegate(bool val)
				{
					PrisonEsc.loopState = val;
					if (val)
					{
						PrisonEsc.EventOnAll("_AddEscape");
					}
					CoroutineManager.RunCoroutine(PrisonEsc.PrisonPointsLoop());
				}, false, "Off", "On", null, null, false);
			}
			CollapsibleButtonGroup weaponGrp = new CollapsibleButtonGroup(PrisonEsc.subMenu, "Bring Weapons", false);
			new DuoButtons(weaponGrp, "Bring Armor", "Bring Armor", delegate
			{
				GetNext.BringItem("Crates/Small Rewards/Armor", 3);
			}, "Bring Keycards", "Bring Keycards", delegate
			{
				GameObject KeyCards = GameObject.Find("Items/Keycards/Keycard");
				KeyCards.gameObject.SetActive(true);
				Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, KeyCards);
				KeyCards.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.4f, 0f);
				for (int j = 1; j < 31; j++)
				{
					GameObject KeyCards2 = GameObject.Find("Items/Keycards/Keycard (" + j.ToString() + ")");
					KeyCards2.gameObject.SetActive(true);
					Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, KeyCards2);
					KeyCards2.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.4f, 0f);
				}
			});
			new DuoButtons(weaponGrp, "Bring AK47", "Bring AK47", delegate
			{
				GameObject Obj = GameObject.Find("Crates/Large Rewards/AK47");
				Obj.gameObject.SetActive(true);
				Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj);
				Obj.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.3f, 0f);
			}, "Bring LMG", "Bring LMG", delegate
			{
				GameObject Obj2 = GameObject.Find("Crates/Large Rewards/LMG");
				Obj2.gameObject.SetActive(true);
				Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj2);
				Obj2.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.3f, 0f);
			});
			new DuoButtons(weaponGrp, "Bring P90", "Bring P90", delegate
			{
				GameObject Obj3 = GameObject.Find("Crates/Large Rewards/P90");
				Obj3.gameObject.SetActive(true);
				Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj3);
				Obj3.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.3f, 0f);
			}, "Bring RPG", "Bring RPG", delegate
			{
				GetNext.BringItem("Crates/Large Rewards/RPG", 2);
			});
			new DuoButtons(weaponGrp, "Bring Machete", "Bring Machete", delegate
			{
				GameObject Obj4 = GameObject.Find("Crates/Large Rewards/Machete");
				Obj4.gameObject.SetActive(true);
				Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj4);
				Obj4.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.3f, 0f);
			}, "Bring Broom", "Bring Broom", delegate
			{
				GameObject Obj5 = GameObject.Find("Crates/Large Rewards/Broomstick");
				Obj5.gameObject.SetActive(true);
				Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj5);
				Obj5.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.5f, 0f);
			});
			new DuoButtons(weaponGrp, "Bring Sniper", "Bring Sniper", delegate
			{
				GetNext.BringItem("Items/Static Guns/Sniper", 2);
			}, "Bring M4A1", "Bring M4A1", delegate
			{
				GetNext.BringItem("Items/Static Guns/M4A1", 4);
			});
			new DuoButtons(weaponGrp, "Bring Knife", "Bring Knife", delegate
			{
				GetNext.BringItem("Items/Knives/Knife", 5);
			}, "Bring Shotgun", "Bring Shotgun", delegate
			{
				GetNext.BringItem("Items/Static Guns/Shotgun", 4);
			});
			new DuoButtons(weaponGrp, "Bring SMG", "Bring SMG", delegate
			{
				GetNext.BringItem("Items/Static Guns/SMG", 4);
			}, "Bring Pistol", "Bring Pistol", delegate
			{
				GetNext.BringItem("Items/Static Guns/Pistol", 12);
			});
			new DuoButtons(weaponGrp, "Bring Magnum", "Bring Magnum", delegate
			{
				GameObject Obj6 = GameObject.Find("Items/Static Guns//Magnum");
				Obj6.gameObject.SetActive(true);
				Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj6);
				Obj6.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.3f, 0f);
			}, "Bring Revolver", "Bring Revolver", delegate
			{
				GameObject Obj7 = GameObject.Find("Items/Static Guns//Revolver");
				Obj7.gameObject.SetActive(true);
				Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj7);
				Obj7.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.3f, 0f);
			});
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0001017A File Offset: 0x0000E37A
		internal static void AddAction()
		{
			JoinLeavePatch.OnLocalPlayerLeave = (Action<Player>)Delegate.Combine(JoinLeavePatch.OnLocalPlayerLeave, delegate(Player player)
			{
				try
				{
					bool state = PrisonEsc.prisonDoorsToggle.State;
					if (state)
					{
						PrisonEsc.prisonDoorsToggle.State = false;
					}
				}
				catch
				{
				}
				PrisonEsc.loopToggle.State = false;
			});
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000101B0 File Offset: 0x0000E3B0
		internal static IEnumerator PrisonPointsLoop()
		{
			bool flag;
			do
			{
				PrisonEsc.EventOnAll("__0__AddPoints");
				yield return new WaitForSeconds(0f);
				flag = !PrisonEsc.loopState;
			}
			while (!flag);
			yield break;
			yield break;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000101B8 File Offset: 0x0000E3B8
		public static void EventOnPlayer(VRCPlayer player, string uEvent)
		{
			string currentPlayerName = player._player.ToString();
			for (int index = 0; index < 34; index++)
			{
				string text = "Scorecard";
				string text2;
				if (index != 0)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					defaultInterpolatedStringHandler..ctor(3, 1);
					defaultInterpolatedStringHandler.AppendLiteral(" (");
					defaultInterpolatedStringHandler.AppendFormatted<int>(index);
					defaultInterpolatedStringHandler.AppendLiteral(")");
					text2 = defaultInterpolatedStringHandler.ToStringAndClear();
				}
				else
				{
					text2 = "";
				}
				string scorecardName = text + text2;
				GameObject scorecardObject = GameObject.Find("Spawn Area/Signs/Scoreboard Sign/Panel/Scroll View Scorecards/Viewport/Content Scorecards/" + scorecardName);
				bool flag = scorecardObject != null;
				if (flag)
				{
					Transform transform = scorecardObject.transform.Find("Panel/Text Name");
					Text playerLabel = ((transform != null) ? transform.GetComponent<Text>() : null);
					bool flag2 = playerLabel.text == currentPlayerName;
					if (flag2)
					{
						GameObject gameObject = GameObject.Find("Spawn Area/Signs/Scoreboard Sign/Panel/Scroll View Scorecards/Viewport/Content Scorecards/" + scorecardName);
						UdonBehaviour udonComponent = ((gameObject != null) ? gameObject.GetComponent<UdonBehaviour>() : null);
						if (udonComponent != null)
						{
							udonComponent.SendCustomEvent(uEvent);
						}
					}
				}
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000102B8 File Offset: 0x0000E4B8
		public static void EventOnAll(string uEvent)
		{
			for (int index = 0; index < 34; index++)
			{
				string text = "Scorecard";
				string text2;
				if (index != 0)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					defaultInterpolatedStringHandler..ctor(3, 1);
					defaultInterpolatedStringHandler.AppendLiteral(" (");
					defaultInterpolatedStringHandler.AppendFormatted<int>(index);
					defaultInterpolatedStringHandler.AppendLiteral(")");
					text2 = defaultInterpolatedStringHandler.ToStringAndClear();
				}
				else
				{
					text2 = "";
				}
				string scorecardName = text + text2;
				GameObject scorecardObject = GameObject.Find("Spawn Area/Signs/Scoreboard Sign/Panel/Scroll View Scorecards/Viewport/Content Scorecards/" + scorecardName);
				bool flag = scorecardObject != null;
				if (flag)
				{
					Transform transform = scorecardObject.transform.Find("Panel/Text Name");
					Text text3 = ((transform != null) ? transform.GetComponent<Text>() : null);
					GameObject gameObject = GameObject.Find("Spawn Area/Signs/Scoreboard Sign/Panel/Scroll View Scorecards/Viewport/Content Scorecards/" + scorecardName);
					UdonBehaviour udonComponent = ((gameObject != null) ? gameObject.GetComponent<UdonBehaviour>() : null);
					if (udonComponent != null)
					{
						udonComponent.SendCustomEvent(uEvent);
					}
				}
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00010394 File Offset: 0x0000E594
		internal static GameObject GetPlayerData(string userDisplayName)
		{
			string[] basePaths = new string[] { "Game Canvas/Panel Playing/Panel Spectate/Minimap/Nametags/Nametag/Name", "Game Canvas/Panel Playing/Panel Prisoners/Scroll View/Viewport/Content Pris Names/List Player/Player Name", "Game Canvas/Panel Playing/Panel Guards/Scroll View/Viewport/Content Guard Names/List Player/Player Name" };
			string[] dynamicPaths = new string[] { "Game Canvas/Panel Playing/Panel Spectate/Minimap/Nametags/Nametag ({0})/Name", "Game Canvas/Panel Playing/Panel Prisoners/Scroll View/Viewport/Content Pris Names/List Player ({0})/Player Name", "Game Canvas/Panel Playing/Panel Guards/Scroll View/Viewport/Content Guard Names/List Player ({0})/Player Name" };
			foreach (string basePath in basePaths)
			{
				Transform nametagObject = GameObject.Find("Spawn Area").FindObject(basePath);
				bool flag = nametagObject != null && nametagObject.gameObject.activeInHierarchy;
				if (flag)
				{
					TMP_Text textComponent = nametagObject.GetComponent<TMP_Text>();
					CLog.D(textComponent.text, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\WorldEx\\PrisonEsc.cs", 282);
					bool flag2 = textComponent.text.Equals(userDisplayName);
					if (flag2)
					{
						return GameObject.Find("Scripts/Player Objects/PlayerData");
					}
				}
			}
			for (int i = 1; i < 34; i++)
			{
				foreach (string pathFormat in dynamicPaths)
				{
					string path = string.Format(pathFormat, i);
					Transform listPlayerObject = GameObject.Find("Spawn Area").FindObject(path);
					bool flag3 = listPlayerObject != null && listPlayerObject.gameObject.activeInHierarchy;
					if (flag3)
					{
						TMP_Text textComponent2 = listPlayerObject.GetComponent<TMP_Text>();
						CLog.D(textComponent2.text, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\WorldEx\\PrisonEsc.cs", 297);
						bool flag4 = textComponent2.text.Equals(userDisplayName);
						if (flag4)
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
							defaultInterpolatedStringHandler..ctor(36, 1);
							defaultInterpolatedStringHandler.AppendLiteral("Scripts/Player Objects/PlayerData (");
							defaultInterpolatedStringHandler.AppendFormatted<int>(i);
							defaultInterpolatedStringHandler.AppendLiteral(")");
							return GameObject.Find(defaultInterpolatedStringHandler.ToStringAndClear());
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0001057C File Offset: 0x0000E77C
		internal static GameObject GetPlayerDataViaScore(string userDisplayName)
		{
			for (int index = 0; index < 34; index++)
			{
				string text = "Scorecard";
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				string text2;
				if (index != 0)
				{
					defaultInterpolatedStringHandler..ctor(3, 1);
					defaultInterpolatedStringHandler.AppendLiteral(" (");
					defaultInterpolatedStringHandler.AppendFormatted<int>(index);
					defaultInterpolatedStringHandler.AppendLiteral(")");
					text2 = defaultInterpolatedStringHandler.ToStringAndClear();
				}
				else
				{
					text2 = "";
				}
				string scorecardName = text + text2;
				GameObject scorecardObject = GameObject.Find("Spawn Area/Signs/Scoreboard Sign/Panel/Scroll View Scorecards/Viewport/Content Scorecards/" + scorecardName);
				bool flag = scorecardObject != null;
				if (flag)
				{
					Transform transform = scorecardObject.transform.Find("Panel/Text Name");
					Text playerLabel = ((transform != null) ? transform.GetComponent<Text>() : null);
					bool flag2 = playerLabel.text == userDisplayName;
					if (flag2)
					{
						string text3 = "Scripts/Player Objects/PlayerData";
						string text4;
						if (index != 0)
						{
							defaultInterpolatedStringHandler..ctor(3, 1);
							defaultInterpolatedStringHandler.AppendLiteral(" (");
							defaultInterpolatedStringHandler.AppendFormatted<int>(index);
							defaultInterpolatedStringHandler.AppendLiteral(")");
							text4 = defaultInterpolatedStringHandler.ToStringAndClear();
						}
						else
						{
							text4 = "";
						}
						return GameObject.Find(text3 + text4);
					}
				}
			}
			return null;
		}

		// Token: 0x0400018A RID: 394
		public static VRCPage subMenu;

		// Token: 0x0400018B RID: 395
		internal static VRCToggle loopToggle;

		// Token: 0x0400018C RID: 396
		internal static VRCToggle prisonDoorsToggle;

		// Token: 0x0400018D RID: 397
		internal static bool loopState;
	}
}
