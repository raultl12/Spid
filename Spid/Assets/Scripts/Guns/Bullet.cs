using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody rb;
    [SerializeField] private int lifeTime;
    [SerializeField] private GameObject sparks;

    private GameObject bulletHole;
    

    private void Awake(){
        rb = GetComponent<Rigidbody>();
    }
    private void Start(){
        Invoke("KillBullet", lifeTime);
    }
    private void OnCollisionEnter(Collision collision){
        bulletHole.SetActive(true);
        KillBullet();
    }

    private void KillBullet(){
        rb.velocity = Vector3.zero;
        ParticleSystem particles = sparks.GetComponent<ParticleSystem>();
        particles.Play();
        
        Destroy(gameObject, particles.main.duration);
    }

    public void SetHole(GameObject hole){
        bulletHole = hole;
    }
}
