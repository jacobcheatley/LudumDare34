using System;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    [Header("Movement")]
    public float minSpeed = 3f;
    public float maxSpeed = 10f;
    [SerializeField] private float horizontalSpeedRatio = 0.75f;

    [Header("Eating")]
    [SerializeField] private float speedBoostPerFly = 0.15f;
    [SerializeField] private float speedDecay = 3.0f;
    [SerializeField] private float timeBetweenEatsBeforeSlow = 3.0f;

    [Header("Getting Hit")]
    [SerializeField] private float speedPenaltyPerBird = 0.5f;
    [SerializeField] private float hitCooldown = 1.0f;

    //Hidden or private
    [HideInInspector] public float speed;
    private float timeOfNextEat;
    private float horizontalInput;
    private float timeSinceLastHit;

    //Events
    [HideInInspector] public event EventHandler<ChangedSpeedArgs> ChangedSpeed;
    protected virtual void OnChangedSpeed(ChangedSpeedArgs e)
    {
        EventHandler<ChangedSpeedArgs> handler = ChangedSpeed;
        if (handler != null)
            handler(this, e);
    }

    void Start()
    {
        speed = minSpeed;
        timeSinceLastHit = hitCooldown;
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

        //Movement
        PerformMovement();

        //Tracking variables
        timeSinceLastHit += Time.deltaTime;
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
        else if (other.name == "Bird(Clone)")
        {
            GetHitByBird();
        }
        else if (other.name == "Asteroid(Clone)")
        {
            
        }
    }

    void EatFly()
    {
        timeOfNextEat = Time.time + timeBetweenEatsBeforeSlow;
        speed += speedBoostPerFly;
        OnChangedSpeed(new ChangedSpeedArgs { Amount = speedBoostPerFly });
    }

    void GetHitByBird()
    {
        if (timeSinceLastHit > hitCooldown)
        {
            timeSinceLastHit = 0;
            speed -= speedPenaltyPerBird;
            OnChangedSpeed(new ChangedSpeedArgs { Amount = -speedPenaltyPerBird });
        }
    }
}

public class ChangedSpeedArgs : EventArgs
{
    public float Amount { get; set; }
}