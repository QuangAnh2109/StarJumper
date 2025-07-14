using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyController : BaseDestroyController
{
    [SerializeField] private List<string> safeTags;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!safeTags.Contains(other.gameObject.tag))
        {
            Destroy(gameObject, destroyDelay);
        }
    }
}