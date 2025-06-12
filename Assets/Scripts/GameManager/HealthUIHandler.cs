using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class HealthUIHandler : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private UIDocument uiDocument;

    private VisualElement healthBarMask;
    private Label healthLabel;
    private int currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (uiDocument != null)
        {
            VisualElement root = uiDocument.rootVisualElement;
            healthBarMask = root.Q<VisualElement>("HealthBarMask");
            healthLabel = root.Q<Label>("HealthLabel");
        }
    }

    private void Start()
    {
        if (characterData != null)
        {
            UpdateHealthUI();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(characterData != null && currentHealth != characterData.CurrentHealth)
        {
            UpdateHealthUI();
        }
    }

    public void UpdateHealthUI()
    {
        currentHealth = characterData.CurrentHealth;
        if (healthBarMask != null)
        {
            float healthPercent = Mathf.Lerp(8f, 88f, currentHealth / characterData.GetMaxHealth());
            healthBarMask.style.width = Length.Percent(healthPercent);
        }
        if (healthLabel != null)
        {
            healthLabel.text = $"{currentHealth}/{characterData.GetMaxHealth()}";
        }
    }
}
