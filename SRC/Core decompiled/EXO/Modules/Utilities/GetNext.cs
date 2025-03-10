using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using EXO.LogTools;
using UnityEngine;
using VRC.SDKBase;

namespace EXO.Modules.Utilities
{
	// Token: 0x02000075 RID: 117
	internal class GetNext
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x00016AD0 File Offset: 0x00014CD0
		public static void BringItem(string objectPath, int maxItems)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(58, 2);
			defaultInterpolatedStringHandler.AppendLiteral("[ GetNext.BringItem ] Called with objectPath: ");
			defaultInterpolatedStringHandler.AppendFormatted(objectPath);
			defaultInterpolatedStringHandler.AppendLiteral(", maxItems: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(maxItems);
			CLog.D(defaultInterpolatedStringHandler.ToStringAndClear(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 19);
			bool flag = !GetNext.currentItemIndices.ContainsKey(objectPath);
			if (flag)
			{
				GetNext.currentItemIndices[objectPath] = 0;
				CLog.D("[ GetNext.BringItem ] Initialized currentItemIndices for " + objectPath, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 25);
			}
			string baseObjectName = (objectPath.Contains("(") ? objectPath.Substring(0, objectPath.IndexOf(" (")) : objectPath);
			CLog.D("[ GetNext.BringItem ] Base object name: " + baseObjectName, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 30);
			string text = baseObjectName;
			string text2;
			if (GetNext.currentItemIndices[objectPath] <= 0)
			{
				text2 = "";
			}
			else
			{
				defaultInterpolatedStringHandler..ctor(3, 1);
				defaultInterpolatedStringHandler.AppendLiteral(" (");
				defaultInterpolatedStringHandler.AppendFormatted<int>(GetNext.currentItemIndices[objectPath]);
				defaultInterpolatedStringHandler.AppendLiteral(")");
				text2 = defaultInterpolatedStringHandler.ToStringAndClear();
			}
			string objectToFind = text + text2;
			CLog.L("[ GetNext.BringItem ] Looking for object: " + objectToFind, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 34);
			GameObject Obj = GameObject.Find(objectToFind);
			bool flag2 = Obj == null && GetNext.currentItemIndices[objectPath] == 0;
			if (flag2)
			{
				CLog.D("[ GetNext.BringItem ] Object not found: " + objectToFind, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 39);
				objectToFind = objectPath + " (0)";
				CLog.D("[ GetNext.BringItem ] Retrying with: " + objectToFind, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 41);
				Obj = GameObject.Find(objectToFind);
			}
			bool flag3 = Obj;
			if (flag3)
			{
				CLog.L("[ GetNext.BringItem ] Found object: " + objectToFind, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 46);
				Obj.SetActive(true);
				Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, Obj);
				Obj.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.3f, 0f);
			}
			else
			{
				CLog.D("[ GetNext.BringItem ] Failed to find object: " + objectToFind + "\n", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 52);
			}
			Dictionary<string, int> dictionary = GetNext.currentItemIndices;
			int num = dictionary[objectPath];
			dictionary[objectPath] = num + 1;
			defaultInterpolatedStringHandler..ctor(48, 2);
			defaultInterpolatedStringHandler.AppendLiteral("[ GetNext.BringItem ] Incremented index for ");
			defaultInterpolatedStringHandler.AppendFormatted(objectPath);
			defaultInterpolatedStringHandler.AppendLiteral(" to ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(GetNext.currentItemIndices[objectPath]);
			CLog.D(defaultInterpolatedStringHandler.ToStringAndClear(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 57);
			bool flag4 = GetNext.currentItemIndices[objectPath] >= maxItems;
			if (flag4)
			{
				GetNext.currentItemIndices[objectPath] = 0;
				CLog.D("[ GetNext.BringItem ] Reached max items for " + objectPath + ", resetting index to 0.", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 63);
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00016DCC File Offset: 0x00014FCC
		public static GameObject Item(string objectPath, int maxItems)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(53, 2);
			defaultInterpolatedStringHandler.AppendLiteral("[ GetNext.Item ] Called with objectPath: ");
			defaultInterpolatedStringHandler.AppendFormatted(objectPath);
			defaultInterpolatedStringHandler.AppendLiteral(", maxItems: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(maxItems);
			CLog.D(defaultInterpolatedStringHandler.ToStringAndClear(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 69);
			bool flag = !GetNext.currentItemIndices.ContainsKey(objectPath);
			if (flag)
			{
				GetNext.currentItemIndices[objectPath] = 0;
				CLog.D("[ GetNext.Item ] Initialized currentItemIndices for " + objectPath, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 75);
			}
			string baseObjectName = (objectPath.Contains("(") ? objectPath.Substring(0, objectPath.IndexOf(" (")) : objectPath);
			CLog.D("[ GetNext.Item ] Base object name: " + baseObjectName, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 80);
			string text = baseObjectName;
			string text2;
			if (GetNext.currentItemIndices[objectPath] <= 0)
			{
				text2 = "";
			}
			else
			{
				defaultInterpolatedStringHandler..ctor(3, 1);
				defaultInterpolatedStringHandler.AppendLiteral(" (");
				defaultInterpolatedStringHandler.AppendFormatted<int>(GetNext.currentItemIndices[objectPath]);
				defaultInterpolatedStringHandler.AppendLiteral(")");
				text2 = defaultInterpolatedStringHandler.ToStringAndClear();
			}
			string objectToFind = text + text2;
			CLog.L("[ GetNext.Item ] Looking for object: " + objectToFind, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 84);
			GameObject Obj = GameObject.Find(objectToFind);
			bool flag2 = Obj == null && GetNext.currentItemIndices[objectPath] == 0;
			if (flag2)
			{
				CLog.D("[ GetNext.Item ] Object not found: " + objectToFind, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 89);
				objectToFind = objectPath + " (0)";
				CLog.D("[ GetNext.Item ] Retrying with: " + objectToFind, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 91);
				Obj = GameObject.Find(objectToFind);
			}
			bool flag3 = Obj;
			if (flag3)
			{
				CLog.L("[ GetNext.Item ] Found object: " + objectToFind, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 95);
			}
			else
			{
				CLog.D("[ GetNext.Item ] Failed to find object: " + objectToFind + "\n", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 97);
			}
			Dictionary<string, int> dictionary = GetNext.currentItemIndices;
			int num = dictionary[objectPath];
			dictionary[objectPath] = num + 1;
			defaultInterpolatedStringHandler..ctor(43, 2);
			defaultInterpolatedStringHandler.AppendLiteral("[ GetNext.Item ] Incremented index for ");
			defaultInterpolatedStringHandler.AppendFormatted(objectPath);
			defaultInterpolatedStringHandler.AppendLiteral(" to ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(GetNext.currentItemIndices[objectPath]);
			CLog.D(defaultInterpolatedStringHandler.ToStringAndClear(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 101);
			bool flag4 = GetNext.currentItemIndices[objectPath] >= maxItems;
			if (flag4)
			{
				GetNext.currentItemIndices[objectPath] = 0;
				CLog.D("[ GetNext.Item ] Reached max items for " + objectPath + ", resetting index to 0.", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 107);
			}
			return Obj;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00017080 File Offset: 0x00015280
		public static List<GameObject> GetAllItems(string baseObjectName, int maxItems)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(64, 2);
			defaultInterpolatedStringHandler.AppendLiteral("[ GetNext.GetAllItems ] Called with baseObjectName: ");
			defaultInterpolatedStringHandler.AppendFormatted(baseObjectName);
			defaultInterpolatedStringHandler.AppendLiteral(", maxItems: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(maxItems);
			CLog.D(defaultInterpolatedStringHandler.ToStringAndClear(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 116);
			List<GameObject> objects = new List<GameObject>();
			for (int i = 0; i < maxItems; i++)
			{
				string text;
				if (i <= 0)
				{
					text = "";
				}
				else
				{
					defaultInterpolatedStringHandler..ctor(3, 1);
					defaultInterpolatedStringHandler.AppendLiteral(" (");
					defaultInterpolatedStringHandler.AppendFormatted<int>(i);
					defaultInterpolatedStringHandler.AppendLiteral(")");
					text = defaultInterpolatedStringHandler.ToStringAndClear();
				}
				string objectToFind = baseObjectName + text;
				CLog.L("[ GetNext.GetAllItems ] Looking for object: " + objectToFind, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 124);
				GameObject Obj = GameObject.Find(objectToFind);
				bool flag = Obj == null && i == 0;
				if (flag)
				{
					CLog.D("[ GetNext.GetAllItems ] Object not found: " + objectToFind, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 129);
					objectToFind = baseObjectName + " (0)";
					CLog.D("[ GetNext.GetAllItems ] Retrying with: " + objectToFind, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 131);
					Obj = GameObject.Find(objectToFind);
				}
				bool flag2 = Obj;
				if (flag2)
				{
					CLog.L("[ GetNext.GetAllItems ] Found object: " + objectToFind, false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 136);
					objects.Add(Obj);
				}
				else
				{
					CLog.D("[ GetNext.GetAllItems ] Failed to find object: " + objectToFind + "\n", false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Modules\\Utilities\\GetNext.cs", 140);
				}
			}
			return objects;
		}

		// Token: 0x040001FA RID: 506
		private static Dictionary<string, int> currentItemIndices = new Dictionary<string, int>();
	}
}
