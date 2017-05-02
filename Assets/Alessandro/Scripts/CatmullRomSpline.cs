﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatmullRomSpline : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;

    //the lower the value, the smoother the line
    //1/resolution represents the number of intermediate points there will be between the knots of the spline
    public float resolution;

    List<Vector3> positions = new List<Vector3>();

    //cachedValues
    Vector2[] temp;
    Vector2 t = Vector2.zero;
    Vector3 intermediatePoint = Vector3.zero;
    Vector3 pi_minus_1, pi, pi_plus_1, pi_plus_2; 

    public void Draw(List<Transform> knots)
    {
        positions.Clear();
        //Draw the Catmull-Rom spline between the points for each pair on points
        for (int i = 0; i < knots.Count; i++)
        {
            //avoids getting out of range
            if (i > 0 && i + 2 <= knots.Count - 1)
            {
                //four points are needed to draw a spline
                pi_minus_1 = knots[i - 1].position;
                pi = knots[i].position;
                pi_plus_1 = knots[i + 1].position;
                pi_plus_2 = knots[i + 2].position;

                FindIntermediatePoints(pi_minus_1, pi, pi_plus_1, pi_plus_2);
            }
        }

        //draw line in unity game
        lineRenderer.numPositions = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());


        //create collider for the line
        if (temp == null)
            temp = new Vector2[positions.Count];

        for (int i = 0; i < positions.Count; i++)
        {
            t.x = positions[i].x;
            t.y = positions[i].y;
            temp[i] = t; 
        }
        edgeCollider.points = temp;
    }

    //finds some intermediate points and adds them to the positions list
    void FindIntermediatePoints(Vector3 pi_minus_1, Vector3 pi, Vector3 pi_plus_1, Vector3 pi_plus_2)
    {
        positions.Add(pi);

        //how many intermediate points should we get? 
        int numberOfIntermediatePoints = Mathf.FloorToInt(1f / resolution);

        for (int i = 1; i < numberOfIntermediatePoints; i++)
        {
            float percentageOnTheLine = i * resolution;
            intermediatePoint = GetCatmullRomPosition(percentageOnTheLine, pi_minus_1, pi, pi_plus_1, pi_plus_2);
            positions.Add(intermediatePoint);
        }

        positions.Add(pi_plus_1);
    }

    Vector3 GetCatmullRomPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        //coefficients
        Vector3 a = 2f * p1;
        Vector3 b = p2 - p0;
        Vector3 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
        Vector3 d = -p0 + 3f * p1 - 3f * p2 + p3;

        Vector3 pos = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));

        return pos;
    }
}