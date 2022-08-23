using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    float step;
    Controls controls;
    Vector3 lastPos;
    bool growingPending;
public GameObject tailPrefab;
public GameObject foodPrefab;
public GameObject leftSide;
public GameObject rightSide;
public GameObject topSide;
public GameObject bottomSide;
public Text textoCount;
private int puntuacion = 0;

    public List<Transform> tail = new List<Transform>();
    enum Controls
    {
        up,
        down,
        left,
        right
    }

    private void Start()
    {
        step = GetComponent<SpriteRenderer>().bounds.size.x;
        StartCoroutine(MoveCoroutine());
        CreateFood();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)&& controls != Controls.left)
        {
            controls = Controls.right;
        }else if (Input.GetKeyDown(KeyCode.LeftArrow)&& controls != Controls.right)
        {
            controls = Controls.left;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && controls != Controls.down)
        {
            controls = Controls.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && controls != Controls.up)
        {
            controls = Controls.down;
        }
    }

    private void Move()
    {
        lastPos = transform.position;
        Vector3 nextPos = Vector3.zero;
        if (controls == Controls.left)
        {
            nextPos = Vector3.left;
        }
        else if (controls == Controls.right)
        {
            nextPos = Vector3.right;
        }
        else if (controls == Controls.up)
        {
            nextPos = Vector3.up;
        }
        else if (controls == Controls.down)
        {
            nextPos = Vector3.down;
        }
        transform.position += nextPos * step;
        MoveTail();
    }
    void MoveTail()
    {
        for (int i = 0; i < tail.Count; i++)
        {
            Vector3 temp = tail[i].position;
            tail[i].position = lastPos;
            lastPos = temp;
        }
        if (growingPending) CreateTail();
    }

    IEnumerator MoveCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            Move();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag= collision.gameObject.tag;
        if (collision.gameObject.tag == "Food")
        {
            print("colision");
            growingPending = true;
            Destroy(collision.gameObject);
            CreateFood();
        }
        else if (tag == "limit"||tag=="Tail")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        }
    void CreateTail()
    {
        GameObject newTail = Instantiate(tailPrefab, lastPos, Quaternion.identity);
        newTail.name = "Tail_" + tail.Count;
        tail.Add(newTail.transform);
        growingPending = false;
    }
    void CreateFood()
    {
        Vector2 pos = new Vector2(Random.Range(leftSide.transform.position.x, rightSide.transform.position.x),Random.Range(topSide.transform.position.y, bottomSide.transform.position.y));
        Instantiate(foodPrefab,pos, Quaternion.identity);
    }
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log (other.gameObject.tag);
        if (other.gameObject.tag == "food")
        {
            puntuacion = puntuacion + 1;
            textoCount.text = puntuacion.ToString();
        }
    }

}
