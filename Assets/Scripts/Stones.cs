using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stones : MonoBehaviour
{
	public LayerMask Ground;//获取定义的图层ground
	public Transform leftpoint, rightpoint;//获取左右边界点
	public float Speed;//定义速度

	private float leftx, rightx;//定义用来接收左右点
	private Rigidbody2D rb;//获取Rigidbody组件
	private Collider2D Coll;//获取碰撞体组件
	private bool Faceleft = true;//刚开始让平台向左的条件是正确

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();//游戏开始时获取刚体组件
		Coll = GetComponent<Collider2D>();//游戏开始时获取碰撞体组件
		leftx = leftpoint.position.x;//获取左临界点的坐标
		rightx = rightpoint.position.x;//获取右临界点的坐标
		Destroy(leftpoint.gameObject);//为防止每帧出现过多的临界点，每次删除一次
		Destroy(rightpoint.gameObject);//为防止每帧出现过多的临界点，每次删除一次
	}

	void Update()
	{
		Movement();//调用移动函数
	}

	void Movement()//移动函数
	{
		if (Faceleft)//当面向左边时
		{
			rb.velocity = new Vector2(-Speed, 0);//平台向左移动
			if (transform.position.x < leftx)//当平台的位置超过左边界点的位置时
			{
				rb.velocity = new Vector2(Speed, 0);//平台向右移动
				Faceleft = false;
			}
		}
		else//否则面向右边时
		{
			rb.velocity = new Vector2(Speed, 0);//平台向右移动
			if (transform.position.x > rightx)//当平台的位置超过右边界点的位置时
			{
				rb.velocity = new Vector2(-Speed, 0);//平台向左移动
				Faceleft = true;
			}
		}
	}
}
