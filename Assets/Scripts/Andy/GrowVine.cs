using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrowVine : MonoBehaviour
{
    public float growValue = -0.5f; // this value is increased as the player waters the plant
    private Material growMat;
    private float growStepProgress;
    float stepProportionComplete = 0;
    
    private List<float> growValList = new List<float>();
    private List<float> scaleValList = new List<float>();
    private float growStepSize = 0;
    


    // Start is called before the first frame update
    void Start()
    {
        growMat = gameObject.GetComponent<Renderer>().material;

        // the grow step bound values
        growValList.Add(0);
        growValList.Add(0.12f);
        growValList.Add(0.24f);
        growValList.Add(0.39f);
        growValList.Add(0.48f);
        growValList.Add(0.57f);
        growValList.Add(0.72f);
        growValList.Add(1f);

        // the scale step bound values
        scaleValList.Add(-3.5f);
        scaleValList.Add(-2.9f);
        scaleValList.Add(-2.7f);
        scaleValList.Add(-2.5f);
        scaleValList.Add(-2.1f);
        scaleValList.Add(-1.9f);
        scaleValList.Add(-1.1f);
        scaleValList.Add(0);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift)) // for testing
        {
            growValue += Time.deltaTime / 20;
        }

        growMat.SetFloat("Grow", growValue); // set the shader "Grow" variable to the "growValue"

        /// SETS THE SHADER "Scale" VALUE, DEPENDENT ON WHAT THE "growValue" CURRENTLY IS (to ensure the vine grows smoothly)
        if (growValue < growValList[1]) // if grow value is in step 1
        {
            SetScaleValue(1); // set scale value for step 1
        }
        else if (growValue < growValList[2])// if grow value is in step 1...
        {
            SetScaleValue(2); // set scale value for step 1...
        }
        else if (growValue < growValList[3])
        {
            SetScaleValue(3);
        }
        else if (growValue < growValList[4])
        {
            SetScaleValue(4);
        }
        else if (growValue < growValList[5])
        {
            SetScaleValue(5);
        }
        else if (growValue < growValList[6])
        {
            SetScaleValue(6);
        }
        else if (growValue < growValList[7])
        {
            SetScaleValue(7);
        }
        else if (growValue < growValList[8])
        {
            SetScaleValue(8);
        }
    }

    void SetScaleValue(int stepIndex)
    {
        growStepSize = growValList[stepIndex] - growValList[stepIndex - 1]; // get the current step size
        growStepProgress = growValue - growValList[stepIndex - 1];  // get the grow progress in the current step
        stepProportionComplete = growStepProgress / growStepSize;  // get the grow step proportion complete

        growMat.SetFloat("Scale", Mathf.Lerp(scaleValList[stepIndex - 1], scaleValList[stepIndex], stepProportionComplete));  // set scale value as interpolated by the grow step proportion complete val
    }
}
