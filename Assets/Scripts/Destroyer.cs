using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destroyer : MonoBehaviour
{
    public event UnityAction<GameObject> RoadDestroyTriggerReached;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Respawn")
        {
            GameObject target = other.transform.parent.gameObject;
            RoadDestroyTriggerReached?.Invoke(target);
        }
    }
}
