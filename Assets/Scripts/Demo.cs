using UnityEngine;
using System.Collections;

public class Demo : MonoBehaviour {

	public float offsetx;
	public float offsety;

	private int player = 0;
	private int posX = 1;
	private int posY = 1;

	public AudioClip wall;
	public AudioClip error;
	public AudioClip placement;

	public GameObject cursor;
	public GameObject[] marks = new GameObject[2];

	private int[,] curState = new int[3, 3];
	private ArrayList addedMarks = new ArrayList();

	void Start () {
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				curState[i, j] = -1;
			}
		}
	}

	void Update () {
		bool directionPressed = false;
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			directionPressed = true;
			if (posX < 1) {
				AudioSource.PlayClipAtPoint(wall, Vector3.zero, 1);
			}
			else {
				posX--;
			}
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			directionPressed = true;
			if (posX > 1) {
				AudioSource.PlayClipAtPoint(wall, Vector3.zero, 1);
			}
			else {
				posX++;
			}
		}
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			directionPressed = true;
			if (posY < 1) {
				AudioSource.PlayClipAtPoint(wall, Vector3.zero, 1);
			}
			else {
				posY--;
			}
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			directionPressed = true;
			if (posY > 1) {
				AudioSource.PlayClipAtPoint(wall, Vector3.zero, 1);
			}
			else {
				posY++;
			}
		}
		if (!directionPressed && Input.GetKeyDown(KeyCode.Space)) {
			if (curState[posY, posX] != -1) {
				AudioSource.PlayClipAtPoint(error, Vector3.zero);
			} else {
				AudioSource.PlayClipAtPoint(placement, Vector3.zero);
				GameObject mark = (GameObject) GameObject.Instantiate(marks[player]);
				mark.transform.position = new Vector3((posX - 1) * offsetx, 0, (posY - 1) * offsety);
				addedMarks.Add(mark);
				curState[posY, posX] = player;
				player = ++player % 2;
				checkWin();
				if (addedMarks.Count == 9) {
					reset();
				}
			}
		}
		cursor.transform.position = new Vector3((posX - 1) * offsetx, 0, (posY - 1) * offsety);
	}

	void checkWin() {
		for (int i = 0; i < 3; i++) {
			if (curState[i, 0] == curState[i, 1] && curState[i, 0] == curState[i, 2] && curState[i, 0] != -1) {
				reset();
				return;
			}
		}
		for (int i = 0; i < 3; i++) {
			if (curState[0, i] == curState[1, i] && curState[0, i] == curState[2, i] && curState[0, i] != -1) {
				reset();
				return;
			}
		}
		if (curState[0, 0] == curState[1, 1] && curState[0, 0] == curState[2, 2] && curState[0, 0] != -1) {
			reset();
			return;
		}
		if (curState[0, 2] == curState[1, 1] && curState[0, 2] == curState[2, 0] && curState[0, 2] != -1) {
			reset();
			return;
		}
	}

	void reset() {
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				curState[i, j] = -1;
			}
		}
		foreach (Object o in addedMarks) {
			Destroy(o);
		}
		addedMarks = new ArrayList();
		player = 0;
		posX = 1;
	 	posY = 1;
	}
}