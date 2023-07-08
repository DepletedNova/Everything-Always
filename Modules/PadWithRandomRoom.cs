using Kitchen.Layouts;
using Kitchen.Layouts.Modules;
using System.Collections.Generic;
using UnityEngine;

namespace EverythingAlways.Modules
{
    internal class PadWithRandomRoom : LayoutModule
    {
        public RoomType Type;

        public bool doHorizontal;
        public bool doVertical;

        public int Distance;

        public override void ActOn(LayoutBlueprint blueprint)
        {
            Dictionary<LayoutPosition, Room> dictionary = new Dictionary<LayoutPosition, Room>();
            Bounds bounds = blueprint.GetBounds();
            bounds.Expand(0.1f);
            Room value = new Room(Type);
            int horizontalRandom = doHorizontal ? Random.Range(0, 2) * 2 - 1 : 0;
            int verticalRandom = doVertical ? Random.Range(0, 2) * 2 - 1 : 0;
            for (int i = (int)bounds.min.x - (horizontalRandom == -1 ? Distance : 0); i <= (int)bounds.max.x + (horizontalRandom == 1 ? Distance : 0); i++)
            {
                for (int j = (int)bounds.min.y - (verticalRandom == -1 ? Distance : 0); j <= (int)bounds.max.y + (verticalRandom == 1 ? Distance : 0); j++)
                {
                    if (bounds.Contains(new Vector3(i, j, 0f)))
                    {
                        dictionary[new LayoutPosition(i, j)] = blueprint[i, j];
                    }
                    else
                    {
                        dictionary[new LayoutPosition(i, j)] = value;
                    }
                }
            }

            blueprint.Tiles = dictionary;
        }
    }
}
