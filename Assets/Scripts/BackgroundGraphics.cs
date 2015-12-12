using System.Collections;
using UnityEngine;

public class BackgroundGraphics : MonoBehaviour
{
    [SerializeField] private float transitionDistance = 15f;

    [Header("Colors")]
    [SerializeField] private Color skyColor;
    [SerializeField] private Color spaceColor;
    [SerializeField] private Color sunColor;

    private BackgroundStage bg;
    private SpriteRenderer backgroundSprite;
    
    void Start()
    {
        bg = GetComponent<BackgroundStage>();
        bg.ChangedStage += ChangedStage;

        backgroundSprite = GetComponent<SpriteRenderer>();
        backgroundSprite.color = skyColor;
    }

    void Update()
    {
//        if (transform.position.y > constants.skyEndHeight && bg.CurrentStage == BackgroundStage.Stage.Sky)
//        {
//            bg.CurrentStage++;
//            StartCoroutine(TransitionBackground(skyColor, spaceColor));
//        }
//        if (transform.position.y > constants.spaceEndHeight && bg.CurrentStage == BackgroundStage.Stage.Space)
//        {
//            bg.CurrentStage++;
//            StartCoroutine(TransitionBackground(spaceColor, sunColor));
//        }
    }

    void ChangedStage(object sender, ChangedStageArgs e)
    {
        switch (e.NewStage)
        {
            case BackgroundStage.Stage.Space:
                StartCoroutine(TransitionBackground(skyColor, spaceColor));
                break;
            case BackgroundStage.Stage.Sun:
                StartCoroutine(TransitionBackground(spaceColor, sunColor));
                break;
        }
    }

    IEnumerator TransitionBackground(Color from, Color to)
    {
        float initialHeight = transform.position.y;
        float endHeight = initialHeight + transitionDistance;
        while (transform.position.y <= endHeight)
        {
            backgroundSprite.color = Color.Lerp(from, to, (transform.position.y - initialHeight) / transitionDistance);
            yield return null;
        }
    }
}
