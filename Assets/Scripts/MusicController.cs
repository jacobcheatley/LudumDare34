﻿using System;
using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioMixerSnapshot sky;
    [SerializeField] private AudioMixerSnapshot space;
    [SerializeField] private AudioMixerSnapshot sun;
    [SerializeField] private AudioMixerSnapshot sadness;

    [SerializeField] private float transitionTime = 1f;
    private BackgroundStage bg;

    void Start()
    {
        bg = GetComponent<BackgroundStage>();
        bg.ChangedStage += ChangedStage;
        bg.EndGame += EndGame;
    }

    private void EndGame(object sender, EventArgs e)
    {
        sadness.TransitionTo(transitionTime);
    }

    private void ChangedStage(object sender, ChangedStageArgs e)
    {
        switch (e.NewStage)
        {
            case BackgroundStage.Stage.Sky:
                sky.TransitionTo(transitionTime);
                break;
            case BackgroundStage.Stage.Space:
                space.TransitionTo(transitionTime);
                break;
            case BackgroundStage.Stage.Sun:
                sun.TransitionTo(transitionTime);
                break;
        }
    }
}
