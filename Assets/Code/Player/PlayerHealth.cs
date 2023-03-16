using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    private Slider hpBar;
    private Slider exhastedHpBar;

    private void Start()
    {
        hpBar = UI.Instance.HpBar;
        exhastedHpBar = UI.Instance.ExhastedHpBar;
        hpBar.maxValue = healthDetails.MaximumHealth;
        exhastedHpBar.maxValue = healthDetails.MaximumHealth;
      
    }

    public void SetStartingHealth(int startingHeath)
    {
        this.healthDetails.StartingHeath = startingHeath;
        currentHealth = startingHeath;
        GameManager.Instance.UI.HpBar.value = currentHealth;
    }

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
        hpBar.value = currentHealth;
        StartCoroutine(ShowStaminaCost());
    }

    protected override void Die()
    {
        base.Die();
        UI.Instance.DeathMenu.gameObject.SetActive(true);
    }

    public void AddHealth(float health)
    {
        print(health);
        if (currentHealth+health>healthDetails.MaximumHealth)
            currentHealth = healthDetails.MaximumHealth;
        else
            currentHealth = currentHealth + health;
           
        hpBar.value = currentHealth;
        print(currentHealth);
    }

    private IEnumerator ShowStaminaCost()
    {
        if (exhastedHpBar.value < currentHealth)
            exhastedHpBar.value = currentHealth;
        yield return new WaitForSeconds(UI.Instance.SecondsUntilBarExosted);
        do
        {
            exhastedHpBar.value -= Time.deltaTime * UI.Instance.BarExhastedMultiplier;
            yield return new WaitForEndOfFrame();

        } while (exhastedHpBar.value > currentHealth);

    }
}
