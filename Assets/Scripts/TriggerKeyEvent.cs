using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerKeyEvent : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameVariables.Key = true;
        Destroy(gameObject);
    }
}
