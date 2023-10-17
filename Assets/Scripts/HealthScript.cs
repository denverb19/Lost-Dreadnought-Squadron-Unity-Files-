using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    [SerializeField] float maxHealthOfObject = 5f;
    [SerializeField] int scoreValue = 1;
    [SerializeField] ParticleSystem explosionEffect;
    [SerializeField] AudioClip explosionSound;
    GameManager thisGameManager;
    const int EXPLOSION = 0;
    float healthOfObject = 1f;
    CameraShake thisCamShake;
    AudioScript thisAudioScript;
    ScoreKeeper thisScoreKeeper;
    UIDisplay thisUIDisplay;
    bool isPlayer;
    void Start()
    {
        thisCamShake = Camera.main.GetComponent<CameraShake>();
        thisAudioScript = FindObjectOfType<AudioScript>();
        thisScoreKeeper = FindObjectOfType<ScoreKeeper>();
        isPlayer = (gameObject.layer == LayerMask.NameToLayer("Player"));
        healthOfObject = maxHealthOfObject;
        thisGameManager = FindObjectOfType<GameManager>();
        thisUIDisplay = FindObjectOfType<UIDisplay>();
    }
    public float GetHealth()
    {
        return healthOfObject;
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        DamageDealer localDamageDealer = other.GetComponent<DamageDealer>();
        if (localDamageDealer != null)
        {
            if (isPlayer)
            {
                if (!transform.GetChild(0).GetComponent<Animator>().GetBool("isEvading"))//Only damage player if not evading
                {
                    TakeDamage(localDamageDealer.GetDamage());
                    localDamageDealer.Hit();
                }
            }
            else
            {
                TakeDamage(localDamageDealer.GetDamage());
                localDamageDealer.Hit();
            }
        }
    }
    private void TakeDamage(int damageTaken)
    {
        healthOfObject -= damageTaken;
        thisAudioScript.PlaySound(EXPLOSION);
        PlayExplosion();
        if (isPlayer)
        {
            ShakeCamera();
            thisUIDisplay.UpdateHealth(healthOfObject/maxHealthOfObject);
            if (healthOfObject <= 0)
            {
                Destroy(gameObject);
                thisGameManager.LoadScene(2);
            }
        }
        else
        {
            if (healthOfObject <= 0)
            {
                thisScoreKeeper.IncreaseScore(scoreValue);
                thisUIDisplay.UpdateScore(thisScoreKeeper.GetScore());
                Destroy(gameObject);
            }
        }
    }
    private void PlayExplosion()
    {
        if (explosionEffect != null)
        {
            ParticleSystem thisExplosion = Instantiate(explosionEffect, transform.position, Quaternion.Euler(90, 0, 0));
            Destroy(thisExplosion, thisExplosion.main.duration + thisExplosion.main.startLifetime.constantMax);
        } 
    }
    private void ShakeCamera()
    {
        if (thisCamShake != null)
        {
            thisCamShake.ShakeCam();
        }
    }
}
