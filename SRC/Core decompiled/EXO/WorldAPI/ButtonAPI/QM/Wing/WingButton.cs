using System;
using EXO.Modules.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EXO.WorldAPI.ButtonAPI.QM.Wing
{
	// Token: 0x02000038 RID: 56
	internal class WingButton
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0000AEC0 File Offset: 0x000090C0
		public WingButton(Transform par, string text, Action listener, bool left = true, Sprite Icon = null)
		{
			Transform button;
			if (left)
			{
				button = par.FindObject("Wing_Left/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emoji");
			}
			else
			{
				button = par.FindObject("Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emoji");
			}
			GameObject inst = Object.Instantiate<Transform>(button, button.parent).gameObject;
			inst.transform.SetAsFirstSibling();
			TextMeshProUGUI txt = inst.GetComponentInChildren<TextMeshProUGUI>();
			txt.richText = true;
			txt.text = text;
			try
			{
				inst.transform.Find("Container/Icon").GetComponent<Image>().overrideSprite = Icon;
			}
			catch
			{
			}
			Button btn = inst.GetComponent<Button>();
			btn.onClick.RemoveAllListeners();
			btn.onClick.AddListener(listener);
		}
	}
}
