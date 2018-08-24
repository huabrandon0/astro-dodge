using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Extensions;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    /// <summary>
    /// Stores the current game currency.
    /// </summary>
    public class Currency : Singleton<Currency>
    {
        protected Currency() { }

        [System.Serializable]
        public class ResponseEvents
        {
            public GameEventInt AddToCurrency;
            public GameEventInt SetCurrency;
        }

        [SerializeField] ResponseEvents _responseEvents;

        [System.Serializable]
        public class InvokeEvents
        {
            public GameEventInt CurrencyChanged;
        }

        [SerializeField] InvokeEvents _invokeEvents;

        int _currency = 0;

		void OnEnable()
		{
            _responseEvents.AddToCurrency.AddListener(AddToCurrency);
            _responseEvents.SetCurrency.AddListener(SetCurrency);
		}

		void OnDisable()
		{
            _responseEvents.AddToCurrency.RemoveListener(AddToCurrency);
            _responseEvents.SetCurrency.RemoveListener(SetCurrency);
		}

		public void AddToCurrency(int val)
        {
            if (val != 0)
            {
                _currency += val;
                _invokeEvents.CurrencyChanged.Invoke(_currency);
            }
        }

        public void SetCurrency(int val)
        {
            _currency = val;
            _invokeEvents.CurrencyChanged.Invoke(_currency);
        }

        public bool Withdraw(int val)
        {
            if (val > _currency)
                return false;
            else
            {
                _currency -= val;
                _invokeEvents.CurrencyChanged.Invoke(_currency);
                return true;
            }
        }
    }
}
