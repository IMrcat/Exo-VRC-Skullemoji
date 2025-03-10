using System;
using EXO.LogTools;

namespace EXO
{
	// Token: 0x0200002E RID: 46
	internal class Config
	{
		// Token: 0x0600019E RID: 414 RVA: 0x00008BE4 File Offset: 0x00006DE4
		internal static void Log(string msg)
		{
			CLog.Tag();
			"[".WriteToConsole(15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Config.cs", 27).WriteToConsole("Config", 11, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Config.cs", 28).WriteToConsole("] ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Config.cs", 29)
				.WriteLineToConsole(msg, 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Config.cs", 30);
		}

		// Token: 0x040000B9 RID: 185
		public static ConfigSettings cfg = ConfigManager.instance.Cfg;

		// Token: 0x040000BA RID: 186
		public static Keybindings binds = ConfigManager.keybinds.Cfg;

		// Token: 0x040000BB RID: 187
		public static RenderSettings gui = ConfigManager.render.Cfg;
	}
}
