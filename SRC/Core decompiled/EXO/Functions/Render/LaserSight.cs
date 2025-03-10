using System;
using System.Collections.Generic;
using EXO.Core;
using EXO.Menus.SubMenus;
using UnityEngine;

namespace EXO.Functions.Render
{
	// Token: 0x0200008F RID: 143
	internal class LaserSight : FunctionModule
	{
		// Token: 0x060005B0 RID: 1456 RVA: 0x0001E0F0 File Offset: 0x0001C2F0
		private static Mesh CreateCircleMesh(float radius, int segments)
		{
			Mesh mesh = new Mesh();
			Vector3[] vertices = new Vector3[segments + 1];
			int[] triangles = new int[segments * 3];
			vertices[0] = Vector3.zero;
			float angleStep = 360f / (float)segments;
			for (int i = 1; i <= segments; i++)
			{
				float angle = angleStep * (float)i * 0.017453292f;
				vertices[i] = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);
			}
			for (int j = 0; j < segments; j++)
			{
				int vertexIndex = j + 1;
				triangles[j * 3] = 0;
				triangles[j * 3 + 1] = vertexIndex;
				triangles[j * 3 + 2] = ((vertexIndex == segments) ? 1 : (vertexIndex + 1));
			}
			mesh.vertices = vertices;
			mesh.triangles = triangles;
			mesh.RecalculateNormals();
			return mesh;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0001E1E4 File Offset: 0x0001C3E4
		internal static void PrepLineRenderer(string objectName)
		{
			GameObject targetObject = GameObject.Find(objectName);
			bool flag = targetObject != null;
			if (flag)
			{
				GameObject temp = new GameObject(objectName + " Laser");
				temp.transform.SetParent(targetObject.transform);
				LineRenderer line = temp.AddComponent<LineRenderer>();
				line.startWidth = 0.005f;
				line.endWidth = 0.005f;
				line.alignment = LineAlignment.View;
				line.material = new Material(Shader.Find("Unlit/Color"));
				Color red = new Color(1f, 0f, 0f, 1f);
				line.material.color = red;
				line.enabled = true;
				GameObject circle = new GameObject("Circle");
				circle.transform.SetParent(temp.transform);
				MeshFilter meshFilter = circle.AddComponent<MeshFilter>();
				meshFilter.mesh = LaserSight.CreateCircleMesh(0.015f, 20);
				MeshRenderer meshRenderer = circle.AddComponent<MeshRenderer>();
				meshRenderer.material = new Material(Shader.Find("GUI/Text Shader"))
				{
					color = red
				};
				LaserSight.lineRenderers[objectName] = line;
			}
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0001E314 File Offset: 0x0001C514
		private static void UpdateLineRenderer(string objectName, Vector3 directionOverride = default(Vector3), float minDistanceThreshold = 0.15f)
		{
			LineRenderer line;
			bool flag = LaserSight.lineRenderers.TryGetValue(objectName, ref line) && line != null;
			if (flag)
			{
				GameObject targetObject = GameObject.Find(objectName);
				bool flag2 = targetObject != null;
				if (flag2)
				{
					Vector3 startPosition = targetObject.transform.position;
					line.SetPosition(1, startPosition);
					Vector3 direction = ((directionOverride == default(Vector3)) ? targetObject.transform.forward : directionOverride);
					Ray ray = new Ray(startPosition, direction);
					RaycastHit hitInfo;
					bool cast = Physics.Raycast(ray, out hitInfo, float.MaxValue, LaserSight.raycastLayerMask);
					bool flag3 = cast && hitInfo.transform != null && hitInfo.distance > minDistanceThreshold && !hitInfo.transform.name.ToLower().Contains("mirror");
					if (flag3)
					{
						Vector3 endPoint = hitInfo.point;
						line.SetPosition(0, endPoint);
						bool flag4 = line.transform.childCount > 0;
						if (flag4)
						{
							Transform circleTransform = line.transform.GetChild(0);
							circleTransform.gameObject.SetActive(true);
							circleTransform.position = endPoint + hitInfo.normal * 0.001f;
							circleTransform.rotation = Quaternion.LookRotation(hitInfo.normal);
						}
					}
					else
					{
						Vector3 endPoint = ray.GetPoint(200f);
						line.SetPosition(0, endPoint);
						bool flag5 = line.transform.childCount > 0;
						if (flag5)
						{
							Transform circleTransform2 = line.transform.GetChild(0);
							circleTransform2.gameObject.SetActive(false);
						}
					}
				}
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001E4C8 File Offset: 0x0001C6C8
		public override void OnUpdate()
		{
			bool toggleLaser = Murder4.toggleLaser;
			if (toggleLaser)
			{
				bool flag = !LaserSight.lineRenderers.ContainsKey(this.revolverPath);
				if (flag)
				{
					LaserSight.PrepLineRenderer(this.revolverPath);
				}
				LaserSight.UpdateLineRenderer(this.revolverPath, default(Vector3), 0.15f);
				bool flag2 = !LaserSight.lineRenderers.ContainsKey(this.shotgunPath);
				if (flag2)
				{
					LaserSight.PrepLineRenderer(this.shotgunPath);
				}
				LaserSight.UpdateLineRenderer(this.shotgunPath, default(Vector3), 0.15f);
				bool flag3 = !LaserSight.lineRenderers.ContainsKey(this.lugerPath);
				if (flag3)
				{
					LaserSight.PrepLineRenderer(this.lugerPath);
				}
				LaserSight.UpdateLineRenderer(this.lugerPath, default(Vector3), 0.15f);
			}
			else
			{
				List<string> keysToRemove = new List<string>();
				foreach (KeyValuePair<string, LineRenderer> kvp in LaserSight.lineRenderers)
				{
					bool flag4 = kvp.Value != null;
					if (flag4)
					{
						Object.Destroy(kvp.Value.gameObject);
						keysToRemove.Add(kvp.Key);
					}
				}
				foreach (string key in keysToRemove)
				{
					LaserSight.lineRenderers.Remove(key);
				}
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0001E66C File Offset: 0x0001C86C
		public override void OnPlayerWasDestroyed()
		{
			foreach (KeyValuePair<string, LineRenderer> kvp in LaserSight.lineRenderers)
			{
				bool flag = kvp.Value != null;
				if (flag)
				{
					Object.Destroy(kvp.Value.gameObject);
				}
			}
			LaserSight.lineRenderers.Clear();
		}

		// Token: 0x04000299 RID: 665
		internal static Dictionary<string, LineRenderer> lineRenderers = new Dictionary<string, LineRenderer>();

		// Token: 0x0400029A RID: 666
		internal string revolverPath = "Game Logic/Weapons/Revolver/Recoil Anim/Recoil";

		// Token: 0x0400029B RID: 667
		internal string shotgunPath = "Game Logic/Weapons/Unlockables/Shotgun (0)/Recoil Anim/Recoil";

		// Token: 0x0400029C RID: 668
		internal string lugerPath = "Game Logic/Weapons/Unlockables/Luger (0)/Recoil Anim/Recoil";

		// Token: 0x0400029D RID: 669
		private static LayerMask raycastLayerMask = ~LayerMask.GetMask(new string[] { "IgnoreRaycastEXO" });
	}
}
