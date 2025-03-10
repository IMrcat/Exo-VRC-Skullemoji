using System;
using EXO.Wrappers;
using UnityEngine;
using VRC;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

namespace EXO.Modules.Utilities
{
	// Token: 0x02000079 RID: 121
	internal static class UdonUtils
	{
		// Token: 0x06000434 RID: 1076 RVA: 0x00017FD8 File Offset: 0x000161D8
		public static void SendUdonEvent(string obj, string uEvent, NetworkEventTarget target)
		{
			GameObject gameObject = GameObject.Find(obj);
			bool flag = gameObject;
			if (flag)
			{
				gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(target, uEvent);
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00018008 File Offset: 0x00016208
		public static void SendUdonEventsWithName(string uEvent)
		{
			WorldWrapper.udonBehaviours = Object.FindObjectsOfType<UdonBehaviour>();
			foreach (UdonBehaviour udonBehaviour in WorldWrapper.udonBehaviours)
			{
				bool flag = udonBehaviour._eventTable.ContainsKey(uEvent);
				if (flag)
				{
					udonBehaviour.gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(NetworkEventTarget.All, uEvent);
				}
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00018064 File Offset: 0x00016264
		public static Player GrabOwner(this GameObject gameObject)
		{
			foreach (Player player in PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0)
			{
				bool flag = player.field_Private_VRCPlayerApi_0.IsOwner(gameObject);
				if (flag)
				{
					return player;
				}
			}
			return null;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x000180B0 File Offset: 0x000162B0
		public static void SetEventOwner(this GameObject gameObject, Player player)
		{
			bool flag = gameObject.GrabOwner() != player;
			if (flag)
			{
				Networking.SetOwner(player.field_Private_VRCPlayerApi_0, gameObject);
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000180DC File Offset: 0x000162DC
		public static void TargetedEvent(string uEvent, VRCPlayer player)
		{
			WorldWrapper.udonBehaviours = Object.FindObjectsOfType<UdonBehaviour>();
			foreach (UdonBehaviour udonBehaviour in WorldWrapper.udonBehaviours)
			{
				bool flag = udonBehaviour._eventTable.ContainsKey(uEvent);
				if (flag)
				{
					udonBehaviour.gameObject.SetEventOwner(player.prop_Player_0);
					udonBehaviour.SendCustomNetworkEvent(NetworkEventTarget.Owner, uEvent);
				}
			}
		}
	}
}
