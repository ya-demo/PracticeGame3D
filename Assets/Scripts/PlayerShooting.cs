using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damageOerShot = 20;
    public float range = 100f;
    private Ray shootRay;
    private RaycastHit shootHit;
    private int shootableMask;
    private Light gunLight;
    private ParticleSystem gunParticle;
    private AudioSource gunAudio;
    private LineRenderer gunLine;

    public float timeBetweenBullets = 0.15f;
    private float effectsDiplayTime = 0.2f;
    float timer;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Enemy");
        gunParticle = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    private void Shoot()
    {
        timer = 0f; 
        gunAudio.Play();
        gunLight.enabled = true;
        gunParticle.Stop();
        gunParticle.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            gunLine.SetPosition(1, shootHit.point);
        }else{
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }

    }

    void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(Input.GetButtonDown("Fire1") && timer >= timeBetweenBullets)
        {
            Shoot();
        }
        if(timer >= timeBetweenBullets * effectsDiplayTime)
        {
            DisableEffects();
        }
    }
}
