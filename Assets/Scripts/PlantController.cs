using System;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    [Header("Movement")]
    public float minSpeed = 3f;
    public float maxSpeed = 10f;
    [SerializeField] private float horizontalSpeedRatio = 0.75f;

    [Header("Eating")]
    [SerializeField] private float speedBoostPerFly = 0.25f;
    [SerializeField] private float speedBoostPerAlien = 0.5f;
    [SerializeField] private float speedDecay = 3.0f;
    [SerializeField] private float timeBetweenEatsBeforeSlow = 3.0f;

    [Header("Getting Hit")]
    [SerializeField] private float speedPenaltyPerBird = 2.5f;
    [SerializeField] private float speedPenaltyPerAsteroid = 5f;
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
            EatSpeedBoost(speedBoostPerFly);
            Destroy(other.gameObject);
        }
        else if (other.name == "Bird(Clone)")
        {
            GetHitByBird();
        }
        else if (other.name == "Asteroid(Clone)")
        {
            GetHitByAsteroid();
        }
        else if (other.name == "Alien(Clone)")
        {
            EatSpeedBoost(speedBoostPerAlien);
            Destroy(other.gameObject);
        }
    }

    void EatSpeedBoost(float speedBoost)
    {
        timeOfNextEat = Time.time + timeBetweenEatsBeforeSlow;
        speed += speedBoost;
        OnChangedSpeed(new ChangedSpeedArgs { Amount = speedBoost });
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

    void GetHitByAsteroid()
    {
        if (timeSinceLastHit > hitCooldown)
        {
            timeSinceLastHit = 0;
            speed -= speedPenaltyPerAsteroid;
            OnChangedSpeed(new ChangedSpeedArgs {Amount = -speedPenaltyPerAsteroid});
        }
    }
}

public class ChangedSpeedArgs : EventArgs
{
    public float Amount { get; set; }
}