using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class RopeDriver : MonoBehaviour
{
    [SerializeField]
    private Transform ropeStart;

    [SerializeField]
    private Transform ropeEnd;

    [SerializeField]
    private Transform cameraPos;

    [SerializeField]
    private GameObject carabiner; 

    [SerializeField]
    private GameObject[] points = new GameObject[0];

    [SerializeField]
    private GameObject[] conns = new GameObject[0];

    [SerializeField, Min(0.01f)] private float size = 0.3f;

    private Vector3 CountConPos(Vector3 start, Vector3 end) => (start + end) / 2f;
    private Vector3 CountSizeOfCon(Vector3 start, Vector3 end) => new Vector3(size, size, (start - end).magnitude / 2f);
    private Quaternion CountRoationOfCon(Vector3 start, Vector3 end) => Quaternion.LookRotation(Vector3.up, end - start);

    [SerializeField]
    private float offset; 

    private void FixedUpdate()
    {

        if (carabiner.activeSelf)
           {
               ropeStart.position = new Vector3(cameraPos.position.x, cameraPos.position.y - offset, cameraPos.position.z);

               ropeEnd.position = carabiner.transform.position; 

        PlaceConnector();
    }

       
    }

    void PlaceConnector()
    {
        for (int i = 0; i < points.Length - 1; i++)
        {
            conns[i].transform.position = CountConPos(points[i].transform.position, points[i + 1].transform.position);
            conns[i].transform.localRotation = CountRoationOfCon(points[i].transform.localPosition, points[i + 1].transform.localPosition);
            //conns[i].transform.localScale = CountSizeOfCon(points[i].transform.position, points[i + 1].transform.position);
        }
    }
}

