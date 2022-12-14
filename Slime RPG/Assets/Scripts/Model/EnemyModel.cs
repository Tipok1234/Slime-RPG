using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.UI;

namespace Assets.Scripts.Model
{
    public abstract class EnemyModel : MonoBehaviour
    {
        public float Damage => _damage;
        public float HP => _hp;


        [SerializeField] protected LayerMask _enemyLayer;

        [SerializeField] protected float _hp;
        [SerializeField] protected float _damage;
        [SerializeField] protected float _speedMove;
        [SerializeField] protected float _reloadTime;
        [SerializeField] protected float _sphereRadius;
        public virtual void UpdateHP(float damage)
        {
            
        }

        public virtual void TakeDamage(float damage)
        {
            _hp -= damage;
        }
        //public virtual void TakeDamage()
        //{
           
        //}
    }
}
