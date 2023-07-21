using KitchenLib.Utils;
using UnityEngine;

namespace EverythingAlways.Setting.Appliances
{
    public class PicnicFloor : CustomSettingAppliance
    {
        public override string UniqueNameID => "Picnic Floor";
        public override GameObject Prefab => GetPrefab("Ground");

        public override void SetupPrefab(GameObject prefab)
        {
            prefab.ApplyMaterialToChild("Plane", "Grass");
        }
    }
}
