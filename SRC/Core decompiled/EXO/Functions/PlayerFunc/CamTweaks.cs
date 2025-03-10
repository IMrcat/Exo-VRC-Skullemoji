using System;
using System.Collections;
using CoreRuntime.Manager;
using EXO.Core;
using EXO.LogTools;
using EXO.Wrappers;
using UnityEngine;

namespace EXO.Functions.PlayerFunc
{
	// Token: 0x02000093 RID: 147
	internal class CamTweaks : FunctionModule
	{
		// Token: 0x060005D2 RID: 1490 RVA: 0x0001F3CE File Offset: 0x0001D5CE
		public override void OnPlayerWasInit()
		{
			CoroutineManager.RunCoroutine(CamTweaks.WaitForPlayer());
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0001F3DB File Offset: 0x0001D5DB
		private static IEnumerator WaitForPlayer()
		{
			while (!PlayerWrapper.LocalPlayer || PlayerWrapper.LocalVRCPlayerAPI == null)
			{
				yield return new WaitForSeconds(2f);
			}
			CamTweaks.SetProperties(Camera.main);
			CamTweaks.previousHeight = PlayerWrapper.LocalVRCPlayerAPI.GetAvatarEyeHeightAsMeters();
			yield break;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001F3E3 File Offset: 0x0001D5E3
		public override void OnFixedUpdate()
		{
			CamTweaks.OnHeightChange();
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0001F3EB File Offset: 0x0001D5EB
		internal static void SetProperties(Camera cam)
		{
			cam.nearClipPlane = 0.001f;
			cam.farClipPlane = 1000f;
			cam.clearFlags = CameraClearFlags.Skybox;
			cam.cameraType = CameraType.Game;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001F418 File Offset: 0x0001D618
		internal static void OnHeightChange()
		{
			bool flag = Mathf.Abs(CamTweaks.currentHeight - CamTweaks.previousHeight) > Mathf.Epsilon && PlayerWrapper.LocalPlayer;
			if (flag)
			{
				CLog.L("Avatar height changed from " + CamTweaks.previousHeight.ToString() + " to " + CamTweaks.currentHeight.ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\PlayerFunc\\CamTweaks.cs", 42);
				CamTweaks.previousHeight = CamTweaks.currentHeight;
				ThirdPerson.ResetCameras(0f);
			}
		}

		// Token: 0x040002A9 RID: 681
		internal static float previousHeight = 1.6f;

		// Token: 0x040002AA RID: 682
		internal static float currentHeight = -1f;
	}
}
