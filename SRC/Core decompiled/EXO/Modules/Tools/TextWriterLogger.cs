using System;
using System.IO;

namespace EXO.Modules.Tools
{
	// Token: 0x0200007D RID: 125
	public class TextWriterLogger : ILogger
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00018E66 File Offset: 0x00017066
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x00018E6E File Offset: 0x0001706E
		public TextWriter Writer { get; set; }

		// Token: 0x0600047A RID: 1146 RVA: 0x00018E77 File Offset: 0x00017077
		public TextWriterLogger(TextWriter writer)
		{
			this.Writer = writer;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00018E8C File Offset: 0x0001708C
		public void WriteLine(string message, params object[] arguments)
		{
			bool flag = arguments != null && arguments.Length != 0;
			if (flag)
			{
				this.Writer.WriteLine(message, arguments);
			}
			else
			{
				this.Writer.WriteLine(message);
			}
		}
	}
}
