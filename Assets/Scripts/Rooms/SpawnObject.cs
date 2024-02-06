using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PP
{
    public class SpawnObject : MonoBehaviour
    {

        public GameObject[] Objs;

        // Start is called before the first frame update
        void Start()
        {
            int rand = Random.Range(0, Objs.Length);
            GameObject instance = (GameObject)Instantiate(Objs[rand], transform.position, Quaternion.identity);
            instance.transform.parent = transform;
        }
    }
}
