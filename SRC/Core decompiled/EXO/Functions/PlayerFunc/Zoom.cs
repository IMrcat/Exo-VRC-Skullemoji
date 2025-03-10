using System;
using System.Collections;
using System.Runtime.CompilerServices;
using CoreRuntime.Manager;
using EXO.Core;
using EXO.Functions.InputManager;
using EXO.LogTools;
using EXO.Modules.Utilities;
using EXO.Wrappers;
using UnityEngine;
using UnityEngine.UI;
using WorldAPI;

namespace EXO.Functions.PlayerFunc
{
	// Token: 0x02000098 RID: 152
	internal class Zoom : FunctionModule
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x000208C5 File Offset: 0x0001EAC5
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x000208CC File Offset: 0x0001EACC
		internal static bool FovChanged { get; private set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x000208D4 File Offset: 0x0001EAD4
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x000208DB File Offset: 0x0001EADB
		internal static float Fov { get; private set; } = -1f;

		// Token: 0x060005F4 RID: 1524 RVA: 0x000208E4 File Offset: 0x0001EAE4
		public static void AfterMenuLoad()
		{
			Slider mainSliderComp = APIBase.MMM.FindObject("Container/MMParent/Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/Graphics/GraphicsQuality/Basic/VerticalLayoutGroup/FieldOfView/RightItemContainer/Slider");
			Zoom.Fov = VRCInputManager.Method_Public_Static_Single_EnumNPublicSealedvaCoHeToTaThShPeVoViUnique_0(VRCInputManager.EnumNPublicSealedvaCoHeToTaThShPeVoViUnique.DesktopFOV);
			mainSliderComp.onValueChanged.AddListener(delegate(float val)
			{
				Zoom.Fov = val;
			});
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(23, 1);
			defaultInterpolatedStringHandler.AppendLiteral("Using Current Set Fov: ");
			defaultInterpolatedStringHandler.AppendFormatted<float>(Zoom.Fov);
			CLog.L(defaultInterpolatedStringHandler.ToStringAndClear(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\PlayerFunc\\Zoom.cs", 32);
			CoroutineManager.RunCoroutine(Zoom.Loop());
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00020986 File Offset: 0x0001EB86
		private static IEnumerator Loop()
		{
			for (;;)
			{
				Zoom.Fov = VRCInputManager.Method_Public_Static_Single_EnumNPublicSealedvaCoHeToTaThShPeVoViUnique_0(VRCInputManager.EnumNPublicSealedvaCoHeToTaThShPeVoViUnique.DesktopFOV);
				yield return new WaitForSeconds(1f);
			}
			yield break;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00020990 File Offset: 0x0001EB90
		public override void OnUpdate()
		{
			bool flag = !Config.cfg.ZoomBind || !Camera.main || !PlayerWrapper.LocalPlayer;
			if (!flag)
			{
				bool flag2 = Zoom.zoomFov <= 1f;
				if (flag2)
				{
					Zoom.zoomFov = 1f;
				}
				bool flag3 = Camera.main.fieldOfView <= 1f;
				if (flag3)
				{
					Camera.main.fieldOfView = 1f;
				}
				bool flag4 = Zoom.zoomFov >= 140f;
				if (flag4)
				{
					Zoom.zoomFov = 140f;
				}
				bool flag5 = Camera.main.fieldOfView >= 140f;
				if (flag5)
				{
					Camera.main.fieldOfView = 140f;
				}
				bool key = Input.GetKey(InputMap.GetKeyCode(Config.binds.CameraResetBind));
				if (key)
				{
					Zoom.zoomFov = 20f;
				}
				try
				{
					bool flag6 = InputMap.IsPressed(InputMap.GetKeyCode(Config.binds.ZoomBind1), InputMap.GetKeyCode(Config.binds.ZoomBind2)) && Config.cfg.ZoomBind;
					if (flag6)
					{
						float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
						bool flag7 = mouseWheel > 0f && Zoom.zoomFov > 10f;
						if (flag7)
						{
							Zoom.zoomFov -= 5f;
						}
						else
						{
							bool flag8 = mouseWheel < 0f && Zoom.zoomFov < 140f && Zoom.zoomFov >= 10f;
							if (flag8)
							{
								Zoom.zoomFov += 5f;
							}
							else
							{
								bool flag9 = mouseWheel > 0f && Zoom.zoomFov <= 10f && Zoom.zoomFov > 1f;
								if (flag9)
								{
									Zoom.zoomFov -= 1f;
								}
								else
								{
									bool flag10 = mouseWheel < 0f && Zoom.zoomFov <= 10f;
									if (flag10)
									{
										Zoom.zoomFov += 1f;
									}
								}
							}
						}
						Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, Zoom.zoomFov, Time.deltaTime * 10f);
						VRCPlayer.field_Internal_Static_VRCPlayer_0.GetComponent<LocomotionInputController>().field_Protected_NeckMouseRotator_0.field_Public_Single_1 = 0.05f;
					}
					else
					{
						bool flag11 = !InputMap.IsPressed(InputMap.GetKeyCode(Config.binds.ZoomBind1), InputMap.GetKeyCode(Config.binds.ZoomBind2));
						if (flag11)
						{
							Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, Zoom.Fov, Time.deltaTime * 10f);
							VRCPlayer.field_Internal_Static_VRCPlayer_0.GetComponent<LocomotionInputController>().field_Protected_NeckMouseRotator_0.field_Public_Single_1 = 0f;
						}
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x040002BC RID: 700
		internal static float zoomFov = 20f;
	}
}
