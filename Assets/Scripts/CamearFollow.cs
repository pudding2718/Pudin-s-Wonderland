using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamearFollow : MonoBehaviour
{
	public Transform target;
	public Vector3 offset;
	[Range(1, 10)]
	public float smoothFactor;
	public Vector3 minValues, maxValues;

	private void FixedUpdate()
	{
		Follow();
	}

	void Follow()
	{
		Vector3 targetPosistion = target.position + offset;
		//Verify if the targetPosition is out of bound or not
		//Limit it to the min and max values
		Vector3 boundPosition = new Vector3(
			Mathf.Clamp(targetPosistion.x, minValues.x, maxValues.x),
			Mathf.Clamp(targetPosistion.y, minValues.y, maxValues.y),
			Mathf.Clamp(targetPosistion.z, minValues.z, maxValues.z));


		Vector3 smoothedPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
		transform.position = smoothedPosition;

	}
}
