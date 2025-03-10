using System;
using System.Linq;
using System.Runtime.CompilerServices;
using EXO.Patches;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.Networking;
using VRC.SDK.Internal;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;
using VRCSDK2;

namespace EXO.Wrappers
{
	// Token: 0x02000036 RID: 54
	internal class WorldWrapper
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x0000AA9F File Offset: 0x00008C9F
		internal static void AddAction()
		{
			JoinLeavePatch.OnLocalPlayerJoin = (Action<Player>)Delegate.Combine(JoinLeavePatch.OnLocalPlayerJoin, delegate(Player plr)
			{
				WorldWrapper.Init();
				WorldWrapper.FindItems();
				WorldWrapper.FindTriggers();
				WorldWrapper.FindInteractables();
			});
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000AAD5 File Offset: 0x00008CD5
		internal static ApiWorld ApiWorld
		{
			get
			{
				return RoomManager.field_Internal_Static_ApiWorld_0;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000AADC File Offset: 0x00008CDC
		[Nullable(2)]
		internal static ApiWorldInstance ApiWorldInstance
		{
			[NullableContext(2)]
			get
			{
				bool flag = WorldWrapper.CurrentWorld == -1;
				if (flag)
				{
					WorldWrapper.CurrentWorld = ((RoomManager.field_Private_Static_ApiWorldInstance_0 != null) ? 0 : 1);
				}
				return (WorldWrapper.CurrentWorld == 0) ? RoomManager.field_Private_Static_ApiWorldInstance_0 : RoomManager.field_Private_Static_ApiWorldInstance_1;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000AB1D File Offset: 0x00008D1D
		internal static string WorldID
		{
			get
			{
				return WorldWrapper.ApiWorld.id;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000AB29 File Offset: 0x00008D29
		internal static string InstanceID
		{
			get
			{
				return WorldWrapper.ApiWorldInstance.instanceId;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000AB35 File Offset: 0x00008D35
		internal static string Current_World_ID
		{
			get
			{
				return WorldWrapper.WorldID + ":" + WorldWrapper.InstanceID;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000AB4B File Offset: 0x00008D4B
		internal static bool In_World
		{
			get
			{
				return WorldWrapper.ApiWorld != null && PlayerWrapper.LocalPlayer != null;
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000AB64 File Offset: 0x00008D64
		internal static void FindItems()
		{
			WorldWrapper.sdk2Items = Resources.FindObjectsOfTypeAll<global::VRCSDK2.VRC_Pickup>();
			WorldWrapper.sdk3Items = Resources.FindObjectsOfTypeAll<VRCPickup>();
			WorldWrapper.allSyncItems = Resources.FindObjectsOfTypeAll<VRC_ObjectSync>();
			WorldWrapper.allSDK3SyncItems = Resources.FindObjectsOfTypeAll<VRCObjectSync>();
			WorldWrapper.allPoolItems = Resources.FindObjectsOfTypeAll<VRCObjectPool>();
			WorldWrapper.sdk3Items = Object.FindObjectsOfType<VRCPickup>();
			WorldWrapper.allSyncItems = Enumerable.ToArray<VRC_ObjectSync>(Enumerable.Where<VRC_ObjectSync>(Resources.FindObjectsOfTypeAll<VRC_ObjectSync>(), (VRC_ObjectSync x) => !Enumerable.Any<string>(WorldWrapper.toSkip, (string y) => y.Contains(x.gameObject.name))));
			WorldWrapper.allBaseUdonItem = Enumerable.ToArray<global::VRC.SDKBase.VRC_Pickup>(Enumerable.Where<global::VRC.SDKBase.VRC_Pickup>(Resources.FindObjectsOfTypeAll<global::VRC.SDKBase.VRC_Pickup>(), (global::VRC.SDKBase.VRC_Pickup x) => !Enumerable.Any<string>(WorldWrapper.toSkip, (string y) => y.Contains(x.gameObject.name))));
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000AC19 File Offset: 0x00008E19
		internal static void FindInteractables()
		{
			WorldWrapper.allInteractable = Resources.FindObjectsOfTypeAll<VRCInteractable>();
			WorldWrapper.allBaseInteractable = Resources.FindObjectsOfTypeAll<global::VRC.SDKBase.VRC_Interactable>();
			WorldWrapper.allSDK2Interactable = Resources.FindObjectsOfTypeAll<global::VRCSDK2.VRC_Interactable>();
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000AC3A File Offset: 0x00008E3A
		internal static void FindTriggers()
		{
			WorldWrapper.allTriggers = Resources.FindObjectsOfTypeAll<global::VRC.SDKBase.VRC_Trigger>();
			WorldWrapper.allSDK2Triggers = Resources.FindObjectsOfTypeAll<global::VRCSDK2.VRC_Trigger>();
			WorldWrapper.allTriggerCol = Resources.FindObjectsOfTypeAll<global::VRC.SDKBase.VRC_TriggerColliderEventTrigger>();
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000AC5C File Offset: 0x00008E5C
		internal static void Init()
		{
			WorldWrapper.SceneDescriptor = Object.FindObjectOfType<global::VRC.SDKBase.VRC_SceneDescriptor>(true);
			WorldWrapper.SDK2SceneDescriptor = Object.FindObjectOfType<global::VRCSDK2.VRC_SceneDescriptor>(true);
			WorldWrapper.SDK3SceneDescriptor = Object.FindObjectOfType<VRCSceneDescriptor>(true);
			WorldWrapper.udonBehaviours = Object.FindObjectsOfType<UdonBehaviour>();
			WorldWrapper.udonSync = Resources.FindObjectsOfTypeAll<UdonSync>();
			WorldWrapper.udonManagers = Resources.FindObjectsOfTypeAll<UdonManager>();
			WorldWrapper.udonOnTrigger = Resources.FindObjectsOfTypeAll<OnTriggerStayProxy>();
			WorldWrapper.udonOnCol = Resources.FindObjectsOfTypeAll<OnCollisionStayProxy>();
			WorldWrapper.udonOnRender = Resources.FindObjectsOfTypeAll<OnRenderObjectProxy>();
			WorldWrapper.allSDKUdon = Resources.FindObjectsOfTypeAll<VRCUdonAnalytics>();
			if (WorldWrapper.highlightsFX == null)
			{
				WorldWrapper.highlightsFX = Enumerable.FirstOrDefault<HighlightsFXStandalone>(Resources.FindObjectsOfTypeAll<HighlightsFXStandalone>());
			}
			WorldWrapper.oldRespawnHight = WorldWrapper.SDKRespawnHeight;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000ACF6 File Offset: 0x00008EF6
		internal static InstanceAccessType GetWorldType()
		{
			return WorldWrapper.ApiWorldInstance.type;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000AD04 File Offset: 0x00008F04
		internal static InstanceAccessType GetWorldType(string WorldID)
		{
			bool flag = WorldID.Contains("~canRequestInvite");
			InstanceAccessType instanceAccessType;
			if (flag)
			{
				instanceAccessType = InstanceAccessType.InvitePlus;
			}
			else
			{
				bool flag2 = WorldID.Contains("~private");
				if (flag2)
				{
					instanceAccessType = InstanceAccessType.InviteOnly;
				}
				else
				{
					bool flag3 = WorldID.Contains("~friends");
					if (flag3)
					{
						instanceAccessType = InstanceAccessType.FriendsOnly;
					}
					else
					{
						bool flag4 = WorldID.Contains("~hidden");
						if (flag4)
						{
							instanceAccessType = InstanceAccessType.FriendsOfGuests;
						}
						else
						{
							instanceAccessType = InstanceAccessType.Public;
						}
					}
				}
			}
			return instanceAccessType;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000AD68 File Offset: 0x00008F68
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000ADD4 File Offset: 0x00008FD4
		internal static float SDKRespawnHeight
		{
			get
			{
				bool flag = WorldWrapper.SDK3SceneDescriptor != null;
				float num;
				if (flag)
				{
					num = WorldWrapper.SDK3SceneDescriptor.RespawnHeightY;
				}
				else
				{
					bool flag2 = WorldWrapper.SDK2SceneDescriptor != null;
					if (flag2)
					{
						num = WorldWrapper.SDK2SceneDescriptor.RespawnHeightY;
					}
					else
					{
						bool flag3 = WorldWrapper.SceneDescriptor != null;
						if (flag3)
						{
							num = WorldWrapper.SceneDescriptor.RespawnHeightY;
						}
						else
						{
							num = 0f;
						}
					}
				}
				return num;
			}
			set
			{
				bool flag = WorldWrapper.SDK3SceneDescriptor != null;
				if (flag)
				{
					WorldWrapper.SDK3SceneDescriptor.RespawnHeightY = value;
				}
				else
				{
					bool flag2 = WorldWrapper.SDK2SceneDescriptor != null;
					if (flag2)
					{
						WorldWrapper.SDK2SceneDescriptor.RespawnHeightY = value;
					}
					else
					{
						bool flag3 = WorldWrapper.SceneDescriptor != null;
						if (flag3)
						{
							WorldWrapper.SceneDescriptor.RespawnHeightY = value;
						}
					}
				}
			}
		}

		// Token: 0x04000116 RID: 278
		private static int CurrentWorld = -1;

		// Token: 0x04000117 RID: 279
		private static global::VRC.SDKBase.VRC_SceneDescriptor SceneDescriptor;

		// Token: 0x04000118 RID: 280
		private static global::VRCSDK2.VRC_SceneDescriptor SDK2SceneDescriptor;

		// Token: 0x04000119 RID: 281
		private static VRCSceneDescriptor SDK3SceneDescriptor;

		// Token: 0x0400011A RID: 282
		internal static HighlightsFXStandalone highlightsFX;

		// Token: 0x0400011B RID: 283
		internal static UdonBehaviour[] udonBehaviours;

		// Token: 0x0400011C RID: 284
		internal static Il2CppArrayBase<UdonSync> udonSync;

		// Token: 0x0400011D RID: 285
		internal static Il2CppArrayBase<UdonManager> udonManagers = Resources.FindObjectsOfTypeAll<UdonManager>();

		// Token: 0x0400011E RID: 286
		internal static Il2CppArrayBase<OnTriggerStayProxy> udonOnTrigger;

		// Token: 0x0400011F RID: 287
		internal static Il2CppArrayBase<OnCollisionStayProxy> udonOnCol;

		// Token: 0x04000120 RID: 288
		internal static Il2CppArrayBase<OnRenderObjectProxy> udonOnRender;

		// Token: 0x04000121 RID: 289
		internal static Il2CppArrayBase<VRCUdonAnalytics> allSDKUdon;

		// Token: 0x04000122 RID: 290
		internal static global::VRC.SDKBase.VRC_Pickup[] allBaseUdonItem;

		// Token: 0x04000123 RID: 291
		internal static VRC_ObjectSync[] allSyncItems;

		// Token: 0x04000124 RID: 292
		internal static Il2CppArrayBase<global::VRCSDK2.VRC_Pickup> sdk2Items;

		// Token: 0x04000125 RID: 293
		internal static Il2CppArrayBase<VRCPickup> sdk3Items;

		// Token: 0x04000126 RID: 294
		internal static Il2CppArrayBase<VRCObjectSync> allSDK3SyncItems;

		// Token: 0x04000127 RID: 295
		internal static Il2CppArrayBase<VRCObjectPool> allPoolItems;

		// Token: 0x04000128 RID: 296
		internal static Il2CppArrayBase<VRCInteractable> allInteractable;

		// Token: 0x04000129 RID: 297
		internal static Il2CppArrayBase<global::VRCSDK2.VRC_Interactable> allSDK2Interactable;

		// Token: 0x0400012A RID: 298
		internal static Il2CppArrayBase<global::VRC.SDKBase.VRC_Interactable> allBaseInteractable;

		// Token: 0x0400012B RID: 299
		internal static Il2CppArrayBase<global::VRC.SDKBase.VRC_Trigger> allTriggers;

		// Token: 0x0400012C RID: 300
		internal static Il2CppArrayBase<global::VRC.SDKBase.VRC_TriggerColliderEventTrigger> allTriggerCol;

		// Token: 0x0400012D RID: 301
		internal static Il2CppArrayBase<global::VRCSDK2.VRC_Trigger> allSDK2Triggers;

		// Token: 0x0400012E RID: 302
		internal static float oldRespawnHight;

		// Token: 0x0400012F RID: 303
		private static readonly string[] toSkip = new string[] { "PhotoCamera", "MirrorPickup", "ViewFinder", "AvatarDebugConsole", "OscDebugConsole" };
	}
}
