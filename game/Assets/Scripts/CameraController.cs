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

	[SerializeField] private Vector3 pos2d;
	[SerializeField] private Vector3 pos3d;

	public Transform player;

	public float timeTakenDuringLerp2D = 0.9f;
	public float timeTakenDuringLerp3D = 1f;
	private float timeStartedLerping;

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
		transform.LookAt (player);
		if (Input.GetButtonDown("Change Perspective"))
		{
			timeStartedLerping = Time.time;
			orthoOn = !orthoOn;
			if (orthoOn) {
				blender.BlendToMatrix (ortho, 1f);
//				Debug.Log ("2D");
			} else {
				blender.BlendToMatrix (perspective, 5f);
			}
		}
	}

	void FixedUpdate(){
		if (worldManager.mode2d) {
			if(transform.position != pos2d){
				float timeSinceStarted = Time.time - timeStartedLerping;
				float percentageComplete = timeSinceStarted / timeTakenDuringLerp2D;
				transform.localPosition = Vector3.Lerp(pos3d, pos2d, percentageComplete);
			}
		} else {
			if (transform.position != pos3d){
				float timeSinceStarted = Time.time - timeStartedLerping;
				float percentageComplete = timeSinceStarted / timeTakenDuringLerp3D;
				transform.localPosition = Vector3.Lerp(pos2d, pos3d, percentageComplete);
			}
		}
	}
}
