using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    public class Counter : MonoBehaviour
    {
        [System.Serializable]
        public class ResponseEvents
        {
            public GameEventInt AddToCount;
            public GameEventInt SetCount;
        }

        [System.Serializable]
        public class InvokeEvents
        {
            public GameEventInt CountChanged;
        }

        [SerializeField] ResponseEvents _responseEvents;
        [SerializeField] InvokeEvents _invokeEvents;

        public static int _count = 0;

		void OnEnable()
		{
            _responseEvents.AddToCount.AddListener(AddToCount);
            _responseEvents.SetCount.AddListener(SetCount);
		}

		void OnDisable()
		{
            _responseEvents.AddToCount.RemoveListener(AddToCount);
            _responseEvents.SetCount.RemoveListener(SetCount);
		}

        void AddToCount(int val)
        {
            _count += val;
            _invokeEvents.CountChanged.Invoke(_count);
        }

        void SetCount(int val)
        {
            _count = val;
            _invokeEvents.CountChanged.Invoke(_count);
        }
    }
}
