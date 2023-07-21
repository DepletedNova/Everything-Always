using CustomSettingsAndLayouts;
using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace EverythingAlways.Setting.Appliances
{
    public class PicnicBasket : CustomAppliance
    {
        public override string UniqueNameID => "Picnic Basket";
        public override GameObject Prefab => GetPrefab("Picnic Basket");

        public override RarityTier RarityTier => RarityTier.Common;
        public override PriceTier PriceTier => PriceTier.Free;
        public override ShoppingTags ShoppingTags => ShoppingTags.SpecialEvent;
        public override bool IsPurchasable => true;

        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Picnic Basket", "Good for storing!", new(), new()))
        };

        public override List<IApplianceProperty> Properties => new()
        {
            new CItemHolder(),
            new CDisableAutomation()
        };

        public override void SetupPrefab(GameObject prefab)
        {
            prefab.ApplyMaterialToChild("blanket", "Wood 1");
            prefab.TryAddComponent<HoldPointContainer>().HoldPoint = prefab.transform.Find("HoldPoint");
        }

        public override void OnRegister(Appliance gdo)
        {
            base.OnRegister(gdo);
            Registry.AddSettingDecoration(GetCastedGDO<RestaurantSetting, PicnicSetting>(), gdo);
        }
    }
}
