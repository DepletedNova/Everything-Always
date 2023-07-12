using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace EverythingAlways.Setting.Appliances
{
    public class PicnicBalloons : CustomAppliance
    {
        public override string UniqueNameID => "Picnic Balloons";
        public override GameObject Prefab => GetPrefab("Picnic Balloons");

        public override OccupancyLayer Layer => OccupancyLayer.Floor;
        public override RarityTier RarityTier => RarityTier.Common;
        public override PriceTier PriceTier => PriceTier.Free;
        public override ShoppingTags ShoppingTags => ShoppingTags.SpecialEvent;

        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Picnic Balloons", "Colorful!", new(), new()))
        };

        public override void SetupPrefab(GameObject prefab)
        {
            prefab.ApplyMaterialToChild("Balloons", "Plastic - Yellow", "Plastic - Blue", "Plastic - Red");
            prefab.ApplyMaterialToChild("Strand", "Plastic - White", "Wood 1");
        }
    }
}
