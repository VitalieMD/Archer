using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    [SerializeField] float selfDestroyTime;
    private void Start()
    {
        Destroy(gameObject, selfDestroyTime);
    }
}
