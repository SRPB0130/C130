using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DamageText : MonoBehaviour
{
    public float floatSpeed = 2f;
    public float duration = 1f;

    private TextMeshPro text;

     void Awake()
    {
        text = GetComponent<TextMeshPro>(); 
    }

    public void Show(int damage)
    {
        text.text = damage.ToString(); 
        Destroy(gameObject, duration);
    }

    void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
        transform.LookAt(Camera.main.transform);

        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }
}
