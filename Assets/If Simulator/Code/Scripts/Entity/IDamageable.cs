using UnityEngine;

public interface IDamageable 
{
    public void Damage(float damage, Color color);

    public void Heal(float heal, Color color);
}
