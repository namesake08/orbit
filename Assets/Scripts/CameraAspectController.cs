using UnityEngine;
using System.Collections;


namespace Assets.Scripts
{
    public class CameraAspectController : MonoBehaviour
    {

        public float fWidth = 1.5f;  // Desired width 

        void Start()
        {
            Camera cam = GetComponent<Camera>();
            //cam.orthographicSize = Screen.width / (2 * 145);
        }

        void OnGUI()
        {
            GUI.Label(new Rect(120, 120, 200, 200), "Screen width: " + Screen.width + "\nScreen height: " + Screen.height);
        }

    }
}