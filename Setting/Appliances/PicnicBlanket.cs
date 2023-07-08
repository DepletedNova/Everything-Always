using EverythingAlways.Randomization;
using Kitchen;
using KitchenLib.Utils;
using UnityEngine;

namespace EverythingAlways.Setting.Appliances
{
    public class PicnicBlanket : CustomSettingAppliance
    {
        public override string UniqueNameID => "Picnic Blanket";
        public override GameObject Prefab => GetPrefab("Picnic Blanket");

        public override void SetupPrefab(GameObject prefab)
        {
            var blanket = prefab.GetChild("blanket");
            blanket.TryAddComponent<RandomMaterials>();
            blanket.TryAddComponent<Spin>();
            blanket.TryAddComponent<Jitter>().Distance = 0.25f;
        }
    }
}
