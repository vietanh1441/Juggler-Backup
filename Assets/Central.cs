using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Central : MonoBehaviour {
    public Text text;
    private int timer = 3;
    public int max_ball = 3;
    private int shot_num = 0;
    private GameObject hand;
    public GameObject predict;
    public GameObject ball;
    private Vector2 pos;
    private int current_ball = 2;
   
    public GameObject cam;


    public int coin = 0;
	// Use this for initialization
	void Start () {
        hand = GameObject.FindGameObjectWithTag("Hand");
        bool supportsMultiTouch = Input.multiTouchEnabled;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        print("MultiTouchSupport : " + supportsMultiTouch);
        print(Input.touchCount);
        ETCInput.SetControlVisible("panic_left", false);

    }
	
    public void StartGame()
    {
        cam.SendMessage("GoToOrigin");
        //Disable the play button
        ETCInput.SetControlVisible("panic_left", true);
        ETCInput.SetControlVisible("Play", false);

    }

    //Pause Game
    public void PauseGame()
    {
        Time.timeScale = 0;
        cam.SendMessage("GoToPause");
        //Display Button
        ETCInput.SetControlVisible("Unpause", true);
        ETCInput.SetControlVisible("Music", true);
        ETCInput.SetControlVisible("Sound", true);

    }
    //UnPause Game

    public void UnPauseGame()
    {
        cam.SendMessage("GoToOrigin");
        timer = 3;
        Timer();
    }

    public void Timer()
    {
        if (timer != 0)
        {
            Invoke("Timer", 1);
            timer--;
        }
    }

    //Dead -> Restart

    public void Shoot()
    {
        shot_num++;
        //CheckBall(shot_num);
    }

    public void IncreaseCoin()
    {
        coin++;
        //TODO: save into playerpref;
    }

    void InitPos()
    {
        pos.y = 10;
        pos.x = Random.Range(-7, 7);
        ShowPos();
    }

    void ShowPos()
    {
        predict.transform.position = new Vector2(pos.x, 9);
     //   Instantiate(predict, new Vector2(pos.x, 7), Quaternion.identity);
    }

    //Make a new ball
     public void MakeBall()
    {
        //Add a maximum available ball available for player to choose. If 
        //number of current ball is equal to max, do not create another

        if (current_ball < max_ball)    //error checking
        {
            Instantiate(ball, pos, Quaternion.identity);
            UnShowPos();
            current_ball++;

            //if current_ball == max, then disable the button to make new ball
            if(current_ball >= max_ball)
            {
                ETCInput.SetControlVisible("panic_left", false);
               // ETCInput.SetControlActivated("panic_left", false);
                
            }
        }
    }

   void CheckBall(int num)
    {
        
        if(num == 1)
        {
            InitPos();
            
           
        }
        if(num==2)
        {
            MakeBall();
        }
        if (num > 2)
        {
            if (num % 5 == 0)
            {
                InitPos();
            }
            if (num % 5 == 1)
            {
                MakeBall();
            }
        }
    }
    
    void UnShowPos()
    {
        predict.transform.position = new Vector2(20, 20);
    }

	// Update is called once per frame
	void Update () {


        text.text = "touch Count: " + Input.touchCount;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
