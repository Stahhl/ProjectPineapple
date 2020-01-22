using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class HelloWorld
{
    private static string message = "Hello World!";

    public static void PrintMessage(bool isServer)
    {
        if(isServer == true)
        {
            Console.WriteLine(message);
        }
        else
        {
            Debug.Log(message);
        }
    }
    public static string GetMessage()
    {
        return message;
    }
}
