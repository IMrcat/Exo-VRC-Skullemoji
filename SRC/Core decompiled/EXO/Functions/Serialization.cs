using System;
using System.Runtime.CompilerServices;
using System.Text;
using ExitGames.Client.Photon;
using EXO.LogTools;
using EXO.Modules.Tools;
using EXO.Modules.Utilities;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppSystem;
using Il2CppSystem.Collections;
using Il2CppSystem.Collections.Generic;
using Il2CppSystem.IO;
using Il2CppSystem.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace EXO.Functions
{
	// Token: 0x0200008B RID: 139
	internal static class Serialization
	{
		// Token: 0x06000583 RID: 1411 RVA: 0x0001C740 File Offset: 0x0001A940
		internal static byte[] GetRndByteArray(int sizeInKb)
		{
			global::System.Random random = new global::System.Random();
			byte[] array = new byte[sizeInKb * 1024];
			random.NextBytes(array);
			return array;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001C770 File Offset: 0x0001A970
		internal static global::UnityEngine.Object ByteArrayToObjectUnity(byte[] arrBytes)
		{
			Il2CppStructArray<byte> il2CppStructArray = new Il2CppStructArray<byte>((IntPtr)arrBytes.Length);
			arrBytes.CopyTo(il2CppStructArray, 0);
			global::Il2CppSystem.Object @object = new global::Il2CppSystem.Object(il2CppStructArray.Pointer);
			return new global::UnityEngine.Object(@object.Pointer);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001C7B4 File Offset: 0x0001A9B4
		internal static byte[] ToByteArray(global::Il2CppSystem.Object obj)
		{
			bool flag = obj == null;
			byte[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				BinaryFormatter bf = new BinaryFormatter();
				MemoryStream ms = new MemoryStream();
				bf.Serialize(ms, obj);
				array = ms.ToArray();
			}
			return array;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0001C7F4 File Offset: 0x0001A9F4
		public static byte[] SerializeObjectToBytes(object obj)
		{
			bool flag = obj == null;
			byte[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				string json = new MicroJsonSerializer().Serialize(obj);
				array = Encoding.UTF8.GetBytes(json);
			}
			return array;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0001C82C File Offset: 0x0001AA2C
		internal static T DeserializeBytesToObject<T>(byte[] byteArray) where T : class
		{
			bool flag = byteArray == null || byteArray.Length == 0;
			T t;
			if (flag)
			{
				t = default(T);
			}
			else
			{
				string json = Encoding.UTF8.GetString(byteArray);
				bool flag2 = json.Contains("-Pad-");
				if (flag2)
				{
					json = json.Substring(0, json.IndexOf("-Pad-"));
				}
				t = new MicroJsonSerializer().Deserialize<T>(json);
			}
			return t;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001C894 File Offset: 0x0001AA94
		public static int lastIndexOfByteArray(byte[] source, byte[] pattern)
		{
			byte[] reversedSource = Serialization.reverseArray(source);
			byte[] reversedPattern = Serialization.reverseArray(pattern);
			int index = Serialization.IndexOfByteArray(reversedSource, reversedPattern);
			bool flag = index == -1;
			int num;
			if (flag)
			{
				num = -1;
			}
			else
			{
				num = source.Length - index - pattern.Length;
			}
			return num;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0001C8D8 File Offset: 0x0001AAD8
		public static byte[] reverseArray(byte[] array)
		{
			byte[] reversedArray = new byte[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				reversedArray[i] = array[array.Length - 1 - i];
			}
			return reversedArray;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001C914 File Offset: 0x0001AB14
		public static int IndexOfByteArray(byte[] source, byte[] pattern)
		{
			bool flag = source == null || pattern == null || source.Length == 0 || pattern.Length == 0 || source.Length < pattern.Length;
			int num;
			if (flag)
			{
				num = -1;
			}
			else
			{
				for (int i = 0; i <= source.Length - pattern.Length; i++)
				{
					bool flag2 = Serialization.Matches(source, i, pattern);
					if (flag2)
					{
						return i;
					}
				}
				num = -1;
			}
			return num;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001C978 File Offset: 0x0001AB78
		public static bool Matches(byte[] source, int startIndex, byte[] pattern)
		{
			for (int i = 0; i < pattern.Length; i++)
			{
				bool flag = source[startIndex + i] != pattern[i];
				if (flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001C9B4 File Offset: 0x0001ABB4
		internal static T IL2CPPFromByteArray<T>(byte[] data)
		{
			bool flag = data == null;
			T t;
			if (flag)
			{
				t = default(T);
			}
			else
			{
				Il2CppStructArray<byte> array = new Il2CppStructArray<byte>(data);
				object obj = new BinaryFormatter().Deserialize(new MemoryStream(data));
				t = (T)((object)obj);
			}
			return t;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001CA00 File Offset: 0x0001AC00
		internal static Il2CppArrayBase<T> FromIL2CPPToManaged<T>(global::Il2CppSystem.Object obj)
		{
			return Il2CppArrayBase<T>.WrapNativeGenericArrayPointer(obj.Pointer);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001CA10 File Offset: 0x0001AC10
		internal static global::Il2CppSystem.Object MakeObject(double item)
		{
			Double @double = default(Double);
			@double.m_value = item;
			return @double.BoxIl2CppObject() ?? null;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001CA3E File Offset: 0x0001AC3E
		internal static global::Il2CppSystem.Object MakeObject(byte[] arry)
		{
			return new global::Il2CppSystem.Object(new Il2CppStructArray<byte>(arry).Pointer);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001CA50 File Offset: 0x0001AC50
		internal static JObject ParseIl2CppObject(global::Il2CppSystem.Object obj)
		{
			JObject jobj;
			try
			{
				bool flag = obj != null;
				if (flag)
				{
					Type objType = obj.GetIl2CppType();
					jobj = new JObject();
					try
					{
						bool flag2 = objType == Il2CppType.Of<ParameterDictionary>();
						if (flag2)
						{
							jobj["type"] = "ParameterDictionary";
							ParameterDictionary hashtable = obj.Cast<ParameterDictionary>();
							IEnumerator enumerator3;
							if (hashtable == null)
							{
								enumerator3 = null;
							}
							else
							{
								NonAllocDictionary<byte, global::Il2CppSystem.Object> paramDict = hashtable.paramDict;
								enumerator3 = ((paramDict != null) ? paramDict.System_Collections_IEnumerable_GetEnumerator() : null);
							}
							IEnumerator enumerator = enumerator3;
							JObject jdata = new JObject();
							bool flag3 = enumerator != null;
							if (flag3)
							{
								while (enumerator.MoveNext())
								{
									global::Il2CppSystem.Object @object = enumerator.Current;
									DictionaryEntry entry = @object.Cast<DictionaryEntry>();
									jdata[entry.Key.ToString()] = Serialization.ParseIl2CppObject(entry.Value);
								}
								jobj["data"] = jdata;
							}
							else
							{
								jdata["data"] = "null";
							}
						}
						else
						{
							bool flag4 = objType == Il2CppType.Of<Hashtable>();
							if (flag4)
							{
								jobj["type"] = "HashTable";
								Hashtable hashtable2 = obj.Cast<Hashtable>();
								IEnumerator enumerator2 = hashtable2.System_Collections_IEnumerable_GetEnumerator();
								JObject jdata2 = new JObject();
								while (enumerator2.MoveNext())
								{
									global::Il2CppSystem.Object object2 = enumerator2.Current;
									DictionaryEntry entry2 = object2.Cast<DictionaryEntry>();
									jdata2[entry2.Key.ToString()] = Serialization.ParseIl2CppObject(entry2.Value);
								}
								jobj["data"] = jdata2;
							}
							else
							{
								bool flag5 = objType == Il2CppType.Of<Dictionary<string, global::Il2CppSystem.Object>>();
								if (flag5)
								{
									jobj["type"] = "Dictionary<String, Object>";
									Dictionary<string, global::Il2CppSystem.Object> dictionary = obj.Cast<Dictionary<string, global::Il2CppSystem.Object>>();
									JObject jdata3 = new JObject();
									foreach (KeyValuePair<string, global::Il2CppSystem.Object> entry3 in dictionary)
									{
										jdata3[entry3.Key] = Serialization.ParseIl2CppObject(entry3.Value);
									}
									jobj["data"] = jdata3;
								}
								else
								{
									bool flag6 = objType == Il2CppType.Of<Dictionary<byte, global::Il2CppSystem.Object>>();
									if (flag6)
									{
										jobj["type"] = "Dictionary<Byte, Object>";
										Dictionary<byte, global::Il2CppSystem.Object> dictionary2 = obj.Cast<Dictionary<byte, global::Il2CppSystem.Object>>();
										JObject jdata4 = new JObject();
										foreach (KeyValuePair<byte, global::Il2CppSystem.Object> entry4 in dictionary2)
										{
											jdata4[entry4.Key.ToString()] = Serialization.ParseIl2CppObject(entry4.Value);
										}
										jobj["data"] = jdata4;
									}
									else
									{
										bool flag7 = objType == Il2CppType.Of<DictionaryEntry>();
										if (flag7)
										{
											Type type = obj.GetType();
											DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 7);
											defaultInterpolatedStringHandler.AppendFormatted<global::Il2CppSystem.Object>(obj);
											defaultInterpolatedStringHandler.AppendLiteral(", ");
											defaultInterpolatedStringHandler.AppendFormatted<Type>(type);
											defaultInterpolatedStringHandler.AppendLiteral(", ");
											defaultInterpolatedStringHandler.AppendFormatted(type.Name);
											defaultInterpolatedStringHandler.AppendLiteral(", ");
											defaultInterpolatedStringHandler.AppendFormatted(type.FullName);
											defaultInterpolatedStringHandler.AppendLiteral(", ");
											defaultInterpolatedStringHandler.AppendFormatted<Type>(objType);
											defaultInterpolatedStringHandler.AppendLiteral(", ");
											defaultInterpolatedStringHandler.AppendFormatted(objType.Name);
											defaultInterpolatedStringHandler.AppendLiteral(", ");
											defaultInterpolatedStringHandler.AppendFormatted(objType.FullName);
											CLog.D(defaultInterpolatedStringHandler.ToStringAndClear(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\Serialization.cs", 197);
											jobj["type"] = "DictionaryEntry";
											DictionaryEntry entry5 = obj.Cast<DictionaryEntry>();
											jobj["data"] = new JObject
											{
												new JProperty("key", Serialization.ParseIl2CppObject(entry5.Key)),
												new JProperty("value", Serialization.ParseIl2CppObject(entry5.Key))
											};
										}
										else
										{
											bool flag8 = objType == Il2CppType.Of<ArraySegment<byte>>();
											if (flag8)
											{
												jobj["type"] = "ArraySegment<Byte>";
												byte[] arraySeg = obj.Cast<ArraySegment<byte>>().Array;
												JObject jobject = jobj;
												string text = "data";
												JObject jobject2 = new JObject();
												jobject2.Add(new JProperty("String", arraySeg.Join((byte x) => Convert.ToChar(x), "")));
												jobject2.Add(new JProperty("Base64", Convert.ToBase64String(arraySeg)));
												jobject2.Add(new JProperty("ByteData", arraySeg.EzByteConvert<byte>()));
												jobject[text] = jobject2;
											}
											else
											{
												string name = objType.Name;
												string fullname = objType.FullName;
												bool flag9 = fullname.StartsWith("System.");
												if (flag9)
												{
													jobj["type"] = fullname;
													string text2 = name;
													string text3 = text2;
													uint num = <PrivateImplementationDetails>.ComputeStringHash(text3);
													if (num <= 2386971688U)
													{
														if (num <= 760156106U)
														{
															if (num != 498965336U)
															{
																if (num == 760156106U)
																{
																	if (text3 == "Object[]")
																	{
																		JArray jsonObjectArray = new JArray();
																		foreach (global::Il2CppSystem.Object object3 in obj.Cast<IEnumerable>())
																		{
																			jsonObjectArray.Add(Serialization.ParseIl2CppObject(object3));
																		}
																		jobj["data"] = jsonObjectArray;
																		goto IL_096F;
																	}
																}
															}
															else if (text3 == "String[][]")
															{
																JArray jsonStringArrayArray = new JArray();
																CLog.D(obj.ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\Serialization.cs", 266);
																IEnumerator stringArrayArrayEnumerator = obj.Cast<IEnumerable>().GetEnumerator();
																while (stringArrayArrayEnumerator.MoveNext())
																{
																	JArray jsonInnerStringArray = new JArray();
																	Il2CppStringArray innerStringArrayEnumerator = stringArrayArrayEnumerator.Current.Cast<Il2CppStringArray>();
																	CLog.D(innerStringArrayEnumerator.Length.ToString(), false, "C:\\Users\\jshil\\Downloads\\C#\\!Hexed-Base\\!EXO-Internal-Base\\EXO\\Functions\\Serialization.cs", 273);
																	foreach (string str in innerStringArrayEnumerator)
																	{
																		jsonInnerStringArray.Add(str);
																	}
																	jsonStringArrayArray.Add(jsonInnerStringArray);
																}
																jobj["data"] = jsonStringArrayArray;
																goto IL_096F;
															}
														}
														else if (num != 1615808600U)
														{
															if (num != 1977367276U)
															{
																if (num == 2386971688U)
																{
																	if (text3 == "Double")
																	{
																		jobj["data"] = Convert.ToDouble(obj);
																		goto IL_096F;
																	}
																}
															}
															else if (text3 == "String[]")
															{
																JArray jsonStringArray = new JArray();
																foreach (global::Il2CppSystem.Object object4 in obj.Cast<IEnumerable>())
																{
																	jsonStringArray.Add(Convert.ToString(object4));
																}
																jobj["data"] = jsonStringArray;
																goto IL_096F;
															}
														}
														else if (text3 == "String")
														{
															jobj["data"] = Convert.ToString(obj);
															goto IL_096F;
														}
													}
													else if (num <= 2711245919U)
													{
														if (num != 2642490659U)
														{
															if (num == 2711245919U)
															{
																if (text3 == "Int32")
																{
																	jobj["data"] = Convert.ToInt32(obj);
																	goto IL_096F;
																}
															}
														}
														else if (text3 == "Byte[]")
														{
															byte[] Bytes = Il2CppArrayBase<byte>.WrapNativeGenericArrayPointer(obj.Pointer);
															JObject jobject3 = jobj;
															string text4 = "data";
															JObject jobject4 = new JObject();
															jobject4.Add(new JProperty("String", Bytes.Join((byte x) => Convert.ToChar(x), "")));
															jobject4.Add(new JProperty("Base64", Convert.ToBase64String(Bytes)));
															jobject4.Add(new JProperty("ByteData", Bytes.EzByteConvert<byte>()));
															jobject3[text4] = jobject4;
															goto IL_096F;
														}
													}
													else if (num != 3409549631U)
													{
														if (num != 3646816451U)
														{
															if (num == 3969205087U)
															{
																if (text3 == "Boolean")
																{
																	jobj["data"] = Convert.ToBoolean(obj);
																	goto IL_096F;
																}
															}
														}
														else if (text3 == "Int32[]")
														{
															JArray int32Array = new JArray();
															foreach (global::Il2CppSystem.Object object5 in obj.Cast<IEnumerable>())
															{
																int32Array.Add(Convert.ToInt32(object5));
															}
															jobj["data"] = int32Array;
															goto IL_096F;
														}
													}
													else if (text3 == "Byte")
													{
														jobj["data"] = Convert.ToByte(obj);
														goto IL_096F;
													}
													jobj["type"] = fullname;
													jobj["data"] = obj.ToString();
													IL_096F:;
												}
												else
												{
													jobj["type"] = fullname;
													jobj["data"] = obj.ToString();
												}
											}
										}
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						bool flag10 = !jobj.ContainsKey("type");
						if (flag10)
						{
							jobj["type"] = objType.FullName;
						}
						jobj["exception"] = ex.ToString();
					}
				}
				else
				{
					jobj = new JObject(new object[]
					{
						new JProperty("type", "Object"),
						new JProperty("data", "null")
					});
				}
			}
			catch (Exception ex2)
			{
				jobj = new JObject(new object[]
				{
					new JProperty("type", "Object"),
					new JProperty("exception", ex2.ToString())
				});
			}
			return jobj;
		}
	}
}
