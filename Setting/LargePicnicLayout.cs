using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using System.Collections.Generic;

namespace EverythingAlways.Setting
{
    internal class LargePicnicLayout : CustomLayout, IWontRegister
    {
        public override string UniqueNameID => "Large Picnic Layout Profile";

        public override int MaximumTables => 1;

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
            new(5, "Output", 9, "Input"),

            new(4, "Output", 6, "Input"),
            new(6, "Output", 7, "Input"),

            new(7, "Output", 8, "Input"),
            new(8, "Output", 9, "AppendFrom"),
            new(9, "Output", 12, "Input"),

            new(7, "Output", 10, "Input"),
            new(10, "Output", 11, "Input"),
            new(11, "Output", 12, "AppendFrom"),

            //new(12, "Output", 13, "Input"),
            new(12, "Output", 14, "Input"),
        };

        public override LayoutGraph Graph => CreateLayoutGraph(new() {
                CreateRoomGrid(3, 2, RoomType.Kitchen), // 0
                CreatePadWithRoom(3, 3, 3, 3, RoomType.Garden), // 1
                CreateMergeRoomsByType(), // 2
                CreateSplitRooms(1, 1, 0, 0), // 3
                CreateRecentreLayout(), // 4
                
                CreateCreateFrontDoor(RoomType.Garden, true), // 5

                CreateFindAllFeatures(FeatureType.Door), // 6
                CreateFilterByRoom(true, RoomType.NoRoom, false), // 7

                CreateFilterOnePerPair(), // 8
                CreateAppendFeatures(), // 9

                CreateFilterByFreeSpace(), // 10
                CreateSwitchFeatures(FeatureType.Hatch), // 11
                CreateAppendFeatures(), // 12

                CreateRequireAccessible(true, true), // 13
                CreateOutput(), // 14
            });
    }
}
