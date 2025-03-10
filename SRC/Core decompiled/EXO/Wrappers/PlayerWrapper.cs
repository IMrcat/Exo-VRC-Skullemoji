using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using EXO.Functions.Avatar;
using EXO.Modules.Utilities;
using Il2CppSystem.Collections.Generic;
using Photon.Realtime;
using RootMotion.FinalIK;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using VRC;
using VRC.Avatar;
using VRC.Core;
using VRC.SDKBase;
using VRC.UI.Elements.Menus;

namespace EXO.Wrappers
{
	// Token: 0x02000035 RID: 53
	internal static class PlayerWrapper
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00009CC8 File Offset: 0x00007EC8
		private static GameObject UserInterface
		{
			get
			{
				return GameObject.Find("Canvas_QuickMenu(Clone)").transform.parent.gameObject;
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00009CE4 File Offset: 0x00007EE4
		internal static GameObject GetLocalPlayer()
		{
			foreach (GameObject gameObject in PlayerWrapper.GetAllGameObjects())
			{
				bool flag = gameObject.name.StartsWith("VRCPlayer[Local]");
				if (flag)
				{
					return gameObject;
				}
			}
			return new GameObject();
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00009D30 File Offset: 0x00007F30
		internal static global::VRC.Player[] GrabAllPlayersArr()
		{
			List<global::VRC.Player> players = new List<global::VRC.Player>();
			foreach (GameObject gameObject in PlayerWrapper.GetAllGameObjects())
			{
				bool flag = gameObject.name.StartsWith("VRCPlayer");
				if (flag)
				{
					global::VRC.Player playerComponent = gameObject.GetComponent<global::VRC.Player>();
					bool flag2 = playerComponent != null;
					if (flag2)
					{
						players.Add(playerComponent);
					}
				}
			}
			return players.ToArray();
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00009DA4 File Offset: 0x00007FA4
		internal static GameObject GrabAllPlayers()
		{
			foreach (GameObject gameObject in PlayerWrapper.GetAllGameObjects())
			{
				bool flag = gameObject.name.StartsWith("VRCPlayer");
				if (flag)
				{
					return gameObject;
				}
			}
			return new GameObject();
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00009DF0 File Offset: 0x00007FF0
		internal static GameObject[] GetAllGameObjects()
		{
			return SceneManager.GetActiveScene().GetRootGameObjects();
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00009E14 File Offset: 0x00008014
		public static string GetGameObjectPath(Transform transform)
		{
			string path = transform.name;
			while (transform.parent != null)
			{
				transform = transform.parent;
				path = transform.name + "/" + path;
			}
			return path;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00009E5C File Offset: 0x0000805C
		internal static global::VRC.Player[] GetAllPlayers
		{
			get
			{
				return PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray();
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00009E74 File Offset: 0x00008074
		internal static global::VRC.Player GetByActorID(int actorId)
		{
			return Enumerable.FirstOrDefault<global::VRC.Player>(PlayerWrapper.GetAllPlayers, (global::VRC.Player a) => a.field_Private_VRCPlayerApi_0.playerId == actorId);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009EA4 File Offset: 0x000080A4
		internal static global::VRC.Player GetByAvatrID(string AvId)
		{
			return Enumerable.FirstOrDefault<global::VRC.Player>(PlayerWrapper.GetAllPlayers, delegate(global::VRC.Player a)
			{
				ApiAvatar apiAvatar = a.prop_ApiAvatar_0;
				return ((apiAvatar != null) ? apiAvatar.id : null) == AvId;
			});
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00009ED4 File Offset: 0x000080D4
		internal static global::VRC.Player GetByUsrID(string usrID)
		{
			return Enumerable.First<global::VRC.Player>(PlayerWrapper.GetAllPlayers, (global::VRC.Player x) => x.prop_APIUser_0.id == usrID);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009F04 File Offset: 0x00008104
		internal static void Teleport(this global::VRC.Player player)
		{
			PlayerWrapper.LocalVRCPlayer.transform.position = player.prop_VRCPlayer_0.transform.position;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00009F26 File Offset: 0x00008126
		internal static bool CanHearMe(int Actor)
		{
			return InterestManager.Method_Public_Static_Boolean_Int32_PDM_0(Actor);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00009F2E File Offset: 0x0000812E
		internal static bool CanHearMe(this Photon.Realtime.Player Player)
		{
			return PlayerWrapper.CanHearMe(Player.ActorID());
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00009F3B File Offset: 0x0000813B
		internal static bool CanHearMe(this global::VRC.Player Player)
		{
			return PlayerWrapper.CanHearMe(Player.GetPhotonPlayer().ActorID());
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00009F4D File Offset: 0x0000814D
		internal static global::VRC.Player LocalPlayer
		{
			get
			{
				return global::VRC.Player.prop_Player_0;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00009F54 File Offset: 0x00008154
		internal static string LocalUserID
		{
			get
			{
				return PlayerWrapper.LocalPlayer.UserID();
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00009F60 File Offset: 0x00008160
		internal static VRCPlayer LocalVRCPlayer
		{
			get
			{
				return VRCPlayer.field_Internal_Static_VRCPlayer_0;
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00009F67 File Offset: 0x00008167
		internal static APIUser GetAPIUser(this global::VRC.Player player)
		{
			return player.prop_APIUser_0;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00009F6F File Offset: 0x0000816F
		internal static float GetFrames(this global::VRC.Player player)
		{
			return (player._playerNet.prop_Byte_0 != 0) ? Mathf.Floor(1000f / (float)player._playerNet.prop_Byte_0) : (-1f);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00009F9C File Offset: 0x0000819C
		internal static short GetPing(this global::VRC.Player player)
		{
			return player._playerNet.field_Private_Int16_0;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00009FAC File Offset: 0x000081AC
		internal static bool IsBot(this global::VRC.Player player)
		{
			return (player.GetPing() <= 0 && player.GetFrames() <= 0f && player.UserID() != APIUser.CurrentUser.id) || player.transform.position == Vector3.zero;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00009FFE File Offset: 0x000081FE
		internal static global::VRC.Player GetSelectedUser
		{
			get
			{
				return PlayerWrapper.GetByUsrID(PlayerWrapper.UserInterface.GetComponentInChildren<SelectedUserMenuQM>().field_Private_IUser_0.prop_String_0);
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000A019 File Offset: 0x00008219
		internal static global::VRC.Player GetPlayer(this VRCPlayer player)
		{
			return player.prop_Player_0;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000A021 File Offset: 0x00008221
		internal static VRCPlayer GetVRCPlayer(this global::VRC.Player player)
		{
			return player._vrcplayer;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000A029 File Offset: 0x00008229
		internal static VRCPlayer GetVRCPlayerW
		{
			get
			{
				return PlayerWrapper.LocalPlayer._vrcplayer;
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000A035 File Offset: 0x00008235
		internal static Color GetTrustColor(this global::VRC.Player player)
		{
			return VRCPlayer.Method_Public_Static_Color_APIUser_0(player.GetAPIUser());
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000A042 File Offset: 0x00008242
		internal static APIUser GetAPIUser(this VRCPlayer Instance)
		{
			return Instance.GetPlayer().GetAPIUser();
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000A04F File Offset: 0x0000824F
		internal static VRCPlayerApi GetVRCPlayerApi(this global::VRC.Player Instance)
		{
			return (Instance != null) ? Instance.prop_VRCPlayerApi_0 : null;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000A05D File Offset: 0x0000825D
		internal static bool GetIsMaster(this global::VRC.Player Instance)
		{
			return Instance.GetVRCPlayerApi().isMaster;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000A06A File Offset: 0x0000826A
		internal static bool GetIsVRCDev(this global::VRC.Player Instance)
		{
			return Instance.GetVRCPlayerApi().isModerator;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000A077 File Offset: 0x00008277
		internal static int GetActorNumber(this global::VRC.Player player)
		{
			return (player.GetVRCPlayerApi() != null) ? player.GetVRCPlayerApi().playerId : (-1);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000A08F File Offset: 0x0000828F
		internal static USpeaker GetUspeaker(this global::VRC.Player player)
		{
			return player.prop_USpeaker_0;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000A097 File Offset: 0x00008297
		internal static Photon.Realtime.Player GetPhotonPlayer(this global::VRC.Player player)
		{
			return player.prop_Player_1;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000A0A0 File Offset: 0x000082A0
		internal static bool ClientDetect(this global::VRC.Player player)
		{
			return player.GetFrames() > 111f || player.GetFrames() < 10f || player.GetPing() > 5400 || player.GetPing() < 10 || PlayerWrapper.ClientUsers.Contains(player.UserID());
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000A0F1 File Offset: 0x000082F1
		internal static ApiAvatar GetAPIAvatar(this VRCPlayer vrcPlayer)
		{
			return vrcPlayer.prop_ApiAvatar_0;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A0F9 File Offset: 0x000082F9
		internal static ApiAvatar GetAPIAvatar(this global::VRC.Player player)
		{
			return player.GetVRCPlayer().GetAPIAvatar();
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000A106 File Offset: 0x00008306
		internal static VRCPlayer GetVRCPlayer2
		{
			get
			{
				return VRCPlayer.field_Internal_Static_VRCPlayer_0;
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000A10D File Offset: 0x0000830D
		internal static Animator GetAnimator(this VRCPlayer player)
		{
			return player.field_Internal_Animator_0;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000A115 File Offset: 0x00008315
		internal static string UserID(this global::VRC.Player Instance)
		{
			return Instance.GetAPIUser().id;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000A122 File Offset: 0x00008322
		internal static string UserID(this VRCPlayer Instance)
		{
			return Instance.GetAPIUser().id;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A12F File Offset: 0x0000832F
		internal static void ReloadAvatar(this global::VRC.Player Instance)
		{
			VRCPlayer.Method_Public_Static_Void_APIUser_0(Instance.GetAPIUser());
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000A13D File Offset: 0x0000833D
		internal static bool IsFriend(this global::VRC.Player player)
		{
			return APIUser.CurrentUser.friendIDs.Contains(player.field_Private_APIUser_0.id);
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000A159 File Offset: 0x00008359
		internal static int GetPlayerCount
		{
			get
			{
				return VRCPlayerApi.GetPlayerCount();
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000A160 File Offset: 0x00008360
		internal static string DisplayName(this VRCPlayer Instance)
		{
			return Instance.GetAPIUser().displayName;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000A16D File Offset: 0x0000836D
		internal static string DisplayName(this global::VRC.Player Instance)
		{
			return Instance.GetAPIUser().displayName;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000A17A File Offset: 0x0000837A
		internal static string DisplayName(this APIUser Instance)
		{
			return Instance.displayName;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000A182 File Offset: 0x00008382
		internal static VRCPlayerApi GetVRCPlayerApi(this VRCPlayer Instance)
		{
			return Instance.prop_VRCPlayerApi_0;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000A18A File Offset: 0x0000838A
		internal static bool GetIsInVR(this VRCPlayer Instance)
		{
			return Instance.GetVRCPlayerApi().IsUserInVR();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000A197 File Offset: 0x00008397
		internal static bool IsQuest(this global::VRC.Player player)
		{
			return player.GetAPIUser().IsOnMobile;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000A1A4 File Offset: 0x000083A4
		internal static APIUser LocalAPIUser
		{
			get
			{
				return APIUser.CurrentUser;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000A1AB File Offset: 0x000083AB
		internal static USpeaker LocalUSpeaker
		{
			get
			{
				return PlayerWrapper.LocalVRCPlayer.prop_USpeaker_0;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000A1B7 File Offset: 0x000083B7
		internal static VRCPlayerApi LocalVRCPlayerAPI
		{
			get
			{
				return PlayerWrapper.LocalVRCPlayer.field_Private_VRCPlayerApi_0;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000A1C3 File Offset: 0x000083C3
		internal static PlayerManager PManager
		{
			get
			{
				return PlayerManager.field_Private_Static_PlayerManager_0;
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000A1CA File Offset: 0x000083CA
		internal static int ActorID(this Photon.Realtime.Player player)
		{
			return player.field_Private_Int32_0;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000A1D2 File Offset: 0x000083D2
		internal static int ActorID(this global::VRC.Player player)
		{
			return player.GetPhotonPlayer().field_Private_Int32_0;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000A1E0 File Offset: 0x000083E0
		internal static bool IsAdmin(this global::VRC.Player player)
		{
			return player.prop_APIUser_0.hasModerationPowers || player.prop_APIUser_0.tags.Contains("admin_moderator") || player.prop_APIUser_0.hasSuperPowers || player.prop_APIUser_0.tags.Contains("admin_");
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000A236 File Offset: 0x00008436
		internal static void Respawn()
		{
			SpawnManager.field_Private_Static_SpawnManager_0.Method_Public_Void_Player_0(PlayerWrapper.LocalPlayer);
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000A248 File Offset: 0x00008448
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000A254 File Offset: 0x00008454
		internal static float RespawnHight
		{
			get
			{
				return PlayerWrapper.LocalVRCPlayer.field_Private_Single_2;
			}
			set
			{
				PlayerWrapper.LocalVRCPlayer.field_Private_Single_2 = value;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000A264 File Offset: 0x00008464
		internal static global::VRC.Player GetNetworkOwner(GameObject gameObject)
		{
			List<global::VRC.Player> all_player = Enumerable.ToList<global::VRC.Player>(PlayerWrapper.GetAllPlayers);
			foreach (global::VRC.Player player in all_player)
			{
				bool flag = player != null && player.prop_APIUser_0 != null;
				if (flag)
				{
					bool flag2 = Networking.GetOwner(gameObject) == player.field_Private_VRCPlayerApi_0;
					if (flag2)
					{
						return player;
					}
				}
			}
			return null;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000A2F4 File Offset: 0x000084F4
		internal static global::VRC.Player GetPlayerByUserID(string UserID)
		{
			return Enumerable.FirstOrDefault<global::VRC.Player>(Enumerable.Where<global::VRC.Player>(Enumerable.ToList<global::VRC.Player>(PlayerWrapper.GetAllPlayers), (global::VRC.Player p) => p.GetAPIUser().id == UserID));
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000A330 File Offset: 0x00008530
		internal static global::VRC.Player GetPlayerWithPlayerID(int playerID)
		{
			List<global::VRC.Player> list = Enumerable.ToList<global::VRC.Player>(PlayerWrapper.GetAllPlayers);
			foreach (global::VRC.Player player in list.ToArray())
			{
				bool flag = player.GetVRCPlayerApi().playerId == playerID;
				if (flag)
				{
					return player;
				}
			}
			return null;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000A384 File Offset: 0x00008584
		internal static void Tele2MousePos()
		{
			RaycastHit[] PosData = Physics.RaycastAll(new Ray(Camera.main.transform.position, Camera.main.transform.forward));
			bool flag = PosData.Length != 0;
			if (flag)
			{
				PlayerWrapper.LocalPlayer.transform.position = PosData[0].point;
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000A3E8 File Offset: 0x000085E8
		internal static GameObject Clone(this global::VRC.Player player, bool Mul = true)
		{
			GameObject Clone = Object.Instantiate<GameObject>(player._vrcplayer.field_Private_VRCAvatarManager_0.prop_GameObject_0, null, true);
			Animator component = Clone.GetComponent<Animator>();
			bool flag = component != null && component.isHuman;
			if (flag)
			{
				component.GetBoneTransform(HumanBodyBones.Head).localScale = Vector3.one;
			}
			Clone.name = "Cloned Avatar";
			component.enabled = false;
			Clone.GetComponent<VRIK>().enabled = false;
			Clone.transform.position = player.transform.position;
			Clone.transform.rotation = player.transform.rotation;
			List<SkinnedMeshRenderer> transforms = AviUtils.FindAllComponentsInGameObject<SkinnedMeshRenderer>(Clone, true, false, true);
			foreach (SkinnedMeshRenderer rend in transforms)
			{
				rend.gameObject.layer = 0;
			}
			if (Mul)
			{
				PlayerWrapper.AllClones.Add(Clone);
			}
			Object.DontDestroyOnLoad(Clone);
			return Clone;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000A504 File Offset: 0x00008704
		public static bool ChangeAvatar(string avatarID)
		{
			bool flag = avatarID.StartsWith("avtr_") && PlayerWrapper.LocalAPIUser.avatarId != avatarID;
			return flag && SomeAvatarFunction.Method_Public_Static_Boolean_ApiAvatar_String_ApiAvatar_Boolean_0(new ApiAvatar
			{
				id = avatarID
			}, avatarID, null, false);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000A554 File Offset: 0x00008754
		internal static bool IsInVR()
		{
			bool flag = PlayerWrapper.__isVR == -1;
			if (flag)
			{
				List<XRDisplaySubsystem> xrDisplaySubsystems = new List<XRDisplaySubsystem>();
				SubsystemManager.GetInstances<XRDisplaySubsystem>(xrDisplaySubsystems);
				foreach (XRDisplaySubsystem xrDisplay in xrDisplaySubsystems)
				{
					bool running = xrDisplay.running;
					if (running)
					{
						PlayerWrapper.__isVR = 1;
						return true;
					}
				}
				bool isLoggedIn = APIUser.IsLoggedIn;
				if (isLoggedIn)
				{
					PlayerWrapper.__isVR = 2;
				}
			}
			return PlayerWrapper.__isVR == 1;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000A5CD File Offset: 0x000087CD
		internal static bool IsLoading(this global::VRC.Player plr)
		{
			PipelineManager pipelineManager = plr.FindObject("ForwardDirection/Avatar");
			return ((pipelineManager != null) ? pipelineManager.blueprintId : null) == "avtr_749445a8-d9bf-4d48-b077-d18b776f66f7";
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000A5F0 File Offset: 0x000087F0
		internal static global::VRC.Player GetByDisplayName(string displayName)
		{
			return Enumerable.FirstOrDefault<global::VRC.Player>(PlayerWrapper.GetAllPlayers, (global::VRC.Player a) => a.DisplayName() == displayName);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000A620 File Offset: 0x00008820
		internal static global::VRC.Player GetByAvatrID(string AvId, bool singleOrNull = true)
		{
			return singleOrNull ? PlayerWrapper.GetAllPlayers.SingleOrNull(delegate(global::VRC.Player a)
			{
				ApiAvatar apiAvatar = a.prop_ApiAvatar_0;
				return ((apiAvatar != null) ? apiAvatar.id : null) == AvId;
			}) : Enumerable.FirstOrDefault<global::VRC.Player>(PlayerWrapper.GetAllPlayers, delegate(global::VRC.Player a)
			{
				ApiAvatar apiAvatar2 = a.prop_ApiAvatar_0;
				return ((apiAvatar2 != null) ? apiAvatar2.id : null) == AvId;
			});
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000A66B File Offset: 0x0000886B
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000A67C File Offset: 0x0000887C
		internal static Vector3 LocalPlayerPostion
		{
			get
			{
				return PlayerWrapper.LocalPlayer.transform.position;
			}
			set
			{
				PlayerWrapper.LocalPlayer.transform.position = value;
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000A68F File Offset: 0x0000888F
		internal static Vector3 Postion(this global::VRC.Player plr)
		{
			return plr.transform.localPosition;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000A69C File Offset: 0x0000889C
		internal static global::VRC.Player getOwner(this VRC_Pickup pickUp)
		{
			return PlayerWrapper.GetByActorID((((pickUp != null) ? pickUp.currentPlayer : null) != null) ? pickUp.currentPlayer.playerId : (-1));
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000A6BF File Offset: 0x000088BF
		internal static global::VRC.Player getOwnerOfGameObject(this GameObject gameObject)
		{
			return PlayerWrapper.GetByActorID(Networking.GetOwner(gameObject).playerId);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000A6D1 File Offset: 0x000088D1
		internal static bool IsFBT(this global::VRC.Player plr)
		{
			IkController field_Internal_IkController_ = plr.GetVRCAvatarManager().field_Internal_IkController_0;
			return field_Internal_IkController_ != null && field_Internal_IkController_.prop_EnumNPublicSealedvaInNoLiThFoFiSi8vUnique_0 == IkController.EnumNPublicSealedvaInNoLiThFoFiSi8vUnique.SixPoint;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000A6ED File Offset: 0x000088ED
		internal static Animator GetAnimator(this global::VRC.Player player)
		{
			return player._vrcplayer.GetAnimator();
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000A6FA File Offset: 0x000088FA
		internal static Transform GetBoneTransform(this global::VRC.Player player, HumanBodyBones bone)
		{
			return player.GetAnimator().GetBoneTransform(bone);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000A708 File Offset: 0x00008908
		internal static bool IsFriend(this VRCPlayer player)
		{
			List<string> friendIDs = PlayerWrapper.LocalAPIUser.friendIDs;
			string text;
			if (player == null)
			{
				text = null;
			}
			else
			{
				APIUser apiuser = player.GetAPIUser();
				text = ((apiuser != null) ? apiuser.id : null);
			}
			return friendIDs.Contains(text);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000A731 File Offset: 0x00008931
		internal static bool IsPrivate(this ApiAvatar Av)
		{
			return Av.releaseStatus.ToLower() != "public";
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000A748 File Offset: 0x00008948
		internal static bool IsPublic(this ApiAvatar Av)
		{
			return Av.releaseStatus.ToLower() == "public";
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000A75F File Offset: 0x0000895F
		internal static void SetVolume(this global::VRC.Player player, float vol)
		{
			player.GetUspeaker().field_Private_SimpleAudioGain_0.field_Public_Single_0 = vol;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000A773 File Offset: 0x00008973
		internal static List<VRCHandGrasper> GetHandGraspers(this VRCPlayer player)
		{
			return (player != null) ? player.field_Private_List_1_VRCHandGrasper_0 : null;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000A781 File Offset: 0x00008981
		internal static List<VRCHandGrasper> GetHandGraspers(this global::VRC.Player player)
		{
			List<VRCHandGrasper> list;
			if (player == null)
			{
				list = null;
			}
			else
			{
				VRCPlayer vrcplayer = player._vrcplayer;
				list = ((vrcplayer != null) ? vrcplayer.field_Private_List_1_VRCHandGrasper_0 : null);
			}
			return list;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000A79B File Offset: 0x0000899B
		internal static VRCHandGrasper GetHandGrasper(VRC_Pickup.PickupHand obj)
		{
			return (obj == VRC_Pickup.PickupHand.Left) ? PlayerWrapper.LocalPlayer.GetHandGraspers()[0] : PlayerWrapper.LocalPlayer.GetHandGraspers()[1];
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000A7C4 File Offset: 0x000089C4
		internal static bool IsHoldingItem(this global::VRC.Player plr)
		{
			Object @object;
			if (plr == null)
			{
				@object = null;
			}
			else
			{
				List<VRCHandGrasper> handGraspers = plr.GetHandGraspers();
				if (handGraspers == null)
				{
					@object = null;
				}
				else
				{
					VRCHandGrasper vrchandGrasper = handGraspers[0];
					@object = ((vrchandGrasper != null) ? vrchandGrasper.field_Internal_VRC_Pickup_0 : null);
				}
			}
			bool flag;
			if (!(@object != null))
			{
				Object object2;
				if (plr == null)
				{
					object2 = null;
				}
				else
				{
					List<VRCHandGrasper> handGraspers2 = plr.GetHandGraspers();
					if (handGraspers2 == null)
					{
						object2 = null;
					}
					else
					{
						VRCHandGrasper vrchandGrasper2 = handGraspers2[1];
						object2 = ((vrchandGrasper2 != null) ? vrchandGrasper2.field_Internal_VRC_Pickup_0 : null);
					}
				}
				flag = object2 != null;
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000A82C File Offset: 0x00008A2C
		internal static bool GetStreamerMode
		{
			get
			{
				return VRCInputManager.Method_Public_Static_Boolean_EnumNPublicSealedvaCoHeToTaThShPeVoViUnique_0(VRCInputManager.EnumNPublicSealedvaCoHeToTaThShPeVoViUnique.StreamerModeEnabled);
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000A835 File Offset: 0x00008A35
		internal static VRCAvatarManager GetVRCAvatarManager(this global::VRC.Player plr)
		{
			return plr._vrcplayer.field_Private_VRCAvatarManager_0;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000A842 File Offset: 0x00008A42
		internal static byte GetFrames(this global::VRC.Player player, bool raw)
		{
			return raw ? player._playerNet.prop_Byte_0 : 27;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000A856 File Offset: 0x00008A56
		internal static void Gain(this global::VRC.Player plr, float value)
		{
			plr.GetUspeaker().field_Private_SimpleAudioGain_0.field_Public_Single_0 = value;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000A86A File Offset: 0x00008A6A
		internal static float Gain(this global::VRC.Player plr)
		{
			return plr.GetUspeaker().field_Private_SimpleAudioGain_0.field_Public_Single_0;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000A87C File Offset: 0x00008A7C
		internal static global::VRC.Player GetRandomPlayer()
		{
			return Enumerable.ToArray<global::VRC.Player>(PlayerWrapper.GetAllPlayers).Random<global::VRC.Player>();
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000A890 File Offset: 0x00008A90
		internal static int GetOwnedItemCount(this global::VRC.Player plr)
		{
			int? us = ((plr != null) ? new int?(plr.ActorID()) : default(int?));
			return Enumerable.Count<VRC_Pickup>(Enumerable.Where<VRC_Pickup>(WorldWrapper.allBaseUdonItem, delegate(VRC_Pickup x)
			{
				bool flag;
				if (x == null)
				{
					flag = us == null;
				}
				else
				{
					VRCPlayerApi currentPlayer = x.currentPlayer;
					int? num = ((currentPlayer != null) ? new int?(currentPlayer.playerId) : default(int?));
					int? us2 = us;
					flag = (num.GetValueOrDefault() == us2.GetValueOrDefault()) & (num != null == (us2 != null));
				}
				return flag;
			}));
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000A8E4 File Offset: 0x00008AE4
		internal static string GetFramesColor(this global::VRC.Player player)
		{
			float fps = player.GetFrames();
			bool flag = fps >= 60f;
			string text2;
			if (flag)
			{
				string text = "<color=green>FPS</color> : ";
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(0, 1);
				defaultInterpolatedStringHandler.AppendFormatted<float>(fps);
				text2 = text + defaultInterpolatedStringHandler.ToStringAndClear().HexColor("green");
			}
			else
			{
				bool flag2 = fps >= 20f;
				if (flag2)
				{
					string text3 = "<color=yellow>FPS</color> : ";
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					defaultInterpolatedStringHandler..ctor(0, 1);
					defaultInterpolatedStringHandler.AppendFormatted<float>(fps);
					text2 = text3 + defaultInterpolatedStringHandler.ToStringAndClear().HexColor("yellow");
				}
				else
				{
					string text4 = "<color=red>FPS</color> : ";
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					defaultInterpolatedStringHandler..ctor(0, 1);
					defaultInterpolatedStringHandler.AppendFormatted<float>(fps);
					text2 = text4 + defaultInterpolatedStringHandler.ToStringAndClear().HexColor("red");
				}
			}
			return text2;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000A9AC File Offset: 0x00008BAC
		internal static string GetPingColor(this global::VRC.Player player)
		{
			short ping = player.GetPing();
			bool flag = ping >= 150;
			string text2;
			if (flag)
			{
				string text = "<color=red>Ping</color> : ";
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				defaultInterpolatedStringHandler..ctor(0, 1);
				defaultInterpolatedStringHandler.AppendFormatted<short>(ping);
				text2 = text + defaultInterpolatedStringHandler.ToStringAndClear().HexColor("red");
			}
			else
			{
				bool flag2 = ping >= 60;
				if (flag2)
				{
					string text3 = "<color=yellow>Ping</color> : ";
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					defaultInterpolatedStringHandler..ctor(0, 1);
					defaultInterpolatedStringHandler.AppendFormatted<short>(ping);
					text2 = text3 + defaultInterpolatedStringHandler.ToStringAndClear().HexColor("yellow");
				}
				else
				{
					string text4 = "<color=green>Ping</color> : ";
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
					defaultInterpolatedStringHandler..ctor(0, 1);
					defaultInterpolatedStringHandler.AppendFormatted<short>(ping);
					text2 = text4 + defaultInterpolatedStringHandler.ToStringAndClear().HexColor("green");
				}
			}
			return text2;
		}

		// Token: 0x04000110 RID: 272
		private static VRC_EventHandler handler;

		// Token: 0x04000111 RID: 273
		internal static List<string> ClientUsers = new List<string>();

		// Token: 0x04000112 RID: 274
		internal static List<string> CrashedUsers = new List<string>();

		// Token: 0x04000113 RID: 275
		internal static List<string> FakeCrashedUsers = new List<string>();

		// Token: 0x04000114 RID: 276
		internal static List<GameObject> AllClones = new List<GameObject>();

		// Token: 0x04000115 RID: 277
		private static int __isVR = -1;
	}
}
