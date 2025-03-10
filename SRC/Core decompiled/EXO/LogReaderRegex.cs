using System;
using System.CodeDom.Compiler;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions.Generated;

// Token: 0x02000004 RID: 4
internal static class LogReaderRegex
{
	// Token: 0x06000005 RID: 5 RVA: 0x00002050 File Offset: 0x00000250
	[GeneratedRegex("\\x1b\\[38;2;(\\d+);(\\d+);(\\d+)m")]
	[GeneratedCode("System.Text.RegularExpressions.Generator", "8.0.10.36612")]
	internal static Regex AnsiColorRegex()
	{
		return <RegexGenerator_g>FCEA4971BCB569DC71BF0F67619934F4E5B552059C05873899862A31FB1BB2C5E__AnsiColorRegex_0.Instance;
	}
}
