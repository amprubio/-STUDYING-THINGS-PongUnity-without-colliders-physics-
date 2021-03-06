﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    //CONSTANTES
    [Header("CONSTANTES DEL MUNDO")]
    public const int ALTOMUNDO = 6, ANCHOMUNDO = 8;     
   
    private enum Estado { Start, Serve, play, Done };
    private enum Player { Left, Right };
 
    //inicio para determinar el primer saque P1score y P2score corresponden a las puntuaciones
    private int inicio = 0, p1score, p2score;

    private Estado est;
    private Player ServingPlayer;
   
    //Class Random from the System library to generate random ints
    private System.Random rnd = new System.Random();

    //Estructure of the singleton instance
    public static GameManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    //END of awake method

    void Start()
    {
        //decides who is the first player to serve

        while (inicio == 0)
        {
            inicio = rnd.Next(-1, 2);
        }
        
        if (inicio == -1)
        {
            ServingPlayer = Player.Left;
        }
        else if (inicio == 1)
        {
            ServingPlayer = Player.Right;
        }

        p1score = 0;
        p2score = 0;

        est = new Estado();
        est = Estado.Start;
    }

    void Update() {


        //si se pulsa enter en el estado start --> pasa a Serve, si se vuelve a pulsar --> estado play
        //despues cuando alguien pierde se vuelve al serve, hasta que uno de los jugadores llegue a 10 puntos, entonces pasaria a done

        if (Input.GetKey(KeyCode.Return) && est == Estado.Start)
        {
            est = Estado.Serve;
        }
        else if (Input.GetKey(KeyCode.Return) && est == Estado.Serve)
        {
            est = Estado.play;
        }

        if (Input.GetKey(KeyCode.Return) && est == Estado.Done) //with this you can replay the game
        {
            est = Estado.Start;
            p1score = 0;
            p2score = 0;
        }

        else if (Input.GetKey(KeyCode.Escape) && est==Estado.Done)
        {
            est = Estado.Start;
            p1score = 0;
            p2score = 0;
        }
        
    }

    //Añade puntos al P1
    public void AddPointsToP1()
    {
        p1score++;
        print("P1 points: " + p1score);
        print("P2 points: " + p2score);
        if(ServingPlayer==Player.Right)
            ChangePlayer(ref ServingPlayer);
        if (p1score < 10 && p2score < 10)
        {
            est = Estado.Serve;
        }
        else
        {
            est = Estado.Done;
            if (p1score == 10)
            {
                print("enhorabuena P1");
            }
            else
            {
                print("enhorabuena P2");
            }


            p1score = 0;
            p2score = 0;
        }
    }
    //Añade puntos al P2
    public void AddPointsToP2()
    {
        p2score++;

        if (ServingPlayer == Player.Left)
            ChangePlayer(ref ServingPlayer);

        print("P1 points: " + p1score);
        print("P2 points: " + p2score);

        if (p1score < 10 && p2score < 10)
        {
            est = Estado.Serve;
        }
        else
        {
            est = Estado.Done;
            if (p1score == 10)
            {
                print("enhorabuena P1");
            }
            else
            {
                print("enhorabuena P2");
            }
            p1score = 0;
            p2score = 0;
        }
    }
    //devuelve que el jugador sea Left o !Left(Right) para que en ballmanager se cambie la velocidad a -velocidad
    public bool LeftPlayer()
    {
        return ServingPlayer == Player.Left;
    }
    //comprueba el estado de juego que sea Play 
    public bool PlayState()
    {
        return est == Estado.play;
    }

    //pasa por referencia porque no se cambia en ningun metodo tipo update
    private void ChangePlayer(ref Player p)
    {
        if (p == Player.Left)
        {
            p = Player.Right;
        }

       else if (p == Player.Right)
        {
            p = Player.Left;
        }
    }
}
