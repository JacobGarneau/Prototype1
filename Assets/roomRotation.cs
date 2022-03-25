using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomRotation : MonoBehaviour
{
    public GameObject rotationTarget;
    public GameObject axisX;
    public GameObject axisY;
    public GameObject axisZ;
    public float rotationSpeed = 0.05f;
    public float pullSpeed = 0.2f;
    private GameObject pullTarget;
    private GameObject pushTarget;
    private Transform targetTransform;
    public string pullableTag;
    private float pullSize = 9.0f;
    private float pushSize = 1.0f;
    private float rotateDestinationX;
    private float rotateDestinationY;
    private float rotateDestinationZ;
    private Camera mainCamera;
    private string rotationAxis = "x";
    public bool axisChangeable = false;
    public string fixedTag;

    // Start is called before the first frame update
    void Start()
    {
        targetTransform = rotationTarget.GetComponent<Transform>();
        rotateDestinationX = targetTransform.localRotation.x;
        rotateDestinationY = targetTransform.localRotation.y;
        rotateDestinationZ = targetTransform.localRotation.z;
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

        // ROTATION AXIS CHANGE

        if (Input.GetKeyDown(KeyCode.X) && axisChangeable)
        {
            rotationAxis = "x";

            axisX.SetActive(true);
            axisY.SetActive(false);
            axisZ.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Y) && axisChangeable)
        {
            rotationAxis = "y";

            axisX.SetActive(false);
            axisY.SetActive(true);
            axisZ.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Z) && axisChangeable)
        {
            rotationAxis = "z";

            axisX.SetActive(false);
            axisY.SetActive(false);
            axisZ.SetActive(true);
        }

        // ROOM ROTATION

        // X axis rotation

        if (rotationAxis == "x")
        {
            targetTransform.localRotation = Quaternion.Lerp(targetTransform.localRotation, Quaternion.Euler(rotateDestinationX, targetTransform.localRotation.y, targetTransform.localRotation.z), rotationSpeed);

            // Set rotation direction

            if (Input.GetKeyDown(KeyCode.Q))
            {
                rotateDestinationX += 90;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                rotateDestinationX -= 90;
            }
        }

        // Y axis rotation

        if (rotationAxis == "y")
        {
            targetTransform.localRotation = Quaternion.Lerp(targetTransform.localRotation, Quaternion.Euler(targetTransform.localRotation.x, rotateDestinationY, targetTransform.localRotation.z), rotationSpeed);

            // Set rotation direction

            if (Input.GetKeyDown(KeyCode.Q))
            {
                rotateDestinationY += 90;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                rotateDestinationY -= 90;
            }
        }

        // Z axis rotation

        if (rotationAxis == "z")
        {
            targetTransform.localRotation = Quaternion.Lerp(targetTransform.localRotation, Quaternion.Euler(targetTransform.localRotation.x, targetTransform.localRotation.y, rotateDestinationZ), rotationSpeed);

            // Set rotation direction

            if (Input.GetKeyDown(KeyCode.Q))
            {
                rotateDestinationZ += 90;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                rotateDestinationZ -= 90;
            }
        } 
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == fixedTag)
        {
            axisChangeable = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == fixedTag)
        {
            axisChangeable = false;
        }
    }
}
