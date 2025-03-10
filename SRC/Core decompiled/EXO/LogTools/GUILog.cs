using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CoreRuntime.Manager;
using TMPro;
using UnityEngine;

namespace EXO.LogTools
{
	// Token: 0x02000087 RID: 135
	internal class GUILog
	{
		// Token: 0x06000573 RID: 1395 RVA: 0x0001BF78 File Offset: 0x0001A178
		internal static void InitToScreen()
		{
			bool flag = GUILog.isReady;
			if (!flag)
			{
				GameObject parentObject = GameObject.Find("UnscaledUI/HudContent/HUD_UI 2(Clone)/VR Canvas/Container/Left");
				GameObject logObject = new GameObject("LogObject");
				logObject.transform.SetParent(parentObject.transform, false);
				GUILog.logText = logObject.AddComponent<TextMeshProUGUI>();
				GUILog.logText.text = "";
				GUILog.logText.fontSize = 14f;
				RectTransform rectTransform = GUILog.logText.GetComponent<RectTransform>();
				rectTransform.sizeDelta = new Vector2(800f, 400f);
				rectTransform.anchorMin = new Vector2(0f, 1f);
				rectTransform.anchorMax = new Vector2(0f, 1f);
				rectTransform.pivot = new Vector2(0f, 1f);
				rectTransform.anchoredPosition = Vector2.zero;
				logObject.transform.localPosition = new Vector3(15f, 30f, 0f);
				GUILog.isReady = true;
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0001C080 File Offset: 0x0001A280
		internal static void DisplayOnScreen(string message)
		{
			bool flag = !Config.cfg.ScreenLogger;
			if (!flag)
			{
				GUILog.InitToScreen();
				bool flag2 = !GUILog.isReady;
				if (!flag2)
				{
					bool boldScreenLogs = Config.cfg.BoldScreenLogs;
					if (boldScreenLogs)
					{
						GUILog.messages.Enqueue(new KeyValuePair<string, float>("<b><color=#ffffff>[</color><color=#c00000>EXO</color><color=#ffffff>]</color> " + message + "<b>", Time.time));
					}
					else
					{
						GUILog.messages.Enqueue(new KeyValuePair<string, float>("<color=#ffffff>[</color><color=#c00000>EXO</color><color=#ffffff>]</color> " + message, Time.time));
					}
					while (GUILog.messages.Count > GUILog.maxMessages)
					{
						GUILog.messages.Dequeue();
					}
					GUILog.logText.text = "";
					foreach (KeyValuePair<string, float> msg in Enumerable.Reverse<KeyValuePair<string, float>>(GUILog.messages))
					{
						bool flag3 = Time.time - msg.Value <= 10f;
						if (flag3)
						{
							TextMeshProUGUI textMeshProUGUI = GUILog.logText;
							textMeshProUGUI.text = textMeshProUGUI.text + msg.Key + "\n";
						}
					}
					bool screenLogFade = Config.cfg.ScreenLogFade;
					if (screenLogFade)
					{
						CoroutineManager.RunCoroutine(GUILog.FadeOut(GUILog.logText));
					}
					else
					{
						GUILog.logText.alpha = 0.7f;
					}
				}
			}
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001C1FC File Offset: 0x0001A3FC
		private static IEnumerator FadeOut(TextMeshProUGUI text)
		{
			float duration = 3f;
			Color originalColor = text.color;
			for (float t = 0f; t < duration; t += Time.deltaTime)
			{
				text.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1f, 0f, t / duration));
				yield return null;
			}
			text.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
			yield break;
		}

		// Token: 0x04000275 RID: 629
		internal static bool isReady = false;

		// Token: 0x04000276 RID: 630
		internal static TextMeshProUGUI logText;

		// Token: 0x04000277 RID: 631
		internal static Queue<KeyValuePair<string, float>> messages = new Queue<KeyValuePair<string, float>>();

		// Token: 0x04000278 RID: 632
		internal static int maxMessages = 12;
	}
}
