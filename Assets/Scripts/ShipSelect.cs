﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidRage.Game
{
    public class ShipSelect : MonoBehaviour
    {
        [SerializeField] GameObject[] _ships;

        GameObject _currentShip;

        void Awake()
        {
            if (_ships.Length <= 0)
            {
                Debug.LogError("nooo!!");
            }

            SelectShipRandom();
        }

        public void SelectShip(int index)
        {
            if (index < 0 || index >= _ships.Length)
                return;

            if (_currentShip)
                Destroy(_currentShip);

            _currentShip = Instantiate(_ships[index], transform);
        }

        public void SelectShipRandom()
        {
            int index = Random.Range(0, _ships.Length);
            SelectShip(index);
        }
    }
}
