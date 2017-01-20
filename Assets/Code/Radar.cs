using UnityEngine;
using System.Collections.Generic;

public class Radar : MonoBehaviour {

	// Prefab de las casillas
	public GameObject squarePrefab;

	// Prefab de los enemigos
	public GameObject enemyPrefab;

	// Tamaño del tablero de enemigos
	public int size;

	// Radio del ping
	public float pingRadius;

	// Tablero de enemigos
	private List<Enemy> enemies;
	private Transform enemiesParent;

	// Referencia a las casillas
	private Square[,] squares;
	private Transform squaresParent;

	// Referencia al transform del objeto
	private Transform _transform;

	private void Awake() {
		_transform = transform;
	}

	private void Start() {
		// Inicializa el tablero de enemigos
		enemies = new List<Enemy>();
		enemiesParent = new GameObject("Enemies").transform;
		enemiesParent.SetParent(_transform, false);

		// TEMP: Por ahora hay enemigos en cada sitio
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				// Crea el enemigo
				GameObject enemy = Instantiate(enemyPrefab, enemiesParent);
				enemies[i, j] = enemy.GetComponent<Enemy>();

				// Coloca el enemigo en su sitio (asume tamaño 1)
				Transform enemyTransform = enemy.transform;
				enemyTransform.localPosition = new Vector3(i + 0.5f - size / 2.0f, j + 0.5f - size / 2.0f, 0.0f);
				enemyTransform.localScale = enemyTransform.lossyScale;
			}
		}

		// Crea las casillas
		squares = new Square[size, size];
		squaresParent = new GameObject("Squares").transform;
		squaresParent.SetParent(_transform, false);
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				// Crea la casilla
				GameObject square = Instantiate(squarePrefab, squaresParent);
				squares[i, j] = square.GetComponent<Square>();
				squares[i, j].Initialize(this, i, j);

				// Coloca la casilla en su sitio (asume tamaño 1)
				Transform squareTransform = square.transform;
				squareTransform.localPosition = new Vector3(i + 0.5f - size / 2.0f, j + 0.5f - size / 2.0f, 0.0f);
				squareTransform.localScale = squareTransform.lossyScale;
			}
		}
	}

	public Enemy GetEnemy(int x, int y) {
		if (!IsInsideGrid(x, y))
			return null;
		else
			return enemies[x, y];
	}

	public bool IsInsideGrid(int x, int y) {
		return !(x < 0 || x >= size || y < 0 || y >= size);
	}

	public void Ping(int x, int y, float radius) {
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				// Mira si hay enemigo en la casilla
				Enemy enemy = GetEnemy(i, j);
				if (enemy == null)
					continue;
				
				// Mira si la distancia es adecuada
				Vector3 distance = new Vector2(x, y) - new Vector2(i, j);
				if (distance.sqrMagnitude <= radius * radius)
					enemy.Show();
			}
		}
	}
}
