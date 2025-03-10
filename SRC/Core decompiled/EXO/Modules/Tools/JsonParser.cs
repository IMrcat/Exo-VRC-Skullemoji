using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EXO.Modules.Tools
{
	// Token: 0x0200007E RID: 126
	public class JsonParser
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00018EC6 File Offset: 0x000170C6
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x00018ECE File Offset: 0x000170CE
		private string Input { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00018ED7 File Offset: 0x000170D7
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x00018EDF File Offset: 0x000170DF
		private int InputLength { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x00018EE8 File Offset: 0x000170E8
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x00018EF0 File Offset: 0x000170F0
		private int Pos { get; set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00018EF9 File Offset: 0x000170F9
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x00018F01 File Offset: 0x00017101
		private int Line { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00018F0A File Offset: 0x0001710A
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x00018F12 File Offset: 0x00017112
		private int Col { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00018F1B File Offset: 0x0001711B
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x00018F23 File Offset: 0x00017123
		public ILogger Logger { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00018F2C File Offset: 0x0001712C
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x00018F34 File Offset: 0x00017134
		public bool CollectLineInfo { get; set; }

		// Token: 0x0600048A RID: 1162 RVA: 0x00018F40 File Offset: 0x00017140
		public object Parse(string text)
		{
			bool flag = text == null;
			if (flag)
			{
				throw this.BuildParserException("input is null");
			}
			this.Input = text;
			this.InputLength = text.Length;
			this.Pos = 0;
			this.Line = 1;
			this.Col = 1;
			object o = this.Value();
			this.SkipWhitespace();
			bool flag2 = this.Pos != this.InputLength;
			if (flag2)
			{
				throw this.BuildParserException("extra characters at end");
			}
			return o;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00018FC4 File Offset: 0x000171C4
		private void WriteLineLog(string msg, params object[] args)
		{
			bool flag = this.Logger != null;
			if (flag)
			{
				this.Logger.WriteLine(msg, args);
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00018FF0 File Offset: 0x000171F0
		private ParserException BuildParserException(string msg)
		{
			bool collectLineInfo = this.CollectLineInfo;
			ParserException ex;
			if (collectLineInfo)
			{
				ex = new ParserException(string.Format(CultureInfo.InvariantCulture, "Parse error: {0} at line {1}, column {2}.", msg, this.Line, this.Col), this.Line, this.Col);
			}
			else
			{
				ex = new ParserException("Parse error: " + msg + ".", 0, 0);
			}
			return ex;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00019060 File Offset: 0x00017260
		private void AdvanceInput(int n)
		{
			bool collectLineInfo = this.CollectLineInfo;
			if (collectLineInfo)
			{
				for (int i = this.Pos; i < this.Pos + n; i++)
				{
					char c = this.Input.get_Chars(i);
					bool flag = c == '\n';
					if (flag)
					{
						int num = this.Line;
						this.Line = num + 1;
						this.Col = 1;
					}
					else
					{
						int num = this.Col;
						this.Col = num + 1;
					}
				}
			}
			this.Pos += n;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x000190F4 File Offset: 0x000172F4
		private string Accept(string s)
		{
			int len = s.Length;
			bool flag = this.Pos + len > this.InputLength;
			string text;
			if (flag)
			{
				text = null;
			}
			else
			{
				bool flag2 = this.Input.IndexOf(s, this.Pos, len, 4) != -1;
				if (flag2)
				{
					string match = this.Input.Substring(this.Pos, len);
					this.AdvanceInput(len);
					text = match;
				}
				else
				{
					text = null;
				}
			}
			return text;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00019168 File Offset: 0x00017368
		private char Expect(char c)
		{
			bool flag = this.Pos >= this.InputLength || this.Input.get_Chars(this.Pos) != c;
			if (flag)
			{
				ReadOnlySpan<char> readOnlySpan = "expected '";
				char c2 = c;
				ReadOnlySpan<char> readOnlySpan2 = new ReadOnlySpan<char>(ref c2);
				char c3 = '\'';
				throw this.BuildParserException(readOnlySpan + readOnlySpan2 + new ReadOnlySpan<char>(ref c3));
			}
			this.AdvanceInput(1);
			return c;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x000191DC File Offset: 0x000173DC
		private object Value()
		{
			this.SkipWhitespace();
			bool flag = this.Pos >= this.InputLength;
			if (flag)
			{
				throw this.BuildParserException("input contains no value");
			}
			char nextChar = this.Input.get_Chars(this.Pos);
			bool flag2 = nextChar == '"';
			object obj;
			if (flag2)
			{
				this.AdvanceInput(1);
				obj = this.String();
			}
			else
			{
				bool flag3 = nextChar == '[';
				if (flag3)
				{
					this.AdvanceInput(1);
					obj = this.List();
				}
				else
				{
					bool flag4 = nextChar == '{';
					if (flag4)
					{
						this.AdvanceInput(1);
						obj = this.Dictionary();
					}
					else
					{
						bool flag5 = char.IsDigit(nextChar) || nextChar == '-';
						if (flag5)
						{
							obj = this.Number();
						}
						else
						{
							obj = this.Literal();
						}
					}
				}
			}
			return obj;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000192A8 File Offset: 0x000174A8
		private object Number()
		{
			int currentPos = this.Pos;
			bool dotSeen = false;
			this.Accept((char c) => c == '-', ref currentPos);
			this.ExpectDigits(ref currentPos);
			bool flag = this.Accept((char c) => c == '.', ref currentPos);
			if (flag)
			{
				dotSeen = true;
				this.ExpectDigits(ref currentPos);
			}
			bool flag2 = this.Accept((char c) => c == 'e' || c == 'E', ref currentPos);
			if (flag2)
			{
				this.Accept((char c) => c == '-' || c == '+', ref currentPos);
				this.ExpectDigits(ref currentPos);
			}
			int len = currentPos - this.Pos;
			string num = this.Input.Substring(this.Pos, len);
			bool flag3 = dotSeen;
			object obj;
			if (flag3)
			{
				decimal d;
				bool flag4 = decimal.TryParse(num, 164, CultureInfo.InvariantCulture, ref d);
				if (flag4)
				{
					this.WriteLineLog("decimal: {0}", new object[] { d });
					this.AdvanceInput(len);
					obj = d;
				}
				else
				{
					double dbl;
					bool flag5 = double.TryParse(num, 164, CultureInfo.InvariantCulture, ref dbl);
					if (!flag5)
					{
						throw this.BuildParserException("cannot parse decimal number");
					}
					this.WriteLineLog("double: {0}", new object[] { dbl });
					this.AdvanceInput(len);
					obj = dbl;
				}
			}
			else
			{
				int i;
				bool flag6 = int.TryParse(num, 132, CultureInfo.InvariantCulture, ref i);
				if (flag6)
				{
					this.WriteLineLog("int: {0}", new object[] { i });
					this.AdvanceInput(len);
					obj = i;
				}
				else
				{
					long j;
					bool flag7 = long.TryParse(num, 132, CultureInfo.InvariantCulture, ref j);
					if (!flag7)
					{
						throw this.BuildParserException("cannot parse integer number");
					}
					this.WriteLineLog("long: {0}", new object[] { j });
					this.AdvanceInput(len);
					obj = j;
				}
			}
			return obj;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x000194F4 File Offset: 0x000176F4
		private bool Accept(Predicate<char> predicate, ref int pos)
		{
			bool flag = pos < this.InputLength && predicate.Invoke(this.Input.get_Chars(pos));
			bool flag2;
			if (flag)
			{
				pos++;
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00019538 File Offset: 0x00017738
		private void ExpectDigits(ref int pos)
		{
			int start = pos;
			while (pos < this.InputLength && char.IsDigit(this.Input.get_Chars(pos)))
			{
				pos++;
			}
			bool flag = start == pos;
			if (flag)
			{
				throw this.BuildParserException("not a number");
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001958C File Offset: 0x0001778C
		private string String()
		{
			int currentPos = this.Pos;
			StringBuilder sb = new StringBuilder();
			for (;;)
			{
				bool flag = currentPos >= this.InputLength;
				if (flag)
				{
					break;
				}
				char c = this.Input.get_Chars(currentPos);
				bool flag2 = c == '"';
				if (flag2)
				{
					goto Block_2;
				}
				bool flag3 = c == '\\';
				if (flag3)
				{
					currentPos++;
					bool flag4 = currentPos >= this.InputLength;
					if (flag4)
					{
						goto Block_4;
					}
					c = this.Input.get_Chars(currentPos);
					char c2 = c;
					char c3 = c2;
					if (c3 <= '\\')
					{
						if (c3 != '"' && c3 != '/' && c3 != '\\')
						{
							goto Block_8;
						}
						sb.Append(c);
					}
					else if (c3 <= 'f')
					{
						if (c3 != 'b')
						{
							if (c3 != 'f')
							{
								goto Block_11;
							}
							sb.Append('\f');
						}
						else
						{
							sb.Append('\b');
						}
					}
					else
					{
						if (c3 != 'n')
						{
							switch (c3)
							{
							case 'r':
								sb.Append('\r');
								goto IL_01E6;
							case 't':
								sb.Append('\t');
								goto IL_01E6;
							case 'u':
							{
								currentPos += 4;
								bool flag5 = currentPos >= this.InputLength;
								if (flag5)
								{
									goto Block_14;
								}
								int u;
								bool flag6 = !int.TryParse(this.Input.Substring(currentPos - 3, 4), 512, NumberFormatInfo.InvariantInfo, ref u);
								if (flag6)
								{
									goto Block_15;
								}
								sb.Append((char)u);
								goto IL_01E6;
							}
							}
							goto Block_13;
						}
						sb.Append('\n');
					}
					IL_01E6:;
				}
				else
				{
					bool flag7 = c < ' ';
					if (flag7)
					{
						goto Block_16;
					}
					sb.Append(c);
				}
				currentPos++;
			}
			throw this.BuildParserException("unterminated string");
			Block_2:
			int len = currentPos - this.Pos;
			this.AdvanceInput(len + 1);
			this.WriteLineLog("string: {0}", new object[] { sb });
			return sb.ToString();
			Block_4:
			throw this.BuildParserException("unterminated escape sequence string");
			Block_8:
			Block_11:
			Block_13:
			goto IL_01DA;
			Block_14:
			throw this.BuildParserException("unterminated unicode escape in string");
			Block_15:
			throw this.BuildParserException("not a well-formed unicode escape sequence in string");
			IL_01DA:
			throw this.BuildParserException("unknown escape sequence in string");
			Block_16:
			throw this.BuildParserException("control character in string");
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000197B4 File Offset: 0x000179B4
		private object Literal()
		{
			bool flag = this.Accept("true") != null;
			object obj;
			if (flag)
			{
				this.WriteLineLog("bool: true", Array.Empty<object>());
				obj = true;
			}
			else
			{
				bool flag2 = this.Accept("false") != null;
				if (flag2)
				{
					this.WriteLineLog("bool: false", Array.Empty<object>());
					obj = false;
				}
				else
				{
					bool flag3 = this.Accept("null") != null;
					if (!flag3)
					{
						throw this.BuildParserException("unknown token");
					}
					this.WriteLineLog("null", Array.Empty<object>());
					obj = null;
				}
			}
			return obj;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00019854 File Offset: 0x00017A54
		private IList<object> List()
		{
			this.WriteLineLog("list: [", Array.Empty<object>());
			List<object> list = new List<object>();
			this.SkipWhitespace();
			bool flag = this.IsNext(']');
			IList<object> list2;
			if (flag)
			{
				this.AdvanceInput(1);
				list2 = list;
			}
			else
			{
				object obj;
				do
				{
					this.SkipWhitespace();
					obj = this.Value();
					bool flag2 = obj != null;
					if (flag2)
					{
						list.Add(obj);
						this.SkipWhitespace();
						bool flag3 = this.IsNext(']');
						if (flag3)
						{
							break;
						}
						this.Expect(',');
					}
				}
				while (obj != null);
				this.Expect(']');
				this.WriteLineLog("]", Array.Empty<object>());
				list2 = list;
			}
			return list2;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00019908 File Offset: 0x00017B08
		private IDictionary<string, object> Dictionary()
		{
			this.WriteLineLog("Dictionary: {", Array.Empty<object>());
			Dictionary<string, object> dict = new Dictionary<string, object>();
			this.SkipWhitespace();
			bool flag = this.IsNext('}');
			IDictionary<string, object> dictionary;
			if (flag)
			{
				this.AdvanceInput(1);
				dictionary = dict;
			}
			else
			{
				KeyValuePair<string, object>? kvp = default(KeyValuePair<string, object>?);
				do
				{
					this.SkipWhitespace();
					kvp = this.KeyValuePair();
					bool flag2 = kvp != null;
					if (flag2)
					{
						dict[kvp.Value.Key] = kvp.Value.Value;
					}
					this.SkipWhitespace();
					bool flag3 = this.IsNext('}');
					if (flag3)
					{
						break;
					}
					this.Expect(',');
				}
				while (kvp != null);
				this.Expect('}');
				this.WriteLineLog("}", Array.Empty<object>());
				dictionary = dict;
			}
			return dictionary;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000199EC File Offset: 0x00017BEC
		private KeyValuePair<string, object>? KeyValuePair()
		{
			this.Expect('"');
			string key = this.String();
			this.SkipWhitespace();
			this.Expect(':');
			object obj = this.Value();
			return new KeyValuePair<string, object>?(new KeyValuePair<string, object>(key, obj));
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00019A34 File Offset: 0x00017C34
		private void SkipWhitespace()
		{
			int i = this.Pos;
			while (this.IsWhiteSpace(i))
			{
				i++;
			}
			bool flag = i != this.Pos;
			if (flag)
			{
				this.AdvanceInput(i - this.Pos);
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00019A7C File Offset: 0x00017C7C
		private bool IsWhiteSpace(int n)
		{
			bool flag = n >= this.InputLength;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				char c = this.Input.get_Chars(n);
				flag2 = c == ' ' || c == '\t' || c == '\r' || c == '\n';
			}
			return flag2;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00019AC8 File Offset: 0x00017CC8
		private bool IsNext(char c)
		{
			return this.Pos < this.InputLength && this.Input.get_Chars(this.Pos) == c;
		}
	}
}
