using EverythingAlways.Setting.Appliances;
using Kitchen;
using Kitchen.Layouts;
using KitchenData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EverythingAlways.Setting
{
    public class PicnicDecorator : Decorator
    {
        public class DecorationsConfiguration : IDecorationConfiguration
        {
            public struct Scatter
            {
                public float Probability;

                public Appliance Appliance;
            }

            public List<Scatter> Scatters;

            public Appliance Cobblestone;

            public Appliance Ground;

            public bool OnlyDecorateLowerHalf;

            public IDecorator Decorator => new PicnicDecorator();
        }

        private int PathStartLocation = -20;

        public override bool Decorate(Room room)
        {
            DecorationsConfiguration decorationsConfiguration = Configuration as DecorationsConfiguration;
            if (decorationsConfiguration != null)
            {
                Bounds bounds = Blueprint.GetBounds();
                Vector3 frontDoor = Blueprint.GetFrontDoor();
                NewPiece(decorationsConfiguration.Ground, 0f, 0f);
                for (float x1 = bounds.min.x - 4f; x1 <= bounds.max.x + 4f; x1 += 1f)
                {
                    foreach (DecorationsConfiguration.Scatter scatter in decorationsConfiguration.Scatters)
                    {
                        if (Random.value < scatter.Probability)
                        {
                            NewPiece(scatter.Appliance, x1, bounds.min.y - 6f);
                        }

                        if (!decorationsConfiguration.OnlyDecorateLowerHalf && Random.value < scatter.Probability)
                        {
                            NewPiece(scatter.Appliance, x1, bounds.max.y + 3f);
                        }
                    }
                }

                for (float y1 = bounds.min.y - 2f; y1 <= bounds.max.y + 2f; y1 += 1f)
                {
                    if (decorationsConfiguration.OnlyDecorateLowerHalf && y1 >= 0f)
                    {
                        continue;
                    }

                    foreach (DecorationsConfiguration.Scatter scatter2 in decorationsConfiguration.Scatters)
                    {
                        if (y1 > bounds.min.y && Random.value < scatter2.Probability)
                        {
                            NewPiece(scatter2.Appliance, bounds.min.x - 3f, y1);
                        }

                        if (Random.value < scatter2.Probability)
                        {
                            NewPiece(scatter2.Appliance, bounds.max.x + 4f, y1);
                        }
                    }
                }

                if (decorationsConfiguration.Cobblestone != null)
                {
                    for (float x2 = PathStartLocation; x2 <= frontDoor.x; x2 += 0.8f)
                    {
                        NewPiece(decorationsConfiguration.Cobblestone, x2, bounds.min.y - 1.2f);
                    }
                }

                for (float x3 = bounds.min.x - 1f; x3 <= bounds.max.x + 1f; x3 += 1f)
                {
                    NewPiece(AssetReference.OutdoorMovementBlocker, x3, bounds.min.y - 3f);
                }

                /*var blanket = GetCastedGDO<Appliance, PicnicBlanket>();
                NewPiece(blanket, bounds.min.x + bounds.size.x * 0.2f, bounds.min.y + 2.75f + Random.Range(-1f, 1f));
                NewPiece(blanket, bounds.min.x + bounds.size.x * 0.5f, bounds.min.y + 2.75f + Random.Range(-1f, 1f));
                NewPiece(blanket, bounds.min.x + bounds.size.x * 0.8f, bounds.min.y + 2.75f + Random.Range(-1f, 1f));*/

                NewPiece(AssetReference.OutdoorMovementBlocker, bounds.min.x - 1f, bounds.min.y - 1f);
                NewPiece(AssetReference.OutdoorMovementBlocker, bounds.min.x - 1f, bounds.min.y - 2f);
                NewPiece(AssetReference.OutdoorMovementBlocker, bounds.max.x + 1f, bounds.min.y - 1f);
                NewPiece(AssetReference.OutdoorMovementBlocker, bounds.max.x + 1f, bounds.min.y - 2f);
                NewPiece(Profile.ExternalBin, frontDoor.x, frontDoor.z - 3f);   

                Decorations.Add(new CLayoutAppliancePlacement
                {
                    Appliance = AssetReference.Nameplate,
                    Position = new Vector3((frontDoor.x < 3f) ? (frontDoor.x + 1f) : (frontDoor.x - 1f), -0.65f, bounds.min.y - 1f),
                    Rotation = Quaternion.identity,
                });

                return true;
            }

            return false;
        }
    }
}
