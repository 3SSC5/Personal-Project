using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PP
{
    public class RoomType : MonoBehaviour
    {
        public int type;
        // Start is called before the first frame update
        public void DestroyObj()
        {
            Destroy(gameObject);
        }
    }
}
