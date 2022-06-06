using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : RewindableObject
{
    [SerializeField] private bool startDirectionRight = true;
    public float speed = 5f;
    public bool isOn;

    static Vector4 defaultVisualSpeed = new Vector4(0, 0.25f, 0, 0);

    static Color regularColor = new Color(1, 1, 0, 1);
    static Color reverseColor = new Color(1, 0, 1, 1);

    private List<GameObject> targetList;

    bool firstPassRewind, firstPassRegular;

    [SerializeField] List<Material> conveyorRotatorMaterials, conveyorArrowMaterials;

    private void Start()
    {
        gameObject.tag = "Rewindable";

        GrabMaterials();

        // Prevent the belt to reverse itself at the start
        firstPassRegular = true;

        targetList = new List<GameObject>();
        if (!startDirectionRight) 
        {
            ReverseConveyorDirection();
        }

        SetStateAction();
    }

    protected override void DoAction()
    {
        // First frame only
        if (!firstPassRegular)
        {
            firstPassRegular = true;
            firstPassRewind = false;

            ReverseConveyorDirection();
        }


        if (isOn)
        {
            if (targetList.Count == 0) return;

            foreach (GameObject currentGameObject in targetList)
            {
                currentGameObject.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            }
        }
        
    }

    void GrabMaterials()
    {
        foreach (Transform _child in transform)
        {
            if (_child.name.Contains("BODY"))
            {
                conveyorRotatorMaterials.Add(_child.GetComponent<MeshRenderer>().materials[1]);
                conveyorArrowMaterials.Add(_child.GetComponent<MeshRenderer>().materials[2]);
            }
            else if (_child.name.Contains("TAIL") || _child.name.Contains("HEAD"))
            {
                conveyorRotatorMaterials.Add(_child.GetComponent<MeshRenderer>().materials[0]);
                conveyorArrowMaterials.Add(_child.GetComponent<MeshRenderer>().materials[3]);
            }
        }
    }

    public void ReverseConveyorDirection()
    {
        speed = -speed;


        // 1 for right or -1 for left
        float sign = Mathf.Sign(speed);
        
        foreach (Material _material in conveyorArrowMaterials)
        {
            // Incrase the value by 180 then resets it to 0 if it is 360 (so do a 180� flip)
            _material.SetFloat("Rotation", (_material.GetFloat("Rotation") + 180) % 360);

            if (sign == -1)
            {
                _material.SetColor("Color", reverseColor);
            }
            else
            {
                _material.SetColor("Color", regularColor);
            }

        }
        foreach (Material _material in conveyorRotatorMaterials)
        {
            _material.SetVector("Speed", defaultVisualSpeed * sign);
        }

        
    }

    protected override void DuringRewind()
    {
        // First frame only
        if (!firstPassRewind)
        {
            firstPassRewind = true;
            firstPassRegular = false;

            ReverseConveyorDirection();


        }

        if (isOn)
        {
            if (targetList.Count == 0) return;

            foreach (GameObject currentGameObject in targetList)
            {
                currentGameObject.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("SBlock")) 
            targetList.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        if(targetList.Contains(collision.gameObject)) 
            targetList.Remove(collision.gameObject);
    }
}