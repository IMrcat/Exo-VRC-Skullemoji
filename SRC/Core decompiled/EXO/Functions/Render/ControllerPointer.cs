using System;
using EXO.Core;
using EXO.Functions.PlayerFunc;
using EXO.LogTools;
using EXO.Modules.Utilities;
using EXO.Wrappers;
using UnityEngine;

namespace EXO.Functions.Render
{
	// Token: 0x0200008D RID: 141
	internal class ControllerPointer : FunctionModule
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0001D90C File Offset: 0x0001BB0C
		internal static GameObject ControllerRight
		{
			get
			{
				bool flag = ControllerPointer.catched_Right == null;
				if (flag)
				{
					ControllerPointer.catched_Right = GameObject.Find("TrackingVolume").FindObject("TrackingSteam2(Clone)/SteamCamera/[CameraRig]/Controller (right)/PointerOrigin").gameObject;
				}
				return ControllerPointer.catched_Right;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0001D950 File Offset: 0x0001BB50
		internal static GameObject ControllerLeft
		{
			get
			{
				bool flag = ControllerPointer.catched_Left == null;
				if (flag)
				{
					ControllerPointer.catched_Left = GameObject.Find("TrackingVolume").FindObject("TrackingSteam2(Clone)/SteamCamera/[CameraRig]/Controller (left)/PointerOrigin").gameObject;
				}
				return ControllerPointer.catched_Left;
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001D994 File Offset: 0x0001BB94
		public override void OnUpdate()
		{
			bool flag = !PlayerWrapper.LocalPlayer || !MenuCore.quickMenu;
			if (!flag)
			{
				bool flag2 = PlayerWrapper.IsInVR();
				if (flag2)
				{
					try
					{
						bool flag3 = ControllerPointer.rightLine != null;
						if (flag3)
						{
							ControllerPointer.PrepObj(ref ControllerPointer.rightLine, ControllerPointer.ControllerRight);
						}
						ControllerPointer.UpdateLineRenderer(ControllerPointer.ControllerRight, ref ControllerPointer.rightLine, ref ControllerPointer.rayRight);
						bool flag4 = ControllerPointer.leftLine != null;
						if (flag4)
						{
							ControllerPointer.PrepObj(ref ControllerPointer.leftLine, ControllerPointer.ControllerLeft);
						}
						ControllerPointer.UpdateLineRenderer(ControllerPointer.ControllerLeft, ref ControllerPointer.leftLine, ref ControllerPointer.rayLeft);
						bool flag5 = ControllerPointer.rightLine != null;
						if (flag5)
						{
							ControllerPointer.rightLine.enabled = ControllerPointer.isRightLineVisible;
						}
						bool flag6 = ControllerPointer.leftLine != null;
						if (flag6)
						{
							ControllerPointer.leftLine.enabled = ControllerPointer.isLeftLineVisible;
						}
					}
					catch (Exception e)
					{
						string text = "Line Render Error\n";
						Exception ex = e;
						CLog.D(text + ((ex != null) ? ex.ToString() : null), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\Render\\ControllerPointer.cs", 79);
					}
				}
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001DAC0 File Offset: 0x0001BCC0
		private static void UpdateLineRenderer(GameObject controller, ref LineRenderer line, ref Ray ray)
		{
			line.SetPosition(1, controller.transform.position);
			ray = new Ray(controller.transform.position, controller.transform.forward);
			RaycastHit[] hits = Physics.RaycastAll(ray, float.MaxValue);
			RaycastHit? closestHit = default(RaycastHit?);
			foreach (RaycastHit hit in hits)
			{
				bool flag = hit.transform != null && !hit.transform.name.ToLower().Contains("mirror");
				if (flag)
				{
					bool flag2 = closestHit == null || hit.distance < closestHit.Value.distance;
					if (flag2)
					{
						closestHit = new RaycastHit?(hit);
					}
				}
			}
			bool flag3 = closestHit != null;
			if (flag3)
			{
				line.SetPosition(0, closestHit.Value.point);
			}
			else
			{
				line.SetPosition(0, ray.origin + ray.direction * 200f);
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001DBF4 File Offset: 0x0001BDF4
		private static void PrepObj(ref LineRenderer line, GameObject parent)
		{
			GameObject temp = new GameObject(parent.name + " Pointer");
			temp.transform.SetParent(parent.transform);
			line = temp.AddComponent<LineRenderer>();
			line.startWidth = 0.002f * CamTweaks.currentHeight;
			line.endWidth = 0.002f * CamTweaks.currentHeight;
			line.alignment = LineAlignment.View;
			line.material = new Material(Shader.Find("GUI/Text Shader"));
			line.material.color = new Color(1f, 0f, 0f, 1f);
			line.enabled = true;
		}

		// Token: 0x04000287 RID: 647
		private static GameObject catched_Right;

		// Token: 0x04000288 RID: 648
		private static LineRenderer rightLine;

		// Token: 0x04000289 RID: 649
		private static GameObject catched_Left;

		// Token: 0x0400028A RID: 650
		private static LineRenderer leftLine;

		// Token: 0x0400028B RID: 651
		internal static Ray rayRight;

		// Token: 0x0400028C RID: 652
		internal static Ray rayLeft;

		// Token: 0x0400028D RID: 653
		internal static bool isRightLineVisible;

		// Token: 0x0400028E RID: 654
		internal static bool isLeftLineVisible;
	}
}
