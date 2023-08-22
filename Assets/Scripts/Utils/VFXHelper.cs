using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXHelper : MonoBehaviour
{
    [SerializeField] private ParticleSystem vfx;
    public string tagToCheck = "Player";

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.tag == tagToCheck)
        {
            vfx.Play();
        }
    }
}
