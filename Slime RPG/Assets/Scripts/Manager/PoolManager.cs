using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Model;
using Assets.Scripts.UI;

namespace Assets.Scripts.Manager
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance => instance;

        [SerializeField] private BulletModel _bulletPrefab;
        [SerializeField] private Transform _bulletParent;

        [SerializeField] private Transform _modelTransform;
        [SerializeField] private AttackModelUI _attackModelUIPrefab;

        private List<BulletModel> _bulletModels;
        private List<AttackModelUI> _attackModelsUI;

        private int _bulletCount = 10;
        private int _attackPrefabCount = 10;

        private static PoolManager instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;

            _bulletModels = new List<BulletModel>();
            _attackModelsUI = new List<AttackModelUI>();

            for (int i = 0; i < _bulletCount; i++)
            {
                _bulletModels.Add(Instantiate(_bulletPrefab, _bulletParent));
            }

            for (int i = 0; i < _attackPrefabCount; i++)
            {
                _attackModelsUI.Add(Instantiate(_attackModelUIPrefab, _modelTransform));
            }
        }

        public AttackModelUI GetAttackModelUI(Transform attackModelPos)
        {
            for (int i = 0; i < _attackModelsUI.Count; i++)
            {
                if(_attackModelsUI[i].gameObject.activeSelf == false)
                {
                    _attackModelsUI[i].transform.position = attackModelPos.position;
                    _attackModelsUI[i].gameObject.SetActive(true);

                    return _attackModelsUI[i];
                }
            }

            var newModel = Instantiate(_attackModelUIPrefab, _modelTransform);
            newModel.transform.position = attackModelPos.position;
            newModel.gameObject.SetActive(true);
            _attackModelsUI.Add(newModel);

            return _attackModelsUI[_attackModelsUI.Count - 1];
        }

        public void ReturnModelAttackToPool(AttackModelUI attackModelUI)
        {
            attackModelUI.gameObject.SetActive(false);
            attackModelUI.transform.position = _modelTransform.position;
        }

        public BulletModel GetBullet(Transform bulletPos)
        {
            for (int i = 0; i < _bulletModels.Count; i++)
            {
                if(_bulletModels[i].gameObject.activeSelf == false)
                {
                    _bulletModels[i].transform.position = bulletPos.position;
                    _bulletModels[i].gameObject.SetActive(true);
                    return _bulletModels[i];
                }
            }

            var newBullet = Instantiate(_bulletPrefab, _bulletParent);
            newBullet.transform.position = bulletPos.position;
            newBullet.gameObject.SetActive(true);
            _bulletModels.Add(newBullet);

            return _bulletModels[_bulletModels.Count - 1];
        }
    }
}
