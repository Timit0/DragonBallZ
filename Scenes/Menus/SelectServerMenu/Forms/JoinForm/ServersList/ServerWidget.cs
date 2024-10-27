using Godot;
using System;

public partial class ServerWidget : Control
{
    [Export]
    public Label Label {get;set;}
    [Export]
    protected Button button {get;set;}
    [Export]
    protected Resource joinButtonTarget {get;set;}

    public HostServer HostServer {get;set;} = new HostServer();

    public override void _Ready()
    {
        button.Pressed += on_button_join_pressed;

        this.Label.Text = this.HostServer.UserHost.Username;
        base._Ready();
    }

    private void on_button_join_pressed()
    {
        ServerConfigSingleton.Instance.ServerMode = ServerConfigSingleton.ConfigServerEnum.JOIN;
		ServerConfigSingleton.Instance.IpAdresse = this.HostServer.HostIp;
		SceneSignals.Instance.EmitSignal(nameof(SceneSignals.Instance.ChangeToThisScene), joinButtonTarget.ResourcePath);
    }

}
