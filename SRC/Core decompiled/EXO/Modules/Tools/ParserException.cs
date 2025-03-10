using System;

namespace EXO.Modules.Tools
{
	// Token: 0x0200007B RID: 123
	public class ParserException : Exception
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00018E29 File Offset: 0x00017029
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x00018E31 File Offset: 0x00017031
		public int Line { get; private set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00018E3A File Offset: 0x0001703A
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x00018E42 File Offset: 0x00017042
		public int Column { get; private set; }

		// Token: 0x06000476 RID: 1142 RVA: 0x00018E4B File Offset: 0x0001704B
		public ParserException(string msg, int line, int col)
			: base(msg)
		{
			this.Line = line;
			this.Column = col;
		}
	}
}
