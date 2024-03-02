using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpineRotator : MonoBehaviour
{
    Animator anim;
    
    Camera mainCam;

    [SerializeField] float weight = 1f;
    [SerializeField] float bodyWeight = 1f;
    [SerializeField] float headWeight = 1f;
    [SerializeField] float eyesWeight = 1f;
    [SerializeField] float clampWeight = 1f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        mainCam = Camera.main;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        Debug.Log("X");
        anim.SetLookAtWeight(weight, bodyWeight, headWeight, eyesWeight, clampWeight);
        Ray lookAtRay = new Ray(transform.position, mainCam.transform.forward);
        

        anim.SetLookAtPosition(lookAtRay.GetPoint(25));
    }
}
