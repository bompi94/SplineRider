using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawController : MonoBehaviour
{

    public GameObject spline;
    public GameObject point;
    public int pointsNeededToDraw = 4; 

    int counter = 0;
    List<Transform> pointsTransforms = new List<Transform>(); 

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            counter++;
            if (counter < pointsNeededToDraw)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameObject pointClone = Instantiate(point);
                pointClone.transform.position = mousePos;
                pointsTransforms.Add(pointClone.transform);
                if(counter == 1)
                    pointsTransforms.Add(pointClone.transform);
            }

            else
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameObject pointClone = Instantiate(point);
                pointClone.transform.position = mousePos;
                pointsTransforms.Add(pointClone.transform);
                pointsTransforms.Add(pointClone.transform);

                GameObject splineClone = Instantiate(spline);
                splineClone.GetComponent<CatmullRomSpline>().Draw(pointsTransforms.ToArray()); 
                counter = 0;
                pointsTransforms.Clear(); 
            }

        }
    }
}
