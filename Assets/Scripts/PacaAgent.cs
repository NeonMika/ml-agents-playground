using System;
using Attributes;
using DefaultNamespace;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

[RequireComponent(typeof(Paca))]
public class PacaAgent : Agent
{
    [HideInInspector] public Paca Paca;

    [ReadOnlyField] public float CumulativeReward = 0f;

    private EnvironmentParameters _resetParams;
    private StatsRecorder _statsRecorder;
    private int _foodLayer;

    public override void Initialize()
    {
        //Debug.Log("PacaAgent Initialize");
    }

    public override void OnEpisodeBegin()
    {
        // Debug.Log("PacaAgent OnEpisodeBegin");
        Paca = GetComponent<Paca>();
        _resetParams = Academy.Instance.EnvironmentParameters;
        _statsRecorder = Academy.Instance.StatsRecorder;
        _foodLayer = 1 << LayerMask.NameToLayer("Food");
        
        Paca.World.Reset();

        //Reset the parameters when the Agent is reset.
        SetResetParameters();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Paca.CurrentlySeeing.Sort((collider1, collider2) =>
        {
            float dist1 = (collider1.transform.position - transform.position).magnitude;
            float dist2 = (collider2.transform.position - transform.position).magnitude;
            if (dist1 < dist2)
            {
                return -1;
            }

            if (dist1 > dist2)
            {
                return 1;
            }

            return 0;
        });

        for (int i = 0; i < 5; i++)
        {
            if (Paca.CurrentlySeeing.Count > i)
            {
                Vector3 toTarget = Paca.CurrentlySeeing[i].transform.localPosition - transform.localPosition;
                if (Paca.CurrentlySeeing[i].GetComponent<Fence>() != null)
                {
                    // Fence
                    // Debug.Log("Fence");
                    sensor.AddObservation(1);
                    sensor.AddObservation(0);
                }
                else
                {
                    // Food
                    // Debug.Log("Food");
                    sensor.AddObservation(0);
                    sensor.AddObservation(1);
                }

                sensor.AddObservation(toTarget.x);
                sensor.AddObservation(toTarget.y);
            }
            else
            {
                sensor.AddObservation(0);
                sensor.AddObservation(0);
                sensor.AddObservation(0);
                sensor.AddObservation(0);
            }
        }
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        // Debug.Log("Action");

        // Get the action index for movement
        float dx = Mathf.Clamp(vectorAction[0], -1f, 1f);
        float dy = Mathf.Clamp(vectorAction[1], -1f, 1f);
        float visionIncrease = Mathf.Clamp(vectorAction[2], -1f, 1f);

        foreach (Fence fence in Paca.World.Fences)
        {
            if (fence.Collider.IsTouching(Paca.TouchCollider))
            {
                AddReward(-1f);
                EndEpisode();
                _statsRecorder.Add("FoodFound", 0);
                _statsRecorder.Add("FenceTouched", 1);
                _statsRecorder.Add("Starved", 0);
                return;
            }
        }

        Target touchedFood = null;
        foreach (Collider2D collider in Paca.CurrentlyTouching)
        {
            if (collider.GetComponent<Target>() != null)
            {
                touchedFood = collider.GetComponent<Target>();
                break;
            }
        }

        if (touchedFood != null)
        {
            AddReward(1f - Mathf.Lerp(0.5f, 0f, Paca.Hunger / Paca.HungerFull));
            _statsRecorder.Add("Hunger", Paca.Hunger);
            _statsRecorder.Add("FoodFound", 1);
            _statsRecorder.Add("FenceTouched", 0);
            _statsRecorder.Add("Starved", 0);
            EndEpisode();
            return;
        }

        if (Paca.Hunger <= 0f)
        {
            AddReward(-1f);
            _statsRecorder.Add("FoodFound", 0);
            _statsRecorder.Add("FenceTouched", 0);
            _statsRecorder.Add("Starved", 1);
            EndEpisode();
            return;
        }

        Paca.Step(dx, dy, visionIncrease);
        Paca.Text.text = "Reward: " + GetCumulativeReward().ToString("F2") + "\nHunger: " + Paca.Hunger.ToString("F2");
    }

    public void SetResetParameters()
    {
        //Set the attributes of the ball by fetching the information from the academy
        // TODO, see example below
        //m_BallRb.mass = m_ResetParams.GetWithDefault("mass", 1.0f);
        //var scale = m_ResetParams.GetWithDefault("scale", 1.0f);
        //ball.transform.localScale = new Vector3(scale, scale, scale);
    }
}