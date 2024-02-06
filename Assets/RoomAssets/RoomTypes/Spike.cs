using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PP
{
    public class Spike : MonoBehaviour
    {
        private PlayerHealth pH;
        public int damage = 2;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            pH = collision.gameObject.GetComponent<PlayerHealth>();

            if (collision.gameObject.tag == "Player" && collision.gameObject.tag != "Player")
            {
                pH.TakeDamage(damage);
            }
        }
    }
}
