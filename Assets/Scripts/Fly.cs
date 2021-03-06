﻿using UnityEngine;

public class Fly : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float lifetime = 6f;
    [SerializeField] private float maxWanderDistance = 1.5f;
    [SerializeField] private float timeBetweenWanders = 0.5f;
    [SerializeField] private float speed = 1.0f;

    [Header("Sounds")]
    public AudioClip Death;

    private Vector3 initialPosition;
    private Vector3 wanderPoint;
    private float nextWanderTime;
    private MusicController mController;

    void Start()
    {
        Destroy(gameObject, lifetime);
        initialPosition = transform.position;
        wanderPoint = ChooseNewWanderPoint();
        mController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MusicController>();
    }

    void Update()
    {
        if (Time.time > nextWanderTime)
            wanderPoint = ChooseNewWanderPoint();
        //Yes, I am aware how bad and hacky the end game code is
        if (!mController.SFXplaying)
            Destroy(gameObject);
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, wanderPoint, step);
    }

    Vector3 ChooseNewWanderPoint()
    {
        nextWanderTime = Time.time + timeBetweenWanders;
        Vector2 newPos = Random.insideUnitCircle.normalized * maxWanderDistance;
        return initialPosition + new Vector3(newPos.x, newPos.y);
    }
}
