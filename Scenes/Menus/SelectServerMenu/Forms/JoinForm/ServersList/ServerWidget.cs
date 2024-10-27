using Godot;
using System;

public partial class ServerWidget : Control
{
    [Export]
    public Label Label {get;set;}

    public HostServer HostServer {get;set;}
}
