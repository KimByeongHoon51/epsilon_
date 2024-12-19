using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BlackHole : MonoBehaviour
{
    public GameObject currentBackground; // 현재 배경 (Background(basic))
    public GameObject newBackground;     // 새로운 배경 (Background(delta))
    public Collider2D newBoundaryCollider; // 새 배경의 경계 Collider
    public CinemachineConfiner2D cinemachineConfiner; // Cinemachine Confiner 2D 컴포넌트
    public GameObject[] meteorSpawners; // 여러 메테오 생성기 배열

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // PlayerBoundary 컴포넌트 가져오기
            PlayerBoundary playerBoundary = other.GetComponent<PlayerBoundary>();

            // 현재 배경 비활성화
            if (currentBackground != null)
            {
                currentBackground.SetActive(false);
                Collider2D currentCollider = currentBackground.GetComponent<Collider2D>();
                if (currentCollider != null)
                {
                    currentCollider.enabled = false; // 현재 배경 Collider 비활성화
                }
                Debug.Log("Current Background deactivated and collider disabled.");
            }

            // 새로운 배경 활성화
            if (newBackground != null)
            {
                newBackground.SetActive(true);
                Collider2D newCollider = newBackground.GetComponent<Collider2D>();
                if (newCollider != null)
                {
                    newCollider.enabled = true; // 새 배경 Collider 활성화
                }
                Debug.Log("New Background activated and collider enabled.");
            }

            // PlayerBoundary의 경계 Collider를 업데이트
            if (playerBoundary != null && newBoundaryCollider != null)
            {
                playerBoundary.SetBoundary(newBoundaryCollider); // 새 경계 설정
                Debug.Log("Player boundary updated to new background collider.");
            }

            // Cinemachine Confiner 2D의 Bounding Shape 업데이트
            if (cinemachineConfiner != null && newBoundaryCollider != null)
            {
                cinemachineConfiner.m_BoundingShape2D = newBoundaryCollider; // 새로운 Collider 설정
                cinemachineConfiner.InvalidateCache(); // 경계 캐시 무효화
                Debug.Log("Cinemachine Confiner updated to new boundary.");
            }

            // **메테오 생성기 활성화**
           foreach (GameObject spawner in meteorSpawners)
        {
            if (spawner != null)
            {
                spawner.SetActive(true);
            }
        }

            // 블랙홀 비활성화
            gameObject.SetActive(false);
            Debug.Log("Black Hole deactivated.");
        }
    }
}



