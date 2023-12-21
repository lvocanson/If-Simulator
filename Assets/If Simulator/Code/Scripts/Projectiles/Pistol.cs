using Ability;
using UnityEngine;
using Utility;

public class Pistol : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField, Min(0.01f)] private float _projectileSpeed = 10f;
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject ProjectileSpawnPoint;

    private void Awake()
    {
    }

    public void Shoot()
    {
        if (_target != null)
        {
            Vector3 direction = _target.transform.position - ProjectileSpawnPoint.transform.position;
            Projectile bullet = Instantiate(_bulletPrefab, ProjectileSpawnPoint.transform.position, Quaternion.identity)
                .GetComponent<Projectile>();

            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            bullet.Initialize(gameObject.layer, direction.normalized * _projectileSpeed);
        }
        else
        {
            Debug.LogWarning("No target defined for the Pistol");
        }
    }
}
