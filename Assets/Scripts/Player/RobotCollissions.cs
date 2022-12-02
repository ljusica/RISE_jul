using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCollissions : MonoBehaviour
{
    [SerializeField] private GameObject[] fireworks;
    [SerializeField] private float force;

    [SerializeField] RoboGameHandler gameHandler;

    private ParticleSystem[] fireworkParticles;
    private AudioSource[] fireworkSounds;

    private Rigidbody rb;
    private RobotController robotController;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        robotController = GetComponent<RobotController>();
        fireworkParticles = new ParticleSystem[3];
        fireworkSounds = new AudioSource[3];

        for (int i = 0; i < fireworks.Length; i++)
        {
            fireworkParticles[i] = fireworks[i].GetComponent<ParticleSystem>();
            fireworkSounds[i] = fireworks[i].GetComponent<AudioSource>();
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Chair"))
        {
            rb.AddForce(col.transform.forward * force);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Playzone"))
        {
            robotController.isGrounded = true;
        }        
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Winzone"))
        {
            StartCoroutine(nameof(SpawnFireworks));
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Respawn"))
        {
            transform.position = startPos;
            rb.velocity = Vector3.zero;
        }

        if (col.CompareTag("Playzone"))
        {
            robotController.isGrounded = false;
        }
    }

    IEnumerator SpawnFireworks()
    {
        PlayFireworks(0);

        yield return new WaitForSeconds(0.7f);
        PlayFireworks(1);

        yield return new WaitForSeconds(1.2f);
        PlayFireworks(2);
        gameHandler.GoBackToOffice();
    }

    private void PlayFireworks(int index)
    {
        var main = fireworkParticles[index].main;
        main.simulationSpeed = 5;
        fireworkParticles[index].Play();
        fireworkSounds[index].Play();
    }
}
