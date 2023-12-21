using System.Collections;
using UnityEngine;
using FiniteStateMachine;

public class Kamikaz_Attack : BaseState
{
    [Header("References")]
    [SerializeField] private Enemy _enemy;
    
    [Header("Data")]
    [SerializeField] private float _explosionDelay = 2f;
    [SerializeField] private GameObject _explosionPrefab;
    

    private ExplosionBehavior _explosionInstance;
    
    
    private void OnEnable()
    {
        _enemy.Agent.isStopped = true;
        _enemy.Agent.velocity = Vector3.zero;
        
        StartCoroutine(ExplodeHimself());
    }

    private IEnumerator ExplodeHimself()
    {
        yield return new WaitForSeconds(_explosionDelay);
        
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity).GetComponent<ExplosionBehavior>().StartExplosion(gameObject.layer);
        
        _enemy.Kill();
    }
    

    private void OnDisable()
    {
        _enemy.Agent.isStopped = false;
    }
}
