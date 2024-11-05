using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;           // 追従するプレイヤーの位置
    public float maxSpeed = 3.0f;      // 最大速度
    public float minSpeed = 0.5f;      // 最小速度（近づくと低下）
    public float approachDistance = 5.0f; // プレイヤーに近づき始める距離
    public float attackDistance = 1.5f;   // 攻撃を開始する距離
    public float smoothTime = 0.3f;    // 速度が変わる滑らかさ
    public float attackCooldown = 2.0f; // 攻撃のクールダウン時間

    private float currentSpeed = 0.0f;  // 現在の速度
    private Rigidbody rb;               // Rigidbodyの参照
    private bool canAttack = true;      // 攻撃可能かどうかのフラグ
    private float lastAttackTime = 0.0f; // 最後の攻撃時間

    void Start()
    {
        // Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("Playerが設定されていません！");
            return;
        }

        // プレイヤーとの距離を計算
        float distance = Vector3.Distance(transform.position, player.position);

        // 攻撃範囲内にいるかどうかチェック
        if (distance <= attackDistance && canAttack)
        {
            Attack();
            return; // 攻撃中は移動しない
        }

        // 距離に応じて速度を調整（近づくと速度が低くなる）
        float targetSpeed = (distance < approachDistance) ? minSpeed : maxSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, smoothTime * Time.fixedDeltaTime);

        // プレイヤーへの方向を計算
        Vector3 direction = (player.position - transform.position).normalized;

        // 敵をプレイヤーに向かって移動
        rb.MovePosition(transform.position + direction * currentSpeed * Time.fixedDeltaTime);

        // プレイヤーの方を向く
        transform.LookAt(player);
    }

    void Attack()
    {
        // 攻撃処理をここに記述
        Debug.Log("攻撃！");

        // クールダウンを設定
        canAttack = false;
        lastAttackTime = Time.time;

        // 一定時間後に攻撃可能にするコルーチンを開始
        Invoke("ResetAttack", attackCooldown);
    }

    void ResetAttack()
    {
        canAttack = true;
    }
}
