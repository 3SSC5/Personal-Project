using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PP
{
    public class LevelGeneration : MonoBehaviour
    {
        public Transform[] SP;
        public GameObject[] R;

        private int direction;

        private float TbR;
        public float sTbR = 0.25f;

        public LayerMask Room;

        public float mA;
        public float miX;
        public float maX;
        public float miY;

        private int dC;

        public bool sG;

        private void Update()
        {
            if (TbR <= 0 && sG == false)
            {
                Move();
                TbR = sTbR;
            }
            else
            {
                TbR -= Time.deltaTime;
            }
        }

        private void Start()
        {
            int rSP = Random.Range(0, SP.Length);
            transform.position = SP[rSP].position;
            Instantiate(R[0], transform.position, Quaternion.identity);

            direction = Random.Range(1, 6);
        }

        public void Move()
        {
            if (direction == 1 || direction == 2)
            {
                if(transform.position.x < maX)
                {
                    dC = 0;
                    Vector2 pos = new Vector2(transform.position.x + mA, transform.position.y);
                    transform.position = pos;

                    int randRoom = Random.Range(1, 4);
                    Instantiate(R[randRoom], transform.position, Quaternion.identity);

                    direction = Random.Range(1, 6);
                    if (direction == 3)
                    {
                        direction = 2;
                    }
                    else if (direction == 4)
                    {
                        direction = 5;
                    }
                }
                else
                {
                    direction = 5;
                }
            }
            else if (direction == 3 || direction == 4)
            {
                if (transform.position.x > miX)
                {
                    dC = 0;
                    Vector2 pos = new Vector2(transform.position.x - mA, transform.position.y);
                    transform.position = pos;

                    int randRoom = Random.Range(1, 4);
                    Instantiate(R[randRoom], transform.position, Quaternion.identity);

                    direction = Random.Range(3, 6);
                }
                else
                {
                    direction = 5;
                }
            }
            else if (direction == 5)
            {
                dC++;
                if(transform.position.y > miY)
                {
                    Collider2D RoomD = Physics2D.OverlapCircle(transform.position, 1, Room);

                    if (RoomD.GetComponent<RoomType>().type != 1 && RoomD.GetComponent<RoomType>().type != 3)
                    {
                        if(dC >= 2)
                        {
                            RoomD.GetComponent<RoomType>().DestroyObj();
                            Instantiate(R[3], transform.position, Quaternion.identity);
                        }
                        else
                        {
                            RoomD.GetComponent<RoomType>().DestroyObj();

                            int randRoomDownOpening = Random.Range(1, 4);

                            if (randRoomDownOpening == 2)
                            {
                                randRoomDownOpening = 1;
                            }
                            Instantiate(R[randRoomDownOpening], transform.position, Quaternion.identity);
                        }
                    }

                    Vector2 pos = new Vector2(transform.position.x, transform.position.y - mA);
                    transform.position = pos;

                    direction = Random.Range(1,6);

                    int randRoom = Random.Range(2, 4);
                    Instantiate(R[randRoom], transform.position, Quaternion.identity);
                }
                else
                {
                    sG = true;
                }
            }
        }
    }
}
