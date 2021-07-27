using System;
using Verse;

namespace FactionColors
{
	// Token: 0x0200002D RID: 45
	public class TestClass : ThingWithComps
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000057C8 File Offset: 0x000039C8
		public override Graphic Graphic
		{
			get
			{
				return GraphicDatabase.Get<Graphic_Single>(this.def.graphicData.texPath, ShaderDatabase.Cutout);
			}
		}
	}
}
