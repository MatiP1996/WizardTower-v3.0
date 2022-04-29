using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrowVine : MonoBehaviour
{
    public float growValue = -0.0f; // this value is increased as the player waters the enchanted vine

    private float mainGrowValOffset = 0;
    private float lowGrowValOffset = -0.3f;
    private float midGrowValOffset = -0.5f;
    private float topGrowValOffset = -0.75f;

    private Material mainGrowMat;
    private Material lowGrowMat;
    private Material midGrowMat;
    private Material topGrowMat;

    private float growStepProgress;
    float stepProportionComplete = 0;
    
    private List<float> mainBranchGrowValList = new List<float>();
    private List<float> mainBranchScaleValList = new List<float>();

    private List<float> lowerBranchGrowValList = new List<float>();
    private List<float> lowerBranchScaleValList = new List<float>();

    private List<float> midBranchGrowValList = new List<float>();
    private List<float> midBranchScaleValList = new List<float>();

    private List<float> topBranchGrowValList = new List<float>();
    private List<float> topBranchScaleValList = new List<float>();

    private List<float> colliderGrowValList = new List<float>();
    private List<float> colliderScaleValList = new List<float>();
    private List<float> colliderPositionValList = new List<float>();
    
    private float growStepSize = 0;

    public GameObject mainBranch;
    public GameObject lowerBranch;
    public GameObject midBranch;
    public GameObject topBranch;

    public GameObject colliderObject;

    public WateringCan wateringCanScript;

    // Start is called before the first frame update
    void Start()
    {
        // assign the material variables
        mainGrowMat = mainBranch.GetComponent<Renderer>().material;
        lowGrowMat = lowerBranch.GetComponent<Renderer>().material;
        midGrowMat = midBranch.GetComponent<Renderer>().material;
        topGrowMat = topBranch.GetComponent<Renderer>().material;

        // MAIN BRANCH
        // set the grow step bound values
        mainBranchGrowValList.Add(0);      //1
        mainBranchGrowValList.Add(0.12f);  //2
        mainBranchGrowValList.Add(0.24f);  //3
        mainBranchGrowValList.Add(0.39f);  //4
        mainBranchGrowValList.Add(0.48f);  //5
        mainBranchGrowValList.Add(0.57f);  //6
        mainBranchGrowValList.Add(0.72f);  //7
        mainBranchGrowValList.Add(1f);     //8
        // set the scale step bound values
        mainBranchScaleValList.Add(-2.7f); //1
        mainBranchScaleValList.Add(-2.2f); //2
        mainBranchScaleValList.Add(-2.2f); //3
        mainBranchScaleValList.Add(-1.6f); //4
        mainBranchScaleValList.Add(-1.3f); //5
        mainBranchScaleValList.Add(-1.2f); //6
        mainBranchScaleValList.Add(-0.9f); //7
        mainBranchScaleValList.Add(0);     //8

        // LOWER BRANCH
        // set the grow step bound values
        lowerBranchGrowValList.Add(-0.15f); //1
        lowerBranchGrowValList.Add(-0.07f); //2
        lowerBranchGrowValList.Add(0.05f); //3
        lowerBranchGrowValList.Add(0.16f); //4
        lowerBranchGrowValList.Add(0.23f); //5
        lowerBranchGrowValList.Add(0.48f); //6
        lowerBranchGrowValList.Add(0.48f); //7
        lowerBranchGrowValList.Add(1f); //8
        // set the scale step bound values
        lowerBranchScaleValList.Add(-1.8f); //1
        lowerBranchScaleValList.Add(-1.6f); //2
        lowerBranchScaleValList.Add(-1.4f); //3
        lowerBranchScaleValList.Add(-0.3f); //4
        lowerBranchScaleValList.Add(-0.3f); //5
        lowerBranchScaleValList.Add(0); //6
        lowerBranchScaleValList.Add(0); //7
        lowerBranchScaleValList.Add(0); //8

        // MID BRANCH
        // set the grow step bound values
        midBranchGrowValList.Add(-0.16f); //1
        midBranchGrowValList.Add(-0.06f); //2
        midBranchGrowValList.Add(0.04f); //3
        midBranchGrowValList.Add(0.11f); //4
        midBranchGrowValList.Add(0.27f); //5
        midBranchGrowValList.Add(0.27f); //6
        midBranchGrowValList.Add(0.27f); //7
        midBranchGrowValList.Add(1); //8
        // set the scale step bound values
        midBranchScaleValList.Add(-1.4f); //1
        midBranchScaleValList.Add(-1.4f); //2
        midBranchScaleValList.Add(-0.8f); //3
        midBranchScaleValList.Add(-0.5f); //4
        midBranchScaleValList.Add(0); //5
        midBranchScaleValList.Add(0); //6
        midBranchScaleValList.Add(0); //7
        midBranchScaleValList.Add(0); //8

        // TOP BRANCH
        // set the grow step bound values
        topBranchGrowValList.Add(-0.14f); //1
        topBranchGrowValList.Add(-0.08f); //2
        topBranchGrowValList.Add(0); //3
        topBranchGrowValList.Add(0.1f); //4
        topBranchGrowValList.Add(0.16f); //5
        topBranchGrowValList.Add(1); //6
        topBranchGrowValList.Add(1); //7
        topBranchGrowValList.Add(1); //8
        // set the scale step bound values
        topBranchScaleValList.Add(-0.7f); //1
        topBranchScaleValList.Add(-0.7f); //2
        topBranchScaleValList.Add(-0.5f); //3
        topBranchScaleValList.Add(-0.3f); //4
        topBranchScaleValList.Add(-0.2f); //5
        topBranchScaleValList.Add(0); //6
        topBranchScaleValList.Add(0); //7
        topBranchScaleValList.Add(0); //8
        
        // COLLIDER
        // set collider grow value list
        colliderGrowValList.Add(0);
        colliderGrowValList.Add(0.2f);
        colliderGrowValList.Add(0.4f);
        colliderGrowValList.Add(0.6f);
        colliderGrowValList.Add(0.8f);
        colliderGrowValList.Add(1);
        // set collider Y axis scale list
        colliderScaleValList.Add(1.29f);
        colliderScaleValList.Add(3.28f);
        colliderScaleValList.Add(5.37f);
        colliderScaleValList.Add(7.33f);
        colliderScaleValList.Add(9.25f);
        colliderScaleValList.Add(10.69f);
        // set collider Y axis position list
        colliderPositionValList.Add(7.691f + 2);
        colliderPositionValList.Add(8.95f + 2);
        colliderPositionValList.Add(9.94f + 2);
        colliderPositionValList.Add(10.88f + 2);
        colliderPositionValList.Add(11.85f + 2);
        colliderPositionValList.Add(12.65f + 2);

        SetCollidierScaleAndPosition(1);

    }

    // Update is called once per frame
    void Update()
    {
        mainGrowMat.SetFloat("Grow", growValue + mainGrowValOffset); // set the shader "Grow" variable to the "growValue"
        lowGrowMat.SetFloat("Grow", growValue + lowGrowValOffset); // set the shader "Grow" variable to the "growValue"
        midGrowMat.SetFloat("Grow", growValue + midGrowValOffset); // set the shader "Grow" variable to the "growValue"
        topGrowMat.SetFloat("Grow", growValue + topGrowValOffset); // set the shader "Grow" variable to the "growValue"


        if (wateringCanScript.isWatering == true)
        {
            /// SETS THE SHADER "Scale" VALUE, DEPENDENT ON WHAT THE "growValue" CURRENTLY IS (to ensure the vine grows smoothly)
            if (growValue < mainBranchGrowValList[1])                                                                               // if grow value is in step 1
            {
                SetScaleValue(1, mainBranchGrowValList, mainBranchScaleValList, mainGrowMat, mainGrowValOffset); // set scale value for step 1
            }
            else if (growValue < mainBranchGrowValList[2])                                                                         // else if grow value is in step 1...
            {
                SetScaleValue(2, mainBranchGrowValList, mainBranchScaleValList, mainGrowMat, mainGrowValOffset); // set scale value for step 1...
            }
            else if (growValue < mainBranchGrowValList[3])
            {
                SetScaleValue(3, mainBranchGrowValList, mainBranchScaleValList, mainGrowMat, mainGrowValOffset);
            }
            else if (growValue < mainBranchGrowValList[4])
            {
                SetScaleValue(4, mainBranchGrowValList, mainBranchScaleValList, mainGrowMat, mainGrowValOffset);
            }
            else if (growValue < mainBranchGrowValList[5])
            {
                SetScaleValue(5, mainBranchGrowValList, mainBranchScaleValList, mainGrowMat, mainGrowValOffset);
            }
            else if (growValue < mainBranchGrowValList[6])
            {
                SetScaleValue(6, mainBranchGrowValList, mainBranchScaleValList, mainGrowMat, mainGrowValOffset);
            }
            else if (growValue < mainBranchGrowValList[7])
            {
                SetScaleValue(7, mainBranchGrowValList, mainBranchScaleValList, mainGrowMat, mainGrowValOffset);
            }
            else if (growValue < mainBranchGrowValList[8])
            {
                SetScaleValue(8, mainBranchGrowValList, mainBranchScaleValList, mainGrowMat, mainGrowValOffset);
            }

            /// SETS THE SHADER "Scale" VALUE, DEPENDENT ON WHAT THE "growValue" CURRENTLY IS (to ensure the vine grows smoothly)
            if (growValue + lowGrowValOffset < lowerBranchGrowValList[1])                                                          // if grow value is in step 1
            {
                SetScaleValue(1, lowerBranchGrowValList, lowerBranchScaleValList, lowGrowMat, lowGrowValOffset); // set scale value for step 1
            }
            else if (growValue + lowGrowValOffset < lowerBranchGrowValList[2])                                                     // else if grow value is in step 1...
            {
                SetScaleValue(2, lowerBranchGrowValList, lowerBranchScaleValList, lowGrowMat, lowGrowValOffset); // set scale value for step 1...
            }
            else if (growValue + lowGrowValOffset < lowerBranchGrowValList[3])
            {
                SetScaleValue(3, lowerBranchGrowValList, lowerBranchScaleValList, lowGrowMat, lowGrowValOffset);
            }
            else if (growValue + lowGrowValOffset < lowerBranchGrowValList[4])
            {
                SetScaleValue(4, lowerBranchGrowValList, lowerBranchScaleValList, lowGrowMat, lowGrowValOffset);
            }
            else if (growValue + lowGrowValOffset < lowerBranchGrowValList[5])
            {
                SetScaleValue(5, lowerBranchGrowValList, lowerBranchScaleValList, lowGrowMat, lowGrowValOffset);
            }
            else if (growValue + lowGrowValOffset < lowerBranchGrowValList[6])
            {
                SetScaleValue(6, lowerBranchGrowValList, lowerBranchScaleValList, lowGrowMat, lowGrowValOffset);
            }
            else if (growValue + lowGrowValOffset < lowerBranchGrowValList[7])
            {
                SetScaleValue(7, lowerBranchGrowValList, lowerBranchScaleValList, lowGrowMat, lowGrowValOffset);
            }
            else if (growValue + lowGrowValOffset < lowerBranchGrowValList[8])
            {
                SetScaleValue(8, lowerBranchGrowValList, lowerBranchScaleValList, lowGrowMat, lowGrowValOffset);
            }

            /// SETS THE SHADER "Scale" VALUE, DEPENDENT ON WHAT THE "growValue" CURRENTLY IS (to ensure the vine grows smoothly)
            if (growValue + midGrowValOffset < midBranchGrowValList[1])                                                        // if grow value is in step 1
            {
                SetScaleValue(1, midBranchGrowValList, midBranchScaleValList, midGrowMat, midGrowValOffset); // set scale value for step 1
            }
            else if (growValue + midGrowValOffset < midBranchGrowValList[2])                                                   // else if grow value is in step 1...
            {
                SetScaleValue(2, midBranchGrowValList, midBranchScaleValList, midGrowMat, midGrowValOffset); // set scale value for step 1...
            }
            else if (growValue + midGrowValOffset < midBranchGrowValList[3])
            {
                SetScaleValue(3, midBranchGrowValList, midBranchScaleValList, midGrowMat, midGrowValOffset);
            }
            else if (growValue + midGrowValOffset < midBranchGrowValList[4])
            {
                SetScaleValue(4, midBranchGrowValList, midBranchScaleValList, midGrowMat, midGrowValOffset);
            }
            else if (growValue + midGrowValOffset < midBranchGrowValList[5])
            {
                SetScaleValue(5, midBranchGrowValList, midBranchScaleValList, midGrowMat, midGrowValOffset);
            }
            else if (growValue + midGrowValOffset < midBranchGrowValList[6])
            {
                SetScaleValue(6, midBranchGrowValList, midBranchScaleValList, midGrowMat, midGrowValOffset);
            }
            else if (growValue + midGrowValOffset < midBranchGrowValList[7])
            {
                SetScaleValue(7, midBranchGrowValList, midBranchScaleValList, midGrowMat, midGrowValOffset);
            }
            else if (growValue + midGrowValOffset < midBranchGrowValList[8])
            {
                SetScaleValue(8, midBranchGrowValList, midBranchScaleValList, midGrowMat, midGrowValOffset);
            }

            /// SETS THE SHADER "Scale" VALUE, DEPENDENT ON WHAT THE "growValue" CURRENTLY IS (to ensure the vine grows smoothly)
            if (growValue + topGrowValOffset < topBranchGrowValList[1])                                                         // if grow value is in step 1
            {
                SetScaleValue(1, topBranchGrowValList, topBranchScaleValList, topGrowMat, topGrowValOffset);  // set scale value for step 1
            }
            else if (growValue + topGrowValOffset < topBranchGrowValList[2])                                                    // else if grow value is in step 1...
            {
                SetScaleValue(2, topBranchGrowValList, topBranchScaleValList, topGrowMat, topGrowValOffset);  // set scale value for step 1...
            }
            else if (growValue + topGrowValOffset < topBranchGrowValList[3])
            {
                SetScaleValue(3, topBranchGrowValList, topBranchScaleValList, topGrowMat, topGrowValOffset);
            }
            else if (growValue + topGrowValOffset < topBranchGrowValList[4])
            {
                SetScaleValue(4, topBranchGrowValList, topBranchScaleValList, topGrowMat, topGrowValOffset);
            }
            else if (growValue + topGrowValOffset < topBranchGrowValList[5])
            {
                SetScaleValue(5, topBranchGrowValList, topBranchScaleValList, topGrowMat, topGrowValOffset);
            }
            else if (growValue + topGrowValOffset < topBranchGrowValList[6])
            {
                SetScaleValue(6, topBranchGrowValList, topBranchScaleValList, topGrowMat, topGrowValOffset);
            }
            else if (growValue + topGrowValOffset < topBranchGrowValList[7])
            {
                SetScaleValue(7, topBranchGrowValList, topBranchScaleValList, topGrowMat, topGrowValOffset);
            }
            else if (growValue + topGrowValOffset < topBranchGrowValList[8])
            {
                SetScaleValue(8, topBranchGrowValList, topBranchScaleValList, topGrowMat, topGrowValOffset);
            }

            // SETS NEW SCALE AND POSITION FOR THE COLLIDER, BASED ON WHAT THE GROW VALUE CURRENTLY IS
            if (growValue < colliderGrowValList[1])         // if grow value is in step 1
            {
                SetCollidierScaleAndPosition(1);   // set scale and position values for step 1
            }
            else if (growValue < colliderGrowValList[2])   // else if grow value is in step 2...
            {
                SetCollidierScaleAndPosition(2);   // set scale and position values for step 2...
            }
            else if (growValue < colliderGrowValList[3])
            {
                SetCollidierScaleAndPosition(3);
            }
            else if (growValue < colliderGrowValList[4])
            {
                SetCollidierScaleAndPosition(4);
            }
            else if (growValue < colliderGrowValList[5])
            {
                SetCollidierScaleAndPosition(5);
            }
            else if (growValue < colliderGrowValList[6])
            {
                SetCollidierScaleAndPosition(6);
            }
        }
    }

    void SetScaleValue(int stepIndex, List<float> growValList, List<float> scaleValList, Material growMat, float offset)
    {
        growStepSize = growValList[stepIndex] - growValList[stepIndex - 1];  // get the current step size
        growStepProgress = growValue + offset - growValList[stepIndex - 1];  // get the grow progress for the current grow step
        stepProportionComplete = growStepProgress / growStepSize;            // get the grow step proportion complete

        growMat.SetFloat("Scale", Mathf.Lerp(scaleValList[stepIndex - 1], scaleValList[stepIndex], stepProportionComplete));  // set scale value as interpolated by the grow step proportion complete val
    }


    void SetCollidierScaleAndPosition(int stepIndex)
    {
        float stepSize = 0.2f;                                             // step size is constant for collider
        growStepProgress = growValue - colliderGrowValList[stepIndex - 1]; // get the grow progress for the current grow step
        stepProportionComplete = growStepProgress / stepSize;              // get the grow step proportion complete

        Vector3 newPosition = colliderObject.transform.position;                                                                              // set newPosition to the current collider position
        newPosition.y = Mathf.Lerp(colliderPositionValList[stepIndex - 1], colliderPositionValList[stepIndex], stepProportionComplete); // set the newPosition Y axis, as an interpolation between the bounds of the current position step
        colliderObject.transform.position = newPosition;                                                                                      // set the collider position to newPosition

        Vector3 newScale = colliderObject.transform.localScale;                                                                       // set newScale to the current collider scale
        newScale.y = Mathf.Lerp(colliderScaleValList[stepIndex - 1], colliderScaleValList[stepIndex], stepProportionComplete); // set the newScale Y axis, as an interpolation between the bounds of the current scale step
        colliderObject.transform.localScale = newScale;                                                                              // set the collider scale to newScale
    }
}
