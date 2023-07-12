using KitchenData;
using KitchenLib.Customs;
using System.Collections.Generic;

namespace EverythingAlways.Setting
{
    public class PicnicCardsPack : CustomModularUnlockPack
    {
        public override string UniqueNameID => "Picnic Cards Pack";

        public override List<IUnlockSet> Sets => new()
        {
            new UnlockSetAutomatic()
        };

        public override List<IUnlockFilter> Filter => new()
        {
            new FilterBasic()
            {
                AllowBaseDishes = true,
            }
        };

        public override List<IUnlockSorter> Sorters => new()
        {
            new UnlockSorterShuffle(),
            new UnlockSorterPriority()
            {
                PriorityProbability = 0.25f,
                PrioritiseRequirements = true,
                Groups = new()
                {
                    UnlockGroup.Special
                },
                DishTypes = new()
                {
                    DishType.Side,
                    DishType.Dessert,
                    DishType.Starter
                },
            },
        };
        public override List<ConditionalOptions> ConditionalOptions => new()
        {
            new ConditionalOptions
            {
                Selector = new UnlockSelectorGroupChoice()
                {
                    Group1 = UnlockGroup.Dish,
                    Group2 = UnlockGroup.Dish,
                },
                Condition = new UnlockConditionRegular()
                {
                    DayInterval = 3,
                    DayOffset = 0,
                    DayMin = 1,
                    DayMax = -1,
                    TierRequired = -1
                }
            }
        };
    }
}
