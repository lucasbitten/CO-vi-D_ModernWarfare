﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingController : MonoBehaviour
{
    public float weaponRange = 50f;                     
    [SerializeField] private LayerMask hitLayer;


    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float shootForce;
    [SerializeField] private Vector3 shootOffset = Vector3.up;
    [SerializeField] private Transform shootingTarget;
    [SerializeField] private float shootingRate = 0.5f;


    [SerializeField]private Camera fpsCam;
    [SerializeField] private Image crosshair;
    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject maskPrefab;

    private float shootingDelay;
    private bool canShoot = true;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

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
        if (shootingDelay >= 0)
        {
            shootingDelay -= Time.deltaTime;
        }


        // Create a vector at the center of our camera's viewport
        Vector3 lineOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        // Draw a line in the Scene View  from the point lineOrigin in the direction of fpsCam.transform.forward * weaponRange, using the color green
        Debug.DrawRay(lineOrigin, fpsCam.transform.forward * weaponRange, Color.green);

        if (Input.GetButtonDown("Fire1") && canShoot && shootingDelay <= 0)
        {
            if (!Physics.Raycast(shootingPoint.position, shootingPoint.forward, 0.5f))
            {
                AudioManager.instance.Play("Shooting");
                particles.SetActive(true);
                shootingDelay = shootingRate;
                animator.SetTrigger("Shooting");
                GameObject mask = Instantiate(maskPrefab, shootingPoint.position, shootingPoint.rotation * Quaternion.AngleAxis(180, Vector3.up));
                mask.GetComponent<Rigidbody>().AddForce((shootingTarget.position - shootingPoint.position).normalized* shootForce + shootOffset);
                Destroy(mask.gameObject, 10);

            }
        }


        RaycastHit hit;

        if (Physics.Raycast(lineOrigin, fpsCam.transform.forward, out hit, weaponRange, hitLayer))
        {
            crosshair.color = Color.green;
        }
        else
        {
            crosshair.color = Color.white;

        }

    }
}
