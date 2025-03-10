using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;

namespace EXO_Engine
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(0)]
	internal class GetHardware
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002AE8 File Offset: 0x00000CE8
		internal static string GetID()
		{
			string cpuID = GetHardware.GetHardwareID("Win32_Processor", "ProcessorId");
			string motherboardID = GetHardware.GetHardwareID("Win32_BaseBoard", "SerialNumber");
			string gpuID = GetHardware.GetHardwareID("Win32_VideoController", "PNPDeviceID");
			string ramID = GetHardware.GetHardwareID("Win32_PhysicalMemory", "SerialNumber");
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 4);
			defaultInterpolatedStringHandler.AppendFormatted(cpuID);
			defaultInterpolatedStringHandler.AppendLiteral("-");
			defaultInterpolatedStringHandler.AppendFormatted(motherboardID);
			defaultInterpolatedStringHandler.AppendLiteral("-");
			defaultInterpolatedStringHandler.AppendFormatted(gpuID);
			defaultInterpolatedStringHandler.AppendLiteral("-");
			defaultInterpolatedStringHandler.AppendFormatted(ramID);
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002B9C File Offset: 0x00000D9C
		private static string GetHardwareID(string wmiClass, string wmiProperty)
		{
			try
			{
				ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT " + wmiProperty + " FROM " + wmiClass);
				using (IEnumerator<ManagementObject> enumerator = searcher.Get().Cast<ManagementObject>().GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						ManagementObject obj = enumerator.Current;
						object obj2 = obj[wmiProperty];
						return ((obj2 != null) ? obj2.ToString() : null) ?? "Not Found";
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
			}
			return "Not Found";
		}
	}
}
