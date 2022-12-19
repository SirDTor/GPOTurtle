using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class ObjectOnMap : MonoBehaviour
{
    //общее для всех объектов время тика
    public static float stepTime = 0.25f;
    public Rigidbody body;
    public SphereCollider collider;

    protected void Start()
    {
        collider = GetComponent<SphereCollider>();
        body = GetComponent<Rigidbody>();
        body.isKinematic = true;
        collider.isTrigger = true;
        body.useGravity = false;
    }
}
