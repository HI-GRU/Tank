using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRadius = 15F;    // 생성 반경
    [SerializeField] private float spawnInterval = 3F;   // 생성 간격

    private Camera mainCamera;
    private float timer = 0f;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        // 플레이어 주변 랜덤한 위치 계산
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 playerPosition = Player.Instance.transform.position;
        Vector3 spawnPosition = playerPosition + new Vector3(randomDirection.x, randomDirection.y, 0F) * spawnRadius;

        // 화면에 보이는 위치인지 확인
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(spawnPosition);
        if (viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
            viewportPoint.y >= 0 && viewportPoint.y <= 1)
        {
            // 화면 안에 있으면 더 멀리 생성
            spawnPosition = playerPosition + new Vector3(randomDirection.x, randomDirection.y, 0f) * (spawnRadius * 1.5f);
        }

        // 적 생성
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    // Editor에서 생성 반경을 시각적으로 확인하기 위한 기즈모
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (Player.Instance != null)
        {
            Gizmos.DrawWireSphere(Player.Instance.transform.position, spawnRadius);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(Player.Instance.transform.position, spawnRadius * 1.5f);
        }
    }
}