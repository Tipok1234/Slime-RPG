using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Assets.Scripts.Manager;
using System;

namespace Assets.Scripts.UI
{
    public class AttackModelUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _damageText;

        private Sequence _sequence;
        public void SetupDamageText(float damage)
        {
            _damageText.text = "-" + damage.ToString();

            _sequence = DOTween.Sequence();
            _sequence.Append(_damageText.transform.DOLocalMoveY(50, 2f));
            _sequence.Append(_damageText.DOFade(0, 2f));

            _sequence.AppendCallback(() => { ResetDamageText(); });
        }

        public void ResetDamageText()
        {
            _damageText.DOFade(1, 0f);
            _damageText.transform.position = Vector3.zero;
            PoolManager.Instance.ReturnModelAttackToPool(this);
        }
    }
}
