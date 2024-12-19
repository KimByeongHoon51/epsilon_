using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; // 최대 체력
    public float currentHealth;   // 현재 체력
    public Slider healthSlider;   // 체력 UI 슬라이더
    public float fuelDecreaseInterval = 1.0f; // 체력 감소 주기
    public float fuelDecreaseAmount = 1.0f;   // 체력 감소량

    private Coroutine fuelCoroutine; // 체력 감소 관리 코루틴

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        StartFuelConsumption();
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            DisablePlayer();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
        Debug.Log($"Player took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            DisablePlayer();
        }
    }

    public void RestoreHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    public void ModifyFuelDecreaseAmount(float newAmount)
    {
        fuelDecreaseAmount = newAmount;
        Debug.Log($"Fuel decrease amount updated to {fuelDecreaseAmount} per second.");
    }

    private void StartFuelConsumption()
    {
        if (fuelCoroutine != null)
        {
            StopCoroutine(fuelCoroutine);
        }
        fuelCoroutine = StartCoroutine(ConsumeFuel());
    }

    private IEnumerator ConsumeFuel()
    {
        while (currentHealth > 0)
        {
            yield return new WaitForSeconds(fuelDecreaseInterval);
            currentHealth -= fuelDecreaseAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            UpdateHealthUI();

            if (currentHealth <= 0)
            {
                Debug.Log("Out of fuel!");
                StopCoroutine(fuelCoroutine);
            }
        }
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }

    void DisablePlayer()
    {
        Debug.Log("Player has died! Disabling player object.");
        gameObject.SetActive(false);

         // 모든 MeteorSpawner를 비활성화
        MeteorSpawner[] spawners = FindObjectsOfType<MeteorSpawner>();
        foreach (MeteorSpawner spawner in spawners)
        {
                spawner.StopSpawning();
        }
    }


}
