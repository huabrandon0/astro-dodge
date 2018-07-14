using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerExitDisable : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        other.gameObject.SetActive(false);
    }
}
