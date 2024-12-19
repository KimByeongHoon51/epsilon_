using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceGarbageSpawner : MonoBehaviour
{
    public GameObject spaceGarbagePrefab; // 우주 쓰레기 프리팹
    public int garbageCount = 10; // 생성할 우주 쓰레기 개수
    public Vector2 mapSize = new Vector2(80f, 45f); // 맵 크기 (가로, 세로)
    public Transform spawnArea; // 스폰 영역의 중심 위치

    private bool hasSpawned = false; // 우주 쓰레기가 이미 생성되었는지 확인

    void Start()
    {
        // 게임 시작 시 우주 쓰레기 생성
        SpawnSpaceGarbage();
    }

    void SpawnSpaceGarbage()
    {
        if (hasSpawned) return; // 이미 생성했으면 중단

        for (int i = 0; i < garbageCount; i++)
        {
            // 맵 크기 내 랜덤 위치 생성
            Vector2 randomPosition = new Vector2(
                Random.Range(-mapSize.x / 2, mapSize.x / 2),
                Random.Range(-mapSize.y / 2, mapSize.y / 2)
            );

            // 스폰 영역의 위치를 기준으로 오프셋 적용
            Vector3 spawnPosition = spawnArea.position + (Vector3)randomPosition;

            // 우주 쓰레기 생성
            Instantiate(spaceGarbagePrefab, spawnPosition, Quaternion.identity);
        }

        hasSpawned = true; // 우주 쓰레기 생성 완료
    }

}