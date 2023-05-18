using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public float speed;
    private Rigidbody rb;
    private Vector3 movement;
    public AudioClip hitWall, getItem;
    AudioSource audioSource;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public GameObject clearPanel;
    private TextMeshProUGUI clearText;
    [SerializeField] public int maxScore = 500;
    private int score;
    private int gameOverFlag = 0; //0 not 1 clear 2 over
    [SerializeField] public DataManager dataManager;



    void Start()
    {

        rb = GetComponent<Rigidbody>();
        clearText = clearPanel.transform.Find("ClearText").GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();

        movement = Vector3.zero;

        score = 0;
        scoreText.text = "Score:" + score.ToString();

        clearPanel.SetActive(false);

    }

    private void Update()
    {
        if (this.transform.position.y < -10)
            gameOverFlag = 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameOverFlag == 0)
            rb.AddForce(movement * speed);
        else
        {
            speed = 0;
            rb.velocity = Vector3.zero;
            if (gameOverFlag == 1)
                clearText.text = "Game Clear!";
            else if (gameOverFlag == 2)
                clearText.text = "Game Over!";

            clearPanel.SetActive(true);
        }



    }

    void OnMove(InputValue value)
    {
        Vector2 moveDirection = value.Get<Vector2>();
        movement = new Vector3(moveDirection.x, 0.0f, moveDirection.y);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            audioSource.PlayOneShot(hitWall);
            if (SceneController.gameMode == 0)
            {
                gameOverFlag = 0;
            }
            else if (SceneController.gameMode == 1)
            {
                gameOverFlag = 2;
            }
        }


    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("PickUp"))
        {
            audioSource.PlayOneShot(getItem);
            Destroy(other.gameObject);
            score += 100;
            scoreText.text = "Score:" + score.ToString();
            if (score >= maxScore)
            {
                gameOverFlag = 1;
            }
        }
        dataManager.UpdateData(score.ToString() + "," + DateTime.Now.ToString("yyyyMMddHHmmss"));
    }


}

/*using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;                    // ･ﾗ･・､･茫`､ﾎﾋﾙ､ｵ
    private Rigidbody rb;                                   // rigidbody
    private Vector3 movement;                               // ﾟMﾐﾐｷｽﾏ・

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movement = Vector3.zero;
    }

    void FixedUpdate()
    {
        rb.AddForce(movement * speed);                                        // ﾁｦ､ﾓ､ｨ､・
    }

    void OnMove(InputValue value)
    {
        Vector2 moveDirection = value.Get<Vector2>();                       // ･ｭｩ`ﾈ・ｦ､ﾎｷｽﾏ｡ｵﾃ
        movement = new Vector3(moveDirection.x, 0.0f, moveDirection.y);     // 3ｴﾎﾔｪ､ﾋ我轍
    }
    void OnTriggerEnter(Collider other)
    {
    }
    void OnCollisionEnter(Collision collision)
    {
    }
}*/