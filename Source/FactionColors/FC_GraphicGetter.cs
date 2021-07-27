using System;
using UnityEngine;
using Verse;

namespace FactionColors
{
	// Token: 0x02000023 RID: 35
	[StaticConstructorOnStartup]
	public static class FC_GraphicGetter
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00004D14 File Offset: 0x00002F14
		public static Graphic GetFCGraphic(string textpath, Shader shader, Vector2 drawSize, Color col1, Color col2)
		{
			return GraphicDatabase.Get<Graphic_Single>(textpath, ShaderDatabase.CutoutComplex, drawSize, col1, col2);
		}
	}
}
