using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine;

public class TileManager : MonoBehaviour {

    public int mapLenght = 500;                                     //Lower and upper limit for our random number of walls per level.15
    public int mapHeight = 3;
    public int amountObjects = 5;

    public GameObject[] floorTiles;
    public GameObject[] plataformTiles;
    public GameObject[] backgroundImages;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void InitialiseList()
    {
        //Clear our list gridPositions.
        gridPositions.Clear();

        //Loop through x axis (columns).
        for (int x = 1; x < mapLenght - 1; x++)
        {
            //Within each column, loop through y axis (rows).
            for (int y = 1; y < mapHeight - 1; y++)
            {
                //At each index add a new Vector3 to our list with the x and y coordinates of that position.
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    public void TilesSetup()
    {
        createBackground();
        createPlataform();
        createFlyingObjects();
    }

    private void createBackground()
    {
        for(int i = 0; i < mapLenght; i = i + 19) {
            Vector3 position = new Vector3(i, 3f, 1f);
            gridPositions.Remove(position);
            GameObject toInstantiate = backgroundImages[Random.Range(0, backgroundImages.Length - 1)];
            GameObject instance =
               Instantiate(toInstantiate, position, Quaternion.identity) as GameObject;
            //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
            instance.transform.SetParent(boardHolder);
        }
    }

    private void createFlyingObjects()
    {
        float lastDistance = 0;
        for(int i = 0; i < amountObjects; i++)
        {
            int brickAmount = Random.Range(1, 4);
            int maxDistance = mapLenght / amountObjects;
            int minDistance = maxDistance - (15/amountObjects);
            if (maxDistance < minDistance)
            {
                maxDistance = minDistance;
            }

            lastDistance = Random.Range(minDistance, maxDistance)+lastDistance;
            Debug.Log("Distância: " + lastDistance);
            for (int brickIndex = 0; brickIndex < brickAmount; brickIndex++)
            {
                
                Vector3 position = new Vector3((lastDistance+brickIndex)*0.3f, 3f, 0f);
                gridPositions.Remove(position);
                GameObject toInstantiate = plataformTiles[Random.Range(0, plataformTiles.Length - 1)];
                GameObject instance =
                   Instantiate(toInstantiate, position, Quaternion.identity) as GameObject;
                //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    private void createPlataform()
    {
        int occupiedSize = 0;
        int floorOscillation = 3;
        List<int> parts = new List<int>();


        for (int i = 0; i < floorOscillation - 1; i++)
        {
            int partSize = Random.Range(1, mapLenght - occupiedSize);
            occupiedSize += partSize;
            parts.Add(partSize);
        }

        parts.Add(mapLenght - occupiedSize);
        int index = 0;
        foreach (int partSize in parts)
        {
            int heightOscillation = Random.Range(1, 3);
            for (int i = 0; i < partSize; i++, index++)
            {
                Vector3 objectPosition = new Vector3(index * 0.3f, heightOscillation * 0.5f);
                gridPositions.Remove(objectPosition);
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length - 1)];
                GameObject instance =
                   Instantiate(toInstantiate, objectPosition, Quaternion.identity) as GameObject;
                //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                instance.transform.SetParent(boardHolder);
            }
        }
    }
}
