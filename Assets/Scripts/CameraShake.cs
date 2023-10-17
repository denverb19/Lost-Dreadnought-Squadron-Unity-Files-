using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 0.6f;
    [SerializeField] float shakeMagnitude = 0.15f;
    Vector3 initialPosition;
    void Start()
    {
        initialPosition = transform.position;
    }
    public void ShakeCam()
    {
        StartCoroutine(ShakeRoutine());
    }
    IEnumerator ShakeRoutine()
    {
        float elapsedShakeTime = 0f;
        while (elapsedShakeTime < shakeDuration)
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            elapsedShakeTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
    }
}
