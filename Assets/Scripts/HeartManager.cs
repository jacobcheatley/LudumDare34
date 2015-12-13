using System;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    [SerializeField] private GameObject heart;
    [SerializeField] private float spacing;

    private PlantController player;
    private float maxHealth;
    private Image[] heartImages;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlantController>();
        player.ChangedHealth += UpdateHeartFills;
        maxHealth = player.maxHealth;
        heartImages = new Image[(int)maxHealth];
        float firstX = -spacing * maxHealth / 2 + spacing / 2;
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject newHeart = Instantiate(heart, Vector3.zero, Quaternion.identity) as GameObject;
            newHeart.transform.SetParent(transform);
            newHeart.transform.localPosition = new Vector3(firstX + i * spacing, 0);
            heartImages[i] = newHeart.GetComponent<Image>();
        }
    }

    private void UpdateHeartFills(object sender, ChangedHealthArgs e)
    {
        int fullHearts = (int)e.NewHealth;
        float partialFinalHeart = e.NewHealth - fullHearts;
        for (int i = 0; i < fullHearts; i++)
            heartImages[i].fillAmount = 1;
        if (fullHearts != maxHealth)
        {
            heartImages[fullHearts].fillAmount = partialFinalHeart;
            for (int i = fullHearts + 1; i < heartImages.Length; i++)
                heartImages[i].fillAmount = 0;
        }
    }
}
