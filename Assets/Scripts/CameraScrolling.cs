using UnityEngine;

public class CameraScrolling : MonoBehaviour
{
    private PlantController plant;
    

    void Start()
    {
        plant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlantController>();
    }

    void LateUpdate()
    {
        transform.position += new Vector3(0f, plant.speed * Time.deltaTime);
    }
}
