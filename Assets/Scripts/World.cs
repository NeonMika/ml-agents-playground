using Attributes;
using DefaultNamespace;
using UnityEngine;

public class World : MonoBehaviour
{
    public Paca[] Pacas;
    public Target[] Targets = {};
    public Fence[] Fences = {};

    [NotNullField] public Paca PacaPrefab;
    [NotNullField] public Target TargetPrefab;
    [NotNullField] public Fence FencePrefab;
    [NotNullField] public GameObject Room;

    public Vector2 Size => new Vector2(Room.transform.localScale.x, Room.transform.localScale.y);

    // Start is called before the first frame update
    void Start()
    {
        Pacas = new[] {Instantiate(PacaPrefab, transform)};
        Pacas[0].World = this;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnValidate()
    {
    }

    public void Reset()
    {
        foreach (Paca paca in Pacas)
        {
            paca.Reset();
        }

        foreach (Target target in Targets)
        {
            Destroy(target.gameObject);
        }

        int nNewTargets = (int) Random.Range(1f, 5.99f);
        Targets = new Target[nNewTargets];
        for (int i = 0; i < nNewTargets; i++)
        {
            Target newTarget = Instantiate(TargetPrefab, transform);
            newTarget.World = this;
            Targets[i] = newTarget;
            newTarget.Reset();
        }
        
        foreach (Fence fence in Fences)
        {
            Destroy(fence.gameObject);
        }

        int nNewFences = (int) Random.Range(1f, 5.99f);
        Fences = new Fence[nNewFences];
        for (int i = 0; i < nNewFences; i++)
        {
            Fence newFence = Instantiate(FencePrefab, transform);
            newFence.World = this;
            Fences[i] = newFence;
            newFence.Reset();
        }
    }
}