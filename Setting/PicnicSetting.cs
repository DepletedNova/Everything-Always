using EverythingAlways.Setting.Appliances;
using EverythingAlways.Unlocks;
using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EverythingAlways.Setting
{
    internal class PicnicSetting : CustomRestaurantSetting
    {
        public override string UniqueNameID => "Picnic Setting";
        public override GameObject Prefab => GetPrefab("Picnic Snowglobe");
        public override WeatherMode WeatherMode => WeatherMode.Wind;
        public override bool AlwaysLight => true;
        public override Unlock StartingUnlock => GetCastedGDO<Unlock, PicnicUnlock>();
        public override UnlockPack UnlockPack => GetCastedGDO<CompositeUnlockPack, PicnicCompositeUnlockPack>();
        public override List<IDecorationConfiguration> Decorators => new()
        {
            new PicnicDecorator.DecorationsConfiguration()
            {
                Ground = GetCastedGDO<Appliance, PicnicFloor>(),
                OnlyDecorateLowerHalf = false,
                Cobblestone = GetGDO<Appliance>(ApplianceReferences.Cobblestone),
                Scatters = new()
                {
                    new()
                    {
                        Appliance = GetGDO<Appliance>(ApplianceReferences.Rock),
                        Probability = 0.1f
                    },
                    new()
                    {
                        Appliance = GetGDO<Appliance>(ApplianceReferences.Tree),
                        Probability = 0.15f
                    },
                    new()
                    {
                        Appliance = GetCastedGDO<Appliance, PicnicFlower>(),
                        Probability = 0.5f
                    },
                    new()
                    {
                        Appliance = GetCastedGDO<Appliance, BackdropPicnicBlanket>(),
                        Probability = 0.05f
                    },
                }
            }
        };

        public override List<(Locale, BasicInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateBasicInfo("Picnic", ""))
        };

        public override void OnRegister(RestaurantSetting gdo)
        {
            Prefab.ApplyMaterialToChild("Base", "Grass");
            Prefab.ApplyMaterialToChild("Extras", "Plastic - White", "Plastic - Red", "Wood 1");
        }
    }
}
