using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteThisThing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeleteThis());
    }

    IEnumerator DeleteThis()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
