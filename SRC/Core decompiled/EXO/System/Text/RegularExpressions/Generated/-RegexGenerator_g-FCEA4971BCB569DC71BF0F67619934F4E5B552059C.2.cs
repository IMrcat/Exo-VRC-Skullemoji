using System;
using System.CodeDom.Compiler;

namespace System.Text.RegularExpressions.Generated
{
	// Token: 0x02000006 RID: 6
	[GeneratedCode("System.Text.RegularExpressions.Generator", "8.0.10.36612")]
	internal static class <RegexGenerator_g>FCEA4971BCB569DC71BF0F67619934F4E5B552059C05873899862A31FB1BB2C5E__Utilities
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020B4 File Offset: 0x000002B4
		// Note: this type is marked as 'beforefieldinit'.
		static <RegexGenerator_g>FCEA4971BCB569DC71BF0F67619934F4E5B552059C05873899862A31FB1BB2C5E__Utilities()
		{
			object data = AppContext.GetData("REGEX_DEFAULT_MATCH_TIMEOUT");
			TimeSpan timeSpan;
			if (data is TimeSpan)
			{
				TimeSpan timeout = (TimeSpan)data;
				timeSpan = timeout;
			}
			else
			{
				timeSpan = Regex.InfiniteMatchTimeout;
			}
			<RegexGenerator_g>FCEA4971BCB569DC71BF0F67619934F4E5B552059C05873899862A31FB1BB2C5E__Utilities.s_defaultTimeout = timeSpan;
			<RegexGenerator_g>FCEA4971BCB569DC71BF0F67619934F4E5B552059C05873899862A31FB1BB2C5E__Utilities.s_hasTimeout = <RegexGenerator_g>FCEA4971BCB569DC71BF0F67619934F4E5B552059C05873899862A31FB1BB2C5E__Utilities.s_defaultTimeout != Regex.InfiniteMatchTimeout;
		}

		// Token: 0x04000002 RID: 2
		internal static readonly TimeSpan s_defaultTimeout;

		// Token: 0x04000003 RID: 3
		internal static readonly bool s_hasTimeout;
	}
}
