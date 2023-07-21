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
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Feast", "Customers can order any food at any phase. They'll eat more food at a faster pace and make more mess.", "Oh no"))
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
                        MessFactor = 1.5f,
                        SidesModifier = 0.25f,
                    },
                    PatienceModifiers = new()
                    {
                        Eating = -0.5f,
                        Thinking = 0.15f,
                        Seating = 0.7f
                    }
                }
            },
            new StatusEffect()
            {
                Status = PICNIC_STATUS
            },
            new StatusEffect()
            {
                Status = BUFFET_STATUS
            }
        };
    }
}
