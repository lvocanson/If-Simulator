using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Marge_Script : MonoBehaviour
{
    [SerializeField] private float _speed;
    private GameObject _player;
    private Rigidbody2D _rigidbody2D;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _player = GameObject.FindWithTag("Player");
    
        Vector3 direction = _player.transform.position - transform.position;
        _rigidbody2D.velocity = new Vector2(direction.x, direction.y).normalized * _speed;
    }
    
    void Update()
    {
        
    }
}
