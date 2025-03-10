using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EXO.LogTools;

namespace EXO.Core
{
	// Token: 0x020000B4 RID: 180
	internal class ModuleCore
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0002676C File Offset: 0x0002496C
		private static IEnumerable<ModsModule> EModule
		{
			get
			{
				IEnumerable<ModsModule> enumerable;
				if ((enumerable = ModuleCore.Module) == null)
				{
					enumerable = (ModuleCore.Module = Enumerable.ToList<ModsModule>(Enumerable.Select<Type, ModsModule>(Enumerable.Where<Type>(Assembly.GetExecutingAssembly().GetTypesSafe(), (Type o) => o.IsSubclassOf(typeof(ModsModule))), (Type a) => (ModsModule)Activator.CreateInstance(a))));
				}
				return enumerable;
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x000267E0 File Offset: 0x000249E0
		public static void OnLoad()
		{
			foreach (ModsModule mod in ModuleCore.EModule)
			{
				try
				{
					if (mod != null)
					{
						mod.OnInject();
					}
				}
				catch (Exception ex)
				{
					CLog.E("Module OnInject Error! " + mod.GetType().FullName, ex);
				}
			}
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00026868 File Offset: 0x00024A68
		public static void OnUpdate()
		{
			foreach (ModsModule mod in ModuleCore.EModule)
			{
				try
				{
					if (mod != null)
					{
						mod.OnUpdate();
					}
				}
				catch (Exception ex)
				{
					CLog.E("Module Update Error! " + mod.GetType().FullName, ex);
				}
			}
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000268F0 File Offset: 0x00024AF0
		public static void OnFixedUpdate()
		{
			foreach (ModsModule mod in ModuleCore.EModule)
			{
				try
				{
					if (mod != null)
					{
						mod.OnFixedUpdate();
					}
				}
				catch (Exception ex)
				{
					CLog.E("Module Fixed Update Error! " + mod.GetType().FullName, ex);
				}
			}
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00026978 File Offset: 0x00024B78
		public static void OnPlayerWasInit()
		{
			foreach (ModsModule mod in ModuleCore.EModule)
			{
				try
				{
					if (mod != null)
					{
						mod.OnPlayerWasInit();
					}
				}
				catch (Exception ex)
				{
					CLog.E("Module PlayerWasInit Error! " + mod.GetType().FullName, ex);
				}
			}
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00026A00 File Offset: 0x00024C00
		public static void OnPlayerWasDestroyed()
		{
			foreach (ModsModule mod in ModuleCore.EModule)
			{
				try
				{
					if (mod != null)
					{
						mod.OnPlayerWasDestroyed();
					}
				}
				catch (Exception ex)
				{
					CLog.E("Module PlayerWasDestroyed Error! " + mod.GetType().FullName, ex);
				}
			}
		}

		// Token: 0x0400031C RID: 796
		private static IEnumerable<ModsModule> Module;
	}
}
