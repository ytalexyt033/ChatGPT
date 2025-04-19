using UnityEngine;

public class PlayerVitals : MonoBehaviour
{
    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float staminaRegen = 5f;
    public float runCost = 10f; // Стамина в секунду
    public float jumpCost = 20f;

    private float currentStamina;
    private bool isRunning;

    private void Start() => currentStamina = maxStamina;

    private void Update()
    {
        if (!isRunning)
            currentStamina = Mathf.Min(currentStamina + staminaRegen * Time.deltaTime, maxStamina);
    }

    public bool TryUseStamina(string action)
    {
        float cost = action switch
        {
            "run" => runCost * Time.deltaTime,
            "jump" => jumpCost,
            _ => 0f
        };

        if (currentStamina >= cost)
        {
            currentStamina -= cost;
            isRunning = action == "run";
            return true;
        }
        return false;
    }
}