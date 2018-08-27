using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

namespace AsteroidRage.Animation
{
    public class PlayerCameraAnimator : MonoBehaviour
    {
        Animator _anim;

        void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        public void SetBoolIsInShop(bool val)
        {
            _anim.SetBool("IsInShop", val);
        }
    }
}
