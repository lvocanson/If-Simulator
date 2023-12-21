using Ability;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab = null;
    [SerializeField, Min(0.01f)] private float _projectileSpeed = 10f;
    [SerializeField, Min(1)] private int _projectileCount = 10;
    [SerializeField, Range(0, 360)] private float _spreadAngle = 30f;
    [SerializeField, Range(0, 360)] private float _angleOffset = 0f;
    private float _firstProjectileOffset;
    private float _offsetBetweenProjectiles;

    private void Awake()
    {
        _firstProjectileOffset = _angleOffset - _spreadAngle / 2;
        _offsetBetweenProjectiles = _spreadAngle / (_projectileCount - 1);
    }

    public void Shoot()
    {
        for (int i = 0; i < _projectileCount; i++)
        {
            var projectile = Instantiate(_projectilePrefab, transform.position, transform.rotation).GetComponent<Projectile>();
            projectile.SetSpeed(_projectileSpeed);
            Vector2 direction = Quaternion.Euler(0, 0, _firstProjectileOffset + _offsetBetweenProjectiles * i) * transform.up;
            projectile.Initialize(gameObject.layer, direction);
        }
    }
}
