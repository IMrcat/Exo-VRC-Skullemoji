using System;
using EXO.Core;
using UnityEngine;

namespace EXO.Functions.PlayerFunc
{
	// Token: 0x02000094 RID: 148
	internal class HeadLight : FunctionModule
	{
		// Token: 0x060005D9 RID: 1497 RVA: 0x0001F4B8 File Offset: 0x0001D6B8
		internal static void HeadFlashlights(bool state)
		{
			bool flag = RoomManager.field_Internal_Static_ApiWorld_0 != null;
			if (flag)
			{
				Transform boneTransform = VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Internal_Animator_0.GetBoneTransform(HumanBodyBones.Head);
				bool flag2 = boneTransform;
				if (flag2)
				{
					Light component = boneTransform.GetComponent<Light>();
					if (state)
					{
						Light light = boneTransform.gameObject.AddComponent<Light>();
						light.color = Color.white;
						light.type = LightType.Point;
						light.shadows = LightShadows.None;
						light.range = float.MaxValue;
						light.spotAngle = float.MaxValue;
						light.intensity = HeadLight.Intensity;
					}
					bool flag3 = !state;
					if (flag3)
					{
						bool flag4 = RoomManager.field_Internal_Static_ApiWorld_0 != null;
						if (flag4)
						{
							Object.Destroy(component);
						}
					}
				}
			}
		}

		// Token: 0x040002AB RID: 683
		public static float Range = 20f;

		// Token: 0x040002AC RID: 684
		public static float Intensity = 2f;
	}
}
