using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandpaController : MonoBehaviour
{
    public static GrandpaController Instance;
    [Header( "Movement Settings" )]
    public float moveSpeed = 5f; // 怪物移动速度
    public Transform player; // 玩家 Transform
    private bool isChasing = false; // 是否正在追逐玩家

    [Header( "Effects" )]
    public AudioSource chaseAudio; // 追逐音效
    public GameObject chaseUIEffect; // 追逐 UI 效果

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (isChasing) {
            ChasePlayer(); // 追逐玩家
        }
    }

    public void StartChasing() {
        isChasing = true;
        chaseAudio.Play();
        chaseUIEffect.SetActive( true );
    }

    public void StopChasing() {
        isChasing = false;
        chaseAudio.Stop();
        chaseUIEffect.SetActive( false );
    }

    private void ChasePlayer() {
        Vector3 direction = ( player.position - transform.position ).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
