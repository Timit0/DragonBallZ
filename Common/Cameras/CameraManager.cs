using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class CameraManager : Node
{
    public enum CamList
    {
        MAIN_CAM,
        ZOOMED_CAM
    }

    [Export]
    public Node MainCamera { get; set; }

    [Export]
    public Node ZoomedCamera { get; set; }

    private GodotObject _cameraManagerGDS = null;
    public GodotObject CameraManagerGDS
    {
        get
        {
            if (_cameraManagerGDS == null)
            {
                GDScript cameraScript = GD.Load<GDScript>("res://Common/Cameras/CameraManager.gd");
                _cameraManagerGDS = (GodotObject)cameraScript.New();
            }
            return _cameraManagerGDS;
        }
        set
        {
            this._cameraManagerGDS = value;
        }
    }
    public override void _Ready()
    {
        CameraSignals.Instance.ActiveThisCamera += on_active_this_camera;

        CameraSignals.Instance.EmitSignal(nameof(CameraSignals.Instance.ActiveThisCamera), (int)CamList.MAIN_CAM);

        base._Ready();
    }

    private void on_active_this_camera(int enumInt)
    {
        try
        {
            Node2D player = GetPlayer();
            switch (enumInt)
            {
                case (int)CamList.MAIN_CAM:
                    CameraManagerGDS.Call("prepare", MainCamera);
                    CameraManagerGDS.Call("remove_all_followed_group");
                    CameraManagerGDS.Call("add_follow_target", player);
                    break;
                case (int)CamList.ZOOMED_CAM:
                    CameraManagerGDS.Call("prepare", ZoomedCamera);
                    CameraManagerGDS.Call("remove_all_followed_group");
                    CameraManagerGDS.Call("add_follow_target", player);
                    break;
            }
            CameraManagerGDS.Call("cam_active_is", true);
        }
        catch (Exception e)
        {
            // GD.PrintErr(e.Message);
        }
    }

    private Node2D GetPlayer()
    {
        while (true)
        {
            if (IsInstanceValid(GetParent()))
            {
                return GetParent() as Node2D;
            }
        }
    }
}