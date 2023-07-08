using HarmonyLib;
using Kitchen;
using KitchenData;
using KitchenLib.Utils;
using Unity.Collections;
using UnityEngine;

namespace EverythingAlways.Patches
{
    [HarmonyPatch(typeof(AssignMenuRequests))]
    public class AssignMenuRequests_Patch
    {
        private static object[] parameters = new object[] { PICNIC_STATUS };

        [HarmonyPrefix]
        [HarmonyPatch(nameof(AssignMenuRequests.PickRandomMenuItem))]
        public static bool PickRandomMenuItem_Prefix(NativeArray<CMenuItem> items, MenuPhase phase, ref int __result, ref AssignMenuRequests __instance)
        {
            if (!(bool)ReflectionUtils.GetMethod<GameSystemBase>("HasStatus").Invoke(__instance, parameters))
                return true;

            float maxWeight = 0f;
            foreach (CMenuItem cMenuItem in items)
            {
                maxWeight += cMenuItem.Weight;
            }

            float weightCounter = Random.Range(0f, maxWeight);
            for (int i = 0; i < items.Length; i++)
            {
                weightCounter -= items[i].Weight;
                if (weightCounter <= 0f)
                {
                    __result = i;
                    return false;
                }
            }

            __result = -1;
            return false;
        }
    }
}
