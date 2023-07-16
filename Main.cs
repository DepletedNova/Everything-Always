global using static EverythingAlways.References;
global using static EverythingAlways.Helper;
global using static KitchenLib.Utils.GDOUtils;
using KitchenData;
using KitchenLib;
using KitchenLib.Customs;
using KitchenLib.Utils;
using KitchenMods;
using System.Reflection;
using UnityEngine;
using KitchenLib.Event;
using EverythingAlways.Setting;
using System.Linq;
using CustomSettingsAndLayouts;
using KitchenLib.References;
using System.Collections.Generic;
using Kitchen;
using Unity.Entities;
using EverythingAlways.Setting.Appliances;

namespace EverythingAlways
{
    public class Main : BaseMod
    {
        public const string GUID = "Nova.EverythingAlways";
        public const string VERSION = "0.2.2";

        public Main() : base(GUID, "Everything Always", "Depleted Supernova#1957", VERSION, ">=1.0.0", Assembly.GetExecutingAssembly()) { instance = this; }

        internal static AssetBundle Bundle;

        protected override void OnPostActivate(Mod mod)
        {
            Bundle = mod.GetPacks<AssetBundleModPack>().SelectMany(e => e.AssetBundles).ToList()[0];

            AddGameData();

            AddMaterials();

            Events.BuildGameDataEvent += delegate (object s, BuildGameDataEventArgs args)
            {
                SetupGardenGate();

                var settingGDO = GetCustomGameDataObject<PicnicSetting>();
                if (settingGDO?.GameDataObject != null)
                {
                    var rSetting = settingGDO.GameDataObject as RestaurantSetting;
                    Registry.GrantCustomSetting(rSetting);
                    Registry.AddSettingLayout(rSetting, new List<LayoutProfile>
                    {
                        GetCastedGDO<LayoutProfile, PicnicLayout>(),
                        GetCastedGDO<LayoutProfile, SidePicnicLayout>(),
                        //GetCastedGDO<LayoutProfile, LargePicnicLayout>()
                    });
                }
            };
        }

        internal void AddMaterials()
        {
            AddMaterial(MaterialUtils.CreateFlat("Light Stone", 0xB7B09C));

            // Picnic
            AddMaterial(MaterialUtils.CreateFlat("Picnic - Blue", 0x2E3C92));
            AddMaterial(MaterialUtils.CreateFlat("Picnic - Light Blue", 0x6F78B3));

            AddMaterial(MaterialUtils.CreateFlat("Picnic - Yellow", 0xE8831E));
            AddMaterial(MaterialUtils.CreateFlat("Picnic - Light Yellow", 0xD8AB3A));
        }

        private static new Main instance;
        public static MenuPhase SidesAvailable(CGroupMealPhase phase)
        {
            if (instance.GetOrCreate<SGlobalStatusList>().Has(BUFFET_STATUS))
                return MenuPhase.Main;
            return phase.Phase;
        }

        internal static GameObject FenceGate;
        internal void SetupGardenGate()
        {
            FenceGate = GetPrefab("Fence Gate");

            FenceGate.ApplyMaterialToChild("Post", "Wood 2");
            for (int i = 0; i < FenceGate.GetChildCount(); i++)
            {
                var child = FenceGate.GetChild(i);
                if (child.name.Contains("Door"))
                {
                    var door = child.GetChild("Door");
                    door.ApplyMaterial("Wood 1", "Wood 2");

                    //var phase = child.TryAddComponent<PhaseWhenStuck>();
                    //phase.Collider = door.GetComponent<BoxCollider>();
                    //phase.Joint = door.GetComponent<HingeJoint>();
                }
            }
        }

        internal void AddGameData()
        {
            MethodInfo AddGDOMethod = typeof(BaseMod).GetMethod(nameof(BaseMod.AddGameDataObject));
            int counter = 0;
            Log("Registering GameDataObjects.");
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsAbstract || typeof(IWontRegister).IsAssignableFrom(type))
                    continue;

                if (typeof(CustomGameDataObject).IsAssignableFrom(type))
                {
                    MethodInfo generic = AddGDOMethod.MakeGenericMethod(type);
                    generic.Invoke(this, null);
                    counter++;
                }
            }
            Log($"Registered {counter} GameDataObjects.");
        }

        #region Logging
        internal static void LogInfo(string msg) { Debug.Log($"[{GUID}] " + msg); }
        internal static void LogWarning(string msg) { Debug.LogWarning($"[{GUID}] " + msg); }
        internal static void LogError(string msg) { Debug.LogError($"[{GUID}] " + msg); }
        internal static void LogInfo(object msg) { LogInfo($"[{GUID}] " + msg.ToString()); }
        internal static void LogWarning(object msg) { LogWarning($"[{GUID}] " + msg.ToString()); }
        internal static void LogError(object msg) { LogError($"[{GUID}] " + msg.ToString()); }
        #endregion
    }

    public interface IWontRegister { }

    public static class References
    {
        public static RestaurantStatus PICNIC_STATUS = (RestaurantStatus)VariousUtils.GetID("Picnic Status");
        public static RestaurantStatus BUFFET_STATUS = (RestaurantStatus)VariousUtils.GetID("Buffet Status");
    }

    public static class Helper
    {
        public static T GetGDO<T>(int id) where T : GameDataObject => (T)GetExistingGDO(id);
        public static GameObject GetPrefab(string name) => Main.Bundle.LoadAsset<GameObject>(name);
    }
}
