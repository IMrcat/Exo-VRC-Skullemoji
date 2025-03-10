using System;

namespace EXO.Core
{
	// Token: 0x020000B1 RID: 177
	internal abstract class FunctionModule
	{
		// Token: 0x0600067B RID: 1659 RVA: 0x000264D1 File Offset: 0x000246D1
		public virtual void OnInject()
		{
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x000264D4 File Offset: 0x000246D4
		public virtual void OnUpdate()
		{
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x000264D7 File Offset: 0x000246D7
		public virtual void OnFixedUpdate()
		{
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x000264DA File Offset: 0x000246DA
		public virtual void OnPlayerWasInit()
		{
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x000264DD File Offset: 0x000246DD
		public virtual void OnPlayerWasDestroyed()
		{
		}
	}
}
