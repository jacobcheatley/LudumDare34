using System.Collections;
using UnityEngine;

public class BackgroundGraphics : MonoBehaviour
{
    [SerializeField] private GameConstantsData constants;
    [SerializeField] private float transitionDistance = 15f;

    [Header("Colors")]
    [SerializeField] private Color skyColor;
    [SerializeField] private Color spaceColor;
    [SerializeField] private Color sunColor;

    private Material backgroundMaterial;
    private bool inSky = true;
    private bool inSpace = false;
    private bool nearSun = false;

    void Start()
    {
        backgroundMaterial = GetComponent<MeshRenderer>().material;
        backgroundMaterial.color = skyColor;
    }

    void Update()
    {
        if (transform.position.y < constants.skyEndHeight)
            backgroundMaterial.color = skyColor;
        else if (transform.position.y < constants.skyEndHeight + transitionDistance)
            backgroundMaterial.color = Color.Lerp(skyColor, spaceColor, (transform.position.y - constants.skyEndHeight) / transitionDistance);
        else if (transform.position.y < constants.spaceEndHeight)
            backgroundMaterial.color = spaceColor;
        else if (transform.position.y < constants.spaceEndHeight + transitionDistance)
            backgroundMaterial.color = Color.Lerp(spaceColor, sunColor, (transform.position.y - constants.spaceEndHeight) / transitionDistance);
        else
            backgroundMaterial.color = sunColor;
    }
}
