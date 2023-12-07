using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicNeeds : MonoBehaviour
{
    
    //popups when game loses
    public GameObject loseHealth_popup;
    public GameObject loseHunger_popup;

    //pops up when game is won
    public GameObject win_popup;

    //placeholder background for popup
    public GameObject background;

    //player stat bars
    public Image hunger_bar_foreground;
    public Image health_bar_foreground;

    //stat bar amounts
    public static float hunger_remaining;
    public static float health_remaining;
    public float hunger_max = 60.0f;
    public float health_max = 60.0f;

    //checkers
    public static bool is_dead = false;
    public static bool win_check = false;

    //time win condition counter
    public float time_count = 0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        hunger_remaining = hunger_max;
        health_remaining = health_max;
    }

    void Update()
    {
        //decrease hunger 
        hunger_remaining -= Time.deltaTime;
        //count how long has passed
        time_count += Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //determine fill amount of bars while game has not been won
        if(hunger_remaining > 0 && win_check == false)
        {
            hunger_bar_foreground.fillAmount = hunger_remaining / hunger_max;
            health_bar_foreground.fillAmount = health_remaining / hunger_max;
        }

        //display lose popup if hunger runs out and empty health bar
        else if (hunger_remaining < 0 && win_check == false && is_dead == false){
            is_dead = true;
            loseHunger_popup.SetActive(true);
            health_bar_foreground.gameObject.SetActive(false);
            background.SetActive(true);
        }

        //display lose popup if health runs out and empty hunger bar
        if(health_remaining < 0 && is_dead == false && win_check == false)
        {
            is_dead = true;
            loseHealth_popup.SetActive(true);
            hunger_bar_foreground.gameObject.SetActive(false);
            background.SetActive(true);
        }
        //win after certain time if health and hunger are not depleated
        if(hunger_remaining > 0 && health_remaining > 0 && time_count >= 60.0f){
            win_check = true;
            EnemyController.enableWalk = false;
            win_popup.SetActive(true);
            background.SetActive(true);

        }
    }
}
