using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SortOrderActive : MonoBehaviour
{

    SpriteRenderer sr;
    public int offset;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sr.sortingOrder = (int)(-Mathf.Round(transform.parent.transform.position.y)+offset);
    }
}
