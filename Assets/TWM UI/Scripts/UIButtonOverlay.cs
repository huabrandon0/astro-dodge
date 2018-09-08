using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TWM.UI
{
    public class UIButtonOverlay : MonoBehaviour
    {
        List<UIButton> _buttons;

        void Awake()
        {
            _buttons = GetComponentsInChildren<UIButton>().ToList();
        }

        public void Enable()
        {
            foreach (UIButton button in _buttons)
            {
                button.Enable();
            }
            StopAllCoroutines();
        }

        public void Disable()
        {
            foreach (UIButton button in _buttons)
            {
                button.Disable();
            }
            StopAllCoroutines();
        }

        public void DisableForSeconds(float time)
        {
            Disable();
            StartCoroutine(EnableAfterSeconds(time));
        }

        IEnumerator EnableAfterSeconds(float time)
        {
            yield return new WaitForSeconds(time);
            Enable();
        }
    }
}
