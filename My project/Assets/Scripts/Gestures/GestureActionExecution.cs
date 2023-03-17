using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureActionExecution : MonoBehaviour
{
    public ParticleSystem ps1;
    public ParticleSystem ps2;
    public ParticleSystem ps3;
    public ParticleSystem ps4;
    public ParticleSystem ps5;
    public ParticleSystem ps6;
    public TractorBeam tractorBeam;

    public void ExecuteAction(int gestureId)
    {
        switch (gestureId)
        {
            case 1:

                ps1.Play();

                break;

            case 2:
                ps2.Play();
                break;

            case 3:
                ps3.Play();
                break;

            case 4:
                ps4.Play();
                break;

            case 5:
                ps5.Play();
                break;

            case 6:
                tractorBeam.WaiveOff();
                break;

            default:
                throw new System.Exception("Call Gesture Action went wrong. Possible missclasification");
        }
    }
}
