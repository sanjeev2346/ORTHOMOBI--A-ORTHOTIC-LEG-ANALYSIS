using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.TestTools;

public class CharacterControl : MonoBehaviour
{
    public Transform thighL;
    public Transform thighR;
    public Transform shinL;
    public Transform shinR;
    public Transform footL;
    public Transform footR;
    SerialPort stream = new SerialPort("COM4",9600);

    void Start()
    {
        stream.Open();
    }

    float getAngle()
    {
        if (stream.IsOpen)
        {
            string angle = stream.ReadLine();
            return float.Parse(angle);
        }
        return 0;
    }

    float ConvertRange(
    float originalStart, float originalEnd, // original range
    float newStart, float newEnd, // desired range
    float value) // value to convert
    {
        double scale = (double)(newEnd - newStart) / (originalEnd - originalStart);
        return (float)(newStart + ((value - originalStart) * scale));
    }

    void Update()
    {
        float xAng = getAngle();
        float normalizedXAng = ConvertRange(0.0f, 360.0f, 0.0f, 90.0f, xAng);
        Vector3 currTL = new Vector3(90.0f+normalizedXAng, 0.0f, 0.0f);
        thighL.eulerAngles = currTL;
    }
}
