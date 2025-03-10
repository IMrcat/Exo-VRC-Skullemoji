using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EXO.LogTools;
using HexedTools.HookUtils;

namespace EXO.Core
{
	// Token: 0x020000B6 RID: 182
	internal class PatchCore
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00026AAC File Offset: 0x00024CAC
		private static IEnumerable<PatchModule> EModule
		{
			get
			{
				IEnumerable<PatchModule> enumerable;
				if ((enumerable = PatchCore.Module) == null)
				{
					enumerable = (PatchCore.Module = Enumerable.ToList<PatchModule>(Enumerable.Select<Type, PatchModule>(Enumerable.Where<Type>(Assembly.GetExecutingAssembly().GetTypesSafe(), (Type o) => o.IsSubclassOf(typeof(PatchModule))), (Type a) => (PatchModule)Activator.CreateInstance(a))));
				}
				return enumerable;
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00026B20 File Offset: 0x00024D20
		public static void LoadPatches()
		{
			PatchCore.PatchLogs("Starting Patches...");
			foreach (PatchModule patch in PatchCore.EModule)
			{
				try
				{
					MethodInfo isOverriding = patch.GetType().GetMethod("LoadPatch");
					bool flag = isOverriding.DeclaringType == patch.GetType();
					if (flag)
					{
						PatchCore.PatchLogs("Patching " + patch.GetType().Name + "...");
						if (patch != null)
						{
							patch.LoadPatch();
						}
					}
				}
				catch (Exception ex)
				{
					string text = "Patching Error! ";
					string fullName = patch.GetType().FullName;
					string text2 = "\n";
					Exception ex2 = ex;
					PatchCore.PatchLogs(text + fullName + text2 + ((ex2 != null) ? ex2.ToString() : null));
				}
			}
			PatchCore.PatchLogs("Finished Patches");
			Logs.WriteOut("", 15);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00026C2C File Offset: 0x00024E2C
		internal static void PatchLogs(string msg)
		{
			CLog.Tag();
			"[".WriteToConsole(15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Core\\PatchCore.cs", 45).WriteToConsole("Patch", 10, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Core\\PatchCore.cs", 46).WriteToConsole("] ", 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Core\\PatchCore.cs", 47)
				.WriteLineToConsole(msg, 15, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Core\\PatchCore.cs", 48);
		}

		// Token: 0x0400031D RID: 797
		private static IEnumerable<PatchModule> Module;
	}
}
