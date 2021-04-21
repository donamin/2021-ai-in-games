using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour {

    public Color tileColor;
    private Board board;
    private Point coord;

    private GameManager gameManager;

    void Awake()
    {
        board = GameObject.Find("Board").GetComponent<Board>();
        gameManager = GameObject.Find("Managers").GetComponent<GameManager>();
	
       
    }

    void Start()
    {
        tileColor = GetComponent<Renderer>().material.color;
        board.state.Count++;

        coord = new Point((board.state.Count - 1) /board.state.Size, (board.state.Count - 1) % board.state.Size);
    }
	
    #region Mouse input methods
    void OnMouseEnter()
    {
        if (!board.state.GameOver)
        {
            // Highlights the tile as yellow
            GetComponent<Renderer>().material.color = Color.yellow;
        }
    }

    void OnMouseExit()
    {
        // Sets the tile back to its original color
        GetComponent<Renderer>().material.color = tileColor;
    }

	public void setColour(Color c)
	{
		tileColor = c;
		GetComponent<Renderer>().material.color = tileColor;
	}

    void OnMouseUpAsButton()
    {
		if (board.state.turn == BoardState.GameTurn.Player && !board.state.GameOver)
        {
            gameManager.SetInput(coord);
        }
    }
    #endregion Mouse input methods
}
