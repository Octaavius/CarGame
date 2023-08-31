using UnityEngine;
using UnityEngine.UI;
 
public class fpsCounter : MonoBehaviour
{
    public Text display_Text;
    
    private int frameCount;
    private float pollingTime = 0.5f;
    private float time;
 
    public void Update ()
    {
        time += Time.deltaTime;

        frameCount++;
        
        if(time >= pollingTime){
            int frameRate = Mathf.RoundToInt(frameCount / time);
            display_Text.text = frameRate.ToString();

            time -= pollingTime;
            frameCount = 0;
        }
    }
}
