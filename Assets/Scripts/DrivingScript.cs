using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrivingScript : MonoBehaviour
{
    public WheelScript[] wheels; //tu bêd¹ wszystkie nasze ko³a
    public float torque = 200; //moment obrotowy
    public float maxSteerAngle = 30; //maxymalny k¹t wychylenia
    public float maxBrakeTorque = 500; //moment hamowania
    public float maxSpeed = 400; //max predkoœæ
    public Rigidbody rb;
    public float currentSpeed; //aktualna prêdkoœæ
    public GameObject backLights;
    public float nitroFuel = 3;
    public GameObject nitroLights;

    public float rpm; //obroty na minutê
    public int currentGear = 1; //aktualny bieg
    float currentGearPerc; //aktualny bieg wyra¿ony w procentach
    public AudioSource engineSound;

    public GameObject cameraTarget;

    public Text nitroText;

    private void Start()
    {
        engineSound = GameObject.FindGameObjectWithTag("AudioSource").GetComponent<AudioSource>();
        nitroText = GameObject.FindGameObjectWithTag("Fuel").GetComponent<Text>();
        nitroText.text = nitroFuel.ToString();
    }

    public void Drive(float accel, float brake, float steer, bool stop = false)
    {
        accel = Mathf.Clamp(accel, -1, 1);
        steer = Mathf.Clamp(steer, -1, 1) * maxSteerAngle;
        brake = Mathf.Clamp(brake, 0, 1) * maxBrakeTorque;

        if (brake != 0)
        {
            backLights.SetActive(true);
        }
        else
        {
            backLights.SetActive(false);
        }

        float thrustTorque = 0;

        currentSpeed = rb.velocity.magnitude * 3;

        if (currentSpeed < maxSpeed)
        {
            thrustTorque = accel * torque * Engine();
        }

        if (stop) brake = 20000000;

        foreach (WheelScript wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = thrustTorque;

            if (wheel.frontWheel)
            {
                wheel.wheelCollider.steerAngle = steer;
            }
            else
            {                
                wheel.wheelCollider.brakeTorque = brake * 10000;
            }
            Quaternion quat;
            Vector3 position;
            wheel.wheelCollider.GetWorldPose(out position, out quat);
            wheel.wheel.transform.position = position;
            wheel.wheel.transform.rotation = quat;
        }
    }
    public float Engine()
    {
        float gears = 5;
        float gearPerc = (1f / gears);
        float speedPerc = Mathf.Abs(currentSpeed / maxSpeed);
        currentSpeed = rb.velocity.magnitude * 3;
        float targetGearFactor = Mathf.InverseLerp(gearPerc * currentGear, gearPerc * (currentGear + 1), speedPerc);
        currentGearPerc = Mathf.Lerp(currentGearPerc, targetGearFactor, Time.deltaTime * 5f);
        var gearsFactor = currentGear / gears;
        rpm = Mathf.Lerp(gearsFactor, 1, currentGearPerc);
        float upperGear = (1 / gears) * (currentGear + 1);
        float downGear = (1 / gears) * currentGear;
        if (currentGear > 0 && speedPerc < downGear) currentGear--;
        if (speedPerc > upperGear && (currentGear < (gears - 1))) currentGear++;
        float pitch = Mathf.Lerp(1, 6, rpm);
        return Mathf.Min(6, pitch) * 0.2f;
    }

    public void EngineSound()
    {
        engineSound.pitch = Engine();
    }

    void Boost(float boostPower)
    {
        rb.AddForce(rb.gameObject.transform.forward * boostPower);
    }
    public void Nitro(bool on)
    {
        if (nitroFuel > 0 && on)
        {
            CancelInvoke("NitroLightsOff");
            Boost(500000);
            nitroFuel -= 1f;
            ChangeFuelTekst();
            nitroLights.SetActive(true);
        }
        else
        {
            Invoke("NitroLightsOff", 2);
        }
    }

    public void NitroLightsOff()
    {
        nitroLights.SetActive(false);
    }

    public void ChangeFuelTekst()
    {
        nitroText.text = nitroFuel.ToString();
    }
}
