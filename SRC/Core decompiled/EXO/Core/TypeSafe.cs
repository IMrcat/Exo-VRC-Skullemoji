using System;
using System.Linq;
using System.Reflection;

namespace EXO.Core
{
	// Token: 0x020000B8 RID: 184
	internal static class TypeSafe
	{
		// Token: 0x060006A2 RID: 1698 RVA: 0x00026CA0 File Offset: 0x00024EA0
		public static Type[] GetTypesSafe(this Assembly assembly)
		{
			Type[] array;
			try
			{
				array = assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				array = Enumerable.ToArray<Type>(Enumerable.Where<Type>(ex.Types, (Type t) => t != null));
			}
			return array;
		}
	}
}
