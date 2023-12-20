using UnityEngine;

public class DamageableEntityProxy : MonoBehaviour, IDamageable
{
    [SerializeField] private DamageableEntity _forwardTo;

    public void Damage(float damage) => _forwardTo.Damage(damage);
}
