using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MatrixBlend))]
public class CameraController : MonoBehaviour {

	private Matrix4x4 ortho, perspective;

	// matrix4x4 ortho set up
	private float aspect;
	private MatrixBlend blender;

	private bool orthoOn;

	Camera cam; // reference to camera
	WorldManager worldManager; // reference to worldmanager

	[Header("Matrix4x4 perspective")]
	public float fov = 60f;
	public float near = .3f;
	public float far = 1000f;
	public float orthographicSize = 50f;

//	[Header("3D Camera Positioning")]
//	[SerializeField] private Vector3 pos3D; // position vector
//	[SerializeField] private Vector3 rot3D; // rotation vector
//
//	[Header("2D Camera Positioning")]
//	[SerializeField] private Vector3 pos2D; // position vector
//	[SerializeField] private Vector3 rot2D; // rotation vector

//	public float timeTakenDuringLerp = 10f;
//	private float timeStartedLerping;

	void Start()
	{
		// get camera
		cam = GetComponent<Camera> ();
		worldManager = WorldManager.instance;

		// 2d ortho set up
		aspect = (float) Screen.width / (float) Screen.height;
		ortho = Matrix4x4.Ortho(-orthographicSize * aspect, orthographicSize * aspect, -orthographicSize, orthographicSize, near, far);

		// 3d perspective set up
		perspective = Matrix4x4.Perspective(fov, aspect, near, far);

		// initial camera position
		if (worldManager.mode2d) {
			cam.projectionMatrix = ortho;
			orthoOn = true;
		} else {
			cam.projectionMatrix = perspective;
		}

		// get matrixblend
		blender = (MatrixBlend) GetComponent(typeof(MatrixBlend));
	}

	void Update()
	{
		if (Input.GetButtonDown("Change Perspective"))
		{
//			timeStartedLerping = Time.time;
			orthoOn = !orthoOn;
			if (orthoOn) {
				blender.BlendToMatrix (ortho, 1f);
//				Debug.Log ("2D");
//				float timeSinceStarted = Time.time - timeStartedLerping;
//				float percentageComplete = timeSinceStarted / timeTakenDuringLerp;
//				Quaternion goalRotation = Quaternion.Euler (rot2D);
//				Quaternion startRotation = Quaternion.Euler (rot3D);
//				transform.localPosition = Vector3.Lerp (pos3D, pos2D, percentageComplete);
//				transform.localRotation = Quaternion.Lerp (startRotation, goalRotation, Time.time * timeTakenDuringLerp);
			} else {
				blender.BlendToMatrix (perspective, 5f);
//				Debug.Log ("3D");
//				float timeSinceStarted = Time.time - timeStartedLerping;
//				float percentageComplete = timeSinceStarted / timeTakenDuringLerp;
//				Quaternion goalRotation = Quaternion.Euler (rot3D);
//				Quaternion startRotation = Quaternion.Euler (rot2D);
//				transform.localPosition = Vector3.Lerp (pos2D, pos3D, percentageComplete);
//				transform.localRotation = Quaternion.Lerp (startRotation, goalRotation, Time.time * timeTakenDuringLerp);
			}
		}
	}
}
