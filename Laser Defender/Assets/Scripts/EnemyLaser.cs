using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour 
{
  public float Damage = 50f;

  public void HitPlayer()
  {
    Destroy(gameObject);
  }
}
