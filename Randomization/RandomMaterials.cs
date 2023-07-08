using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace EverythingAlways.Randomization
{
    public class RandomMaterials : MonoBehaviour
    {
        private void Start()
        {
            gameObject.ApplyMaterial(Materials[Random.Range(0, Materials.Count)]);
        }

        public List<string[]> Materials = new()
        {
            new[] { "Picnic - Light Yellow", "Picnic - Yellow", "Plastic - White" },
            new[] { "Picnic - Light Blue", "Picnic - Blue", "Plastic - White" },
            new[] { "Plastic - Dark Green", "Plastic - Very Dark Green", "Plastic - White" },
            new[] { "Paint - Deep Red", "Clothing Red", "Plastic - White" },
        };
    }
}
