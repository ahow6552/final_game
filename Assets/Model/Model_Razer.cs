using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RaycastGun : MonoBehaviour
{
    public Transform laserOrigin;
    [SerializeField] private List<Vector3> laserPoint = new List<Vector3>();
    [SerializeField] private int reflectNum;

    LineRenderer laserLine;
    Vector3 dir = new Vector3(1, 0, 0);

    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
        laserLine.material.color = Color.white;
    }

    void Update()
    {
        StartCoroutine(ShootLaser());
        CastLaser();
        laserLine.positionCount = laserPoint.Count;
        laserLine.SetPositions(laserPoint.ToArray());
    }

    void CastLaser()
    {
        laserPoint.Clear();
        int i = 0;
        Vector3 rayOrigin = this.transform.position;
        dir = new Vector3(1, 0, 0);
        laserPoint.Add(laserOrigin.position);
        reflectNum = 1;
        RaycastHit hit;
        do
        {
            if (Physics.Raycast(rayOrigin, dir, out hit, 200f) && hit.transform.CompareTag("door"))
            {
                laserPoint.Add(hit.point);
                Debug.Log("U hit door");
            }
            else if(Physics.Raycast(rayOrigin, dir, out hit, 200f) && hit.transform.CompareTag("wall"))
            {
                laserPoint.Add(hit.point);
            }
            else if (Physics.Raycast(rayOrigin, dir, out hit, 200f))
            {
                laserPoint.Add(hit.point);
                reflectNum++;
            }
            else
            {
                laserPoint.Add(laserOrigin.position + dir * 15f);
            }
            dir = Vector3.Reflect(hit.point - rayOrigin, hit.normal);
            rayOrigin = hit.point + dir * 0.1f;
            ++i;
        } while (i < reflectNum);
    }

    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(0.1f);
    }
}