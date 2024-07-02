using System;
using Godot;

public partial class Scene : Node2D
{
    public override void _Ready()
    {
        ActorSignals.Instance.BehindActor += on_actor_behind;
        base._Ready();
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

		if(FindChild(emitterName) is Actor)
		{
			emitter = FindChild(emitterName) as Actor;
		}

		if(FindChild(targetName) is Actor)
		{
			target = FindChild(targetName) as Actor;
		}

        if(emitter is null)
        {
            try
            {
                emitter = GetNode(emitterName) as Actor;
            }catch(Exception e)
            {
                GD.Print(e);
            }
            if(emitter is null)
            {
                return;
            }
        }

        if(target is null)
        {
            try
            {
                target = GetNode(targetName) as Actor;
            }catch(Exception e)
            {
                GD.Print(e);
            }
            if(target is null)
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
}