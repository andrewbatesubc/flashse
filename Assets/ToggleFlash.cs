using UnityEngine;

public class ToggleFlash : MonoBehaviour
{
	private bool active;
	private int camID = 0;
	private AndroidJavaObject phoneCamera;
	private AndroidJavaClass cameraClass;

	void Awake()
	{
		WebCamDevice[] devices = WebCamTexture.devices;
		cameraClass = new AndroidJavaClass("android.hardware.Camera");      
	}

	public void ToggleAndroidFlashlight()
	{
		if (!active)
		{
			EnableFlash();
		}
		else
		{
			DisableFlash();
		}
	}

	private void EnableFlash()
	{
		phoneCamera = cameraClass.CallStatic<AndroidJavaObject>("open", camID);
		AndroidJavaObject cameraParameters = phoneCamera.Call<AndroidJavaObject>("getParameters");
		cameraParameters.Call("setFlashMode", "torch");
		phoneCamera.Call("setParameters", cameraParameters);
		phoneCamera.Call("startPreview");
		active = true;
	}

	private void DisableFlash()
	{
		phoneCamera.Call("stopPreview");
		phoneCamera.Call("release");
		active = false;
	}
}
