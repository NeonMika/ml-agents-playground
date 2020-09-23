using System.Collections.Generic;
using Attributes;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    [RequireComponent(typeof(TrailRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Paca : MonoBehaviour
    {
        public float HungerFull = 100f;
        public float MoveCostPerUnit = 1f;
        public float ViewIncreaseCostPerUnit = 1f;
        public float MaxMoveStepSize = 1f;
        public float ViewIncrease = 1f;
        public float ViewDecrease = 0.1f;
        public float IdleCost = 0.1f;

        public float LatestCost { get; private set; } = 0f;

        [NotNullField] public World World;
        [NotNullField] public GameObject TouchGO;
        [NotNullField] public GameObject SeeGO;
        [NotNullField] public Text Text;

        private TrailRenderer _trailRenderer;
        [HideInInspector] public Collider2D TouchCollider;
        [HideInInspector] public CircleCollider2D SeeCollider;

        private float _hunger;

        public float Hunger
        {
            get => _hunger;
            set
            {
                _hunger = value;
                //Text.text = _hunger.ToString();
            }
        }

        public List<Collider2D> CurrentlyTouching = new List<Collider2D>();
        public List<Collider2D> CurrentlySeeing = new List<Collider2D>();

        private void Awake()
        {
            _trailRenderer = GetComponent<TrailRenderer>();
            SeeCollider = SeeGO.GetComponent<CircleCollider2D>();
            SeeGO.GetComponent<ForwardOnCollision>().OnTriggerEnter2DForward +=
                coll =>
                {
                    if (coll.transform.IsChildOf(World.transform))
                    {
                        CurrentlySeeing.Add(coll);
                    }
                };
            SeeGO.GetComponent<ForwardOnCollision>().OnTriggerExit2DForward +=
                coll => { CurrentlySeeing.Remove(coll); };
            TouchCollider = TouchGO.GetComponent<Collider2D>();
            TouchGO.GetComponent<ForwardOnCollision>().OnTriggerEnter2DForward +=
                coll =>
                {
                    if (coll.transform.IsChildOf(World.transform))
                    {
                        CurrentlyTouching.Add(coll);
                    }
                };
            TouchGO.GetComponent<ForwardOnCollision>().OnTriggerExit2DForward +=
                coll => CurrentlyTouching.Remove(coll);
        }

        private void Start()
        {
            Reset();
        }

        public void Reset()
        {
            transform.localPosition = new Vector3(
                Random.Range(-World.Size.x / 2f * 0.9f, World.Size.x / 2f * 0.9f),
                Random.Range(-World.Size.y / 2f * 0.9f, World.Size.x / 2f * 0.9f),
                0f);
            Hunger = HungerFull;
            SeeCollider.radius = World.Size.magnitude / 6f;
            CurrentlySeeing.Clear();
            CurrentlyTouching.Clear();
            if (_trailRenderer != null)
            {
                _trailRenderer.Clear();
            }
        }

        public void Step(float dx, float dy, float visionIncrease)
        {
            LatestCost = IdleCost;
            if (visionIncrease >= 0f)
            {
                // View increase
                SeeCollider.radius += ViewIncrease;
                LatestCost += ViewIncrease * ViewIncreaseCostPerUnit;
            }
            else
            {
                // View decrease
                SeeCollider.radius -= ViewDecrease;

                // Move
                Vector3 offset = new Vector3(dx * MaxMoveStepSize, dy * MaxMoveStepSize, 0);
                float stepSize = offset.magnitude;
                LatestCost += stepSize * MoveCostPerUnit;
                transform.localPosition += offset;
            }

            Hunger -= LatestCost;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0f, 1f, 0f, 0.2f);
            GizmoHelper.DrawCircle(transform.position, SeeCollider.radius);

            foreach (Collider2D seen in CurrentlySeeing)
            {
                if (seen.GetComponent<Fence>() != null)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                }

                Gizmos.DrawLine(transform.position, seen.transform.position);
            }
        }
    }
}