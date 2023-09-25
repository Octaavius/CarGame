using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMode : MonoBehaviour
{
    public GameObject stuf;
    
    // Start is called before the first frame update
    void Start()
    {
        GameMode gm = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>().gameMode;
        if(gm == GameMode.TimeAtack){
            stuf.SetActive(true);
        }   
    }
}
