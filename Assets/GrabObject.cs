// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.XR.Interaction.Toolkit;
// using UnityEngine.InputSystem;
// using System;

// public class GrabObject : MonoBehaviour
// {
//     [SerializeField] 
//     private LayerMask grabbableMask;                  
//     [SerializeField] 
//     private InputActionReference grabButtonPress;     
//     [SerializeField] 
//     private Transform rayOrigin;                      

//     private GameObject grabbedObject = null;          
//     private Rigidbody grabbedRb = null;               
//     public float grabDistance = 5.0f;                 
//     public float throwForce = 10f;                    

//     void Start()
//     {
//         grabButtonPress.action.started += OnGrab;     
//         grabButtonPress.action.canceled += OnRelease; 
//     }

//     void OnGrab(InputAction.CallbackContext context)
//     {
//         if (grabbedObject == null)
//         {
//             RaycastHit hit;
//             bool didHit = Physics.Raycast(
//                 rayOrigin.position,
//                 rayOrigin.forward,
//                 out hit,
//                 grabDistance,
//                 grabbableMask);

//             if (didHit)
//             {
//                 grabbedObject = hit.collider.gameObject;
//                 grabbedRb = grabbedObject.GetComponent<Rigidbody>();

//                 if (grabbedRb != null)
//                 {
//                     Debug.Log("Object grabbed: " + grabbedObject.name);
//                     grabbedRb.isKinematic = true;                 
//                     grabbedObject.transform.SetParent(rayOrigin); 
//                 }
//                 else
//                 {
//                     Debug.LogWarning("Grabbed object has no Rigidbody.");
//                 }
//             }
//             else
//             {
//                 Debug.LogWarning("No grabbable object in range.");
//             }
//         }
//     }

//     void OnRelease(InputAction.CallbackContext context)
//     {
//         if (grabbedObject != null)
//         {
//             grabbedObject.transform.SetParent(null);
//             grabbedRb.isKinematic = false;
//             grabbedRb.AddForce(rayOrigin.forward * throwForce, ForceMode.VelocityChange);

//             Debug.Log("Object released: " + grabbedObject.name);
//             grabbedObject = null;
//             grabbedRb = null;
//         }
//     }

//     void OnDestroy()
//     {
//         grabButtonPress.action.started -= OnGrab;
//         grabButtonPress.action.canceled -= OnRelease;
//     }
// }


using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class GrabObject : MonoBehaviour
{
    [SerializeField]
    private LayerMask grabbableMask;                  
    [SerializeField]
    private InputActionReference grabButtonPress;     
    [SerializeField]
    private Transform rayOrigin;                      

    private GameObject grabbedObject = null;          
    private Rigidbody grabbedRb = null;               
    public float grabDistance = 5.0f;                 
    public float throwForce = 10f;                    

    void Start()
    {
        grabButtonPress.action.started += OnGrab;     
        grabButtonPress.action.canceled += OnRelease; 
    }

    void OnGrab(InputAction.CallbackContext context)
    {
        if (grabbedObject == null)
        {
            RaycastHit hit;
            bool didHit = Physics.Raycast(
                rayOrigin.position,
                rayOrigin.forward,
                out hit,
                grabDistance,
                grabbableMask);

            if (didHit)
            {
                grabbedObject = hit.collider.gameObject;
                grabbedRb = grabbedObject.GetComponent<Rigidbody>();

                if (grabbedRb != null)
                {
                    Debug.Log("Object grabbed: " + grabbedObject.name);
                    grabbedRb.isKinematic = true;                 
                    grabbedObject.transform.SetParent(rayOrigin); 
                    grabbedObject.transform.localScale *= 0.1f; // Scale down to one-tenth
                }
                else
                {
                    Debug.LogWarning("Grabbed object has no Rigidbody.");
                }
            }
            else
            {
                Debug.LogWarning("No grabbable object in range.");
            }
        }
    }

    void OnRelease(InputAction.CallbackContext context)
    {
        if (grabbedObject != null)
        {
            // Detach the object from the parent
            grabbedObject.transform.SetParent(null);
            grabbedRb.isKinematic = false;
            grabbedRb.AddForce(rayOrigin.forward * throwForce, ForceMode.VelocityChange);

            Debug.Log("Object released: " + grabbedObject.name);
            
            // Start the coroutine to destroy the object after 1 second
            StartCoroutine(DestroyAfterTime(grabbedObject, 1f));

            // Reset the grabbed object and Rigidbody reference
            grabbedObject = null;
            grabbedRb = null;
        }
    }

    private IEnumerator DestroyAfterTime(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj); // Destroy the object after the delay
    }

    void OnDestroy()
    {
        // Unsubscribe from the input actions
        grabButtonPress.action.started -= OnGrab;
        grabButtonPress.action.canceled -= OnRelease;
    }
}
