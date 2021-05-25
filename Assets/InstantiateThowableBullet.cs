using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateThowableBullet :MonoBehaviour
{
   public ThrowBullet throwableBullet;

   void Start()
   {
      throwableBullet = Instantiate(throwableBullet, new Vector3(1, 0.5f, 1), Quaternion.identity);
   }
  

}