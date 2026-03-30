using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject leftBound;

    [SerializeField]
    private GameObject righBound;

    [SerializeField]
    private GameObject dropYLocationTop;

    [SerializeField]
    private GameObject dropYLocationBottom;

    [SerializeField]
    private GameObject[] drops;

    [SerializeField]
    private Sprite UFOActive;

    [SerializeField]
    private Sprite UFOInactive;

    [SerializeField]
    private SpriteRenderer UFO;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    private GameObject heldDrop = null;
    private GameObject spawnParent;
    private GameObject currentDropY;

    bool onTop = true;

    int NumDropped = 0;

    private bool paused = false;

    private void Start()
    {
        spawnParent = GameObject.Find("Spawn Parent");
        currentDropY = dropYLocationTop;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        paused = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;

            if (paused)
            {
                pauseMenu.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(false);
            }
        }

        if (paused) return;

        if (heldDrop == null)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f; 

            mouseWorldPos.y = currentDropY.transform.position.y;

            mouseWorldPos.x = Mathf.Clamp(mouseWorldPos.x, leftBound.transform.position.x, righBound.transform.position.x);

            heldDrop = Instantiate(
                drops[Random.Range(0, drops.Length)],
                mouseWorldPos,
                Quaternion.identity,
                spawnParent.transform
            );
        }
        else
        {

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f; 

            mouseWorldPos.y = currentDropY.transform.position.y;

            mouseWorldPos.x = Mathf.Clamp(mouseWorldPos.x, leftBound.transform.position.x, righBound.transform.position.x);

            heldDrop.transform.localPosition = mouseWorldPos;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                heldDrop.GetComponent<Rigidbody2D>().gravityScale = 1;
                heldDrop.GetComponent<Collider2D>().enabled = true;
                

                NumDropped++;

                scoreText.text = "Score:\n" + NumDropped;

                if(NumDropped % 10 == 0)
                {
                    Physics2D.gravity -= Physics2D.gravity * 2;

                    if(onTop)
                    {
                        currentDropY = dropYLocationBottom;
                    }
                    else
                    {
                        currentDropY = dropYLocationTop;
                    }

                    UFO.sprite = UFOActive;
                    
                    onTop = !onTop;
                }
                else
                {
                    UFO.sprite = UFOInactive;
                }
                heldDrop = null;
            }
        }
    }

}
