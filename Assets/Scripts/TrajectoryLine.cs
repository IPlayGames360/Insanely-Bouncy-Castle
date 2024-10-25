using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryLine : MonoBehaviour
{
    public LineRenderer lr;

	private void Awake()
	{
		lr = GetComponent<LineRenderer>();
	}

	public void RenderLine(Vector3 startPoint, Vector3 endPoint)
	{
		lr.positionCount = 2;
		Vector3[] points = new Vector3[2];
		points[1] = startPoint;
		points[0] = endPoint;

		lr.SetPositions(points);
	}

	public void EndLine()
	{
		lr.positionCount = 0;
	}

}
