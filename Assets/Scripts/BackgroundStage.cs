using UnityEngine;

public class BackgroundStage : MonoBehaviour
{
    public enum Stage
    {
        Sky,
        Space,
        Sun
    }
    [HideInInspector] public Stage stage = Stage.Sky;
}
