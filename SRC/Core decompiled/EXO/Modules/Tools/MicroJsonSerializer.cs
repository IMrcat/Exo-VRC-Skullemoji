using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace EXO.Modules.Tools
{
	// Token: 0x0200007F RID: 127
	public class MicroJsonSerializer
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x00019B08 File Offset: 0x00017D08
		public static T DeserializeObject<T>(string text)
		{
			return new MicroJsonSerializer().Deserialize<T>(text);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00019B28 File Offset: 0x00017D28
		public static string SerializeObject(object obj)
		{
			return new MicroJsonSerializer().Serialize(obj);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00019B48 File Offset: 0x00017D48
		public T Deserialize<T>(string text)
		{
			return this.Deserialize<T>(text, new JsonParser());
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00019B68 File Offset: 0x00017D68
		public T Deserialize<T>(string text, JsonParser parser)
		{
			bool flag = parser == null;
			if (flag)
			{
				throw new ArgumentException("An invalid argument was specified.", "parser");
			}
			object o = parser.Parse(text);
			return (T)((object)this.Deserialize(o, typeof(T)));
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00019BB1 File Offset: 0x00017DB1
		public MicroJsonSerializer()
		{
			this.TypeInfoPropertyName = "@type";
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00019BE8 File Offset: 0x00017DE8
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x00019BF0 File Offset: 0x00017DF0
		public bool UseTypeInfo { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00019BF9 File Offset: 0x00017DF9
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x00019C01 File Offset: 0x00017E01
		public string TypeInfoPropertyName { get; set; }

		// Token: 0x060004A6 RID: 1190 RVA: 0x00019C0C File Offset: 0x00017E0C
		public T Deserialize<T>(object value)
		{
			return (T)((object)this.Deserialize(value, typeof(T)));
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00019C34 File Offset: 0x00017E34
		private object Deserialize(object from, Type type)
		{
			bool flag = from == null;
			object obj;
			if (flag)
			{
				obj = null;
			}
			else
			{
				bool flag2 = type == typeof(byte[]);
				if (flag2)
				{
					bool flag3 = from is string;
					if (!flag3)
					{
						throw new InvalidOperationException("Invalid format for byte array.");
					}
					obj = Convert.FromBase64String(from as string);
				}
				else
				{
					IDictionary<string, object> dict = from as IDictionary<string, object>;
					bool flag4 = dict != null;
					if (flag4)
					{
						bool useTypeInfo = this.UseTypeInfo;
						if (useTypeInfo)
						{
							object typeNameObject;
							bool flag5 = dict.TryGetValue(this.TypeInfoPropertyName, ref typeNameObject);
							if (flag5)
							{
								string typeName = typeNameObject as string;
								bool flag6 = !string.IsNullOrEmpty(typeName);
								if (flag6)
								{
									Type derivedType;
									bool flag7 = !this.TypeCache.TryGetValue(typeName, ref derivedType);
									if (flag7)
									{
										derivedType = Enumerable.FirstOrDefault<Type>(type.Assembly.GetTypes(), (Type t) => t != type && type.IsAssignableFrom(t) && string.Equals(t.Name, typeName, 5));
										this.TypeCache[typeName] = derivedType ?? typeof(object);
									}
									bool flag8 = derivedType != null && derivedType != typeof(object);
									if (flag8)
									{
										type = derivedType;
									}
								}
							}
						}
						object to = Activator.CreateInstance(type);
						this.DeserializeDictionary(dict, to);
						obj = to;
					}
					else
					{
						IList list = from as IList;
						bool flag9 = list != null;
						if (flag9)
						{
							bool isArray = type.IsArray;
							if (isArray)
							{
								Type elementType = type.GetElementType();
								Array arr = Array.CreateInstance(elementType, list.Count);
								this.DeserializeArray(list, arr, elementType);
								obj = arr;
							}
							else
							{
								IList to2 = (IList)Activator.CreateInstance(type);
								this.DeserializeList(list, to2);
								obj = to2;
							}
						}
						else
						{
							bool flag10 = typeof(IList).IsAssignableFrom(type);
							if (flag10)
							{
								IList to3 = (IList)Activator.CreateInstance(type);
								Type itemType = to3.GetType().GetProperty("BringItem").PropertyType;
								to3.Add(this.Deserialize(from, itemType));
								obj = to3;
							}
							else
							{
								bool isEnum = type.IsEnum;
								if (isEnum)
								{
									obj = Enum.Parse(type, from.ToString(), true);
								}
								else
								{
									bool flag11 = type == typeof(DateTime);
									if (flag11)
									{
										string date = from as string;
										bool flag12 = date != null;
										if (flag12)
										{
											Match dateTimeMatch = MicroJsonSerializer.DateTimeRegex.Match(date);
											bool success = dateTimeMatch.Success;
											if (success)
											{
												long ticks = long.Parse(dateTimeMatch.Groups[1].Value, NumberFormatInfo.InvariantInfo);
												long epochTicks = ticks * 10000L + 621355968000000000L;
												return new DateTime(epochTicks, 1).ToLocalTime();
											}
										}
									}
									bool flag13 = type == typeof(Guid);
									if (flag13)
									{
										string guid = from as string;
										bool flag14 = guid != null;
										if (flag14)
										{
											Guid g;
											bool flag15 = Guid.TryParse(guid, ref g);
											if (flag15)
											{
												return g;
											}
										}
									}
									bool flag16 = type == typeof(Uri);
									if (flag16)
									{
										string uri = from as string;
										bool flag17 = uri != null;
										if (flag17)
										{
											Uri u;
											bool flag18 = Uri.TryCreate(uri, 0, ref u);
											if (flag18)
											{
												return u;
											}
										}
									}
									bool flag19 = !type.IsAssignableFrom(from.GetType());
									if (flag19)
									{
										bool flag20 = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable);
										if (flag20)
										{
											type = type.GetGenericArguments()[0];
										}
										obj = Convert.ChangeType(from, type, CultureInfo.InvariantCulture);
									}
									else
									{
										obj = from;
									}
								}
							}
						}
					}
				}
			}
			return obj;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0001A098 File Offset: 0x00018298
		private void DeserializeArray(IList from, Array to, Type itemType)
		{
			for (int i = 0; i < from.Count; i++)
			{
				to.SetValue(this.Deserialize(from[i], itemType), i);
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001A0D4 File Offset: 0x000182D4
		private void DeserializeList(IList from, IList to)
		{
			Type itemType = to.GetType().GetProperty("BringItem").PropertyType;
			foreach (object item in from)
			{
				to.Add(this.Deserialize(item, itemType));
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001A148 File Offset: 0x00018348
		private void DeserializeDictionary(IEnumerable<KeyValuePair<string, object>> from, object to)
		{
			Type type = to.GetType();
			IDictionary dict = to as IDictionary;
			bool flag = dict != null;
			if (flag)
			{
				Type valType = typeof(object);
				while (type != typeof(object))
				{
					bool isGenericType = type.IsGenericType;
					if (isGenericType)
					{
						valType = type.GetGenericArguments()[1];
						break;
					}
					type = type.BaseType;
				}
				foreach (KeyValuePair<string, object> pair in from)
				{
					dict[pair.Key] = this.Deserialize(pair.Value, valType);
				}
			}
			else
			{
				foreach (KeyValuePair<string, object> pair2 in from)
				{
					MicroJsonSerializer.SetterMember member = this.GetMember(type, pair2.Key);
					bool flag2 = member != null;
					if (flag2)
					{
						member.Set.Invoke(to, this.Deserialize(pair2.Value, member.Type));
					}
				}
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001A28C File Offset: 0x0001848C
		private MicroJsonSerializer.SetterMember GetMember(Type type, string name)
		{
			string key = name + type.GetHashCode().ToString();
			MicroJsonSerializer.SetterMember member;
			bool flag = !this.MemberCache.TryGetValue(key, ref member);
			if (flag)
			{
				FieldInfo fieldInfo = type.GetField(name, 21);
				bool flag2 = fieldInfo != null;
				if (flag2)
				{
					member = new MicroJsonSerializer.SetterMember
					{
						Type = fieldInfo.FieldType,
						Set = delegate(object o, object v)
						{
							fieldInfo.SetValue(o, v);
						}
					};
					this.MemberCache[key] = member;
				}
				else
				{
					PropertyInfo propertyInfo = type.GetProperty(name, 21);
					bool flag3 = propertyInfo != null && propertyInfo.CanWrite;
					if (flag3)
					{
						member = new MicroJsonSerializer.SetterMember
						{
							Type = propertyInfo.PropertyType,
							Set = delegate(object o, object v)
							{
								propertyInfo.SetValue(o, v, null);
							}
						};
						this.MemberCache[key] = member;
					}
					else
					{
						this.MemberCache[key] = null;
					}
				}
			}
			return member;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001A3C8 File Offset: 0x000185C8
		public string Serialize(object obj)
		{
			bool flag = obj == null;
			string text;
			if (flag)
			{
				text = "null";
			}
			else
			{
				IList list = obj as IList;
				bool flag2 = list != null && !(obj is IEnumerable<KeyValuePair<string, object>>);
				if (flag2)
				{
					StringBuilder sb = new StringBuilder("[");
					bool flag3 = list.Count > 0;
					if (flag3)
					{
						sb.Append(string.Join(",", Enumerable.ToArray<string>(Enumerable.Select<object, string>(Enumerable.Cast<object>(list), (object i) => this.Serialize(i)))));
					}
					sb.Append("]");
					text = sb.ToString();
				}
				else
				{
					string str = obj as string;
					bool flag4 = str != null;
					if (flag4)
					{
						text = "\"" + MicroJsonSerializer.EscapeString(str) + "\"";
					}
					else
					{
						bool flag5 = obj is int;
						if (flag5)
						{
							text = obj.ToString();
						}
						else
						{
							bool? b = obj as bool?;
							bool flag6 = b != null;
							if (flag6)
							{
								text = (b.Value ? "true" : "false");
							}
							else
							{
								bool flag7 = obj is decimal;
								if (flag7)
								{
									text = ((IFormattable)obj).ToString("G", NumberFormatInfo.InvariantInfo);
								}
								else
								{
									bool flag8 = obj is double || obj is float;
									if (flag8)
									{
										text = ((IFormattable)obj).ToString("R", NumberFormatInfo.InvariantInfo);
									}
									else
									{
										bool flag9 = obj is Enum;
										if (flag9)
										{
											text = "\"" + MicroJsonSerializer.EscapeString(obj.ToString()) + "\"";
										}
										else
										{
											bool flag10 = obj is char;
											if (flag10)
											{
												text = "\"" + ((obj != null) ? obj.ToString() : null) + "\"";
											}
											else
											{
												bool isPrimitive = obj.GetType().IsPrimitive;
												if (isPrimitive)
												{
													text = (string)Convert.ChangeType(obj, typeof(string), CultureInfo.InvariantCulture);
												}
												else
												{
													bool flag11 = obj is DateTime;
													if (flag11)
													{
														text = MicroJsonSerializer.SerializeDateTime(obj);
													}
													else
													{
														bool flag12 = obj is Guid;
														if (flag12)
														{
															text = "\"" + ((obj != null) ? obj.ToString() : null) + "\"";
														}
														else
														{
															bool flag13 = obj is Uri;
															if (flag13)
															{
																text = "\"" + ((obj != null) ? obj.ToString() : null) + "\"";
															}
															else
															{
																text = this.SerializeComplexType(obj);
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0001A670 File Offset: 0x00018870
		private static string SerializeDateTime(object o)
		{
			return "\"\\/Date(" + ((((DateTime)o).ToUniversalTime().Ticks - 621355968000000000L) / 10000L).ToString() + ")\\/\"";
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001A6C0 File Offset: 0x000188C0
		private static string EscapeString(string src)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < src.Length; i++)
			{
				char c = src.get_Chars(i);
				bool flag = c == '"' || c == '\\';
				if (flag)
				{
					sb.Append('\\');
					sb.Append(c);
				}
				else
				{
					bool flag2 = c < ' ';
					if (flag2)
					{
						int u = (int)c;
						switch (u)
						{
						case 8:
							sb.Append("\\b");
							break;
						case 9:
							sb.Append("\\t");
							break;
						case 10:
							sb.Append("\\n");
							break;
						case 11:
							goto IL_00C8;
						case 12:
							sb.Append("\\f");
							break;
						case 13:
							sb.Append("\\r");
							break;
						default:
							goto IL_00C8;
						}
						goto IL_00F9;
						IL_00C8:
						sb.Append("\\u");
						sb.Append(u.ToString("X4", NumberFormatInfo.InvariantInfo));
					}
					else
					{
						sb.Append(c);
					}
				}
				IL_00F9:;
			}
			return sb.ToString();
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0001A7E4 File Offset: 0x000189E4
		private string SerializeComplexType(object o)
		{
			StringBuilder s = new StringBuilder("{");
			bool flag = o is IDictionary || o is IEnumerable<KeyValuePair<string, object>>;
			if (flag)
			{
				this.SerializeDictionary(o, s);
			}
			else
			{
				this.SerializeProperties(o, s);
			}
			s.Append("}");
			return s.ToString();
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001A844 File Offset: 0x00018A44
		private void SerializeProperties(object o, StringBuilder s)
		{
			Type type = o.GetType();
			MicroJsonSerializer.GetterMember[] members = this.GetMembers(type);
			bool flag = this.UseTypeInfo && ((type.BaseType != typeof(object) && type.BaseType != null) || Enumerable.Any<Type>(type.GetInterfaces()));
			if (flag)
			{
				s.Append("\"");
				s.Append(this.TypeInfoPropertyName);
				s.Append("\":\"");
				s.Append(type.Name);
				s.Append("\",");
			}
			foreach (MicroJsonSerializer.GetterMember member in members)
			{
				object val = member.Get.Invoke(o);
				bool flag2 = val != null && (member.DefaultValue == null || !val.Equals(member.DefaultValue));
				if (flag2)
				{
					string v = this.Serialize(val);
					s.Append("\"");
					s.Append(member.Name);
					s.Append("\":");
					s.Append(v);
					s.Append(",");
				}
			}
			bool flag3 = s.Length > 0 && s.get_Chars(s.Length - 1) == ',';
			if (flag3)
			{
				s.Remove(s.Length - 1, 1);
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0001A9BC File Offset: 0x00018BBC
		private void SerializeDictionary(object o, StringBuilder s)
		{
			IDictionary dict = o as IDictionary;
			bool flag = dict != null;
			IEnumerable<KeyValuePair<string, object>> kvps;
			if (flag)
			{
				kvps = Enumerable.Select<object, KeyValuePair<string, object>>(Enumerable.Cast<object>(dict.Keys), (object k) => new KeyValuePair<string, object>(k.ToString(), dict[k]));
			}
			else
			{
				kvps = (IEnumerable<KeyValuePair<string, object>>)o;
			}
			List<KeyValuePair<string, object>> kvpList = Enumerable.ToList<KeyValuePair<string, object>>(kvps);
			kvpList.Sort((KeyValuePair<string, object> e1, KeyValuePair<string, object> e2) => string.Compare(e1.Key, e2.Key, 5));
			foreach (KeyValuePair<string, object> kvp in kvpList)
			{
				s.Append("\"");
				s.Append(kvp.Key);
				s.Append("\":");
				s.Append(this.Serialize(kvp.Value));
				s.Append(",");
			}
			bool flag2 = s.Length > 0 && s.get_Chars(s.Length - 1) == ',';
			if (flag2)
			{
				s.Remove(s.Length - 1, 1);
			}
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001AAFC File Offset: 0x00018CFC
		private MicroJsonSerializer.GetterMember[] GetMembers(Type type)
		{
			MicroJsonSerializer.GetterMember[] members;
			bool flag = !this.MembersCache.TryGetValue(type, ref members);
			if (flag)
			{
				IEnumerable<MicroJsonSerializer.GetterMember> props = Enumerable.Select<PropertyInfo, MicroJsonSerializer.GetterMember>(Enumerable.Where<PropertyInfo>(type.GetProperties(21), (PropertyInfo p) => p.CanWrite), (PropertyInfo p) => MicroJsonSerializer.BuildGetterMember(p));
				IEnumerable<MicroJsonSerializer.GetterMember> fields = Enumerable.Select<FieldInfo, MicroJsonSerializer.GetterMember>(type.GetFields(21), (FieldInfo f) => MicroJsonSerializer.BuildGetterMember(f));
				members = Enumerable.ToArray<MicroJsonSerializer.GetterMember>(Enumerable.OrderBy<MicroJsonSerializer.GetterMember, string>(Enumerable.Concat<MicroJsonSerializer.GetterMember>(props, fields), (MicroJsonSerializer.GetterMember g) => g.Name, StringComparer.OrdinalIgnoreCase));
				this.MembersCache[type] = members;
			}
			return members;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001ABF0 File Offset: 0x00018DF0
		private static MicroJsonSerializer.GetterMember BuildGetterMember(PropertyInfo p)
		{
			DefaultValueAttribute defaultAttribute = Enumerable.FirstOrDefault<object>(p.GetCustomAttributes(typeof(DefaultValueAttribute), true)) as DefaultValueAttribute;
			return new MicroJsonSerializer.GetterMember
			{
				Name = p.Name,
				Get = (object o) => p.GetValue(o, null),
				DefaultValue = ((defaultAttribute != null) ? defaultAttribute.Value : MicroJsonSerializer.GetDefaultValueForType(p.PropertyType))
			};
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001AC7C File Offset: 0x00018E7C
		private static MicroJsonSerializer.GetterMember BuildGetterMember(FieldInfo f)
		{
			DefaultValueAttribute defaultAttribute = Enumerable.FirstOrDefault<object>(f.GetCustomAttributes(typeof(DefaultValueAttribute), true)) as DefaultValueAttribute;
			return new MicroJsonSerializer.GetterMember
			{
				Name = f.Name,
				Get = (object o) => f.GetValue(o),
				DefaultValue = ((defaultAttribute != null) ? defaultAttribute.Value : MicroJsonSerializer.GetDefaultValueForType(f.FieldType))
			};
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001AD08 File Offset: 0x00018F08
		private static object GetDefaultValueForType(Type type)
		{
			return type.IsValueType ? Activator.CreateInstance(type) : null;
		}

		// Token: 0x04000216 RID: 534
		private static Regex DateTimeRegex = new Regex("^/Date\\((-?\\d+)\\)/$");

		// Token: 0x04000217 RID: 535
		private Dictionary<string, Type> TypeCache = new Dictionary<string, Type>();

		// Token: 0x04000218 RID: 536
		private Dictionary<string, MicroJsonSerializer.SetterMember> MemberCache = new Dictionary<string, MicroJsonSerializer.SetterMember>();

		// Token: 0x04000219 RID: 537
		private Dictionary<Type, MicroJsonSerializer.GetterMember[]> MembersCache = new Dictionary<Type, MicroJsonSerializer.GetterMember[]>();

		// Token: 0x02000169 RID: 361
		private class SetterMember
		{
			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0003170A File Offset: 0x0002F90A
			// (set) Token: 0x06000A54 RID: 2644 RVA: 0x00031712 File Offset: 0x0002F912
			public Type Type { get; set; }

			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0003171B File Offset: 0x0002F91B
			// (set) Token: 0x06000A56 RID: 2646 RVA: 0x00031723 File Offset: 0x0002F923
			public Action<object, object> Set { get; set; }
		}

		// Token: 0x0200016A RID: 362
		private class GetterMember
		{
			// Token: 0x170001B3 RID: 435
			// (get) Token: 0x06000A58 RID: 2648 RVA: 0x00031735 File Offset: 0x0002F935
			// (set) Token: 0x06000A59 RID: 2649 RVA: 0x0003173D File Offset: 0x0002F93D
			public string Name { get; set; }

			// Token: 0x170001B4 RID: 436
			// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00031746 File Offset: 0x0002F946
			// (set) Token: 0x06000A5B RID: 2651 RVA: 0x0003174E File Offset: 0x0002F94E
			public Func<object, object> Get { get; set; }

			// Token: 0x170001B5 RID: 437
			// (get) Token: 0x06000A5C RID: 2652 RVA: 0x00031757 File Offset: 0x0002F957
			// (set) Token: 0x06000A5D RID: 2653 RVA: 0x0003175F File Offset: 0x0002F95F
			public object DefaultValue { get; set; }
		}
	}
}
