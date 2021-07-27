using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace FactionColors
{
	// Token: 0x0200001C RID: 28
	public class FactionDefUniform : FactionDef
	{
		// Token: 0x0600005F RID: 95 RVA: 0x000043D8 File Offset: 0x000025D8
		public override void ResolveReferences()
		{
			base.ResolveReferences();
			if (this.Subfactions != null)
			{
				Subfaction subfaction = this.Subfactions.RandomElementByWeight((Subfaction sub2) => sub2.weight);
				this.fixedName = subfaction.SubfactionName;
				this.description = subfaction.SubfactionDescription;
				this.FactionColor1 = subfaction.SubfactionColor1;
				this.FactionColor2 = subfaction.SubfactionColor2;
				if (subfaction.SubfactionPawnGroupMakers != null)
				{
					this.pawnGroupMakers = subfaction.SubfactionPawnGroupMakers;
				}
				this.PreferredChaosGod = subfaction.SubfactionPreferredChaosGod;
				if (subfaction.SubfactionNameMaker != null)
				{
					// need to refactor to interface with cultrue def
				//	this.pawnNameMaker = subfaction.SubfactionNameMaker;
				}
			}
		}

		// Token: 0x0400004E RID: 78
		public Color FactionColor1 = Color.white;

		// Token: 0x0400004F RID: 79
		public Color FactionColor2 = Color.black;

		// Token: 0x04000050 RID: 80
		public string PreferredChaosGod = "ChaosUndivided";

		// Token: 0x04000051 RID: 81
		public List<Subfaction> Subfactions;
	}
}
