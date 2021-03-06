﻿// Copyright (C) 2011-2015 Bossland GmbH
// See the file LICENSE for the source code's detailed license

using Buddy.BehaviorTree;
using BenderCombat.Core;
using BenderCombat.Helpers;

namespace BenderCombat.Routines
{
	public class Plasmatech : RotationBase
	{
		public override string Name
		{
			get { return "Vanguard Plasmatech"; }
		}

		public override Composite Buffs
		{
			get
			{
				return new PrioritySelector(
					Spell.Buff("Plasma Cell"),
					Spell.Buff("Fortification")
					);
			}
		}

		public override Composite Cooldowns
		{
			get
			{
				return new PrioritySelector(
					Spell.Buff("Tenacity"),
					Spell.Buff("Battle Focus"),
					Spell.Buff("Recharge Cells", ret => Me.ResourcePercent() <= 50),
					Spell.Buff("Reserve Powercell"),
					Spell.Buff("Reactive Shield", ret => Me.HealthPercent <= 60),
					Spell.Buff("Adrenaline Rush", ret => Me.HealthPercent <= 30)
					);
			}
		}

		public override Composite SingleTarget
		{
			get
			{
				return new PrioritySelector(
					//Movement
					CombatMovement.CloseDistance(Distance.Melee),
					new Decorator(ret => Me.ResourcePercent() < 60,
						new PrioritySelector(
							Spell.Cast("High Impact Bolt", ret => Me.HasBuff("Ionic Accelerator")),
							Spell.Cast("Hammer Shot")
							)),
					Spell.Cast("High Impact Bolt"),
					Spell.Cast("Assault Plastique"),
					Spell.DoT("Incendiary Round", "", 12000),
					Spell.Cast("Shockstrike"),
					Spell.Cast("Ion Pulse")
					);
			}
		}

		public override Composite AreaOfEffect
		{
			get
			{
				return new Decorator(ret => Targeting.ShouldAoe,
					new PrioritySelector(
						Spell.CastOnGround("Mortar Volley"),
						Spell.Cast("Pulse Cannon", ret => Me.CurrentTarget.Distance <= 1f),
						Spell.Cast("Explosive Surge", ret => Me.CurrentTarget.Distance <= .5f)
						));
			}
		}
	}
}