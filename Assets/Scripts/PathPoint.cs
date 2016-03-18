using UnityEngine;
using System.Collections;
using System;

public class PathPoint :  MonoBehaviour ,IComparable<PathPoint>,IEquatable<PathPoint>{
    Vector2 position;
    PathPoint pathpoint;
    int aufwand;

    public PathPoint(Vector2 position, PathPoint lastpoint,int aufwand)
    {
        this.position = position;
        pathpoint = lastpoint;
        this.aufwand = aufwand;
        ID = "" + position.x + position.y;
    }


    public PathPoint GetLastPoint() { return pathpoint; }
    public Vector2 GetPosition() { return position; }
    public float GetAufwand() { return aufwand; }
    public string ID { get; set; }

    public string ToString()
    {
        return "Wert: " + aufwand + " Position: " + position;
    }

    public int CompareTo(PathPoint other)
    {
        if (aufwand.CompareTo(other.GetAufwand()) != 0)
            return aufwand.CompareTo(other.GetAufwand());
        else
            return 0;
    }

    public bool Equals(PathPoint other)
    {
        if (other == null) return false;
        return this.ID.Equals(other.ID);
    }
}
