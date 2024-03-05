using OculusSampleFramework;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public GameObject bullet;
    public float shootForce;
    public Transform shootPoint;
    private DistanceGrabbable _distanceGrabbable;

    private void Awake()
    {
        _distanceGrabbable = GetComponent<DistanceGrabbable>();
    }

    void Update()
    {
        if (_distanceGrabbable.isGrabbed)
        {
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            {
                Instantiate(bullet, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>().AddForce(shootPoint.forward * shootForce);
            }
        }
    }
}
