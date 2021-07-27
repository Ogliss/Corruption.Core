using System;
using Verse;

namespace FactionColors
{
	// Token: 0x0200000B RID: 11
	public class CompProperties_AccessoryDrawer : CompProperties
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000030F8 File Offset: 0x000012F8
		public CompProperties_AccessoryDrawer()
		{
			this.compClass = typeof(CompAccessoryDrawer);
		}

		// Token: 0x04000013 RID: 19
		public string texPath;

		// Token: 0x04000014 RID: 20
		public bool useBodyTypes;

		// Token: 0x04000015 RID: 21
		public bool useCrownTypes;

		// Token: 0x04000016 RID: 22
		public bool UseParentColors;

		// Token: 0x04000017 RID: 23
		public bool UseColorGenerator;

		// Token: 0x04000018 RID: 24
		public ShaderTypeDef shader;

		// Token: 0x04000019 RID: 25
		public Type graphicClass;

		// Token: 0x0400001A RID: 26
		public ThingDef templateApparelDef;
	}
}
