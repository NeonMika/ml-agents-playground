using Attributes;
using DefaultNamespace;
using UnityEngine;

public class World : MonoBehaviour
{
    [NotNullField] public Fence FencePrefab;
    public Fence[] Fences = { };

    [NotNullField] public Paca PacaPrefab;
    public Paca[] Pacas;
    [NotNullField] public GameObject Room;
    [NotNullField] public Target TargetPrefab;
    public Target[] Targets = { };

    public Vector2 Size => new Vector2(Room.transform.localScale.x, Room.transform.localScale.y);

    // Start is called before the first frame update
    private void Awake()
    {
        Pacas = new[] {Instantiate(PacaPrefab, transform)};
        Pacas[0].World = this;
    }

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        foreach (Paca paca in Pacas) paca.Reset();

        foreach (Target target in Targets) Destroy(target.gameObject);

        var nNewTargets = (int) Random.Range(1f, 5.99f);
        Targets = new Target[nNewTargets];
        for (var i = 0; i < nNewTargets; i++)
        {
            Target newTarget = Instantiate(TargetPrefab, transform);
            newTarget.World = this;
            Targets[i] = newTarget;
            newTarget.Reset();
        }

        foreach (Fence fence in Fences) Destroy(fence.gameObject);

        var nNewFences = (int) Random.Range(1f, 5.99f);
        Fences = new Fence[nNewFences];
        for (var i = 0; i < nNewFences; i++)
        {
            Fence newFence = Instantiate(FencePrefab, transform);
            newFence.World = this;
            Fences[i] = newFence;
            newFence.Reset();
        }
    }
}