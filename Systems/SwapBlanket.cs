using EverythingAlways.Components;
using Kitchen;
using KitchenMods;
using Unity.Entities;

namespace EverythingAlways.Systems
{
    [UpdateBefore(typeof(RotateAppliances))]
    public class SwapBlanket : ApplianceInteractionSystem, IModSystem
    {
        protected override InteractionType RequiredType => InteractionType.Act;

        protected override bool IsPossible(ref InteractionData data) =>
            HasComponent<CBlanket>(data.Target) && HasComponent<CAppliance>(data.Target);

        protected override void Perform(ref InteractionData data)
        {
            var cBlanket = GetComponent<CBlanket>(data.Target);
            cBlanket.Current = (cBlanket.Current + 1) % cBlanket.MaxColors;
            Set(data.Target, cBlanket);
        }
    }
}
