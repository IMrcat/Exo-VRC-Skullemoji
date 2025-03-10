using System;
using System.Collections;
using System.Runtime.CompilerServices;
using CoreRuntime.Manager;
using EXO.Core;
using EXO.Modules.Utilities;
using EXO.Wrappers;
using TMPro;
using UnityEngine;

namespace EXO.Modules
{
	// Token: 0x02000070 RID: 112
	internal class HudDisplay : ModsModule
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x000152C8 File Offset: 0x000134C8
		public override void OnPlayerWasInit()
		{
			bool flag = !HudDisplay.isUpdating;
			if (flag)
			{
				CoroutineManager.RunCoroutine(HudDisplay.UpdateDisplay());
			}
			HudDisplay.InitToScreen();
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000152F3 File Offset: 0x000134F3
		public override void OnPlayerWasDestroyed()
		{
			CoroutineManager.StopCoroutine(HudDisplay.UpdateDisplay());
			HudDisplay.isUpdating = false;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00015308 File Offset: 0x00013508
		internal static void InitToScreen()
		{
			bool flag = HudDisplay.isReady || MenuCore.quickMenu == null;
			if (!flag)
			{
				GameObject parentObject = GameObject.Find("UnscaledUI/HudContent/HUD_UI 2(Clone)/VR Canvas/Container/Left");
				bool flag2 = parentObject == null;
				if (!flag2)
				{
					GameObject displayObject = new GameObject("DisplayObject");
					displayObject.transform.SetParent(parentObject.transform, false);
					HudDisplay.display = displayObject.AddComponent<TextMeshProUGUI>();
					HudDisplay.display.text = "";
					HudDisplay.display.fontSize = 15f;
					HudDisplay.display.alpha = 0.7f;
					RectTransform rectTransform = HudDisplay.display.GetComponent<RectTransform>();
					rectTransform.sizeDelta = new Vector2(100f, 100f);
					rectTransform.anchorMin = new Vector2(1f, 1f);
					rectTransform.anchorMax = new Vector2(1f, 1f);
					rectTransform.pivot = new Vector2(1f, 1f);
					rectTransform.anchoredPosition = Vector2.zero;
					displayObject.transform.localPosition = new Vector3(-70f, 20f, 0f);
					HudDisplay.isReady = true;
				}
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0001543F File Offset: 0x0001363F
		internal static IEnumerator UpdateDisplay()
		{
			HudDisplay.isUpdating = true;
			bool flag = !Config.cfg.DisplayHUD;
			if (flag)
			{
				yield break;
			}
			bool flag2 = !HudDisplay.isReady;
			if (flag2)
			{
				yield return new WaitForSeconds(1f);
			}
			for (;;)
			{
				HudDisplay.display.text = HudDisplay.DisplayFrames() + "\n" + HudDisplay.DisplayPing();
				yield return new WaitForSeconds(1f);
			}
			yield break;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00015448 File Offset: 0x00013648
		internal static string DisplayFrames()
		{
			string text2;
			try
			{
				float fps = Mathf.Floor(1f / Time.deltaTime);
				bool flag = fps >= 60f;
				if (flag)
				{
					string text = "<b><color=green>FPS</color> : ";
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
					defaultInterpolatedStringHandler.AppendFormatted<float>(fps);
					text2 = text + defaultInterpolatedStringHandler.ToStringAndClear().HexColor("green") + "</b>";
				}
				else
				{
					bool flag2 = fps >= 20f;
					if (flag2)
					{
						string text3 = "<b><color=yellow>FPS</color> : ";
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
						defaultInterpolatedStringHandler.AppendFormatted<float>(fps);
						text2 = text3 + defaultInterpolatedStringHandler.ToStringAndClear().HexColor("yellow") + "</b>";
					}
					else
					{
						string text4 = "<b><color=red>FPS</color> : ";
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
						defaultInterpolatedStringHandler.AppendFormatted<float>(fps);
						text2 = text4 + defaultInterpolatedStringHandler.ToStringAndClear().HexColor("red") + "</b>";
					}
				}
			}
			catch (Exception)
			{
				text2 = "";
			}
			return text2;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00015544 File Offset: 0x00013744
		internal static string DisplayPing()
		{
			string text2;
			try
			{
				short ping = PlayerWrapper.LocalPlayer.GetPing();
				bool flag = ping >= 150;
				if (flag)
				{
					string text = "<b><color=red>Ping</color> : ";
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
					defaultInterpolatedStringHandler.AppendFormatted<short>(ping);
					text2 = text + defaultInterpolatedStringHandler.ToStringAndClear().HexColor("red") + "</b>";
				}
				else
				{
					bool flag2 = ping >= 60;
					if (flag2)
					{
						string text3 = "<b><color=yellow>Ping</color> : ";
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
						defaultInterpolatedStringHandler.AppendFormatted<short>(ping);
						text2 = text3 + defaultInterpolatedStringHandler.ToStringAndClear().HexColor("yellow") + "</b>";
					}
					else
					{
						string text4 = "<b><color=green>Ping</color> : ";
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 1);
						defaultInterpolatedStringHandler.AppendFormatted<short>(ping);
						text2 = text4 + defaultInterpolatedStringHandler.ToStringAndClear().HexColor("green") + "</b>";
					}
				}
			}
			catch (Exception)
			{
				text2 = "";
			}
			return text2;
		}

		// Token: 0x040001EA RID: 490
		internal static bool isReady;

		// Token: 0x040001EB RID: 491
		internal static bool isUpdating;

		// Token: 0x040001EC RID: 492
		internal static TextMeshProUGUI display;
	}
}
