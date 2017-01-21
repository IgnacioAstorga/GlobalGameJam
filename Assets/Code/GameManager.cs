using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    //vidas del jugador
    public int lives;

    //array de torretas
    public List<Turret> turrets = new List<Turret>();

    //array de switchH
    public List<SwitchH> switchesH = new List<SwitchH>();

    //array de switchV
    public List<SwitchV> switchesV = new List<SwitchV>();

    //radar
    public Radar radar;

    //array de enemigos
    public List<Enemy> enemies = new List<Enemy>();


    private void Start()
    {
        lives = 10;


    }
}

