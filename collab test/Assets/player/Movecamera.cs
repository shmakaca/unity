using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movecamera : MonoBehaviour
{
    public Transform cameraPoistion;

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPoistion.position;
    }
}
