using System;
using UnityEngine;

public class BackgroundStage : MonoBehaviour
{
    [SerializeField] private GameConstantsData constants;

    public enum Stage
    {
        Sky,
        Space,
        Sun
    }

    private Stage _currentStage = Stage.Sky;
    public Stage CurrentStage
    {
        get { return _currentStage; }
        set
        {
            _currentStage = value; 
            OnChangedStage(new ChangedStageArgs { NewStage = _currentStage });
        }
    }

    //Events
    [HideInInspector] public event EventHandler<ChangedStageArgs> ChangedStage;
    protected virtual void OnChangedStage(ChangedStageArgs e)
    {
        EventHandler<ChangedStageArgs> handler = ChangedStage;
        if (handler != null)
            handler(this, e);
    }

    void Update()
    {
        if (transform.position.y > constants.skyEndHeight && CurrentStage == Stage.Sky)
            CurrentStage++;
        if (transform.position.y > constants.spaceEndHeight && CurrentStage == Stage.Space)
            CurrentStage++;
    }
}

public class ChangedStageArgs : EventArgs
{
    public BackgroundStage.Stage NewStage { get; set; }
}