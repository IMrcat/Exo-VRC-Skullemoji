using System;
using System.Runtime.CompilerServices;
using EXO.Core;
using EXO.LogTools;
using EXO.Modules.Utilities;
using EXO.Patches;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC;
using VRC.Core;
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
	// Token: 0x02000055 RID: 85
	internal class Murder4 : MenuModule
	{
		// Token: 0x06000340 RID: 832 RVA: 0x0000F040 File Offset: 0x0000D240
		public override void LoadMenu()
		{
			Murder4.AddAction();
			Murder4.subMenu = new VRCPage("Murder 4", false, true, false, null, "", null, false);
			CollapsibleButtonGroup mainGrp = new CollapsibleButtonGroup(Murder4.subMenu, "Murder4", false);
			new DuoToggles(mainGrp, "Gold Gun", "On", "Off", delegate(bool val)
			{
				Murder4.goldGun = val;
			}, "Gold Gun All", "On", "Off", delegate(bool val)
			{
				Murder4.goldGunGlobal = val;
			}, null, null, 24f, 24f, false, false);
			new DuoToggles(mainGrp, "Semi Auto", "On", "Off", delegate(bool val)
			{
				Murder4.semiAuto = val;
			}, "Semi Auto All", "On", "Off", delegate(bool val)
			{
				Murder4.semiAutoGlobal = val;
			}, null, null, 24f, 24f, false, false);
			new DuoToggles(mainGrp, "Boom Gun", "On", "Off", delegate(bool val)
			{
				Murder4.explosiveRounds = val;
			}, "Boom Gun All", "On", "Off", delegate(bool val)
			{
				Murder4.explosiveRoundsGlobal = val;
			}, null, null, 24f, 24f, false, false);
			new DuoToggles(mainGrp, "Laser Sight", "On", "Off", delegate(bool val)
			{
				Murder4.toggleLaser = val;
				GameObject.Find("Environment/Named Locations").SetActive(false);
				GameObject.Find("Environment/Sounds").SetActive(false);
			}, "Xmas", "On", "Off", delegate(bool val)
			{
				Transform xmas = GameObject.Find("Game Logic/Weapons/Revolver/Recoil Anim/Recoil/geo (xmas)").transform;
				xmas.gameObject.SetActive(val);
			}, null, null, 24f, 24f, false, false);
			new VRCToggle(mainGrp, "Door Colliders", delegate(bool val)
			{
				Murder4.doorColliders = val;
				Murder4.DoorColliders(Murder4.doorColliders);
			}, Murder4.doorColliders, "Off", "On", null, null, false);
			new VRCToggle(mainGrp, "Death Hud", delegate(bool val)
			{
				Murder4.deathHud = val;
				Murder4.DeathHudToggle(Murder4.deathHud);
			}, true, "Off", "On", null, null, false);
			new VRCToggle(mainGrp, "Remove Walls", delegate(bool val)
			{
				MovementMenu.flyBtn.State = val;
				bool flag = Murder4.environment == null;
				if (flag)
				{
					Murder4.environment = GameObject.Find("Environment");
				}
				Murder4.environment.SetActive(!val);
			}, false, "Off", "On", null, null, false);
			new DuoToggles(mainGrp, "Game Box", "On", "Off", delegate(bool val)
			{
				if (val)
				{
					CLog.L("GameBox is Upsized", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\WorldEx\\Murder4.cs", 105);
					foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
					{
						bool flag2 = gameObject.name.Contains("Game Area Bounds");
						bool flag4 = flag2;
						if (flag4)
						{
							gameObject.transform.localScale = new Vector3(134210.1f, 134210.1f, 134210.1f);
						}
					}
				}
				else
				{
					CLog.L("GameBox Reset", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\WorldEx\\Murder4.cs", 115);
					foreach (GameObject gameObject2 in Resources.FindObjectsOfTypeAll<GameObject>())
					{
						bool flag3 = gameObject2.name.Contains("Game Area Bounds");
						bool flag5 = flag3;
						if (flag5)
						{
							gameObject2.transform.localScale = new Vector3(73f, 73f, 73f);
						}
					}
				}
			}, "Remove Shit", "On", "Off", delegate(bool val)
			{
				foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
				{
					string hierarchyPath = obj.transform.GetHierarchyPath(null);
					string text = hierarchyPath;
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 2131548715U)
					{
						if (num <= 796575264U)
						{
							if (num <= 467570543U)
							{
								if (num <= 405995996U)
								{
									if (num != 383822167U)
									{
										if (num != 405995996U)
										{
											continue;
										}
										if (!(text == "Environment/Cellar/barrel_day"))
										{
											continue;
										}
									}
									else if (!(text == "Environment/Garage/Car"))
									{
										continue;
									}
								}
								else if (num != 463890120U)
								{
									if (num != 467570543U)
									{
										continue;
									}
									if (!(text == "Environment/Billiard/Round Table B"))
									{
										continue;
									}
								}
								else if (!(text == "Environment/Conservatory"))
								{
									continue;
								}
							}
							else if (num <= 605367242U)
							{
								if (num != 594808300U)
								{
									if (num != 605367242U)
									{
										continue;
									}
									if (!(text == "Environment/Cellar/crate stack a_day"))
									{
										continue;
									}
								}
								else if (!(text == "Environment/Lounge/Couch"))
								{
									continue;
								}
							}
							else if (num != 692836283U)
							{
								if (num != 712848608U)
								{
									if (num != 796575264U)
									{
										continue;
									}
									if (!(text == "Environment/Lounge/Round Table B"))
									{
										continue;
									}
								}
								else if (!(text == "Environment/Study/Exec Chair_day"))
								{
									continue;
								}
							}
							else if (!(text == "Environment/Lounge/Loveseat"))
							{
								continue;
							}
						}
						else if (num <= 1164137223U)
						{
							if (num <= 912646547U)
							{
								if (num != 838366675U)
								{
									if (num != 912646547U)
									{
										continue;
									}
									if (!(text == "Environment/Study/typewriter_day"))
									{
										continue;
									}
								}
								else if (!(text == "Environment/Cellar/crate stack b_day"))
								{
									continue;
								}
							}
							else if (num != 997226039U)
							{
								if (num != 1164137223U)
								{
									continue;
								}
								if (!(text == "Environment/Dining/Dining Chair"))
								{
									continue;
								}
							}
							else if (!(text == "Environment/Cellar/Kitchen Cabinet"))
							{
								continue;
							}
						}
						else if (num <= 1635966011U)
						{
							if (num != 1564679559U)
							{
								if (num != 1635966011U)
								{
									continue;
								}
								if (!(text == "Environment/Dining/Dining Table_day"))
								{
									continue;
								}
							}
							else if (!(text == "Environment/Exterior/fountain_day"))
							{
								continue;
							}
						}
						else if (num != 1673595972U)
						{
							if (num != 1725197039U)
							{
								if (num != 2131548715U)
								{
									continue;
								}
								if (!(text == "Environment/Library/Dining Chair"))
								{
									continue;
								}
							}
							else if (!(text == "Environment/Bedroom/Small stool_day"))
							{
								continue;
							}
						}
						else if (!(text == "Environment/Bedroom/bed_day"))
						{
							continue;
						}
					}
					else if (num <= 2793735676U)
					{
						if (num <= 2345708179U)
						{
							if (num <= 2204046108U)
							{
								if (num != 2157057071U)
								{
									if (num != 2204046108U)
									{
										continue;
									}
									if (!(text == "Environment/Cellar/crate stack c_day"))
									{
										continue;
									}
								}
								else if (!(text == "Environment/Bathroom/bathtub_day"))
								{
									continue;
								}
							}
							else if (num != 2268111056U)
							{
								if (num != 2345708179U)
								{
									continue;
								}
								if (!(text == "Environment/Billiard/Billiard Table_day"))
								{
									continue;
								}
							}
							else if (!(text == "Environment/Lounge/Table Lamp"))
							{
								continue;
							}
						}
						else if (num <= 2610327542U)
						{
							if (num != 2448638925U)
							{
								if (num != 2610327542U)
								{
									continue;
								}
								if (!(text == "Environment/Ballroom/piano_day"))
								{
									continue;
								}
							}
							else if (!(text == "Environment/Lounge/Armchair"))
							{
								continue;
							}
						}
						else if (num != 2647554462U)
						{
							if (num != 2725196435U)
							{
								if (num != 2793735676U)
								{
									continue;
								}
								if (!(text == "Environment/Library/Medium Table"))
								{
									continue;
								}
							}
							else if (!(text == "Environment/Study/Table Lamp"))
							{
								continue;
							}
						}
						else if (!(text == "Environment/Library/library globe_day"))
						{
							continue;
						}
					}
					else if (num <= 3506308318U)
					{
						if (num <= 3296762319U)
						{
							if (num != 3129540431U)
							{
								if (num != 3296762319U)
								{
									continue;
								}
								if (!(text == "Environment/Dining/Non Feast Stuff"))
								{
									continue;
								}
							}
							else if (!(text == "Environment/Bathroom/Bathroom Sink"))
							{
								continue;
							}
						}
						else if (num != 3351739515U)
						{
							if (num != 3428928348U)
							{
								if (num != 3506308318U)
								{
									continue;
								}
								if (!(text == "Environment/Kitchen/Kitchen Cabinet"))
								{
									continue;
								}
							}
							else if (!(text == "Game Logic/signs"))
							{
								continue;
							}
						}
						else if (!(text == "Environment/Lounge/Coffee Table_day"))
						{
							continue;
						}
					}
					else if (num <= 3692068840U)
					{
						if (num != 3538008838U)
						{
							if (num != 3692068840U)
							{
								continue;
							}
							if (!(text == "Environment/Study/Desk"))
							{
								continue;
							}
						}
						else if (!(text == "Environment/Billiard/Loveseat"))
						{
							continue;
						}
					}
					else if (num != 3816374047U)
					{
						if (num != 4189042281U)
						{
							if (num != 4217587728U)
							{
								continue;
							}
							if (!(text == "Environment/Occlusion"))
							{
								continue;
							}
						}
						else if (!(text == "Environment/Cellar/wine barrel_day"))
						{
							continue;
						}
					}
					else if (!(text == "Environment/Exterior/tree_day"))
					{
						continue;
					}
					obj.SetActive(!val);
				}
			}, null, null, 24f, 24f, false, false);
			new VRCButton(mainGrp, "Force Start", "Force starts the game", delegate
			{
				bool flag6 = Murder4.Obj == null;
				if (flag6)
				{
					Murder4.Obj = GameObject.Find("Game Logic").GetComponent<UdonBehaviour>();
				}
				Murder4.Obj.SendCustomNetworkEvent(NetworkEventTarget.All, "Btn_Start");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new DuoButtons(mainGrp, "Self Murder", "Sets your role as murderer", delegate
			{
				CLog.L(APIUser.CurrentUser.displayName.ToString() + " set to murderer", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\WorldEx\\Murder4.cs", 177);
				for (int i = 0; i < 24; i++)
				{
					string playerEntryPath = "Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + i.ToString() + ")/Player Name Text";
					Transform playerEntryTransform = GameObject.Find("Game Logic").FindObject(playerEntryPath);
					GameObject playerEntry = ((playerEntryTransform != null) ? playerEntryTransform.gameObject : null);
					bool flag7 = playerEntry != null;
					if (flag7)
					{
						TMP_Text tmpTextComponent = playerEntry.GetComponent<TMP_Text>();
						Text textComponent = playerEntry.GetComponent<Text>();
						string playerName = APIUser.CurrentUser.displayName.ToString();
						bool flag8 = (tmpTextComponent != null && tmpTextComponent.text.Equals(playerName)) || (tmpTextComponent == null && textComponent != null && textComponent.text.Equals(playerName));
						if (flag8)
						{
							GameObject.Find("Player Node (" + i.ToString() + ")").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncAssignM");
							break;
						}
					}
					else
					{
						bool flag9 = !Murder4.SelfNode("SyncAssignM");
						if (flag9)
						{
							CLog.D("Trying SelfNode...", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\WorldEx\\Murder4.cs", 198);
						}
					}
				}
			}, "Self Bystander", "Sets your role as bystander", delegate
			{
				CLog.L(APIUser.CurrentUser.displayName.ToString() + " set to bystander", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\WorldEx\\Murder4.cs", 203);
				for (int j = 0; j < 24; j++)
				{
					string playerEntryPath2 = "Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + j.ToString() + ")/Player Name Text";
					Transform playerEntryTransform2 = GameObject.Find("Game Logic").FindObject(playerEntryPath2);
					GameObject playerEntry2 = ((playerEntryTransform2 != null) ? playerEntryTransform2.gameObject : null);
					bool flag10 = playerEntry2 != null;
					if (flag10)
					{
						TMP_Text tmpTextComponent2 = playerEntry2.GetComponent<TMP_Text>();
						Text textComponent2 = playerEntry2.GetComponent<Text>();
						string playerName2 = APIUser.CurrentUser.displayName.ToString();
						bool flag11 = (tmpTextComponent2 != null && tmpTextComponent2.text.Equals(playerName2)) || (tmpTextComponent2 == null && textComponent2 != null && textComponent2.text.Equals(playerName2));
						if (flag11)
						{
							GameObject.Find("Player Node (" + j.ToString() + ")").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncAssignB");
							break;
						}
					}
					else
					{
						bool flag12 = !Murder4.SelfNode("SyncAssignB");
						if (flag12)
						{
							CLog.D("Trying SelfNode...", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\WorldEx\\Murder4.cs", 224);
						}
					}
				}
			});
			new DuoButtons(mainGrp, "Murder Win", "Makes murderer win", delegate
			{
				bool flag13 = Murder4.Obj == null;
				if (flag13)
				{
					Murder4.Obj = GameObject.Find("Game Logic").GetComponent<UdonBehaviour>();
				}
				Murder4.Obj.SendCustomNetworkEvent(NetworkEventTarget.All, "SyncVictoryM");
			}, "Bystander Win", "Makes bystanders win", delegate
			{
				bool flag14 = Murder4.Obj == null;
				if (flag14)
				{
					Murder4.Obj = GameObject.Find("Game Logic").GetComponent<UdonBehaviour>();
				}
				Murder4.Obj.SendCustomNetworkEvent(NetworkEventTarget.All, "SyncVictoryB");
			});
			new DuoButtons(mainGrp, "Everyone Murder", "Makes everyone a murderer", delegate
			{
				for (int k = 0; k < 24; k++)
				{
					GameObject flag15 = GameObject.Find("Player Node (" + k.ToString() + ")");
					flag15.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncAssignM");
				}
			}, "Everyone Bystander", "Makes everyone a bystander", delegate
			{
				for (int l = 0; l < 24; l++)
				{
					GameObject flag16 = GameObject.Find("Player Node (" + l.ToString() + ")");
					flag16.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "SyncAssignB");
				}
			});
			new DuoButtons(mainGrp, "Lights On", "Turns on the lights", delegate
			{
				UdonUtils.SendUdonEventsWithName("SwitchUp");
			}, "Lights Off", "turns off the lights", delegate
			{
				UdonUtils.SendUdonEventsWithName("SwitchDown");
			});
			new DuoButtons(mainGrp, "Open All", "Open All", delegate
			{
				UdonUtils.SendUdonEventsWithName("SyncOpen");
				UdonUtils.SendUdonEventsWithName("SyncUnlockR");
				UdonUtils.SendUdonEventsWithName("SyncOpenR");
				UdonUtils.SendUdonEventsWithName("SyncBreakR");
			}, "Close All", "Close All", delegate
			{
				UdonUtils.SendUdonEventsWithName("SyncClose");
				UdonUtils.SendUdonEventsWithName("SyncLock");
			});
			new DuoButtons(mainGrp, "Shoot Revolver", "Shoots the revolver", delegate
			{
				bool flag17 = Murder4.Rev == null;
				if (flag17)
				{
					Murder4.Rev = GameObject.Find("Game Logic/Weapons/Revolver").GetComponent<UdonBehaviour>();
				}
				Murder4.Rev.SendCustomNetworkEvent(NetworkEventTarget.Owner, "Fire");
			}, "Spam Revolver", "Spam shoots the revolver", delegate
			{
				bool flag18 = Murder4.Rev == null;
				if (flag18)
				{
					Murder4.Rev = GameObject.Find("Game Logic/Weapons/Revolver").GetComponent<UdonBehaviour>();
				}
				Murder4.Rev.SendCustomNetworkEvent(NetworkEventTarget.All, "Fire");
				new WaitForSeconds(0.5f);
				Murder4.Rev.SendCustomNetworkEvent(NetworkEventTarget.All, "Fire");
				new WaitForSeconds(0.5f);
				Murder4.Rev.SendCustomNetworkEvent(NetworkEventTarget.All, "Fire");
			});
			new DuoButtons(mainGrp, "Shoot All", "Shoots the guns", delegate
			{
				UdonUtils.SendUdonEventsWithName("Fire");
			}, "Spam Guns", "Spam shoots all guns", delegate
			{
				UdonUtils.SendUdonEventsWithName("Fire");
				new WaitForSeconds(0.5f);
				UdonUtils.SendUdonEventsWithName("Fire");
				new WaitForSeconds(0.5f);
				UdonUtils.SendUdonEventsWithName("Fire");
			});
			new VRCButton(mainGrp, "Release Snake", "Releases the snake", delegate
			{
				GameObject.Find("Game Logic/Snakes/SnakeDispenser").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "DispenseSnake");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(mainGrp, "Kill All", "Kill Everyone", delegate
			{
				GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, "KillLocalPlayer");
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			CollapsibleButtonGroup weaponGrp = new CollapsibleButtonGroup(Murder4.subMenu, "Bring Weapons", true);
			new VRCButton(weaponGrp, "Bring Revolver", "Bring Revolver", delegate
			{
				GameObject flag19 = GameObject.Find("Game Logic/Weapons/Revolver");
				bool flag20 = flag19;
				if (flag20)
				{
					Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, flag19);
					flag19.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
				}
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(weaponGrp, "Bring Luger", "Bring Luger", delegate
			{
				GameObject flag21 = GameObject.Find("Game Logic/Weapons/Unlockables/Luger (0)");
				bool flag22 = flag21;
				if (flag22)
				{
					Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, flag21);
					flag21.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
				}
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(weaponGrp, "Bring Shotgun", "Bring Shotgun", delegate
			{
				GameObject flag23 = GameObject.Find("Game Logic/Weapons/Unlockables/Shotgun (0)");
				bool flag24 = flag23;
				if (flag24)
				{
					Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, flag23);
					flag23.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
				}
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(weaponGrp, "Bring Frag", "Bring Frag", delegate
			{
				GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)/Intact").active = true;
				GameObject flag25 = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)");
				bool flag26 = flag25;
				if (flag26)
				{
					Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, flag25);
					flag25.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
				}
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(weaponGrp, "Bring Smoke", "Bring Smoke", delegate
			{
				GameObject flag27 = GameObject.Find("Game Logic/Weapons/Unlockables/Smoke (0)");
				bool flag28 = flag27;
				if (flag28)
				{
					Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, flag27);
					flag27.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
				}
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(weaponGrp, "Bring Camera", "Bring Camera", delegate
			{
				GameObject flag29 = GameObject.Find("Game Logic/Polaroids Unlock Camera/FlashCamera");
				bool flag30 = flag29;
				if (flag30)
				{
					Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, flag29);
					flag29.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
				}
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(weaponGrp, "Bring Traps", "Bring Traps", delegate
			{
				GetNext.BringItem("Game Logic/Weapons/Bear Trap", 3);
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
			new VRCButton(weaponGrp, "Bring Knifes", "Bring Knifes", delegate
			{
				GetNext.BringItem("Weapons/Knife", 6);
			}, false, false, null, ExtentedControl.HalfType.Normal, false);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000F82C File Offset: 0x0000DA2C
		internal static void DoorColliders(bool state)
		{
			foreach (BoxCollider Doors in Resources.FindObjectsOfTypeAll<BoxCollider>())
			{
				bool flag = Doors.gameObject.name.Contains("Closed collision geo");
				if (flag)
				{
					Doors.GetComponent<BoxCollider>().enabled = state;
				}
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000F89C File Offset: 0x0000DA9C
		internal static bool SelfNode(string uEvent)
		{
			GameObject nodeAtCenter = null;
			for (int i = 0; i < 24; i++)
			{
				GameObject currentNode = GameObject.Find("Player Node (" + i.ToString() + ")");
				Vector3 colliderCenter = currentNode.GetComponent<CapsuleCollider>().center;
				float radius = currentNode.GetComponent<CapsuleCollider>().radius;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(37, 3);
				defaultInterpolatedStringHandler.AppendLiteral(" center: ");
				defaultInterpolatedStringHandler.AppendFormatted<Vector3>(colliderCenter);
				defaultInterpolatedStringHandler.AppendLiteral(" | radius: ");
				defaultInterpolatedStringHandler.AppendFormatted<float>(radius);
				defaultInterpolatedStringHandler.AppendLiteral(" | Player Node (");
				defaultInterpolatedStringHandler.AppendFormatted<int>(i);
				defaultInterpolatedStringHandler.AppendLiteral(")");
				CLog.D(defaultInterpolatedStringHandler.ToStringAndClear(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\WorldEx\\Murder4.cs", 397);
				bool flag = radius == 0.5f && colliderCenter == Vector3.zero;
				if (flag)
				{
					CLog.D("==============================", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\WorldEx\\Murder4.cs", 400);
					defaultInterpolatedStringHandler..ctor(33, 3);
					defaultInterpolatedStringHandler.AppendLiteral("Match Found! ");
					defaultInterpolatedStringHandler.AppendFormatted<Vector3>(colliderCenter);
					defaultInterpolatedStringHandler.AppendLiteral(" : ");
					defaultInterpolatedStringHandler.AppendFormatted<float>(radius);
					defaultInterpolatedStringHandler.AppendLiteral(" : Player Node (");
					defaultInterpolatedStringHandler.AppendFormatted<int>(i);
					defaultInterpolatedStringHandler.AppendLiteral(")");
					CLog.D(defaultInterpolatedStringHandler.ToStringAndClear(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\WorldEx\\Murder4.cs", 401);
					CLog.D("==============================", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Menus\\SubMenus\\WorldEx\\Murder4.cs", 402);
					nodeAtCenter = currentNode;
					break;
				}
			}
			if (nodeAtCenter != null)
			{
				nodeAtCenter.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, uEvent);
			}
			return nodeAtCenter;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000FA50 File Offset: 0x0000DC50
		internal static void DeathHudToggle(bool state)
		{
			foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
			{
				bool flag = gameObject.name.Contains("Death HUD Anim");
				if (flag)
				{
					gameObject.active = state;
				}
				bool flag2 = gameObject.name.Contains("Blind HUD Anim");
				if (flag2)
				{
					gameObject.active = state;
				}
				bool flag3 = gameObject.name.Contains("Flashbang HUD Anim");
				if (flag3)
				{
					gameObject.active = state;
				}
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000FAF0 File Offset: 0x0000DCF0
		internal static void AddAction()
		{
			JoinLeavePatch.OnLocalPlayerJoin = (Action<Player>)Delegate.Combine(JoinLeavePatch.OnLocalPlayerJoin, delegate(Player plr)
			{
				bool flag = Murder4.toggleLaser;
				if (flag)
				{
					GameObject.Find("Environment/Named Locations").SetActive(false);
					GameObject.Find("Environment/Sounds").SetActive(false);
				}
				Murder4.DeathHudToggle(Murder4.deathHud);
				Murder4.DoorColliders(Murder4.doorColliders);
				GameObject ped = GameObject.Find("Environment/Lobby (day only)/Pedestals");
				bool flag2 = ped;
				if (flag2)
				{
					ped.SetActive(false);
				}
				GameObject portalEnable = GameObject.Find("Portals enabler");
				bool flag3 = portalEnable;
				if (flag3)
				{
					portalEnable.SetActive(false);
				}
				GameObject signs = GameObject.Find("Game Logic/Signs");
				bool flag4 = signs;
				if (flag4)
				{
					signs.SetActive(false);
				}
			});
		}

		// Token: 0x0400017C RID: 380
		public static VRCPage subMenu;

		// Token: 0x0400017D RID: 381
		private static UdonBehaviour Obj;

		// Token: 0x0400017E RID: 382
		private static UdonBehaviour Rev;

		// Token: 0x0400017F RID: 383
		internal static bool goldGun;

		// Token: 0x04000180 RID: 384
		internal static bool goldGunGlobal;

		// Token: 0x04000181 RID: 385
		internal static bool semiAuto;

		// Token: 0x04000182 RID: 386
		internal static bool semiAutoGlobal;

		// Token: 0x04000183 RID: 387
		internal static bool explosiveRounds;

		// Token: 0x04000184 RID: 388
		internal static bool explosiveRoundsGlobal;

		// Token: 0x04000185 RID: 389
		internal static bool toggleLaser = false;

		// Token: 0x04000186 RID: 390
		internal static bool doorColliders = true;

		// Token: 0x04000187 RID: 391
		internal static bool deathHud = true;

		// Token: 0x04000188 RID: 392
		internal static bool showRolesState = false;

		// Token: 0x04000189 RID: 393
		internal static GameObject environment;
	}
}
