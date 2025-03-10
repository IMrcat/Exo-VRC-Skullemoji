using System;
using System.Collections.Generic;
using System.Linq;
using EXO.Core;
using EXO.Functions.InputManager;
using EXO.LogTools;
using EXO.Modules.Utilities;
using EXO.Wrappers;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem;
using UnityEngine;
using VRC;
using VRC.Animation;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.UI.Elements;
using VRCSDK2;

namespace EXO.Functions.PlayerFunc
{
	// Token: 0x02000096 RID: 150
	internal class MoveFunc : FunctionModule
	{
		// Token: 0x060005E0 RID: 1504 RVA: 0x0001F728 File Offset: 0x0001D928
		public override void OnPlayerWasInit()
		{
			bool flag = MoveFunc.noClipToggle;
			if (flag)
			{
				MoveFunc.NoClip();
			}
			MoveFunc.ForceJump();
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001F74B File Offset: 0x0001D94B
		public override void OnUpdate()
		{
			MoveFunc.ComfortFly();
			MoveFunc.Speed();
			MoveFunc.JetPack();
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001F760 File Offset: 0x0001D960
		internal static void ComfortFly()
		{
			try
			{
				Player player = PlayerWrapper.LocalPlayer;
				bool flag = !Config.cfg.FlyToggle || player == null;
				if (!flag)
				{
					bool flag2 = MoveFunc.motionState == null;
					if (flag2)
					{
						MoveFunc.motionState = player.GetComponent<VRCMotionState>();
					}
					bool flag3 = Config.cfg.FlyToggle || AttachToPlayer.isAttached;
					if (flag3)
					{
						Physics.gravity = Vector3.zero;
					}
					Transform playerTransform = player.transform;
					bool flag4 = VRCInputManager.Method_Public_Static_VRCInput_String_0("Jump").prop_Single_2 == 1f;
					if (flag4)
					{
						Vector3 velocity = Networking.LocalPlayer.GetVelocity();
						velocity.y = Networking.LocalPlayer.GetJumpImpulse();
						Networking.LocalPlayer.SetVelocity(velocity);
					}
					bool flag5 = PlayerWrapper.IsInVR();
					if (flag5)
					{
						playerTransform.position += playerTransform.forward * Time.deltaTime * UtilFunc.GetInputSingle("Vertical") * MoveFunc.flySpeed;
						playerTransform.position += playerTransform.right * Time.deltaTime * UtilFunc.GetInputSingle("Horizontal") * MoveFunc.flySpeed;
						playerTransform.position += new Vector3(0f, Time.deltaTime * UtilFunc.GetInputSingle("LookVertical") * MoveFunc.flySpeed, 0f);
					}
					else
					{
						float speed = (Input.GetKey(KeyCode.LeftShift) ? (MoveFunc.flySpeed * 2f) : MoveFunc.flySpeed);
						playerTransform.position += playerTransform.forward * Time.deltaTime * Input.GetAxis("Vertical") * speed;
						playerTransform.position += playerTransform.right * Time.deltaTime * Input.GetAxis("Horizontal") * speed;
						bool key = Input.GetKey(InputMap.GetKeyCode(Config.binds.FlyDownBtn));
						if (key)
						{
							playerTransform.position -= new Vector3(0f, Time.deltaTime * speed, 0f);
						}
						bool key2 = Input.GetKey(InputMap.GetKeyCode(Config.binds.FlyUpBtn));
						if (key2)
						{
							playerTransform.position += new Vector3(0f, Time.deltaTime * speed, 0f);
						}
					}
					VRCMotionState vrcmotionState = MoveFunc.motionState;
					if (vrcmotionState != null)
					{
						vrcmotionState.Reset();
					}
				}
			}
			catch (Exception e)
			{
				CLog.E("Fly Error", e);
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001FA40 File Offset: 0x0001DC40
		internal static void NoClip()
		{
			try
			{
				Player player = PlayerWrapper.LocalPlayer;
				bool flag = player == null;
				if (!flag)
				{
					Il2CppArrayBase<Collider> colliders = global::UnityEngine.Object.FindObjectsOfType<Collider>();
					Collider ownCollider = Enumerable.FirstOrDefault<Collider>(player.GetComponents<Collider>());
					using (IEnumerator<Collider> enumerator = colliders.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Collider collider = enumerator.Current;
							bool flag2 = Enumerable.Any<Type>(MoveFunc.DontAntiClip, (Type comp) => collider.GetComponent(comp) != null);
							if (!flag2)
							{
								bool flag3 = collider == ownCollider;
								if (!flag3)
								{
									bool flag4 = (!MoveFunc.noClipToggle || !collider.enabled) && (MoveFunc.noClipToggle || !MoveFunc.disabledColliders.Contains(collider.GetInstanceID()));
									if (!flag4)
									{
										collider.enabled = !MoveFunc.noClipToggle;
										bool flag5 = MoveFunc.noClipToggle;
										if (flag5)
										{
											MoveFunc.disabledColliders.Add(collider.GetInstanceID());
										}
									}
								}
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				CLog.E("No Clip Error", e);
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001FBB4 File Offset: 0x0001DDB4
		internal static void Speed()
		{
			try
			{
				Player player = PlayerWrapper.LocalPlayer;
				bool flag = !Config.cfg.SpeedToggle || player == null;
				if (!flag)
				{
					bool flag2 = MoveFunc.motionState == null;
					if (flag2)
					{
						MoveFunc.motionState = player.GetComponent<VRCMotionState>();
					}
					Transform playerTransform = player.transform;
					bool flag3 = PlayerWrapper.IsInVR();
					if (flag3)
					{
						playerTransform.position += playerTransform.forward * Time.deltaTime * UtilFunc.GetInputSingle("Vertical") * MoveFunc.speedVal * 2f;
						playerTransform.position += playerTransform.right * Time.deltaTime * UtilFunc.GetInputSingle("Horizontal") * MoveFunc.speedVal * 2f;
						bool flyToggle = Config.cfg.FlyToggle;
						if (flyToggle)
						{
							playerTransform.position += new Vector3(0f, Time.deltaTime * UtilFunc.GetInputSingle("LookVertical") * MoveFunc.speedVal * 2f);
						}
					}
					else
					{
						float speed = (Input.GetKey(KeyCode.LeftShift) ? (MoveFunc.speedVal * 2f) : MoveFunc.speedVal);
						playerTransform.position += playerTransform.forward * Time.deltaTime * Input.GetAxis("Vertical") * speed;
						playerTransform.position += playerTransform.right * Time.deltaTime * Input.GetAxis("Horizontal") * speed;
						bool flyToggle2 = Config.cfg.FlyToggle;
						if (flyToggle2)
						{
							bool key = Input.GetKey(InputMap.GetKeyCode(Config.binds.FlyDownBtn));
							if (key)
							{
								playerTransform.position -= new Vector3(0f, Time.deltaTime * speed, 0f);
							}
							bool key2 = Input.GetKey(InputMap.GetKeyCode(Config.binds.FlyUpBtn));
							if (key2)
							{
								playerTransform.position += new Vector3(0f, Time.deltaTime * speed, 0f);
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				CLog.E("Speed Error", e);
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001FE50 File Offset: 0x0001E050
		internal static void JetPack()
		{
			try
			{
				bool flag = !Config.cfg.JetPackToggle || PlayerWrapper.LocalPlayer == null;
				if (!flag)
				{
					bool flag2 = VRCInputManager.Method_Public_Static_VRCInput_String_0("Jump").prop_Single_2 == 1f;
					if (flag2)
					{
						Vector3 velocity = Networking.LocalPlayer.GetVelocity();
						velocity.y = Networking.LocalPlayer.GetJumpImpulse();
						Networking.LocalPlayer.SetVelocity(velocity);
					}
				}
			}
			catch (Exception e)
			{
				CLog.E("Jet Pack Error", e);
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001FEE8 File Offset: 0x0001E0E8
		internal static void ForceJump()
		{
			try
			{
				bool flag = Config.cfg.AutoJumpState && VRCPlayerApi.AllPlayers != null;
				if (flag)
				{
					Networking.LocalPlayer.SetJumpImpulse(3.5f);
				}
			}
			catch (Exception e)
			{
				CLog.E("Force Jump Error", e);
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001FF54 File Offset: 0x0001E154
		// Note: this type is marked as 'beforefieldinit'.
		static MoveFunc()
		{
			List<Type> list = new List<Type>();
			list.Add(Il2CppType.Of<PlayerSelector>());
			list.Add(Il2CppType.Of<global::VRC.SDKBase.VRC_Pickup>());
			list.Add(Il2CppType.Of<QuickMenu>());
			list.Add(Il2CppType.Of<VRC_Station>());
			list.Add(Il2CppType.Of<global::VRC.SDKBase.VRC_AvatarPedestal>());
			list.Add(Il2CppType.Of<global::VRC.SDKBase.VRC_UiShape>());
			list.Add(Il2CppType.Of<VRCUiShape>());
			MoveFunc.DontAntiClip = list;
		}

		// Token: 0x040002AD RID: 685
		internal static VRCMotionState motionState;

		// Token: 0x040002AE RID: 686
		private static readonly List<int> disabledColliders = new List<int>();

		// Token: 0x040002AF RID: 687
		internal static bool noClipToggle = false;

		// Token: 0x040002B0 RID: 688
		public static float flySpeed = 3f;

		// Token: 0x040002B1 RID: 689
		public static float fastFlySpeed = 10f;

		// Token: 0x040002B2 RID: 690
		public static float speedVal = 3f;

		// Token: 0x040002B3 RID: 691
		private static readonly List<Type> DontAntiClip;
	}
}
