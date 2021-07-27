using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace FactionColors
{
	public class Apparel_HardShield : Apparel
	{
		private FactionItemDef factionItemDef
		{
			get
			{
				return this.def as FactionItemDef;
			}
		}

		public override Graphic Graphic
		{
			get
			{
				return base.Graphic;
			}
		}

		public CompHardShield shieldComp
		{
			get
			{
				bool flag = this.cachedComp == null;
				if (flag)
				{
					this.cachedComp = this.TryGetCompFast<CompHardShield>();
				}
				return this.cachedComp;
			}
		}

		public override bool CheckPreAbsorbDamage(DamageInfo dinfo)
		{
			float num = (float)this.GetPawnMeleeSkill();
			float shieldSizeFactor = this.shieldComp.CProps.ShieldSizeFactor;
			float shieldSturdiness = this.shieldComp.CProps.ShieldSturdiness;
			int attackerSkill = this.GetAttackerSkill(dinfo);
			float p = (float)attackerSkill / (num * shieldSizeFactor * shieldSturdiness);
			bool flag = Mathf.Pow(0.5f, p) > Rand.Value;
			bool result;
			if (flag)
			{
				this.HitPoints -= (int)(dinfo.Amount / shieldSturdiness);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public override void DrawWornExtras()
		{
			bool flag = this.ShouldDraw();
			if (flag)
			{
				Vector3 vector = this.DrawPos;
				Vector3 a = (this.factionItemDef != null) ? this.factionItemDef.ItemMeshSize : Vector3.one;
				Material material = this.Graphic.MatAt(base.Wearer.Rotation, null);
				Matrix4x4 matrix = default(Matrix4x4);
				Mesh mesh = MeshPool.plane10;
				bool flag2 = base.Wearer.Rotation == Rot4.North;
				if (flag2)
				{
					vector += new Vector3(-0.2f, -1f, 0f);
				}
				else
				{
					bool flag3 = base.Wearer.Rotation == Rot4.East;
					if (flag3)
					{
						vector += new Vector3(0.2f, -1f, 0f);
						mesh = MeshPool.plane10Flip;
					}
					else
					{
						bool flag4 = base.Wearer.Rotation == Rot4.South;
						if (flag4)
						{
							vector += new Vector3(0.2f, 0.1f, 0f);
						}
					}
				}
				matrix.SetTRS(vector, Quaternion.AngleAxis(0f, Vector3.up), a * 1.2f);
				Graphics.DrawMesh(mesh, matrix, material, 0);
			}
		}

		private bool ShouldDraw()
		{
			return (base.Wearer.carryTracker == null || base.Wearer.carryTracker.CarriedThing == null) && (base.Wearer.Drafted || (base.Wearer.CurJob != null && base.Wearer.CurJob.def.alwaysShowWeapon) || (base.Wearer.mindState.duty != null && base.Wearer.mindState.duty.def.alwaysShowWeapon));
		}

		private int GetPawnMeleeSkill()
		{
			bool flag = base.Wearer != null;
			int result;
			if (flag)
			{
				result = base.Wearer.skills.GetSkill(SkillDefOf.Melee).Level;
			}
			else
			{
				result = 1;
			}
			return result;
		}

		/*
		public override IEnumerable<Gizmo> GetWornGizmos()
		{
			Apparel_HardShield.Gizmo_HardShield gizmo = new Apparel_HardShield.Gizmo_HardShield(this);
			yield return gizmo;
			yield break;
		}
		*/
		private int GetAttackerSkill(DamageInfo dinfo)
		{
			Pawn pawn = dinfo.Instigator as Pawn;
			bool flag = pawn != null;
			int result;
			if (flag)
			{
				bool isRangedWeapon = dinfo.Weapon.IsRangedWeapon;
				if (isRangedWeapon)
				{
					result = pawn.skills.GetSkill(SkillDefOf.Shooting).Level;
				}
				else
				{
					result = base.Wearer.skills.GetSkill(SkillDefOf.Melee).Level;
				}
			}
			else
			{
				result = 10;
			}
			return result;
		}

		private CompHardShield cachedComp;
		private Vector3 actDrawPos;

	[StaticConstructorOnStartup]
	internal class Gizmo_HardShield : Gizmo
	{
		public Gizmo_HardShield(Apparel_HardShield shield)
		{
			this.shield = shield;
		}

		public override float GetWidth(float maxWidth)
		{
			return 140f;
		}

		public override GizmoResult GizmoOnGUI(Vector2 topLeft, float maxWidth, GizmoRenderParms parms)
		{
			Rect overRect = new Rect(topLeft.x, topLeft.y, this.GetWidth(maxWidth), 75f);
			Find.WindowStack.ImmediateWindow(984688, overRect, WindowLayer.GameUI, delegate ()
			{
				Rect rect = overRect.AtZero().ContractedBy(6f);
				Rect rect2 = rect;
				rect2.height -= overRect.height / 3f;
				rect2.yMin -= overRect.height / 3f;
				Texture2D texture2D = this.shield.def.uiIcon;
				bool flag = texture2D == null;
				if (flag)
				{
					texture2D = BaseContent.BadTex;
				}
				Widgets.DrawTextureFitted(new Rect(rect), texture2D, 0.85f, Vector2.one, this.iconTexCoords, 0f, null);
				Rect rect3 = rect;
				rect3.height = overRect.height / 2f;
				Text.Font = GameFont.Tiny;
				Widgets.Label(rect3, this.shield.LabelCap);
				Rect rect4 = rect;
				rect4.yMin = overRect.height / 2f * 3f;
				float fillPercent = (float)(this.shield.HitPoints / this.shield.MaxHitPoints);
				Widgets.FillableBar(rect4, fillPercent, Apparel_HardShield.Gizmo_HardShield.FullShieldBarTex, Apparel_HardShield.Gizmo_HardShield.EmptyShieldBarTex, false);
				Text.Font = GameFont.Small;
				Text.Anchor = TextAnchor.MiddleCenter;
				Widgets.Label(rect4, this.shield.HitPoints.ToString("F0") + " / " + ((float)this.shield.MaxHitPoints * 100f).ToString("F0"));
				Text.Anchor = TextAnchor.UpperLeft;
			}, true, false, 1f);
			return new GizmoResult(GizmoState.Clear);
		}
		public Apparel_HardShield shield;
		private static readonly Texture2D FullShieldBarTex = SolidColorMaterials.NewSolidColorTexture(new Color(0.5f, 0.2f, 0.5f, 0.24f));
		private static readonly Texture2D EmptyShieldBarTex = SolidColorMaterials.NewSolidColorTexture(Color.clear);
		private Rect iconTexCoords = new Rect(0f, 0f, 1f, 1f);
	}
	}
}
