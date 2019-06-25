using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBar : MonoBehaviour
{
    public RectTransform mainIcon;
    public float timeStep;
    public float onestepAngle;
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
            if (Time.time - startTime >= timeStep)
            {
                Vector3 iconAngle = mainIcon.localEulerAngles;
                iconAngle.z += onestepAngle;
                mainIcon.localEulerAngles = iconAngle;
                startTime = Time.time;
            }     
    }
}
