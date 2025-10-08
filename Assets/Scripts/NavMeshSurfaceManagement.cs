using NavMeshPlus.Components;
using UnityEngine;

public class NavMeshSurfaceManagement : MonoBehaviour
{

    public static NavMeshSurfaceManagement Instanse { get; private set; }

    private NavMeshSurface _navMeshSurface;


    private void Awake()
    {
        Instanse = this;
        _navMeshSurface = GetComponent<NavMeshSurface>();
        _navMeshSurface.hideEditorLogs = true;
    }


    public void RebakeNavmesheSurface()
    {
        _navMeshSurface.BuildNavMesh();
    }
}
