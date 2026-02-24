using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIController : MonoBehaviour
{
    public static UIController instance;
    [Header("Energy Bar")]
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text TMPro;
    [Header("Health Bar")]
    [SerializeField] private Slider Health;
    [SerializeField] private TMP_Text Healthtext;
    [Header("Experience Bar")]
    [SerializeField] private Slider Experienceslider;
    [SerializeField] private TMP_Text Experiencetext;



    [SerializeField] public GameObject Paused;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }
    public void UpdateEnergy(float currentEnergy, float maxEnergyy)
    {
        slider.maxValue = maxEnergyy;
        slider.value = Mathf.RoundToInt(currentEnergy);
        TMPro.text= slider.value + "/" + slider.maxValue;

    }
    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        Health.maxValue = maxHealth;
        Health.value = Mathf.RoundToInt(currentHealth);
        Healthtext.text= Health.value + "/" + Health.maxValue;

    }
    public void UpdateExperience(float currentExperience, float maxExperience)
    {
        Experienceslider.maxValue = maxExperience;
        Experienceslider.value = Mathf.RoundToInt(currentExperience);
        Experiencetext.text= Experienceslider.value + "/" + Experienceslider.maxValue;

    }
   
}
