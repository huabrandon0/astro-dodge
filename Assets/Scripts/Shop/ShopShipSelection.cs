using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;
using AsteroidRage.Data;

namespace AsteroidRage.Game
{
    // Reference: Unity3d.College's video "UI Model Display / Character Select UI"
    public class ShopShipSelection : MonoBehaviour
    {
        [System.Serializable]
        class Ship
        {
            public Ship(Transform model, string name, int cost)
            {
                Model = model;
                Name = name;
                Cost = cost;
            }
            
            public Transform Model;
            public string Name;
            public int Cost;
        }

        [SerializeField] Ship[] _ships;
        bool[] _unlockedShips;

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
            public GameEvent CanAffordShip;
            public GameEvent CannotAffordShip;
        }

        [SerializeField] InvokeEvents _invokeEvents;
        
        void Start()
        {
            GameData loadedGameData = GameDataManager.Instance.GetGameData();
            Load(loadedGameData);
        }

        public void Load(GameData gameData)
        {
            _unlockedShips = new bool[_ships.Length];
            bool[] unlockedShips = gameData.UnlockedShips;

            if (unlockedShips != null && unlockedShips.Length > 0)
            {
                int lower = Mathf.Min(_unlockedShips.Length, unlockedShips.Length);

                for (int i = 0; i < lower; i++)
                {
                    _unlockedShips[i] = unlockedShips[i];
                }
            }
            else if (_unlockedShips.Length > 0)
            {
                _unlockedShips[0] = true;

                for (int i = 1; i < _unlockedShips.Length; i++)
                {
                    _unlockedShips[i] = false;
                }
            }
            
            _index = _chosenIndex = gameData.LastChosenShip;
            _invokeEvents.ChangeShipIndex.Invoke(_index);
        }

        public void ShowModels()
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
        }

        void ChangeModel(int index)
        {
            if (index < 0 || index >= _ships.Length)
                return;

            _invokeEvents.ChangeShipName.Invoke(_ships[index].Name);

            if (index == _chosenIndex)
            {
                _invokeEvents.ShipSelected.Invoke();
            }
            else if (_unlockedShips[index])
                _invokeEvents.ShipUnlocked.Invoke();
            else
            {
                _invokeEvents.ShipLocked.Invoke();
                _invokeEvents.ChangeShipCost.Invoke(_ships[index].Cost);

                if (Currency.Instance.GetCurrency() >= _ships[index].Cost)
                    _invokeEvents.CanAffordShip.Invoke();
                else
                    _invokeEvents.CannotAffordShip.Invoke();
            }

            EnableModel(_ships[index]);
        }

        public void ConfirmSelection()
        {
            if (_unlockedShips[_index])
            {
                _chosenIndex = _index;
                _invokeEvents.ChangeShipIndex.Invoke(_index);
                _invokeEvents.ShipChosen.Invoke();
                GameDataManager.Instance.UpdateLastChosenShip(_chosenIndex);
                GameDataManager.Instance.SaveGame();
            }
            else
            {
                if (Currency.Instance.Withdraw(_ships[_index].Cost))
                {
                    _unlockedShips[_index] = true;
                    _invokeEvents.ShipUnlocked.Invoke();
                    _invokeEvents.ShipBought.Invoke();
                    GameDataManager.Instance.UpdateUnlockedShips(_unlockedShips);
                    GameDataManager.Instance.SaveGame();
                }
                else
                {
                    _invokeEvents.ChangeShipFailed.Invoke();
                }
            }
        }
    }
}
