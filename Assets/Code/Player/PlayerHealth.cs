using UnityEngine;

public class PlayerHealth : Health
{
   
    public void SetStartingHealth(int startingHeath)
    {
        this.healthDetails.StartingHeath = startingHeath;
        currentHealth = startingHeath;
        print(UI.Instance);
        UI.Instance.HpBar.value = currentHealth;
    }

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
        UI.Instance.HpBar.value = currentHealth;
    }
}
