using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidRage.Game
{
    [RequireComponent(typeof(Animator))]
    public class ShipAnimator : MonoBehaviour
    {
        Animator _anim;

        void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        public void SetBoolShipOnScreen(bool val)
        {
            _anim.SetBool("ShipOnScreen", val);
        }
    }
}
