using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    public class ShipSelect : MonoBehaviour
    {
        [SerializeField] GameObject[] _ships;

        GameObject _currentShip;

        float _invisibleTime;
        
        [System.Serializable]
        public class ResponseEvents
        {
            public GameEventInt ChangeShipIndex;
        }

        [SerializeField] ResponseEvents _responseEvents;

        void Awake()
        {
            if (_ships.Length <= 0)
            {
                Debug.LogError("nooo!!");
            }

            //SelectShipRandom();
            SelectShip(0);

            _responseEvents.ChangeShipIndex.AddListener(SelectShip);
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

        public void InvisibleSkill()
        {
            StartCoroutine(MakeInvisible(_invisibleTime));
        }

        IEnumerator MakeInvisible(float time)
        {
            GetComponentInChildren<Renderer>();
            // WIP !


            yield return null;
        }
    }
}
