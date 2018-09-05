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

        public void SetBoolMovingLeft(bool val)
        {
            _anim.SetBool("MovingLeft", val);
        }

        public void SetBoolMovingRight(bool val)
        {
            _anim.SetBool("MovingRight", val);
        }

        public void ResetTriggers()
        {
            _anim.ResetTrigger("MoveLeft");
            _anim.ResetTrigger("MoveRight");
            _anim.ResetTrigger("ShipStartup");
            _anim.ResetTrigger("ShipDeath");
            _anim.ResetTrigger("BarrelRoll");
            // ShipRestart
        }
    }
}
