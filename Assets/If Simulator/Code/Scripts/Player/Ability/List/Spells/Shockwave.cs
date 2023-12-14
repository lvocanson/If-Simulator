using UnityEngine;

namespace Ability.List.Spells
{
    public class Shockwave : AbilityActive
    {
        [SerializeField] private GameObject _shockwavePrefab;
        [SerializeField] private Transform _shockwaveSpawnPoint;
        
        [SerializeField] private float _speed = 10f;
        
        private GameObject _shockwaveInstance;

        // private void OnTriggerEnter(Collider other)
        // {
        //     if (other.gameObject.layer != ENEMY_LAYER) return;
        //     other.transform.GetComponent<EnemyBehaviour>().SetState(EnemyBehaviour.EnemyState.FROZEN);
        // }
        
        protected override void OnEffectStart()
        {
            Debug.Log("Shockwave: OnEffectStart");
            _shockwaveInstance = Instantiate(_shockwavePrefab, _shockwaveSpawnPoint.position, Quaternion.identity);
        }

        protected override void OnEffectUpdate()
        {
            _shockwaveInstance.transform.localScale += Vector3.one * (_speed * Time.deltaTime); 
        }

        protected override void OnEffectEnd()
        {
            Destroy(_shockwaveInstance);
        }
    }
}