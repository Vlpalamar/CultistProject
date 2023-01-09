using UnityEngine;

public class PlayerHealth : Health
{
   
    public void SetStartingHealth(int startingHeath)
    {
        this.healthDetails.StartingHeath = startingHeath;
        currentHealth = startingHeath;
    }
}
