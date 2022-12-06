using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CameraRetainStartPosition : MonoBehaviour
{
    Vector3 startPosition;
    Quaternion startRotation;

    [SerializeField] CinemachineVirtualCamera roboBrain;

    void Awake()
    {
        startPosition =  transform.position;
        startRotation = transform.rotation;

        roboBrain = GetComponent<CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        CinemachineFramingTransposer transposer = roboBrain.GetCinemachineComponent<CinemachineFramingTransposer>();
        transposer.ForceCameraPosition(Vector3.zero, Quaternion.identity);
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
