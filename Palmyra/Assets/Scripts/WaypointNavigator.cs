﻿using Microsoft.MixedReality.Toolkit.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] List<Waypoint> waypoints;
    [SerializeField] RobotBehaviour robotBehaviour;
    [HideInInspector] public Waypoint nextWaypoint;
    [SerializeField] TextToSpeech textToSpeech;
    [SerializeField] GameObject signEvent;
    [SerializeField] GameObject dogEvent;
    [SerializeField] GameObject stairsEvent;

    private int index = 0;

    void Start()
    {
        nextWaypoint = waypoints[index];
    }

    void Update()
    {
        if(nextWaypoint != null) {
            Vector3 waypointProjection = new Vector3(nextWaypoint.transform.position.x, 0, nextWaypoint.transform.position.z);
            Vector3 cameraProjection = new Vector3(cam.transform.position.x, 0, cam.transform.position.z);

            if (Vector3.Distance(waypointProjection, cameraProjection) < 9 && !nextWaypoint.triggered) {
                CloseToWaypoint(nextWaypoint);
            }

            if (Vector3.Distance(waypointProjection, cameraProjection) < 3) {
                if(index < waypoints.Count) {
                    index++;
                    nextWaypoint = waypoints[index];
                    if (index == waypoints.Count) {
                        WaypointsCompleted();
                    }
                }
            }
        }
    }

    private void CloseToWaypoint(Waypoint waypoint) {
        waypoint.triggered = true;
        switch (waypoint.waypointType) {
            case Waypoint.WaypointType.Regular:
                break;
            case Waypoint.WaypointType.Sign:
                TrainSignEvent();
                break;
            case Waypoint.WaypointType.Dog:
                DogEvent();
                break;
            case Waypoint.WaypointType.Stairs:
                StairsEvent();
                break;
            case Waypoint.WaypointType.Finish:
                break;
            default:
                break;
        }
    }

    private void TrainSignEvent() {
        signEvent.SetActive(true);
    }

    private void DogEvent() {
        dogEvent.SetActive(true);
        textToSpeech.StartSpeaking("Angry dog detected, don't pet, 97% confidence of bite.");
    }

    private void StairsEvent() {
        stairsEvent.SetActive(true);
        textToSpeech.StartSpeaking("Stairs ahead, be careful, don't fall.");
    }

    private void WaypointsCompleted() {
        Debug.Log("Waypoints completed");
        robotBehaviour.ChangeAppState(RobotBehaviour.State.Finish);
    }
}
