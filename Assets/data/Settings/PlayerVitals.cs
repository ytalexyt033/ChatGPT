using UnityEngine;
using UnityEngine.UI;

public class PlayerVitals : MonoBehaviour
{
    public float maxHP = 100f;
    public float maxStamina = 100f;
    public float staminaRegenRate = 5f;
    public float staminaRunCost = 10f; // Расход выносливости при беге в секунду
    public float staminaJumpCost = 20f; // Расход выносливости при прыжке

    private float currentHP;
    private float currentStamina;

    public Slider healthSlider;
    public Slider staminaSlider;

    private void Start()
    {
        currentHP = maxHP;
        currentStamina = maxStamina;
        UpdateUI();
    }

    private void Update()
    {
        // Регенерация выносливости
        if (currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            UpdateUI();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        UpdateUI();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public bool UseStamina(float amount)
    {
        if (currentStamina >= amount)
        {
            currentStamina -= amount;
            UpdateUI();
            return true;
        }
        return false;
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        // Логика смерти (например, рестарт уровня)
    }

    private void UpdateUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHP / maxHP;
        }

        if (staminaSlider != null)
        {
            staminaSlider.value = currentStamina / maxStamina;
        }
    }

    public bool CanRun() // Проверка, достаточно ли выносливости для бега
    {
        return currentStamina > staminaRunCost * Time.deltaTime;
    }

    public bool CanJump() // Проверка, достаточно ли выносливости для прыжка
    {
        return currentStamina >= staminaJumpCost;
    }
}
