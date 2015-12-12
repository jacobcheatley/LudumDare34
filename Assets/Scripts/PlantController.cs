using UnityEngine;

public class PlantController : MonoBehaviour
{
    [SerializeField] private float verticalSpeed = 1;
    [SerializeField] private float horizontalSpeed = 1;

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 velocity = new Vector3(horizontalInput * horizontalSpeed, verticalSpeed);
        Vector3 displacement = velocity * Time.deltaTime;

        transform.Translate(displacement);
    }
}
