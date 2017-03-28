using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject topLeftGO;
    public GameObject topRightGO;
    public GameObject botLeftGO;
    public GameObject botRightGO;
    public GameObject leftGO;
    public GameObject rightGO;
    public GameObject topGO;
    public GameObject botGO;

    public Sprite topLeft;
    public Sprite topRight;
    public Sprite botLeft;
    public Sprite botRight;
    public Sprite left;
    public Sprite right;
    public Sprite top;
    public Sprite bot;

    public GameObject spawnParent;

    Vector2 prevScreenSize;

    public EdgeCollider2D col;

	void Start ()
    {
        topLeft = topLeftGO.GetComponent<SpriteRenderer>().sprite;
        topRight = topRightGO.GetComponent<SpriteRenderer>().sprite;
        botLeft = botLeftGO.GetComponent<SpriteRenderer>().sprite;
        botRight = botRightGO.GetComponent<SpriteRenderer>().sprite;
        left = leftGO.GetComponent<SpriteRenderer>().sprite;
        right = rightGO.GetComponent<SpriteRenderer>().sprite;
        top = topGO.GetComponent<SpriteRenderer>().sprite;
        bot = botGO.GetComponent<SpriteRenderer>().sprite;

        prevScreenSize = new Vector2(Screen.width, Screen.height);
        Generate();
    }
	
	void Update ()
    {
        if (prevScreenSize.x != Screen.width || prevScreenSize.y != Screen.height)
        {
            Regenerate();
        }
    }

    void Regenerate()
    {
        List<Transform> childs = new List<Transform>();
        for (int i = 0; i != spawnParent.transform.childCount; i++)
        {
            childs.Add(spawnParent.transform.GetChild(i));
        }

        for (int i = 0; i != childs.Count; i++)
        {
            Destroy(childs[i].gameObject);
        }

        Generate();
    }

    void Generate()
    {
        // move corner sprites
        topLeftGO.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Screen.height, 0.0f));
        topRightGO.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        botLeftGO.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f));
        botRightGO.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f));

        Vector2[] colPoints = new Vector2[5];

        colPoints[0].x = topLeftGO.transform.position.x + (topLeft.bounds.extents.x * 2.0f);
        colPoints[0].y = topLeftGO.transform.position.y - (topLeft.bounds.extents.y * 2.0f);

        colPoints[1].x = topRightGO.transform.position.x - (topRight.bounds.extents.x * 2.0f);
        colPoints[1].y = topRightGO.transform.position.y - (topRight.bounds.extents.y * 2.0f);

        colPoints[2].x = botRightGO.transform.position.x - (botRight.bounds.extents.x * 2.0f);
        colPoints[2].y = botRightGO.transform.position.y + (botRight.bounds.extents.y * 2.0f);

        colPoints[3].x = botLeftGO.transform.position.x + (botLeft.bounds.extents.x * 2.0f);
        colPoints[3].y = botLeftGO.transform.position.y + (botLeft.bounds.extents.y * 2.0f);

        colPoints[4].x = topLeftGO.transform.position.x + (topLeft.bounds.extents.x * 2.0f);
        colPoints[4].y = topLeftGO.transform.position.y - (topLeft.bounds.extents.y * 2.0f);

        col.points = colPoints;


        float extentScalar = 0.98f;

        // create walls
        Vector3 topLeftScreenWorld = topLeftGO.transform.position;
        Vector3 botRightScreenWorld = botRightGO.transform.position;

        float curYPos = botRightScreenWorld.y - left.bounds.extents.y;
        while (curYPos < topLeftScreenWorld.y)
        {
            // left wall
            GameObject newObj = Instantiate(leftGO);
            Vector3 newObjPos = newObj.transform.position;
            newObjPos.y = curYPos;
            newObjPos.x = topLeftScreenWorld.x;
            curYPos += left.bounds.extents.y * extentScalar;

            newObj.transform.position = newObjPos;
            newObj.transform.SetParent(spawnParent.transform);

            //right wall
            newObj = Instantiate(rightGO);
            newObjPos = newObj.transform.position;
            newObjPos.y = curYPos;
            newObjPos.x = botRightScreenWorld.x;
            curYPos += right.bounds.extents.y * extentScalar;

            newObj.transform.position = newObjPos;
            newObj.transform.SetParent(spawnParent.transform);
        }


        float curXPos = topLeftScreenWorld.x - bot.bounds.extents.x;
        while (curXPos < botRightScreenWorld.x)
        {
            // top
            GameObject newObj = Instantiate(topGO);
            Vector3 newObjPos = newObj.transform.position;
            newObjPos.y = topLeftScreenWorld.y;
            newObjPos.x = curXPos;
            curXPos += top.bounds.extents.x * extentScalar;

            newObj.transform.position = newObjPos;
            newObj.transform.SetParent(spawnParent.transform);

            // bot
            newObj = Instantiate(botGO);
            newObjPos = newObj.transform.position;
            newObjPos.y = botRightScreenWorld.y;
            newObjPos.x = curXPos;
            curXPos += bot.bounds.extents.x * extentScalar;

            newObj.transform.position = newObjPos;
            newObj.transform.SetParent(spawnParent.transform);
        }
    }
}
