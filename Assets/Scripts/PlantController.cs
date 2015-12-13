using System;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    [Header("Movement")]
    public float minSpeed = 3f;
    public float maxSpeed = 10f;
    [SerializeField] private float horizontalSpeedRatio = 0.75f;

    [Header("Stats")]
    public float maxHealth = 5f;

    [Header("Eating")]
    [SerializeField] private float speedBoostPerFly = 0.25f;
    [SerializeField] private float healthBoostPerFly = 0.25f;
    [SerializeField] private float speedBoostPerAlien = 0.5f;
    [SerializeField] private float healthBoostPerAlien = 0.25f;
    [SerializeField] private float speedDecay = 3.0f;
    [SerializeField] private float timeBetweenEatsBeforeSlow = 3.0f;

    [Header("Getting Hit")]
    [SerializeField] private float speedPenaltyPerBird = 2.5f;
    [SerializeField] private float damagePerBird = 1f;
    [SerializeField] private float speedPenaltyPerAsteroid = 5f;
    [SerializeField] private float damagePerAsteroid = 1f;
    [SerializeField] private float hitCooldown = 1.0f;

    [Header("Sounds")]
    [SerializeField] private AudioClip chomp;
    [SerializeField] private AudioClip hit;

    //Hidden or private
    [HideInInspector] public float speed;
    [HideInInspector] public float health;
    private float timeOfNextEat;
    private float horizontalInput;
    private float timeSinceLastHit;
    private AudioSource audioSource;

    //Events
    public event EventHandler<ChangedSpeedArgs> ChangedSpeed;
    protected virtual void OnChangedSpeed(ChangedSpeedArgs e)
    {
        EventHandler<ChangedSpeedArgs> handler = ChangedSpeed;
        if (handler != null)
            handler(this, e);
    }

    public event EventHandler<ChangedHealthArgs> ChangedHealth;
    protected virtual void OnChangedHealth(ChangedHealthArgs e)
    {
        EventHandler<ChangedHealthArgs> handler = ChangedHealth;
        if (handler != null)
            handler(this, e);
    }

    //Methods
    void Start()
    {
        speed = minSpeed;
        health = maxHealth;
        timeSinceLastHit = hitCooldown;
        timeOfNextEat = Time.time;
        audioSource = GetComponent<AudioSource>();
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
            ModifyHealth(healthBoostPerFly);
            audioSource.PlayOneShot(other.GetComponent<Fly>().Death, 0.5f);
            Destroy(other.gameObject);
        }
        else if (other.name == "Bird(Clone)")
        {
            ModifyHealth(-damagePerBird);
            HitSpeedPenalty(speedPenaltyPerBird);
        }
        else if (other.name == "Asteroid(Clone)")
        {
            ModifyHealth(-damagePerAsteroid);
            HitSpeedPenalty(speedPenaltyPerAsteroid);
        }
        else if (other.name == "Alien(Clone)")
        {
            EatSpeedBoost(speedBoostPerAlien);
            ModifyHealth(healthBoostPerAlien);
            audioSource.PlayOneShot(Utility.ChooseOne(other.GetComponent<Alien>().DeathSounds), 0.5f);
            Destroy(other.gameObject);
        }
    }

    void ModifyHealth(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);
        OnChangedHealth(new ChangedHealthArgs { Amount = amount, NewHealth = health });
    }
    
    void EatSpeedBoost(float speedBoost)
    {
        timeOfNextEat = Time.time + timeBetweenEatsBeforeSlow;
        speed += speedBoost;
        OnChangedSpeed(new ChangedSpeedArgs { Amount = speedBoost, NewSpeed = speed});
        audioSource.PlayOneShot(chomp);
    }

    private void HitSpeedPenalty(float speedPenalty)
    {
        if (timeSinceLastHit > hitCooldown)
        {
            timeSinceLastHit = 0;
            speed -= speedPenalty;
            OnChangedSpeed(new ChangedSpeedArgs { Amount = -speedPenalty, NewSpeed = speed });
        }
        audioSource.PlayOneShot(hit);
    }
}

public class ChangedSpeedArgs : EventArgs
{
    public float Amount { get; set; }
    public float NewSpeed { get; set; }
}

public class ChangedHealthArgs : EventArgs
{
    public float Amount { get; set; }
    public float NewHealth { get; set; }
}
