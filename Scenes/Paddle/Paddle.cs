using Godot;
using System;

public partial class Paddle : Area2D
{
[Export] float _speed = 100.0f;
[Export] float _margin = 50.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionPressed("right") == true) {  //For the right action
			Position += new Vector2(_speed * (float)delta, 0);
		}
		if(Input.IsActionPressed("left") == true){ //For the left action
			Position -= new Vector2(_speed * (float)delta , 0);
		}
		Rect2 vpr = GetViewportRect();  //Rect2 for the Values of the Screen or the View Port
		if(Position.X < vpr.Position.X + _margin){
           Position = new Vector2(vpr.Position.X + _margin , Position.Y); //For the X Position
		}
		if (Position.X > vpr.End.X - _margin){ //For the Y Position
			Position = new Vector2(vpr.End.X - _margin, Position.Y);
		}
	}
}
