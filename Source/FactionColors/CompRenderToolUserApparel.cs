using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace FactionColors
{
	// Token: 0x02000016 RID: 22
	public class CompRenderToolUserApparel : ThingComp
	{
		// Token: 0x06000047 RID: 71 RVA: 0x000037C0 File Offset: 0x000019C0
		public override void PostSpawnSetup(bool respawnAfterLoad)
		{
			base.PostSpawnSetup(respawnAfterLoad);
			bool flag = (this.app = (this.parent as Apparel)) != null;
			if (flag)
			{
				bool flag2 = (this.Wearer = this.app.Wearer) != null;
				if (flag2)
				{
					bool flag3 = (this.renderer = this.Wearer.Drawer.renderer) == null;
					if (flag3)
					{
						Log.Error("No PawnRenderer for :" + this.Wearer.ToString(), false);
					}
					else
					{
						this.DoRender = true;
					}
				}
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003860 File Offset: 0x00001A60
		private void DrawApparelOnTooluser(PawnRenderer renderer, Vector3 drawLoc, Rot4 bodyFacing, Quaternion quat)
		{
			for (int i = 0; i < renderer.graphics.apparelGraphics.Count; i++)
			{
				ApparelGraphicRecord apparelGraphicRecord = renderer.graphics.apparelGraphics[i];
				Mesh mesh = renderer.graphics.nakedGraphic.MeshAt(bodyFacing);
				Material material = apparelGraphicRecord.graphic.MatAt(bodyFacing, null);
				material = renderer.graphics.flasher.GetDamagedMat(material);
				GenDraw.DrawMeshNowOrLater(mesh, drawLoc, quat, material, false);
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000038E4 File Offset: 0x00001AE4
		public override void PostDraw()
		{
			bool doRender = this.DoRender;
			if (doRender)
			{
				Vector3 drawPos = this.Wearer.DrawPos;
				bool flag = this.Wearer.GetPosture() == PawnPosture.Standing;
				if (flag)
				{
					Rot4 rotation = this.Wearer.Rotation;
					Quaternion identity = Quaternion.identity;
					this.DrawApparelOnTooluser(this.renderer, drawPos, rotation, identity);
				}
			}
		}

		// Token: 0x0400003B RID: 59
		public bool RenderApparel = true;

		// Token: 0x0400003C RID: 60
		private Apparel app;

		// Token: 0x0400003D RID: 61
		private Pawn Wearer;

		// Token: 0x0400003E RID: 62
		private PawnRenderer renderer;

		// Token: 0x0400003F RID: 63
		private bool DoRender;
	}
}
