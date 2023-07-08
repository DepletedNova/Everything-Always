using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using System.Collections.Generic;

namespace EverythingAlways
{
    public abstract class CustomSettingAppliance : CustomAppliance
    {
        public override List<IApplianceProperty> Properties => new()
        {
            new CImmovable(),
            new CStatic(),
        };

        public override bool IsNonInteractive => true;
    }
}
