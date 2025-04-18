using UnityEngine;

public class PlayerVitals : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float healthRegenRate = 1f;
    
    [Header("Stamina Settings")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaRegenRate = 2f;
    [SerializeField] private float runStaminaCost = 10f;
    [SerializeField] private float jumpStaminaCost = 20f;

    private float _currentHealth;
    private float _currentStamina;
    private bool _isRunning;

    public float Health => _currentHealth;
    public float Stamina => _currentStamina;
    public bool HasStaminaForRun => _currentStamina > runStaminaCost;

    private void Awake()
    {
        _currentHealth = maxHealth;
        _currentStamina = maxStamina;
    }

    private void Update()
    {
        if (_isRunning)
        {
            _currentStamina -= runStaminaCost * Time.deltaTime;
        }
        else
        {
            _currentStamina += staminaRegenRate * Time.deltaTime;
        }

        _currentStamina = Mathf.Clamp(_currentStamina, 0, maxStamina);
        _currentHealth = Mathf.Clamp(_currentHealth + healthRegenRate * Time.deltaTime, 0, maxHealth);
    }

    public bool TryUseJumpStamina()
    {
        if (_currentStamina >= jumpStaminaCost)
        {
            _currentStamina -= jumpStaminaCost;
            return true;
        }
        return false;
    }

    public void SetRunning(bool isRunning)
    {
        _isRunning = isRunning;
    }
}