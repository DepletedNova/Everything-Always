﻿using EverythingAlways.Modules;
using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using System.Collections.Generic;
using System.Reflection;
using XNode;

namespace EverythingAlways.Setting
{
    internal class PicnicLayout : CustomLayout
    {
        public override string UniqueNameID => "Picnic Layout Profile";

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
            //new(2, "Output", 3, "Input"),
            new(2, "Output", 4, "Input"),
            new(4, "Output", 5, "Input"),
            new(5, "Output", 6, "Input"),

            new(6, "Output", 7, "Input"),
            new(7, "Output", 11, "Input"),

            new(6, "Output", 8, "Input"),
            new(8, "Output", 9, "Input"),

            new(8, "Output", 9, "Input"),
            new(9, "Output", 10, "Input"),
            new(10, "Output", 11, "AppendFrom"),
            new(11, "Output", 16, "Input"),

            new(8, "Output", 12, "Input"),
            new(12, "Output", 14, "Input"),
            //new(13, "Output", 14, "Input"),
            new(14, "Output", 15, "Input"),
            new(15, "Output", 16, "AppendFrom"),

            new(16, "Output", 17, "Input"),
            new(17, "Output", 18, "Input"),
        };

        public override LayoutGraph Graph => CreateLayoutGraph(new() {
                CreateRoomGrid(3, 2, RoomType.Kitchen), // 0
                CreatePadWithRandomRoom(true, false, 2, RoomType.Dining), // 1
                CreatePadWithRoom(0, 1, 1, 3, RoomType.Garden), // 2
                CreatePadWithRoom(0, 0, 0, 3, RoomType.Garden), // 3
                CreateMergeRoomsByType(), // 4
                CreateSplitRooms(1, 1, 4, 4), // 5
                CreateRecentreLayout(), // 6

                CreateCreateFrontDoor(RoomType.Garden, true), // 7

                CreateFindAllFeatures(FeatureType.Door), // 8

                CreateFilterBySide(true), // 9
                CreateFilterOnePerPair(), // 10

                CreateAppendFeatures(), // 11

                CreateFilterByRoom(false, RoomType.Kitchen, true, RoomType.Garden), // 12
                CreateFilterByFreeSpace(), // 13
                CreateFilterBySide(false), // 14
                CreateSwitchFeatures(FeatureType.Hatch), // 15

                CreateAppendFeatures(), // 16

                CreateRequireAccessible(true, true), // 17
                
                CreateOutput() // 18
            });
    }
}
