using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    // Reference: Unity3d.College's video "UI Model DIsplay / Character Select UI"
    public class ShopShipSelection : MonoBehaviour
    {
        List<Transform> _models;

        int _index = 0;

        [System.Serializable]
        public class InvokeEvents
        {
            public GameEventInt ChangeShipIndex;
        }

        [SerializeField] InvokeEvents _invokeEvents;

        void Awake()
        {
            _models = new List<Transform>();
            for (int i = 0; i < transform.childCount; i++)
            {
                var model = transform.GetChild(i);
                _models.Add(model);
            }

            EnableModel(_models[_index]);
        }

        public void Increment()
        {
            int desiredIndex = _index + 1;
            if (desiredIndex >= _models.Count)
                _index = 0;
            else
                _index = desiredIndex;

            EnableModel(_models[_index]);
        }

        public void Decrement()
        {
            int desiredIndex = _index - 1;
            if (desiredIndex < 0)
                _index = _models.Count - 1;
            else
                _index = desiredIndex;

            EnableModel(_models[_index]);
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

        public void ConfirmSelection()
        {
            Debug.Log("new ship! " + _index);
            _invokeEvents.ChangeShipIndex.Invoke(_index);
        }

        public List<Transform> GetModels()
        {
            return new List<Transform>(_models);
        }
    }
}
