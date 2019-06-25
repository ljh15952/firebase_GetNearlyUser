using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using UnityEngine.UI;

public class firebaseMng : MonoBehaviour
{

    private static firebaseMng Instance;
    public DatabaseReference reference;

    public LocationMng LM;
    public UIManager UM;


    public List<User> real_users = new List<User>();

    public Text n_name;
    public List<Text> n_storys = new List<Text>();

    public GameObject fa;


    // Use this for initialization
    void Start()
    {
        //DB호출전 설정
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://fillingmoon-89e94.firebaseio.com/");
        //관리자호출~
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        //User123 user1 = new User123("ljh", "genius");
        //User123 user2 = new User123("kyl", "babo");
        //string json = JsonUtility.ToJson(user1);
        //string json2 = JsonUtility.ToJson(user2);

        ////Using Json
        //reference.Child("users").Child("user1").SetRawJsonValueAsync(json);
        ////Using Path
        //reference.Child("users").Child("user2").Child("name").SetValueAsync(user2.name);
        //reference.Child("users").Child("user2").Child("email").SetValueAsync(user2.email);
        ////Using Push
        //string key = reference.Child("users").Push().Key;
        //reference.Child("users").Child(key).Child("email").SetValueAsync("This is Push()");
        ////using Update
        //Dictionary<string, object> update = user2.ToDictionary();
        //reference.Child("users").Child("user3").SetRawJsonValueAsync(json);
        //reference.Child("users").Child("user3").UpdateChildrenAsync(update);


        // reference.Child("SEX").Child("SEX2").Child("SEX3").SetValueAsync("SEX4");
        ///    reference.Child("S65asdqwqweeeqEX2").Child("SEqweX2qweqwe456").SetValueAsync("SEeqweXwasdqe4");
        /////      reference.Child("Users").Child("ljh").SetValueAsync("SEeqweXwasdqe4");

        //     reference.Child("users").Child("user3").UpdateChildrenAsync(update);
        // read();
        Debug.Log(GetUserChildrenCount());

    }

    void SetUserID()
    {
        if (PlayerPrefs.HasKey("u_ID")) //있으면 return
            return;

        PlayerPrefs.SetInt("u_ID", (int)GetUserChildrenCount());
        Debug.Log(GetUserChildrenCount());
        //    Debug.Log(temp +"ASD");
    }

    int GetUserID()
    {
        return PlayerPrefs.GetInt("u_ID");
    }


    public void SetLocation()
    {
        //storynum 도 playerperafs로 저장해야대
        long childct = GetUserID();
        reference.Child("users").Child(childct.ToString()).Child("latitude").SetValueAsync(LM.text_latitude); // my
        reference.Child("users").Child(childct.ToString()).Child("longitude").SetValueAsync(LM.text_longitude); // my
        reference.Child("users").Child(childct.ToString()).Child("name").SetValueAsync(UM.u_name);  // my
        reference.Child("users").Child(childct.ToString()).Child("story" + UM.storynum).SetValueAsync(UM.u_text[UM.storynum_lacal]);  // my
    }



    public void getUserData()
    {
        reference.Child("users").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                for (int i = 0; i < temp; i++)
                {
                    string s = snapshot.Child(i.ToString()).Child("latitude").Value.ToString();
                    string s2 = snapshot.Child(i.ToString()).Child("longitude").Value.ToString();
                    string s3 = snapshot.Child(i.ToString()).Child("name").Value.ToString();
                    List<string> storys = new List<string>();
                    for (int j = 0; j < 15; j++)
                    {
                        if (snapshot.Child(i.ToString()).Child("story" + j).Exists)
                        {
                            storys.Add(snapshot.Child(i.ToString()).Child("story" + j).Value.ToString());
                        }
                    }
                    User U = new User(float.Parse(s), float.Parse(s2), s3,storys);
                    real_users.Add(U);
                }
            }
            else if (task.IsFaulted)
            {
                // Handle the error...
                Debug.Log("getData fail ");
            }
            else
            {
                Debug.Log("getData cancel ");
            }
        });
    }
    public GameObject fuck;

    public void GetneartlyUser()
    {
        fuck.SetActive(false);
        fa.SetActive(true);
        getUserData();
        double[,] sums = new double[30, 2];


        for (int i = 0; i < temp; i++)
        {
            sums[i, 0] = (distance((float)LM.text_latitude, (float)LM.text_longitude, (float)real_users[i].text_latitude, (float)real_users[i].text_longitude));
            sums[i, 1] = i;
        }

        for (int i = 0; i < temp - 1; i++)
        {
            for (int j = 0; j < temp - 1 - i; j++)
            {
                if (sums[j, 0] > sums[j + 1, 0])
                {
                    double t = sums[j, 0];
                    sums[j, 0] = sums[j + 1, 0];
                    sums[j + 1, 0] = t;
                    Debug.Log("ASDSD");
                }
            }
        }
        Debug.Log(real_users[1].user_name);

        int t1 = (int)sums[0, 1];
        //if (UM.u_name == real_users[t1].user_name)
        //    de.text = real_users[t1 + 1].user_name;
        //else
        //    de.text = real_users[t1].user_name;
        if (UM.u_name == real_users[t1].user_name)
        {
            n_name.text = real_users[t1 + 1].user_name;
            for(int i=0; i< real_users[t1 + 1].storys.Count; i++)
            {
                n_storys[i].text = real_users[t1 + 1].storys[i];

            }
        }
        else
        {
            n_name.text = real_users[t1].user_name;
            for (int i = 0; i < real_users[t1].storys.Count; i++)
            {
                n_storys[i].text = real_users[t1].storys[i];

            }
        }
    }

    long temp = 10;
    public long GetUserChildrenCount()
    {
        reference.Child("users").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                temp = snapshot.ChildrenCount;
            }
            else if (task.IsFaulted)
            {
                // Handle the error...
                Debug.Log("getData fail ");
            }
            else
            {
                Debug.Log("getData cancel ");
            }
        });
        return temp;
    }
    public static double distance(float lat1, float lon1, float lat2, float lon2)
    {
        // ACOS(COS(RADIANS(90 - 지점1의위도)) * COS(RADIANS(90 - 지점2의위도)) + SIN(RADIANS(90 - 지점1의위도))
        //* SIN(RADIANS(90 - 지점2의위도)) * COS(RADIANS(지점1의경도 - 지점2의경도))) * 6371
        float dist = Mathf.Acos(Mathf.Cos(Mathf.Deg2Rad * (90 - lat1)) * Mathf.Cos(Mathf.Deg2Rad * (90 - lat2)) + Mathf.Sin(Mathf.Deg2Rad * (90 - lat1))
             * Mathf.Sin(Mathf.Deg2Rad * (90 - lat2)) * Mathf.Cos(Mathf.Deg2Rad * (lon1 - lon2))) * 6371;
        Debug.Log(dist);
        return dist;
    }

}



