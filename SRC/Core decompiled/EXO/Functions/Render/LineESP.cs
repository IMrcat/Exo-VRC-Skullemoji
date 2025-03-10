using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EXO.Core;
using EXO.Functions.PlayerFunc;
using EXO.Menus.SelectMenus;
using EXO.Patches;
using EXO.Wrappers;
using UnityEngine;
using VRC;

namespace EXO.Functions.Render
{
	// Token: 0x02000090 RID: 144
	internal class LineESP : FunctionModule
	{
		// Token: 0x060005B7 RID: 1463 RVA: 0x0001E73C File Offset: 0x0001C93C
		public override void OnPlayerWasInit()
		{
			bool esp = LineESP.ESP;
			if (esp)
			{
				LineESP.EnableLines();
			}
			JoinLeavePatch.OnPlayerJoin = (Action<Player>)Delegate.Combine(JoinLeavePatch.OnPlayerJoin, delegate(Player player)
			{
				LineESP.<OnPlayerWasInit>g__PlayerEvent|2_0();
			});
			JoinLeavePatch.OnPlayerLeave = (Action<Player>)Delegate.Combine(JoinLeavePatch.OnPlayerLeave, delegate(Player player)
			{
				LineESP.<OnPlayerWasInit>g__PlayerEvent|2_0();
			});
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001E7C2 File Offset: 0x0001C9C2
		public override void OnPlayerWasDestroyed()
		{
			LineESP.DisableLines();
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0001E7CC File Offset: 0x0001C9CC
		public override void OnUpdate()
		{
			try
			{
				foreach (Player player in PlayerWrapper.GetAllPlayers)
				{
					LineESP.RenderLines(player);
				}
			}
			catch (Exception e)
			{
			}
			try
			{
				bool lineSelectESP = ListenerSelect.lineSelectESP;
				if (lineSelectESP)
				{
					LineESP.RenderLines(ListenerSelect.target);
				}
			}
			catch (Exception e2)
			{
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0001E840 File Offset: 0x0001CA40
		internal static void RenderLines(Player player)
		{
			LineRenderer lineRenderer;
			bool flag = player.GetAPIUser() != null && LineESP.playerLines.TryGetValue(player.UserID(), ref lineRenderer);
			if (flag)
			{
				Vector3 localPlayerPos = PlayerWrapper.LocalPlayerPostion;
				Vector3 targetPlayerPos = player.transform.position;
				lineRenderer.SetPosition(0, localPlayerPos);
				lineRenderer.SetPosition(1, targetPlayerPos);
				lineRenderer.startWidth = 0.005f * CamTweaks.currentHeight;
				lineRenderer.endWidth = 0.005f * CamTweaks.currentHeight;
				lineRenderer.enabled = true;
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001E8C4 File Offset: 0x0001CAC4
		public static void EnableListenerLine(Player player)
		{
			bool flag = !LineESP.playerLines.ContainsKey(player.UserID());
			if (flag)
			{
				LineRenderer lineRenderer = LineESP.PrepObj(player);
				LineESP.playerLines.Add(player.UserID(), lineRenderer);
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001E904 File Offset: 0x0001CB04
		public static void EnableLines()
		{
			foreach (Player player in PlayerWrapper.GetAllPlayers)
			{
				bool flag = !LineESP.playerLines.ContainsKey(player.UserID());
				if (flag)
				{
					LineRenderer lineRenderer = LineESP.PrepObj(player);
					LineESP.playerLines.Add(player.UserID(), lineRenderer);
				}
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001E964 File Offset: 0x0001CB64
		public static void DisableLines()
		{
			foreach (LineRenderer line in LineESP.playerLines.Values)
			{
				bool flag = line != null;
				if (flag)
				{
					Object.Destroy(line.gameObject);
				}
			}
			LineESP.playerLines.Clear();
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001E9DC File Offset: 0x0001CBDC
		private static LineRenderer PrepObj(Player player)
		{
			LineRenderer line = new GameObject(player.prop_APIUser_0.displayName + " ESP")
			{
				transform = 
				{
					parent = player.transform
				}
			}.AddComponent<LineRenderer>();
			line.startWidth = 0.005f * CamTweaks.currentHeight;
			line.endWidth = 0.005f * CamTweaks.currentHeight;
			line.alignment = LineAlignment.View;
			line.material = new Material(Shader.Find("GUI/Text Shader"));
			bool trustColors = Config.cfg.TrustColors;
			if (trustColors)
			{
				line.material.color = player.GetTrustColor();
			}
			else
			{
				bool flag = player.IsFriend();
				if (flag)
				{
					line.material.color = Color.yellow;
				}
				else
				{
					line.material.color = new Color(1f, 0f, 0f, 1f);
				}
			}
			return line;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001EAE8 File Offset: 0x0001CCE8
		[CompilerGenerated]
		internal static void <OnPlayerWasInit>g__PlayerEvent|2_0()
		{
			bool esp = LineESP.ESP;
			if (esp)
			{
				LineESP.DisableLines();
				LineESP.EnableLines();
			}
		}

		// Token: 0x0400029E RID: 670
		internal static bool ESP = false;

		// Token: 0x0400029F RID: 671
		private static Dictionary<string, LineRenderer> playerLines = new Dictionary<string, LineRenderer>();
	}
}
