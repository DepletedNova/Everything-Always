using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace EverythingAlways.Randomization
{
    public class RandomFlowers : MonoBehaviour
    {
        private void Start()
        {
            int range = Random.Range(Min, Max);
            for (int i = 0; i < range; i++)
            {
                var circularRandom = Random.insideUnitCircle * Distance;
                var gameObject = Instantiate(Base);
                gameObject.transform.SetParent(transform, false);
                gameObject.transform.localPosition = new Vector3(circularRandom.x, -0.025f, circularRandom.y);
                gameObject.transform.rotation = Quaternion.Euler(0f, (float)Random.Range(0, 360f), 0);
                gameObject.transform.localScale = Random.Range(1f, 1.25f) * Vector3.one;
                gameObject.ApplyMaterial(RandomMaterials[Random.Range(0, RandomMaterials.Count)]);
            }
        }

        public float Distance = 1.5f;
        public int Min = 2;
        public int Max = 6;

        public GameObject Base = GetPrefab("Flower");

        public List<string[]> RandomMaterials = new() {
                new[] { "Plastic - Black", "Plastic - Red" },
                new[] { "Plastic - White", "Plastic - Yellow" },
                new[] { "Plastic - Yellow", "Plastic - White" },
                new[] { "Plastic - White", "Plastic - Blue" },
                new[] { "Plastic - Yellow", "Clothing Pink" },
            };
    }
}
