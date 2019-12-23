using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag : MonoBehaviour
{
    private int bagId;
    private static int idCounter = 0;
    private static int bagCount;
    private static int[] numOfBagsOpens;
    private static List<Bag> listOfOpenBags = new List<Bag>();
    private static GameObject bagUi;
    private static Transform firstSlot;

    private bool playerBag = false;
    private int myNum = 0;

    private int maxSpaces = 24;
    private int currecntSpaces;

    private int maxWeight = 100;
    private int currentWeight;

    private GameObject myUi;

    private List<Item> itemList = new List<Item>();
    private List<GameObject> tempIcons = new List<GameObject>();


    public int BagId
    {
        get
        {
            return bagId;
        }

    }

    private void Start()
    {
        currecntSpaces = 0;
        currentWeight = 0;
    }

    public void initBag(bool playerBag)
    {
        this.playerBag = playerBag;
        init();
    }

    private void init()
    {
        bagCount = -1;
        idCounter++;
        bagId = idCounter;

        if (numOfBagsOpens == null)
        {
            numOfBagsOpens = new int[3];

            for (int i = 0; i<numOfBagsOpens.Length; i++)
                numOfBagsOpens[i] = 0;
        }
    }

    public static void setInventory(GameObject _bagUi)
    {
        bagUi = _bagUi;
    }


    public bool addItem(Item item)
    {
        if (bagSpaceCheck(item) == false)
        {
            return false;
        }

        itemList.Add(item);
        currecntSpaces++;
        calcBagWeight();
        return true;
    }

    public void calcBagWeight()
    {
        int count = 0;
        foreach (Item item in itemList)
        {
            count += item.weight;
        }
        currentWeight = count;
    }

    public Item getItem(int i)
    {
        return itemList[i];
    }

    private bool bagSpaceCheck(Item item)
    {
        if ((currecntSpaces + 1) >= maxSpaces)
        {
            return false;
        }
        else
        {
            if ((currentWeight + item.weight) > maxWeight)
            {
                return false;
            }
        }
        return true;
    }

    public void removeItem(int pos)
    {
        itemList.RemoveAt(pos);
    }

    public void switchPlaces(int i1, int i2)
    {
        //switch items places inside the bag.
        Item itemHolder = itemList[i1];
        itemList[i1] = itemList[i2];
        itemList[i2] = itemHolder;

    }

    public int bagSize()
    {
        return itemList.Count;
    }

    public static void refreshIcons()
    {
        print("(Static) bags refresh icons.");

        foreach (Bag bag in listOfOpenBags)
        {
            bag.removeIcons();
            bag.spawnIcons();
        }
    }

    public void openBag(string name)
    {
        print(bagId);

        if (myUi == null)
        {
            if (bagCount < numOfBagsOpens.Length)
            {
                myUi = Instantiate(bagUi);
                myUi.GetComponent<BagUi>().initBagUi(this);
                myUi.SetActive(true);
                myUi.transform.parent = bagUi.transform.parent.transform;
                myUi.transform.Find("Text").GetComponent<Text>().text = name;

                if (playerBag == true)
                {
                    print("Player bag.");
                    myUi.transform.position = new Vector2(bagUi.transform.position.x+200, bagUi.transform.position.y);
                }
                else
                {
                    print("Not player bag.");

                    for (int i = 0; i < numOfBagsOpens.Length; i++)
                    {
                        if (numOfBagsOpens[i] == 0)
                        {
                            myUi.transform.position = new Vector2(bagUi.transform.position.x - i * 200, bagUi.transform.position.y);
                            this.myNum = i;
                            bagCount++;
                            break;
                        }
                    }
                }
                print("Open bag: " + myNum + ", " + playerBag);

                spawnIcons();
            }
            listOfOpenBags.Add(this);
        }
        else
        {
            listOfOpenBags.Remove(this);

            print("Remove bag: " + myNum + ", " + playerBag);
            removeIcons();
            if(playerBag == false)
            {
                numOfBagsOpens[myNum] = 0;
                bagCount--;
            }
            Destroy(myUi.gameObject);
            myUi = null;

        }
    }

    public void spawnIcons()
    {
        float iconsSpaces = 60;
        int width = 3;
        int height = 8;
        int itemIndex = 0;

        firstSlot = myUi.transform.Find("firstSlot");
        myUi.transform.SetAsFirstSibling();

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (itemIndex == itemList.Count)
                {
                    return;
                }
                Vector3 pos = new Vector3(firstSlot.position.x + j * iconsSpaces, firstSlot.position.y + i * iconsSpaces, firstSlot.position.z);
                GameObject go = Instantiate(Resources.Load<GameObject>("itemIcon"), pos, firstSlot.transform.rotation, myUi.transform);
                go.transform.Find("mask/icon").GetComponent<Image>().sprite = itemList[itemIndex].getSprite;

                go.AddComponent<ItemTicket>();
                go.GetComponent<ItemTicket>().initTicket(this, itemIndex);

                tempIcons.Add(go);
                itemIndex++;

            }
        }

    }
    public void removeIcons()
    {
        foreach (GameObject icon in tempIcons)
        {
            Destroy(icon.gameObject);
        }
    }
}
