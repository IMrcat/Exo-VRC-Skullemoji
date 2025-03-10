using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace EXO.Functions.Managers
{
	// Token: 0x020000A4 RID: 164
	internal static class UpdateManager
	{
		// Token: 0x06000632 RID: 1586 RVA: 0x00022A5A File Offset: 0x00020C5A
		public static void StartFixedUpdate()
		{
			UpdateManager.cancellationTokenSource = new CancellationTokenSource();
			Task.Run(() => UpdateManager.FixedUpdateLoop(UpdateManager.cancellationTokenSource.Token));
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00022A8C File Offset: 0x00020C8C
		public static void StopFixedUpdate()
		{
			CancellationTokenSource cancellationTokenSource = UpdateManager.cancellationTokenSource;
			if (cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00022AA0 File Offset: 0x00020CA0
		[DebuggerStepThrough]
		private static Task FixedUpdateLoop(CancellationToken cancellationToken)
		{
			UpdateManager.<FixedUpdateLoop>d__4 <FixedUpdateLoop>d__ = new UpdateManager.<FixedUpdateLoop>d__4();
			<FixedUpdateLoop>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FixedUpdateLoop>d__.cancellationToken = cancellationToken;
			<FixedUpdateLoop>d__.<>1__state = -1;
			<FixedUpdateLoop>d__.<>t__builder.Start<UpdateManager.<FixedUpdateLoop>d__4>(ref <FixedUpdateLoop>d__);
			return <FixedUpdateLoop>d__.<>t__builder.Task;
		}

		// Token: 0x040002E7 RID: 743
		private static bool playerInit;

		// Token: 0x040002E8 RID: 744
		private static CancellationTokenSource cancellationTokenSource;
	}
}
