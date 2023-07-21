using EverythingAlways.Components;
using Kitchen;
using KitchenLib.Utils;
using KitchenMods;
using MessagePack;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

using static KitchenLib.Utils.MaterialUtils;

namespace EverythingAlways.Views
{
    public class BlanketView : UpdatableObjectView<BlanketView.ViewData>
    {
        public List<Material[]> BlanketColors = new()
        {
            GetMaterialArray("Picnic - Light Yellow", "Picnic - Yellow", "Plastic - White"),
            GetMaterialArray("Picnic - Light Blue", "Picnic - Blue", "Plastic - White"),
            GetMaterialArray("Plastic - Dark Green", "Plastic - Very Dark Green", "Plastic - White"),
            GetMaterialArray("Paint - Deep Red", "Clothing Red", "Plastic - White"),
        };
        public GameObject Object;

        private ViewData Data;

        protected override void UpdateData(ViewData data)
        {
            Data = data;

            if (BlanketColors != null && Data.Current < BlanketColors.Count)
                Object.ApplyMaterial(BlanketColors[Data.Current]);
        }

        private class UpdateView : IncrementalViewSystemBase<ViewData>, IModSystem
        {
            private EntityQuery Query;
            protected override void Initialise()
            {
                Query = GetEntityQuery(new QueryHelper()
                    .All(typeof(CBlanket), typeof(CAppliance), typeof(CLinkedView)));
            }

            protected override void OnUpdate()
            {
                var entities = Query.ToEntityArray(Allocator.Temp);
                foreach (var entity in entities)
                {
                    var cBlanket = GetComponent<CBlanket>(entity);
                    var cView = GetComponent<CLinkedView>(entity);

                    SendUpdate(cView, new()
                    {
                        Current = cBlanket.Current
                    }, MessageType.SpecificViewUpdate);
                }
                entities.Dispose();
            }
        }

        [MessagePackObject(false)]
        public struct ViewData : ISpecificViewData, IViewData.ICheckForChanges<ViewData>
        {
            [Key(0)] public int Current;

            public IUpdatableObject GetRelevantSubview(IObjectView view) => view.GetSubView<BlanketView>();

            public bool IsChangedFrom(ViewData check) => check.Current != Current;
        }
    }
}
