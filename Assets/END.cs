using PP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PP
{
    public class END : MonoBehaviour
    {

        public GameManager gm;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                gm.GameOver();
            }
        }
    }
}