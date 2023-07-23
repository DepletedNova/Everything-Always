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
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Picnic Basket", "Good for storing!", new(), new())),
            (Locale.Polish, LocalisationUtils.CreateApplianceInfo("Koszyk Piknikowy", "Dobre do przechowywania!", new(), new())),
            (Locale.Turkish, LocalisationUtils.CreateApplianceInfo("Piknik Sepeti", "Saklamak için iyi!", new(), new())),
            (Locale.ChineseSimplified, LocalisationUtils.CreateApplianceInfo("野餐篮子", "适合存放！", new(), new())),
            (Locale.ChineseTraditional, LocalisationUtils.CreateApplianceInfo("野餐籃子", "適合存放！", new(), new())),
            (Locale.French, LocalisationUtils.CreateApplianceInfo("Panier Pique-nique", "Bon pour ranger!", new(), new())),
            (Locale.German, LocalisationUtils.CreateApplianceInfo("Picknickkorb", "Gut zum Aufbewahren!", new(), new())),
            (Locale.Japanese, LocalisationUtils.CreateApplianceInfo("ピクニックバスケット", "収納にいいですね！", new(), new())),
            (Locale.PortugueseBrazil, LocalisationUtils.CreateApplianceInfo("Cesta de Piquenique", "Bom para guardar!", new(), new())),
            (Locale.Russian, LocalisationUtils.CreateApplianceInfo("Корзинка для пикника", "Хорошо для хранения!", new(), new())),
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
