using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidRage.Game
{
    [CreateAssetMenu(menuName = "Data/ShipInfo")]
    public class ShipInfo : ScriptableObject
    {
        public string Name;
        public int Cost;
    }
}
