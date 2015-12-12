using UnityEngine;

public class PlantController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float minSpeed = 3f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float horizontalSpeedRatio = 0.75f;

    [Header("Eating")]
    [SerializeField] private float speedBoostPerFly = 0.15f;
    [SerializeField] private float speedDecay = 3.0f;
    [SerializeField] private float timeBetweenEatsBeforeSlow = 1.5f;

    //Hidden or private
    [HideInInspector] public float speed;
    private float timeOfNextEat;
    private float horizontalInput;

    void Start()
    {
        speed = minSpeed;
        timeOfNextEat = Time.time;
    }

    void Update()
    {
        //Input
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //Modifying Speed
        if (Time.time > timeOfNextEat)
            speed -= speedDecay * Time.deltaTime;
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        Debug.Log(speed);

        //Movement
        PerformMovement();
    }

    void PerformMovement()
    {
        Vector3 velocity = new Vector3(horizontalInput * speed * horizontalSpeedRatio, speed);
        Vector3 displacement = velocity * Time.deltaTime;
        transform.position += displacement;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Fly(Clone)")
        {
            EatFly();
            Destroy(other.gameObject);
        }
    }

    void EatFly()
    {
        timeOfNextEat = Time.time + timeBetweenEatsBeforeSlow;
        speed += speedBoostPerFly;
    }
}
