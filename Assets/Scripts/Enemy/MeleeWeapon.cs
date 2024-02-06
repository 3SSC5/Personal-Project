
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

namespace PP
{
    public class MeleeWeapon : MonoBehaviour
    {
        private EnemyHealth eH;
        public int damage;

        private void Start()
        {

        }

        private void FixedUpdate()
        {
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            eH = collision.gameObject.GetComponent<EnemyHealth>();

            if (collision.gameObject.tag == "Enemy")
            {
                eH.TakeDamage(damage);
            }
        }
    }
}
