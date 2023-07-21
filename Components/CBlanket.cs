using KitchenData;
using KitchenMods;
using Unity.Entities;
using UnityEngine;

namespace EverythingAlways.Components
{
    public struct CBlanket : IApplianceProperty, IModComponent, IAttachmentLogic
    {
        public int Current;
        public int MaxColors;

        public void Attach(EntityManager em, EntityCommandBuffer ecb, Entity e)
        {
            Current = Random.Range(0, MaxColors);
            ecb.AddComponent(e, this);
        }
    }
}
