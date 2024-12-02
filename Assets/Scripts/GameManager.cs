using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ClearManager cm;

    public GameObject[] tetrominoes;

    public Transform spawnPosition;

    public AnimationCurve speedCurve = new AnimationCurve(new Keyframe(5, 1f), new Keyframe(10, 1.25f));

    public IEnumerator SpawnTetromino()
    {
        while (true)
        {
            // Spawn random tetromino from array
            int r = Random.Range(0, tetrominoes.Length);
            GameObject tetromino = Instantiate(tetrominoes[r], spawnPosition.position, Quaternion.identity);

            // Add Controlled behavior
            Controlled ctrl = tetromino.AddComponent<Controlled>();

            // Set speed based on score
            ctrl.speed = speedCurve.Evaluate(cm.score);

            // Wait until Controlled is destroyed
            while (tetromino.GetComponent<Controlled>() != null)
            {
                yield return null;
            }

            // Wait 1 second before spawning next
            yield return new WaitForSeconds(1f);
        }
    }

    void Start()
    {
        StartCoroutine(SpawnTetromino());
    }
}
