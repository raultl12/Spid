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

    //Animations
    private string SHOOT = "ShootAnimation";

    private void Awake(){
        pistol.shootPoint = shootPoint;
        pistol.fpsCam = fpsCam;
        pistol.bulletHole =  bulletHole;
        bulletsLeft = magazineSize;
        bulletsText.SetText(bulletsLeft.ToString());
        textColor = bulletsText.color;
        //animator = GetComponent<Animator>();
    }

    private void Update(){
        HandleInput();
    }

    private void HandleInput(){
        if(Input.GetKeyDown(pistol.shootKey)){
            if(bulletsLeft > 0){
                //ChangeAnimationState(SHOOT);
                pistol.Shoot();
                bulletsLeft--;
                bulletsText.SetText(bulletsLeft.ToString());
            }
            if(bulletsLeft == 0){
                bulletsText.color = Color.red;
            }
        }

        if(Input.GetKeyDown(pistol.reloadKey)){
            bulletsLeft = magazineSize;
            bulletsText.SetText(bulletsLeft.ToString());
            bulletsText.color = textColor;
        }
    }

    private void ChangeAnimationState(string newState){
        if(currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
