using UnityEngine;
using UnityEngine.UI;


public class HealthBarShow : MonoBehaviour
{

    private Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {   
        float healthFromScricpt = PlayerHealth.health;
        float maxHealthFromScript = PlayerHealth.maxHealth;
        healthBar.fillAmount = healthFromScricpt / maxHealthFromScript;
    }
}
