using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    public class ShipSelect : Singleton<ShipSelect>
    {
        protected ShipSelect() { }

        [SerializeField] GameObject[] _ships;

        GameObject _currentShip;
        
        [System.Serializable]
        public class ResponseEvents
        {
            public GameEventInt ChangeShipIndex;
        }

        [SerializeField] ResponseEvents _responseEvents;

        void Awake()
        {
            if (_ships.Length <= 0)
                Debug.LogError("nooo!! where are the ships!?");
            
            _responseEvents.ChangeShipIndex.AddListener(SelectShip);
        }

        // Expects non-ship index.
        public void SelectShip(int index)
        {
            if (index < 0 || index >= _ships.Length)
                return;

            if (_currentShip)
                Destroy(_currentShip);

            _currentShip = Instantiate(_ships[index], transform);
        }
    }
}
