using System;
using System.Collections.Generic;
using EXO.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EXO.Functions.MenuOverrides
{
	// Token: 0x0200009F RID: 159
	internal class QMConsole
	{
		// Token: 0x06000619 RID: 1561 RVA: 0x00022250 File Offset: 0x00020450
		internal static void InitConsole()
		{
			bool flag = QMConsole.isReady || MenuCore.quickMenu == null;
			if (!flag)
			{
				GameObject parentObject = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup");
				QMConsole.logObject = new GameObject("EXO_Console");
				QMConsole.logObject.transform.SetParent(parentObject.transform, false);
				GameObject box = new GameObject("Box");
				box.transform.SetParent(QMConsole.logObject.transform, false);
				Image boxImage = box.AddComponent<Image>();
				boxImage.color = new Color(0f, 0f, 0f, 0.5f);
				RectMask2D mask = box.AddComponent<RectMask2D>();
				RectTransform boxRect = box.GetComponent<RectTransform>();
				boxRect.sizeDelta = QMConsole.consoleSize;
				boxRect.anchorMin = new Vector2(0f, 1f);
				boxRect.anchorMax = new Vector2(0f, 1f);
				boxRect.pivot = new Vector2(0f, 1f);
				boxRect.anchoredPosition = Vector2.zero;
				box.transform.localPosition = Vector2.zero;
				ScrollRect scrollRect = QMConsole.logObject.AddComponent<ScrollRect>();
				scrollRect.horizontal = false;
				scrollRect.vertical = true;
				GameObject viewport = new GameObject("Viewport");
				viewport.transform.SetParent(QMConsole.logObject.transform, false);
				RectTransform viewportRect = viewport.AddComponent<RectTransform>();
				viewportRect.sizeDelta = QMConsole.consoleSize;
				viewportRect.anchorMin = new Vector2(0f, 1f);
				viewportRect.anchorMax = new Vector2(0f, 1f);
				viewportRect.pivot = new Vector2(0f, 1f);
				viewportRect.anchoredPosition = Vector2.zero;
				viewport.AddComponent<CanvasRenderer>();
				viewport.AddComponent<Image>();
				viewport.AddComponent<Mask>().showMaskGraphic = false;
				scrollRect.viewport = viewportRect;
				viewport.transform.localPosition = Vector2.zero;
				GameObject content = new GameObject("Content");
				content.transform.SetParent(viewport.transform, false);
				RectTransform contentRect = content.AddComponent<RectTransform>();
				contentRect.sizeDelta = QMConsole.consoleSize;
				QMConsole.logText = content.AddComponent<TextMeshProUGUI>();
				QMConsole.logText.text = "";
				QMConsole.logText.fontSize = 20f;
				QMConsole.logText.alignment = TextAlignmentOptions.BottomLeft;
				QMConsole.logText.color = Color.white;
				QMConsole.logText.enableWordWrapping = true;
				QMConsole.logText.rectTransform.sizeDelta = QMConsole.consoleSize;
				QMConsole.logText.rectTransform.anchorMin = new Vector2(0f, 1f);
				QMConsole.logText.rectTransform.anchorMax = new Vector2(0f, 1f);
				QMConsole.logText.rectTransform.pivot = new Vector2(0f, 1f);
				QMConsole.logText.rectTransform.anchoredPosition = Vector2.zero;
				QMConsole.logText.transform.localPosition = Vector2.zero;
				scrollRect.content = contentRect;
				box.transform.localPosition = Vector2.zero;
				viewport.transform.localPosition = Vector2.zero;
				content.transform.localPosition = Vector2.zero;
				QMConsole.logText.transform.localPosition = Vector2.zero;
				QMConsole.isReady = true;
			}
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00022604 File Offset: 0x00020804
		internal static void LogToQM(string message)
		{
			QMConsole.InitConsole();
			bool flag = !QMConsole.isReady;
			if (!flag)
			{
				bool boldQMConsole = Config.cfg.BoldQMConsole;
				if (boldQMConsole)
				{
					QMConsole.messages.Enqueue("<b>" + message + "<b>");
				}
				else
				{
					QMConsole.messages.Enqueue(message);
				}
				string allMessages = string.Concat(QMConsole.messages);
				bool flag2 = allMessages.Length > 10000;
				if (flag2)
				{
					string text = allMessages;
					int length = text.Length;
					int num = length - 10000;
					allMessages = text.Substring(num, length - num);
				}
				bool boldQMConsole2 = Config.cfg.BoldQMConsole;
				if (boldQMConsole2)
				{
					QMConsole.logText.text = "<b>" + allMessages + "<b>";
				}
				else
				{
					QMConsole.logText.text = allMessages ?? "";
				}
			}
		}

		// Token: 0x040002D6 RID: 726
		internal static bool isReady = false;

		// Token: 0x040002D7 RID: 727
		internal static TextMeshProUGUI logText;

		// Token: 0x040002D8 RID: 728
		internal static Queue<string> messages = new Queue<string>();

		// Token: 0x040002D9 RID: 729
		internal static GameObject logObject;

		// Token: 0x040002DA RID: 730
		internal static Vector2 consoleSize = new Vector2(650f, 800f);
	}
}
