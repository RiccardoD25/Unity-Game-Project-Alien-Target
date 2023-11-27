
//////////code for MyGameManager/////
//////paste INSTEAD of existing code////////
///make sure the name of the script you are pasting into is EXACTLY MyGameManager//////

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public static class ApplicationData // available to all the scripts in this project.
{
    public static bool MakeNewBullet = false;
    public static bool MakeNewTarget = false;
    public static float BulletsFired;
    public static float BulletsMax; // number of shots available
    public static float BulletsHit;
    public static float BulletsMissed;
    public static float Percentage;
    public static float HighScore;
}
public class MyGameManager : MonoBehaviour
{
    // Reference to the Prefab/gameobject. Drag a Prefab into this field in the Inspector.
    public GameObject Bullet;
    public GameObject Target;
    public GameObject Launcher;
    public GameObject Lighting;
    public Text FiredDisplay;
    public Text HitDisplay;
    public Text MissedDisplay;
    public Text PercentDisplay;
    public Text MessageDisplay;
    public Text HighScoreDisplay;

    public GameObject ReplayButton;

    void Start() // will happen as soon as app starts
    {
        ApplicationData.BulletsFired = 0;
        ApplicationData.BulletsMax = 10;//IMPORTANT: bullets available in every round
        ApplicationData.BulletsHit = 0;
        ApplicationData.BulletsMissed = 0;
        ApplicationData.Percentage = 0;
        ///innitialize counters

        PercentDisplay.text = "Percentage: 0.00%";
        MessageDisplay.text = "";
        HighScoreDisplay.text = "Highest Score so far: " + ApplicationData.HighScore.ToString("P1");
        ///innitialize text display

        Instantiate(Bullet, Launcher.transform.position, Launcher.transform.rotation);//make a new, first bullet


        Instantiate(Target, new Vector3(UnityEngine.Random.Range(-15.0f, 15.0f), 0.5f, 7f), Quaternion.identity);
        //make a new target, random x from -15 to 15, y=0.5, z=7, and same rotation as world.
    }// end of start.

    // Update is called once per frame once per frame 60 times per sec.
    void Update()
    {



        FiredDisplay.text = "Bullets Fired: " + ApplicationData.BulletsFired.ToString() + " out of " + ApplicationData.BulletsMax.ToString();
        HitDisplay.text = "Bullets hit: " + ApplicationData.BulletsHit.ToString();
        ApplicationData.BulletsMissed = ApplicationData.BulletsFired - ApplicationData.BulletsHit;
        MissedDisplay.text = "Bullets missed: " + ApplicationData.BulletsMissed.ToString();

        //now we calculate percentage of success
        if (ApplicationData.BulletsHit > 0)
        { //so we don't devide by 0, but if it is 1 or more then divide.
            ApplicationData.Percentage = ApplicationData.BulletsHit / ApplicationData.BulletsFired;
            PercentDisplay.text = "Percentage: " + ApplicationData.Percentage.ToString("P1");
        }
        else // user did not hit anything yet.... Don't calculate, just put zeros.
        {
            ApplicationData.Percentage = 0;
            PercentDisplay.text = "Percentage: " + ApplicationData.Percentage.ToString("P1");
        }







        if (ApplicationData.BulletsFired < ApplicationData.BulletsMax)
        { //if there are still bullets left, game not over!!!

            ///update text display

            if (ApplicationData.MakeNewBullet == true)
            {//if something told us its time to instaciate a new bullet (a hit or a miss)....

                ApplicationData.MakeNewBullet = false;
                // makes sure it makes only one new bullet each time after fired.

                Instantiate(Bullet, Launcher.transform.position, Launcher.transform.rotation);
                ApplicationData.BulletsFired++;

            }
            if (ApplicationData.MakeNewTarget == true)
            {//if target is destroyed its time to instaciate a new target (prev one destroyed).
                ApplicationData.MakeNewTarget = false; // tell the application to up the score by 1.

                Instantiate(Target, new Vector3(UnityEngine.Random.Range(-15.0f, 15.0f), 0.5f, 7f), Quaternion.identity);//make a new target, random x from __ to __
                ApplicationData.BulletsHit++;
                print("add hit");

            }

        }
        else//no bullets left, game is over!
        {
            ApplicationData.MakeNewTarget = false;
            ApplicationData.MakeNewBullet = false;
            MessageDisplay.text = "GAME OVER!";

            ReplayButton.SetActive(true);//show replay button
            Lighting.SetActive(false);//turn off the lights

        }//end if not out of bullets
    }//end update

    public void MyReplay()// a function we will attach to the replay button, when clicked.
    {

        if (ApplicationData.Percentage > ApplicationData.HighScore)//if got a higher score than prev so far...
        {
            ApplicationData.HighScore = ApplicationData.Percentage; //make that the new high score
            // new king!!!
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//go to same scene we are on now, reload

        // print("replay");
    }//end MyReplay



    public void ExitGame()//a function to be called by the exit button
    {
        Application.Quit();
    }

}//class
