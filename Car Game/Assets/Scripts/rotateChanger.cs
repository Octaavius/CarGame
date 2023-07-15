using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateChanger : MonoBehaviour
{
    public void changeReverse(){
        transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    public void changeDrive(){
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
