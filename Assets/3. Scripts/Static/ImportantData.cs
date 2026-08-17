using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ImportantData
{
    public static int dayCount;
    public static int gameMinutes;

    public static List<Character> canUsedStudents = new(); 
    public static List<Character> usedStudents = new();

    public static int maxFloorCount;
    public static int nowFloorCount;
    public static Dictionary<int, GameObject> floorRoomsList = new();

    public static Dictionary<Item, int> gettingItemList = new();

    public static void SetDefultValue()
    {
        maxFloorCount = 0;
        nowFloorCount = 0;
        floorRoomsList = null;
    }
}
