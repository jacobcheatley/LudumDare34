using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem), typeof(AudioSource))]
public class BlazeOfGlory : MonoBehaviour
{
    [SerializeField] private float delay;

    private bool alreadyExploding;
    private AudioSource explosionSource;
    private ParticleSystem explosionSystem;
    private BackgroundStage bg;

    void Start()
    {
        explosionSource = GetComponent<AudioSource>();
        bg = transform.parent.GetComponent<BackgroundStage>();
        bg.EndGame += EXPLOSIONS;
        explosionSystem = GetComponent<ParticleSystem>();
    }

    private void EXPLOSIONS(object sender, EventArgs eventArgs)
    {
        if (!alreadyExploding)
        {
            alreadyExploding = true;
            StartCoroutine(ACTUALLYEXPLODE());
        }
    }

    IEnumerator ACTUALLYEXPLODE()
    {
        yield return new WaitForSeconds(delay);
        explosionSystem.Play();
        explosionSource.Play();
    }
}
