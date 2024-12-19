using UnityEngine;

public class Fuel : MonoBehaviour
{
    public float healthRestoreAmount = 20f; // 회복량
    private bool isPlayerNearby = false; // 플레이어 근처 확인
    private GameObject player; // 충돌한 플레이어 저장

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true; // 플레이어 근처 상태 설정
            player = other.gameObject; // 플레이어 오브젝트 저장
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false; // 플레이어 떠남
            player = null; // 플레이어 오브젝트 초기화
        }
    }

    public bool TryConsumeFuel()
    {
        if (isPlayerNearby && player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.RestoreHealth(healthRestoreAmount); // 체력 회복
                Destroy(gameObject); // 연료 오브젝트 제거
                return true; // 성공적으로 소비됨
            }
        }
        return false; // 소비 실패
    }
}
