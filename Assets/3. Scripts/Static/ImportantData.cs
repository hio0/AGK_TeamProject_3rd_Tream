using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ImportantData
{
    public static int dayCount;
    public static int gameMinutes;
    public static int moneyCount;

    public static List<Character> canUsedStudents = new(); 
    public static List<Character> usedStudents = new();

    public static int maxFloorCount;
    public static int nowFloorCount;
    public static Dictionary<int, GameObject> floorRoomsList = new();

    public static Dictionary<ItemData, int> gettingItemList = new();
    public static Dictionary<ItemData, int> myItemList = new();

    public static void SetDefultValue()
    {
        dayCount = 1;
        gameMinutes = 0;
        moneyCount = 30;

        canUsedStudents.Clear();
        usedStudents.Clear();

        maxFloorCount = 0;
        nowFloorCount = 0;

        floorRoomsList.Clear();
        gettingItemList.Clear();
        myItemList.Clear();
    }
}
