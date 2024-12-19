using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceGarbage : MonoBehaviour
{
    public float newSpeed = 4f; // 고정된 속도 값
    private bool isPlayerNearby = false; // 플레이어 근처 상태
    private GameObject player; // 플레이어 오브젝트 저장

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
            isPlayerNearby = false; // 플레이어가 떠난 상태로 변경
            player = null;
        }
    }

    public bool TryConsumeGarbage()
    {
        if (isPlayerNearby && player != null)
        {
            // PlayerController 가져오기
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.moveSpeed = newSpeed; // 속도를 직접 변경
                Debug.Log($"SpaceGarbage consumed! New speed: {newSpeed}");
                Destroy(gameObject); // 우주쓰레기 제거
                return true; // 성공적으로 소비됨
            }
        }
        return false; // 소비 실패
    }
}
