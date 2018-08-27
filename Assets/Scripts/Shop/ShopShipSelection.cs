using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    // Reference: Unity3d.College's video "UI Model Display / Character Select UI"
    public class ShopShipSelection : MonoBehaviour
    {
        [System.Serializable]
        class Ship
        {
            public Ship(Transform model, string name, bool unlocked, int cost)
            {
                Model = model;
                Name = name;
                Unlocked = unlocked;
                Cost = cost;
            }
            
            public Transform Model;
            public string Name;
            public bool Unlocked;
            public int Cost;
        }

        [SerializeField] Ship[] _ships;

        int _index = 0;
        int _chosenIndex = 0;

        [System.Serializable]
        public class InvokeEvents
        {
            public GameEventInt ChangeShipIndex;
            public GameEvent ChangeShipFailed;
            public GameEventInt ChangeShipCost;
            public GameEventString ChangeShipName;
            public GameEvent ShipLocked;
            public GameEvent ShipUnlocked;
            public GameEvent ShipSelected;
            public GameEvent ShipBought;
            public GameEvent ShipChosen;
        }

        [SerializeField] InvokeEvents _invokeEvents;
        
        //_ships = new List<Ship>();
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    Ship ship = new Ship(transform.GetChild(i), _unlockedModels[i], 100);
        //    _ships.Add(ship);
        //}

        //_models = new List<Transform>();
        //_unlocked = new List<bool>();
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    var model = transform.GetChild(i);
        //    _models.Add(model);
        //    _unlocked.Add(_unlockedModels[i]);
        //}

        void Start()
        {
            ChangeModel(_index);
        }

        public void Increment()
        {
            int desiredIndex = _index + 1;
            if (desiredIndex >= _ships.Length)
                _index = 0;
            else
                _index = desiredIndex;

            ChangeModel(_index);
        }

        public void Decrement()
        {
            int desiredIndex = _index - 1;
            if (desiredIndex < 0)
                _index = _ships.Length - 1;
            else
                _index = desiredIndex;
            
            ChangeModel(_index);
        }

        void EnableModel(Ship ship)
        {
            foreach (Ship shipe in _ships)
            {
                if (ship != shipe)
                    shipe.Model.gameObject.SetActive(false);
                else
                    ship.Model.gameObject.SetActive(true);
            }

            //for (int i = 0; i < transform.childCount; i++)
            //{
            //    var transformToToggle = _ships[i].Model;
            //    bool shouldBeActive = transformToToggle == modelTransform;

            //    transformToToggle.gameObject.SetActive(shouldBeActive);
            //}
        }

        void ChangeModel(int index)
        {
            if (index < 0 || index >= _ships.Length)
                return;

            //Debug.Log("changing to " + _ships[index].Name);

            _invokeEvents.ChangeShipName.Invoke(_ships[index].Name);

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

            EnableModel(_ships[index]);
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
