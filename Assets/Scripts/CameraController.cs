using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    private Transform player;
    private float x_offs;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        x_offs = transform.position.x - player.position.x;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 v = new Vector3(player.position.x, player.position.y, -10f);
        transform.position = v;
	}
}
