using UnityEngine;
using Academy.HoloToolkit.Unity;
using System;

public class GameBoardManager : Singleton<GameBoardManager>
{
    public GameObject Mine;
    public GameObject GamePiece;
    
    private static readonly System.Random getrandom = new System.Random();
    //public Vector3 cellSize = new Vector3(1.5f, 0.5f, 1.5f);
    public Vector3 cellSize = new Vector3(15.0f, 5.0f, 15.0f);
    public void GenerateBoard(GameObject floor) {
        GenerateGameBoard(cellSize, 1.1f, floor.GetComponent<SurfacePlane>());
        SpatialMappingManager.Instance.undrawVisibleMesh(); //disable mesh

    }


    public void GenerateGameBoard(Vector3 requiredSize, float clearance, SurfacePlane plane)
    {
        OrientedBoundingBox bb = plane.Plane.Bounds;
        Quaternion orientation = (plane.transform.forward.y < 0) ? (plane.transform.rotation * Quaternion.FromToRotation(-Vector3.forward, Vector3.forward)) : plane.transform.rotation;
        // Note that xy in local plane coordinate system correspond to what would be xz in global space
        Vector3 halfExtents = new Vector3(requiredSize.x, requiredSize.z, requiredSize.y) * 0.5f;
        int xpos = 0;

        for (float y = -bb.Extents.y + halfExtents.y; y <= bb.Extents.y - halfExtents.y; y += 2 * halfExtents.y)
        {
            int ypos = 0;
            for (float x = -bb.Extents.x + halfExtents.x; x <= bb.Extents.x - halfExtents.x; x += 2 * halfExtents.x)
            {
                Vector3 center = plane.transform.position + orientation * new Vector3(x, y, halfExtents.z + clearance);
                Collider[] colliders = Physics.OverlapBox(center, halfExtents, orientation);
                if (colliders.Length == 0)
                {
                    int cellsFromCamera = 2;
                    GameObject cell;
                    if ((Math.Abs(Camera.main.transform.position.x - center.x) <= (2 * halfExtents.x) * cellsFromCamera) ||
                        (Math.Abs(Camera.main.transform.position.z - center.z) <= (2 * halfExtents.z) * cellsFromCamera))
                    {
                        Debug.Log("Within camera range, placing GamePiece");
                        cell = Instantiate(GamePiece, gameObject.transform, true);
                    }
                    else
                    {
                        //replace with fisher yates
                        int isMine = getrandom.Next(1, 3);
                        if (isMine == 1)
                        {
                            Debug.Log("Placing mine");
                            cell = Instantiate(Mine, gameObject.transform, true);
                        }
                        else
                        {
                            Debug.Log("Placing GamePiece");
                            cell = Instantiate(GamePiece, gameObject.transform, true);
                        }
                    }

                    //cell.transform.localScale = 2 * halfExtents;
                    cell.transform.position = center;
                    // might be more effecient to use a dictionary to locate cells than the gameObject.find
                    cell.transform.name = "cell:" + xpos.ToString() + "," + ypos.ToString();
                    cell.transform.transform.rotation = orientation;
                    cell.SetActive(true);
                }
                ypos++;
            }
            xpos++;
        }
    }

}
