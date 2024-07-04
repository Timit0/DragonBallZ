using Godot;
using System;

public partial class PlayerSkinCase : Control
{
	[ExportGroup("Textures")]
	[Export]
	protected Texture2D DefaultPanelTexture {get;set;}
	[Export]
	protected Texture2D SelectedPanelTexture {get;set;}
	[Export]
	public AtlasTexture SkinTexture {get;set;}


	[ExportGroup("Nodes")]
	[Export]
	protected TextureRect textureRect {get;set;}

	[Export]
	protected Panel panel {get;set;}
	[ExportGroup("")]

	public PlayerModel PlayerModel {get;set;} = new PlayerModel();

	protected StyleBoxTexture styleBoxTexture {get;set;} = new StyleBoxTexture();

	public bool ContainMouse {get;set;}

    public override void _Ready()
    {
		textureRect.Texture = SkinTexture;

		panel.MouseEntered += on_mouse_entered;
		panel.MouseExited += on_mouse_exited;

        base._Ready();
    }

    private void on_mouse_entered()
    {
        styleBoxTexture.Texture = SelectedPanelTexture;
		panel.AddThemeStyleboxOverride("panel", styleBoxTexture);
		ContainMouse = true;
    }


    private void on_mouse_exited()
    {
        styleBoxTexture.Texture = DefaultPanelTexture;
		panel.AddThemeStyleboxOverride("panel", styleBoxTexture);
		ContainMouse = false;
    }

    public override void _Input(InputEvent @event)
    {
		if(Input.IsActionJustPressed("mouse_left_click") && ContainMouse)
		{
			PlayerSingleton.Instance.PlayerModel.SkinTexture = GD.Load<Texture2D>(SkinTexture.Atlas.ResourcePath);
			SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), "res://Scenes/TestScene/TestScene.tscn");
		}

        base._Input(@event);
    }
}
