using System;
using System.Reflection;

namespace EXO
{
	// Token: 0x02000031 RID: 49
	public class RenderSettings
	{
		// Token: 0x0600023B RID: 571 RVA: 0x000094D4 File Offset: 0x000076D4
		internal static void WriteConfig()
		{
			Type type = Config.gui.GetType();
			PropertyInfo[] properties = type.GetProperties(20);
			Config.Log("");
			Config.Log("Render:");
			foreach (PropertyInfo property in properties)
			{
				string name = property.Name;
				string text = ": ";
				object value = property.GetValue(Config.gui);
				Config.Log(name + text + ((value != null) ? value.ToString() : null));
			}
		}
	}
}
