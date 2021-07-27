using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace FactionColors
{
	// Token: 0x02000027 RID: 39
	public class Graphic_RandomMulti : Graphic_Random
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00004D58 File Offset: 0x00002F58
		public new virtual void DrawWorker(Vector3 loc, Rot4 rot, ThingDef thingDef, Thing thing, float extraRotation)
		{
			Mesh mesh = this.MeshAt(rot);
			Quaternion quaternion = base.QuatFromRot(Rot4.North);
			bool flag = extraRotation != 0f;
			if (flag)
			{
				quaternion *= Quaternion.Euler(Vector3.up * extraRotation);
			}
			loc += base.DrawOffset(rot);
			Material mat = this.MatAt(rot, thing);
			this.DrawMeshInt(mesh, loc, quaternion, mat);
			bool flag2 = base.ShadowGraphic != null;
			if (flag2)
			{
				base.ShadowGraphic.DrawWorker(loc, rot, thingDef, thing, extraRotation);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004DEC File Offset: 0x00002FEC
		public override void Init(GraphicRequest req)
		{
			this.data = req.graphicData;
			bool flag = req.path.NullOrEmpty();
			if (flag)
			{
				throw new ArgumentNullException("folderPath");
			}
			bool flag2 = req.shader == null;
			if (flag2)
			{
				throw new ArgumentNullException("shader");
			}
			this.path = req.path;
			this.color = req.color;
			this.colorTwo = req.colorTwo;
			this.drawSize = req.drawSize;
			List<Texture2D> list = (from x in ContentFinder<Texture2D>.GetAllInFolder(req.path)
			where !x.name.EndsWith(Graphic_Single.MaskSuffix) && !Graphic_RandomMulti.rot4Names.Any((string y) => x.name.EndsWith(y)) && !Graphic_RandomMulti.rot4Masks.Any((string y) => x.name.EndsWith(y))
			orderby x.name
			select x).ToList<Texture2D>();
			bool flag3 = list.NullOrEmpty<Texture2D>();
			if (flag3)
			{
				Log.Error("Collection cannot init: No textures found at path " + req.path, false);
				this.subGraphics = new Graphic[]
				{
					BaseContent.BadGraphic
				};
			}
			else
			{
				this.subGraphics = new Graphic[list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					string text = req.path + "/" + list[i].name;
					this.subGraphics[i] = GraphicDatabase.Get(typeof(Graphic_Multi), text, req.shader, this.drawSize, this.color, this.colorTwo, null, req.shaderParameters);
				}
			}
		}

		// Token: 0x04000068 RID: 104
		private static string[] rot4Names = new string[]
		{
			"_east",
			"_north",
			"_south",
			"_west"
		};

		// Token: 0x04000069 RID: 105
		private static string[] rot4Masks = new string[]
		{
			"_eastm",
			"_northm",
			"_southm",
			"_westm"
		};
	}
}
