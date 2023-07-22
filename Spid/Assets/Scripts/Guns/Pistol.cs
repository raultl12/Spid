using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pistol : MonoBehaviour
{
    [SerializeField] private Gun pistol;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Camera fpsCam;
    [SerializeField] private GameObject bulletHole;
    [SerializeField] private TextMeshProUGUI bulletsText;
    private int magazineSize = 16;
    private int bulletsLeft;
    private Color textColor;

    private Animator animator;
    private string currentState;

    private bool canShoot = true;
    private bool canReload = true;

    //Animations
    private string IDLE = "GunIdle";
    private string SHOOT = "ShootAnim";
    private string RELOAD = "GunReload";

    private void Awake(){
        pistol.shootPoint = shootPoint;
        pistol.fpsCam = fpsCam;
        pistol.bulletHole =  bulletHole;
        bulletsLeft = magazineSize;
        bulletsText.SetText(bulletsLeft.ToString());
        textColor = bulletsText.color;
        animator = GetComponent<Animator>();
    }

    private void Start(){
        ChangeAnimationState(IDLE);
    }

    private void Update(){
        HandleInput();
    }

    private void HandleInput(){
        if(Input.GetKeyDown(pistol.shootKey)){
            if(bulletsLeft > 0 && canShoot){
                canReload = false;
                ChangeAnimationState(SHOOT);
                Invoke("SetGunIdle", 0.1f);
                pistol.Shoot();
                bulletsLeft--;
                bulletsText.SetText(bulletsLeft.ToString());
                Invoke("ResetCanReload", 0.1f);
            }
            if(bulletsLeft == 0){
                bulletsText.color = Color.red;
            }
        }

        if(Input.GetKeyDown(pistol.reloadKey) && canReload && bulletsLeft < magazineSize){
            canReload = false;
            canShoot = false;
            ChangeAnimationState(RELOAD);
            Invoke("SetGunIdle", 0.3f);
            bulletsLeft = magazineSize;
            bulletsText.SetText(bulletsLeft.ToString());
            bulletsText.color = textColor;
            Invoke("ResetCanShoot", 0.3f);
            Invoke("ResetCanReload", 0.3f);
        }
    }

    private void ChangeAnimationState(string newState){
        if(currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }

    private void SetGunIdle(){
        ChangeAnimationState(IDLE);
    }

    private void ResetCanShoot(){
        canShoot = true;
    }

    private void ResetCanReload(){
        canReload = true;
    }
}
