using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerSettings;

public class ShootingMode_PlayerController : MonoBehaviour
{
    private Controls controls;
    public Vector2 inputView;

    private Vector3 newCameraRotation;
    private Vector3 newPlayerRotation;

    [Header("References")] public Transform cameraHolder;
    [Header("Settings")]
    public PlayerSettingsModel playerSettings;
    public float viewClampYMin = -70;
    public float viewClampYMax = 80;
    
    public Transform PrefabProjectile;
    public float ProjectileStartSpeed = 50;
    public float OffsetForwardShoot = 2;
    public float TimeBetweenShots = 0.5f;
    private float TimeShoot = 0;

    void Awake()
    {
        controls = new Controls();

        controls.Shooting.Look.performed += e => inputView = e.ReadValue<Vector2>();
        controls.Shooting.Fire.performed += f => ShootSystem();
        
        controls.Enable();
        Cursor.lockState = CursorLockMode.Locked;

        newCameraRotation = cameraHolder.localRotation.eulerAngles;
        newPlayerRotation = transform.localRotation.eulerAngles;
    }

    private void Update()
    {
        CalculateView();
        TimeShoot -= Time.deltaTime;
    }

    private void CalculateView()
    {
        newPlayerRotation.y += playerSettings.ViewXSensitivity * inputView.x * Time.deltaTime;
        transform.rotation = Quaternion.Euler(newPlayerRotation);
        
        newCameraRotation.x += playerSettings.ViewYSensitivity * -inputView.y * Time.deltaTime;
        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x, viewClampYMin, viewClampYMax);
        newCameraRotation.y += playerSettings.ViewXSensitivity * inputView.x * Time.deltaTime;
        
        cameraHolder.localRotation = Quaternion.Euler(newCameraRotation);
    }

    private void ShootSystem()
    {
        if (TimeShoot <= 0)
        {
            TimeShoot = TimeBetweenShots;

            //Création du projetctile au bon endroit
            Transform proj = GameObject.Instantiate<Transform>(PrefabProjectile,
                transform.position + transform.forward * OffsetForwardShoot, transform.rotation);
            //Ajout d une impulsion de départ
            proj.GetComponent<Rigidbody>().AddForce(transform.forward * ProjectileStartSpeed, ForceMode.Impulse);
        }
    }
}