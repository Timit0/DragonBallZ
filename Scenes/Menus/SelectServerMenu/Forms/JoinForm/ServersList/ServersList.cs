using Godot;
using System;
using System.Collections.Generic;

public partial class ServersList : Control
{
    [Export]
    protected VBoxContainer vBoxContainer {get;set;}

    [Export]
    protected Button button {get;set;}

    public override void _Ready()
    {
        button.Pressed += on_refresh_button_pressed;

        RemoveAllServers();
        AddServerList();

        base._Ready();
    }

    private void on_refresh_button_pressed()
    {
        RemoveAllServers();
        AddServerList();
    }

    protected async void AddServerList()
    {
        List<HostServer> hostServers = await ApiSingleton.Instance.PostOnApiGetAllAvailableHostServer();
        foreach (HostServer hostServer in hostServers)
        {
            vBoxContainer.AddChild(FactorySingleton.Instance.AddServerWidget(hostServer));
        }
    }

    protected void RemoveAllServers()
    {
        foreach (Node node in vBoxContainer.GetChildren())
        {
            node.QueueFree();
        }
    }
}
