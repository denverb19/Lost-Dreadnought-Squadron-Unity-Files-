using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI thisScoreTM;
    [SerializeField] GameObject healthSliderObject;
    Slider healthSlider;
    void Awake()
    {
        healthSlider = healthSliderObject.GetComponent<Slider>();
    }
    void Start()
    {
        thisScoreTM.text = "000000000";
    }
    public void UpdateScore(int newScore)
    {
        thisScoreTM.text = newScore.ToString("000000000");
    }
    public void UpdateHealth(float newHealth)
    {
        newHealth = Mathf.Clamp(newHealth, 0, 1);
        healthSlider.value = newHealth;
    }
}
