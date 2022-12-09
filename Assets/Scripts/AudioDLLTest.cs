using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ManagedAudio;

public class AudioDLLTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MyUtilities util = new MyUtilities();
        util.AddValues(1, 4);
        print(util.c);
    }

    // Update is called once per frame
    void Update()
    {
        print(MyUtilities.GenerateRandom(0, 100));
    }
}
