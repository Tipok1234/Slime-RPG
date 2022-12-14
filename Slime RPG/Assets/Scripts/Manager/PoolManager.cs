using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Model;

namespace Assets.Scripts.Manager
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance => instance;

        [SerializeField] private BulletModel _bulletPrefab;
        [SerializeField] private Transform _bulletParent;

        private List<BulletModel> _bulletModels;

        private int _bulletCount = 10;

        private static PoolManager instance;
        private void Awake()
        {
            if (instance == null)
                instance = this;

            _bulletModels = new List<BulletModel>();

            for (int i = 0; i < _bulletCount; i++)
            {
                _bulletModels.Add(Instantiate(_bulletPrefab, _bulletParent));
            }
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
