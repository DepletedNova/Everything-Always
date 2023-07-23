using CustomSettingsAndLayouts;
using EverythingAlways.Components;
using EverythingAlways.Randomization;
using EverythingAlways.Views;
using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using System.Linq;
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
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Picnic Blanket", "Rotating the blanket will change its color!", new(), new())),
            (Locale.Polish, LocalisationUtils.CreateApplianceInfo("Koc Piknikowy", "Obracanie koca zmieni jego kolor", new(), new())),
            (Locale.Turkish, LocalisationUtils.CreateApplianceInfo("Piknik Battaniyesi", "Battaniyeyi döndürmek rengini değiştirir", new(), new())),
            (Locale.ChineseSimplified, LocalisationUtils.CreateApplianceInfo("野餐毯", "旋转毯子会改变颜色", new(), new())),
            (Locale.ChineseTraditional, LocalisationUtils.CreateApplianceInfo("野餐毯", "旋轉毯子會改變顏色", new(), new())),
            (Locale.French, LocalisationUtils.CreateApplianceInfo("Nappe de Pique-nique", "La rotation de la couverture changera sa couleur", new(), new())),
            (Locale.German, LocalisationUtils.CreateApplianceInfo("Picknickdecke", "Durch Drehen der Decke ändert sich ihre Farbe", new(), new())),
            (Locale.Japanese, LocalisationUtils.CreateApplianceInfo("ピクニックブランケット", "", new(), new())),
            (Locale.PortugueseBrazil, LocalisationUtils.CreateApplianceInfo("Toalha de Piquenique", "Girar o cobertor mudará sua cor", new(), new())),
            (Locale.Russian, LocalisationUtils.CreateApplianceInfo("Одеяло для пикника", "Вращение одеяла изменит его цвет", new(), new())),
        };

        public override void SetupPrefab(GameObject prefab)
        {
            var blanket = prefab.GetChild("blanket");
            blanket.ApplyMaterial("Picnic - Light Blue", "Picnic - Blue", "Plastic - White");

            var view = prefab.TryAddComponent<BlanketView>();
            view.Object = blanket;
        }

        public override void OnRegister(Appliance gdo)
        {
            Registry.AddSettingDecoration(GetCastedGDO<RestaurantSetting, PicnicSetting>(), gdo);
        }

        public override List<IApplianceProperty> Properties => new()
        {
            new CBlanket
            {
                MaxColors = 4,
            }
        };
    }
}
