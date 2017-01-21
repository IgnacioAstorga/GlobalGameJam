using UnityEngine;
using System.Collections.Generic;

public interface GameInterface 
{

    //esta funcion ejecuta el juego
    void gameStart();

    //se ejecuta al pausar
    void gamePause();

    //al perder todas las vidas
    void gameOver();

    //animacion antes de jugar
    void gameIntro();

    //cada frame
    void gameUpdate();

    
}
