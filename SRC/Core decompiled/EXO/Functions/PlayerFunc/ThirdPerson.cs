using System;
using EXO.Core;
using EXO.Functions.InputManager;
using EXO.Functions.Managers;
using EXO.LogTools;
using EXO.Menus.SubMenus.Visuals;
using EXO.Modules.Utilities;
using EXO.Wrappers;
using UnityEngine;

namespace EXO.Functions.PlayerFunc
{
	// Token: 0x02000097 RID: 151
	internal class ThirdPerson : FunctionModule
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x0001FFF0 File Offset: 0x0001E1F0
		public override void OnUpdate()
		{
			bool flag = InputMap.WasPressed(InputMap.GetKeyCode(Config.binds.ThirdPersonBind1), InputMap.GetKeyCode(Config.binds.ThirdPersonBind2)) && Config.cfg.ThirdPersonBind && !ThirdPerson.isUpdating && PlayerInit.playerInit;
			if (flag)
			{
				ThirdPerson.On3rdPersonStart();
			}
			bool flag2 = ThirdPerson.isUpdating && PlayerInit.playerInit;
			if (flag2)
			{
				ThirdPerson.Camera3rdUpdate();
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00020064 File Offset: 0x0001E264
		public static void On3rdPersonStart()
		{
			ThirdPerson.mainCamera = Camera.main.gameObject;
			bool flag = ThirdPerson.mainCamera != null;
			if (flag)
			{
				CamTweaks.previousHeight = PlayerWrapper.LocalVRCPlayerAPI.GetAvatarEyeHeightAsMeters();
				ThirdPerson.backCamera = GameObject.CreatePrimitive(PrimitiveType.Cube);
				Object.Destroy(ThirdPerson.backCamera.GetComponent<MeshRenderer>());
				ThirdPerson.frontCamera = GameObject.CreatePrimitive(PrimitiveType.Cube);
				Object.Destroy(ThirdPerson.frontCamera.GetComponent<MeshRenderer>());
				ThirdPerson.backCamera.transform.localScale = ThirdPerson.mainCamera.transform.localScale;
				Rigidbody rigidbodyB = ThirdPerson.backCamera.AddComponent<Rigidbody>();
				rigidbodyB.isKinematic = true;
				rigidbodyB.useGravity = false;
				bool flag2 = ThirdPerson.backCamera.GetComponent<Collider>();
				if (flag2)
				{
					ThirdPerson.backCamera.GetComponent<Collider>().enabled = false;
				}
				ThirdPerson.backCamera.GetComponent<Renderer>().enabled = false;
				ThirdPerson.backCamera.AddComponent<Camera>();
				ThirdPerson.backCamera.transform.parent = ThirdPerson.mainCamera.transform;
				ThirdPerson.backCamera.transform.rotation = ThirdPerson.mainCamera.transform.rotation;
				ThirdPerson.backCamera.GetComponent<Camera>().fieldOfView = ThirdPerson.fov;
				ThirdPerson.mainCamera.GetComponent<Camera>().enabled = false;
				ThirdPerson.frontCamera.transform.localScale = ThirdPerson.mainCamera.transform.localScale;
				Rigidbody rigidbodyF = ThirdPerson.frontCamera.AddComponent<Rigidbody>();
				rigidbodyF.isKinematic = true;
				rigidbodyF.useGravity = false;
				bool flag3 = ThirdPerson.frontCamera.GetComponent<Collider>();
				if (flag3)
				{
					ThirdPerson.frontCamera.GetComponent<Collider>().enabled = false;
				}
				ThirdPerson.frontCamera.GetComponent<Renderer>().enabled = false;
				ThirdPerson.frontCamera.AddComponent<Camera>();
				ThirdPerson.frontCamera.transform.parent = ThirdPerson.mainCamera.transform;
				ThirdPerson.frontCamera.transform.rotation = ThirdPerson.mainCamera.transform.rotation;
				ThirdPerson.frontCamera.transform.Rotate(0f, 180f, 0f);
				ThirdPerson.frontCamera.GetComponent<Camera>().fieldOfView = ThirdPerson.fov;
				ThirdPerson.backCamera.GetComponent<Camera>().enabled = false;
				ThirdPerson.frontCamera.GetComponent<Camera>().enabled = false;
				ThirdPerson.mainCamera.GetComponent<Camera>().enabled = true;
				UtilFunc.Delay(0.01f, delegate
				{
					ThirdPerson.ResetCameras(CamMenu.offsetValue);
				});
				ThirdPerson.isUpdating = true;
			}
			else
			{
				CLog.L("[Third Person] Camera was Null!!", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\PlayerFunc\\ThirdPerson.cs", 84);
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00020314 File Offset: 0x0001E514
		public static void Switch()
		{
			Camera mainCam = ThirdPerson.mainCamera.GetComponent<Camera>();
			Camera backCam = ThirdPerson.backCamera.GetComponent<Camera>();
			Camera frontCam = ThirdPerson.frontCamera.GetComponent<Camera>();
			CamTweaks.SetProperties(backCam);
			CamTweaks.SetProperties(frontCam);
			switch (ThirdPerson.lastCamera)
			{
			case 0:
				ThirdPerson.lastCamera = 1;
				mainCam.enabled = false;
				backCam.enabled = true;
				frontCam.enabled = false;
				ThirdPerson.inThird = true;
				break;
			case 1:
				ThirdPerson.lastCamera = 2;
				mainCam.enabled = false;
				backCam.enabled = false;
				frontCam.enabled = true;
				ThirdPerson.inThird = true;
				break;
			case 2:
				ThirdPerson.lastCamera = 0;
				mainCam.enabled = true;
				backCam.enabled = false;
				frontCam.enabled = false;
				ThirdPerson.inThird = false;
				break;
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000203E4 File Offset: 0x0001E5E4
		public static void Camera3rdUpdate()
		{
			bool flag = !Config.cfg.ThirdPersonBind || PlayerWrapper.LocalVRCPlayer == null || PlayerWrapper.LocalVRCPlayerAPI == null;
			if (!flag)
			{
				try
				{
					CamTweaks.currentHeight = PlayerWrapper.LocalVRCPlayerAPI.GetAvatarEyeHeightAsMeters();
					ThirdPerson.frontCamera.GetComponent<Camera>().fieldOfView = ThirdPerson.fov;
					ThirdPerson.backCamera.GetComponent<Camera>().fieldOfView = ThirdPerson.fov;
					bool flag2 = ThirdPerson.backCamera != null && ThirdPerson.frontCamera != null && ThirdPerson.inThird && ThirdPerson.lastCamera != 0;
					if (flag2)
					{
						bool keyDown = Input.GetKeyDown(InputMap.GetKeyCode(Config.binds.CameraResetBind));
						if (keyDown)
						{
							ThirdPerson.ResetCameras(CamMenu.offsetValue);
						}
						float axis = Input.GetAxis("Mouse ScrollWheel");
						float distance = Vector3.Distance(ThirdPerson.frontCamera.transform.position, ThirdPerson.backCamera.transform.position);
						bool flag3 = axis > 0f && distance > ThirdPerson.proxThresh * CamTweaks.currentHeight;
						if (flag3)
						{
							ThirdPerson.backCamera.transform.position += ThirdPerson.backCamera.transform.forward * 0.1f * CamTweaks.currentHeight;
							ThirdPerson.frontCamera.transform.position -= ThirdPerson.backCamera.transform.forward * 0.1f * CamTweaks.currentHeight;
						}
						else
						{
							bool flag4 = axis < 0f;
							if (flag4)
							{
								ThirdPerson.backCamera.transform.position -= ThirdPerson.backCamera.transform.forward * 0.1f * CamTweaks.currentHeight;
								ThirdPerson.frontCamera.transform.position += ThirdPerson.backCamera.transform.forward * 0.1f * CamTweaks.currentHeight;
							}
						}
					}
					bool flag5 = InputMap.WasPressed(InputMap.GetKeyCode(Config.binds.ThirdPersonBind1), InputMap.GetKeyCode(Config.binds.ThirdPersonBind2)) && Config.cfg.ThirdPersonBind;
					if (flag5)
					{
						ThirdPerson.Switch();
					}
				}
				catch (Exception e)
				{
					string text = "Error\n";
					Exception ex = e;
					CLog.L(text + ((ex != null) ? ex.ToString() : null), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\PlayerFunc\\ThirdPerson.cs", 158);
				}
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000206A0 File Offset: 0x0001E8A0
		internal static void ResetCameras(float offset = 0f)
		{
			bool flag = !ThirdPerson.inThird;
			if (!flag)
			{
				Vector3 cameraPos = ThirdPerson.mainCamera.transform.position;
				Vector3 centerOfHead;
				try
				{
					centerOfHead = PlayerWrapper.LocalVRCPlayer.GetAnimator().GetBoneTransform(HumanBodyBones.Head).position;
				}
				catch
				{
					centerOfHead = ThirdPerson.mainCamera.transform.position;
				}
				float distance = Vector3.Distance(centerOfHead, cameraPos);
				float height = ((CamTweaks.currentHeight == -1f) ? CamTweaks.previousHeight : CamTweaks.currentHeight);
				ThirdPerson.backCamera.transform.position = cameraPos;
				Vector3 newPos = ThirdPerson.backCamera.transform.position;
				newPos -= ThirdPerson.mainCamera.transform.forward * distance;
				newPos += ThirdPerson.mainCamera.transform.right * offset * CamTweaks.currentHeight;
				ThirdPerson.backCamera.transform.position = newPos;
				ThirdPerson.backCamera.transform.position -= ThirdPerson.backCamera.transform.forward * 1.5f * height;
				ThirdPerson.frontCamera.transform.position = cameraPos;
				newPos = ThirdPerson.frontCamera.transform.position;
				newPos -= ThirdPerson.mainCamera.transform.forward * distance;
				newPos += ThirdPerson.mainCamera.transform.right * offset * CamTweaks.currentHeight;
				ThirdPerson.frontCamera.transform.position = newPos;
				ThirdPerson.frontCamera.transform.position += -ThirdPerson.frontCamera.transform.forward * 1.5f * height;
			}
		}

		// Token: 0x040002B4 RID: 692
		public static bool inThird = false;

		// Token: 0x040002B5 RID: 693
		private static GameObject mainCamera;

		// Token: 0x040002B6 RID: 694
		private static GameObject backCamera;

		// Token: 0x040002B7 RID: 695
		private static GameObject frontCamera;

		// Token: 0x040002B8 RID: 696
		private static int lastCamera;

		// Token: 0x040002B9 RID: 697
		internal static bool isUpdating;

		// Token: 0x040002BA RID: 698
		internal static float fov = 80f;

		// Token: 0x040002BB RID: 699
		public static float proxThresh = 0.8f;
	}
}
