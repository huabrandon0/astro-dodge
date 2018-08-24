using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    // Reference: Unity3d.College's video "UI Model Display / Character Select UI"
    public class ShopShipSelection : MonoBehaviour
    {
        class Ship
        {
            public Ship(Transform model, bool unlocked, int cost)
            {
                Model = model;
                Unlocked = unlocked;
                Cost = cost;
            }

            public Transform Model;
            public bool Unlocked;
            public int Cost;
        }

        List<Ship> _ships;
        
        [SerializeField] bool[] _unlockedModels;

        int _index = 0;
        int _chosenIndex = 0;

        [System.Serializable]
        public class InvokeEvents
        {
            public GameEventInt ChangeShipIndex;
            public GameEvent ChangeShipFailed;
            public GameEventInt ChangeShipCost;
            public GameEvent ShipLocked;
            public GameEvent ShipUnlocked;
            public GameEvent ShipSelected;
            public GameEvent ShipBought;
            public GameEvent ShipChosen;
        }

        [SerializeField] InvokeEvents _invokeEvents;

        void Awake()
        {
            _ships = new List<Ship>();
            for (int i = 0; i < transform.childCount; i++)
            {
                Ship ship = new Ship(transform.GetChild(i), _unlockedModels[i], 100);
                _ships.Add(ship);
            }

            //_models = new List<Transform>();
            //_unlocked = new List<bool>();
            //for (int i = 0; i < transform.childCount; i++)
            //{
            //    var model = transform.GetChild(i);
            //    _models.Add(model);
            //    _unlocked.Add(_unlockedModels[i]);
            //}
            
            ChangeModel(_index);
        }

        public void Increment()
        {
            int desiredIndex = _index + 1;
            if (desiredIndex >= _ships.Count)
                _index = 0;
            else
                _index = desiredIndex;

            ChangeModel(_index);
        }

        public void Decrement()
        {
            int desiredIndex = _index - 1;
            if (desiredIndex < 0)
                _index = _ships.Count - 1;
            else
                _index = desiredIndex;
            
            ChangeModel(_index);
        }

        void EnableModel(Transform modelTransform)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var transformToToggle = transform.GetChild(i);
                bool shouldBeActive = transformToToggle == modelTransform;

                transformToToggle.gameObject.SetActive(shouldBeActive);
            }
        }

        void ChangeModel(int index)
        {
            if (index < 0 || index >= _ships.Count)
                return;

            if (index == _chosenIndex)
            {
                _invokeEvents.ShipSelected.Invoke();
            }
            else if (_ships[_index].Unlocked)
                _invokeEvents.ShipUnlocked.Invoke();
            else
            {
                _invokeEvents.ShipLocked.Invoke();
                _invokeEvents.ChangeShipCost.Invoke(_ships[_index].Cost);
            }

            EnableModel(_ships[index].Model);
        }

        public void ConfirmSelection()
        {
            if (_ships[_index].Unlocked)
            {
                Debug.Log("new ship! " + _index);
                _chosenIndex = _index;
                _invokeEvents.ChangeShipIndex.Invoke(_index);
                _invokeEvents.ShipChosen.Invoke();
            }
            else
            {
                if (Currency.Instance.Withdraw(_ships[_index].Cost))
                {
                    Debug.Log("wow u bot ship");
                    _ships[_index].Unlocked = true;
                    _invokeEvents.ShipUnlocked.Invoke();
                    _invokeEvents.ShipBought.Invoke();
                }
                else
                {
                    Debug.Log("wow u poor");
                    _invokeEvents.ChangeShipFailed.Invoke();
                }
            }
        }
    }
}
