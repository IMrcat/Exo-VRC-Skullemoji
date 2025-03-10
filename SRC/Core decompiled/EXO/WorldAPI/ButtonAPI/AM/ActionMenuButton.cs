using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using VRC.Localization;

namespace WorldAPI.ButtonAPI.AM
{
	// Token: 0x02000028 RID: 40
	public class ActionMenuButton
	{
		// Token: 0x0600017C RID: 380 RVA: 0x000083BC File Offset: 0x000065BC
		public ActionMenuButton(ActionMenuBaseMenu baseMenu, string text, Action action, [Nullable(2)] Texture2D icon = null)
		{
			this.buttonText = text;
			bool flag = icon != null;
			if (flag)
			{
				bool flag2 = !icon.hideFlags.HasFlag(HideFlags.DontSave);
				if (flag2)
				{
					icon.hideFlags |= HideFlags.DontUnloadUnusedAsset;
				}
			}
			this.buttonIcon = icon;
			this.buttonAction = delegate
			{
				action.Invoke();
				return true;
			};
			bool flag3 = baseMenu == ActionMenuBaseMenu.MainMenu;
			if (flag3)
			{
				ActionWheelAPI.mainMenuButtons.Add(this);
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00008454 File Offset: 0x00006654
		public ActionMenuButton(ActionMenuPage basePage, string text, Action action, [Nullable(2)] Texture2D icon = null)
		{
			this.buttonText = text;
			bool flag = icon != null;
			if (flag)
			{
				bool flag2 = !icon.hideFlags.HasFlag(HideFlags.DontSave);
				if (flag2)
				{
					icon.hideFlags |= HideFlags.DontUnloadUnusedAsset;
				}
			}
			this.buttonIcon = icon;
			this.buttonAction = delegate
			{
				action.Invoke();
				return true;
			};
			basePage.buttons.Add(this);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000084E0 File Offset: 0x000066E0
		public void SetButtonText(string newText)
		{
			this.buttonText = newText;
			bool flag = this.currentPedalOption != null;
			if (flag)
			{
				this.currentPedalOption.prop_LocalizableString_0 = newText.Localize(null, null, null);
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000851C File Offset: 0x0000671C
		internal void SetIcon(Texture2D newTexture)
		{
			bool flag = newTexture == null;
			if (!flag)
			{
				bool flag2 = !newTexture.hideFlags.HasFlag(HideFlags.DontSave);
				if (flag2)
				{
					newTexture.hideFlags |= HideFlags.DontUnloadUnusedAsset;
				}
				this.buttonIcon = newTexture;
				PedalOption pedalOption = this.currentPedalOption;
				if (pedalOption != null)
				{
					pedalOption.Method_Public_Virtual_Final_New_Void_Texture2D_0(newTexture);
				}
			}
		}

		// Token: 0x040000A2 RID: 162
		[Nullable(2)]
		public PedalOption currentPedalOption;

		// Token: 0x040000A3 RID: 163
		public string buttonText;

		// Token: 0x040000A4 RID: 164
		[Nullable(2)]
		public Texture2D buttonIcon;

		// Token: 0x040000A5 RID: 165
		public readonly Func<bool> buttonAction;
	}
}
