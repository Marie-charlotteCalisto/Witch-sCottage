using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectView : MonoBehaviour
{
    public GameObject BuildCam;
    public GameObject FPCam;
    public GameObject TPCam;

    // Start is called before the first frame update
    void Start()
    {
        BuildCam.SetActive(true);        
        FPCam.SetActive(false);
        TPCam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
            
    }


    public void onClickBuildMode()
    {
        BuildCam.SetActive(true);        
        FPCam.SetActive(false);
        TPCam.SetActive(false);
 
    }
    public void onClickFP()
    {
        FPCam.SetActive(true);
        BuildCam.SetActive(false);        
        TPCam.SetActive(false);
 
    }
    public void onClickTP()
    {
        FPCam.SetActive(false);
        BuildCam.SetActive(false);        
        TPCam.SetActive(true);
 
    }

}
