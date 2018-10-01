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
            public Ship(Transform model, string name, int cost, int index)
            {
                Model = model;
                Name = name;
                Cost = cost;
                Index = index;
            }
            
            public Transform Model;
            public string Name;
            public int Cost;
            public int Index;
        }

        [SerializeField] Ship[] _ships;
        bool[] _unlockedShips;

        Dictionary<int, int> _indexToShipIndex = new Dictionary<int, int>();
        Dictionary<int, int> _shipIndexToIndex = new Dictionary<int, int>();

        int _shipIndex = 0;
        int _chosenShipIndex = 0;

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

        [SerializeField] string _lastChosenShipPlayerPref;

        void Awake()
        {
            for (int i = 0; i < _ships.Length; i++)
            {
                _indexToShipIndex.Add(i, _ships[i].Index);
                _shipIndexToIndex.Add(_ships[i].Index, i);
            }
        }

        void Start()
        {
            GameData loadedData = GameDataManager.Instance.GetGameData();
            Load(loadedData);

            int startingIndex = 0;
            if (PlayerPrefs.HasKey(_lastChosenShipPlayerPref))
                startingIndex = PlayerPrefs.GetInt(_lastChosenShipPlayerPref);

            _shipIndex = _chosenShipIndex = startingIndex;

            int index;
            if (_shipIndexToIndex.TryGetValue(_shipIndex, out index))
                _invokeEvents.ChangeShipIndex.Invoke(index);
            
            GameDataManager.OnGameDataUpdated += Load;
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
                for (int i = 0; i < _unlockedShips.Length; i++)
                {
                    _unlockedShips[i] = false;
                }

                _unlockedShips[_shipIndexToIndex[0]] = true;
            }
        }

        public void ShowModels()
        {
            ChangeModel(_shipIndex);
        }

        public void Increment()
        {
            int desiredShipIndex = _shipIndex + 1;
            if (desiredShipIndex >= _ships.Length)
                _shipIndex = 0;
            else
                _shipIndex = desiredShipIndex;

            ChangeModel(_shipIndex);
        }

        public void Decrement()
        {
            int desiredShipIndex = _shipIndex - 1;

            if (desiredShipIndex < 0)
                _shipIndex = _ships.Length - 1;
            else
                _shipIndex = desiredShipIndex;
            
            ChangeModel(_shipIndex);
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

        // Expects ship index.
        void ChangeModel(int shipIndex)
        {
            //string s = "";
            //foreach (bool b in _unlockedShips)
            //{
            //    s += b.ToString() + " ";
            //}
            //Debug.Log(s);

            int index;
            if (!_shipIndexToIndex.TryGetValue(shipIndex, out index))
                return;

            if (index < 0 || index >= _ships.Length)
                return;

            _invokeEvents.ChangeShipName.Invoke(_ships[index].Name);

            if (shipIndex == _chosenShipIndex)
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
            int index;
            if (!_shipIndexToIndex.TryGetValue(_shipIndex, out index))
                return;

            if (_unlockedShips[index])
            {
                _chosenShipIndex = _shipIndex;
                _invokeEvents.ChangeShipIndex.Invoke(index);
                _invokeEvents.ShipChosen.Invoke();
                PlayerPrefs.SetInt(_lastChosenShipPlayerPref, _chosenShipIndex);
                GameDataManager.Instance.SaveGame();
            }
            else
            {
                if (Currency.Instance.Withdraw(_ships[index].Cost))
                {
                    _unlockedShips[index] = true;
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
