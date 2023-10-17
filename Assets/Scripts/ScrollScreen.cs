using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScreen : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;
    Vector2 thisOffset;
    Material thisMaterial;
    void Start()
    {
        thisMaterial = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        Vector2 deltaOffset = moveSpeed * Time.deltaTime;
        thisMaterial.mainTextureOffset += deltaOffset;
    }
}
