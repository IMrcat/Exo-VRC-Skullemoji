using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CoreRuntime.Manager;
using EXO.Core;
using EXO.Functions.PlayerFunc;
using EXO.Modules.API;
using EXO.Modules.Utilities;
using EXO.Patches;
using EXO.Wrappers;
using TMPro;
using UnityEngine;
using VRC;

namespace EXO.Modules
{
	// Token: 0x02000071 RID: 113
	internal class NamePlates : ModsModule
	{
		// Token: 0x060003CC RID: 972 RVA: 0x00015644 File Offset: 0x00013844
		public override void OnInject()
		{
			Delegate onLocalPlayerJoin = JoinLeavePatch.OnLocalPlayerJoin;
			Action<Player> action;
			if ((action = NamePlates.<>O.<0>__AddTagsForPlayer) == null)
			{
				action = (NamePlates.<>O.<0>__AddTagsForPlayer = new Action<Player>(NamePlates.AddTagsForPlayer));
			}
			JoinLeavePatch.OnLocalPlayerJoin = (Action<Player>)Delegate.Combine(onLocalPlayerJoin, action);
			Delegate onPlayerJoin = JoinLeavePatch.OnPlayerJoin;
			Action<Player> action2;
			if ((action2 = NamePlates.<>O.<0>__AddTagsForPlayer) == null)
			{
				action2 = (NamePlates.<>O.<0>__AddTagsForPlayer = new Action<Player>(NamePlates.AddTagsForPlayer));
			}
			JoinLeavePatch.OnPlayerJoin = (Action<Player>)Delegate.Combine(onPlayerJoin, action2);
			Delegate onLocalPlayerLeave = JoinLeavePatch.OnLocalPlayerLeave;
			Action<Player> action3;
			if ((action3 = NamePlates.<>O.<1>__RemoveTags) == null)
			{
				action3 = (NamePlates.<>O.<1>__RemoveTags = new Action<Player>(NamePlates.RemoveTags));
			}
			JoinLeavePatch.OnLocalPlayerLeave = (Action<Player>)Delegate.Combine(onLocalPlayerLeave, action3);
			Delegate onPlayerLeave = JoinLeavePatch.OnPlayerLeave;
			Action<Player> action4;
			if ((action4 = NamePlates.<>O.<1>__RemoveTags) == null)
			{
				action4 = (NamePlates.<>O.<1>__RemoveTags = new Action<Player>(NamePlates.RemoveTags));
			}
			JoinLeavePatch.OnPlayerLeave = (Action<Player>)Delegate.Combine(onPlayerLeave, action4);
			QuickMenuPatch.OnQuickMenuOpen = (Action)Delegate.Combine(QuickMenuPatch.OnQuickMenuOpen, delegate
			{
				NamePlates.MoveTags(100f);
			});
			QuickMenuPatch.OnQuickMenuClose = (Action)Delegate.Combine(QuickMenuPatch.OnQuickMenuClose, delegate
			{
				NamePlates.MoveTags(-100f);
			});
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0001577C File Offset: 0x0001397C
		private static void AddTagsForPlayer(Player player)
		{
			bool flag = !Config.cfg.NamePlates;
			if (!flag)
			{
				UtilFunc.Delay(3f, delegate
				{
					NamePlates.AddTag("Temp", "", player, 0f, 45f, 0f);
					NamePlates.AddTag("Stats", "EXO", player, 0f, 45f, 0f);
					NamePlates.AddTag("Info", "EXO", player, 0f, 75f, 0f);
				});
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000157C4 File Offset: 0x000139C4
		private static void MoveTags(float y)
		{
			foreach (Dictionary<string, Transform> tagList in NamePlates.tags.Values)
			{
				foreach (Transform tag in tagList.Values)
				{
					bool flag = tag != null;
					if (flag)
					{
						tag.transform.localPosition += new Vector3(0f, y, 0f);
					}
				}
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00015888 File Offset: 0x00013A88
		public override void OnPlayerWasInit()
		{
			bool flag = !NamePlates.isUpdating;
			if (flag)
			{
				CoroutineManager.RunCoroutine(NamePlates.UpdateNamePlate());
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000158AD File Offset: 0x00013AAD
		public override void OnPlayerWasDestroyed()
		{
			CoroutineManager.StopCoroutine(NamePlates.UpdateNamePlate());
			NamePlates.isUpdating = false;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000158C1 File Offset: 0x00013AC1
		internal static IEnumerator UpdateNamePlate()
		{
			NamePlates.isUpdating = true;
			for (;;)
			{
				foreach (string userID in Enumerable.ToList<string>(NamePlates.tags.Keys))
				{
					Player player = PlayerWrapper.GetByUsrID(userID);
					bool flag = player == null;
					if (!flag)
					{
						PlayerNet playerNet = player._playerNet;
						bool flag2 = playerNet == null;
						if (flag2)
						{
							yield return new WaitForSeconds(1f);
						}
						bool flag3 = playerNet.field_Private_Byte_0 == 0 && playerNet.field_Private_Byte_1 == 0;
						if (flag3)
						{
							yield return new WaitForSeconds(1f);
						}
						NamePlates.RemoveTag(userID, "Temp");
						string blocked = (AntiBlock.hasBlockedYou.Contains(player.ActorID()) ? (" [" + "Blocked".HexColor("#ff0000") + "]") : "");
						string muted = (AntiBlock.hasMutedYou.Contains(player.ActorID()) ? (" [" + "Muted".HexColor("#606060") + "]") : "");
						UserRank rank = User.GetRank(player.GetAPIUser());
						string text = userID;
						string text2 = "Info";
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 5);
						defaultInterpolatedStringHandler.AppendFormatted(player.GetIsMaster() ? ("[" + "Master".HexColor("#c00000") + "] ") : "");
						defaultInterpolatedStringHandler.AppendLiteral("[");
						defaultInterpolatedStringHandler.AppendFormatted(rank.ToString().HexColor(NamePlates.GetRankColor(rank, player)));
						defaultInterpolatedStringHandler.AppendLiteral("] ");
						defaultInterpolatedStringHandler.AppendLiteral("[");
						defaultInterpolatedStringHandler.AppendFormatted(NamePlates.Platform(player));
						defaultInterpolatedStringHandler.AppendLiteral("]");
						defaultInterpolatedStringHandler.AppendFormatted(blocked);
						defaultInterpolatedStringHandler.AppendFormatted(muted);
						NamePlates.UpdateTag(text, text2, defaultInterpolatedStringHandler.ToStringAndClear());
						string text3 = userID;
						string text4 = "Stats";
						defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 2);
						defaultInterpolatedStringHandler.AppendLiteral("[");
						defaultInterpolatedStringHandler.AppendFormatted(player.GetFramesColor());
						defaultInterpolatedStringHandler.AppendLiteral("] ");
						defaultInterpolatedStringHandler.AppendLiteral("[");
						defaultInterpolatedStringHandler.AppendFormatted(player.GetPingColor());
						defaultInterpolatedStringHandler.AppendLiteral("]");
						NamePlates.UpdateTag(text3, text4, defaultInterpolatedStringHandler.ToStringAndClear());
						player = null;
						playerNet = null;
						blocked = null;
						muted = null;
						userID = null;
					}
				}
				List<string>.Enumerator enumerator = default(List<string>.Enumerator);
				yield return new WaitForSeconds(2f);
			}
			yield break;
			yield break;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000158CC File Offset: 0x00013ACC
		private static string Platform(Player player)
		{
			bool flag = !player._vrcplayer.GetIsInVR();
			string text;
			if (flag)
			{
				text = "Desktop".HexColor("#909090");
			}
			else
			{
				bool flag2 = player.IsQuest() && !player.IsFBT();
				if (flag2)
				{
					text = "Quest".HexColor("#00ff00");
				}
				else
				{
					string platform = (player.IsFBT() ? "FBT".HexColor("#c00000") : "VR".HexColor("#c00000"));
					bool flag3 = player.IsQuest();
					if (flag3)
					{
						platform += " Quest".HexColor("#00ff00");
					}
					text = platform;
				}
			}
			return text;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00015978 File Offset: 0x00013B78
		private static void AddTag(string tagID, string CustomTag, Player player, float x = 0f, float y = 45f, float z = 0f)
		{
			bool isOpen = QuickMenuPatch.IsOpen;
			if (isOpen)
			{
				y += 100f;
			}
			PlayerNameplate namePlate = player._vrcplayer.field_Public_PlayerNameplate_0;
			Transform transform = Object.Instantiate<Transform>(namePlate.gameObject.transform.Find("Contents/Quick Stats"), namePlate.gameObject.transform.Find("Contents"));
			transform.parent = namePlate.gameObject.transform.Find("Contents");
			transform.gameObject.SetActive(true);
			TextMeshProUGUI component = transform.Find("Trust Text").GetComponent<TextMeshProUGUI>();
			component.color = Color.white;
			transform.Find("Trust Icon").gameObject.SetActive(false);
			transform.Find("Performance Icon").gameObject.SetActive(false);
			transform.Find("Performance Text").gameObject.SetActive(false);
			transform.Find("Friend Anchor Stats").gameObject.SetActive(false);
			transform.name = "EXO NamePlate " + tagID;
			transform.gameObject.transform.localPosition = new Vector3(x, y, z);
			component.richText = true;
			component.text = CustomTag;
			bool flag = !NamePlates.tags.ContainsKey(player.UserID());
			if (flag)
			{
				NamePlates.tags[player.UserID()] = new Dictionary<string, Transform>();
			}
			NamePlates.tags[player.UserID()][tagID] = transform;
			NamePlates.SetPlateColor(namePlate, player, User.GetRank(player.GetAPIUser()));
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00015B10 File Offset: 0x00013D10
		internal static void SetPlateColor(PlayerNameplate namePlate, Player player, UserRank rank)
		{
			Color newColor = NamePlates.GetRankColor(rank, player);
			player._vrcplayer.field_Public_PlayerNameplate_0.field_Public_Sprite_3 = player._vrcplayer.field_Public_PlayerNameplate_0.field_Public_Sprite_4;
			newColor.a = namePlate.FindObject("Contents/Main/Background").color.a;
			namePlate.field_Public_Color_0 = newColor;
			namePlate.field_Public_Color_1 = newColor;
			namePlate.field_Public_Graphic_0.color = newColor;
			namePlate.field_Public_Graphic_1.color = newColor;
			namePlate.field_Public_Graphic_2.color = newColor;
			namePlate.field_Public_Graphic_3.color = newColor;
			namePlate.field_Public_Graphic_4.color = newColor;
			namePlate.field_Public_Graphic_5.color = newColor;
			namePlate.field_Public_Graphic_6.color = newColor;
			namePlate.field_Public_Graphic_7.color = newColor;
			namePlate.field_Public_Graphic_8.color = newColor;
			namePlate.field_Public_Graphic_9.color = newColor;
			namePlate.field_Public_Graphic_10.color = newColor;
			namePlate.gameObject.transform.Find("Contents/Quick Stats").GetComponent<ImageThreeSlice>().color = newColor;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00015C24 File Offset: 0x00013E24
		internal static Color GetRankColor(UserRank rank, Player player)
		{
			Color newColor;
			switch (rank)
			{
			case UserRank.MOD:
				newColor = new Color(1f, 0f, 0f);
				break;
			case UserRank.Trusted:
				newColor = new Color(0.5058824f, 0.2627451f, 0.9019608f);
				break;
			case UserRank.Known:
				newColor = new Color(1f, 0.48235294f, 0.25882354f);
				break;
			case UserRank.User:
				newColor = new Color(0.16862746f, 0.8117647f, 0.36078432f);
				break;
			case UserRank.New:
				newColor = new Color(0.09019608f, 0.47058824f, 1f);
				break;
			case UserRank.Visitor:
				newColor = new Color(0.8f, 0.8f, 0.8f);
				break;
			default:
				newColor = player.GetTrustColor();
				break;
			}
			return newColor;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00015CFC File Offset: 0x00013EFC
		public static void UpdateTag(string userID, string tagID, string newTag)
		{
			Dictionary<string, Transform> tag;
			Transform value;
			bool flag = NamePlates.tags.TryGetValue(userID, ref tag) && tag.TryGetValue(tagID, ref value);
			if (flag)
			{
				TextMeshProUGUI component = value.Find("Trust Text").GetComponent<TextMeshProUGUI>();
				component.text = newTag;
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00015D44 File Offset: 0x00013F44
		public static void RemoveTag(string userID, string tagID)
		{
			Dictionary<string, Transform> tag;
			Transform value;
			bool flag = NamePlates.tags.TryGetValue(userID, ref tag) && tag.TryGetValue(tagID, ref value);
			if (flag)
			{
				Object.Destroy(value.gameObject);
				tag.Remove(tagID);
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00015D88 File Offset: 0x00013F88
		private static void RemoveTags(Player player)
		{
			bool flag = player == null || !NamePlates.tags.ContainsKey(player.UserID());
			if (!flag)
			{
				foreach (Transform tag in NamePlates.tags[player.UserID()].Values)
				{
					bool flag2 = tag != null;
					if (flag2)
					{
						Object.Destroy(tag.gameObject);
					}
				}
				NamePlates.tags.Remove(player.UserID());
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00015E34 File Offset: 0x00014034
		public static void EnableTags()
		{
			foreach (Player player in PlayerWrapper.GetAllPlayers)
			{
				bool flag = !NamePlates.tags.ContainsKey(player.UserID());
				if (flag)
				{
					NamePlates.AddTagsForPlayer(player);
				}
			}
			foreach (Dictionary<string, Transform> tagList in NamePlates.tags.Values)
			{
				foreach (Transform tag in tagList.Values)
				{
					tag.gameObject.SetActive(true);
				}
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00015F10 File Offset: 0x00014110
		public static void DisableTags()
		{
			foreach (Dictionary<string, Transform> tagList in NamePlates.tags.Values)
			{
				foreach (Transform tag in tagList.Values)
				{
					tag.gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x040001ED RID: 493
		private static Dictionary<string, Dictionary<string, Transform>> tags = new Dictionary<string, Dictionary<string, Transform>>();

		// Token: 0x040001EE RID: 494
		internal static bool isUpdating;

		// Token: 0x0200014E RID: 334
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040005E2 RID: 1506
			public static Action<Player> <0>__AddTagsForPlayer;

			// Token: 0x040005E3 RID: 1507
			public static Action<Player> <1>__RemoveTags;
		}
	}
}
