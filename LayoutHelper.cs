global using static EverythingAlways.LayoutHelper;
using EverythingAlways.Modules;
using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenData.Workshop;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;

namespace EverythingAlways
{
    internal static class LayoutHelper
    {
        public static LayoutGraph CreateLayoutGraph(List<Node> nodes)
        {
            var graph = ScriptableObject.CreateInstance<LayoutGraph>();
            graph.nodes = nodes;
            return graph;
        }

        public static RoomGrid CreateRoomGrid(int Width, int Height, RoomType Type = RoomType.Dining, bool SetType = true)
        {
            var module = ScriptableObject.CreateInstance<RoomGrid>();
            module.Width = Width;
            module.Height = Height;
            module.Type = Type;
            module.SetType = SetType;
            return module;
        }

        public static PadWithRoom CreatePadWithRoom(int Above, int Left, int Right, int Below, RoomType Type = RoomType.Unassigned)
        {
            var module = ScriptableObject.CreateInstance<PadWithRoom>();
            module.Above = Above;
            module.Left = Left;
            module.Right = Right;
            module.Below = Below;
            module.Type = Type;
            return module;
        }

        public static PadWithRandomRoom CreatePadWithRandomRoom(bool DoHorizontal, bool DoVertical, int Distance, RoomType Type = RoomType.Unassigned)
        {
            var module = ScriptableObject.CreateInstance<PadWithRandomRoom>();
            module.doHorizontal = DoHorizontal;
            module.doVertical = DoVertical;
            module.Distance = Distance;
            module.Type = Type;
            return module;
        }

        public static MergeRoomsByType CreateMergeRoomsByType() => ScriptableObject.CreateInstance<MergeRoomsByType>();

        public static InsertRandomRoom CreateInsertRandomRoom(RoomType Type)
        {
            var module = ScriptableObject.CreateInstance<InsertRandomRoom>();
            module.Type = Type;
            return module;
        }

        public static SplitRooms CreateSplitRooms(int UniformX, int UniformY, int RandomX, int RandomY)
        {
            var module = ScriptableObject.CreateInstance<SplitRooms>();
            module.UniformX = UniformX;
            module.UniformY = UniformY;
            module.RandomX = RandomX;
            module.RandomY = RandomY;
            return module;
        }

        public static SplitLine CreateSplitLine(int Position, bool IsRow, int Count)
        {
            var module = ScriptableObject.CreateInstance<SplitLine>();
            module.Position = Position;
            module.Count = Count;
            module.IsRow = IsRow;
            return module;
        }

        public static RecentreLayout CreateRecentreLayout() => ScriptableObject.CreateInstance<RecentreLayout>();

        public static CreateFrontDoor CreateCreateFrontDoor(RoomType Type, bool ForceFirstHalf)
        {
            var module = ScriptableObject.CreateInstance<CreateFrontDoor>();
            module.Type = Type;
            module.ForceFirstHalf = ForceFirstHalf;
            return module;
        }

        public static FindAllFeatures CreateFindAllFeatures(FeatureType Feature)
        {
            var module = ScriptableObject.CreateInstance<FindAllFeatures>();
            module.Feature = Feature;
            return module;
        }

        public static FilterBySide CreateFilterBySide(bool Horizontal)
        {
            var module = ScriptableObject.CreateInstance<FilterBySide>();
            module.Horizontal = Horizontal;
            return module;
        }

        public static FilterSelectCount CreateFilterSelectCount(int Count)
        {
            var module = ScriptableObject.CreateInstance<FilterSelectCount>();
            module.Count = Count;
            return module;
        }

        public static FilterOnePerPair CreateFilterOnePerPair() => ScriptableObject.CreateInstance<FilterOnePerPair>();

        public static AppendFeatures CreateAppendFeatures() => ScriptableObject.CreateInstance<AppendFeatures>();

        public static FilterByRoom CreateFilterByRoom(bool RemoveMode, RoomType Type1, bool FilterSecond, RoomType Type2 = RoomType.NoRoom)
        {
            var module = ScriptableObject.CreateInstance<FilterByRoom>();
            module.RemoveMode = RemoveMode;
            module.Type1 = Type1;
            module.FilterSecond = FilterSecond;
            module.Type2 = Type2;
            return module;
        }

        public static FilterByFreeSpace CreateFilterByFreeSpace() => ScriptableObject.CreateInstance<FilterByFreeSpace>();

        public static SwitchFeatures CreateSwitchFeatures(FeatureType SetToFeature)
        {
            var module = ScriptableObject.CreateInstance<SwitchFeatures>();
            module.SetToFeature = SetToFeature;
            return module;
        }

        public static RequireAccessible CreateRequireAccessible(bool AllowGardens, bool ResultStatus)
        {
            var module = ScriptableObject.CreateInstance<RequireAccessible>();
            module.AllowGardens = AllowGardens;
            module.ResultStatus = ResultStatus;
            return module;
        }

        public static Output CreateOutput() => ScriptableObject.CreateInstance<Output>();
    }
}
