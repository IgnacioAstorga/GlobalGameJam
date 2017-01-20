using UnityEngine;
using System.Collections.Generic;

public class Radar : MonoBehaviour {

	// Prefab de las casillas
	public GameObject squarePrefab;

	// Prefab de los enemigos
	public GameObject enemyPrefab;

	// Tamaño del tablero de enemigos
	public int size;

	// Si activado, el tablero será circular
	public bool rounded;
	public bool roundedSpawn;

	// Radio del ping
	public float pingRadius;

	// Tiempo entre cada aparición de enemigo
	public float spawnTime;

	// Tablero de enemigos
	private List<Enemy> enemies;
	private Transform enemiesParent;

	// Referencia a las casillas
	private Square[,] squares;
	private Transform squaresParent;

	// Referencia al transform del objeto
	private Transform _transform;

	// Remaining time for the next enemy to spawn
	private float _timeRemaining = -1.0f;

	private void Awake() {
		_transform = transform;
	}

	private void Start() {
		// Inicializa el tablero de enemigos
		enemies = new List<Enemy>();
		enemiesParent = new GameObject("Enemies").transform;
		enemiesParent.SetParent(_transform, false);

		// Crea las casillas
		squares = new Square[size, size];
		squaresParent = new GameObject("Squares").transform;
		squaresParent.SetParent(_transform, false);
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < size; j++) {
				Vector3 pos = GetWorldPosition(i, j);
				if (rounded && pos.sqrMagnitude > size * size / 4.0f)
					continue;

				// Crea la casilla
				GameObject square = Instantiate(squarePrefab, squaresParent);
				squares[i, j] = square.GetComponent<Square>();
				squares[i, j].Initialize(this, i, j);

				// Coloca la casilla en su sitio (asume tamaño 1)
				Transform squareTransform = square.transform;
				squareTransform.localPosition = pos;
				squareTransform.localScale = squareTransform.lossyScale;
			}
		}
	}

	private void Update() {
		_timeRemaining -= Time.deltaTime;
		if (_timeRemaining < 0.0f) {
			// Reinicia el contador
			_timeRemaining = spawnTime;

			// Selecciona una casilla aleatoria para aparecer
			if (!roundedSpawn) {
				int side = Random.Range(0, 4);
				int index = Random.Range(0, size);
				switch (side) {
					case 0:
						SpawnEnemy(index, 0);
						break;
					case 1:
						SpawnEnemy(index, size - 1);
						break;
					case 2:
						SpawnEnemy(0, index);
						break;
					case 3:
						SpawnEnemy(size - 1, index);
						break;
				}
			}
			else {
				float spawnAngle = Random.Range(0.0f, 2.0f * Mathf.PI);
				Vector3 position = new Vector3(Mathf.Cos(spawnAngle), Mathf.Sin(spawnAngle), 0.0f) * size / 2.0f;
				SpawnEnemy(position);
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
		return x >= 0 && x < size && y >= 0 && y < size;
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

	public void SpawnEnemy(int x, int y) {
		// Crea el enemigo y lo emparenta
		GameObject enemy = Instantiate(enemyPrefab, enemiesParent);
		enemies.Add(enemy.GetComponent<Enemy>());

		// Coloca el enemigo en su sitio (asume tamaño 1)
		Transform enemyTransform = enemy.transform;
		enemyTransform.localPosition = GetWorldPosition(x, y);
		enemyTransform.localScale = enemyTransform.lossyScale;
	}

	public void SpawnEnemy(Vector3 position) {
		// Crea el enemigo y lo emparenta
		GameObject enemy = Instantiate(enemyPrefab, enemiesParent);
		enemies.Add(enemy.GetComponent<Enemy>());

		// Coloca el enemigo en su sitio (asume tamaño 1)
		Transform enemyTransform = enemy.transform;
		enemyTransform.localPosition = position;
		enemyTransform.localScale = enemyTransform.lossyScale;
	}
}
