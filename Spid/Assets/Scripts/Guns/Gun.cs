using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    [SerializeField] private Bullet bullet;
    [SerializeField] public Transform shootPoint;
    [SerializeField] private int shootForce;
    [SerializeField] private int magazineSize;
    private int bulletsLeft;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private bool allowHold;

    //Keys
    public KeyCode shootKey = KeyCode.Mouse0;
    public KeyCode reloadKey = KeyCode.R;

    public Camera fpsCam;
    public GameObject bulletHole;

    private GameObject currentHole;
    

    private void Awake(){
        bulletsLeft = magazineSize;
    }

    public void Shoot(){
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        Vector3 targetPoint;

        if(Physics.Raycast(ray, out hit)){
            targetPoint = hit.point;
            currentHole = Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));
            currentHole.SetActive(false);
        }
        else{
            targetPoint = ray.GetPoint(75);
        }

        Vector3 shootDirection = targetPoint - shootPoint.position;

        Bullet currentBullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);

        if(currentHole != null){
            currentBullet.SetHole(currentHole);
        }

        currentBullet.GetComponent<Rigidbody>().AddForce(shootDirection.normalized * shootForce, ForceMode.Impulse);
    }
}
