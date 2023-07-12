using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using System.Collections.Generic;

namespace EverythingAlways.Setting
{
    internal class SidePicnicLayout : CustomLayout
    {
        public override string UniqueNameID => "Side Picnic Layout Profile";

        public override int MaximumTables => 2;

        public override GameDataObject Table => GetExistingGDO(ApplianceReferences.TableLarge);
        public override GameDataObject Counter => GetExistingGDO(ApplianceReferences.Countertop);
        public override Appliance ExternalBin => GetGDO<Appliance>(ApplianceReferences.WheelieBin);
        public override Appliance WallPiece => GetGDO<Appliance>(ApplianceReferences.WallPiece);
        public override Appliance InternalWallPiece => GetGDO<Appliance>(ApplianceReferences.InternalWallPiece);
        public override Appliance StreetPiece => GetGDO<Appliance>(ApplianceReferences.StreetPiece);

        public override List<GameDataObject> RequiredAppliances => new()
        {
            GetExistingGDO(ApplianceReferences.Countertop),
            GetExistingGDO(ApplianceReferences.SinkStarting),
            GetExistingGDO(ApplianceReferences.BinStarting),
            GetExistingGDO(ApplianceReferences.BlueprintCabinet),
            GetExistingGDO(ApplianceReferences.ItemSourceReservation),
            GetExistingGDO(ApplianceReferences.ItemSourceReservation),
            GetExistingGDO(ApplianceReferences.ItemSourceReservation),
            GetExistingGDO(ApplianceReferences.ItemSourceReservation),
            GetExistingGDO(ApplianceReferences.ItemSourceReservation),
            GetExistingGDO(ApplianceReferences.ItemSourceReservation),
        };

        protected override List<Connection> Connections => new List<Connection>()
        {
            new(0, "Output", 1, "Input"),
            new(1, "Output", 2, "Input"),
            new(2, "Output", 3, "Input"),
            new(3, "Output", 4, "Input"),
            new(4, "Output", 5, "Input"),
            new(5, "Output", 6, "Input"),

            new(6, "Output", 7, "Input"),
            new(7, "Output", 11, "Input"),

            new(6, "Output", 8, "Input"),

            new(8, "Output", 9, "Input"),
            new(9, "Output", 10, "Input"),
            new(10, "Output", 11, "AppendFrom"),

            new(11, "Output", 14, "Input"),

            new(8, "Output", 12, "Input"),
            new(12, "Output", 13, "Input"),
            new(13, "Output", 14, "AppendFrom"),

            new(14, "Output", 15, "Input"),
            new(15, "Output", 16, "Input"),
        };

        public override LayoutGraph Graph => CreateLayoutGraph(new() {
                CreateRoomGrid(1, 2, RoomType.Garden), // 0
                CreateInsertRandomRoom(RoomType.Dining), // 1
                CreatePadWithRandomRoom(true, false, 1, RoomType.Garden), // 2
                CreatePadWithRandomRoom(true, false, 1, RoomType.Kitchen), // 3
                CreateSplitRooms(2, 3, 4, 8), // 4
                CreateMergeRoomsByType(), // 5
                CreateRecentreLayout(), // 6

                CreateCreateFrontDoor(RoomType.Garden, false), // 7

                CreateFindAllFeatures(FeatureType.Hatch), // 8

                CreateFilterOnePerPair(), // 9
                CreateSwitchFeatures(FeatureType.Door), // 10

                CreateAppendFeatures(), // 11

                CreateFilterByRoom(false, RoomType.Kitchen, true, RoomType.Garden), // 12
                CreateFilterBySide(true), // 13
                
                CreateAppendFeatures(), // 14

                CreateRequireAccessible(true, true), // 15
                CreateOutput(), // 16
            });
    }
}
