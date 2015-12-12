using UnityEngine;

public class CameraScrolling : MonoBehaviour
{
    [SerializeField] private float playerVerticalSpeed;

    void Start()
    {
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.position += Vector3.back;
    }

    void Update()
    {
        transform.position += new Vector3(0f, playerVerticalSpeed * Time.deltaTime);
    }
}
