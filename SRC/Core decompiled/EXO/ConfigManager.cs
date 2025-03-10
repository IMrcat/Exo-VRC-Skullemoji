using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using HexedTools.HookUtils;
using UnityEngine;

namespace EXO
{
	// Token: 0x02000032 RID: 50
	internal class ConfigManager
	{
		// Token: 0x0600023D RID: 573 RVA: 0x0000955A File Offset: 0x0000775A
		internal static void LoadConfig()
		{
			Config.Log("Loading Config...");
			Logs.WriteOut("", 15);
			ConfigManager.MakeConfig();
			ConfigManager.AutoSave();
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00009584 File Offset: 0x00007784
		public static void MakeConfig()
		{
			ConfigManager.instance = new ConfigInit<ConfigSettings>(Directory.CreateDirectory(AppStart.HexedDirectory.FullName + "\\EXO").FullName + "\\Config.json", "Config.json");
			ConfigManager.keybinds = new ConfigInit<Keybindings>(Directory.CreateDirectory(AppStart.HexedDirectory.FullName + "\\EXO").FullName + "\\Keybindings.json", "Keybindings.json");
			ConfigManager.render = new ConfigInit<RenderSettings>(Directory.CreateDirectory(AppStart.HexedDirectory.FullName + "\\EXO").FullName + "\\Render.json", "Render.json");
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00009637 File Offset: 0x00007837
		public static void AutoSave()
		{
			ConfigManager.<AutoSave>g__saveLoop|5_0();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00009641 File Offset: 0x00007841
		public static void Save()
		{
			Config.Log("Saving...");
			ConfigManager.instance.Save();
			ConfigManager.keybinds.Save();
			ConfigManager.render.Save();
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000967C File Offset: 0x0000787C
		[CompilerGenerated]
		internal static IEnumerator <AutoSave>g__saveLoop|5_0()
		{
			for (;;)
			{
				yield return new WaitForSeconds(15f);
				ConfigManager.instance.Save();
				ConfigManager.keybinds.Save();
				ConfigManager.render.Save();
			}
			yield break;
		}

		// Token: 0x04000108 RID: 264
		public static ConfigInit<ConfigSettings> instance;

		// Token: 0x04000109 RID: 265
		public static ConfigInit<Keybindings> keybinds;

		// Token: 0x0400010A RID: 266
		public static ConfigInit<RenderSettings> render;
	}
}
