using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f; // 현재 이동 속도
    public float rotateSpeed = 180f; // 회전 속도
    private Rigidbody2D rb;

    private float originalSpeed; // 원래 이동 속도 저장용

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalSpeed = moveSpeed; // 초기 이동 속도 저장
    }

    void Update()
{
    // 스페이스바 입력 처리
    if (Input.GetKeyDown(KeyCode.Space))
    {
        InteractWithNearbyObjects();
    }
}
    void FixedUpdate()
    {
        // 이동 처리
        float verticalInput = Input.GetAxisRaw("Vertical");
        rb.velocity = -transform.right * verticalInput * moveSpeed;

        // 회전 처리
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.angularVelocity = -horizontalInput * rotateSpeed;
    }

    private void InteractWithNearbyObjects()
    {
    // 플레이어 주변 Collider 탐색
        Collider2D[] nearbyColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f); // 반경 0.5f
        foreach (Collider2D collider in nearbyColliders)
        {
        // 연료 상호작용
        Fuel fuel = collider.GetComponent<Fuel>();
        if (fuel != null)
        {
            if (fuel.TryConsumeFuel())
            {
                Debug.Log("연료를 성공적으로 먹었습니다!");
                break; // 한 번만 실행
            }
        }

        // 우주쓰레기 상호작용
        SpaceGarbage garbage = collider.GetComponent<SpaceGarbage>();
        if (garbage != null)
        {
            if (garbage.TryConsumeGarbage())
            {
                Debug.Log("우주쓰레기를 성공적으로 먹었습니다!");
                break; // 한 번만 실행
            }
        }

       DeltaSpaceGarbage deltagarbage = collider.GetComponent<DeltaSpaceGarbage>();
            if (deltagarbage != null)
            {
                if (deltagarbage.TryConsumeGarbage())
                {
                    Debug.Log("Delta Space Garbage consumed! Speed permanently changed to 5.");
                    break;
                }
            }

        DeltaShip deltaShip = collider.GetComponent<DeltaShip>(); // 델타쉽 상호작용 후 플레이어 체력 속도 감소, 속도 증가
        if (deltaShip != null && deltaShip.TryUpgradePlayer())
        {
            Debug.Log("Player's fuel decrease amount updated by DeltaShip!");
            break;
        }

        if (deltaShip != null && deltaShip.TryUpgradePlayer())
        {
            Debug.Log("Player's speed upgraded by DeltaShip!");
            break;
        }
        
        }
    }

}
