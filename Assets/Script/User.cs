using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//序列化
[Serializable]
public class User
{
    //使用者帳號
    public string UserAccount;
    //使用者密碼
    public string UserPassword;
    //使用者名稱
    public string UserName;
    //使用者得分
    public int Score;
    
    public User()
    {
        UserAccount = staticvar.UserAccount;
        UserPassword = staticvar.UserPassword;
        UserName = staticvar.UserName;
        Score = staticvar.Score;
    }
}
