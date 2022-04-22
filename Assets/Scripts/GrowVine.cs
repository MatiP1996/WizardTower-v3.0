using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrowVine : MonoBehaviour
{
    float growValue = -0.5f;

    private Material growMat;
    private float growStepProgress;

    private int steps = 6;

    float stepProportionComplete = 0;


    // -0.5 / -4.2
    // - 0.19 / -3.8
    // 0 / -2.2
    // 0.13 // -1.3
    // 0.33 / -0.2
    // 1 / 0

    //grow step sizes
    //1) 0.31
    //2) 0.19
    //3) 0.13
    //4) 0.2
    //5) 0.67

    // Start is called before the first frame update
    void Start()
    {
        growMat = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        growValue += Time.deltaTime / 5;
        

        growMat.SetFloat("Grow", growValue);


        if (growValue < -0.19f)
        {
            growStepProgress = growValue + 0.5f;
            stepProportionComplete = growStepProgress / 0.31f;

            growMat.SetFloat("Scale", Mathf.Lerp(-4.2f, -3.8f, stepProportionComplete));
        }
        else if (growValue < 0)
        {
            growStepProgress = growValue + 0.19f;
            stepProportionComplete = growStepProgress / 0.19f;

            growMat.SetFloat("Scale", Mathf.Lerp(-3.8f, -2.2f, stepProportionComplete));
            
        }
        else if (growValue < 0.13f)
        {
            growStepProgress = growValue;
            stepProportionComplete = growStepProgress / 0.13f;

            growMat.SetFloat("Scale", Mathf.Lerp(-2.2f, -1.3f, stepProportionComplete));
        }
        else if (growValue < 0.33f)
        {
            growStepProgress = growValue - 0.13f;
            stepProportionComplete = growStepProgress / 0.2f;

            growMat.SetFloat("Scale", Mathf.Lerp(-1.3f, -0.2f, stepProportionComplete));
        }
        else if (growValue < 1.0f)
        {
            growStepProgress = growValue - 0.33f;
            stepProportionComplete = growStepProgress / 0.67f;

            growMat.SetFloat("Scale", Mathf.Lerp(-0.2f, 0, stepProportionComplete));
        }
    }
}
