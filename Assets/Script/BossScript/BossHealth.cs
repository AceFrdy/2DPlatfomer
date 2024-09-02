using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
	public Slider healthSlider;
	public TMP_Text healthBarText;
	private Damageable bossDamageable;

	private void Awake()
	{
		GameObject boss = GameObject.FindGameObjectWithTag("Boss");
		bossDamageable = boss.GetComponent<Damageable>();

		if (boss == null)
		{
			Debug.LogError("No Damageable component found on Boss!");
		}
	}

	private void Start()
    {
        // Inisialisasi health bar
        if (bossDamageable != null)
        {
            healthSlider.value = CalculateSliderPercentage(bossDamageable.Health, bossDamageable.MaxHealth);
            // healthBarText.text = " HP " + bossDamageable.Health + " / " + bossDamageable.MaxHealth;
        }
    }
	public void OnEnable()
    {
        if (bossDamageable != null)
        {
            bossDamageable.healthChanged.AddListener(OnBossHealthChanged);
        }
    }

    public void OnDisable()
    {
        if (bossDamageable != null)
        {
            bossDamageable.healthChanged.RemoveListener(OnBossHealthChanged);
        }
    }

	private void OnDestroy()
	{
		if (bossDamageable != null)
		{
			bossDamageable.healthChanged.RemoveListener(OnBossHealthChanged);
		}
	}

	private float CalculateSliderPercentage(float currentHealth, float maxHealth)
	{
		return currentHealth / maxHealth;
	}

	private void OnBossHealthChanged(int newHealth, int maxHealth)
	{
		healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
		// healthBarText.text = " HP " + newHealth + " / " + maxHealth;
	}
	void DestroyPlayer()
{
    // Logika penghancuran player
    Destroy(gameObject); // Menghancurkan player

    // Panggil OnPlayerDestroyed pada semua BossController
    BossController[] bosses = FindObjectsOfType<BossController>();
    foreach (BossController boss in bosses)
    {
        boss.OnPlayerDestroyed();
    }
}

}
