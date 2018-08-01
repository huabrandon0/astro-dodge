using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidRage.UI
{
    public class UIPanel : MonoBehaviour
    {
        public void Toggle()
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
