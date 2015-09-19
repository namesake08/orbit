using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts
{
    public class Clocks : CachedBehaviour
    {
        public event Action OnTick;

        private float _angle = 0;
        private float _currentSeconds;

        [Range(0.1f, 100)]
        public float MaxSeconds = 1f;
        
        public float CurrentSeconds
        {
            get { return _currentSeconds; }
            set 
            {
                if (value > MaxSeconds || value < 0)
                    _currentSeconds = 0;
                else
                    _currentSeconds = value;
            }
        }

        public Transform _arrow;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (CurrentSeconds == 0 && OnTick != null)
                OnTick();

            CurrentSeconds += Time.deltaTime;
            _angle = (CurrentSeconds / MaxSeconds) * 2 * -Mathf.PI;

            // Вращение стрелки
            _arrow.localRotation = Quaternion.Euler(0f, 0f, _angle.ToDegrees());

            // "Анти-вращение" относительно родителя
            if (transform.parent != null)
            {
                Quaternion q = transform.parent.rotation;
                transform.localRotation = new Quaternion(-q.x, -q.y, -q.z, q.w);
            }

        }
    }
}
