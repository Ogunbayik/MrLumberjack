using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [Header("Game Cameras")]
    [SerializeField] private CinemachineVirtualCamera menuCamera;
    [SerializeField] private CinemachineVirtualCamera gameCamera;
    [SerializeField] private CinemachineVirtualCamera cinematicCamera;
    [Header("Cinematic Settings")]
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Vector3 cameraRotation;

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
    private void Start()
    {
        ActivateMenuCamera();
    }
    public void InitializeCinematicCamera(Transform position)
    {
        cinematicCamera.m_Follow = position.transform;
        cinematicCamera.m_LookAt = position.transform;
        cinematicCamera.transform.position = position.position + cameraOffset;
        cinematicCamera.transform.rotation = Quaternion.Euler(cameraRotation);
    }
    public void ActivateCinematicCamera()
    {
        cinematicCamera.gameObject.SetActive(true);
        gameCamera.gameObject.SetActive(false);
        menuCamera.gameObject.SetActive(false);
    }
    public void ActivateGameCamera()
    {
        gameCamera.gameObject.SetActive(true);
        cinematicCamera.gameObject.SetActive(false);
        menuCamera.gameObject.SetActive(false);
    }
    public void ActivateMenuCamera()
    {
        gameCamera.gameObject.SetActive(false);
        cinematicCamera.gameObject.SetActive(false);
        menuCamera.gameObject.SetActive(true);
    }
}
