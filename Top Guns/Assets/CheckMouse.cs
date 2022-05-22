using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMouse : MonoBehaviour
{
    public bool onDeadZone;

    // Update is called once per frame
    void Update()
    {
       
    }


    private void OnMouseOver()
    {
        Debug.Log("cc");
        onDeadZone = true;
    }

    
}
