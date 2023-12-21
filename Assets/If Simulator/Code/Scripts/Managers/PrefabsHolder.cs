using UnityEngine;

public class PrefabsHolder : MonoBehaviour
{
    [SerializeField] private Transform _damagePopupPrefab;
    [SerializeField] private Transform _totalDamagePopupPrefab;

    public Transform DamagePopupPrefab => _damagePopupPrefab;
    public Transform TotalDamagePopupPrefab => _totalDamagePopupPrefab;

}
