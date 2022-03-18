using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridController : MonoBehaviour
{
    // background set up
    private int rows = 7;
    private int columns = 10;
    Vector2 backgroundTilePosition;

    // location of icons spawned
    public IconController[,] iconLocations;

    // Game objects to be spawned
    [SerializeField] private GameObject iconBackgroundTile;
    [SerializeField] private IconController[] icons;



    void Start()
    {
        iconLocations = new IconController[rows, columns];
        setupBackground();       
    }


    void Update()
    {
    }

    private void setupBackground()                                                                                  // Setting up the background tiles into position
    {
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                backgroundTilePosition = new Vector2(x, y);
                GameObject iconBackground = Instantiate(iconBackgroundTile, backgroundTilePosition, Quaternion.identity) as GameObject;
                iconBackground.transform.parent = this.transform;
                iconBackground.name = x + ", " + y;
                setupIcons(new Vector2Int(x,y));
            }
        }
    }

    private void setupIcons(Vector2Int postionToSpawn)                                                                  // Icon to be used selected
    {
        int iconToBeUsed = Random.Range(0, icons.Length);
        spawnIcons(postionToSpawn, icons[iconToBeUsed]);
    }

    private void spawnIcons(Vector2Int postionToSpawn, IconController iconToSpawn)                                      // Spawning the Icon to be used
    {
        IconController icon = Instantiate(iconToSpawn, new Vector3(postionToSpawn.x, postionToSpawn.y, 0), Quaternion.identity);
        icon.transform.parent = this.transform;
        icon.name = postionToSpawn.x + " " + postionToSpawn.y;
        iconLocations[postionToSpawn.x, postionToSpawn.y] = icon;                                                       

        icon.attachGrid(this, rows, columns);
    }

}
