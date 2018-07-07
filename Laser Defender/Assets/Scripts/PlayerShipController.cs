using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : MonoBehaviour 
{
  public float ShipMoveSpeed = 7.5f;
  public float Padding = 1f;
  public float PlayerLaserSpeed = 10f;
  public float PlayerFiringRate = 0.2f;
  public float HitPoints = 500f;

  public AudioClip ShootLaserSfx;
  public GameObject PlayerLaserPrefab;

  private bool _isLeftArrowDown, _isRightArrowDown;
  private Vector3 _leftMost, _rightMost;

  // Use this for initialization
  void Start ()
  {
    var distance = transform.position.z - Camera.main.transform.position.z;
    // The coords taken into ViewportToWorldPoint are relative, 0 for left, 0.5 for middle, 1 for right
    _leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
    _rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
  }
  
  // Update is called once per frame
  void Update () 
  {
    _isLeftArrowDown = Input.GetKey(KeyCode.LeftArrow);
    _isRightArrowDown = Input.GetKey(KeyCode.RightArrow);

    if (_isLeftArrowDown)
      MoveShipLeft();
    if (_isRightArrowDown)
      MoveShipRight();

    if (Input.GetKeyDown(KeyCode.Space))
      InvokeRepeating("FireLaser", 0.00001f, PlayerFiringRate);
    if (Input.GetKeyUp(KeyCode.Space))
      CancelInvoke("FireLaser");
  }

  void OnTriggerEnter2D(Collider2D collider)
  {
    var enemyLaser = collider.gameObject.GetComponent<EnemyLaser>();
    if (enemyLaser != null)
    {
      enemyLaser.HitPlayer();
      HitPoints -= enemyLaser.Damage;
      if (HitPoints <= 0)
      {
        Die();
      }
    }
  }

  private void Die()
  {
    Destroy(gameObject);
    var levelManager = GameObject.FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
    levelManager.LoadLevel("Win");
  }

  private void FireLaser()
  {
    var bottomLeftCorner = transform.TransformPoint(0, 0, 0);
    var topRightCorner = transform.TransformPoint(1, 1, 0);
    var width = topRightCorner.x - bottomLeftCorner.x;
    var height = topRightCorner.y - bottomLeftCorner.y;

    var laserSpawnPosition = new Vector3(transform.position.x + (width/2), transform.position.y + height);
    var laser = Instantiate(PlayerLaserPrefab, laserSpawnPosition, Quaternion.identity);
    //laser.transform.parent = transform;
    laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, PlayerLaserSpeed, 0);
    AudioSource.PlayClipAtPoint(ShootLaserSfx, transform.position);
  }

  #region Move Ship

  private void MoveShip(Vector3 direction)
  {
    var newPosition = transform.position + (direction * ShipMoveSpeed * Time.deltaTime);
    newPosition.x = Mathf.Clamp(newPosition.x, _leftMost.x, _rightMost.x - Padding);
    transform.position = newPosition;
  }

  private void MoveShipRight()
  {
    MoveShip(Vector3.right);
  }

  private void MoveShipLeft()
  {
    MoveShip(Vector3.left);
  } 

  #endregion
}
