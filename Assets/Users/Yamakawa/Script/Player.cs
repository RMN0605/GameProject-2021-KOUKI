using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //---public-------------------------------------------------------    
    //インスペクターで設定する
    public GameObject wind;

    public int add_wind_power = 2300;
    public float diagonal = 0.2f;
    public bool practice = false;
    //---private------------------------------------------------------
    private Wind script;
    private CapsuleCollider2D col = null;
    private Rigidbody2D rb = null;
    private float WPx = 0;
    private float WPy = 0;
    private float g = 9.81f;
    private float v = 1;
    static public bool diagonal_frag = false;
    [SerializeField]
    private Animator Anime;
    [SerializeField]
    private GameObject apex_chanpion;
    private bool deathAnimFlag;
    private int loop = 0;
    private bool dotikatika = true;


    void Start()
    {
        Time.timeScale = 1f;
        diagonal_frag = false;
        col = GetComponent<CapsuleCollider2D>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();


        script = wind.GetComponent<Wind>();


        //WPx = 0.0f;
        //WPy = 0.0f;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (practice == false)
        {
            if (GameManager.gameOver)
            {
                if (!deathAnimFlag)
                {
                    Vector2 frc = new Vector2(0.0f, 2000.0f);
                    rb.AddForce(frc, ForceMode2D.Force);
                    deathAnimFlag = true;
                }
                Anime.SetBool("IsDeath", false);
                //rb.velocity = new Vector2(0, -g);
                col.enabled = false;

            }
        }

        if (practice == true)
        {
            if (GameManager.practiceGameOver)
            {
                if (dotikatika)
                {
                    StartCoroutine(tikatika());
                }
            }
        }
        rb.gravityScale = g;
            rb.drag = v;

            WPx = script.WP_x;
            WPy = script.WP_y;
            Vector3 ash = apex_chanpion.transform.GetChild(RopeController.apex).gameObject.transform.position;

            Vector2 force = new Vector2(WPx * add_wind_power, 0.0f) * Time.deltaTime;    // 力を設定
            rb.AddForce(force, ForceMode2D.Force);

            if (this.transform.position.y < 0 && ash.x < this.transform.position.x && ash.y > -2 && diagonal_frag == true)
            {
                Vector2 diagonal_force = new Vector2(-diagonal * add_wind_power, 0.0f);
                rb.AddForce(diagonal_force, ForceMode2D.Force);
            }

            if (this.transform.position.y < 0 && ash.x > this.transform.position.x && ash.y > -2 && diagonal_frag == true)
            {
                Vector2 diagonal_force = new Vector2(diagonal * add_wind_power, 0.0f);
                rb.AddForce(diagonal_force, ForceMode2D.Force);

                diagonal_frag = false;
            }

            g = (9.81f * (1 - WPy));
            //Debug.Log(g);
            v = (1 * (1 + WPy));
            //Debug.Log(v);
            //    // -----------------
            //    // 境界外判定
            //    // -----------------
            //    // 画面の左下の座標を取得 (左上じゃないので注意)
            //    Vector2 screen_LeftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
            //    // 画面の右上の座標を取得 (右下じゃないので注意)
            //    Vector2 screen_RightTop = Camera.main.ScreenToWorldPoint(
            //        new Vector3(Screen.width, Screen.height, 0)
            //        );
            //    // 現在のプレイヤーの移動情報(向きと強さ)
            //    Vector2 player_velocity = rb.velocity;
            //    // 現在のプレイヤーの位置座標
            //    Vector2 player_pos = transform.position;
            //    // 画面左端に達した時、プレイヤーが左方向に動いていたら、右方向の力に反転する
            //    if ((player_pos.x < screen_LeftBottom.x) && (player_velocity.x < 0))
            //        player_velocity.x *= -1;
            //    // 画面右端に達した時、プレイヤーが右方向に動いていたら、左方向の力に反転する
            //    if ((player_pos.x > screen_RightTop.x) && (player_velocity.x > 0))
            //        player_velocity.x *= -1;
            //    // 画面上端に達した時、プレイヤーが上方向に動いていたら、下方向の力に反転する
            //    if ((player_pos.y > screen_RightTop.y) && (player_velocity.y > 0))
            //        player_velocity.y *= -1;
            //    // 更新
            //    rb.velocity = player_velocity;
            //}


        

    }

    IEnumerator tikatika()
    {
        dotikatika = false;

        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.2f);

        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.2f); 

        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.2f); 

        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.2f); 

        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.2f);

        dotikatika = true;

        GameManager.practiceGameOver = false;
    }
} 