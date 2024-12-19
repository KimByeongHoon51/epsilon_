using UnityEngine;

public class DeltaShip : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private GameObject player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            player = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            player = null;
        }
    }

    public bool TryUpgradePlayer()
    {
        if (isPlayerNearby && player != null)
        {
            // 체력 감소량 업데이트
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ModifyFuelDecreaseAmount(0.8f); // 체력 감소량 0.8로 설정
                Debug.Log("DeltaShip: Fuel decrease amount updated.");
            }

            // 이동 속도 증가
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.moveSpeed = 6f; // 이동 속도를 6로 설정
                Debug.Log("DeltaShip: Player's speed upgraded to 6!");
            }

            return true; // 성공적으로 업그레이드 완료
        }

        return false; // 업그레이드 실패
    }
}
