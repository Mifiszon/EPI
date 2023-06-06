using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text score;
    // Update is called once per frame
    void Update()
    {
        score.text= Time.timeSinceLevelLoad.ToString("0");
    }
}
