using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EXO.LogTools;

namespace EXO.Core
{
	// Token: 0x020000B0 RID: 176
	internal class FunctionCore
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x000261AC File Offset: 0x000243AC
		private static IEnumerable<FunctionModule> EModule
		{
			get
			{
				IEnumerable<FunctionModule> enumerable;
				if ((enumerable = FunctionCore.Module) == null)
				{
					enumerable = (FunctionCore.Module = Enumerable.ToList<FunctionModule>(Enumerable.Select<Type, FunctionModule>(Enumerable.Where<Type>(Assembly.GetExecutingAssembly().GetTypesSafe(), (Type o) => o.IsSubclassOf(typeof(FunctionModule))), (Type a) => (FunctionModule)Activator.CreateInstance(a))));
				}
				return enumerable;
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00026220 File Offset: 0x00024420
		public static void OnLoad()
		{
			foreach (FunctionModule func in FunctionCore.EModule)
			{
				try
				{
					if (func != null)
					{
						func.OnInject();
					}
				}
				catch (Exception ex)
				{
					CLog.E("Function OnInject Error! " + func.GetType().FullName, ex);
				}
			}
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x000262A8 File Offset: 0x000244A8
		public static void OnUpdate()
		{
			foreach (FunctionModule func in FunctionCore.EModule)
			{
				try
				{
					if (func != null)
					{
						func.OnUpdate();
					}
				}
				catch (Exception ex)
				{
					CLog.E("Function Update Error! " + func.GetType().FullName, ex);
				}
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00026330 File Offset: 0x00024530
		public static void OnFixedUpdate()
		{
			foreach (FunctionModule func in FunctionCore.EModule)
			{
				try
				{
					if (func != null)
					{
						func.OnFixedUpdate();
					}
				}
				catch (Exception ex)
				{
					CLog.E("Function Fixed Update Error! " + func.GetType().FullName, ex);
				}
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x000263B8 File Offset: 0x000245B8
		public static void OnPlayerWasInit()
		{
			foreach (FunctionModule func in FunctionCore.EModule)
			{
				try
				{
					if (func != null)
					{
						func.OnPlayerWasInit();
					}
				}
				catch (Exception ex)
				{
					CLog.E("Function PlayerWasInit Error! " + func.GetType().FullName, ex);
				}
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00026440 File Offset: 0x00024640
		public static void OnPlayerWasDestroyed()
		{
			foreach (FunctionModule func in FunctionCore.EModule)
			{
				try
				{
					if (func != null)
					{
						func.OnPlayerWasDestroyed();
					}
				}
				catch (Exception ex)
				{
					CLog.E("PlayerWasDestroyed Error! " + func.GetType().FullName, ex);
				}
			}
		}

		// Token: 0x04000317 RID: 791
		private static IEnumerable<FunctionModule> Module;
	}
}
