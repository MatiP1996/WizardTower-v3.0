using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintUI : MonoBehaviour
{
    public TextMeshProUGUI hintText;
    // Start is called before the first frame update
    void Start()
    {
        hintText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            hintText.text = "Press F to Leave";

        }
    }
}
