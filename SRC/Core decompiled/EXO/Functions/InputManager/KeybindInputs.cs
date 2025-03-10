using System;
using EXO.Core;
using EXO.Functions.PlayerFunc;
using EXO.LogTools;
using EXO.Menus.SelectMenus;
using EXO.Menus.SubMenus;
using EXO.Patches;
using EXO.Wrappers;
using HexedTools.HookUtils;
using UnityEngine;
using UnityEngine.UI;

namespace EXO.Functions.InputManager
{
	// Token: 0x020000A8 RID: 168
	internal class KeybindInputs : FunctionModule
	{
		// Token: 0x06000651 RID: 1617 RVA: 0x00023548 File Offset: 0x00021748
		public override void OnUpdate()
		{
			bool flag = KeybindInputs.logKeys;
			if (flag)
			{
				InputMap.LogAnyKeyPressed();
			}
			bool flag2 = InputMap.WasPressed(InputMap.GetKeyCode(Config.binds.Debug1), InputMap.GetKeyCode(Config.binds.Debug2));
			if (flag2)
			{
				GUILog.InitToScreen();
				GUILog.DisplayOnScreen("Test");
				ScreenAPI.Log("Test");
				CLog.L("Test", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 42);
			}
			else
			{
				bool flag3 = InputMap.WasPressed(InputMap.GetKeyCode(Config.binds.AntiUdonBind1), InputMap.GetKeyCode(Config.binds.AntiUdonBind2)) && Config.cfg.AntiUdonBind;
				if (flag3)
				{
					SettingsMenu.antiUdonBtn.State = !UdonSyncPatch.antiUdon;
					CLog.L("AntiUdon " + UdonSyncPatch.antiUdon.ToString(), true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 50);
				}
				else
				{
					bool flag4 = (InputMap.WasPressed(InputMap.GetKeyCode(Config.binds.SpeedBind1), InputMap.GetKeyCode(Config.binds.SpeedBind2)) || InputMap.WasPressed(InputMap.GetKeyCode(Config.binds.VRSpeedBind1), InputMap.GetKeyCode(Config.binds.VRSpeedBind2))) && Config.cfg.SpeedBind;
					if (flag4)
					{
						MovementMenu.speedBtn.State = !Config.cfg.SpeedToggle;
						CLog.L("Speed " + Config.cfg.SpeedToggle.ToString(), true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 59);
					}
					else
					{
						bool flag5 = ((InputMap.WasPressed(InputMap.GetKeyCode(Config.binds.FlyBind1), InputMap.GetKeyCode(Config.binds.FlyBind2)) || InputMap.WasPressed(InputMap.GetKeyCode(Config.binds.VRFlyBind1), InputMap.GetKeyCode(Config.binds.VRFlyBind2))) && Config.cfg.FlyBind) || (InputMap.DoublePress(0.3f) && Config.cfg.MinecraftFly);
						if (flag5)
						{
							bool noClipToggle = MoveFunc.noClipToggle;
							if (noClipToggle)
							{
								MovementMenu.noClipBtn.State = false;
							}
							MovementMenu.flyBtn.State = !Config.cfg.FlyToggle;
							CLog.L("Fly " + Config.cfg.FlyToggle.ToString(), true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 71);
						}
						else
						{
							bool flag6 = (InputMap.WasPressed(InputMap.GetKeyCode(Config.binds.NoClipBind1), InputMap.GetKeyCode(Config.binds.NoClipBind2)) || InputMap.WasPressed(InputMap.GetKeyCode(Config.binds.VRNoClipBind1), InputMap.GetKeyCode(Config.binds.VRNoClipBind2))) && Config.cfg.NoClipBind;
							if (flag6)
							{
								MovementMenu.noClipBtn.State = !MoveFunc.noClipToggle;
								bool flag7 = !MovementMenu.noClipBtn.State;
								if (flag7)
								{
									MovementMenu.flyBtn.State = false;
								}
								CLog.L("No Clip " + MoveFunc.noClipToggle.ToString(), true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 82);
							}
							else
							{
								bool flag8 = AttachToPlayer.isAttached && InputMap.DoublePress(0.3f);
								if (flag8)
								{
									AttachSelect.attachToggle.State = false;
									CLog.L("Fly " + Config.cfg.FlyToggle.ToString(), true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 90);
								}
								else
								{
									bool flag9 = InputMap.WasPressed(InputMap.GetKeyCode(Config.binds.SerializationBind1), InputMap.GetKeyCode(Config.binds.SerializationBind2)) && Config.cfg.SerializationBind;
									if (flag9)
									{
										UtilsMenu.serialToggle.State = !UtilsMenu.serialToggle.State;
										CLog.L("Serialization " + UtilsMenu.serialToggle.State.ToString(), true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 98);
									}
									else
									{
										bool flag10 = InputMap.WasPressed(InputMap.GetKeyCode(Config.binds.RespawnBind1), InputMap.GetKeyCode(Config.binds.RespawnBind2));
										if (flag10)
										{
											GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Respawn").GetComponent<Button>().onClick.Invoke();
										}
										else
										{
											bool flag11 = InputMap.WasPressed(InputMap.GetKeyCode(Config.binds.MouseTpBind1), InputMap.GetKeyCode(Config.binds.MouseTpBind2)) && Config.cfg.MouseTpBind && Camera.main;
											if (flag11)
											{
												RaycastHit[] PosData = Physics.RaycastAll(new Ray(Camera.main.transform.position, Camera.main.transform.forward));
												bool flag12 = PosData.Length != 0;
												if (flag12)
												{
													PlayerWrapper.LocalPlayer.transform.position = PosData[0].point;
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00023A28 File Offset: 0x00021C28
		internal static void ShowKeyBinds()
		{
			Logs.WriteOut("", 15);
			CLog.L("   You can change all these in VRChat/EXO/Keybinds.json   ", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 120);
			CLog.L("[======================= KeyBind =========================]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 121);
			CLog.L("[                                                         ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 122);
			CLog.L("[                 Respawn   =   Crtl + R                  ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 123);
			CLog.L("[                  Fly Up   =   E                         ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 124);
			CLog.L("[                Fly Down   =   Q                         ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 125);
			CLog.L("[                     Fly   =   Crtl + F                  ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 126);
			CLog.L("[                 No Clip   =   Crtl + X                  ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 127);
			CLog.L("[                   Speed   =   Crtl + G                  ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 128);
			CLog.L("[               Anti Udon   =   Crtl + U                  ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 129);
			CLog.L("[                Mouse TP   =   Crtl + Right Mouse        ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 130);
			CLog.L("[           Serialization   =   Crtl + Back Mouse         ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 131);
			CLog.L("[             Rotate Head   =   Crtl + Mouse Scroll       ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 132);
			CLog.L("[             Rotate Spin   =   Crtl + Shift              ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 133);
			CLog.L("[                                                         ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 134);
			CLog.L("[=======================- Zooms -=========================]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 135);
			CLog.L("[                                                         ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 136);
			CLog.L("[                    Zoom   =   Forward Mouse             ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 137);
			CLog.L("[             Zoom In/Out   =   Mouse Scroll              ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 138);
			CLog.L("[              Zoom Reset   =   Middle Mouse              ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 139);
			CLog.L("[                                                         ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 140);
			CLog.L("[======================- Cameras -========================]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 141);
			CLog.L("[                                                         ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 142);
			CLog.L("[              3rd Person   =   Crtl + T                  ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 143);
			CLog.L("[         3rd Person Zoom   =   Mouse Scroll              ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 144);
			CLog.L("[          3rd Person Fov   =   Side Mouse Buttons        ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 145);
			CLog.L("[        3rd Person Reset   =   Middle Mouse              ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 146);
			CLog.L("[                                                         ]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 147);
			CLog.L("[======================= KeyBind =========================]", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\InputManager\\KeybindInputs.cs", 148);
			Logs.WriteOut("", 15);
		}

		// Token: 0x040002FA RID: 762
		internal static bool logKeys;
	}
}
