using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class SlimeModel : MonoBehaviour
    {
        [SerializeField] private GameObject _slimePrefab;

        [SerializeField] private float _speedMove;
        [SerializeField] private int _damage;
        [SerializeField] private int _hp;
    }
}
