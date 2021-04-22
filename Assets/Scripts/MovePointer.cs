using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MovePointer : MonoBehaviour
{
    [SerializeField]
    float speedOfPointer, pointerWaitSeconds, turningSpeed;
    public Transform [] targets;

    string value;
    int holder;
    bool interruption, isWaiting;
    Vector3 position, forw;

    void Start()
    {
        position = transform.position;
        forw = transform.forward;
        value = "";
        holder = 0;
    }
    
    void Update()
    {
        if (!isWaiting)
            Updater();
    }

    void Updater()
    {
        if (holder < value.Length && !interruption)
        {
            int index = FindIndexOfTarget();
            float step = speedOfPointer * Time.deltaTime * Mathf.Sqrt(Vector3.Distance(transform.position, targets[index].position));
            transform.position = Vector3.MoveTowards(transform.position, targets[index].position, step);
            if (Vector3.Distance(transform.position, targets[index].position) < 0.1f)
            {
                WaitSeconds();
            }

            transform.eulerAngles = targets[index].eulerAngles;

            /*
            float currentDistance, generalDistance, rate;
            currentDistance = Vector3.Distance(targets[index].position, transform.position);
            generalDistance = Vector3.Distance(targets[index].position, position);
            if (currentDistance < 0.000000001f)
                currentDistance = 0.000000001f;
            rate = currentDistance / generalDistance;
            float angleBetween = Mathf.Atan2(targets[index].forward.z, targets[index].forward.x) * Mathf.Rad2Deg - Mathf.Atan2(forw.z, forw.x) * Mathf.Rad2Deg;
            if (angleBetween < 180 || angleBetween > -180)
                angleBetween *= -1;
            transform.eulerAngles = new Vector3(0, (-1) * Mathf.Atan2(forw.z, forw.x) * Mathf.Rad2Deg + 90 + (angleBetween * rate), 0);
            Debug.Log("Tan Degree: " + (Mathf.Atan2(forw.z, forw.x) * Mathf.Rad2Deg - 90));*/
        }
        else if (interruption)
        {
            TurnAround();
        }
    }

    private int FindIndexOfTarget()
    {
        int val = (int)(value[holder]);
        if (val > 47 && val < 58)
            return val - 22;
        else
            return val - (int)'a';
    }

    void TurnAround()
    {
        isWaiting = true;
        StartCoroutine(TurnAroundAsync());
    }

    IEnumerator TurnAroundAsync()
    {
        float y = transform.eulerAngles.y;
        for (int i = 0; i < 360 / turningSpeed; i++)
        {
            transform.eulerAngles = new Vector3(0, y + i * turningSpeed, 0);
            yield return null;
        }
        transform.eulerAngles = new Vector3(0, y, 0);
        yield return new WaitForSeconds(0.1f);
        interruption = false;
        isWaiting = false;
    }

    void WaitSeconds()
    {
        isWaiting = true;
        StartCoroutine(WaitSecondsAsync());
    }

    IEnumerator WaitSecondsAsync()
    {
        yield return new WaitForSeconds(pointerWaitSeconds);
        isWaiting = false;
        holder++;
        if (holder < value.Length && value[holder] == value[holder - 1])
            interruption = true;
        position = transform.position;
        forw = transform.forward;
    }

    public void setValue(Text text)
    {
        this.value = text.text.ToLower().Replace(" ", "");
        holder = 0;
    }
}
