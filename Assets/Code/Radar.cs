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
				enemies.Add(enemy.GetComponent<Enemy>());

				// Coloca el enemigo en su sitio (asume tamaño 1)
				Transform enemyTransform = enemy.transform;
				enemyTransform.localPosition = GetWorldPosition(i, j);
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
				squareTransform.localPosition = GetWorldPosition(i, j);
				squareTransform.localScale = squareTransform.lossyScale;
			}
		}
	}

	public List<Enemy> GetEnemies(int x, int y) {
		if (!IsInsideGrid(x, y))
			return null;

		List<Enemy> enemiesInSquare = new List<Enemy>();
		foreach (Enemy enemy in enemies)
			if (enemy.IsInSquare(x, y))
				enemiesInSquare.Add(enemy);
		return enemiesInSquare;
	}

	public bool IsInsideGrid(int x, int y) {
		return !(x < 0 || x >= size || y < 0 || y >= size);
	}

	public void Ping(int x, int y, float radius) {
		foreach (Enemy enemy in enemies) {
			// Mira si la distancia es adecuada
			Vector3 distance = GetWorldPosition(x, y) - enemy.transform.localPosition;
			if (distance.sqrMagnitude <= radius * radius)
				enemy.Show();
		}
	}

	private Vector3 GetWorldPosition(int x, int y) {
		return new Vector3(x + 0.5f - size / 2.0f, y + 0.5f - size / 2.0f, 0.0f);
	}
}
