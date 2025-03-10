using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CoreRuntime.Manager;
using EXO.Core;
using EXO.Wrappers;
using UnityEngine;
using VRC;
using VRC.SDKBase;

namespace EXO.Functions.Item
{
	// Token: 0x020000A6 RID: 166
	internal class ItemOrbit : FunctionModule
	{
		// Token: 0x0600063F RID: 1599 RVA: 0x00022EC5 File Offset: 0x000210C5
		public override void OnPlayerWasInit()
		{
			CoroutineManager.RunCoroutine(ItemOrbit.OrbitItems(null));
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00022ED3 File Offset: 0x000210D3
		public override void OnPlayerWasDestroyed()
		{
			CoroutineManager.StopCoroutine(ItemOrbit.OrbitItems(null));
			ItemOrbit.hasStarted = false;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00022EE8 File Offset: 0x000210E8
		internal static IEnumerator OrbitItems(List<VRC_Pickup> customPickups = null)
		{
			List<VRC_Pickup> pickups = customPickups ?? Enumerable.ToList<VRC_Pickup>(WorldWrapper.allBaseUdonItem);
			int pickupsLength = pickups.Count;
			bool flag = pickupsLength > 100;
			if (flag)
			{
				float baseOrbitRadius = ItemOrbit.orbitRadius;
				ItemOrbit.orbitRadius = baseOrbitRadius * (1f + (float)(pickupsLength - 100) / 100f * 4f);
			}
			bool flag2 = ItemOrbit.hasStarted;
			if (flag2)
			{
				yield return null;
			}
			ItemOrbit.hasStarted = true;
			for (;;)
			{
				bool flag3 = ItemOrbit.itemType != 0 || ItemOrbit.target == null;
				if (flag3)
				{
					yield return new WaitForSeconds(0.5f);
				}
				else
				{
					float height = ItemOrbit.target.GetVRCPlayerApi().GetAvatarEyeHeightAsMeters() / 2f;
					int num;
					for (int i = 0; i < pickupsLength; i = num + 1)
					{
						VRC_Pickup pickup = pickups[i];
						bool flag4 = !pickup.gameObject.activeInHierarchy;
						if (!flag4)
						{
							bool flag5 = Networking.GetOwner(pickup.gameObject).playerId != PlayerWrapper.LocalVRCPlayerAPI.playerId;
							if (flag5)
							{
								Networking.SetOwner(PlayerWrapper.LocalVRCPlayerAPI, pickup.gameObject);
								bool flag6 = pickupsLength > 50;
								if (flag6)
								{
									yield return null;
								}
							}
							string text = ItemOrbit.orbitShape;
							string text2 = text;
							string text3 = text2;
							Vector3 newPosition;
							if (!(text3 == "head"))
							{
								if (!(text3 == "bubble"))
								{
									if (!(text3 == "ellipse"))
									{
										if (!(text3 == "spiral"))
										{
											if (!(text3 == "figure8"))
											{
												newPosition = ItemOrbit.CalcOrbitPos(i, ItemOrbit.target.gameObject.transform.position, height);
											}
											else
											{
												newPosition = ItemOrbit.CalcFigure8OrbitPos(i, ItemOrbit.target.gameObject.transform.position, height);
											}
										}
										else
										{
											newPosition = ItemOrbit.CalcSpiralOrbitPos(i, ItemOrbit.target.gameObject.transform.position, height);
										}
									}
									else
									{
										newPosition = ItemOrbit.CalcEllipseOrbitPos(i, ItemOrbit.target.gameObject.transform.position, height);
									}
								}
								else
								{
									newPosition = ItemOrbit.CalcBubbleOrbitPos(i, ItemOrbit.target.gameObject.transform.position, height);
								}
							}
							else
							{
								newPosition = ItemOrbit.target._vrcplayer.GetAnimator().GetBoneTransform(HumanBodyBones.Head).position;
							}
							text2 = null;
							pickup.transform.position = newPosition;
							pickup = null;
							newPosition = default(Vector3);
						}
						num = i;
					}
					yield return null;
				}
			}
			yield break;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00022EF8 File Offset: 0x000210F8
		private static Vector3 CalcOrbitPos(int index, Vector3 targetPosition, float height)
		{
			float timeOffset = ItemOrbit.orbitFrequency * (float)index;
			float x = Mathf.Sin(Time.time * ItemOrbit.orbitFrequency + timeOffset) * ItemOrbit.orbitRadius;
			float z = Mathf.Cos(Time.time * ItemOrbit.orbitFrequency + timeOffset) * ItemOrbit.orbitRadius;
			return targetPosition + new Vector3(x, height + ItemOrbit.orbitHeight, z);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00022F5C File Offset: 0x0002115C
		private static Vector3 CalcBubbleOrbitPos(int index, Vector3 targetPosition, float height)
		{
			float timeOffset = ItemOrbit.orbitFrequency * (float)index;
			float angle = Time.time * ItemOrbit.orbitFrequency + timeOffset;
			float angle2 = 6.2831855f * (float)index / 50f;
			float x = ItemOrbit.orbitRadius * Mathf.Sin(angle) * Mathf.Cos(angle2);
			float y = ItemOrbit.orbitRadius * Mathf.Sin(angle) * Mathf.Sin(angle2) + height;
			float z = ItemOrbit.orbitRadius * Mathf.Cos(angle);
			return targetPosition + new Vector3(x, y + height + ItemOrbit.orbitHeight, z);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00022FE8 File Offset: 0x000211E8
		private static Vector3 CalcEllipseOrbitPos(int index, Vector3 targetPosition, float height)
		{
			float timeOffset = ItemOrbit.orbitFrequency * (float)index;
			float x = Mathf.Sin(Time.time * ItemOrbit.orbitFrequency + timeOffset) * ItemOrbit.orbitRadius;
			float z = Mathf.Cos(Time.time * (ItemOrbit.orbitFrequency / 2f) + timeOffset) * (ItemOrbit.orbitRadius * 2f);
			return targetPosition + new Vector3(x, height + ItemOrbit.orbitHeight, z);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00023058 File Offset: 0x00021258
		private static Vector3 CalcSpiralOrbitPos(int index, Vector3 targetPosition, float height)
		{
			float timeOffset = ItemOrbit.orbitFrequency * (float)index;
			float radiusIncreaseRate = 0.05f;
			float minRadius = 1f;
			float radius = minRadius + timeOffset * radiusIncreaseRate;
			bool flag = radius > minRadius + ItemOrbit.orbitRadius * 3f;
			if (flag)
			{
				radius = minRadius + ItemOrbit.orbitRadius * 3f;
			}
			float x = Mathf.Sin(Time.time * ItemOrbit.orbitFrequency + timeOffset) * radius;
			float z = Mathf.Cos(Time.time * ItemOrbit.orbitFrequency + timeOffset) * radius;
			return targetPosition + new Vector3(x, height + ItemOrbit.orbitHeight, z);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x000230F0 File Offset: 0x000212F0
		private static Vector3 CalcFigure8OrbitPos(int index, Vector3 targetPosition, float height)
		{
			float timeOffset = ItemOrbit.orbitFrequency * (float)index;
			float angle = Time.time * ItemOrbit.orbitFrequency + timeOffset;
			float denom = Mathf.Sqrt(1f + Mathf.Pow(Mathf.Sin(angle), 2f));
			float x = 2f * ItemOrbit.orbitRadius * Mathf.Sin(angle) / denom;
			float z = 2f * ItemOrbit.orbitRadius * Mathf.Sin(angle) * Mathf.Cos(angle) / denom;
			return targetPosition + new Vector3(x, height + ItemOrbit.orbitHeight, z);
		}

		// Token: 0x040002EF RID: 751
		internal static bool hasStarted = false;

		// Token: 0x040002F0 RID: 752
		internal static Player target;

		// Token: 0x040002F1 RID: 753
		internal static int itemType = 0;

		// Token: 0x040002F2 RID: 754
		internal static string orbitShape = "circle";

		// Token: 0x040002F3 RID: 755
		internal static float orbitRadius = 1f;

		// Token: 0x040002F4 RID: 756
		internal static float orbitHeight = 0f;

		// Token: 0x040002F5 RID: 757
		internal static float orbitFrequency = 1f;

		// Token: 0x040002F6 RID: 758
		private const int MaxPickupsBeforeDelay = 50;
	}
}
