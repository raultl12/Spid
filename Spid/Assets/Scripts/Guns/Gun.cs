using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    [SerializeField] private GameObject bullet;
    [SerializeField] public Transform shootPoint;
    [SerializeField] private int shootForce;
    [SerializeField] private int magazineSize;
    private int bulletsLeft;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private bool allowHold;

    //Keys
    [SerializeField] public KeyCode shootKey = KeyCode.Mouse0;
    [SerializeField] public KeyCode reloadKey = KeyCode.R;

    [SerializeField] public Camera fpsCam;

    private void Awake(){
        bulletsLeft = magazineSize;
    }

    public void Shoot(){
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        Vector3 targetPoint;

        if(Physics.Raycast(ray, out hit)){
            targetPoint = hit.point;
        }
        else{
            targetPoint = ray.GetPoint(75);
        }

        Vector3 shootDirection = targetPoint - shootPoint.position;

        GameObject currentBullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);

        currentBullet.GetComponent<Rigidbody>().AddForce(shootDirection.normalized * shootForce, ForceMode.Impulse);
    }
}
