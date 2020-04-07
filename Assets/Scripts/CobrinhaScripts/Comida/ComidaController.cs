
using UnityEngine;

public class ComidaController : MonoBehaviour
{
    
    public float lifeTimeobject;
    void Start()
    {
        Invoke("finishObject",lifeTimeobject);
    }

    private void finishObject()
    {
        Destroy(gameObject);
    }
}
