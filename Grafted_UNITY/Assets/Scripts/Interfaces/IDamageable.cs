using UnityEngine;

// Interface for every damageable object in the game
public interface IDamageable
{
    public void takeDamage(float damage);
    public void death();
}
