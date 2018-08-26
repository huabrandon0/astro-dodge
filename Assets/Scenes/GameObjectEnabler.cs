using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidRage.UI
{
    public class GameObjectEnabler : MonoBehaviour
    {
        [SerializeField] GameObject[] _gameObjects;

        public void Enable(int idx)
        {
            if (idx < 0 || idx >= _gameObjects.Length)
                return;

            for (int i = 0; i < _gameObjects.Length; i++)
            {
                if (i == idx)
                    _gameObjects[i].SetActive(true);
                else
                    _gameObjects[i].SetActive(false);
            }
        }
    }
}
