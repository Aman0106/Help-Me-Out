using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    enum Type {Restart, NextLevel, Respawn}
    [SerializeField]Type type;

    void Load(){
        switch (type)
        {
            case Type.Restart:{
                GameSceneManager.Instance.Restart();
                break;
            }
            case Type.NextLevel:{
                GameSceneManager.Instance.LoadNextScene();
                break;
            }
            case Type.Respawn:{
                GameManager.Instance.Respawn();
                GameSceneManager.Instance.Respawn();
                break;
            }
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            Load();
        }
    }
 
}
