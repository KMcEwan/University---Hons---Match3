using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour
{
    public gridController gridCon;
    private int rowsOfGrid;
    private int columnsOfGrid;

    // Icon positions
    [SerializeField] private int iconPositionX;                                             
    [SerializeField] private int iconPositionY;

    // Swipe icons
    private Vector2 iconsPostionAtTouch;
    private Vector2 iconPostionAtRelease;
    private float swipeAngle = 0;
    public IconController otherIcon;
    public float iconMovementSpeed = 5;


    private Vector2 tempPostion;


    void Start()
    {
        iconPositionX = (int)transform.position.x;                                          // assign position to icon
        iconPositionY = (int)transform.position.y;
        Debug.Log(iconPositionX);
    }

    // Update is called once per frame
    void Update()
    {
        if((Mathf.Abs(transform.position.x - iconPositionX) > .01) || Mathf.Abs(transform.position.y - iconPositionY) > 0.01)                           // Stop lerp
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(iconPositionX, iconPositionY), iconMovementSpeed * Time.deltaTime);
            gridCon.iconLocations[iconPositionX, iconPositionY] = this;
        }
        else
        {
            transform.position = new Vector2(iconPositionX, iconPositionY);                                                                             
        }

    }

    private void OnMouseDown()
    {
        iconsPostionAtTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        iconPostionAtRelease = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        calculateAngle();
    }

    private void calculateAngle()
    {
        swipeAngle = Mathf.Atan2(iconPostionAtRelease.y - iconsPostionAtTouch.y, iconPostionAtRelease.x - iconsPostionAtTouch.x);
        swipeAngle = swipeAngle * Mathf.Rad2Deg;
      //   Debug.Log(swipeAngle);

        if(Vector3.Distance(iconsPostionAtTouch, iconPostionAtRelease) > 0.5)
        {
            moveIcons();
        }
       
    }

    public void attachGrid(gridController grid, int gridRows, int gridColumns)
    {
        gridCon = grid;
        rowsOfGrid = gridRows;
        columnsOfGrid = gridColumns;
    }

    private void moveIcons()
    {
        //Debug.Log("move icons");
        if(swipeAngle <= 45 && swipeAngle >= -45 && iconPositionX < rowsOfGrid)                                         // Right swipe
        {
            //Debug.Log("more/less 45");
            otherIcon = gridCon.iconLocations[iconPositionX + 1 , iconPositionY];
            otherIcon.iconPositionX--;
            iconPositionX++;
        }
        else if(swipeAngle > 45 && swipeAngle <= 135 && iconPositionY < columnsOfGrid)                                  // Up swipe
        {
            otherIcon = gridCon.iconLocations[iconPositionX , iconPositionY + 1];
            otherIcon.iconPositionY--;
            iconPositionY++;
        }
        else if (swipeAngle > 135 || swipeAngle <= -135 && iconPositionX > 0)                                            // Left swipe
        {
            otherIcon = gridCon.iconLocations[iconPositionX - 1, iconPositionY ];
            otherIcon.iconPositionX++;
            iconPositionX--;
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && iconPositionY > 0)                                            // Down swipe
        {
            otherIcon = gridCon.iconLocations[iconPositionX, iconPositionY - 1];
            otherIcon.iconPositionY++;
            iconPositionY--;
        }

    }
}
