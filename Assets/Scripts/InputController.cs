using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public bool WantsToShoot = false;
    public bool ModeSwitch = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WantsToShoot = Input.GetButton("Fire1");
        ModeSwitch = Input.GetButton("G");
    }
}
