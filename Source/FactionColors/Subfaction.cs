using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace FactionColors
{
	// Token: 0x0200002C RID: 44
	public class Subfaction
	{
		// Token: 0x04000070 RID: 112
		public string SubfactionName;

		// Token: 0x04000071 RID: 113
		public string SubfactionLabel;

		// Token: 0x04000072 RID: 114
		public string SubfactionDescription;

		// Token: 0x04000073 RID: 115
		public Color SubfactionColor1;

		// Token: 0x04000074 RID: 116
		public Color SubfactionColor2;

		// Token: 0x04000075 RID: 117
		public List<PawnGroupMaker> SubfactionPawnGroupMakers;

		// Token: 0x04000076 RID: 118
		public RulePackDef SubfactionNameMaker;

		// Token: 0x04000077 RID: 119
		public float weight;

		// Token: 0x04000078 RID: 120
		public string SubfactionPreferredChaosGod = "ChaosUndivided";
	}
}
