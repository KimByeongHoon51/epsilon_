using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundary : MonoBehaviour
{
    public Collider2D boundaryCollider; // 현재 경계를 설정하는 Collider
    private Bounds bounds;

    void Start()
    {
        if (boundaryCollider != null)
        {
            UpdateBoundary(); // 초기 경계 설정
        }
    }

    void Update()
    {
        if (boundaryCollider == null) return;

        // 현재 플레이어 위치
        Vector3 position = transform.position;

        // 플레이어 위치를 경계 안으로 Clamp
        position.x = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
        position.y = Mathf.Clamp(position.y, bounds.min.y, bounds.max.y);

        // 위치 적용
        transform.position = position;
    }

    public void SetBoundary(Collider2D newBoundary)
    {
        // 새로운 경계를 설정
        boundaryCollider = newBoundary;
        UpdateBoundary(); // 경계 값 업데이트
    }

    private void UpdateBoundary()
    {
        bounds = boundaryCollider.bounds; // 경계 Bounds 업데이트
        Debug.Log("Boundary updated to: " + bounds);
    }
}
