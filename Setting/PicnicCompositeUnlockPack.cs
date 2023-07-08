using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using System.Collections.Generic;

using static KitchenLib.References.ModularUnlockPackReferences;

namespace EverythingAlways.Setting
{
    public class PicnicCompositeUnlockPack : CustomCompositeUnlockPack
    {
        public override string UniqueNameID => "Picnic Composite Unlock Pack";
        public override List<UnlockPack> Packs => new()
        {
            GetGDO<ModularUnlockPack>(JanuaryCards),
            GetGDO<ModularUnlockPack>(FranchiseCardsPack),
            GetGDO<ModularUnlockPack>(ThemeCardsPack),
        };
    }
}
