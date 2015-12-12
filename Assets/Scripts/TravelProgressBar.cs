﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider), typeof(RectTransform))]
public class TravelProgressBar : MonoBehaviour
{
    [SerializeField] private GameConstantsData constants;
    [SerializeField] private Image plantHead;
    private Transform plantTransform;
    private Slider slider;
    private RectTransform rectTransform;

    void Start()
    {
        plantTransform = GameObject.FindGameObjectWithTag("Player").transform;
        slider = GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = constants.sunEndHeight;

        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        slider.value = plantTransform.position.y;
        plantHead.rectTransform.localPosition = new Vector3(0, 2f + slider.normalizedValue * rectTransform.rect.height - rectTransform.rect.height / 2);
    }
}
