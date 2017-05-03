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
    float[] pointsPosX;
    float[] pointsPosY; 
    GameObject[] pointsUsed; 
    CatmullRomSpline spline;

    //cachedValue
    GameObject pointClone;
    Vector2 mousePos;
    int listCont;


    private void Awake()
    {
        pointsUsed = new GameObject[pointsNeededToDraw + 2];
        pointsPosX = new float[pointsNeededToDraw + 2];
        pointsPosY = new float[pointsNeededToDraw + 2];

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

                DrawSpline();

                ClearAllPoints();
            }
        }
    }

    void DrawSpline()
    {
        spline.Draw(pointsPosX, pointsPosY);
        mySplineObject.SetActive(true);
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

        pointsUsed[listCont] = pointClone;
        pointsPosX[listCont] = pointClone.transform.position.x;
        pointsPosY[listCont] = pointClone.transform.position.y; 
        listCont++;

        //I will add 2 point at the extremes because they are the control points
        if (counter == 1 || counter == pointsNeededToDraw)
        {
            pointsUsed[listCont] = pointClone;
            pointsPosX[listCont] = pointClone.transform.position.x;
            pointsPosY[listCont] = pointClone.transform.position.y;
            listCont++;
        }
    }

    void ClearAllPoints()
    {
        for (int i = 0; i < pointsUsed.Length; i++)
        {
            pointsUsed[i].SetActive(false); 
        }
        listCont = 0; 
    }
}
