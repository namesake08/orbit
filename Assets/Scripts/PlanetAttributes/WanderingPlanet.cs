using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;


namespace Assets.Scripts.PlanetAttributes
{
    public enum eOrientationMode { NODE = 0, TANGENT }

    [AddComponentMenu("Splines/Spline Controller")]
    [RequireComponent(typeof(SplineInterpolator))]
    public class WanderingPlanet : MonoBehaviour
    {

        #region FIELDS

        [SerializeField]
        private GameObject _splineRoot;
        [SerializeField]
        private float _duration = 10;
        [SerializeField]
        private eOrientationMode _orientationMode = eOrientationMode.NODE;
        [SerializeField]
        private eWrapMode _wrapMode = eWrapMode.LOOP;
        [SerializeField]
        private bool _autoStart = true;
        [SerializeField]
        private bool _autoClose = true;
        [SerializeField]
        private bool _hideOnExecute = true;
        #endregion

        #region PROPERTIES

        public GameObject SplineRoot
        {
            get { return _splineRoot; }
        }
        public float Duration
        {
            get { return _duration; }
        }
        public eOrientationMode OrientationMode
        {
            get { return _orientationMode; }
        }
        public eWrapMode WrapMode
        {
            get { return _wrapMode; }
        }
        public bool AutoStart
        {
            get { return _autoStart; }
        }
        public bool AutoClose
        {
            get { return _autoClose; }
        }
        public bool HideOnExecute
        {
            get { return _hideOnExecute; }
        }
        #endregion

        private List<Vector3> _splinePoints;
        private SplineInterpolator mSplineInterp;
        private Transform[] mTransforms;
        private LineRenderer _lineRenderer;

        void OnDrawGizmos()
        {
            if (SplineRoot == null)
                return;

            Transform[] trans = GetTransforms();
            if (trans.Length < 2)
                return;

            SplineInterpolator interp = GetComponent(typeof(SplineInterpolator)) as SplineInterpolator;
            SetupSplineInterpolator(interp, trans);
            interp.StartInterpolation(null, false, WrapMode);

            Vector3 prevPos = trans[0].position;
            for (int c = 1; c <= 100; c++)
            {
                float currTime = c * Duration / 100;
                Vector3 currPos = interp.GetHermiteAtTime(currTime);
                float mag = (currPos - prevPos).magnitude * 2;
                Gizmos.color = new Color(mag, 0, 0, 1);
                Gizmos.DrawLine(prevPos, currPos);
                prevPos = currPos;
            }
        }

        //public void OnRenderObject()
        //{

        //        //Material line_m = LineCreator.CreateLineMaterial();
        //        Material line_m = new Material(
        //         @"Shader ""Lines/Colored Blended"" {
        //             SubShader {
        //                 Tags { ""RenderType""=""Opaque"" }
        //                 Pass {
        //                     ZWrite On
        //                     ZTest LEqual
        //                     Cull Off
        //                     Fog { Mode Off }
        //                     BindChannels {
        //                         Bind ""vertex"", vertex Bind ""color"", color
        //                     }
        //                 }
        //             }
        //         }");
        //        line_m.SetPass(0);
        //        if (mTransforms.Length < 2)
        //            return;

        //        GL.PushMatrix();
        //        GL.Begin(GL.LINES);
        //        Vector3 prevPos = _splinePoints[0];
        //        for (int c = 1; c <= 100; c++)
        //        {
        //            Vector3 currPos = _splinePoints[c];
        //            GL.Color(new Color(0.1f, 0.1f, 0.1f, 0.5f));
        //            GL.Vertex3(prevPos.x, prevPos.y, prevPos.z);
        //            float angle = MathHelper.AngleBetween(currPos, prevPos);
        //            float dist = Vector3.Distance(currPos, prevPos);
        //            GL.Vertex3(currPos.x + dist/2 * Mathf.Cos(angle), currPos.y + dist/2 * Mathf.Sin(angle), currPos.z);
        //            prevPos = currPos;
        //        }
        //        GL.End();
        //        GL.PopMatrix();
        //}

        void Start()
        {

            mSplineInterp = GetComponent(typeof(SplineInterpolator)) as SplineInterpolator;

            mTransforms = GetTransforms();

            if (HideOnExecute)
                DisableTransforms();

            if (AutoStart)
                FollowSpline();

            PerformLineRenderer();
        }


        private void PerformLineRenderer()
        {
            _splinePoints = new List<Vector3>();
            SplineInterpolator interp = GetComponent(typeof(SplineInterpolator)) as SplineInterpolator;
            SetupSplineInterpolator(interp, mTransforms);
            interp.StartInterpolation(null, false, WrapMode);

            Vector3 prevPos = mTransforms[0].position;
            _splinePoints.Add(prevPos);
            for (int c = 1; c <= 100; c++)
            {
                float currTime = c * Duration / 100;
                Vector3 currPos = interp.GetHermiteAtTime(currTime);
                _splinePoints.Add(currPos);
                prevPos = currPos;
            }

            _lineRenderer = gameObject.AddComponent<LineRenderer>();
            _lineRenderer.material = GameValues.SplineLineMaterial;//new Material(Shader);//Shader.Find("Particles/Additive"));
            _lineRenderer.SetColors(new Color(1, 1, 1), new Color(1, 1, 1));
            _lineRenderer.SetWidth(0.2F, 0.2F);
            _lineRenderer.SetVertexCount(_splinePoints.Count);
            for (int i = 0; i < _splinePoints.Count; i++)
            {
                _lineRenderer.SetPosition(i, _splinePoints[i]);
            }
        }

        void SetupSplineInterpolator(SplineInterpolator interp, Transform[] trans)
        {
            interp.Reset();

            float step = (AutoClose) ? Duration / trans.Length :
                Duration / (trans.Length - 1);

            int c;
            for (c = 0; c < trans.Length; c++)
            {
                if (OrientationMode == eOrientationMode.NODE)
                {
                    interp.AddPoint(trans[c].position, trans[c].rotation, step * c, new Vector2(0, 1));
                }
                else if (OrientationMode == eOrientationMode.TANGENT)
                {
                    Quaternion rot;
                    if (c != trans.Length - 1)
                        rot = Quaternion.LookRotation(trans[c + 1].position - trans[c].position, trans[c].up);
                    else if (AutoClose)
                        rot = Quaternion.LookRotation(trans[0].position - trans[c].position, trans[c].up);
                    else
                        rot = trans[c].rotation;

                    interp.AddPoint(trans[c].position, rot, step * c, new Vector2(0, 1));
                }
            }

            if (AutoClose)
                interp.SetAutoCloseMode(step * c);
        }


        /// <summary>
        /// Returns children transforms, sorted by name.
        /// </summary>
        Transform[] GetTransforms()
        {
            if (SplineRoot != null)
            {
                List<Component> components = new List<Component>(SplineRoot.GetComponentsInChildren(typeof(Transform)));
                List<Transform> transforms = components.ConvertAll(c => (Transform)c);

                transforms.Remove(SplineRoot.transform);
                transforms.Sort(delegate(Transform a, Transform b)
                {
                    return a.name.CompareTo(b.name);
                });

                return transforms.ToArray();
            }

            return null;
        }

        /// <summary>
        /// Disables the spline objects, we don't need them outside design-time.
        /// </summary>
        void DisableTransforms()
        {
            if (SplineRoot != null)
            {
                SplineRoot.SetActiveRecursively(false);
            }
        }


        /// <summary>
        /// Starts the interpolation
        /// </summary>
        void FollowSpline()
        {
            if (mTransforms.Length > 0)
            {
                SetupSplineInterpolator(mSplineInterp, mTransforms);
                mSplineInterp.StartInterpolation(null, true, WrapMode);
            }
        }
    }
}