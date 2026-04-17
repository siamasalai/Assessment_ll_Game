using UnityEngine;

public class WorldSpawner : MonoBehaviour
{
    public GameObject[] cityBlocks; // Drag your blocks here
    public float blockLength = 50f; // How long one block is in meters
    private int currentBlockIndex = 0;

    void Update()
    {
        // Check if the car has passed the halfway point of the current block
        if (transform.position.z > (currentBlockIndex * blockLength) + (blockLength / 2))
        {
            SpawnNext();
        }
    }

    void SpawnNext()
    {
        // Move the oldest block (the one far behind you) to the very front
        int indexToMove = currentBlockIndex % cityBlocks.Length;

        Vector3 newPos = cityBlocks[indexToMove].transform.position;
        newPos.z += blockLength * cityBlocks.Length;
        cityBlocks[indexToMove].transform.position = newPos;

        currentBlockIndex++;
    }
}