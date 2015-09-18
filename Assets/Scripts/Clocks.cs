using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts
{
    public class Clocks : CachedBehaviour
    {
        private const float secondsToDegrees = 360f / 60f;
        private float _time = 0;
        private float _angle = 0;
        public float Speed = 2 * Mathf.PI;

        public Transform _arrow;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            _angle -= (Speed * Time.deltaTime).ToDegrees();
            if (_angle < 0)
                _angle = 360;

            // Вращение стрелки
            _arrow.localRotation = Quaternion.Euler(0f, 0f, _angle);

            // "Анти-вращение" относительно родителя
            if (transform.parent != null)
            {
                Quaternion q = transform.parent.rotation;
                transform.localRotation = new Quaternion(-q.x, -q.y, -q.z, q.w);
            }

        }
    }
}
