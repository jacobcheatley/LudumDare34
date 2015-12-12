using UnityEngine;

public class LeafGenerator : MonoBehaviour
{
    [SerializeField] private float averageLeafDistance = 0.4f;
    [SerializeField] private float leafDistanceRange = 0.1f;
    [SerializeField] private float stalkWidth = 0.3f;
    [SerializeField] private GameObject leaf;

    private float heightOfNextLeaf;

    private void Start()
    {
        heightOfNextLeaf = transform.position.y;
    }

    private void Update()
    {
        if (transform.position.y > heightOfNextLeaf)
        {
            bool leftSide = Random.value < 0.5;
            float horizontalPositionOffset = leftSide ? -stalkWidth / 2 : stalkWidth / 2;
            GameObject newLeaf = Instantiate(leaf,
                                             transform.position + new Vector3(horizontalPositionOffset, 0f),
                                             Quaternion.Euler(0, leftSide ? 0 : 180, 0)) as GameObject;
            heightOfNextLeaf = transform.position.y + averageLeafDistance + Random.Range(-leafDistanceRange, leafDistanceRange);
        }
    }
}
