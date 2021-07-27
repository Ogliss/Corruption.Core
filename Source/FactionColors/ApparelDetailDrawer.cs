using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace FactionColors
{
	// Token: 0x02000020 RID: 32
	[StaticConstructorOnStartup]
	public class ApparelDetailDrawer : ThingComp
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00004918 File Offset: 0x00002B18
		public ApparelDetail AppDetail
		{
			get
			{
				bool firstSpawn = this.FirstSpawn;
				if (firstSpawn)
				{
					this.HasDetail = (this.AppProps.DetailChance >= Rand.Range(0.1f, 0.9f));
					bool hasDetail = this.HasDetail;
					if (hasDetail)
					{
						this.appDetailInt = this.AppProps.ApparelDetails.RandomElementByWeight((ApparelDetail x) => x.Commonality);
						this.FirstSpawn = false;
						return this.appDetailInt;
					}
				}
				return this.appDetailInt;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000049B4 File Offset: 0x00002BB4
		public Graphic DetailGraphic
		{
			get
			{
				bool flag = this.AppDetail != null && this.apparel.Wearer == null;
				if (flag)
				{
					this.detailGraphicInt = GraphicDatabase.Get<Graphic_Multi>(this.AppDetail.DetailGraphicPath, ShaderDatabase.CutoutComplex, this.drawSize, this.parent.DrawColor, this.parent.DrawColorTwo);
				}
				else
				{
					bool flag2 = this.AppDetail != null && this.apparel.Wearer != null;
					if (flag2)
					{
						bool flag3 = this.apparel.def.apparel.LastLayer == ApparelLayerDefOf.Overhead;
						string path;
						if (flag3)
						{
							path = this.AppDetail.DetailGraphicPath;
						}
						else
						{
							path = this.AppDetail.DetailGraphicPath + "_" + this.apparel.Wearer.story.bodyType.ToString();
						}
						this.detailGraphicInt = GraphicDatabase.Get<Graphic_Multi>(path, ShaderDatabase.CutoutComplex, this.drawSize, this.parent.DrawColor, this.parent.DrawColorTwo);
					}
				}
				return this.detailGraphicInt;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00004ADC File Offset: 0x00002CDC
		public ApparelDetailProps AppProps
		{
			get
			{
				return (ApparelDetailProps)this.props;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00004AFC File Offset: 0x00002CFC
		private Apparel apparel
		{
			get
			{
				return this.parent as Apparel;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004B1C File Offset: 0x00002D1C
		public override void PostSpawnSetup(bool respawnAfterLoad)
		{
			base.PostSpawnSetup(respawnAfterLoad);
			bool flag = this.AppDetail == null;
			if (flag)
			{
				Log.Message("NoAppdetail", false);
			}
			bool flag2 = this.DetailGraphic == null;
			if (flag2)
			{
				Log.Message("NoAppGraphic", false);
			}
			this.InitiateDetails();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004B6C File Offset: 0x00002D6C
		public void InitiateDetails()
		{
			bool firstSpawn = this.FirstSpawn;
			if (firstSpawn)
			{
				this.HasDetail = (this.AppProps.DetailChance >= Rand.Range(0.1f, 0.9f));
				bool hasDetail = this.HasDetail;
				if (hasDetail)
				{
					this.appDetailInt = this.AppProps.ApparelDetails.RandomElementByWeight((ApparelDetail hd) => hd.Commonality);
				}
			}
			this.FirstSpawn = false;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004BF4 File Offset: 0x00002DF4
		public static bool GetDetailGraphic(Pawn pawn, Apparel curr, Rot4 bodyFacing, out Material detailGraphic)
		{
			detailGraphic = null;
			bool result;
			try
			{
				bool flag = pawn.needs != null && pawn.story != null;
				if (flag)
				{
					ApparelDetailDrawer apparelDetailDrawer;
					bool flag2 = (apparelDetailDrawer = curr.TryGetCompFast<ApparelDetailDrawer>()) != null;
					if (flag2)
					{
						apparelDetailDrawer.PostSpawnSetup(false);
						bool hasDetail = apparelDetailDrawer.HasDetail;
						if (hasDetail)
						{
							detailGraphic = apparelDetailDrawer.DetailGraphic.MatAt(bodyFacing, null);
							return true;
						}
					}
				}
				result = false;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004C78 File Offset: 0x00002E78
		public override void PostExposeData()
		{
			base.PostExposeData();
			Scribe_Values.Look<bool>(ref this.HasDetail, "HasDetail", true, false);
			Scribe_Values.Look<bool>(ref this.FirstSpawn, "FirstSpawn", false, false);
			Scribe_Values.Look<string>(ref this.texPath, "texPath", null, false);
		}

		// Token: 0x0400005D RID: 93
		private bool FirstSpawn = true;

		// Token: 0x0400005E RID: 94
		private ApparelDetail appDetailInt;

		// Token: 0x0400005F RID: 95
		private Graphic detailGraphicInt;

		// Token: 0x04000060 RID: 96
		public bool HasDetail = false;

		// Token: 0x04000061 RID: 97
		private Vector2 drawSize = new Vector2(1.5f, 1.5f);

		// Token: 0x04000062 RID: 98
		private string texPath;
	}
}
