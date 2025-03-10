using System;
using EXO.Modules.Utilities;
using EXO_Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Client.HUD;

namespace EXO.LogTools
{
	// Token: 0x02000089 RID: 137
	internal class ScreenAPI
	{
		// Token: 0x0600057B RID: 1403 RVA: 0x0001C535 File Offset: 0x0001A735
		internal static void Log(string Msg)
		{
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001C538 File Offset: 0x0001A738
		public static void OnInject()
		{
			ScreenAPI.SetupNotificationsHud();
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001C540 File Offset: 0x0001A740
		internal static void SetupNotificationsHud()
		{
			bool isReady = ScreenAPI.IsReady;
			if (!isReady)
			{
				ScreenAPI.MainSprite = BaseImages.FromBase(BaseImages.Panel);
				foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
				{
					bool flag = !gameObject.name.StartsWith("User Event Cell");
					if (!flag)
					{
						gameObject.GetComponentInChildren<TMP_Text>().richText = true;
						GameObject templateObj = gameObject.FindObject("Background").gameObject;
						Vector3 scl = new Vector3(1f, 1f, 1f);
						templateObj.transform.localScale = new Vector3(scl.x + 0.25f, scl.y + 1.05f, scl.z);
						Image ImageCom = templateObj.GetComponent<Image>();
						ImageCom.color = new Color(1f, 1f, 1f, 0.83f);
						ImageCom.overrideSprite = ScreenAPI.MainSprite;
						ImageCom.sprite = ScreenAPI.MainSprite;
					}
				}
				ScreenAPI.IsReady = true;
			}
		}

		// Token: 0x04000280 RID: 640
		private static bool IsReady;

		// Token: 0x04000281 RID: 641
		private static Sprite MainSprite;

		// Token: 0x04000282 RID: 642
		private static Hud_UserEventCarousel ActiveCarousel;
	}
}
