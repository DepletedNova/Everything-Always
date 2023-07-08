using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverythingAlways.Unlocks
{
    internal class BuffetUnlock : CustomUnlockCard
    {
        public override string UniqueNameID => "Buffet Unlock";

        public override bool IsUnlockable => true;
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.LargeDecrease;
        public override UnlockGroup UnlockGroup => UnlockGroup.Generic;
        public override CardType CardType => CardType.Default;

        public override List<UnlockEffect> Effects => new()
        {
            new StatusEffect()
            {
                Status = BUFFET_STATUS
            }
        };

        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Buffet", "Customers can now order sides during the dessert and starter phases.", "Best hope you have metal tables."))
        };
    }
}
