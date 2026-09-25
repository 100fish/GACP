using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Camera thisCamera;
    [SerializeField] private Transform boxCameraPoint;


    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject boxPanel;

    public struct LerpInfo
    {
        public LerpInfo(Transform IlerpObject, Vector3 IstartPosition, Quaternion IstartRotation, Vector3 ItargetPosition, Quaternion ItargetRotation, float IstartTime, float Iduration)
        {
            lerpObject = IlerpObject;
            startPosition = IstartPosition;
            startRotation = IstartRotation;
            targetPosition = ItargetPosition;
            targetRotation = ItargetRotation;
            startTime = IstartTime;
            duration = Iduration;
        }

        public Transform lerpObject;
        public Vector3 startPosition;
        public Quaternion startRotation;
        public Vector3 targetPosition;
        public Quaternion targetRotation;
        public float startTime;
        public float duration;
    }

    private List<LerpInfo> lerps = new List<LerpInfo>();



    public void StartBoxes()
    {
        AddLerp(thisCamera.transform, boxCameraPoint.position, boxCameraPoint.rotation, 3);
    }

    private void Update()
    {
        DoLerps();
    }

    private void DoLerps()
    {
        for (int i = 0; i < lerps.Count; i++)
        {
            float progressTime = Time.time - lerps[i].startTime;

            if(progressTime >= lerps[i].duration)
            {
                lerps.RemoveAt(i);
                i--;
                continue;
            }
                
            
            lerps[i].lerpObject.position = Vector3.Lerp(lerps[i].startPosition, lerps[i].targetPosition, progressTime / lerps[i].duration);
            lerps[i].lerpObject.rotation = Quaternion.Lerp(lerps[i].startRotation, lerps[i].targetRotation, progressTime / lerps[i].duration);


            Debug.Log("ProgressTime: " + progressTime);
        }
    }

    public void AddLerp(Transform lerpObject, Vector3 targetPosition, Quaternion targetRotation, float duration)
    {
        LerpInfo newLerp = new LerpInfo(lerpObject, lerpObject.position, lerpObject.rotation, targetPosition, targetRotation, Time.time, duration);
        lerps.Add(newLerp);
    }
}
