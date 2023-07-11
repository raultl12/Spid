using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [SerializeField] private Gun pistol;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Camera fpsCam;

    private void Awake(){
        pistol.shootPoint = shootPoint;
        pistol.fpsCam = fpsCam;
    }

    private void Update(){
        HandleInput();
    }

    private void HandleInput(){
        if(Input.GetKeyDown(pistol.shootKey)){
            pistol.Shoot();
        }

        if(Input.GetKeyDown(pistol.reloadKey)){
            Debug.Log("reaLOAD");
        }
    }
}
