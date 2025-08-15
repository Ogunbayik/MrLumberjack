using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    private BuildingManager itemReceiver = null;

    [SerializeField] private CinemachineVirtualCamera gameCamera;
    [SerializeField] private CinemachineVirtualCamera cinematicCamera;

    private CinemachineVirtualCamera currentCamera;
    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
    }
    void Start()
    {
        currentCamera = gameCamera;
    }

    public void InitializeCinematicCamera(Transform lookPosition)
    {
        var offsetCamera = new Vector3(0, 5f, -3f);
        cinematicCamera.transform.position = lookPosition.position + offsetCamera;
        cinematicCamera.m_LookAt = itemReceiver.transform;
    }
    public void ChangeCamera()
    {
        if (currentCamera == gameCamera)
            currentCamera = cinematicCamera;
        else
            currentCamera = gameCamera;
    }
    public void SetItemReceiver(BuildingManager itemReceiver)
    {
        this.itemReceiver = itemReceiver;
    }
}
