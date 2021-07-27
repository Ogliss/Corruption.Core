using System;
using UnityEngine;
using Verse;

namespace FactionColors
{
	// Token: 0x02000028 RID: 40
	public class Graphic_SingleFC : Graphic_Single
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00004FEC File Offset: 0x000031EC
		public override Graphic GetColoredVersion(Shader newShader, Color newColor, Color newColorTwo)
		{
			return GraphicDatabase.Get<Graphic_Single>(this.path, newShader, this.drawSize, newColor, newColorTwo, this.data);
		}

		// Token: 0x0400006A RID: 106
		public Shader shader;
	}
}
