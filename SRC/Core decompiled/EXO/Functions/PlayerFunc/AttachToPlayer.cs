using System;
using EXO.Core;
using EXO.Menus.SelectMenus;
using EXO.Patches;
using EXO.Wrappers;
using UnityEngine;
using VRC;

namespace EXO.Functions.PlayerFunc
{
	// Token: 0x02000092 RID: 146
	internal class AttachToPlayer : FunctionModule
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0001F044 File Offset: 0x0001D244
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x0001F04B File Offset: 0x0001D24B
		internal static string attachLocation { get; set; } = "head";

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x0001F053 File Offset: 0x0001D253
		// (set) Token: 0x060005CB RID: 1483 RVA: 0x0001F05A File Offset: 0x0001D25A
		internal static bool isAttached { get; set; } = false;

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0001F062 File Offset: 0x0001D262
		// (set) Token: 0x060005CD RID: 1485 RVA: 0x0001F069 File Offset: 0x0001D269
		internal static Player target { get; set; }

		// Token: 0x060005CE RID: 1486 RVA: 0x0001F071 File Offset: 0x0001D271
		public override void OnPlayerWasInit()
		{
			JoinLeavePatch.OnPlayerJoin = (Action<Player>)Delegate.Combine(JoinLeavePatch.OnPlayerJoin, delegate(Player plr)
			{
				bool flag = plr == AttachToPlayer.target;
				if (flag)
				{
					AttachSelect.attachToggle.State = false;
				}
			});
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001F0A8 File Offset: 0x0001D2A8
		public override void OnUpdate()
		{
			bool flag = !AttachToPlayer.isAttached || AttachToPlayer.target == null || AttachToPlayer.target == AttachToPlayer.localPlayer;
			if (!flag)
			{
				bool flag2 = AttachToPlayer.localPlayer == null;
				if (flag2)
				{
					AttachToPlayer.localPlayer = PlayerWrapper.LocalPlayer;
				}
				string attachLocation = AttachToPlayer.attachLocation;
				string text = attachLocation;
				Transform transfrom;
				Vector3 offset;
				if (!(text == "hip"))
				{
					if (!(text == "rightHand"))
					{
						if (!(text == "leftHand"))
						{
							if (!(text == "rightFoot"))
							{
								if (!(text == "leftFoot"))
								{
									transfrom = AttachToPlayer.target._vrcplayer.GetAnimator().GetBoneTransform(HumanBodyBones.Head);
									offset = Quaternion.Euler(0f, AttachToPlayer.target.transform.eulerAngles.y, 0f) * new Vector3(AttachToPlayer.xOffset, AttachToPlayer.yOffset + 0.2f, AttachToPlayer.zOffset);
								}
								else
								{
									transfrom = AttachToPlayer.target._vrcplayer.GetAnimator().GetBoneTransform(HumanBodyBones.LeftFoot);
									offset = Quaternion.Euler(0f, AttachToPlayer.target.transform.eulerAngles.y, 0f) * new Vector3(AttachToPlayer.xOffset, AttachToPlayer.yOffset, AttachToPlayer.zOffset);
								}
							}
							else
							{
								transfrom = AttachToPlayer.target._vrcplayer.GetAnimator().GetBoneTransform(HumanBodyBones.RightFoot);
								offset = Quaternion.Euler(0f, AttachToPlayer.target.transform.eulerAngles.y, 0f) * new Vector3(AttachToPlayer.xOffset, AttachToPlayer.yOffset, AttachToPlayer.zOffset);
							}
						}
						else
						{
							transfrom = AttachToPlayer.target._vrcplayer.GetAnimator().GetBoneTransform(HumanBodyBones.LeftHand);
							offset = Quaternion.Euler(0f, AttachToPlayer.target.transform.eulerAngles.y, 0f) * new Vector3(AttachToPlayer.xOffset, AttachToPlayer.yOffset, AttachToPlayer.zOffset);
						}
					}
					else
					{
						transfrom = AttachToPlayer.target._vrcplayer.GetAnimator().GetBoneTransform(HumanBodyBones.RightHand);
						offset = Quaternion.Euler(0f, AttachToPlayer.target.transform.eulerAngles.y, 0f) * new Vector3(AttachToPlayer.xOffset, AttachToPlayer.yOffset, AttachToPlayer.zOffset);
					}
				}
				else
				{
					transfrom = AttachToPlayer.target._vrcplayer.GetAnimator().GetBoneTransform(HumanBodyBones.Hips);
					offset = Quaternion.Euler(0f, AttachToPlayer.target.transform.eulerAngles.y, 0f) * new Vector3(AttachToPlayer.xOffset, AttachToPlayer.yOffset, AttachToPlayer.zOffset + 0.3f);
				}
				AttachToPlayer.localPlayer.transform.position = transfrom.position + offset;
			}
		}

		// Token: 0x040002A5 RID: 677
		internal static Player localPlayer;

		// Token: 0x040002A6 RID: 678
		internal static float xOffset = 0f;

		// Token: 0x040002A7 RID: 679
		internal static float yOffset = 0f;

		// Token: 0x040002A8 RID: 680
		internal static float zOffset = 0f;
	}
}
