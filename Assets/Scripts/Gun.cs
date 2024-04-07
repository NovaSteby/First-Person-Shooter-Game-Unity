using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public class Gun : MonoBehaviour
{
    [SerializeField] InputActionAsset actionAsset;


    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject bulletPoint;
    [SerializeField]
    private float bulletSpeed = 600;

    public XRGrabInteractable grabInteractable;

    bool isGrabbing = false;

    public GameObject XrRig;
    // Start is called before the first frame update
    void Start()
    {
        /*_input = transform.root.GetComponent<StarterAssetsInputs>();*/
        var activate = actionAsset.FindActionMap("XRI RightHand Interaction").FindAction("Activate");
        activate.Enable();
        activate.performed += onShoot;
       
        grabInteractable.onSelectEntered.AddListener(OnGrab);
        grabInteractable.onSelectExited.AddListener(OnRelease);

    }
    // Update is called once per frame
    void Update()
    {/*
        if(_input.shoot)
        {
            Shoot();
            _input.shoot = false;

        }*/
    }
    void Shoot()
    {
        Debug.Log("Shoot!");
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        Destroy(bullet, 1);
    }
    private void onShoot(InputAction.CallbackContext context)
    {if(isGrabbing)
        {
            Debug.Log("Shoot!");
            GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
            Destroy(bullet, 1);
        }
       

    }
    private void OnGrab(XRBaseInteractor interactor)
    {
        isGrabbing = true;
        this.gameObject.transform.parent = XrRig.transform;
    }
    private void OnRelease(XRBaseInteractor interactor)
    {
        isGrabbing = false;
        this.gameObject.transform.parent = null;
    }
}
