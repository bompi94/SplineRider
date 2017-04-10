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

    List<Vector3> positions = new List<Vector3>();

    public void Draw(Transform[] knots)
    { 
        positions.Clear();
        //Draw the Catmull-Rom spline between the points for each pair on points
        for (int i = 0; i < knots.Length; i++)
        {
            //avoids getting out of range
            if (i > 0 && i + 2 <= knots.Length - 1)
            {
                Vector3 pi_minus_1 = knots[i - 1].position;
                Vector3 pi = knots[i].position;
                Vector3 pi_plus_1 = knots[i + 1].position;
                Vector3 pi_plus_2 = knots[i + 2].position;

                FindPoints(pi_minus_1, pi, pi_plus_1, pi_plus_2);
            }
        }

        //draw line in unity game
        lineRenderer.numPositions = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
        //create collider for the line
        Vector2[] temp = new Vector2[positions.Count];
        for (int i = 0; i < positions.Count; i++)
        {
            temp[i] = new Vector2(positions[i].x, positions[i].y);
        }
        edgeCollider.points = temp;
    }

    //finds some intermediate points and adds them to the positions list
    void FindPoints(Vector3 pi_minus_1, Vector3 pi, Vector3 pi_plus_1, Vector3 pi_plus_2)
    {
        positions.Add(pi); 
        
        //how many intermediate points should we get? 
        int numberOfIntermediatePoints = Mathf.FloorToInt(1f / resolution);

        for (int i = 1; i < numberOfIntermediatePoints; i++)
        {
            float percentageOnTheLine = i * resolution;
            Vector3 intermediatePoint = GetCatmullRomPosition(percentageOnTheLine, pi_minus_1, pi, pi_plus_1, pi_plus_2);
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