using System;
using System.Collections.Generic;
using Verse;

namespace FactionColors
{
	// Token: 0x02000021 RID: 33
	public class ApparelDetailProps : CompProperties
	{
		// Token: 0x04000063 RID: 99
		public float DetailChance;

		// Token: 0x04000064 RID: 100
		public bool IsHeadDetail = false;

		// Token: 0x04000065 RID: 101
		public bool IsFreeFloating = false;

		// Token: 0x04000066 RID: 102
		public List<ApparelDetail> ApparelDetails;
	}
}
