using System.Collections;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private AnimationCurve _explosionCurve;
    [SerializeField] private float _explosionDuration;
    [SerializeField] private DamageZoneEnter _damageZoneEnter;

    
    public void StartExplosion(int ownerLayer)
    {
        _damageZoneEnter.IgnoreLayer(ownerLayer);
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.fixedDeltaTime / _explosionDuration;
            float power = _explosionCurve.Evaluate(timer);
            gameObject.transform.localScale = Vector3.one * (power * _explosionRadius);
            yield return new WaitForFixedUpdate(); 
        }
        
        Destroy(gameObject);
    }
}
