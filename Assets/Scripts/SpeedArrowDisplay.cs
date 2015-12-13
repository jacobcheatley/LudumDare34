using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpeedArrowDisplay : MonoBehaviour
{
    [SerializeField] private float timeOfGrow = 0.3f;
    [SerializeField] private float maxScaleIncrease = 0.5f;

    private PlantController plant;
    private Image arrow;

    private Vector2 initialSize;

    void Start()
    {
        plant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlantController>();
        arrow = GetComponent<Image>();
        initialSize = arrow.rectTransform.sizeDelta;
        plant.ChangedSpeed += ChangedSpeed;
    }

    void ChangedSpeed(object sender, ChangedSpeedArgs e)
    {
        arrow.fillAmount = (e.NewSpeed - plant.minSpeed) / (plant.maxSpeed - plant.minSpeed);
        if (e.Amount > 0)
        {
            StartCoroutine(IncreaseSize());
        }
    }

    IEnumerator IncreaseSize()
    {
        float CurrentTime = 0;
        while (CurrentTime < timeOfGrow)
        {
            float scale = maxScaleIncrease * CurrentTime / timeOfGrow + 1f;
            arrow.rectTransform.sizeDelta = new Vector2(initialSize.x, initialSize.y * scale);
            CurrentTime += Time.deltaTime;
            yield return null;
        }
        arrow.rectTransform.sizeDelta = initialSize;
    }
}
