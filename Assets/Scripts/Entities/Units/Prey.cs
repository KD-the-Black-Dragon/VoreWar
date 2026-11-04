// Modified for the VoreWar mercenary-company conversion.
// 2026-10-30: Reject disallowed transformation destinations before prey-state callbacks (spec 03A2).
// 2026-11-01: Complete approved ability retirement and close legacy reactivation paths (spec 05A3B17).
// 2026-11-03: Remove permanently retired identity branches/entries while preserving shared behavior (spec 21).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
// 2026-11-03: Implement four active stats and agreed combat rules (spec 29).
using OdinSerializer;
using System.Collections.Generic;
using UnityEngine;

class Prey
{


    [OdinSerialize]
    public Actor_Unit Predator { get; set; }
    [OdinSerialize]
    public Actor_Unit Actor { get; private set; }
    [OdinSerialize]
    public Unit Unit { get; set; }
    [OdinSerialize]
    public List<Prey> SubPrey { get; private set; }
    [OdinSerialize]
    public float EscapeRate { get; private set; }
    [OdinSerialize]
    public int TurnsDigested { get; set; }
    [OdinSerialize]
    public int TurnsBeingSwallowed { get; set; }
    /// <summary>
    /// Turns since last damaged, only updated for alive units
    /// </summary>
    [OdinSerialize]
    public int TurnsSinceLastDamage { get; set; }

    [OdinSerialize]
    public bool ScatDisabled { get; set; }


    [OdinSerialize]
    public List<Traits> SharedTraits;

    public bool HasGivenWeightToPred = false;

    public PreyLocation Location => Predator?.PredatorComponent.Location(this) ?? PreyLocation.stomach;

    public Prey(Actor_Unit actor, Actor_Unit predator, List<Prey> preyList)
    {
        Actor = actor;
        actor.SelfPrey = this;
        Predator = predator;
        Unit = actor.Unit;
        SubPrey = preyList;
        SharedTraits = new List<Traits>();
    }

    public void UpdateEscapeRate()
    {
        if (Actor.Surrendered || ((Predator.Unit.HasTrait(Traits.FriendlyStomach) || Predator.Unit.HasTrait(Traits.Endosoma)) && (Unit.FixedSide == Predator.Unit.GetApparentSide(Unit)) || Unit.GetStatusEffect(StatusEffectType.Hypnotized)?.Strength == Predator.Unit.FixedSide))
        {
            EscapeRate = 0;
            return;
        }
        if (Unit.GetStatusEffect(StatusEffectType.Sleeping) != null)
        {
            EscapeRate = 0;
            return;
        }

        float predPower = Mathf.Pow(15 + Predator.Unit.GetStat(Stat.Voracity), 1.5f);
        float preyPower = Mathf.Pow(15 + Unit.GetStat(Stat.Intelligence), 1.5f);
        float predScore = 4 * ((10 + Predator.PredatorComponent.TotalCapacity()) / 10 + predPower * 3) *
            (Predator.Unit.HealthPct + 1) / (1 + Predator.PredatorComponent.UsageFraction);
        float preyScore = 6 * preyPower * (.2f + Unit.HealthPct * Unit.HealthPct) *
            ((1f + TurnsDigested) / (4f + TurnsDigested));

        preyScore *= Unit.TraitBoosts.Incoming.ChanceToEscape;
        predScore /= Predator.Unit.TraitBoosts.Outgoing.ChanceToEscape;

        if (Predator.Unit.HasTrait(Traits.Inescapable))
            preyScore = 0;

        if (Predator.Unit.HasTrait(Traits.DualStomach))
        {
            if (Predator.PredatorComponent.IsUnitInPrey(Actor, PreyLocation.stomach)) predScore *= .8f;
            else if (Predator.PredatorComponent.IsUnitInPrey(Actor, PreyLocation.stomach2)) predScore *= 1.0f;
        }

        switch (Config.EscapeRate)
        {
            case -1:
                preyScore *= .4f;
                break;
            case 1:
                preyScore *= 2;
                break;
            case 2:
                preyScore *= 6;
                break;
        }

        if (Unit.GetStatusEffect(StatusEffectType.WillingPrey) != null)
        {
            preyScore /= 2;
        }
        if (Predator.Surrendered)
            predScore /= 10;

        EscapeRate = (preyScore / (predScore + preyScore));
        EscapeRate *= Predator.Surrendered ? Config.SurrenderedPredEscapeMult : 1;
        EscapeRate *= TagConditionChecker.ApplyTagEffect(Unit, Predator.Unit, UnitTagModifierEffect.ChanceToEscapeMult);
        // Escape depends on INT versus VOR and condition, never hidden stamina.
        //float statRatio = (float)Predator.Unit.GetStat(Stat.Stomach) / (Unit.GetStat(Stat.Strength) + Unit.GetStat(Stat.Dexterity) + Unit.GetStat(Stat.Will)) * 3;
        //float healthRatio = (Predator.Unit.Health / (float)Predator.Unit.MaxHealth) / (Unit.Health / (float)Unit.MaxHealth);
        //float timeRatio;
        //if (TurnsDigested > 3)
        //    timeRatio = 1;
        //else if (TurnsDigested == 3)
        //    timeRatio = 2;
        //else if (TurnsDigested == 2)
        //    timeRatio = 3;
        //else if (TurnsDigested == 1)
        //    timeRatio = 4;
        //else
        //    timeRatio = 5;
        //float traitRatio = 1;
        //traitRatio /= Actor.Unit.TraitBoosts.Incoming.ChanceToEscape * Predator.Unit.TraitBoosts.Outgoing.ChanceToEscape;

        //float stomachRatio = 1;
        //if (Predator.Unit.HasTrait(Traits.DualStomach))
        //{
        //    if (Predator.PredatorComponent.IsUnitOfSpecificationInPrey(Actor, PreyLocation.stomach)) stomachRatio = .8f;
        //    else if (Predator.PredatorComponent.IsUnitOfSpecificationInPrey(Actor, PreyLocation.stomach2)) stomachRatio = 1f;
        //}

        //if (Unit.GetStatusEffect(StatusEffectType.WillingPrey) != null)
        //{
        //    stomachRatio *= 4;
        //}

        //float combinedRatio = statRatio * healthRatio * timeRatio * traitRatio * stomachRatio;
        //if (Predator.Surrendered)
        //    combinedRatio /= 4;
        //EscapeRate = Math.Min(0.10f / combinedRatio, 1);

    }

    public Prey[] GetAliveSubPrey()
    {
        List<Prey> units = new List<Prey>();
        for (int i = 0; i < SubPrey.Count; i++)
        {
            if (SubPrey[i].Actor.Unit.IsDead == false)
            {
                units.Add(SubPrey[i]);
            }
        }
        return units.ToArray();
    }

    void PreyVaporizer()
    {
        for (int i = 0; i < SubPrey.Count; i++)
        {
            if (SubPrey[i].Unit.IsDead == false)
            {
                SubPrey[i].Actor.SubtractHealth(999);
                SubPrey[i].PreyVaporizer();
            }
        }
    }

    public void ChangeRace(Race race)
    {
        if (!SpeciesPolicy.IsAllowed(race, SpeciesUse.Transformation))
            return;
        if (Unit.Race != Unit.HiddenRace)
        {
            Unit.ResetSharedTraits();
            Actor.RevertRace();
        }

        Predator.PredatorComponent.OnRemoveCallbacks(this, false);
        HashSet<Gender> set = new HashSet<Gender>(Races.GetRace(Unit.Race).CanBeGender);
        bool equals = set.SetEquals(Races.GetRace(race).CanBeGender);
        Unit.ChangeRace(race);
        Unit.SetGear(race);
        if (equals == false || Config.AlwaysRandomizeConverted)
            Unit.TotalRandomizeAppearance();
        else
        {
            var raceAppearance = Races.GetRace(race);
            raceAppearance.RandomCustom(Unit);
        }
        Actor.Movement = 0;
        Actor.AnimationController = new AnimationController();
        Actor.Unit.ReloadTraits();
        Actor.Unit.InitializeTraits();
        Actor.PredatorComponent?.FreeAnyAlivePrey();
        Actor.PredatorComponent?.RefreshSharedTraits();
        Predator.PredatorComponent.OnSwallowCallbacks(this);
        Actor.Surrendered = false;
    }

    public void ChangeSide(int side)
    {
        // Retired: containment cannot change allegiance.
    }

    public void PredWeightGain()
    {
        
        if (!HasGivenWeightToPred)
        {
            HasGivenWeightToPred = true;
            Predator.PredatorComponent.WeightGain(this);

        }
    }

    public List<BoneInfo> GetBoneTypes()
    {
        List<BoneInfo> rtn = new List<BoneInfo>();
        switch (Unit.Race)
        {
            case Race.FeralSlime:
                rtn.Add(new BoneInfo(BoneTypes.SlimePile, Unit.Name, Unit.SkinColor));
                break;
            case Race.Kangaroos:
                rtn.Add(new BoneInfo(BoneTypes.Kangaroo, Unit.Name));
                break;
            case Race.Wyvern:
                rtn.Add(new BoneInfo(BoneTypes.Wyvern, Unit.Name));
                break;
            case Race.WyvernMatron:
                rtn.Add(new BoneInfo(BoneTypes.Wyvern, Unit.Name));
                break;
            case Race.CoralSlugs:
            case Race.RockSlugs:
            case Race.SpitterSlugs:
            case Race.SpringSlugs:
                // No bone
                break;
            case Race.Dragon:
                rtn.Add(new BoneInfo(BoneTypes.WyvernBonesWithoutHead, Unit.Name));
                break;
            default:
                if (Unit.Furry)
                {
                    if (Unit.Race == Race.Bunnies)
                        rtn.Add(new BoneInfo(BoneTypes.FurryRabbitBones, Unit.Name));
                    else
                        rtn.Add(new BoneInfo(BoneTypes.FurryBones, Unit.Name));
                }
                else
                {
                    rtn.Add(new BoneInfo(BoneTypes.GenericBonePile, Unit.Name));
                    if (Unit.Race == Race.Lizards)
                        rtn.Add(new BoneInfo(BoneTypes.LizardSkull, ""));
                    else {
                        rtn.Add(new BoneInfo(BoneTypes.HumanoidSkull, ""));
                    }
                }
                break;

        }
        return rtn;
    }

}
