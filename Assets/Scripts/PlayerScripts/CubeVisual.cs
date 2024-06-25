using System.Threading.Tasks;
using UnityEngine;

public class CubeVisual : MonoBehaviour
{
    public float moveDistance = 1.0f;
    private void OnTriggerEnter(Collider other)
    {
        transform.position = new Vector3(transform.position.x, other.transform.position.y + moveDistance, transform.position.z);
    }
    private void OnTriggerExit(Collider other)
    {
        Wait(() => transform.position = new Vector3(transform.position.x, transform.position.y - moveDistance, transform.position.z));
    }
    private async void Wait(System.Action action)
    {
        await Task.Delay(100);
        action.Invoke();
    }
}
