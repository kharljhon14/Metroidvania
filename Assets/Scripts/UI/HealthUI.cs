using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab = null;
    [SerializeField] private GameObject heartContainer;
    [SerializeField] private AgentHealth playerHealth;

    private int heartCount = 0;
    private List<Image> hearts = new List<Image>();

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        heartCount = playerHealth.currentHealth;

        foreach (Transform child in heartContainer.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < playerHealth.currentHealth; i++)
        {
            hearts.Add(Instantiate(heartPrefab, heartContainer.transform).GetComponent<Image>());
        }
    }

    public void UpdateUI()
    {
        int currentIndex = 1;

        for (int i = 0; i < heartCount; i++)
        {
            if (currentIndex >= playerHealth.currentHealth)
                hearts[i].gameObject.SetActive(false);

            else
                hearts[i].gameObject.SetActive(true);

            currentIndex++;
        }
    }
}
