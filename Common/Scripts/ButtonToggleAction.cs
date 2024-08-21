using Godot;

public partial class ButtonToggleAction : ButtonOverride
{
    [Export]
    public string ActionName { get; set; }

    protected bool toggle_mode { get; set; } = true;

    public override void _Ready()
    {
        this.SetProcessUnhandledInput(false);
        base._Ready();
    }

    public override void _Toggled(bool toggledOn)
    {
        GD.Print("agoo");
        SetProcessUnhandledInput(toggledOn);
        if (toggledOn)
        {
            this.Text = "Await";
        }

        base._Toggled(toggledOn);
    }

    public override void _Pressed()
    {
        this.Text = "Await";
        base._Pressed();
    }
}