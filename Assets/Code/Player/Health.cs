using UnityEngine;

public class Health : MonoBehaviour
{
    private int startingHeath;
    private int currentHealth;
    private int maximumHealth;
    public void SetStartingHealth(int startingHeath)
    {
        this.startingHeath = startingHeath;
        currentHealth = startingHeath;
    }
}
