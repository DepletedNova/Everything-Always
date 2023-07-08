global using static EverythingAlways.LayoutHelper;

using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
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

        public static RoomGrid CreateRoomGrid(int width, int height, RoomType room = RoomType.Dining)
        {
            var roomgrid = ScriptableObject.CreateInstance<RoomGrid>();
            roomgrid.Width = width;
            roomgrid.Height = height;
            roomgrid.Type = room;
            roomgrid.SetType = true;
            return roomgrid;
        }

        public static PadWithRoom CreatePadWithRoom(int bottom, int left, int top, int right, RoomType room)
        {
            var pad = ScriptableObject.CreateInstance<PadWithRoom>();
            pad.Below = bottom;
            pad.Left = left;
            pad.Above = top;
            pad.Right = right;
            pad.Type = room;
            return pad;
        }

        public static MergeRoomsByType CreateMergeRoomsByType() => ScriptableObject.CreateInstance<MergeRoomsByType>();

        public static SwapRoomType CreateSwapRoomType(int x, int y, RoomType room)
        {
            var swaproom = ScriptableObject.CreateInstance<SwapRoomType>();
            swaproom.X = x;
            swaproom.Y = y;
            swaproom.Type = room;
            return swaproom;
        }

        public static SplitRooms CreateSplitRooms(int UniformX, int UniformY, int RandomX, int RandomY)
        {
            var split = ScriptableObject.CreateInstance<SplitRooms>();
            split.UniformX = UniformX;
            split.UniformY = UniformY;
            split.RandomX = RandomX;
            split.RandomY = RandomY;
            return split;
        }

        public static FindAllFeatures CreateFindAllFeatures() => ScriptableObject.CreateInstance<FindAllFeatures>();

        public static CreateFrontDoor CreateCreateFrontDoor(RoomType room, bool ForceFirstHalf)
        {
            var door = ScriptableObject.CreateInstance<CreateFrontDoor>();
            door.Type = room;
            door.ForceFirstHalf = ForceFirstHalf;
            return door;
        }

        public static FilterByFreeSpace CreateFilterByFreeSpace() => ScriptableObject.CreateInstance<FilterByFreeSpace>();

        public static FilterOnePerPair CreateFilterOnePerPair() => ScriptableObject.CreateInstance<FilterOnePerPair>();

        public static MoveFeatureInDirection CreateMoveFeatureInDirection(int OffsetX, int OffsetY)
        {
            var move = ScriptableObject.CreateInstance<MoveFeatureInDirection>();
            move.OffsetX = OffsetX;
            move.OffsetY = OffsetY;
            return move;
        }

        public static SwitchFeatures CreateSwitchFeatures(FeatureType feature)
        {
            var switchFeature = ScriptableObject.CreateInstance<SwitchFeatures>();
            switchFeature.SetToFeature = feature;
            return switchFeature;
        }

        public static AppendFeatures CreateAppendFeatures() => ScriptableObject.CreateInstance<AppendFeatures>();

        public static RecentreLayout CreateRecentreLayout() => ScriptableObject.CreateInstance<RecentreLayout>();

        public static Output CreateOutput() => ScriptableObject.CreateInstance<Output>();
    }
}
