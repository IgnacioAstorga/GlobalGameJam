using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    //vidas del jugador
    public int lives;

    //array de torretas
    public List<Turret> turrets = new List<Turret>();

    //array de switch
    public List<Switch> switchesH = new List<Switch>();
    
    //radar
    public Radar radar;

    //array de enemigos
    public List<Enemy> enemies = new List<Enemy>();


    private void Start()
    {
        lives = 10;


    }
}

