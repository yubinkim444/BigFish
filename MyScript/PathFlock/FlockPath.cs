using UnityEngine;
using System.Collections;

public class FlockPath: MonoBehaviour {

	public Color color = Color.red;
	public bool isDebug = true;
	public float Radius = 2.0f;
	public GameObject[] pointA;
	
	public float Length
	{
		get
		{
			return pointA.Length;
		}
	}
	public Vector3 GetPoint(int index)
	{
		return pointA[index].transform.position;
	}

	/// <summary>
	/// Show Debug Grids and obstacles inside the editor
	/// </summary>
	void OnDrawGizmos()
	{
		if (!isDebug)
			return;
		
		for (int i = 0; i < pointA.Length; i++)
		{
			if (i + 1 < pointA.Length)
			{
				Debug.DrawLine(pointA[i].transform.position, pointA[i + 1].transform.position, color);
			}
		}
	}

}
