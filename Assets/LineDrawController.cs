using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawController : MonoBehaviour
{

    public Color color; 
    public GameObject splinePrefab;
    public GameObject pointPrefab;
    public int pointsNeededToDraw = 4;

    GameObject mySplineObject;
    int counter = 0;
    List<Transform> pointsTransforms = new List<Transform>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            counter++;
            AddPointToShow();
            if (counter == pointsNeededToDraw)
            {
                ResetOldStuff();
                mySplineObject = Instantiate(splinePrefab);
                mySplineObject.GetComponent<LineRenderer>().startColor = color;
                mySplineObject.GetComponent<LineRenderer>().endColor = color;
                CatmullRomSpline spline = mySplineObject.GetComponent<CatmullRomSpline>();
                Transform[] pointsArray = pointsTransforms.ToArray(); 
                spline.Draw(pointsArray);
                ClearAllPoints();
            }

        }
    }

    void ResetOldStuff()
    {
        Destroy(mySplineObject);
        counter = 0; 
    }

    void AddPointToShow()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject pointClone = Instantiate(pointPrefab);
        pointClone.GetComponent<SpriteRenderer>().color = color; 
        pointClone.transform.position = mousePos;
        pointsTransforms.Add(pointClone.transform);

        //I will add 2 point at the extremes because they are the control points
        if (counter == 1 || counter == pointsNeededToDraw) 
            pointsTransforms.Add(pointClone.transform);
    }

    void ClearAllPoints()
    {
        foreach (Transform t in pointsTransforms)
        {
            Destroy(t.gameObject);
        }
        pointsTransforms.Clear();
    }
}
