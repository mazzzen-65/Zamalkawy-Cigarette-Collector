using Godot;
using System;

public partial class Game : Node2D
{
	// Called when the node enters the scene tree for the first time.
	[Export] private PackedScene _gemScene;
	[Export] private Timer _spawnTimer;
	[Export] float _margin = 50.0f;

    [Export] private Label _scoreLabel;
	[Export] private AudioStreamPlayer _music;
	[Export] private AudioStreamPlayer2D _effects;

	[Export] private AudioStream _explodeEffect;
	[Export] private Label _gameOverLabel;


	private int _score = 0;
	public override void _Ready()
	{
	   _spawnTimer.Timeout += SpawnGem;
	   _gameOverLabel.Visible = false;
       SpawnGem();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void SpawnGem(){
		Rect2 vpr = GetViewportRect();
		float rX = (float)GD.RandRange(vpr.Position.X + _margin , vpr.End.X - _margin);
		Gem gem = (Gem)_gemScene.Instantiate();
		AddChild(gem);
		gem.Position = new Vector2(rX , -100);
		gem.OnScored += OnScored;
		gem.OnGemOffScreen += GameOver;
	}

	private void OnScored(){
		GD.Print("Signal on Scored");
		_score ++;
		_scoreLabel.Text = _score.ToString();
		_effects.Play();
	}
    private void GameOver(){
      GD.Print("Game Over");
	  foreach (Node node in GetChildren()){
		node.SetProcess(false);
	  }
	  _spawnTimer.Stop();
	  _music.Stop();
      _effects.Stop();
	  _effects.Stream = _explodeEffect;
	  _effects.Play();
	  ToggleGameOverLabel();
	}
    private void ToggleGameOverLabel(){
		_gameOverLabel.Visible = !_gameOverLabel.Visible;

	}

}
