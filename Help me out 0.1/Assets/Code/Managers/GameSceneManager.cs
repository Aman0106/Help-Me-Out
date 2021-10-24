using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    static GameSceneManager _instance;
    GameObject player;

    [SerializeField] Transform respawnPoint;

    public static GameSceneManager Instance{
        get{
            if(_instance == null)
                _instance = FindObjectOfType<GameSceneManager>();
            return _instance;
        }   
    }

    void Start()
    {
        player = FindObjectOfType<Player_Controller>().gameObject;
    }

    public void LoadScene(int index){
        SceneManager.LoadScene(index);
    }

    public void LoadNextScene(){
        LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void Restart(){
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Respawn() {
        player.transform.position = respawnPoint.position;
    }

}
