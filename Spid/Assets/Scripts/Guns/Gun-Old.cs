using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunOld : MonoBehaviour
{
    //Bullet
    [SerializeField] private GameObject bullet;

    //Bullet force
    [SerializeField] private float shootForce, upwardForce;

    //Gun stats
    [SerializeField] private float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    [SerializeField] private int magazineSize, bulletsPerTap;
    [SerializeField] private bool allowButtonHold;
    private int bulletsLeft, bulletsShot;

    //Bools
    private bool shooting, readyToShoot, reloading;

    //References
    [SerializeField] private Camera fpsCam;
    [SerializeField] private Transform attackPoint;

    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private TextMeshProUGUI ammunitionDisplay;

    [SerializeField] private bool allowInvoke = true;

    private void Awake(){
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update(){
        MyInput();

        if(ammunitionDisplay != null){
            ammunitionDisplay.SetText(bulletsLeft/bulletsPerTap + " / " + magazineSize / bulletsPerTap);
        }
    }

    private void MyInput(){

        //If allow to hold, keep shooting, else shoot once
        if(allowButtonHold){
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else{
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        //Reload
        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading){
            Reload();
        }

        if(readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();

        if(readyToShoot && shooting && !reloading && bulletsLeft > 0){
            bulletsShot = 0;
            Shoot();
        }
    }

    private void Shoot(){
        readyToShoot = false;

        //A ray from the middle of the screen
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        Vector3 targetPoint;

        if(Physics.Raycast(ray, out hit)){
            targetPoint = hit.point;
        }
        else{
            targetPoint = ray.GetPoint(75);
        }

        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate new direction
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        //Instantiate bullet
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        currentBullet.transform.forward = directionWithSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        //Pegar flashazo
        if(muzzleFlash != null){
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        }

        bulletsLeft--;
        bulletsShot++;

        //Invoke reset
        if(allowInvoke){
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        if(bulletsShot < bulletsPerTap && bulletsLeft > 0){
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShot(){
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload(){
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished(){
        bulletsLeft = magazineSize;
        reloading = false;
    }

}
