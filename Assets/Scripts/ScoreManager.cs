using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameObject scorePopup;
    [SerializeField] private GameObject scoreDisplay;

    private Text scoreDisplayText;
    private PlantController player;
    private float currentScore;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlantController>();
        scoreDisplayText = scoreDisplay.GetComponent<Text>();
        player.ChangedScore += ChangedScore;
        UpdateDisplayText();
    }

    private void ChangedScore(object sender, ChangedScoreArgs e)
    {
        GameObject popup = Instantiate(scorePopup, Vector3.zero, Quaternion.identity) as GameObject;
        popup.transform.SetParent(transform);
        popup.transform.localPosition = new Vector3(0, 0, 5f);
        popup.GetComponent<ScorePopup>().Setup(e);

        currentScore += e.Amount;
        UpdateDisplayText();
    }

    private void UpdateDisplayText()
    {
        scoreDisplayText.text = currentScore.ToString("#;-#;0");
    }
}
