using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaerieAnger : MonoBehaviour
{
    public Material screenMaterial;
    public float angerFadeThreshhold = 1f;

    private float angerIntensity = 0f;
    private float timer = 0f;
	
	void Update ()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            angerIntensity = Mathf.InverseLerp(0f, angerFadeThreshhold, timer);
        }
        if (timer < 0f)
        {
            timer = 0f;
        }
	}

    public void SetAnger(float anger)
    {
        timer = anger;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        screenMaterial.SetFloat("_Intensity", angerIntensity);
        Graphics.Blit(source, destination, screenMaterial);
    }
}
