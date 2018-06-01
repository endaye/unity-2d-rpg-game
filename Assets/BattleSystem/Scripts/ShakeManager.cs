using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeManager : MonoBehaviour
{

	private RectTransform target;
	private float duration;
	private float strength;
	private float timeElapsed = 0f;
	private Vector2 originalPos;

	public void Shake(RectTransform target, float time = 1f, float strength = 3f)
	{
		this.target = target;
		originalPos = target.anchoredPosition;
		duration = time;
		this.strength = strength;
		timeElapsed = 0f;
	}

	public void Revert()
	{
		target.anchoredPosition = originalPos;
		target = null;
	}

	// Update is called once per frame
	void Update()
	{
		if (target == null)
		{
			return;
		}

		if (timeElapsed < duration)
		{
			var offsetX = Random.Range(-strength, strength) + originalPos.x;
			var offsetY = Random.Range(-strength, strength) + originalPos.y;
			target.anchoredPosition = new Vector2(offsetX, offsetY);

			timeElapsed += Time.deltaTime;
		}
		else
		{
			Revert();
		}
	}
}
