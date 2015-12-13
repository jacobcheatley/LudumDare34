using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OverlayParallax : MonoBehaviour
{
    [SerializeField] private GameConstantsData constants;
    [SerializeField] private bool isSunParallax;

    private float spriteHeight;
    private float spriteUPP;
    private float initialPos;
    private float targetPos;

    void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        spriteHeight = sprite.rect.height;
        spriteUPP = 1 / sprite.pixelsPerUnit;
        initialPos = -Camera.main.orthographicSize;
        targetPos = 1f + initialPos - (spriteHeight - Camera.main.pixelHeight) * spriteUPP;
    }

    void Update()
    {
        float currentHeight;
        if (isSunParallax)
            currentHeight = Mathf.Lerp(initialPos, targetPos, (transform.parent.transform.position.y - constants.spaceEndHeight) / (constants.sunEndHeight - constants.spaceEndHeight));
        else
            currentHeight = Mathf.Lerp(initialPos, targetPos, (transform.parent.transform.position.y - constants.skyEndHeight) / (constants.spaceEndHeight - constants.skyEndHeight));
        transform.localPosition = new Vector3(0, currentHeight);
    }
}
