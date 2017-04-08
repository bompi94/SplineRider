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
    CatmullRomSpline spline; 

    private void Awake()
    {
        mySplineObject = Instantiate(splinePrefab);
        mySplineObject.GetComponent<LineRenderer>().startColor = color;
        mySplineObject.GetComponent<LineRenderer>().endColor = color;
        spline = mySplineObject.GetComponent<CatmullRomSpline>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            counter++;
            AddPointToShow();
            if (counter == pointsNeededToDraw)
            {
                ResetOldStuff();
               
                Transform[] points = pointsTransforms.ToArray(); 
                spline.Draw(points);
                mySplineObject.SetActive(true); 

                ClearAllPoints();
            }

        }
    }

    void ResetOldStuff()
    {
        mySplineObject.SetActive(false); 
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
