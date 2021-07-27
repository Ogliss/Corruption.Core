using System;
using System.Collections.Generic;
using Verse;

namespace FactionColors
{
	// Token: 0x02000013 RID: 19
	public class CompProperties_FactionColor : CompProperties
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00003720 File Offset: 0x00001920
		public CompProperties_FactionColor()
		{
			this.compClass = typeof(CompFactionColor);
		}

		// Token: 0x04000030 RID: 48
		public bool UseFactionColor = true;

		// Token: 0x04000031 RID: 49
		public bool UseCamouflageColor = false;

		// Token: 0x04000032 RID: 50
		public bool TryUseSecondaryStuffColors = false;

		// Token: 0x04000033 RID: 51
		public bool UseSecondaryColors = false;

		// Token: 0x04000034 RID: 52
		public bool IsRandomMultiGraphic = false;

		// Token: 0x04000035 RID: 53
		public List<Pair<string, int>> RandomGraphicPaths = new List<Pair<string, int>>();

		// Token: 0x04000036 RID: 54
		public ColorGenerator_Options Coloring = new ColorGenerator_Options();
	}
}
