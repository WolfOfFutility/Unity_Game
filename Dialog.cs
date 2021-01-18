using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog
{
    public string CharacterName { get; set; }
    public string CharacterClass { get; set; }
    public string Message { get; set; }

    public Dialog()
    {
        // Default
    }

    public Dialog(string cn, string cc, string m)
    {
        this.CharacterName = cn;
        this.CharacterClass = cc;
        this.Message = m;
    }
}
