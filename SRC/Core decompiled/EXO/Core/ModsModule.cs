using System;

namespace EXO.Core
{
	// Token: 0x020000B5 RID: 181
	internal abstract class ModsModule
	{
		// Token: 0x06000696 RID: 1686 RVA: 0x00026A91 File Offset: 0x00024C91
		public virtual void OnInject()
		{
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00026A94 File Offset: 0x00024C94
		public virtual void OnUpdate()
		{
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00026A97 File Offset: 0x00024C97
		public virtual void OnFixedUpdate()
		{
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00026A9A File Offset: 0x00024C9A
		public virtual void OnPlayerWasInit()
		{
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00026A9D File Offset: 0x00024C9D
		public virtual void OnPlayerWasDestroyed()
		{
		}
	}
}
