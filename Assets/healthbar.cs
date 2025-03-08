using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider; // Assign in Inspector or auto-find
    [SerializeField] private float maxHealth = 100f;  // Maximum health

    void Awake()
    {
        // Try to auto-find the Slider on the current GameObject
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }
        // If not found, try to auto-find the Slider in the children
        if (slider == null)
        {
            slider = GetComponentInChildren<Slider>();
        }
        // If still not found, log an error
        if (slider == null)
        {
            Debug.LogError("HealthBar: No Slider component found on the GameObject or its children. Please assign one in the Inspector.");
        }
    }

    void Start()
    {
        SetMaxHealth(maxHealth);
    }

    public void SetMaxHealth(float health)
    {
        if (slider != null)
        {   
            slider.maxValue = health;
            slider.value = health;
        }
    }

    public void SetHealth(float health)
    {
        if (slider != null)
        {
            slider.value = health;
        }
    }
}
