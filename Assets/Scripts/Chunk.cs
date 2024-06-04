using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private TreeSpawner _treeSpawnerPrefab = null;

    [SerializeField] private int _wallCount = 0;

    public int WallCount { get { return _wallCount; } }

    private void Start()
    {
        if(_treeSpawnerPrefab != null)
        {
            SpawnTreeSpawner();
        }
    }

    private void SpawnTreeSpawner()
    {
        TreeSpawner treeSpawnerInstance = Instantiate(_treeSpawnerPrefab, transform.position, Quaternion.identity);
        treeSpawnerInstance.transform.SetParent(transform);
        treeSpawnerInstance.transform.localPosition = Vector3.zero;
    }
}
