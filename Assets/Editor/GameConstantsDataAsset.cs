using UnityEditor;

public class GameConstantsDataAsset
{
    [MenuItem("Assets/Create/GameConstantsData")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<GameConstantsData>();
    }
}