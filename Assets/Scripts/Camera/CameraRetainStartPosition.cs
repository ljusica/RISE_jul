using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CameraRetainStartPosition : MonoBehaviour
{
    Vector3 startPosition;
    Quaternion startRotation;

    void Awake()
    {
        startPosition =  transform.position;
        startRotation = transform.rotation;
    }

    private async void Start()
    {
        await Delay();
    }

    async Task Delay()
    {
        float endTime = Time.time +1;
        
        while(Time.time < endTime)
        {
            await Task.Yield();
        }
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
