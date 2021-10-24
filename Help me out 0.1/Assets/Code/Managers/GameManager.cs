using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    static GameManager _instance;
    public static GameManager Instance {
        get{
            if(_instance == null){
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    [SerializeField] int lives;
    [SerializeField] Text plLivesText;
    [SerializeField] Text dlLivesText;
    List<SpriteRenderer> dragAbles = new List<SpriteRenderer>();

    int helps;

    private void Start() {
        plLivesText.text = "Lives = "+ lives.ToString();
        dlLivesText.text = "Lives = " + lives.ToString();

        DragAble[] objs = FindObjectsOfType<DragAble>();

        foreach(var obj in objs){
            dragAbles.Add(obj.GetComponent<SpriteRenderer>());
        }
    }

    public void Respawn(){
        if(--lives <= 0)
            GameSceneManager.Instance.Restart();

        plLivesText.text = "Lives = " + lives.ToString();
        dlLivesText.text = "Lives = " + lives.ToString();
    }
    
    public void SetTranseperency(float val){
        foreach(var dragAble in dragAbles){
            Color temp = dragAble.color;
            temp.a = val;
            dragAble.color = temp;//new Vector4(dragAble.color.r, dragAble.color.g, dragAble.color.b, dragAble.color.a);
        }
    }

    public int GetCurLives(){
        return lives;
    }

    public void Helped(){
        helps++;
    }

    public int GetNoOfHelps(){
        return helps;
    }

}