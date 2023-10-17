using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject bigProjectilePrefab;
    [SerializeField] float projectileSpeed = 15f;
    [SerializeField] float projectileLifetime = 4f;
    [SerializeField] float timeBetweenShots = 0.15f;
    [SerializeField] int burstSize = 4;
    Coroutine firingCoroutine;
    GameObject thisLaser;
    AudioScript thisAudioScript;
    const int LASER = 1;
    const int BIGLASER = 2;

    [Header("AI")]
    [SerializeField] float shotDelay = 2f;
    [SerializeField] float shotVariance = 1.5f;
    [SerializeField] bool isAI = false;

    [HideInInspector] public bool isShootingLasers = false;
    void Start()
    {
        thisAudioScript = FindObjectOfType<AudioScript>();
        if (isAI)
        {
            Invoke("StartAIShots", 0.5f);//Don't want shots starting off screen for sound reasons hence the delay
        }
    }
    void StartAIShots()
    {
        isShootingLasers = true;
    }
    void Update()
    {
        Fire();
    }
    void Fire()
    {
        if (isShootingLasers && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!isShootingLasers && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }
    IEnumerator FireContinuously()
    {
        int localBurstCount = 0;
        while (true)
        {
            localBurstCount += 1;
            if (localBurstCount < burstSize)
            {
                thisAudioScript.PlaySound(LASER);
                FireOneLaser(projectilePrefab);
            }
            else
            {
                thisAudioScript.PlaySound(BIGLASER);
                FireOneLaser(bigProjectilePrefab);
            }
            localBurstCount = localBurstCount % burstSize;
            if (isAI)
            {
                yield return new WaitForSeconds(GetRandomShotTime());
            }
            else
            {
                yield return new WaitForSeconds(timeBetweenShots);
            }
        }
    }

    void FireOneLaser(GameObject thisProjectilePrefab)
    {
        thisLaser = Instantiate(thisProjectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D laserBody = thisLaser.GetComponent<Rigidbody2D>();
        if (laserBody != null) 
        {
            laserBody.velocity = transform.up * projectileSpeed;
        }
        Destroy(thisLaser, projectileLifetime);
    }
    private float GetRandomShotTime()
    {
        float timeToReturn = Random.Range(shotDelay - shotVariance, shotDelay + shotVariance);
        return Mathf.Clamp(timeToReturn, timeBetweenShots, float.MaxValue);
    }
}
