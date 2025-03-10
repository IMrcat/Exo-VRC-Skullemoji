using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;

namespace System.Text.RegularExpressions.Generated
{
	// Token: 0x02000005 RID: 5
	[GeneratedCode("System.Text.RegularExpressions.Generator", "8.0.10.36612")]
	[SkipLocalsInit]
	internal sealed class <RegexGenerator_g>FCEA4971BCB569DC71BF0F67619934F4E5B552059C05873899862A31FB1BB2C5E__AnsiColorRegex_0 : Regex
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002058 File Offset: 0x00000258
		private <RegexGenerator_g>FCEA4971BCB569DC71BF0F67619934F4E5B552059C05873899862A31FB1BB2C5E__AnsiColorRegex_0()
		{
			this.pattern = "\\x1b\\[38;2;(\\d+);(\\d+);(\\d+)m";
			this.roptions = 0;
			Regex.ValidateMatchTimeout(<RegexGenerator_g>FCEA4971BCB569DC71BF0F67619934F4E5B552059C05873899862A31FB1BB2C5E__Utilities.s_defaultTimeout);
			this.internalMatchTimeout = <RegexGenerator_g>FCEA4971BCB569DC71BF0F67619934F4E5B552059C05873899862A31FB1BB2C5E__Utilities.s_defaultTimeout;
			this.factory = new <RegexGenerator_g>FCEA4971BCB569DC71BF0F67619934F4E5B552059C05873899862A31FB1BB2C5E__AnsiColorRegex_0.RunnerFactory();
			this.capsize = 4;
		}

		// Token: 0x04000001 RID: 1
		[Nullable(1)]
		internal static readonly <RegexGenerator_g>FCEA4971BCB569DC71BF0F67619934F4E5B552059C05873899862A31FB1BB2C5E__AnsiColorRegex_0 Instance = new <RegexGenerator_g>FCEA4971BCB569DC71BF0F67619934F4E5B552059C05873899862A31FB1BB2C5E__AnsiColorRegex_0();

		// Token: 0x020000BA RID: 186
		private sealed class RunnerFactory : RegexRunnerFactory
		{
			// Token: 0x060006A4 RID: 1700 RVA: 0x00026D3A File Offset: 0x00024F3A
			[NullableContext(1)]
			protected override RegexRunner CreateInstance()
			{
				return new <RegexGenerator_g>FCEA4971BCB569DC71BF0F67619934F4E5B552059C05873899862A31FB1BB2C5E__AnsiColorRegex_0.RunnerFactory.Runner();
			}

			// Token: 0x020001AA RID: 426
			private sealed class Runner : RegexRunner
			{
				// Token: 0x06000B59 RID: 2905 RVA: 0x00035ED8 File Offset: 0x000340D8
				protected override void Scan(ReadOnlySpan<char> inputSpan)
				{
					while (this.TryFindNextPossibleStartingPosition(inputSpan) && !this.TryMatchAtCurrentPosition(inputSpan) && this.runtextpos != inputSpan.Length)
					{
						this.runtextpos++;
						bool s_hasTimeout = <RegexGenerator_g>FCEA4971BCB569DC71BF0F67619934F4E5B552059C05873899862A31FB1BB2C5E__Utilities.s_hasTimeout;
						if (s_hasTimeout)
						{
							base.CheckTimeout();
						}
					}
				}

				// Token: 0x06000B5A RID: 2906 RVA: 0x00035F38 File Offset: 0x00034138
				private bool TryFindNextPossibleStartingPosition(ReadOnlySpan<char> inputSpan)
				{
					int pos = this.runtextpos;
					bool flag = pos <= inputSpan.Length - 13;
					if (flag)
					{
						int i = MemoryExtensions.IndexOf<char>(inputSpan.Slice(pos), "\u001b[38;2;");
						bool flag2 = i >= 0;
						if (flag2)
						{
							this.runtextpos = pos + i;
							return true;
						}
					}
					this.runtextpos = inputSpan.Length;
					return false;
				}

				// Token: 0x06000B5B RID: 2907 RVA: 0x00035FAC File Offset: 0x000341AC
				private unsafe bool TryMatchAtCurrentPosition(ReadOnlySpan<char> inputSpan)
				{
					int pos = this.runtextpos;
					int matchStart = pos;
					ReadOnlySpan<char> slice = inputSpan.Slice(pos);
					bool flag = !MemoryExtensions.StartsWith<char>(slice, "\u001b[38;2;");
					bool flag2;
					if (flag)
					{
						this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
						flag2 = false;
					}
					else
					{
						pos += 7;
						slice = inputSpan.Slice(pos);
						int capture_starting_pos = pos;
						int iteration = 0;
						while (iteration < slice.Length && char.IsDigit((char)(*slice[iteration])))
						{
							iteration++;
						}
						bool flag3 = iteration == 0;
						if (flag3)
						{
							this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
							flag2 = false;
						}
						else
						{
							slice = slice.Slice(iteration);
							pos += iteration;
							base.Capture(1, capture_starting_pos, pos);
							bool flag4 = slice.IsEmpty || *slice[0] != 59;
							if (flag4)
							{
								this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
								flag2 = false;
							}
							else
							{
								pos++;
								slice = inputSpan.Slice(pos);
								int capture_starting_pos2 = pos;
								int iteration2 = 0;
								while (iteration2 < slice.Length && char.IsDigit((char)(*slice[iteration2])))
								{
									iteration2++;
								}
								bool flag5 = iteration2 == 0;
								if (flag5)
								{
									this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
									flag2 = false;
								}
								else
								{
									slice = slice.Slice(iteration2);
									pos += iteration2;
									base.Capture(2, capture_starting_pos2, pos);
									bool flag6 = slice.IsEmpty || *slice[0] != 59;
									if (flag6)
									{
										this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
										flag2 = false;
									}
									else
									{
										pos++;
										slice = inputSpan.Slice(pos);
										int capture_starting_pos3 = pos;
										int iteration3 = 0;
										while (iteration3 < slice.Length && char.IsDigit((char)(*slice[iteration3])))
										{
											iteration3++;
										}
										bool flag7 = iteration3 == 0;
										if (flag7)
										{
											this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
											flag2 = false;
										}
										else
										{
											slice = slice.Slice(iteration3);
											pos += iteration3;
											base.Capture(3, capture_starting_pos3, pos);
											bool flag8 = slice.IsEmpty || *slice[0] != 109;
											if (flag8)
											{
												this.<TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(0);
												flag2 = false;
											}
											else
											{
												pos++;
												this.runtextpos = pos;
												base.Capture(0, matchStart, pos);
												flag2 = true;
											}
										}
									}
								}
							}
						}
					}
					return flag2;
				}

				// Token: 0x06000B5D RID: 2909 RVA: 0x00036228 File Offset: 0x00034428
				[CompilerGenerated]
				[MethodImpl(256)]
				private void <TryMatchAtCurrentPosition>g__UncaptureUntil|2_0(int capturePosition)
				{
					while (base.Crawlpos() > capturePosition)
					{
						base.Uncapture();
					}
				}
			}
		}
	}
}
