using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Vector3[] positions;
    public CinemachineVirtualCamera cam;
    int activePosition = 0;

    void Start()
    {
        if (positions.Length == 0) return;
        cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
        positions[activePosition];
    }

    // Update is called once per frame
    void Update()
    {
        if (positions.Length == 0) return;
        if (Input.GetKeyDown(KeyCode.T))
        {
            activePosition++;
            activePosition = activePosition % positions.Length;
            cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
            positions[activePosition];
        }
    }
}
