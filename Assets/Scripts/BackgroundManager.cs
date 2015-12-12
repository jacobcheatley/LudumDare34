using System.Collections;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private GameConstantsData constants;
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
        backgroundSprite = GetComponent<SpriteRenderer>();
        backgroundSprite.color = skyColor;
    }

    void Update()
    {
        if (transform.position.y > constants.skyEndHeight && bg.stage == BackgroundStage.Stage.Sky)
        {
            bg.stage++;
            StartCoroutine(TransitionBackground(skyColor, spaceColor));
        }
        if (transform.position.y > constants.spaceEndHeight && bg.stage == BackgroundStage.Stage.Space)
        {
            bg.stage++;
            StartCoroutine(TransitionBackground(spaceColor, sunColor));
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
