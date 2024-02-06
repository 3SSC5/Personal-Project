using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PP
{
    public class SpawnRooms : MonoBehaviour
    {
        public LayerMask Room;
        public LevelGeneration lG;

        // Update is called once per frame
        void Update()
        {
            Collider2D rD = Physics2D.OverlapCircle(transform.position, 1, Room);
            if (rD == null && lG.sG == true)
            {
                int rand = Random.Range(0, lG.R.Length);
                Instantiate(lG.R[rand], transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}