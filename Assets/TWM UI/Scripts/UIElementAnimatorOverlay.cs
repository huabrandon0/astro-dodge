using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TWM.UI
{
    public class UIElementAnimatorOverlay : MonoBehaviour
    {
        List<UIElementAnimator> _elementAnimators;

        void Awake()
        {
            _elementAnimators = GetComponentsInChildren<UIElementAnimator>().ToList();
        }

        public void Enter()
        {
            foreach (UIElementAnimator elementAnimator in _elementAnimators)
            {
                elementAnimator.Enter();
            }
        }

        public void Exit()
        {
            foreach (UIElementAnimator elementAnimator in _elementAnimators)
            {
                elementAnimator.Exit();
            }
        }
    }
}
