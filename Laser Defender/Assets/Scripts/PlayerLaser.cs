using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour 
{
  public float Damage = 100f;

  public void HitEnemy()
  {
    Destroy(gameObject);
  }

  public float GetDamage()
  {
    return Damage;
  }

  //void OnTriggerEnter2D(Collider2D collider)
  //{
  //  if (collider.gameObject.tag == "Enemy")
  //    Debug.Log("Enemy hit!!");
  //}
}
