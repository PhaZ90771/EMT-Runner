using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] ObstaclePrefabs;

    private PlayerController playerController;

    private readonly float startDelay = 2f;
    private readonly float repeatRate = 2f;
    private bool isSpawning = false;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (!isSpawning && playerController.IsPlaying) TryStartSpawn();
    }

    private void TryStartSpawn()
    {
        if (!playerController.IsPlaying || isSpawning) return;
        InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);
        isSpawning = true;
    }

    private void SpawnObstacle()
    {
        if (!playerController.IsPlaying)
        {
            CancelInvoke(nameof(SpawnObstacle));
            isSpawning = false;
            return;
        }

        int index = Random.Range(0, ObstaclePrefabs.Length);
        var prefab = ObstaclePrefabs[index];
        var position = transform.position;
        var rotation = prefab.transform.rotation;
        Instantiate(prefab, position, rotation);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        var gizmosColor = Color.magenta;
        Gizmos.color = gizmosColor;
        Handles.color = gizmosColor;

        var guiStyle = new GUIStyle();
        guiStyle.alignment = TextAnchor.LowerCenter;
        guiStyle.normal.textColor = gizmosColor;

        Gizmos.DrawWireCube(transform.position, Vector3.one);
        Handles.Label(transform.position + 1.5f * Vector3.up, "Spawn Manager", guiStyle);
    }
#endif
}
