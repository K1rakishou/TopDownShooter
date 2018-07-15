using UnityEngine;

public class GameZone : MonoBehaviour{
	private Mesh gameZoneMesh;
	public Vector3 upperRightGameZoneCorner { get; set; }
	public Vector3 bottomLeftGameZoneCorner { get; set; }

	// Use this for initialization
	void Start() {
		gameZoneMesh = GetComponent<MeshFilter>().mesh;

		upperRightGameZoneCorner = gameZoneMesh.bounds.max;
		bottomLeftGameZoneCorner = gameZoneMesh.bounds.min;
	}
}