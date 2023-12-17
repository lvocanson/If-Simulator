using System.Collections;
using UnityEngine;
using FiniteStateMachine;
using SAP2D;

public class Kamikaz_Attack : BaseState
{
    [Header("References")]
    [SerializeField] private Enemy _enemy;
    
    [Header("Data")]
    [SerializeField] private float _explosionDelay = 2f;
    [SerializeField] private GameObject _explosionPrefab;
    
    [Header("State Machine")]
    [SerializeField] private SAP2DAgent _SAPAgent;
    

    private ExplosionBehavior _explosionInstance;
    
    
    private void OnEnable()
    {
        _SAPAgent.CanMove = false;
        _SAPAgent.CanSearch = false;
        
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
        _SAPAgent.CanMove = true;
        _SAPAgent.CanSearch = true;
    }
}
