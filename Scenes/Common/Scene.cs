using System;
using Godot;

public partial class Scene : Node2D
{
    public override void _Ready()
    {
        ActorSignals.Instance.BehindActor += on_actor_behind;
        base._Ready();
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("pause_menu"))
        {
            foreach (Node node in GetChildren())
            {
                if (node is PauseMenu)
                {
                    return;
                }
            }
            this.AddChild(FactorySingleton.Instance.GetThisNodeInstantiateFromString<PauseMenu>("res://Scenes/UI/PauseMenu/PauseMenu.tscn"));
        }

        if (Input.IsActionJustPressed("zoomed_camera"))
        {
            CameraSignals.Instance.EmitSignal(nameof(CameraSignals.Instance.ActiveThisCamera), (int)global::CameraManager.CamList.ZOOMED_CAM);
        }

        if (Input.IsActionJustReleased("zoomed_camera"))
        {
            CameraSignals.Instance.EmitSignal(nameof(CameraSignals.Instance.ActiveThisCamera), (int)global::CameraManager.CamList.MAIN_CAM);
        }

        base._Input(@event);
    }

    /// <summary>
    /// If an actor is behind another this function will put it visually behind.
    /// It's gest also the ZIndex if an actor is behind another.
    /// </summary>
    /// <param name="emitterName"></param>
    /// <param name="targetName"></param>
    public void on_actor_behind(string emitterName, string targetName)
    {
        // if(SceneSingleton.Instance.ActualScenePath != this.Path)
        // {
        //     return;
        // }
        //Is Forward
        Actor emitter = null;
        //Is Behind
        Actor target = null;

        try
        {
            if (FindChild(emitterName) is Actor)
            {
                emitter = FindChild(emitterName) as Actor;
            }
        }
        catch (Exception e) { }

        try
        {
            if (FindChild(targetName) is Actor)
            {
                target = FindChild(targetName) as Actor;
            }
        }
        catch (Exception e) { }

        if (emitter is null)
        {
            emitter = GetActor(emitterName);
            if (emitter is null)
            {
                return;
            }
        }

        if (target is null)
        {
            target = GetActor(targetName);
            if (target is null)
            {
                return;
            }
        }

        //If emitter is behind target --> target go to behind
        if (emitter.GetIndex() < target.GetIndex())
        {
            this.MoveChild(target, emitter.GetIndex());
            return;
        }

        if (emitter.ZIndex != (int)global::ZIndex.ZIndexEnum.ACTOR)
        {
            target.ZIndex = emitter.ZIndex;
        }
    }

    public Actor GetActor(string name)
    {
        foreach (Node node in this.GetChildren())
        {
            if (node is Actor actor && actor.Name == name)
            {
                return actor;
            }
        }
        return null;
    }
}