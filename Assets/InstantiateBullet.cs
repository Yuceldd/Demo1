using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class InstantiateBullet : MonoBehaviour
{
    public GameObject Bullet;


    public void Start()
    {
        InstantiateBullets();

    }

    public void InstantiateBullets()
    {
        int y = Random.Range(-7, 20);
        for (int i = -12; i < y; i++)
        {
           int x = Random.Range(-7, 20);


        Instantiate(Bullet, new Vector3(Random.Range(5f, -5f), 1, x), Quaternion.identity);
    }
}
    
}
