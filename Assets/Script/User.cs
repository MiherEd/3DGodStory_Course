using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//�ǦC��
[Serializable]
public class User
{
    //�ϥΪ̱b��
    public string UserAccount;
    //�ϥΪ̱K�X
    public string UserPassword;
    //�ϥΪ̦W��
    public string UserName;
    //�ϥΪ̱o��
    public int Score;
    
    public User()
    {
        UserAccount = staticvar.UserAccount;
        UserPassword = staticvar.UserPassword;
        UserName = staticvar.UserName;
        Score = staticvar.Score;
    }
}
