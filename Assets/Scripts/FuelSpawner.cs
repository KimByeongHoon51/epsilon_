using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSpawner : MonoBehaviour
{
    public GameObject fuelPrefab; // 연료 프리팹
    public int fuelCount = 20; // 생성할 연료 개수
    public Vector2 mapSize = new Vector2(80f, 45f); // 맵 크기 (가로, 세로)
    public Transform spawnArea; // 스폰 영역의 중심 위치

    private bool hasSpawned = false; // 연료가 이미 생성되었는지 확인

    void Start()
    {
        // 게임 시작 시 연료 생성
        SpawnFuel();
    }

    void SpawnFuel()
    {
        if (hasSpawned) return; // 이미 연료를 생성했으면 중단

        for (int i = 0; i < fuelCount; i++)
        {
            // 맵 크기 내 랜덤 위치 생성
            Vector2 randomPosition = new Vector2(
                Random.Range(-mapSize.x / 2, mapSize.x / 2),
                Random.Range(-mapSize.y / 2, mapSize.y / 2)
            );

            // 스폰 영역의 위치를 기준으로 위치 오프셋 적용
            Vector3 spawnPosition = spawnArea.position + (Vector3)randomPosition;

            // 연료 생성
            Instantiate(fuelPrefab, spawnPosition, Quaternion.identity);
        }

        hasSpawned = true; // 연료 생성 완료
    }

    void OnDrawGizmos()
    {
        // 맵 범위를 시각적으로 표시하기 위한 기즈모
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(spawnArea.position, new Vector3(mapSize.x, mapSize.y, 0));
    }
}
