﻿using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("All")]
    [SerializeField] private float distanceAbove = 1f;
    [SerializeField] private float boundaryWidth = 1.0f;
    private float minSpawnX, maxSpawnX;

    [Header("Flies")]
    [SerializeField] private GameObject fly;
    [SerializeField] private float averageDistanceBetweenFlies = 4f;
    [SerializeField] private float rangeDistanceBetweenFlies = 1f;
    private float nextFlySpawnHeight;

    [Header("Birds")]
    [SerializeField] private GameObject bird;
    [SerializeField] private float averageDistanceBetweenBirds = 15f;
    [SerializeField] private float rangeDistanceBetweenBirds = 2f;
    [SerializeField] private float birdXJitter = 0.2f;
    private float nextBirdSpawnHeight;

    [Header("Clouds")]
    [SerializeField] private GameObject cloud;
    [SerializeField] private float averageDistanceBetweenClouds = 3.0f;
    [SerializeField] private float rangeDistanceBetweenClouds = 0.5f;
    private float nextCloudSpawnHeight;

    [Header("Aliens")]
    [SerializeField] private GameObject alien;
    [SerializeField] private float averageDistanceBetweenAliens = 8f;
    [SerializeField] private float rangeDistanceBetweenAliens = 2f;
    private float nextAlienSpawnHeight;

    [Header("Asteroids")]
    [SerializeField] private GameObject asteroid;
    [SerializeField] private float averageDistanceBetweenAsteroids = 15f;
    [SerializeField] private float rangeDistanceBetweenAsteroids = 5f;
    private float nextAsteroidSpawnHeight;

    [Header("GoodSunSprites")]
    [SerializeField] private GameObject goodSprite;
    [SerializeField] private float averageDistanceBetweenGoodSprites = 10f;
    [SerializeField] private float rangeDistanceBetweenGoodSprites = 1f;
    private float nextGoodSpriteSpawnHeight;

    [Header("EvilSunSprites")]
    [SerializeField] private GameObject evilSprite;
    [SerializeField] private float averageDistanceBetweenEvilSprites = 10f;
    [SerializeField] private float rangeDistanceBetweenEvilSprites = 1f;
    private float nextEvilSpriteSpawnHeight;

    private float screenWorldWidth;
    private float screenWorldHeight;
    private BackgroundStage bg;

    void Start()
    {
        bg = GetComponent<BackgroundStage>();

        nextFlySpawnHeight = transform.position.y + averageDistanceBetweenFlies;
        nextBirdSpawnHeight = transform.position.y + averageDistanceBetweenBirds * 2f;
        screenWorldWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        screenWorldHeight = screenWorldWidth * Screen.height / Screen.width;
        minSpawnX = -screenWorldWidth + boundaryWidth;
        maxSpawnX = -minSpawnX;
    }

    void Update()
    {
        if (bg.CurrentStage == BackgroundStage.Stage.Sky)
        {
            if (transform.position.y > nextFlySpawnHeight)
                SpawnFly();
            if (transform.position.y > nextBirdSpawnHeight)
                SpawnBird();
            if (transform.position.y > nextCloudSpawnHeight)
                SpawnCloud();
        }
        else if (bg.CurrentStage == BackgroundStage.Stage.Space)
        {
            if (transform.position.y > nextAsteroidSpawnHeight)
                SpawnAsteroid();
            if (transform.position.y > nextAlienSpawnHeight)
                SpawnAlien();
        }
        else if (bg.CurrentStage == BackgroundStage.Stage.Sun)
        {
            if (transform.position.y > nextGoodSpriteSpawnHeight)
                SpawnGoodSprite();
            if (transform.position.y > nextEvilSpriteSpawnHeight)
                SpawnEvilSprite();
        }
    }

    void SpawnFly()
    {
        nextFlySpawnHeight = transform.position.y + Utility.AroundValue(averageDistanceBetweenFlies, rangeDistanceBetweenFlies);
        float flySpawnX = Random.Range(minSpawnX, maxSpawnX);
        float flySpawnY = DefaultAboveHeight();
        Instantiate(fly, new Vector3(flySpawnX, flySpawnY), Quaternion.identity);
    }

    void SpawnBird()
    {
        nextBirdSpawnHeight = transform.position.y + Utility.AroundValue(averageDistanceBetweenBirds, rangeDistanceBetweenBirds);
        float birdSpawnX = Random.value < 0.5 ? minSpawnX : maxSpawnX + Utility.AroundValue(0, birdXJitter);
        float birdSpawnY = DefaultAboveHeight();
        Instantiate(bird, new Vector3(birdSpawnX, birdSpawnY), Quaternion.identity);
    }

    void SpawnCloud()
    {
        nextCloudSpawnHeight = transform.position.y + Utility.AroundValue(averageDistanceBetweenClouds, rangeDistanceBetweenClouds);
        float cloudSpawnX = minSpawnX * 4f;
        float cloudSpawnY = DefaultAboveHeight();
        Instantiate(cloud, new Vector3(cloudSpawnX, cloudSpawnY, 1), Quaternion.identity);
        //That 1 up there is important for not blocking stuff!
    }

    void SpawnAsteroid()
    {
        nextAsteroidSpawnHeight = transform.position.y + Utility.AroundValue(averageDistanceBetweenAsteroids, rangeDistanceBetweenAsteroids);
        float asteroidSpawnX = Random.value < 0.5 ? minSpawnX * 2f : maxSpawnX * 2f;
        float asteroidSpawnY = DefaultAboveHeight();
        Instantiate(asteroid, new Vector3(asteroidSpawnX, asteroidSpawnY), Quaternion.identity);
    }

    void SpawnAlien()
    {
        nextAlienSpawnHeight = transform.position.y + Utility.AroundValue(averageDistanceBetweenAliens, rangeDistanceBetweenAliens);
        bool leftSide = Random.value < 0.5;
        float alienSpawnX = leftSide ? minSpawnX : maxSpawnX;
        float alienSpawnY = DefaultAboveHeight();
        GameObject alienToSpawn = Instantiate(alien, new Vector3(alienSpawnX, alienSpawnY), Quaternion.identity) as GameObject;
        alienToSpawn.GetComponent<Alien>().Setup(minSpawnX, maxSpawnX, leftSide ? 1 : -1);
    }

    private void SpawnGoodSprite()
    {
        nextGoodSpriteSpawnHeight = transform.position.y + Utility.AroundValue(averageDistanceBetweenGoodSprites, rangeDistanceBetweenGoodSprites);
        float goodSpriteSpawnX = Random.Range(minSpawnX, maxSpawnX);
        float goodSpriteSpawnY = DefaultAboveHeight();
        Instantiate(goodSprite, new Vector3(goodSpriteSpawnX, goodSpriteSpawnY), Quaternion.identity);
    }

    private void SpawnEvilSprite()
    {
        nextEvilSpriteSpawnHeight = transform.position.y + Utility.AroundValue(averageDistanceBetweenEvilSprites, rangeDistanceBetweenEvilSprites);
        float evilSpriteSpawnX = Random.Range(minSpawnX, maxSpawnX);
        float evilSpriteSpawnY = DefaultAboveHeight();
        Instantiate(evilSprite, new Vector3(evilSpriteSpawnX, evilSpriteSpawnY), Quaternion.identity);
    }

    float DefaultAboveHeight()
    {
        return transform.position.y + screenWorldHeight + distanceAbove;
    }
}
