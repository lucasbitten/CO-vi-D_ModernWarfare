using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingController : MonoBehaviour
{
    public float weaponRange = 50f;                     

    [SerializeField]private Camera fpsCam;                                

    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private Image dot;
    [SerializeField] private GameObject maskPrefab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float shootForce;
    private bool canShoot = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            canShoot = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            canShoot = true;
        }
    }

    void Update()
    {
        // todo: make the mask goes in the correct direction
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            if (!Physics.Raycast(shootingPoint.position, shootingPoint.forward, 0.5f))
            {
                GameObject mask = Instantiate(maskPrefab, shootingPoint.position, shootingPoint.rotation * Quaternion.AngleAxis(180, Vector3.up));
                mask.GetComponent<Rigidbody>().AddForce(fpsCam.transform.forward * shootForce);
                Destroy(mask.gameObject, 2);

            }
        }

        // Create a vector at the center of our camera's viewport
        Vector3 lineOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        Debug.Log(lineOrigin);

        // Draw a line in the Scene View  from the point lineOrigin in the direction of fpsCam.transform.forward * weaponRange, using the color green
        Debug.DrawRay(lineOrigin, fpsCam.transform.forward * weaponRange, Color.green);
        Debug.DrawRay(shootingPoint.position, fpsCam.transform.forward * weaponRange, Color.blue);

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
