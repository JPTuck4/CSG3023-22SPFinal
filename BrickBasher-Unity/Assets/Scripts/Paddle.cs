/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: JP
 * Last Edited: 4/28
 * 
 * Description: Paddle controler on Horizontal Axis
****/

/*** Using Namespaces ***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10; //speed of paddle


    // Update is called once per frame
    void Update()
    {
        var pos = this.GetComponent<Transform>().position; //get current position
        pos.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime; //change position based on input
        this.GetComponent<Transform>().position = pos; //update position
    }//end Update()
}
