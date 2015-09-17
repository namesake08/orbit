using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class test : MonoBehaviour
    {
        [SerializeField]
        private float _sizeDifference;

        public void Start()
        {

        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.localScale *= _sizeDifference;
            }
        }

        public void Awake()
        {

        }

        public float SizeDifference
        {
            get { return _sizeDifference; }
            set { _sizeDifference = value; }
        }
    }
}
