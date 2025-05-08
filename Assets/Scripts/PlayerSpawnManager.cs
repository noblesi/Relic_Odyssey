using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private void Start()
    {
        GameObject spawnPoint = GameObject.FindWithTag("PlayerSpawn");
        if(spawnPoint != null && playerPrefab != null)
        {
            Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("스폰 지점 또는 플레이어 프리팹이 존재하지 않습니다.");
        }
    }
}
