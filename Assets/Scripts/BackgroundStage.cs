using System;
using UnityEngine;

public class BackgroundStage : MonoBehaviour
{
    [SerializeField] private GameConstantsData constants;

    public enum Stage
    {
        Sky,
        Space,
        Sun,
        End
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
    public event EventHandler<ChangedStageArgs> ChangedStage;
    protected virtual void OnChangedStage(ChangedStageArgs e)
    {
        EventHandler<ChangedStageArgs> handler = ChangedStage;
        if (handler != null)
            handler(this, e);
    }

    public event EventHandler<EndGameArgs> EndGame;
    protected virtual void OnEndGame(EndGameArgs e)
    {
        EventHandler<EndGameArgs> handler = EndGame;
        if (handler != null)
            handler(this, e);
    }

    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (playerTransform.position.y > constants.skyEndHeight && CurrentStage == Stage.Sky)
            CurrentStage++;
        if (playerTransform.position.y > constants.spaceEndHeight && CurrentStage == Stage.Space)
            CurrentStage++;
        if (playerTransform.position.y > constants.sunEndHeight && CurrentStage == Stage.Sun)
        {
            OnEndGame(new EndGameArgs { Died = false });
            CurrentStage = Stage.End;
        }
    }
}

public class ChangedStageArgs : EventArgs
{
    public BackgroundStage.Stage NewStage { get; set; }
}

public class EndGameArgs : EventArgs
{
    public bool Died { get; set; }
}