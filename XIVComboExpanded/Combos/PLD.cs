namespace XIVComboExpandedPlugin.Combos
{
    internal static class PLD
    {
        public const byte ClassID = 1;
        public const byte JobID = 19;

        public const uint
            FastBlade = 9,
            RiotBlade = 15,
            RageOfHalone = 21,
            GoringBlade = 3538,
            RoyalAuthority = 3539,
            TotalEclipse = 7381,
            Requiescat = 7383,
            HolySpirit = 7384,
            Prominence = 16457,
            HolyCircle = 16458,
            Confiteor = 16459,
            Atonement = 16460,
            BladeOfFaith = 25748,
            BladeOfTruth = 25749,
            BladeOfValor = 25750;

        public static class Buffs
        {
            public const ushort
                Requiescat = 1368,
                SwordOath = 1902;
        }

        public static class Debuffs
        {
            public const ushort
                Placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                RiotBlade = 4,
                RageOfHalone = 26,
                Prominence = 40,
                GoringBlade = 54,
                RoyalAuthority = 60,
                HolyCircle = 72,
                Atonement = 76,
                Confiteor = 80,
                BladeOfFaith = 90,
                BladeOfTruth = 90,
                BladeOfValor = 90;
        }
    }

    internal class PaladinGoringBladeCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinGoringBladeCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == PLD.GoringBlade)
            {
                if (comboTime > 0)
                {
                    if (lastComboMove == PLD.RiotBlade && level >= PLD.Levels.GoringBlade)
                        return PLD.GoringBlade;

                    if (lastComboMove == PLD.FastBlade && level >= PLD.Levels.RiotBlade)
                        return PLD.RiotBlade;
                }

                return PLD.FastBlade;
            }

            return actionID;
        }
    }

    internal class PaladinRoyalAuthorityCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinRoyalAuthorityCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == PLD.RoyalAuthority || actionID == PLD.RageOfHalone)
            {
                if (IsEnabled(CustomComboPreset.PaladinAtonementFeature))
                {
                    if (level >= PLD.Levels.Atonement && HasEffect(PLD.Buffs.SwordOath))
                        return PLD.Atonement;
                }

                if (comboTime > 0)
                {
                    if (lastComboMove == PLD.RiotBlade && level >= PLD.Levels.RageOfHalone)
                        return OriginalHook(PLD.RageOfHalone);

                    if (lastComboMove == PLD.FastBlade && level >= PLD.Levels.RiotBlade)
                        return PLD.RiotBlade;
                }

                return PLD.FastBlade;
            }

            return actionID;
        }
    }

    internal class PaladinProminenceCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinProminenceCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == PLD.Prominence)
            {
                if (comboTime > 0)
                {
                    if (lastComboMove == PLD.TotalEclipse && level >= PLD.Levels.Prominence)
                        return PLD.Prominence;
                }

                return PLD.TotalEclipse;
            }

            return actionID;
        }
    }

    internal class PaladinConfiteorFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinConfiteorFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == PLD.HolySpirit || actionID == PLD.HolyCircle)
            {
                if (lastComboMove == PLD.BladeOfTruth && level >= PLD.Levels.BladeOfValor)
                    return PLD.BladeOfValor;

                if (lastComboMove == PLD.BladeOfFaith && level >= PLD.Levels.BladeOfTruth)
                    return PLD.BladeOfTruth;

                if (lastComboMove == PLD.Confiteor && level >= PLD.Levels.BladeOfFaith)
                    return PLD.BladeOfFaith;

                if (level >= PLD.Levels.Confiteor)
                {
                    var requiescat = FindEffect(PLD.Buffs.Requiescat);
                    if (requiescat != null && (requiescat.StackCount == 1 || LocalPlayer?.CurrentMp < 2000))
                        return PLD.Confiteor;
                }
            }

            return actionID;
        }
    }

    internal class PaladinRequiescatCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.PaladinRequiescatCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == PLD.Requiescat)
            {
                if (lastComboMove == PLD.BladeOfTruth && level >= PLD.Levels.BladeOfValor)
                    return PLD.BladeOfValor;

                if (lastComboMove == PLD.BladeOfFaith && level >= PLD.Levels.BladeOfTruth)
                    return PLD.BladeOfTruth;

                if (lastComboMove == PLD.Confiteor && level >= PLD.Levels.BladeOfFaith)
                    return PLD.BladeOfFaith;

                if (level >= PLD.Levels.Confiteor)
                {
                    var requiescat = FindEffect(PLD.Buffs.Requiescat);
                    if (requiescat != null && (requiescat.StackCount == 1 || LocalPlayer?.CurrentMp < 2000))
                        return PLD.Confiteor;
                }
            }

            return actionID;
        }
    }
}
