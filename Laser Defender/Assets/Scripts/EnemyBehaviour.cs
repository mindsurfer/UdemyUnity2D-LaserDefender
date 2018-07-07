using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour 
{
  public float Hitpoints = 200f;
  public float EnemyLaserSpeed = 5f;
  public float EnemyFireRate = 0.5f;
  public int ScoreValue = 25;

  public AudioClip ShootLaserSfx;
  public AudioClip DeathSfx;
  public GameObject EnemyLaserPrefab;

  private ScoreManager _scoreManager;

  void Start()
  {
    _scoreManager = GameObject.Find("TextScore").GetComponent<ScoreManager>();
  }

  void OnTriggerEnter2D(Collider2D collider)
  {
    var laser = collider.gameObject.GetComponent<PlayerLaser>();
    if (laser != null)
    {
      laser.HitEnemy();
      Hitpoints -= laser.GetDamage();
      if (Hitpoints <= 0)
        Die();
    }
  }

  private void Die()
  {
    AudioSource.PlayClipAtPoint(DeathSfx, transform.position);
    Destroy(gameObject);
    _scoreManager.UpdateScore(ScoreValue);
  }

  void Update()
  {
    var fireLaserProbability = Time.deltaTime * EnemyFireRate;
    if (Random.value < fireLaserProbability)
    {
      FireEnemyLaser();
    }
  }

  void FireEnemyLaser()
  {
    var bottomLeft = transform.TransformPoint(0, 0, 0);
    var topRight = transform.TransformPoint(1, 1, 0);
    var width = topRight.x - bottomLeft.x;

    var enemyLaserSpawnPos = new Vector3(transform.position.x + (width / 2), transform.position.y);
    var enemyLaser = Instantiate(EnemyLaserPrefab, enemyLaserSpawnPos, Quaternion.identity);
    //enemyLaser.transform.parent = transform;
    enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -EnemyLaserSpeed);
    AudioSource.PlayClipAtPoint(ShootLaserSfx, transform.position);
  }
}
