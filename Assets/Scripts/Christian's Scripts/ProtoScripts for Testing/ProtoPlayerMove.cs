using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoPlayerMove : MonoBehaviour
{
    ///public
    public float moveSpeed = 2.5f;
    //getters
    public static int health { get { return _health; } }
    public static int score { get { return _score; } }

    ///private
    private static int _health;
    private static int _score;


    void Awake()
    {
        //initilize values
        _health = 100;
        _score = 0;
    }

    void Update()
    {
        //get inputs
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        //gather to Vector3
        Vector3 output = new Vector3(xInput, 0, yInput).normalized;
        //apply to position
        transform.position += output * moveSpeed * Time.deltaTime;
    }

    //take damage from Bullets
    //collect score from resources
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            //destroy object
            Destroy(other.transform.gameObject);
            //decrement health
            takeDamage(25);
            Debug.Log("Current health: " + health);
        }

        if (other.gameObject.tag == "Collectable")
        {
            //add score
            addScore(other.gameObject.GetComponent<ProtoCollectable>().scoreForCollection);
            Debug.Log("Current score: " + score);
            //destroy
            Destroy(other.transform.gameObject);
        }
    }

    //generic function to deduct health from the player while
    //checking if health has reached 0
    public void takeDamage(int damageTaken)
    {
        //damage player
        _health -= damageTaken;
        if (_health < 1)
        {
            _health = 0; //because negative health looks bad
            //send to GameOver Screen
            Debug.Log("GAME OVER");
            //call SceneManager to get the GameOverScene
            int gameOverInt = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings - 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(gameOverInt);
            DontDestroyOnLoad(GameObject.Find("ScriptManager"));
        }
    }

    public void addScore(int scoreToAdd)
    {
        _score += scoreToAdd;
    }
}
