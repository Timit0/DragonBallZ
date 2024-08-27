using System;
using System.Linq;
using Godot;

public partial class ButtonToggleAction : ButtonOverride
{
    [Export]
    public string ActionName { get; set; }

    [Export]
    public string PropertyNameInSQLite { get; set; }

    public override void _Ready()
    {
        SettingsSingal.Instance.SaveChange += on_save_changed;
        string inputString = SettingsDbContext.Instance.Get(PropertyNameInSQLite).ToString().Split(" ")[0];
        if (inputString != null && inputString != "")
        {
            InputMap.ActionEraseEvents(this.ActionName);
            InputEventFromString(inputString);

            string inputName = InputMap.Singleton.ActionGetEvents(this.ActionName)[0].AsText();
            this.Text = inputName;
            // GD.Print(inputString);
        }
        base._Ready();
    }

    private async void on_save_changed()
    {
        try
        {
            // string inputString = SettingsDbContext.Instance.Get(PropertyNameInSQLite).ToString().Split(" ")[0];
            string inputString = InputMap.Singleton.ActionGetEvents(ActionName).FirstOrDefault().AsText();
            GD.Print(inputString);
            await SettingsDbContext.Instance.UpdateActionEvent(PropertyNameInSQLite, inputString);
        }
        catch (Exception e)
        {
            GD.Print(e.ToString());
        }
    }

    protected void InputEventFromString(string s)
    {
        InputEventKey inputEventKey = new InputEventKey();
        Enum.TryParse(s, out Key key);
        inputEventKey.Keycode = key;

        InputMap.Singleton.ActionAddEvent(this.ActionName, inputEventKey);
    }
}