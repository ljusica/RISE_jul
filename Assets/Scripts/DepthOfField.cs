using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DepthOfField : MonoBehaviour
{
    private Transform camera;
    private Transform player;

    private Vector3 distance;

    public Volume postProcessingVolume;


    void Start()
    {
        camera = Camera.main.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        distance = player.position - camera.position;

        Debug.Log(player.position - camera.position);
    }



}
