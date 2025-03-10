using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using EXO.Core;
using EXO.LogTools;
using EXO.Modules.Utilities;
using EXO.Patches;
using EXO.Wrappers;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRC;
using WorldAPI.ButtonAPI.Extras;

namespace EXO.Functions.PlayerFunc
{
	// Token: 0x02000091 RID: 145
	internal class AntiBlock : FunctionModule
	{
		// Token: 0x060005C2 RID: 1474 RVA: 0x0001EB10 File Offset: 0x0001CD10
		public override void OnPlayerWasInit()
		{
			Transform par = UtilFunc.UserInterface.FindObject("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window");
			Action <>9__1;
			Action setActive = delegate
			{
				float num = 0.2f;
				Action action;
				if ((action = <>9__1) == null)
				{
					action = (<>9__1 = delegate
					{
						Transform blockGrp = par.FindObject("QMParent/Menu_Here/ScrollRect/Viewport/VerticalLayoutGroup/QM_Foldout_BlockedByUsersInWorld");
						Transform blockedUsers = par.FindObject("QMParent/Menu_Here/ScrollRect/Viewport/VerticalLayoutGroup/QM_List_BlockedByUsersInWorld");
						GameObject gameObject = blockGrp.gameObject;
						if (gameObject != null)
						{
							gameObject.SetActive(true);
						}
						GameObject gameObject2 = blockedUsers.gameObject;
						if (gameObject2 != null)
						{
							gameObject2.SetActive(true);
						}
					});
				}
				UtilFunc.Delay(num, action);
			};
			QuickMenuPatch.OnQuickMenuOpen = (Action)Delegate.Combine(QuickMenuPatch.OnQuickMenuOpen, setActive);
			UtilFunc.UserInterface.FindObject("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Here").GetOrAddComponent<Button>().onClick.AddListener(setActive);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001EB84 File Offset: 0x0001CD84
		internal static bool IsBlocking(EventData __0)
		{
			bool flag = __0.Code != 33 || !Config.cfg.AntiBlock;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				Dictionary<byte, global::Il2CppSystem.Object> dict = __0.CustomData.Cast<Dictionary<byte, global::Il2CppSystem.Object>>();
				byte b = dict[0].Unbox<byte>();
				byte b2 = b;
				if (b2 != 8)
				{
					if (b2 != 13)
					{
						if (b2 == 21)
						{
							bool flag3 = dict.Count == 4;
							if (flag3)
							{
								Player player = PlayerWrapper.GetByActorID(dict[1].Unbox<int>());
								bool flag4 = player != null;
								if (flag4)
								{
									bool flag5 = dict[10].Unbox<bool>();
									if (flag5)
									{
										AntiBlock.UserBlock(player);
									}
									else
									{
										bool flag6 = !dict[10].Unbox<bool>() && AntiBlock.hasBlockedYou.Contains(player.ActorID());
										if (flag6)
										{
											CLog.L(player.DisplayName() + " Unblocked You!", true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\PlayerFunc\\AntiBlock.cs", 55);
											AntiBlock.hasBlockedYou.Remove(player.ActorID());
										}
									}
									bool flag7 = dict[11].Unbox<bool>();
									if (flag7)
									{
										AntiBlock.UserMute(player, true);
									}
									else
									{
										bool flag8 = !dict[11].Unbox<bool>() && AntiBlock.hasMutedYou.Contains(player.ActorID());
										if (flag8)
										{
											AntiBlock.UserMute(player, false);
										}
									}
									return dict[10].Unbox<bool>();
								}
								return false;
							}
							else
							{
								bool flag9 = dict.Count == 3;
								if (flag9)
								{
									Il2CppArrayBase<int> blocks = Serialization.FromIL2CPPToManaged<int>(dict[10]);
									Il2CppArrayBase<int> mutes = Serialization.FromIL2CPPToManaged<int>(dict[11]);
									foreach (int actor in blocks)
									{
										Player player2 = PlayerWrapper.GetByActorID(actor);
										bool flag10 = player2 == null;
										if (flag10)
										{
											DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 1);
											defaultInterpolatedStringHandler.AppendLiteral("Unknown Actor (");
											defaultInterpolatedStringHandler.AppendFormatted<int>(actor);
											defaultInterpolatedStringHandler.AppendLiteral(") Blocked you!");
											CLog.L(defaultInterpolatedStringHandler.ToStringAndClear(), true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\PlayerFunc\\AntiBlock.cs", 76);
										}
										else
										{
											bool flag11 = !AntiBlock.hasBlockedYou.Contains(actor);
											if (flag11)
											{
												AntiBlock.UserBlock(player2);
											}
											else
											{
												CLog.L(player2.DisplayName() + " Unblocked You!", true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\PlayerFunc\\AntiBlock.cs", 84);
												AntiBlock.hasBlockedYou.Remove(actor);
											}
										}
									}
									foreach (int actor2 in mutes)
									{
										AntiBlock.UserMute(PlayerWrapper.GetByActorID(actor2), !AntiBlock.hasMutedYou.Contains(actor2));
									}
									bool flag12 = blocks.Length > 0;
									if (flag12)
									{
										return true;
									}
								}
							}
						}
					}
					else
					{
						Player player3 = PlayerWrapper.GetByActorID(dict[1].Unbox<int>());
						Player player4 = PlayerWrapper.GetByActorID(dict[2].Unbox<int>());
						string target = dict[2].ToString().Replace("A vote kick has been initiated against ", "").Replace(", do you agree?", "");
						CLog.L("Votekick started on " + target, true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\PlayerFunc\\AntiBlock.cs", 102);
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
						defaultInterpolatedStringHandler..ctor(24, 2);
						defaultInterpolatedStringHandler.AppendLiteral("Votekick started on ");
						defaultInterpolatedStringHandler.AppendFormatted<Player>(player3);
						defaultInterpolatedStringHandler.AppendLiteral(" by ");
						defaultInterpolatedStringHandler.AppendFormatted<Player>(player4);
						CLog.L(defaultInterpolatedStringHandler.ToStringAndClear(), true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\PlayerFunc\\AntiBlock.cs", 103);
					}
					flag2 = false;
				}
				else
				{
					CLog.L("The Room Owner Just Tried to Mute You!", true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\PlayerFunc\\AntiBlock.cs", 96);
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001EF7C File Offset: 0x0001D17C
		private static void UserMute(Player player, bool muting)
		{
			if (muting)
			{
				CLog.L(player.DisplayName() + " Muted You!", true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\PlayerFunc\\AntiBlock.cs", 114);
				AntiBlock.hasMutedYou.Add(player.ActorID());
			}
			else
			{
				CLog.L(player.DisplayName() + " Unmuted You!", true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\PlayerFunc\\AntiBlock.cs", 119);
				AntiBlock.hasMutedYou.Remove(player.ActorID());
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001EFF3 File Offset: 0x0001D1F3
		internal static void UserBlock(Player player)
		{
			CLog.L(player.DisplayName() + " Blocked You!", true, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\PlayerFunc\\AntiBlock.cs", 126);
			AntiBlock.hasBlockedYou.Add(player.ActorID());
		}

		// Token: 0x040002A0 RID: 672
		internal static List<int> hasBlockedYou = new List<int>();

		// Token: 0x040002A1 RID: 673
		internal static List<int> hasMutedYou = new List<int>();
	}
}
