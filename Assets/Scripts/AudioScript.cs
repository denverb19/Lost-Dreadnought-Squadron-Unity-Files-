using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    [Header("Combat")]
    [SerializeField] AudioClip explosionSound;
    [SerializeField] [Range(0f, 1f)] float explosionVolume = 1f;
    const int EXPLOSION = 0;
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0f, 1f)] float laserVolume = 1f;
    const int LASER = 1;
    [SerializeField] AudioClip bigLaserSound;
    [SerializeField] [Range(0f, 1f)] float bigLaserVolume = 1f;
    const int BIGLASER = 2;
    
    [Header("Movement")]
    [SerializeField] AudioClip evadeSound;
    [SerializeField] [Range(0f, 1f)] float evadeVolume = 1f;
    const int EVASION = 3;
    [SerializeField] AudioClip boostOnSound;
    [SerializeField] [Range(0f, 1f)] float boostOnVolume = 1f;
    const int BOOSTON = 4;
    [SerializeField] AudioClip boostOffSound;
    [SerializeField] [Range(0f, 1f)] float boostOffVolume = 1f;
    const int BOOSTOFF = 5;

    private void Awake() 
    {
        ManageSingleton();
    }
    void ManageSingleton()
    {
        int instanceCount = FindObjectsOfType(GetType()).Length;
        if (instanceCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
   
    }
    public void PlaySound(int chosenSound)
    {
        Vector3 camPosition = Camera.main.transform.position;
        switch (chosenSound)
        {
            case EXPLOSION:
                AudioSource.PlayClipAtPoint(explosionSound, camPosition, explosionVolume);
                break;
            case LASER:
                AudioSource.PlayClipAtPoint(laserSound, camPosition, laserVolume);
                break;
            case BIGLASER:
                AudioSource.PlayClipAtPoint(bigLaserSound, camPosition, bigLaserVolume);
                break;
            case EVASION:
                AudioSource.PlayClipAtPoint(evadeSound, camPosition, evadeVolume);
                break;
            case BOOSTON:
                AudioSource.PlayClipAtPoint(boostOnSound, camPosition, boostOnVolume);
                break;
            case BOOSTOFF:
                AudioSource.PlayClipAtPoint(boostOffSound, camPosition, boostOffVolume);
                break;
        }
    }
}
