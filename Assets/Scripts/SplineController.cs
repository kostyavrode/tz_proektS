using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using Dreamteck.Editor;

public class SplineController : MonoBehaviour
{
    [SerializeField] private ObjectManager objectManager;
    [SerializeField] private Node nodeConnector;
    [SerializeField] private SplineFollower splineFollowerPrefab;
    private SplineFollower splineFollower;
    private List<SplineComputer> splines=new List<SplineComputer>();
    private int currentSpline;
    private void Start()
    {
        splineFollower.GetComponent<SplineFollower>().onEndReached += ChangeSpline;
    }
    private void OnDisable()
    {
        splineFollower.GetComponent<SplineFollower>().onEndReached -= ChangeSpline;
    }
    private void OnApplicationQuit()
    {
        splineFollower.GetComponent<SplineFollower>().onEndReached -= ChangeSpline;
    }
    public void CreateSplines(List<Vector3> dotsList, List<int> splinesCount)
    {
        List<SplinePoint> tempPoint = new List<SplinePoint>();
        int tempK=0;
        for (int i=0; i<splinesCount.Count;i++)
        {
            GameObject newObject = new GameObject("Spline" + i);
        SplineComputer spline = newObject.AddComponent<SplineComputer>();
            splines.Add(spline);
        for (int k = 0; k < splinesCount[i]; k++)
        {
                SplinePoint point = new SplinePoint();
                point.position = dotsList[tempK];
                point.normal = Vector3.up;
                point.size = 1f;
                point.color = Color.white;
                tempK++;
                splines[i].SetPoint(k, point);
                if (k==0 || k==splinesCount[i]-1)
                {
                    if ((i == 0 && k == 0) || (i == splinesCount.Count - 1 && k == splinesCount[i] - 1))
                    {
                        continue;
                    }

                    {
                        tempPoint.Add(splines[i].GetPoint(k));
                        nodeConnector.AddConnection(splines[i], k);
                    }
                }
            }
            
        splines[i].gameObject.AddComponent<SplineRenderer>().spline=splines[i];
        splines[i].gameObject.AddComponent<PathGenerator>().spline = splines[i];
        }
        tempK = 0;
        for (int i = 0; i < splinesCount.Count; i++)
        {
            for (int k = 0; k < splinesCount[i]; k++)
            {
                if (k == 0 || k == splinesCount[i] - 1)
                {
                    if ((i == 0 && k == 0) || (i == splinesCount.Count - 1 && k == splinesCount[i] - 1))
                    {
                        continue;
                    }
                    splines[i].SetPoint(k, tempPoint[tempK]);// если сразу засунуть точки через AddConection и там же присвоить им назад их координаты, то при добавлении следующей у предыдущей ломается координата
                    tempK++;
                }
            }
        }
        splineFollower =Instantiate(splineFollowerPrefab);
        splines[0].GetComponent<SplineComputer>().Subscribe(splineFollower);
        splineFollower.spline = splines[0];
        objectManager.CreateObjects(splines);
    }
    private void ChangeSpline(double percent)
    {
        if (currentSpline < splines.Count-1)
        {
            currentSpline++;
            splineFollower.spline = splines[currentSpline];
            splineFollower.SetPercent(0);
        }
    }
}
