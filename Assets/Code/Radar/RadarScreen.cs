using UnityEngine;
using System.Collections.Generic;

public class RadarScreen : MonoBehaviour {

	// Prefab de las casillas
	public GameObject squarePrefab;

	// Prefab de los enemigos
	public GameObject enemyPrefab;

	// Prefab de los pings
	public Ping pingPrefab;

	// Objeto que contiene la barra de escaneo
	public Transform scanBar;

	// Flecha que indica el CD del ping restante
	public Transform pingCDArrow;
	public float arrowMaxAngle = 80.0f;
	public float arrowSpeed = 2160.0f;

	// Tamaño del tablero de enemigos
	public int size;

	// Radio para hacer daño al jugador
	public float centerRadius;

	// Si activado, el tablero será circular
	public bool rounded;
	public bool roundedSpawn;

	// Radio del ping
	public float pingRadius;

	// Cooldown del ping
	public float pingCD;
	private float remainingPingCD;

	// Tiempo entre cada aparición de enemigo
	public float spawnTime;

	// Tiempo que tarda la barra en hacer el scan (vuelta completa)
	public float scanTime;
	public float heartBeatAngleTolerance;
	public float barGrowSpeed;

	// Si el radar está encendido o no
	[HideInInspector]
	public bool radarOn = true;

	// Tablero de enemigos
	private List<Enemy> enemies;
	private Transform enemiesParent;

	// Referencia a las casillas
	private Square[,] squares;
	private Transform squaresParent;

	// Padre de los pings creados
	private Transform pingParent;

	// Referencia al transform del objeto
	private Transform _transform;

	// Remaining time for the next enemy to spawn
	private float _timeRemaining = -1.0f;

	// Ángulo actual que lleva la barra de escaneo
	private float _scanAngle = 0.0f;

	private bool _playing = false;

	public void Play() {
		_playing = true;
	}

	public void Stop() {
		_playing = false;
		ClearEnemies();
	}

	private void Awake() {
		_transform = transform;
	}

	private void Start() {
		// Inicializa el tablero de enemigos
		enemies = new List<Enemy>();
		enemiesParent = new GameObject("Enemies").transform;
		enemiesParent.SetParent(_transform, false);

		// Crea el padre de los pings
		pingParent = new GameObject("Pings").transform;
		pingParent.SetParent(_transform, false);

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
				GameObject square = (GameObject)Instantiate(squarePrefab, squaresParent);
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
		// Hace girar la barra de escaneo
		_scanAngle += 360.0f * Time.deltaTime / scanTime;
		if (_scanAngle > 360.0f)
			_scanAngle -= 360.0f;
		scanBar.localRotation = Quaternion.AngleAxis(_scanAngle, Vector3.back);

		if (radarOn) {
			// Crece la barra y detecta enemigos
			scanBar.localScale = Vector3.MoveTowards(scanBar.localScale, Vector3.one, barGrowSpeed * Time.deltaTime);
			HeartBeatEnemies(_scanAngle);
		}
		else {
			// Mengua la barra
			scanBar.localScale = Vector3.MoveTowards(scanBar.localScale, Vector3.zero, barGrowSpeed * Time.deltaTime);
		}

		// Maneja CD del ping
		remainingPingCD -= Time.deltaTime;
		float arrowAngle = Mathf.Lerp(-arrowMaxAngle, arrowMaxAngle, 1 - remainingPingCD / pingCD);
		pingCDArrow.localRotation = Quaternion.RotateTowards(pingCDArrow.localRotation, Quaternion.Euler(0, 0, -arrowAngle), arrowSpeed / pingCD * Time.deltaTime);

		// Hace aparecer enemigos
		if (_playing) {
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
	}

	public void ClearEnemies() {
		foreach (Enemy enemy in enemies)
			enemy.Destroy();
		enemies.Clear();
	}

	public List<Enemy> GetEnemies(int x, int y) {
		if (!IsInsideGrid(x, y))
			return null;

		List<Enemy> enemiesInSquare = new List<Enemy>();
		foreach (Enemy enemy in enemies)
			if (enemy.IsInSquare(x, y, size))
				enemiesInSquare.Add(enemy);
		return enemiesInSquare;
	}

	public bool IsInsideGrid(int x, int y) {
		return x >= 0 && x < size && y >= 0 && y < size;
	}

	public void CreatePing(int x, int y, float radius) {
		// No se puede pingear apagado
		if (!radarOn)
			return;

		// Comprueba si el ping está en CD
		if (remainingPingCD >= 0)
			return;
		remainingPingCD = pingCD;

		Ping ping = Instantiate(pingPrefab);
		Transform pingTransform = ping.transform;
		pingTransform.parent = pingParent;

		pingTransform.localPosition = GetWorldPosition(x, y);
		pingTransform.localRotation = Quaternion.identity;
		pingTransform.localScale = Vector3.one;

		ping.x = x;
		ping.y = y;
		ping.radius = radius;
		ping.radar = this;
	}

	public void Ping(int x, int y, float radius) {
		foreach (Enemy enemy in enemies) {
			// Mira si la distancia es adecuada
			Vector3 distance = GetWorldPosition(x, y) - enemy.transform.localPosition;
			if (distance.sqrMagnitude <= radius * radius) {
				enemy.Show();
				enemy.HeartBeat();
			}
		}
	}

	private Vector3 GetWorldPosition(int x, int y) {
		return new Vector3(x + 0.5f - size / 2.0f, y + 0.5f - size / 2.0f, 0.0f);
	}

	public void SpawnEnemy(int x, int y) {
		// Coloca el enemigo en su sitio (asume tamaño 1)
		GameObject enemy = CreateEnemy();
		Transform enemyTransform = enemy.transform;
		enemyTransform.localPosition = GetWorldPosition(x, y);
		enemyTransform.localScale = enemyTransform.lossyScale;
	}

	public void SpawnEnemy(Vector3 position) {
		// Coloca el enemigo en su sitio (asume tamaño 1)
		GameObject enemy = CreateEnemy();
		Transform enemyTransform = enemy.transform;
		enemyTransform.localPosition = position;
		enemyTransform.localScale = enemyTransform.lossyScale;
	}

	private GameObject CreateEnemy() {
		// Crea el enemigo y lo emparenta
		GameObject enemy = (GameObject)Instantiate(enemyPrefab, enemiesParent);
		Enemy enemyComponent = enemy.GetComponent<Enemy>();
		enemies.Add(enemyComponent);
		enemyComponent.radar = this;
		return enemy;
	}

	public void Damage(Enemy enemy, int damage) {
		enemies.Remove(enemy);
		enemy.Destroy();
		GameController.GetInstance().Damage(damage);
	}

	public void DestroyEnemiesAtPosition(int x, int y) {
		List<Enemy> enemiesAtPosition = GetEnemies(x, y);
		foreach (Enemy enemy in enemiesAtPosition) {
			enemies.Remove(enemy);
			enemy.Destroy();
		}
		squares[x, y].PlayEffect();
	}

	private void HeartBeatEnemies(float angle) {
		foreach (Enemy enemy in enemies) {
			Vector3 localPosition = enemy.transform.localPosition;
			float enemyAngle = -Mathf.Atan2(localPosition.y, localPosition.x) * Mathf.Rad2Deg + 180.0f;
			if (Mathf.Abs(angle - enemyAngle) < heartBeatAngleTolerance)
				enemy.HeartBeat();
		}
	}
}
