using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image background;
    [Space]
    [SerializeField] private Gradient gradient;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = GameStats.currentHealth;

        background.color = gradient.Evaluate(1f);
    }

    private void Update()
    {
        slider.value = GameStats.currentHealth;
        background.color = gradient.Evaluate(slider.normalizedValue);
    }
}
