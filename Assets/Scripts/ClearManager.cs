using System.Collections.Generic;
using UnityEngine;

public class ClearManager : MonoBehaviour
{
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 20;

    public int score = 0;

    public TMPro.TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for (int y = 0; y < height; y++)
        {
            var line = CheckLine(y);

            if (line != null)
            {
                foreach (var block in line)
                {
                    Destroy(block);
                }
                score++;
                scoreText.text = "Score: " + score;
            }

        }
    }

    HashSet<GameObject> CheckLine(int line)
    {
        var tetrises = new HashSet<GameObject>();

        float x_offset = (width - 1) * 0.5f;
        float y_offset = (height - 1) * 0.5f;


        for (int x = 0; x < width; x++)
        {
            var pos = transform.position + new Vector3(x - x_offset, line - y_offset, 0.0f);
            var parts = Physics.OverlapSphere(pos, 0.01f);

            if (parts.Length == 0) return null;

            // var block = parts[0].gameObject;
            foreach (var block in parts)
                tetrises.Add(block.gameObject);
        }

        if (tetrises.Count < width) return null;

        return tetrises;
    }

    void OnDrawGizmosSelected()
    {
        float x_offset = (width - 1) * 0.5f;
        float y_offset = (height - 1) * 0.5f;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var pos = transform.position + new Vector3(x - x_offset, y - y_offset, 0.0f);

                if (Physics.OverlapSphere(pos, 0.25f).Length > 0)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.white;
                }

                Gizmos.DrawWireSphere(pos, 0.25f);

            }
        }
    }
}
