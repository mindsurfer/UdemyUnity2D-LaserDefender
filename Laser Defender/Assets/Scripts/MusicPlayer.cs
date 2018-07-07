using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour 
{
  static MusicPlayer _instance = null;

  public AudioClip StartClip;
  public AudioClip GameClip;
  public AudioClip WinClip;

  private AudioSource _music;

  void Awake()
  {
    if (_instance != null && _instance != this)
    {
      Destroy(gameObject);
      print("Duplicate music player self-destructing!");
    }
    else
    {
      _instance = this;
      GameObject.DontDestroyOnLoad(gameObject);
      _music = GetComponent<AudioSource>();
    }
  }

  // Use this for initialization
  void Start () 
  {
    
  } 

  private void OnEnable()
  {
    SceneManager.sceneLoaded += SceneManager_sceneLoaded;
  }

  private void OnDisable()
  {
    SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
  }

  private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
  {
    _music.Stop();

    if (scene.buildIndex == 0)
    {
      _music.clip = StartClip;
      _music.volume = 0.25f;
    }
    else if (scene.buildIndex == 1)
    {
      _music.clip = GameClip;
      _music.volume = 0.1f;
    }
    else if (scene.buildIndex == 2)
    {
      _music.clip = WinClip;
      _music.volume = 0.25f;
    }

    _music.loop = true;
    _music.Play();
  }

  //void OnLevelWasLoaded(int level)
  //{
  //  Debug.Log("LEvel loaded");

  //  _music.Stop();
  //  if (level == 0)
  //    _music.clip = StartClip;
  //  else if (level == 1)
  //    _music.clip = GameClip;
  //  else if (level == 2)
  //    _music.clip = WinClip;
  //  _music.loop = true;
  //  _music.Play();
  //}
  
  // Update is called once per frame
  void Update () 
  {
    
  }
}
