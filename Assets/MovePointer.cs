using UnityEngine;
using UnityEngine.UI;

public class MovePointer : MonoBehaviour
{
    public float speedOfPointer = 1f;
    public Transform [] targets;
    private Vector3 vector3;

    private string value = "";
    private int holder = 0;
    private bool interruption;
    void Start()
    {
        vector3 = transform.position;
        Debug.Log((int)('a'));
        Debug.Log((int)('z'));
        Debug.Log((int)('A'));
        Debug.Log((int)('Z'));
    }
    
    void Update()
    {
        if(holder < value.Length && interruption){
            float step = speedOfPointer * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targets[(int)(value[holder] - 'a')].position, step);
            if (Vector3.Distance(transform.position, targets[(int)(value[holder] - 'a')].position) < 0.001f){
                holder++;
                if (holder < value.Length)
                    interruption = false;
            }
        }
        else if (!interruption){
            float step = speedOfPointer * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, vector3, step);
            if (Vector3.Distance(transform.position,vector3) < 0.001f)
                interruption = true;
        }
    }

    public void setValue(Text text)
    {
        this.value = text.text;
        holder = 0;
    }
}
