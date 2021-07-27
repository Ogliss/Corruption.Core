using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace FactionColors
{
	// Token: 0x02000006 RID: 6
	public class Backpack_Accessory : Apparel
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002B40 File Offset: 0x00000D40
		public Graphic BackpackGraphic(BodyTypeDef bodyType, string graphicPath)
		{
			bool flag = bodyType == null;
			if (flag)
			{
				Log.Error("Getting naked body graphic with undefined body type.", false);
				bodyType = BodyTypeDefOf.Male;
			}
			string path = graphicPath + bodyType.ToString();
			return GraphicDatabase.Get<Graphic_Multi>(path, ShaderDatabase.CutoutComplex, Vector2.one, this.DrawColor, this.DrawColorTwo);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002B98 File Offset: 0x00000D98
		public override void Tick()
		{
			base.Tick();
			bool flag = base.Wearer != null;
			if (flag)
			{
				this.WornGraphic = this.BackpackGraphic(base.Wearer.story.bodyType, this.Graphic.path);
			}
			else
			{
				this.WornGraphic = null;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002BF0 File Offset: 0x00000DF0
		public override void DrawWornExtras()
		{
			bool flag = this.WornGraphic != null;
			if (flag)
			{
				Vector3 drawPos = base.Wearer.Drawer.DrawPos;
				drawPos.y = AltitudeLayer.PawnUnused.AltitudeFor() + 2f;
				float angle = (float)base.Wearer.Rotation.AsInt;
				Material material = this.WornGraphic.MatAt(base.Wearer.Rotation, null);
				Vector3 s = new Vector3(1.4f, 1.4f, 1.4f);
				Matrix4x4 matrix = default(Matrix4x4);
				matrix.SetTRS(drawPos, Quaternion.AngleAxis(angle, Vector3.up), s);
				Graphics.DrawMesh(MeshPool.plane10, matrix, material, 0);
			}
		}

		// Token: 0x04000008 RID: 8
		private Graphic WornGraphic;
	}
}
