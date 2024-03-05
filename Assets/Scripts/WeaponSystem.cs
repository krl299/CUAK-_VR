using System;
using Oculus.Platform;
using OculusSampleFramework;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootForce;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Hand hand;
    private DistanceGrabbable _distanceGrabbable;
    
    enum Hand
    {
        Left,
        Right
    }

    private void Awake()
    {
        _distanceGrabbable = GetComponent<DistanceGrabbable>();
    }

    void Update()
    {
        if (_distanceGrabbable.isGrabbed)
        {
            if (hand == Hand.Left)
            {
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
                {
                    Instantiate(bullet, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>()
                        .AddForce(shootPoint.forward * shootForce);
                }
            }
            else if (hand == Hand.Right)
            {
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                {
                    Instantiate(bullet, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>()
                        .AddForce(shootPoint.forward * shootForce);
                }
            }
        }
    }
}
