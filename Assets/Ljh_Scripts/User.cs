using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public double text_latitude;
    public double text_longitude;
    public string user_name = "NULL";
    public List<string> storys = new List<string>();

    public User(double text_latitude, double text_longitude, string name,List<string> storys)
    {
        this.text_latitude = text_latitude;
        this.text_longitude = text_longitude;
        this.user_name = name;
        this.storys = storys;
    }
}