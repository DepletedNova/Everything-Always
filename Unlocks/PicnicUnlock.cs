using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;

namespace EverythingAlways.Unlocks
{
    public class PicnicUnlock : CustomUnlockCard
    {
        public override string UniqueNameID => "Picnic Unlock";

        public override bool IsUnlockable => false;
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override UnlockGroup UnlockGroup => UnlockGroup.Special;
        public override CardType CardType => CardType.Setting;

        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Feast", "Customers can order any food at any phase. They'll eat food faster and make more mess.", ""))
        };

        public override List<UnlockEffect> Effects => new()
        {
            new GlobalEffect()
            {
                EffectCondition = new CEffectAlways(),
                EffectType = new CTableModifier
                {
                    Attractiveness = 1,
                    DecorationModifiers = DecorationValues.Neutral,
                    OrderingModifiers = new()
                    {
                        MessFactor = 0.75f,
                        SidesModifier = 0.15f,
                        
                    },
                    PatienceModifiers = new()
                    {
                        Eating = -0.5f,
                        FoodDeliverBonus = 0.5f,
                        ProvidesQueuePatienceBoost = true,
                        Seating = 0.5f
                    }
                }
            },
            new StatusEffect()
            {
                Status = PICNIC_STATUS
            }
        };
    }
}
