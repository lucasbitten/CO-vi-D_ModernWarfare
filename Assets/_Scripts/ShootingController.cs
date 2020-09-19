using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingController : MonoBehaviour
{
    public float weaponRange = 50f;                       // Distance in Unity units over which the Debug.DrawRay will be drawn

    [SerializeField]private Camera fpsCam;                                // Holds a reference to the first person camera

    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private Image dot;

    void Update()
    {
        // Create a vector at the center of our camera's viewport
        Vector3 lineOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        // Draw a line in the Scene View  from the point lineOrigin in the direction of fpsCam.transform.forward * weaponRange, using the color green
        Debug.DrawRay(lineOrigin, fpsCam.transform.forward * weaponRange, Color.green);
        RaycastHit hit;

        if (Physics.Raycast(lineOrigin, fpsCam.transform.forward, out hit, weaponRange, hitLayer))
        {
            dot.color = Color.green;
        }
        else
        {
            dot.color = Color.red;

        }

    }
}
