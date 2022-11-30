using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RobotCollissions : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] fireworks;
    [SerializeField] private float force;
    
    private Rigidbody rb;
    private RobotController robotController;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        robotController = GetComponent<RobotController>();

        for (int i = 0; i < fireworks.Length; i++)
        {
            fireworks[i] = fireworks[i].GetComponent<ParticleSystem>();
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
            for (int i = 0; i < fireworks.Length; i++)
            {
                var main = fireworks[i].main;
                main.simulationSpeed = 5;
                fireworks[i].Play();
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Respawn"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (col.CompareTag("Playzone"))
        {
            robotController.isGrounded = false;
        }
    }
}
