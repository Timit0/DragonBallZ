using System;
using System.Collections.Generic;
using System.Net.Http;
using Godot;

public partial class ConnectionFormOverride : Control
{
    [ExportGroup("API")]
    [Export]
    protected string ip { get; set; } = "http://127.0.0.1:8080";
    [Export]
    protected string apiRoute { get; set; }

    [ExportGroup("Nodes")]
    [Export]
    protected LineEdit lineEditUsername { get; set; }
    [Export]
    protected LineEdit lineEditPassword { get; set; }

    [Export]
    protected Button actionButton { get; set; }

    protected System.Net.Http.HttpClient httpClient { get; set; } = new System.Net.Http.HttpClient();

    protected FormUrlEncodedContent dataToSend { get; set; }

    public override void _Ready()
    {
        httpClient.BaseAddress = new Uri(this.ip);
        actionButton.Pressed += on_action_button_pressed;
        base._Ready();
    }

    protected virtual void on_action_button_pressed()
    {
        this.lineEditUsername.Text = "";
        this.lineEditPassword.Text = "";
    }
}