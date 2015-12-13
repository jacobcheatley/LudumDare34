using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScorePopup : MonoBehaviour
{
    [SerializeField] private float lifetime;
    [SerializeField] private Color[] textColors;
    [SerializeField] private Color[] scoreGainColors;
    [SerializeField] private Color[] scoreLossColors;
    [SerializeField] private float distanceUp;
    [SerializeField] private GameObject popupDescriptionText;
    [SerializeField] private GameObject popupChangeText;

    private float elapsedTime;
    private Vector3 initialPosition;
    private Vector3 finalPosition;

    private Text descriptionText;
    private Text changeText;

    public void Setup(ChangedScoreArgs e)
    {
        initialPosition = transform.position;
        finalPosition = initialPosition + new Vector3(0, distanceUp);

        descriptionText = popupDescriptionText.GetComponent<Text>();
        changeText = popupChangeText.GetComponent<Text>();
        descriptionText.text = e.Text;
        changeText.text = e.Amount.ToString("+#;-#;0");

        StartCoroutine(Animate(e.Amount > 0));
    }

    IEnumerator Animate(bool gainedScore)
    {
        while (elapsedTime < lifetime)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / lifetime;
            transform.position = Vector3.Lerp(initialPosition, finalPosition, percent);
            descriptionText.color = Color.Lerp(textColors[0], textColors[1], percent);
            if (gainedScore)
                changeText.color = Color.Lerp(scoreGainColors[0], scoreGainColors[1], percent);
            else
                changeText.color = Color.Lerp(scoreLossColors[0], scoreLossColors[1], percent);
            yield return null;
        }
        Destroy(gameObject);
    }
}
