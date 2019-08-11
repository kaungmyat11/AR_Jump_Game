using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackableScript : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;

    private GameObject player;

    public GameObject[] TargetObjects;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        
        mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.DETECTED)
        {
            //OnFound Target
            Debug.Log("Found the target!!!");
            player.GetComponent<Rigidbody>().useGravity = true;
            for (int i = 0; i < TargetObjects.Length; i++)
            {
                TargetObjects[i].SetActive(true);
            }
        }
        else
        {
            //OnLost Target
            Debug.Log("We Lost the Target");
            player.GetComponent<Rigidbody>().useGravity = false;
            for (int i = 0; i < TargetObjects.Length; i++)
            {
                TargetObjects[i].SetActive(false);
            }
        }
    }
}
