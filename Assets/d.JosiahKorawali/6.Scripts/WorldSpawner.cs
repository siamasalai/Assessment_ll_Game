using UnityEngine;

public class WorldSpawner : MonoBehaviour
{
    [Header("Setup")]
    public GameObject[] cityBlocks; // Drag City_Block_01 and 02 here
    public float blockLength = 95f; // Set this to 95 in the Inspector

    private int currentBlockIndex = 0;

    void Update()
    {
        // Safety Check: Prevent the DivideByZero error if the array is empty
        if (cityBlocks == null || cityBlocks.Length == 0)
        {
            return;
        }

        // Logic: Check if the car has passed the halfway point of the CURRENT block
        // As the car moves past 47.5m, 142.5m, etc., the oldest block teleports ahead.
        float triggerPoint = (currentBlockIndex * blockLength) + (blockLength / 2f);

        if (transform.position.z > triggerPoint)
        {
            SpawnNext();
        }
    }

    void SpawnNext()
    {
        // Determine which block in the array to move (0 or 1)
        int indexToMove = currentBlockIndex % cityBlocks.Length;

        // THE SNAP FIX: 
        // We calculate the absolute Z position where the block MUST land.
        // For 2 blocks of 95m: 
        // First jump lands at 190m (2 * 95)
        // Second jump lands at 285m (3 * 95)
        float nextZPos = (currentBlockIndex + cityBlocks.Length) * blockLength;

        // Move the block to its precise new location
        Vector3 newPos = cityBlocks[indexToMove].transform.position;
        newPos.z = nextZPos;
        cityBlocks[indexToMove].transform.position = newPos;

        // Increment index so the next trigger happens 95 meters later
        currentBlockIndex++;
    }
}