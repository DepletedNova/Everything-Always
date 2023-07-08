using EverythingAlways.Modules;
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
    internal class PicnicLayout : CustomLayoutProfile
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

        public override void OnRegister(LayoutProfile gdo)
        {
            PopulateConnections(gdo.Graph);
        }

        protected virtual List<Connection> Connections { get; set; } = new List<Connection>()
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

            new(8, "Output", 9, "Input"),
            new(9, "Output", 10, "Input"),
            new(10, "Output", 11, "AppendFrom"),
            new(11, "Output", 16, "Input"),

            new(8, "Output", 12, "Input"),
            new(12, "Output", 13, "Input"),
            new(13, "Output", 14, "Input"),
            new(14, "Output", 15, "Input"),
            new(15, "Output", 16, "AppendFrom"),

            new(16, "Output", 17, "Input"),
            new(17, "Output", 18, "Input"),
        };

        public override LayoutGraph Graph => CreateLayoutGraph(new() {
                new RoomGrid() // 0
                {
                    Width = 4,
                    Height = 1,
                    SetType = true,
                    Type = RoomType.Kitchen
                },
                new PadWithRoom() // 1
                {
                    Right = 1,
                    Left = 1,
                    Type = RoomType.Garden
                },
                new PadWithRandomRoom() // 2
                {
                    doHorizontal = true,
                    Distance = 2,
                    Type = RoomType.Dining
                },
                new PadWithRoom() // 3
                {
                    Below = 2,
                    Type = RoomType.Garden
                },
                new MergeRoomsByType(), // 4
                new SplitRooms() // 5
                {
                    UniformX = 1,
                    UniformY = 2,
                    RandomX = 2,
                    RandomY = 2,
                },
                new RecentreLayout(), // 6

                new CreateFrontDoor() // 7
                {
                    Type = RoomType.Garden,
                    ForceFirstHalf = false
                },

                new FindAllFeatures() // 8
                {
                    Feature = FeatureType.Door
                },

                new FilterBySide() // 9
                {
                    Horizontal = true
                },
                new FilterOnePerPair(), // 10

                new AppendFeatures(), // 11

                new FilterByRoom() // 12
                {
                    RemoveMode = false,
                    Type1 = RoomType.Kitchen,
                    FilterSecond = true,
                    Type2 = RoomType.Garden
                },
                new FilterByFreeSpace(), // 13
                new FilterBySide() // 14
                {
                    Horizontal = false
                },
                new SwitchFeatures() // 15
                {
                    SetToFeature = FeatureType.Hatch
                },

                new AppendFeatures(), // 16

                new RequireAccessible() // 17
                {
                    AllowGardens = true,
                    ResultStatus = true
                },
                
                new Output() // 18
            });


        protected struct Connection
        {
            public int FromIndex;
            public string FromPortName;

            public int ToIndex;
            public string ToPortName;

            public Connection(int fromIndex, string fromPortName, int toIndex, string toPortName)
            {
                FromIndex = fromIndex;
                FromPortName = fromPortName;
                ToIndex = toIndex;
                ToPortName = toPortName;
            }
        }

        static FieldInfo f_ports = typeof(Node).GetField("ports", BindingFlags.NonPublic | BindingFlags.Instance);
        private void PopulateConnections(LayoutGraph layoutGraph)
        {
            List<Node> nodes = layoutGraph.nodes;

            foreach (Node node in nodes)
            {
                if (node is LayoutModule layoutModule)
                {
                    layoutModule.graph = layoutGraph;
                }
            }

            foreach (Connection connection in Connections)
            {
                if (!TryGetNodePort(connection.FromIndex, connection.FromPortName, out NodePort fromPort))
                    break;
                if (!TryGetNodePort(connection.ToIndex, connection.ToPortName, out NodePort toPort))
                    break;
                fromPort.Connect(toPort);

                bool TryGetNodePort(int nodeIndex, string portName, out NodePort nodePort)
                {
                    nodePort = null;
                    if (nodeIndex >= nodes.Count)
                    {
                        LogNodeError($"Node index ({nodeIndex}) must be less than the number of nodes ({nodes.Count})");
                        return false;
                    }
                    Node node = nodes[nodeIndex];
                    object obj = f_ports.GetValue(node);
                    if (obj == null || !(obj is Dictionary<string, NodePort> nodeDictionary))
                    {
                        LogNodeError($"Failed to get Node Dictionary from {node.GetType()}");
                        return false;
                    }
                    if (!nodeDictionary.TryGetValue(portName, out nodePort))
                    {
                        Main.LogError($"Failed to get \"{portName}\" port from {node.GetType()}");
                        return false;
                    }
                    return true;
                }

                void LogNodeError(object msg)
                {
                    Main.LogError($"{GetType().FullName} error! {msg}");
                }
            }
        }
    }
}
