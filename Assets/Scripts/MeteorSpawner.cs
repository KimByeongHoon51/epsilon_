using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorPrefab;       // 메테오 프리팹
    public float minSpawnTime = 1f;       // 최소 생성 시간
    public float maxSpawnTime = 4f;       // 최대 생성 시간

    private bool isPlayerAlive = true;    // 플레이어 생존 상태

    void Start()
    {
        StartCoroutine(SpawnMeteor());
    }

    private IEnumerator SpawnMeteor()
    {
        while (isPlayerAlive) // 플레이어가 살아있을 때만 실행
        {
            float spawnDelay = Random.Range(minSpawnTime, maxSpawnTime); // 랜덤 생성 시간
            yield return new WaitForSeconds(spawnDelay);
            SpawnMeteorFromSpawner();
        }
    }

    private void SpawnMeteorFromSpawner()
    {
        // 메테오 생성
        GameObject meteor = Instantiate(meteorPrefab, transform.position, Quaternion.identity);

        // 플레이어를 타겟으로 설정
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Meteor meteorScript = meteor.GetComponent<Meteor>();
            if (meteorScript != null)
            {
                meteorScript.SetTarget(player.transform);
            }
        }
    }

    public void StopSpawning()
    {
        isPlayerAlive = false; // 플레이어가 죽으면 스포너 중지
        Debug.Log("Meteor Spawner stopped: Player is dead.");
    }
}
