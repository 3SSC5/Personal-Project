using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PP
{
    public class MeleeAttackManager : MonoBehaviour
    {
        //How much the player should move either downwards or horizontally when melee attack collides with a GameObject that has EnemyHealth script on it
        public float defaultForce = 300;
        //How much the player should move upwards when melee attack collides with a GameObject that has EnemyHealth script on it
        public float upwardsForce = 600;
        //How long the player should move when melee attack collides with a GameObject that has EnemyHealth script on it
        public float movementTime = .1f;
        //Input detection to see if the button to perform a melee attack has been pressed
        private bool meleeAttack;
        //The animator on the meleePrefab
        public Animator meleeAnimator;

        private Ground ground;


        private Animator anim;

        private void Start()
        {
            //The Animator component on the player
            anim = GetComponent<Animator>();
            //The animator on the meleePrefab
            ground = GetComponent<Ground>();
        }

        private void Awake()
        {
            ground = GetComponent<Ground>();
        }

        private void Update()
        {
            //Method that checks to see what keys are being pressed
            CheckInput();
        }

        private void CheckInput()
        {
            //Checks to see if Backspace key is pressed which I define as melee attack; you can of course change this to anything you would want
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                //Sets the meleeAttack bool to true
                meleeAttack = true;
            }
            else
            {
                //Turns off the meleeAttack bool
                meleeAttack = false;
            }
            //Checks to see if meleeAttack is true and pressing up
            if (meleeAttack && Input.GetAxis("Vertical") > 0)
            {
                //Turns on the animation for the player to perform an upward melee attack
                anim.SetTrigger("UpwardMelee");
                //Turns on the animation on the melee weapon to show the swipe area for the melee attack upwards
                meleeAnimator.SetTrigger("UpwardMeleeSwipe");
            }
            //Checks to see if meleeAttack is true and pressing down while also not grounded
            if (meleeAttack && Input.GetAxis("Vertical") < 0 && !ground.onGround)
            {
                //Turns on the animation for the player to perform a downward melee attack
                anim.SetTrigger("DownwardMelee");
                //Turns on the animation on the melee weapon to show the swipe area for the melee attack downwards
                meleeAnimator.SetTrigger("DownwardMeleeSwipe");
            }
            //Checks to see if meleeAttack is true and not pressing any direction
            if ((meleeAttack && Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") < 0)
                //OR if melee attack is true and pressing down while grounded
                || meleeAttack && (Input.GetAxis("Vertical") < 0 && ground.onGround))
            {
                //Turns on the animation for the player to perform a forward melee attack
                anim.SetTrigger("LeftMelee");
                //Turns on the animation on the melee weapon to show the swipe area for the melee attack forwards
                meleeAnimator.SetTrigger("LeftMelee");
            }

            if ((meleeAttack && Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") > 0)
                //OR if melee attack is true and pressing down while grounded
                || meleeAttack && (Input.GetAxis("Vertical") < 0 && ground.onGround))
            {
                //Turns on the animation for the player to perform a forward melee attack
                anim.SetTrigger("RightMelee");
                //Turns on the animation on the melee weapon to show the swipe area for the melee attack forwards
                meleeAnimator.SetTrigger("RightMelee");
            }
        }
    }
}