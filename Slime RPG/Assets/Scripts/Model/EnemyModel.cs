using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.UI;

namespace Assets.Scripts.Model
{
    public abstract class EnemyModel : MonoBehaviour
    {
        [SerializeField] protected LayerMask _enemyLayer;
        [SerializeField] protected HealthBar _slider;

        [SerializeField] protected int _damage;
        [SerializeField] protected int _hp;
        [SerializeField] protected float _speedMove;
        [SerializeField] protected float _reloadTime;
        [SerializeField] protected float _sphereRadius;
    }
}
