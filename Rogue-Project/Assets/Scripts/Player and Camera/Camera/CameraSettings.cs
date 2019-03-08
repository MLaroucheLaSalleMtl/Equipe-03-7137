using UnityEngine;
using UnityEngine.UI;

public class CameraSettings : MonoBehaviour
{
    //Variable List.//
    float DefaultCamOrtho = 5.0f;

    //Objects.//
    public Slider OrthoCamSlider;
    public Camera PlayerCamera;

    private void Start()
    {
        PlayerCamera.orthographicSize = DefaultCamOrtho; //Adjust the FOV on start to default value.//
        PlayerCamera.enabled = true; //Enables player's camera.//
    }

    public void CameraZoom() //Adjust the distance at which the camera is from the player.//
    {
        PlayerCamera.orthographicSize = OrthoCamSlider.value;
    }
}
