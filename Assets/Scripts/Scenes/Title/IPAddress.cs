using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IPAddress : MonoBehaviour
{
    public TMP_InputField inputField;
    private void Start()
    {
        inputField.text = string.Empty;
    }
}
