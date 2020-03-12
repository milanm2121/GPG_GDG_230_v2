using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class static_collections {
    private static List<serilisable_deak> deaks = new List<serilisable_deak>();

    public static List<serilisable_deak> Deaks { get => deaks; set => deaks = value; }
    public static int[] Collection { get => collection; set => collection = value; }
    public static int Progres { get => progres; set => progres = value; }

    private static int[] collection= new int[150];

    private static int progres;
}
