using Kitchen.Layouts.Modules;
using Kitchen.Layouts;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XNode;

namespace EverythingAlways.Setting
{
    internal class PicnicLayout : CustomLayoutProfile
    {
        public override string UniqueNameID => "Picnic Layout Profile";

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
            new(7, "Output", 10, "Input"),

            new(6, "Output", 8, "Input"),

            new(8, "Output", 9, "Input"),
            new(9, "Output", 10, "AppendFrom"),
            new(10, "Output", 14, "AppendFrom"),

            new(8, "Output", 11, "Input"),
            new(11, "Output", 12, "Input"),
            new(12, "Output", 13, "Input"),
            new(13, "Output", 14, "Input"),

            new(14, "Output", 15, "Input"),
            new(15, "Output", 16, "Input"),
            new(16, "Output", 17, "Input"),
        };

        public override LayoutGraph Graph => CreateLayoutGraph(new() {
                new RoomGrid() // 0
                {
                    Width = 4,
                    Height = 3,
                    Type = RoomType.Unassigned,
                    SetType = false
                },
                new MergeRoomsByType(), // 1
                new PadWithRoom() // 2
                {
                    Right = 3,

                    Left = 0,
                    Above = 0,
                    Below = 0,
                    Type = RoomType.Kitchen,
                },
                new SwapRoomType() // 3
                {
                    Type = RoomType.Dining,
                    X = 0,
                    Y = 0
                },
                new SplitRooms() // 4
                {
                    UniformX = 1,
                    UniformY = 1,
                    RandomX = 0,
                    RandomY = 0,
                },
                new MergeRoomsByType(), // 5
                new RecentreLayout(), // 6

                new CreateFrontDoor() // 7
                {
                    Type = RoomType.Dining,
                    ForceFirstHalf = false
                },

                new FindAllFeatures() // 8
                {
                    Feature = FeatureType.Hatch
                },

                new FilterByFreeSpace(), // 9

                new AppendFeatures(), // 10

                new FilterOnePerPair(), // 11
                new MoveFeatureInDirection() // 12
                {
                    OffsetX = 0,
                    OffsetY = -10,
                },
                new SwitchFeatures() // 13
                {
                    SetToFeature = FeatureType.Door
                },

                new AppendFeatures(), // 14

                new RequireAccessible() // 15
                {
                    AllowGardens = true,
                    ResultStatus = true
                },
                new RequireFeatures() // 16
                {
                    Type = FeatureType.Hatch,
                    Minimum = 3,
                    ResultStatus = true,
                },

                new Output() // 17
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
