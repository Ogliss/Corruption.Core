using System;
using Verse;

namespace FactionColors
{
	// Token: 0x0200000E RID: 14
	public class CompHardShield : ThingComp
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003390 File Offset: 0x00001590
		public CompProperties_HardShield CProps
		{
			get
			{
				return this.props as CompProperties_HardShield;
			}
		}
	}
}
