using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSkyChanger : MonoBehaviour
{

	public Light sun;
	public Color skyboxColor1;
	public Color skyboxColor2;
	public float skyboxExposure;
	public Color newSunColor;
	public float newSunIntensity;

	public bool rainOnMe;


	private Color skyboxBackupColor1;
	private Color skyboxBackupColor2;
	private float skyboxBackupExposure;
	private Color backupShadowColor;
	private Color backupSunColor;
	private float backupSunIntensity;

	private float lerper = 4;


    // Start is called before the first frame update
    void Start()
    {
		skyboxBackupColor1 = RenderSettings.skybox.GetColor ("_SkyTint");
		skyboxBackupColor2 = RenderSettings.skybox.GetColor ("_GroundColor");
		skyboxBackupExposure = RenderSettings.skybox.GetFloat ("_Exposure");
		backupShadowColor = RenderSettings.subtractiveShadowColor;
		backupSunColor = sun.color;
		backupSunIntensity = sun.intensity;
    }

    // Update is called once per frame
    void Update()
    {
		lerper += Time.deltaTime;
		if (rainOnMe == true) {
			Color newSky = Color.Lerp (skyboxBackupColor1, skyboxColor1, lerper/4);
			Color newGround = Color.Lerp (skyboxBackupColor2, skyboxColor2, lerper/4);
			float exposure = Mathf.Lerp (skyboxBackupExposure, skyboxExposure, lerper / 4);
			Color newShadows = Color.Lerp (backupShadowColor, Color.black, lerper/4);
			Color newSun = Color.Lerp (backupSunColor, newSunColor, lerper/4);
			float newIntensity = Mathf.Lerp (backupSunIntensity, newSunIntensity, lerper / 4);
			RenderSettings.skybox.SetColor ("_SkyTint", newSky);
			RenderSettings.skybox.SetColor ("_GroundColor", newGround);
			RenderSettings.skybox.SetFloat ("_Exposure", exposure);
			RenderSettings.subtractiveShadowColor = newShadows;
			sun.color = newSun;
			sun.intensity = newIntensity;

		} else {
			Color newSky = Color.Lerp (skyboxColor1, skyboxBackupColor1, lerper/4);
			Color newGround = Color.Lerp (skyboxColor2, skyboxBackupColor2, lerper/4);
			float exposure = Mathf.Lerp (skyboxExposure, skyboxBackupExposure, lerper / 4);
			Color newShadows = Color.Lerp (Color.black, backupShadowColor, lerper/4);
			Color newSun = Color.Lerp (newSunColor, backupSunColor, lerper/4);
			float newIntensity = Mathf.Lerp (newSunIntensity, backupSunIntensity, lerper / 4);
			RenderSettings.skybox.SetColor ("_SkyTint", newSky);
			RenderSettings.skybox.SetColor ("_GroundColor", newGround);
			RenderSettings.skybox.SetFloat ("_Exposure", exposure);
			RenderSettings.subtractiveShadowColor = newShadows;
			sun.color = newSun;
			sun.intensity = newIntensity;
		}
		DynamicGI.UpdateEnvironment();
    }

	public void StartRain()
	{
		if (rainOnMe == false) {
			lerper = 0;
			rainOnMe = true;
		}
	}

	public void StopRain()
	{
		if (rainOnMe == true) {
			lerper = 0;
			rainOnMe = false;
		}
	}

	public void OnDisable()
	{
		RenderSettings.skybox.SetColor ("_SkyTint", skyboxBackupColor1);
		RenderSettings.skybox.SetColor ("_GroundColor", skyboxBackupColor2);
		RenderSettings.skybox.SetFloat ("_Exposure", skyboxBackupExposure);
		RenderSettings.subtractiveShadowColor = backupShadowColor;
		//sun.color = backupSunColor;
		//sun.intensity = backupSunIntensity;
	}
}
