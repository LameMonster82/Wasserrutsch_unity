using UnityEngine;

public class DarkerTransitionScript : MonoBehaviour
{
    [SerializeField] float a = 0F;
    [SerializeField] float b = 0F;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            ChangeExposure();
        }
    }

    private bool daytime = true;

    private void ChangeExposure()
    {
        float exposure;
        exposure = daytime ? 0.1F : 1.0F;
        RenderSettings.ambientIntensity = exposure;
        daytime = !daytime;
    }
}
