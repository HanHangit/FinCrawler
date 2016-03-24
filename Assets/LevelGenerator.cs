using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{

    public int height, width;
    public string seed;
    public GameObject[] maptile;
    public float tilesize;
    [Range(35, 65)]
    public int percent;
    public int[,,] room;
    public int maxroomwidth, maxroomheight;
    public int spawnminroomx, spawnminroomy;

    int[,] map;
    int currentgaenge;
    bool weggefunden;


    void Start()
    {
        room = new int[2, 5, 5];
        room[0, 0, 0] = 1;
        room[0, 0, 1] = 0;
        room[0, 0, 2] = 0;
        room[0, 0, 3] = 0;
        room[0, 0, 4] = 1;
        room[0, 1, 0] = 0;
        room[0, 1, 1] = 1;
        room[0, 1, 2] = 0;
        room[0, 1, 3] = 1;
        room[0, 1, 4] = 0;
        room[0, 2, 0] = 0;
        room[0, 2, 1] = 0;
        room[0, 2, 2] = 1;
        room[0, 2, 3] = 0;
        room[0, 2, 4] = 0;
        room[0, 3, 0] = 0;
        room[0, 3, 1] = 1;
        room[0, 3, 2] = 0;
        room[0, 3, 3] = 1;
        room[0, 3, 4] = 0;
        room[0, 4, 0] = 1;
        room[0, 4, 1] = 0;
        room[0, 4, 2] = 0;
        room[0, 4, 3] = 0;
        room[0, 4, 4] = 1;
        room[1, 0, 0] = 0;
        room[1, 0, 1] = 0;
        room[1, 0, 2] = 0;
        room[1, 0, 3] = 0;
        room[1, 0, 4] = 0;
        room[1, 1, 0] = 0;
        room[1, 1, 1] = 1;
        room[1, 1, 2] = 1;
        room[1, 1, 3] = 0;
        room[1, 1, 4] = 0;
        room[1, 2, 0] = 0;
        room[1, 2, 1] = 1;
        room[1, 2, 2] = 1;
        room[1, 2, 3] = 0;
        room[1, 2, 4] = 0;
        room[1, 3, 0] = 0;
        room[1, 3, 1] = 0;
        room[1, 3, 2] = 0;
        room[1, 3, 3] = 0;
        room[1, 3, 4] = 0;
        room[1, 4, 0] = 0;
        room[1, 4, 1] = 0;
        room[1, 4, 2] = 0;
        room[1, 4, 3] = 0;
        room[1, 4, 4] = 0;
        currentgaenge = 0;
        map = new int[width, height];
        weggefunden = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            GameObject[] todestroy = GameObject.FindGameObjectsWithTag("Wall");
            foreach (GameObject d in todestroy)
                Destroy(d);
            MapClear();
            weggefunden = false;
            BuildRooms();
            GenerateMap();

        }
    }

    void BuildWay(int startx, int starty, Vector2 dir)
    {
        int x = startx, y = starty;
        while (map[x, y] != 0)
        {
            if (x > 1 && x < width - 2)
                x += (int)dir.x;
            if (y > 1 && y < height - 2)
                y += (int)dir.y;

        }
    }

    void BuildRooms()
    {
        int x, y;
        Vector2 dir = Vector2.zero;
        for (int i = 0; i < room.GetLength(0); ++i)
        {
            if (Random.Range(0, 2) == 0)
            {
                x = Random.Range(spawnminroomx, Mathf.Max(width - maxroomwidth, spawnminroomx));
                y = Random.Range(0, x - spawnminroomx);
                dir = new Vector2(-1, -1);
            }
            else
            {
                y = Random.Range(spawnminroomy, Mathf.Max(height - maxroomheight, spawnminroomy));
                x = Random.Range(0, y - spawnminroomy);
                dir = new Vector2(1, 1);

            }

            //BuildWay(x, y, dir);

            for (int l = 0; l < room.GetLength(1); ++l)
                for (int k = 0; k < room.GetLength(2); ++k)
                {
                    map[x + l, y + k] = room[i, l, k];
                }
        }
    }
    void MapClear()
    {
        for (int i = 0; i < width; i++)
        {
            for (int l = 0; l < height; l++)
            {
                map[i, l] = 1;
            }
        }
    }

    void FinishedWay(int x, int y)
    {
        while (!weggefunden)
        {
            map[x, y] = 0;
            if (x == width - 2 && y == height - 2)
            {
                weggefunden = true;
                break;
            }
            if (x == width - 2)
                ++y;
            else
                ++x;


        }
    }

    void GenerateMap()
    {
        int i = 1, l = 1;
        while (!weggefunden)
        {
            map[i, l] = 0;
            map[i - 1, l - 1] = 0;
            map[i, l - 1] = 0;
            map[i - 1, l] = 0;
            map[i + 1, l + 1] = 0;
            map[i, l + 1] = 0;
            map[i + 1, l] = 0;
            map[i + 1, l - 1] = 0;
            map[i - 1, l + 1] = 0;
            if (Random.Range(0, 100) < percent)
                i += Random.Range(0, 2);
            else
                l += Random.Range(0, 2);

            if (i == width - 2 && l == height - 2)
                weggefunden = true;
            else if (i == width - 2 || l == height - 2)
            {
                FinishedWay(i, l);
                weggefunden = true;
                break;
            }

            map[i, l] = 0;

        }

        for (i = 0; i < width; i++)
        {
            for (l = 0; l < height; l++)
            {
                if (map[i, l] != 0)
                {
                    map[i, l] = 1;
                    Vector2 position = new Vector2(i * tilesize, l * tilesize);
                    Instantiate(maptile[1], position, Quaternion.identity);
                }
                else
                {
                    Vector2 position = new Vector2(i * tilesize, l * tilesize);
                    Instantiate(maptile[0], position, Quaternion.identity);
                }
            }
        }
    }
}
