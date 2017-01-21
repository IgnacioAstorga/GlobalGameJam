using UnityEngine;
using System.Collections.Generic;

public interface GameInterface 
{

    //esta funcion ejecuta el juego
    void GameStart();

    //se ejecuta al pausar
    void GamePause();

    //al perder todas las vidas
    void GameOver();

    //animacion antes de jugar
    void GameIntro();

    //cada frame
    void GameUpdate();

    
}
