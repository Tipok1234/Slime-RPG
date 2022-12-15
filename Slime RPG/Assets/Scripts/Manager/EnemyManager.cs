using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Model;
using System;

namespace Assets.Scripts.Manager
{
    public class EnemyManager : MonoBehaviour
    {
        public event Action DeadUnitAction;

        [SerializeField] private RangeEnemy _rangeEnemy;
        [SerializeField] private MeleeEnemyModel _melleEnemy;

        [SerializeField] private Transform[] _enemyPosition;

        private int _countEnemy = 50;

        private void Start()
        {
            RangeEnemy.DeadAction += OnDeadUnit;
            MeleeEnemyModel.DeadAction += OnDeadUnit;

            StartCoroutine(WaitSpawnEnemys());
        }

        private void OnDestroy()
        {
            RangeEnemy.DeadAction -= OnDeadUnit;
            MeleeEnemyModel.DeadAction -= OnDeadUnit;
        }

        private IEnumerator WaitSpawnEnemys()
        {
            yield return new WaitForSeconds(2f);

            for (int i = 0; i < _countEnemy; i++)
            {
                var randomPos = UnityEngine.Random.Range(0, _enemyPosition.Length);
                var randomNumber = UnityEngine.Random.Range(0, 2);

                if (randomNumber == 0)
                    Instantiate(_melleEnemy, _enemyPosition[randomPos]);
                else
                    Instantiate(_rangeEnemy, _enemyPosition[randomPos]);


                yield return new WaitForSeconds(6f);
            }
        }


        private void OnDeadUnit()
        {
            DeadUnitAction.Invoke();
        }
    }
}
