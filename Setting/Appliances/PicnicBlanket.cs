﻿using CustomSettingsAndLayouts;
using EverythingAlways.Randomization;
using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace EverythingAlways.Setting.Appliances
{
    public class PicnicBlanket : CustomAppliance
    {
        public override string UniqueNameID => "Picnic Blanket";
        public override GameObject Prefab => GetPrefab("Picnic Blanket");

        public override OccupancyLayer Layer => OccupancyLayer.Floor;
        public override RarityTier RarityTier => RarityTier.Common;
        public override PriceTier PriceTier => PriceTier.Free;
        public override ShoppingTags ShoppingTags => ShoppingTags.SpecialEvent;
        public override bool IsPurchasable => true;

        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Picnic Blanket", "A pleasant spot to rest!", new(), new()))
        };

        public override void SetupPrefab(GameObject prefab)
        {
            var blanket = prefab.GetChild("blanket");
            blanket.TryAddComponent<Spin>();

            blanket.ApplyMaterial("Plastic - Dark Green", "Plastic - Very Dark Green", "Plastic - White");
            blanket.TryAddComponent<RandomMaterials>();
        }

        public override void OnRegister(Appliance gdo)
        {
            base.OnRegister(gdo);
            Registry.AddSettingDecoration(GetCastedGDO<RestaurantSetting, PicnicSetting>(), gdo);
        }
    }
}
