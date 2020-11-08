using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortOrderStatic : MonoBehaviour
{
    public int offset;
    SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sortingOrder = (int)(-Mathf.Round(transform.position.y) + offset);
    }


}
