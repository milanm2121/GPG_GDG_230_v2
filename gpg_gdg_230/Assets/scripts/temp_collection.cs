using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class temp_collection
{
    public int[] card_count = new int[150];

    public List<serilisable_deak> sd = new List<serilisable_deak>();

    public int progress;
    public void temp_collection_save()
    {
        sd = static_collections.Deaks;
        card_count = static_collections.Collection;
        progress = static_collections.Progres;
        save_system.saveData(this);
    }
    public void temp_collection_load()
    {
        static_collections.Deaks = sd;
        static_collections.Collection = card_count;
        static_collections.Progres = progress;
    }
}
