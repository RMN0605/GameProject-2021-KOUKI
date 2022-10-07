using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spring : MonoBehaviour
{
	private Transform myTransform;

	[SerializeField, Header("綱の歪み最大値")]
	private float maxBend = 2.5f;

	[SerializeField, Header("綱の深さ")]
	private float pullDepth = 0.2f;

	[SerializeField, Header("引っ張る速度(速くしすぎるとプレイヤーが抜けます)")]
	private float speedRatio = 2f;

	[SerializeField, Header("バネ力")]
	private float springPower = 0.25f;

	/// <summary>
	/// 自分が何番目のhingeかの番号
	/// </summary>
	public int hingeNum;

	/// <summary>
	/// メインカメラをStartで格納
	/// </summary>
	private Camera mainCamera;
	/// <summary>
	/// Rigidbody2DをStartで格納
	/// </summary>
	private Rigidbody2D rb;

	/// <summary>
	/// オブジェクト目標地点をStartで格納
	/// </summary>
	private Vector3 targetPosition;

	/// <summary>
	/// 自分のHingeに指またはマウス左クリックが触れているか
	/// </summary>
	public bool selfPressed;

	/// <summary>
	/// 他のhingeが触れられていて、自分もそのhingeの範囲内にある場合true
	/// </summary>
	public bool subPressed;

	public static bool ropeWork = true;

	private void Start()
	{
		ropeWork = true;
		myTransform = gameObject.transform;
		mainCamera = Camera.main;
		rb = GetComponent<Rigidbody2D>();
		targetPosition = new Vector3(gameObject.transform.position.x, 0, 0);

		//EventTrigger
		//var eventTrigger = GetComponent<EventTrigger>();
		//var pointerDownEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
		//pointerDownEntry.callback.AddListener((data) => OnPointerDownDelegate((PointerEventData)data));
		//eventTrigger.triggers.Add(pointerDownEntry);

	}

	private void FixedUpdate()
	{
		//FingerFunc();
		SpringMovement();
	}

	///// <summary>
	///// 指idによって変わる処理
	///// </summary>
	///// <param name="data"></param>
	//private void OnPointerDownDelegate(PointerEventData data)
	//{
	//	var touchCount = Input.touchCount;

	//	switch (touchCount)
	//	{
	//		case 1:
	//			{
	//				var touch = Input.GetTouch(0);
	//				break;
	//			}
	//		case 2:
	//			{
	//				var touch0 = Input.GetTouch(0);
	//				var touch1 = Input.GetTouch(1);

	//				var delta0 = math.lengthsq(touch0.position - data.position);
	//				var delta1 = math.lengthsq(touch1.position - data.position);
	//				var touch = delta0 < delta1 ? touch0 : touch1;

	//				fingerId = touch.fingerId;
	//				break;
	//			}

	//	}
	//}


	/// <summary>
	/// Hingeに触れたとき
	/// </summary>
	private void OnMouseDown()
	{
		if(ropeWork)
        {
			selfPressed = true;
			RopeController.apex = hingeNum;

			for (int i = 0; i < RopeController.pullWidth; i++)
			{
				if (hingeNum - i < 0)
				{
					break;
				}

				RopeController.hingeSpringRef[hingeNum - i].subPressed = true;

			}

			for (int i = 0; i < RopeController.pullWidth; i++)
			{
				if (hingeNum + i > RopeController.hingeSpringRef.Length - 1)
				{
					break;
				}

				RopeController.hingeSpringRef[hingeNum + i].subPressed = true;

			}

		}

	}


	private void OnMouseEnter()
	{
		if(ropeWork)
        {
			if (Input.GetMouseButton(0))
			{
				selfPressed = true;
				RopeController.apex = hingeNum;

				for (int i = 0; i < RopeController.pullWidth; i++)
				{
					if (hingeNum - i < 0)
					{
						break;
					}

					RopeController.hingeSpringRef[hingeNum - i].subPressed = true;

				}

				for (int i = 0; i < RopeController.pullWidth; i++)
				{
					if (hingeNum + i > RopeController.hingeSpringRef.Length - 1)
					{
						break;
					}

					RopeController.hingeSpringRef[hingeNum + i].subPressed = true;

				}

			}

		}

	}


	//離れたとき
	private void OnMouseUp()
	{
		if(ropeWork)
        {
			for (int i = 0; i < RopeController.hingeSpringRef.Length; i++)
			{
				RopeController.hingeSpringRef[i].selfPressed = false;
				RopeController.hingeSpringRef[i].subPressed = false;

			}
			Player.diagonal_frag = true;

		}
	}

	private void OnMouseExit()
	{
		if(ropeWork)
        {
			for (int i = 0; i < RopeController.hingeSpringRef.Length; i++)
			{
				RopeController.hingeSpringRef[i].selfPressed = false;
				RopeController.hingeSpringRef[i].subPressed = false;

			}
		}

	}


	/// <summary>
	/// 触れると移動し、離すとtargetPositionに向かうバネの関数
	/// </summary>
	void SpringMovement()
	{
		if (!GameManager.gameOver && ropeWork)
		{

			//if (!selfPressed || !subPressed)//何も触られていない時のバネの動き
			//{
			//	float r = rb.mass * (rb.drag * rb.drag) * springPower;
			//	Vector3 diff = targetPosition - transform.position;
			//	Vector3 force = diff * r;
			//	rb.AddForce(force);

			//}

			//var touchCount = Input.touchCount;
			//if (touchCount <= 0)
			//	return;


			if (selfPressed)    //頂点の動き
			{
				//for (var i = 0; i < touchCount; i++)
				//{
				Vector3 pos = new Vector3(myTransform.position.x, Mathf.Clamp(mainCamera.ScreenPointToRay(Input.mousePosition).origin.y, -maxBend, maxBend), myTransform.position.z);
				myTransform.position = Vector3.MoveTowards(myTransform.position, pos, speedRatio * Time.deltaTime);

				//}

			}
			else if (subPressed)     //頂点以外の範囲内の座標の動き
			{
				if (RopeController.hingeSpringRef[RopeController.apex].myTransform.position.y > 0)
				{
					float ratioY = pullDepth * Mathf.Pow(myTransform.position.x - RopeController.hingeSpringRef[RopeController.apex].myTransform.position.x, 2);
					Vector3 pos = new Vector3(myTransform.position.x, Mathf.Clamp(-ratioY + RopeController.hingeSpringRef[RopeController.apex].myTransform.position.y, 0, maxBend), myTransform.position.z);
					myTransform.position = Vector3.MoveTowards(myTransform.position, pos, speedRatio * Time.deltaTime);

				}
				else
				{
					float ratioY = pullDepth * Mathf.Pow(myTransform.position.x - RopeController.hingeSpringRef[RopeController.apex].myTransform.position.x, 2);
					Vector3 pos = new Vector3(myTransform.position.x, Mathf.Clamp(ratioY + RopeController.hingeSpringRef[RopeController.apex].myTransform.position.y, -maxBend, 0), myTransform.position.z);
					myTransform.position = Vector3.MoveTowards(myTransform.position, pos, speedRatio * Time.deltaTime);

				}
			}
			else
			{
				float r = rb.mass * (rb.drag * rb.drag) * springPower;
				Vector3 diff = targetPosition - transform.position;
				Vector3 force = diff * r;
				rb.AddForce(force);

			}


			/// <summary>
			/// 入力検知
			/// </summary>
			//void FingerFunc()
			//{
			//	var touchCount = Input.touchCount;

			//	if (touchCount <= 0)
			//		return;

			//	if (touchCount >= 1)
			//	{
			//		var touchPos = mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
			//		Vector3 screenPos = new Vector3(touchPos.x, touchPos.y, 0);
			//		RaycastHit hit;
			//		Ray ray = mainCamera.ScreenPointToRay(screenPos);

			//		if(Physics.Raycast(ray,out hit))
			//           {
			//			Transform objectHit = hit.transform;
			//			Debug.Log(objectHit);
			//           }

			//		for (var i = 0; i < 2; i++)
			//		{
			//			var touch = Input.GetTouch(i);

			//			if (touch.fingerId == fingerId)
			//			{
			//				switch (touch.phase)
			//				{
			//					case TouchPhase.Began:
			//						//selfPressed = true;
			//						//RopeController.apex = hingeNum;

			//						//for (int j = 0; j < pullWidth; j++)
			//						//{
			//						//	if (hingeNum - j < 0)
			//						//	{
			//						//		break;
			//						//	}

			//						//	RopeController.hingeSpringRef[hingeNum - j].subPressed = true;

			//						//}

			//						//for (int j = 0; j < pullWidth; j++)
			//						//{
			//						//	if (hingeNum + j > RopeController.hingeSpringRef.Length - 1)
			//						//	{
			//						//		break;
			//						//	}

			//						//	RopeController.hingeSpringRef[hingeNum + j].subPressed = true;

			//						//}

			//						break;

			//					case TouchPhase.Moved:

			//					case TouchPhase.Stationary:

			//						break;

			//					case TouchPhase.Ended:

			//						Debug.Log(touchCount);

			//						//for (int j = 0; j < RopeController.hingeSpringRef.Length; j++)
			//						//{
			//						//	RopeController.hingeSpringRef[j].selfPressed = false;
			//						//	RopeController.hingeSpringRef[j].subPressed = false;

			//						//}
			//						fingerId = -1;

			//						break;

			//					case TouchPhase.Canceled:

			//						break;

			//					default:
			//						throw new ArgumentOutOfRangeException();
			//				}
			//			}
			//		}
			//	}

			//}
		}
		else if(GameManager.gameOver)
        {
			float r = rb.mass * (rb.drag * rb.drag) * springPower;
			Vector3 diff = targetPosition - transform.position;
			Vector3 force = diff * r;
			rb.AddForce(force);
		}
	}
}
