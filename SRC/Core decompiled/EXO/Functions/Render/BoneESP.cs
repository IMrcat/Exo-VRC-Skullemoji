using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EXO.Core;
using EXO.LogTools;
using EXO.Menus.SelectMenus;
using EXO.Patches;
using EXO.Wrappers;
using UnityEngine;
using VRC;

namespace EXO.Functions.Render
{
	// Token: 0x0200008C RID: 140
	internal class BoneESP : FunctionModule
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x0001D50C File Offset: 0x0001B70C
		public override void OnUpdate()
		{
			bool flag = BoneESP.render;
			if (flag)
			{
				BoneESP.UpdateRenders();
			}
			bool boneSelectESP = ListenerSelect.boneSelectESP;
			if (boneSelectESP)
			{
				BoneESP.UpdateListener();
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001D538 File Offset: 0x0001B738
		internal static void DisableRenders()
		{
			foreach (Player player in PlayerWrapper.GetAllPlayers)
			{
				bool flag = player.gameObject.GetComponent<LineRenderer>();
				if (flag)
				{
					player.gameObject.GetComponent<LineRenderer>().enabled = false;
				}
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001D588 File Offset: 0x0001B788
		internal static void UpdateListener()
		{
			Player localPlayer = PlayerWrapper.LocalPlayer;
			bool flag = ((localPlayer != null) ? localPlayer.gameObject : null) == null || !EventPatch.Ready;
			if (!flag)
			{
				bool flag2 = BoneESP.listenerTarget != PlayerWrapper.LocalPlayer && !BoneESP.listenerTarget.IsLoading();
				if (flag2)
				{
					try
					{
						BoneESP.RenderPlayer(BoneESP.listenerTarget);
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0001D60C File Offset: 0x0001B80C
		internal static void UpdateRenders()
		{
			Player localPlayer = PlayerWrapper.LocalPlayer;
			bool flag = ((localPlayer != null) ? localPlayer.gameObject : null) == null || !EventPatch.Ready;
			if (!flag)
			{
				foreach (Player player in PlayerWrapper.GetAllPlayers)
				{
					bool flag2 = player != PlayerWrapper.LocalPlayer && !player.IsLoading() && player.GetAPIUser() != null;
					if (flag2)
					{
						try
						{
							BoneESP.RenderPlayer(player);
						}
						catch
						{
						}
					}
				}
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001D6A4 File Offset: 0x0001B8A4
		internal static void RenderPlayer(Player player)
		{
			bool flag;
			if (player == null)
			{
				flag = false;
			}
			else
			{
				Animator animator = player.GetAnimator();
				bool? flag2 = ((animator != null) ? new bool?(animator.isHuman) : default(bool?));
				bool flag3 = false;
				flag = (flag2.GetValueOrDefault() == flag3) & (flag2 != null);
			}
			bool flag4 = flag;
			if (!flag4)
			{
				LineRenderer lineComp = player.gameObject.GetComponent<LineRenderer>();
				bool flag5 = lineComp == null;
				if (flag5)
				{
					lineComp = player.gameObject.AddComponent<LineRenderer>();
					lineComp.positionCount = BoneESP.ToReneder.Length;
					lineComp.numPositions = BoneESP.ToReneder.Length;
					lineComp.alignment = LineAlignment.View;
					lineComp.material = new Material(Shader.Find("GUI/Text Shader"));
					bool trustColors = Config.cfg.TrustColors;
					if (trustColors)
					{
						lineComp.material.color = player.GetTrustColor();
					}
					else
					{
						bool flag6 = player.IsFriend();
						if (flag6)
						{
							lineComp.material.color = Color.yellow;
						}
						else
						{
							lineComp.material.color = new Color(1f, 0f, 0f, 1f);
						}
					}
				}
				lineComp.startWidth = player.field_Private_VRCPlayerApi_0.GetAvatarEyeHeightAsMeters() * 0.005f;
				lineComp.enabled = true;
				bool HasUpperChest = player.GetBoneTransform(HumanBodyBones.UpperChest) != null;
				BoneESP.postions.Clear();
				for (int i = 0; i < BoneESP.ToReneder.Length; i++)
				{
					try
					{
						bool flag7 = !HasUpperChest && BoneESP.ToReneder[i] == HumanBodyBones.UpperChest;
						if (flag7)
						{
							BoneESP.postions.Add(player.GetBoneTransform(HumanBodyBones.Chest).position);
						}
						else
						{
							BoneESP.postions.Add(player.GetBoneTransform(BoneESP.ToReneder[i]).position);
						}
					}
					catch
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
						defaultInterpolatedStringHandler..ctor(4, 1);
						defaultInterpolatedStringHandler.AppendLiteral("Num ");
						defaultInterpolatedStringHandler.AppendFormatted<int>(i);
						CLog.L(defaultInterpolatedStringHandler.ToStringAndClear(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\Render\\BoneESP.cs", 114);
					}
				}
				lineComp.SetPositions(BoneESP.postions.ToArray());
			}
		}

		// Token: 0x04000283 RID: 643
		internal static bool render = false;

		// Token: 0x04000284 RID: 644
		private static readonly List<Vector3> postions = new List<Vector3>();

		// Token: 0x04000285 RID: 645
		internal static Player listenerTarget;

		// Token: 0x04000286 RID: 646
		private static readonly HumanBodyBones[] ToReneder = new HumanBodyBones[]
		{
			HumanBodyBones.Head,
			HumanBodyBones.Neck,
			HumanBodyBones.UpperChest,
			HumanBodyBones.RightShoulder,
			HumanBodyBones.RightUpperArm,
			HumanBodyBones.RightLowerArm,
			HumanBodyBones.RightHand,
			HumanBodyBones.UpperChest,
			HumanBodyBones.LeftShoulder,
			HumanBodyBones.LeftUpperArm,
			HumanBodyBones.LeftLowerArm,
			HumanBodyBones.LeftHand,
			HumanBodyBones.UpperChest,
			HumanBodyBones.Chest,
			HumanBodyBones.Hips,
			HumanBodyBones.RightUpperLeg,
			HumanBodyBones.RightLowerLeg,
			HumanBodyBones.RightFoot,
			HumanBodyBones.RightLowerLeg,
			HumanBodyBones.RightUpperLeg,
			HumanBodyBones.Hips,
			HumanBodyBones.LeftUpperLeg,
			HumanBodyBones.LeftLowerLeg,
			HumanBodyBones.LeftFoot
		};
	}
}
