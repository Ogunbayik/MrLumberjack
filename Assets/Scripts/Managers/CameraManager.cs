using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    private PlayerCarryController player;

    [Header("Game Cameras")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CinemachineVirtualCamera[] menuCameras;
    [SerializeField] private CinemachineVirtualCamera gameCamera;
    [SerializeField] private CinemachineVirtualCamera cinematicCamera;
    [Header("Cinematic Settings")]
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Vector3 cameraRotation;

    private List<Transform> cameraPositionList = new List<Transform>();

    private int priorityActiveIndex = 5;
    private int priorityDeactiveIndex = 1;

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

        player = FindObjectOfType<PlayerCarryController>();
    }
    private void Start()
    {
        foreach (var camera in menuCameras)
        {
            cameraPositionList.Add(camera.transform);
        }

        GameManager.Instance.OnClickStartButton += Instance_OnClickStartButton;
        GameManager.Instance.OnGameStart += Instance_OnGameStart;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnClickStartButton -= Instance_OnClickStartButton;
        GameManager.Instance.OnGameStart -= Instance_OnGameStart;
    }
    private void Instance_OnGameStart()
    {
        ResetCameraPosition();
    }
    private void Instance_OnClickStartButton()
    {
        ActivateGameCamera();
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
        foreach (var camera in menuCameras)
        {
            camera.Priority = priorityDeactiveIndex;
        }

        gameCamera.Priority = priorityDeactiveIndex;
        cinematicCamera.Priority = priorityActiveIndex;
    }
    public void ActivateGameCamera()
    {
        foreach (var camera in menuCameras)
        {
            camera.m_LookAt = player.transform;
            camera.m_Follow = player.transform;
            camera.transform.position = mainCamera.transform.position;
            camera.transform.rotation = mainCamera.transform.rotation;
            camera.Priority = priorityDeactiveIndex;
        }

        cinematicCamera.Priority = priorityDeactiveIndex;
        gameCamera.Priority = priorityActiveIndex;
    }
    private void ResetCameraPosition()
    {
        for (int i = 0; i < menuCameras.Length; i++)
        {
            menuCameras[i].transform.position = cameraPositionList[i].transform.position;
        }
    }
}
