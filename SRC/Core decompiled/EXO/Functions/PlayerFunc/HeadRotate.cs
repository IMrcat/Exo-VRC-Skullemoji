using System;
using EXO.Core;
using EXO.Wrappers;
using UnityEngine;

namespace EXO.Functions.PlayerFunc
{
	// Token: 0x02000095 RID: 149
	internal class HeadRotate : FunctionModule
	{
		// Token: 0x060005DC RID: 1500 RVA: 0x0001F59C File Offset: 0x0001D79C
		public override void OnPlayerWasInit()
		{
			bool flag = Camera.main != null;
			if (flag)
			{
				Camera.main.transform.localRotation = Quaternion.identity;
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001F5D0 File Offset: 0x0001D7D0
		public override void OnPlayerWasDestroyed()
		{
			bool flag = Camera.main != null;
			if (flag)
			{
				Camera.main.transform.localRotation = Quaternion.identity;
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001F604 File Offset: 0x0001D804
		public override void OnUpdate()
		{
			bool flag = PlayerWrapper.IsInVR() || !Config.cfg.HeadFlipper;
			if (!flag)
			{
				Camera main = Camera.main;
				bool flag2 = Input.GetKey(KeyCode.LeftControl) && main;
				if (flag2)
				{
					float scrollDelta = Input.GetAxis("Mouse ScrollWheel") * 10f;
					bool flag3 = Mathf.Abs(scrollDelta) > 0.1f;
					if (flag3)
					{
						Vector3 rotationDirection = ((scrollDelta > 0f) ? (-Vector3.forward) : Vector3.forward);
						main.transform.Rotate(rotationDirection, Space.Self);
					}
					bool keyDown = Input.GetKeyDown(KeyCode.R);
					if (keyDown)
					{
						main.transform.localRotation = Quaternion.identity;
					}
					bool key = Input.GetKey(KeyCode.LeftShift);
					if (key)
					{
						main.transform.Rotate(Random.insideUnitSphere * 360f, Space.World);
					}
				}
				else
				{
					bool flag4 = main && !PlayerWrapper.IsInVR();
					if (flag4)
					{
						main.transform.localRotation = Quaternion.identity;
					}
				}
			}
		}
	}
}
