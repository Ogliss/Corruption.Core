using System;
using System.Collections.Generic;
using Verse;

namespace FactionColors
{
	// Token: 0x02000012 RID: 18
	public class CompProperties_PauldronDrawer : CompProperties
	{
		// Token: 0x06000043 RID: 67 RVA: 0x000036FA File Offset: 0x000018FA
		public CompProperties_PauldronDrawer()
		{
			this.compClass = typeof(CompPauldronDrawer);
		}

		// Token: 0x0400002E RID: 46
		public List<ShoulderPadEntry> PauldronEntries;

		// Token: 0x0400002F RID: 47
		public float PauldronEntryChance = 0.5f;
	}
}
