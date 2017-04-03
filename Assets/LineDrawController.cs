using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawController : MonoBehaviour
{

    public GameObject splinePrefab;
    public GameObject pointPrefab;
    public int pointsNeededToDraw = 4;

    CatmullRomSpline spline;
    GameObject mySplineObject;
    List<Transform> pointsTransforms = new List<Transform>();

    private void Awake()
    {
        mySplineObject = Instantiate(splinePrefab);
        spline = mySplineObject.GetComponent<CatmullRomSpline>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AddPointToShow();
            Transform[] pointsArray = pointsTransforms.ToArray();

            if (pointsTransforms.Count > 3)
                spline.Draw(pointsArray);
        }
    }

    void AddPointToShow()
    {
        if (pointsTransforms.Count != 0)
            pointsTransforms.RemoveAt(pointsTransforms.Count - 1); 

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject pointClone = Instantiate(pointPrefab);
        pointClone.transform.position = mousePos;

        pointsTransforms.Add(pointClone.transform);
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
