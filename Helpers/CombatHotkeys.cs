﻿// Copyright (C) 2011-2015 Bossland GmbH
// See the file LICENSE for the source code's detailed license

using System.Windows.Forms;
using Buddy.Common;
using BenderCombat.Core;

namespace BenderCombat.Helpers
{
	public static class CombatHotkeys
	{
		public static bool EnableAoe;
		public static bool PauseRotation;
		public static bool EnableInterrupts;
		public static bool EnableCharge;

		public static void Initialize()
		{
			EnableAoe = true;
			PauseRotation = false;
			EnableInterrupts = true; 
			EnableCharge = true;

			Hotkeys.RegisterHotkey("Toggle Interrupts (F5)", ChangeInterrupts, Keys.F5);
			Logger.Write("[Hot Key][F5] Toggle Interrupts");

			Hotkeys.RegisterHotkey("Toggle Charge (F6)", ChangeCharge, Keys.F6);
			Logger.Write("[Hot Key][F6] Toggle Charge");
			
			Hotkeys.RegisterHotkey("Toggle AOE (F7)", ChangeAoe, Keys.F7);
			Logger.Write("[Hot Key][F7] Toggle AOE");

			Hotkeys.RegisterHotkey("Pause Rotation (F8)", ChangePause, Keys.F8);
			Logger.Write("[Hot Key][F8] Pause Rotation");

			Hotkeys.RegisterHotkey("Set Tank (F12)", Targeting.SetTank, Keys.F12);
			Logger.Write("[Hot Key][F12] Set Tank");
		}

		private static void ChangeAoe()
		{
			if (EnableAoe)
			{
				Logger.Write("AOE Disabled");
				EnableAoe = false;
			}
			else
			{
				Logger.Write("AOE Enabled");
				EnableAoe = true;
			}
		}

		private static void ChangePause()
		{
			if (PauseRotation)
			{
				Logger.Write("Rotation Resumed");
				PauseRotation = false;
			}
			else
			{
				Logger.Write("Rotation Paused");
				PauseRotation = true;
			}
		}
		
		private static void ChangeCharge()
		{
			if (EnableCharge)
			{
				Logger.Write("Charge Disabled");
				EnableCharge = false;
			}
			else
			{
				Logger.Write("Charge Enabled");
				EnableCharge = true;
			}
		}

		private static void ChangeInterrupts()
		{
			if (EnableInterrupts)
			{
				Logger.Write("Interrupts Disabled");
				EnableInterrupts = false;
			}
			else
			{
				Logger.Write("Interrupts Enabled");
				EnableInterrupts = true;
			}
		}
	}
}