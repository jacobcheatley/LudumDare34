using UnityEngine;

public class CameraScrolling : MonoBehaviour
{
    [SerializeField] private GameConstantsData constants;

    private Transform playerTransform;
    private PlantController plant;
    private bool end;
    private float initialSize;
    private float targetSize;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        plant = player.GetComponent<PlantController>();
    }

    void LateUpdate()
    {
        //Does own end handling because it's easier
        if (!end)
        {
            transform.position += new Vector3(0f, plant.speed * Time.deltaTime);
            if (playerTransform.position.y > constants.sunEndHeight)
            {
                end = true;
            }
        }
    }
}
