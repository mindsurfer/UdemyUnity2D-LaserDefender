using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
  public GameObject enemyB1Prefab;
  public float Width = 10f;
  public float Height = 5f;
  public float EnemyMoveSpeed = 10f;
  public float SpawnDelay = 0.5f;

  private Vector3 _leftMostVector, _rightMostVector;
  private Vector3 _direction;

  // Use this for initialization
  void Start ()
  {
    var distance = transform.position.z - Camera.main.transform.position.z;
    _leftMostVector = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distance));
    _rightMostVector = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, distance));

    _direction = Vector3.left;

    SpawnEnemiesUntilFull();
  }

  void OnDrawGizmos()
  {
    Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height));
  }
  
  // Update is called once per frame
  void Update () 
  {
    var newPosition = transform.position + (_direction * EnemyMoveSpeed * Time.deltaTime);
    var formationLeftEdge = newPosition.x - (0.5 * Width);
    var formationRightEdge = newPosition.x + (0.5 * Width);

    if (formationLeftEdge <= _leftMostVector.x)
      _direction = Vector3.right;
    else if (formationRightEdge >= _rightMostVector.x)
      _direction = Vector3.left;

    transform.position = newPosition;

    if (AllEnemiesDead())
    {
      SpawnEnemiesUntilFull();
    }
  }

  private bool AllEnemiesDead()
  {
    foreach (Transform childTransform in transform)
    {
      if (childTransform.childCount > 0)
        return false;
    }

    return true;
  }

  private void SpawnEnemiesUntilFull()
  {
    var nextPosition = NextFreePosition();
    if (nextPosition)
    {
      var gameObject = Instantiate(enemyB1Prefab, nextPosition.position, Quaternion.identity);
      // This will nest it correctly in the treeview while the game is running
      gameObject.transform.parent = nextPosition;
    }

    if (NextFreePosition())
      Invoke("SpawnEnemiesUntilFull", SpawnDelay);
  }

  private Transform NextFreePosition()
  {
    foreach (Transform childTransform in transform)
    {
      if (childTransform.childCount == 0)
        return childTransform;
    }

    return null;
  }

  private void SpawnEnemies()
  {
    foreach (Transform childTransform in transform)
    {
      var gameObject = Instantiate(enemyB1Prefab, childTransform.position, Quaternion.identity);
      // This will nest it correctly in the treeview while the game is running
      gameObject.transform.parent = childTransform;
      gameObject.tag = "Enemy";
    }
  }
}
