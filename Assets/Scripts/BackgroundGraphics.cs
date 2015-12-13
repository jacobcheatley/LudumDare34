using System.Collections;
using System.Linq;
using UnityEngine;

public class BackgroundGraphics : MonoBehaviour
{
    [SerializeField] private float transitionDistance = 15f;

    [Header("Colors")]
    [SerializeField] private Color skyColor;
    [SerializeField] private Color spaceColor;
    [SerializeField] private Color sunColor;

    [Header("Background Overlays")]
    [SerializeField] private GameObject[] overlayContainers;
    
    private BackgroundStage bg;
    private SpriteRenderer backgroundSprite;
    private SpriteRenderer[] overlayRenderers;
    
    void Start()
    {
        bg = GetComponent<BackgroundStage>();
        bg.ChangedStage += ChangedStage;

        backgroundSprite = GetComponent<SpriteRenderer>();
        backgroundSprite.color = skyColor;

        overlayRenderers = overlayContainers.Select(x => x.GetComponent<SpriteRenderer>()).ToArray();
    }

    void ChangedStage(object sender, ChangedStageArgs e)
    {
        switch (e.NewStage)
        {
            case BackgroundStage.Stage.Space:
                StartCoroutine(TransitionOverlay(0, 1));
                StartCoroutine(TransitionColor(skyColor, spaceColor));
                break;
            case BackgroundStage.Stage.Sun:
                StartCoroutine(TransitionOverlay(1, 2));
                StartCoroutine(TransitionColor(spaceColor, sunColor));
                break;
        }
    }

    IEnumerator TransitionOverlay(int prev, int next)
    {
        float initialHeight = transform.position.y;
        float endHeight = initialHeight + transitionDistance;
        while (transform.position.y <= endHeight)
        {
            overlayRenderers[prev].color = Color.Lerp(Color.white, Color.clear, (transform.position.y - initialHeight) / transitionDistance);
            overlayRenderers[next].color = Color.Lerp(Color.clear, Color.white, (transform.position.y - initialHeight) / transitionDistance);
            yield return null;
        }
    }

    IEnumerator TransitionColor(Color from, Color to)
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
