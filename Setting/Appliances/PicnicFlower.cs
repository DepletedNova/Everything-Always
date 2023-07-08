using EverythingAlways.Randomization;
using KitchenLib.Utils;
using UnityEngine;

namespace EverythingAlways.Setting.Appliances
{
    public class PicnicFlower : CustomSettingAppliance
    {
        public override string UniqueNameID => "Picnic Flower";
        public override GameObject Prefab => GetPrefab("Flowers");

        public override void SetupPrefab(GameObject prefab)
        {
            var random = prefab.AddComponent<RandomFlowers>();
        }
    }
}
