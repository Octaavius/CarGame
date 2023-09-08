using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateColorScript : MonoBehaviour
{
    private GameManager gm;
    public Renderer[] renderer;

    void Awake(){
        gm = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        for(int n = 0; n < renderer.Length; n++){
            renderer[n].material.color = gm.carsColor[gm.lastCarId];
        }
    }
}
