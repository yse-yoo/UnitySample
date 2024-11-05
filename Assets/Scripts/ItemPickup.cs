using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject pickupEffect; // エフェクトのプレハブをバインド

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        // プレイヤーのタグを "Player" としている場合
        if (other.CompareTag("Player"))
        {
            // エフェクトをアイテムの位置で生成
            Instantiate(pickupEffect, transform.position, Quaternion.identity);

            // アイテムを削除（拾ったことを示すため）
            Destroy(gameObject);
        }
    }
}
