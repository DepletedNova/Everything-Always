using HarmonyLib;
using Kitchen;
using Kitchen.Layouts;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace EverythingAlways.Patches
{
    [HarmonyPatch(typeof(LayoutBuilder))]
    internal class LayoutBuilder_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(LayoutBuilder.BuildDoorBetween))]
        public static bool BuildDoorBetween_Prefix(Vector2 tile1, Vector2 tile2, 
            bool is_external, bool is_reversed, bool is_legal_door, bool is_office_door, bool is_trophy_door, bool is_employees_only_door, 
            ref List<Door> ___Doors, ref LayoutBlueprint ___Blueprint, ref Transform ___Parent, ref LayoutBuilder __instance)
        {
            if (!is_external)
                return true;

            if (___Blueprint[tile1].Type != RoomType.Garden && ___Blueprint[tile2].Type != RoomType.Garden)
                return true;

            Vector2 tilePos = (tile1 + tile2) * 0.5f;
            Vector3 localPosition = new(tilePos.x, 0, tilePos.y);

            GameObject gameObject = Object.Instantiate(Main.FenceGate);
            gameObject.transform.parent = ___Parent;
            gameObject.transform.localPosition = localPosition;
            gameObject.transform.localRotation = Quaternion.LookRotation(new Vector3(tile1.x - tile2.x, 0f, tile1.y - tile2.y), Vector3.up);
            gameObject.transform.localScale = Vector3.one;

            Door item = new()
            {
                Tile1 = tile1,
                Tile2 = tile2,
                DoorGameObject = gameObject,
                HatchGameObject = __instance.BuildWallBetween(tile1, tile2, true, false, false, false)
            };
            item.Update(true, true);
            ___Doors.Add(item);

            return false;
        }
    }
}
