    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;
    public float fallTime = 0.8f;
    public static int height = 20;
    public static int width = 10;
    private static Transform[,] grid = new Transform[width, height];
    private int lines;
    private bool moveLeft = false, moveRight = false, rotate = false;
    

    void Start(){
        FindObjectOfType<TetrisManager>().currentBlock = this;
    }

    public void MoveLeft(){
        moveLeft = true;
    }
    public void MoveRight(){
        moveRight = true;
    }

    public void Rotate() {
        rotate = true;
    }


    void Update(){
        if (Input.GetKeyDown(KeyCode.LeftArrow) || moveLeft)
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(-1, 0, 0);
            moveLeft = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || moveRight)
        {
            transform.position -= new Vector3(-1, 0, 0);
            if (!ValidMove())
                transform.position += new Vector3(-1, 0, 0);
            moveRight = false;
            
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || rotate)
        {
            //rotate !
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,0,1), 90);
            if (!ValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            rotate = false;

        }
        if (Time.time - previousTime > (Input.GetKeyDown(KeyCode.DownArrow) ? fallTime / 10 : fallTime)){
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove()){
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();
                this.enabled = false;
                FindObjectOfType<SpawnTetromino>().NewTetromino();
            }
            previousTime = Time.time;
        }

    }

    private void OnDrawGizmos(){
        // Draw a red sphere at the rotation point
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.TransformPoint(rotationPoint), 1f);
    }

    void CheckForLines(){
        lines = 0;
        for (int i = height -1; i >=0; i--){
            if (HasLine(i)) {
                lines++;
            }
        }
        FindObjectOfType<TetrisManager>().AddLine(lines);
    }

    private void RowDown(int i){
        for (int y = i; y < height; y = y++){
            for (int j = 0; j < width; j++) {
                if (grid[j,y] != null) {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position = new Vector3(0, 1, 0);
                }
            }
        }
    }

    private void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, 1] = null;
        }
    }

    bool HasLine(int i) {
        for (int j = 0; j < width; j++){
            if (grid[j,i] == null){
                return false;
            }
        }
        return true;
    }

    void AddToGrid(){
        foreach (Transform children in transform){
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }
    }

    bool ValidMove(){
        foreach (Transform children in transform) {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height){
                return false;
            }
            if(grid[roundedX, roundedY] != null){
                return false;
            }
        }
        return true;
    }
}
