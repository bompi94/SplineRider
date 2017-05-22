using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatmullRomSpline : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;

    //the lower the value, the smoother the line
    //1/resolution represents the number of intermediate points there will be between the knots of the spline
    public float resolution;

    Vector3[] positions;
    int positionsIndex = 0;

    //cachedValues
    Vector2[] colliderPointsArray;
    Vector2 t = Vector2.zero;
    Vector3 intermediatePoint = Vector3.zero;
    Vector3 pi_minus_1, pi, pi_plus_1, pi_plus_2;
    int numberOfIntermediatePoints; 

    private void Awake()
    {
        //how many intermediate points should we get? 
        numberOfIntermediatePoints = Mathf.FloorToInt(1f / resolution);
    }

    public void Draw(float[] x, float[] y)
    {
        if (positions == null)
        {
            positions = new Vector3[(numberOfIntermediatePoints+1) * (x.Length-3)];
            colliderPointsArray = new Vector2[positions.Length];
        }

        positionsIndex = 0; 

        //Draw the Catmull-Rom spline between the points for each pair on points
        for (int i = 0; i < x.Length; i++)
        {
            //avoids getting out of range
            if (i > 0 && i + 2 <= x.Length - 1)
            {
                //four points are needed to draw a spline
                pi_minus_1.x = x[i - 1];
                pi_minus_1.y = y[i - 1];

                pi.x = x[i];
                pi.y = y[i];

                pi_plus_1.x = x[i + 1];
                pi_plus_1.y = y[i + 1];

                pi_plus_2.x = x[i + 2];
                pi_plus_2.y = y[i + 2];

                FindIntermediatePoints(pi_minus_1, pi, pi_plus_1, pi_plus_2);
            }
        }

        //draw line in unity game
        lineRenderer.numPositions = positions.Length;
        lineRenderer.SetPositions(positions);

        //creates a collider that follow the line
        for (int i = 0; i < positions.Length; i++)
        {
            colliderPointsArray[i].x = positions[i].x;
            colliderPointsArray[i].y = positions[i].y;
        }
        edgeCollider.points = colliderPointsArray;
    }

    //finds some intermediate points and adds them to the positions list
    void FindIntermediatePoints(Vector3 pi_minus_1, Vector3 pi, Vector3 pi_plus_1, Vector3 pi_plus_2)
    {
        positions[positionsIndex] = pi;
        positionsIndex++;

        for (int i = 1; i < numberOfIntermediatePoints; i++)
        {
            float percentageOnTheLine = i * resolution;
            intermediatePoint = GetCatmullRomPosition(percentageOnTheLine, pi_minus_1, pi, pi_plus_1, pi_plus_2);

            positions[positionsIndex] = intermediatePoint;
            positionsIndex++;
        }

        positions[positionsIndex] = pi_plus_1;
        positionsIndex++;
    }

    Vector3 GetCatmullRomPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        //coefficients
        Vector3 a = 2f * p1;
        Vector3 b = p2 - p0;
        Vector3 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
        Vector3 d = -p0 + 3f * p1 - 3f * p2 + p3;

        Vector3 pos =  .5f * (a + (b * t) + (c * t * t) + (d * t * t * t));

        return pos;
    }
}