using HarmonyLib;
using Kitchen;
using KitchenLib.Utils;

namespace EverythingAlways.Patches
{
    [HarmonyPatch(typeof(GrantNecessaryAppliances))]
    internal class GrantNecessaryAppliances_Patch
    {
        private static object[] parameters = new object[] { PICNIC_STATUS };

        [HarmonyPostfix]
        [HarmonyPatch("TotalPlates")]
        public static void TotalPlates_Postfix(ref int __result, ref GrantNecessaryAppliances __instance)
        {
            if ((bool)ReflectionUtils.GetMethod<GameSystemBase>("HasStatus").Invoke(__instance, parameters))
            {
                __result /= 2;
            }
        }
    }
}
