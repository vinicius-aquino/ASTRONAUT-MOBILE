using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem particleSystem;
    public float TimeToHide = 3;
    public GameObject graphicItem;

    [Header("Sounds")]
    public AudioSource audioSource;

    private void Awake()
    {
        //if (particleSystem != null) particleSystem.transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            //gameObject.GetComponent<Collider2D>().enabled = false;
            Collect();
        }
    }

    protected virtual void HideItens()
    {
        if (graphicItem != null) graphicItem.SetActive(false);
        Invoke("HideToObject", TimeToHide);//ou Invoke(nameof(HideToObject, TimeToHide));
    }

    protected virtual void Collect() 
    {
        HideItens();
        onCollect();
    }

    private void HideToObject()
    {
        gameObject.SetActive(false);
    }

    protected virtual void onCollect()
    {
        //VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.COIN, transform.position);
        if (particleSystem != null) 
        {
            particleSystem.transform.SetParent(null);
            particleSystem.Play();
        }
        
        if (audioSource != null) audioSource.Play();
    }
}
