// Copyright (C) 2011-2015 Bossland GmbH
// See the file LICENSE for the source code's detailed license

using Buddy.BehaviorTree;
using BenderCombat.Core;
using BenderCombat.Helpers;

namespace BenderCombat.Routines
{
	internal class Carnage : RotationBase
	{
		public override string Name
		{
			get { return "Marauder Carnage"; }
		}

		public override Composite Buffs
		{
			get
			{
				return new PrioritySelector(
					Spell.Buff("Ataru Form"),
					Spell.Buff("Unnatural Might")
					);
			}
		}

		public override Composite Cooldowns
		{
			get
			{
				return new PrioritySelector(
					Spell.Buff("Unleash"),
					Spell.Buff("Cloak of Pain", ret => Me.HealthPercent <= 90),
					Spell.Buff("Force Camouflage", ret => Me.HealthPercent <= 70),
					Spell.Buff("Saber Ward", ret => Me.HealthPercent <= 50),
					Spell.Buff("Undying Rage", ret => Me.HealthPercent <= 10),
					//Spell.Buff("Frenzy", ret => Me.BuffCount("Fury") < 5), // want buff for raids. 
					Spell.Buff("Berserk")
					);
			}
		}

		public override Composite SingleTarget
		{
			get
			{
				return new PrioritySelector(
					//Spell.Cast("Saber Throw",
					//	ret => Me.CurrentTarget.Distance >= 0.5f && Me.CurrentTarget.Distance <= 3f),
					Spell.Cast("Force Charge",
						ret => !BenderCombat.MovementDisabled && Me.CurrentTarget.Distance >= 1f && Me.CurrentTarget.Distance <= 3f),
					Spell.Cast("Dual Saber Throw",
						ret => Me.CurrentTarget.Distance >= 1f && Me.CurrentTarget.Distance <= 3f),

					//Movement
					CombatMovement.CloseDistance(Distance.Melee),

					//Rotation
					Spell.Cast("Disruption", ret => Me.CurrentTarget.IsCasting && !BenderCombat.MovementDisabled),
					Spell.Cast("Massacre", ret => !Me.HasBuff("Massacre")),
					Spell.Cast("Gore"),
					Spell.Cast("Ravage", ret => Me.HasBuff("Gore")),
					Spell.Cast("Vicious Throw"),
					Spell.Cast("Force Scream", ret => Me.HasBuff("Execute") && Me.Level < 58),
					Spell.Cast("Devastating Blast", ret => Me.HasBuff("Execute") && Me.Level > 57),
					Spell.Cast("Massacre"),
					Spell.Cast("Dual Saber Throw"),
					Spell.Cast("Battering Assault"),
					Spell.Cast("Assault")
					);
			}
		}

		public override Composite AreaOfEffect
		{
			get
			{
				return new Decorator(ret => Targeting.ShouldPbaoe,
					new PrioritySelector(
						Spell.Cast("Smash"),
						Spell.Cast("Sweeping Slash")
						));
			}
		}
	}
}