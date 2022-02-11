using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class corridorRotation : MonoBehaviour
{
    public GameObject rotationTarget;
    public float rotationSpeed = 0.05f;
    public float pullSpeed = 0.2f;
    private float currentRotation;
    private GameObject pullTarget;
    private GameObject pushTarget;
    private float targetRotation = 0.0f;
    private Transform targetTransform;
    public string pullableTag;
    private float pullSize = 9.0f;
    private float pushSize = 1.0f;
    private string rotationDirection = "none";
    private float xTarget;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        currentRotation = rotationTarget.transform.localRotation.eulerAngles.x;
        targetTransform = rotationTarget.GetComponent<Transform>();
        xTarget = targetTransform.localRotation.x;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // BLOCK PUSHING / PULLING

        if (Input.GetMouseButtonDown(0))
        {
            Ray theRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHitInfo;
            if (Physics.Raycast(theRay, out rayHitInfo))
            {
                if (rayHitInfo.collider.gameObject.tag == pullableTag)
                {
                    pullTarget = rayHitInfo.collider.gameObject;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray theRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHitInfo;
            if (Physics.Raycast(theRay, out rayHitInfo))
            {
                if (rayHitInfo.collider.gameObject.tag == pullableTag)
                {
                    pushTarget = rayHitInfo.collider.gameObject;
                }
            }
        }
        else if (pushTarget)
        {
            pullTarget = null;
        }

        if (pullTarget && pullTarget.transform.localScale.y < pullSize)
        {
            pullTarget.transform.localScale += new Vector3(0, pullSpeed, 0);
        }

        if (pushTarget && pushTarget.transform.localScale.y > pushSize)
        {
            pushTarget.transform.localScale -= new Vector3(0, pullSpeed, 0);
        } else if (pushTarget)
        {
            pushTarget = null;
        }

        // CORRIDOR ROTATION

        if (Input.GetKeyDown(KeyCode.Q))
        {
            xTarget += 90;
            rotationDirection = "left";
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            xTarget -= 90;
            rotationDirection = "right";
        }

        if (rotationDirection == "left")
        {
            targetTransform.localRotation = Quaternion.Lerp(targetTransform.localRotation, Quaternion.Euler(xTarget, targetTransform.localRotation.y, targetTransform.localRotation.z), rotationSpeed);
        } 
        
        if (rotationDirection == "right")
        {
            targetTransform.localRotation = Quaternion.Lerp(targetTransform.localRotation, Quaternion.Euler(xTarget, targetTransform.localRotation.y, targetTransform.localRotation.z), rotationSpeed);
        }
    }
}
