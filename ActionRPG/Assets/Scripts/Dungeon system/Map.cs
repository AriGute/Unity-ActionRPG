using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private int mapSize = 5;
    private MapType mapType;
    private Room[,] rooms;
    public Room startRoom;
    public Room endRoom;
    private Camera mainCamera;
    public GameObject doorTest;

    public void generateMap(Door doorConnection = null,int mapSize = 2)
    {
        /// <summary>
        /// if 'doorConnection' is undefine, then the enterance/exit lead to the World Map by default.
        /// </summary>
        
        this.mapSize = mapSize;
        generateRooms();
        generateMaze(mapSize);

        GameObject currentDoor;
        currentDoor = Instantiate(Resources.Load<GameObject>("Forest/Doors/doorLeft Variant"));
        currentDoor.transform.position = new Vector3(startRoom.transform.position.x - 5, startRoom.transform.position.y - 2, startRoom.transform.position.z + 20);
        currentDoor.transform.parent = startRoom.transform;
        startRoom.getDoor(0) = currentDoor;
        currentDoor.name = "exitDoor";

        if (doorConnection == null)
        {
            currentDoor.GetComponent<Door>().exitWorldMap = true;
        }
        else
        {
            startRoom.getDoor(0).GetComponent<Door>().targetDoor = doorConnection.gameObject;
            doorConnection.GetComponent<Door>().targetDoor = startRoom.getDoor(0).gameObject;
        }

        this.transform.localScale = new Vector3(1.5f, 1.2f, 1);

    }


    private void generateRooms()
    {
        rooms = new Room[mapSize, mapSize];
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                rooms[i, j] = Instantiate(Resources.Load<Room>("Room"));
                rooms[i, j].transform.parent = this.transform;
                rooms[i, j].generateRoom(MapType.forest, i, j);
                rooms[i, j].gameObject.SetActive(false);
                rooms[i, j].name = "Room[" + i + "," + j + "]";

                rooms[i, j].gameObject.SetActive(true);
                //rooms[i, j].transform.position = new Vector3(j * 15,0- i * 15, 1);
                rooms[i, j].gameObject.SetActive(false);

            }
        }

        startRoom = rooms[Random.Range(0, mapSize) , 0];
        startRoom.name = "Start";
        do
        {
            endRoom = rooms[Random.Range(0, mapSize), Random.Range(0, mapSize)];
        } while (startRoom == endRoom);
        endRoom.name = "End";

        startRoom.gameObject.SetActive(true);
    }

    private void generateMaze(int maxSize)
    {

        Stack<Room> trace = new Stack<Room>();
        Stack visited = new Stack();

        int saftyCount1 = 0;
        int i = 0, j = 0;
        float constDis = 5;

        int[] availableDoors = new int[4];
        int numOfDoors = 4;
        Room currentCell = rooms[0, 0];

        Room nextCell = rooms[0, 0];

        Direction direction = Direction.down;


        do
        {
            GameObject currentDoor = null;
            GameObject nextDoor = null;
            int b = 0;

            int saftyCount2 = 0;
            int chosenDoor = 0;

            for (int a = 0; a < 4; a++)
            {
                //set every 'door flag' to 1(usable).
                availableDoors[a] = 1;
            }
            numOfDoors = 4;




            do//while (the next cell is not marked as 'traced' or 'visited');
            {

                i = currentCell.i;
                j = currentCell.j;

                //change 'door flag' to -1 if the room is at the edge of the frame.
                if (j == 0)
                {
                    availableDoors[0] = -1;
                    numOfDoors--;
                }
                else if (j == maxSize - 1)
                {
                    availableDoors[2] = -1;
                    numOfDoors--;
                }

                if (i == 0)
                {
                    availableDoors[1] = -1;
                    numOfDoors--;
                }
                else if (i == maxSize - 1)
                {
                    availableDoors[3] = -1;
                    numOfDoors--;
                }

                //chose randomly the direction of the next cell.
                int randNum = Random.Range(1, numOfDoors + 1);

                do
                {
                    currentCell.availableDoors = availableDoors;
                    chosenDoor = currentCell.getRandomDoor();
                    if (chosenDoor == -1)
                    {
                        break;
                    }
                    if(currentCell.doors[chosenDoor] != null)
                    {
                        availableDoors[chosenDoor] = -1;
                    }
                } while (currentCell.doors[chosenDoor]!=null);
                if (chosenDoor == -1)
                {
                    currentCell = trace.Pop();
                    break;
                }

                //chose the next cell to bind with the current cell.
                switch (chosenDoor)
                {
                    case 0:
                        //left.
                        j--;
                        direction = Direction.left;
                        break;
                    case 1:
                        //up.
                        i--;
                        direction = Direction.up;
                        break;
                    case 2:
                        //right.
                        j++;
                        direction = Direction.right;
                        break;
                    case 3:
                        //down.
                        i++;
                        direction = Direction.down;
                        break;
                }
                
                nextCell = rooms[i, j];
           
                saftyCount2++;
                if (saftyCount2 == 4)
                {
                   // print("[Map.generateMaze :Prevent 'next room search' never ending loop]");
                    break;
                }

            } while (nextCell.visited == true || nextCell.traced == true);

            if (chosenDoor > -1 && nextCell.traced == false && saftyCount2 < 4)
            {
                //found suitable 'next cell', connect the current and the next cell.
                nextCell.maxDoors = numOfDoors;
                trace.Push(currentCell);

                float randDis = Random.Range(-4, 4);
                switch (direction)
                {
                    case Direction.left:
                        currentDoor = Instantiate(Resources.Load<GameObject>("Forest/Doors/doorLeft Variant"));
                        currentDoor.transform.position = new Vector3(currentCell.transform.position.x-5, currentCell.transform.position.y-2, currentCell.transform.position.z + 20);

                        break;
                    case Direction.up:
                        currentDoor = Instantiate(Resources.Load<GameObject>("Forest/Doors/doorUp Variant"));
                        currentDoor.transform.position = new Vector3(currentCell.transform.position.x + randDis, currentCell.transform.position.y, currentCell.transform.position.z + 20);

                        break;
                    case Direction.right:
                        currentDoor = Instantiate(Resources.Load<GameObject>("Forest/Doors/doorRight Variant"));
                        currentDoor.transform.position = new Vector3(currentCell.transform.position.x + 5, currentCell.transform.position.y-2, currentCell.transform.position.z + 20);

                        break;
                    case Direction.down:
                        currentDoor = Instantiate(Resources.Load<GameObject>("Forest/Doors/doorDown Variant"));
                        currentDoor.transform.position = new Vector3(currentCell.transform.position.x + randDis, currentCell.transform.position.y - 3, currentCell.transform.position.z + 20);

                        break;
                }
                currentDoor.transform.parent = currentCell.transform;
                currentCell.doors[chosenDoor] = currentDoor;


                switch (direction)
                {
                    case Direction.left:
                        nextDoor = Instantiate(Resources.Load<GameObject>("Forest/Doors/doorRight Variant"));
                        nextDoor.transform.position = new Vector3(nextCell.transform.position.x + 5, nextCell.transform.position.y-2, nextCell.transform.position.z + 22);
                        break;
                    case Direction.up:
                        nextDoor = Instantiate(Resources.Load<GameObject>("Forest/Doors/doorDown Variant"));
                        nextDoor.transform.position = new Vector3(nextCell.transform.position.x + randDis, nextCell.transform.position.y - 3, nextCell.transform.position.z + 22);
                        break;
                    case Direction.right:
                        nextDoor = Instantiate(Resources.Load<GameObject>("Forest/Doors/doorLeft Variant"));
                        nextDoor.transform.position = new Vector3(nextCell.transform.position.x - 5, nextCell.transform.position.y-2, nextCell.transform.position.z + 22);
                        break;
                    case Direction.down:
                        nextDoor = Instantiate(Resources.Load<GameObject>("Forest/Doors/doorUp Variant"));
                        nextDoor.transform.position = new Vector3(nextCell.transform.position.x + randDis, nextCell.transform.position.y, nextCell.transform.position.z + 22);
                        break;
                }
                nextDoor.transform.parent = nextCell.transform;
                nextCell.doors[chosenDoor < 2 ? chosenDoor + 2 : chosenDoor - 2] = nextDoor;



                currentDoor.GetComponent<Door>().bindDoors(nextDoor.GetComponent<Door>());
                currentCell.traced = true;
                currentCell = nextCell;
            }
            else
            {
                currentCell.visited = true;
                if (trace.Count == 0)
                {
                    return;
                }
                currentCell = trace.Pop();
            }

            saftyCount1++;
            if (saftyCount1 == maxSize * maxSize * 5)
            {
                //print("[Map.generateMaze : Prevent never ending loop]");
                break;
            }

        } while (trace.Count != 0);
        if (saftyCount1 < maxSize * maxSize * 2)
        {
            //print("[Map.generateMaze : Clip is empty]");
        }
    }
}
