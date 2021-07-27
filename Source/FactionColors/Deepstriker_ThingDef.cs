using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace FactionColors
{
	// Token: 0x02000017 RID: 23
	public class Deepstriker_ThingDef : ThingDef
	{
		// Token: 0x04000040 RID: 64
		public int TicksToExitMap = 200;

		// Token: 0x04000041 RID: 65
		public ThingDef IncomingDef;

		// Token: 0x04000042 RID: 66
		public ThingDef RemainingDef;

		// Token: 0x04000043 RID: 67
		public ThingDef LeavingDef;

		// Token: 0x04000044 RID: 68
		public List<FactionDef> BelongsToFactions;
	}
}
