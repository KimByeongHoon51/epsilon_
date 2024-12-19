using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingSpaceGarbage : MonoBehaviour
{
    public string orbitCenterTag = "OrbitCenter"; // 중심점을 찾기 위한 태그
    public float orbitSpeed = 40f; // 공전 속도 (도/초)
    public float orbitRadius = 3f; // 공전 반경
    public float damage = 10f; // 플레이어에게 줄 데미지
    public float initialAngle = 0f; // 초기 각도 설정

    private Transform orbitCenter; // 중심점
    private float currentAngle; // 현재 각도

    void Start()
    {
        // 중심점 찾기
        GameObject centerObject = GameObject.FindGameObjectWithTag(orbitCenterTag);
        if (centerObject != null)
        {
            orbitCenter = centerObject.transform;
        }
        else
        {
            Debug.LogError("OrbitCenter not found! Ensure it has the correct tag.");
            return;
        }

        currentAngle = initialAngle; // 초기 각도 설정
    }

    void Update()
    {
        if (orbitCenter != null)
        {
            // 공전 로직
            currentAngle += orbitSpeed * Time.deltaTime; // 각도 증가
            float radians = currentAngle * Mathf.Deg2Rad; // 라디안 변환

            // 새로운 위치 계산
            float x = orbitCenter.position.x + Mathf.Cos(radians) * orbitRadius;
            float y = orbitCenter.position.y + Mathf.Sin(radians) * orbitRadius;

            transform.position = new Vector3(x, y, 0f); // 위치 설정
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어와 충돌 시 체력 감소 및 우주쓰레기 제거
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log("Player hit by orbiting space garbage! Damage: " + damage);
            }

            // 우주쓰레기 제거
            Destroy(gameObject);
        }
    }
}
