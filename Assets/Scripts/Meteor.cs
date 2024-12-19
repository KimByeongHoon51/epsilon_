using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float speed = 8f;               // 오브젝트 속도
    public float lifetime = 50f;           // 10초 후 자동 파괴
    public float damage = 10f;             // 플레이어에게 줄 데미지

    private Vector2 moveDirection;         // 초기 이동 방향
    private Rigidbody2D rb;                // Rigidbody2D 컴포넌트 참조

    public void SetTarget(Transform newTarget)
    {
        if (newTarget != null)
        {
            // 플레이어 방향을 기준으로 초기 이동 방향 설정
            moveDirection = (newTarget.position - transform.position).normalized;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = moveDirection * speed; // 초기 방향으로 속도 설정
        Destroy(gameObject, lifetime);       // 10초 후 Meteor 오브젝트 파괴
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 목표와 충돌 시 데미지를 입힘
        if (other.CompareTag("Player"))
        {
            // 플레이어의 체력을 감소시키는 메서드 호출
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Meteor 파괴
            Destroy(gameObject);
        }
    }
}
