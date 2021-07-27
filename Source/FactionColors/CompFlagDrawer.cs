using System;
using UnityEngine;
using Verse;

namespace FactionColors
{
	// Token: 0x0200000D RID: 13
	public class CompFlagDrawer : ThingComp
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00003254 File Offset: 0x00001454
		public CompProperties_FlagDrawer cprops
		{
			get
			{
				return this.props as CompProperties_FlagDrawer;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00003274 File Offset: 0x00001474
		public FactionColorsTracker factioColorTracker
		{
			get
			{
				return FactionColorUtilities.currentFactionColorTracker;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000328C File Offset: 0x0000148C
		public Graphic bannerGraphic
		{
			get
			{
				return GraphicDatabase.Get<Graphic_Single>(this.factioColorTracker.BannerGraphicPath, ShaderDatabase.CutoutComplex, Vector2.one, this.factioColorTracker.PlayerColorOne, this.factioColorTracker.PlayerColorTwo);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000032D0 File Offset: 0x000014D0
		public override void PostDraw()
		{
			base.PostDraw();
			bool flag = this.cprops != null;
			if (flag)
			{
				Mesh plane = MeshPool.plane10;
				Vector3 pos = this.parent.DrawPos + this.cprops.DrawOffset;
				pos.y += 5f;
				Vector3 s = new Vector3(1.5f, 1f, 1.5f);
				Matrix4x4 matrix = default(Matrix4x4);
				matrix.SetTRS(pos, Quaternion.AngleAxis(0f, Vector3.up), s);
				Graphics.DrawMesh(MeshPool.plane10, matrix, this.bannerGraphic.MatAt(this.parent.Rotation, null), 0);
			}
		}
	}
}
