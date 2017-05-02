using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawController : MonoBehaviour
{

    public Color color; 
    public GameObject splinePrefab;
    public ObjectPooler pointPooler; 
    public int pointsNeededToDraw = 4;

    GameObject mySplineObject;
    int counter = 0;
    List<Transform> pointsTransforms = new List<Transform>();
    CatmullRomSpline spline;

    //cachedValue
    GameObject pointClone;
    Vector2 mousePos;


    private void Awake()
    {
        mySplineObject = Instantiate(splinePrefab);
        mySplineObject.GetComponent<LineRenderer>().startColor = color;
        mySplineObject.GetComponent<LineRenderer>().endColor = color;
        spline = mySplineObject.GetComponent<CatmullRomSpline>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale!=0)
        {
            counter++;
            AddPointToShow();
            if (counter == pointsNeededToDraw)
            {
                ResetOldStuff();
               
                spline.Draw(pointsTransforms);
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
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pointClone = pointPooler.GetPooledObject(); 
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
            t.gameObject.SetActive(false); 
        }
        pointsTransforms.Clear();
    }
}
