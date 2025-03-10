using System;
using EXO.Core;
using EXO.LogTools;
using EXO.Wrappers;
using Il2CppSystem;
using VRC;
using VRC.Core;
using VRC.SDKBase;

namespace EXO.Patches.PatchHandlers
{
	// Token: 0x02000046 RID: 70
	internal class RPCHandle : PatchModule
	{
		// Token: 0x0600030E RID: 782 RVA: 0x0000CCB6 File Offset: 0x0000AEB6
		public override void LoadPatch()
		{
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000CCBC File Offset: 0x0000AEBC
		private static bool CaughtEventPatch(VRC_EventDispatcherRFC _instance, ref Player __0, ref VRC_EventHandler.VrcEvent __1, object param_3, int param_4, float param_5)
		{
			bool flag = __0.UserID() == null;
			return !flag || true;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000CCE4 File Offset: 0x0000AEE4
		private static bool ReadRPC(VRC_EventHandler.VrcEvent __1, Player Sender)
		{
			try
			{
				bool flag = ((Sender != null) ? Sender.UserID() : null) == null;
				if (flag)
				{
					return true;
				}
				bool flag2 = !__1.ParameterString.ToLower().Contains("udonsync");
				if (flag2)
				{
					return true;
				}
				string[] DecodedParameterBytes = new string[__1.ParameterBytes.Count];
				try
				{
					bool flag3 = __1.ParameterBytes != null;
					if (flag3)
					{
						Object[] ParamBytes = ParameterSerialization.Method_Public_Static_Il2CppReferenceArray_1_Object_Il2CppStructArray_1_Byte_0(__1.ParameterBytes);
						for (int i = 0; i < ParamBytes.Length; i++)
						{
							DecodedParameterBytes[i] = Convert.ToString(ParamBytes[i]);
						}
					}
				}
				catch (Exception e)
				{
					CLog.L("[Error] Failed To DeSerz RPC!\n" + e.ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Patches\\PatchHandlers\\RPCHandle.cs", 60);
					return false;
				}
				bool IsLocalPlayer = Sender.UserID().Equals(PlayerWrapper.LocalPlayer.UserID());
			}
			catch (Exception e2)
			{
				CLog.E(e2);
				return true;
			}
			return true;
		}
	}
}
