using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.GameLogic
{
    public class PlanetController : CachedBehaviour
    {

        #region FIELDS

        [SerializeField]
        private Transform _initAttachOrbit;

        private Transform _prevOrbitTransform;

        //[SerializeField]
        private Orbit _attachOrbit;

        [SerializeField]
        [Range(0.01f, 10)]
        private float _orbitingSpeedMultiplier = 1;

        private float _orbitingSpeed;

        [SerializeField]
        [Range(-1, 1)]
        private int _orbitingDirection = 1;

        private float _orbitingAngle;

        private bool _invisible;

        #endregion

        #region PROPERTIES

        public Orbit Orbit
        {
            get;
            private set;
        }

        public Planet Planet
        {
            get;
            private set;
        }

        /// <summary>
        /// Орбита, к которой планета привязана.
        /// </summary>
        public Orbit AttachOrbit
        {
            get { return _attachOrbit; }
            set 
            {
                OrbitingTime = 0;

                if (_attachOrbit != null)
                    _attachOrbit.AttachedPlanets.Remove(Planet);

                _attachOrbit = value;
                
                if (value != null)
                {
                    OrbitingAngle = MathHelper.AngleBetween(value.transform.position, transform.position);
                    if (value.AttachedPlanets != null)
                        value.AttachedPlanets.Add(Planet);
                }
            }
        }

        /// <summary>
        /// Направление движения по орбите 
        /// (1: по часовой стрелке; 
        ///  0: отсутствие движения;
        /// -1: против часовой стрелки)
        /// </summary>
        public int OrbitingDirection
        {
            get { return _orbitingDirection; }
            set { _orbitingDirection = Mathf.Clamp(value, -1, 1); }
        }

        /// <summary>
        /// Скорость движения по орбите (количество полных совершаемых кругов в секунду)
        /// </summary>
        public float OrbitingSpeed
        {
            get { return _orbitingSpeed; }
            set { _orbitingSpeed = value; }
        }

        /// <summary>
        /// Множитель скорости движения по орбите
        /// </summary>
        public float OrbitingSpeedMultiplier
        {
            get { return _orbitingSpeedMultiplier; }
            set { _orbitingSpeedMultiplier = value; }
        }

        /// <summary>
        /// Угол положения на орбите 
        /// </summary>
        public float OrbitingAngle
        {
            get { return _orbitingAngle; }
            set
            {
                if (value < -2 * Mathf.PI || value > 2 * Mathf.PI)
                    _orbitingAngle = 0;
                else
                    _orbitingAngle = value;
            }
        }

        /// <summary>
        /// Время нахождения на орбите
        /// </summary>
        public float OrbitingTime
        {
            get;
            private set;
        }

        /// <summary>
        /// Включает/выключает невидимость планеты
        /// </summary>
        public bool Invisible
        {
            get { return _invisible; }
            set 
            { 
                _invisible = value; 
                SetVisibility(); 
            }
        }

        #endregion


        /// <summary>
        /// Сменить направление движения на противоположное.
        /// </summary>
        public void SwitchDirection()
        {
            OrbitingDirection = -OrbitingDirection;
        }

        public void Destroy()
        {
            GameObject effect = Instantiate(GameValues.DestroyEffect);
            Vector3 v = transform.position; v.z -= 1f;
            effect.transform.position = v;
            //effect.transform.localScale *= transform.lossyScale.magnitude;
            ParticleSystem particleSystem = effect.GetComponent<ParticleSystem>();
            particleSystem.startSize *= transform.lossyScale.magnitude / 3f;
            

            Destroy(gameObject);
        }

        protected override void Awake()
        {
            base.Awake();

            Planet = GetComponentInChildren<Planet>();
            Orbit = GetComponentInChildren<Orbit>();

            InitAttachOrbit();

            Planet.OnTriggerEnterAction += OnPlanetTriggerEnter;
            Planet.OnTriggerExitAction += OnPlanetTriggerExit;
            Planet.OnTriggerStayAction += OnPlanetTriggerStay;
        }

        // Use this for initialization
        protected virtual void Start()
        {
            
        }

        
        // Update is called once per frame
        protected virtual void Update()
        {
            if (_initAttachOrbit != _prevOrbitTransform)
                InitAttachOrbit();
            _prevOrbitTransform = _initAttachOrbit;

        }

        protected virtual void FixedUpdate()
        {
            OrbitalMotion();
            SelfRotation();
        }

        protected virtual void OnPlanetTriggerEnter(Collider collider)
        {
            
        }

        protected virtual void OnPlanetTriggerExit(Collider collider)
        {

        }

        protected virtual void OnPlanetTriggerStay(Collider collider)
        {

        }

        private void InitAttachOrbit()
        {
            if (_initAttachOrbit == null)
                return;

            PlanetController controller = _initAttachOrbit.GetComponent<PlanetController>();
            if (controller != null)
            {
                AttachOrbit = controller.Orbit;
                return;
            }
            Orbit orbit = _initAttachOrbit.GetComponent<Orbit>();
            if (orbit != null)
            {
                AttachOrbit = orbit;
                return;
            }
            _initAttachOrbit = null;
        }

        private void SelfRotation()
        {
            float rand = UnityEngine.Random.Range(5.0f, 5.7f);

            Planet.transform.Rotate(new Vector3(0, 0, rand));

            //Orbit.transform.Rotate(new Vector3(0, 0, 5f));
        }

        private void OrbitalMotion()
        {
            if (AttachOrbit == null)
                return;

            OrbitingTime += Time.fixedDeltaTime;
            OrbitingSpeed = 0.5f / AttachOrbit.Radius;

            Vector3 orbitPos = AttachOrbit.transform.position;
            Vector3 targetPos = new Vector3(orbitPos.x, orbitPos.y, transform.position.z);
            float speed = (2 * Mathf.PI) * OrbitingSpeed * OrbitingSpeedMultiplier; //количество кругов, совершаемых в секунду

            OrbitingAngle += OrbitingDirection * speed * Time.deltaTime;
            targetPos.x += Mathf.Cos(OrbitingAngle) * AttachOrbit.Radius;
            targetPos.y += Mathf.Sin(OrbitingAngle) * AttachOrbit.Radius;

            transform.position = targetPos;
        }

        private void SetVisibility()
        {
            //Material m_p = Renderer.material;
            //Material m_o = SelfOrbit.Renderer.material;
            //Color c_p = m_p.color;
            //Color c_o = m_o.color;
            //if (Invisible)
            //    c_p.a = c_o.a = 0.5f;
            //else
            //    c_p.a = c_o.a = 1f;
            //m_p.color = c_p;
            //m_o.color = c_o;
            //Renderer.material = m_p;
            //SelfOrbit.Renderer.material = m_o;

            if (Invisible)
            {
                if (Planet != null)
                    Planet.Renderer.material = GameValues.InvisibleMaterial;
                if (Orbit != null)
                    Orbit.Renderer.material = GameValues.InvisibleMaterial;
            }
            else
            {
                if (Planet != null)
                    Planet.Renderer.material = Planet.StandardMaterial;
                if (Orbit != null)
                    Orbit.Renderer.material = Orbit.StandardMaterial;
            }

            //if (Planet != null)
            //    Planet.Collider.enabled = !Invisible;
            //if (Orbit != null)
            //    Orbit.Collider.enabled = !Invisible;
        }


    }


}