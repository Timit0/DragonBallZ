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

    public GodotObject MainCamera
    {
        get
        {
            return FindChild("MainCamera") as GodotObject;
        }
        set
        {
            this.MainCamera = value;
        }
    }

    public GodotObject ZoomedCamera
    {
        get
        {
            return FindChild("ZoomedCamera") as GodotObject;
        }
        set
        {
            this.ZoomedCamera = value;
        }
    }

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
        switch (enumInt)
        {
            case (int)CamList.MAIN_CAM:
                CameraManagerGDS.Call("prepare", MainCamera);
                CameraManagerGDS.Call("remove_all_followed_group");
                CameraManagerGDS.Call("add_follow_target", GetParent());
                break;
            case (int)CamList.ZOOMED_CAM:
                CameraManagerGDS.Call("prepare", ZoomedCamera);
                CameraManagerGDS.Call("remove_all_followed_group");
                CameraManagerGDS.Call("add_follow_target", GetParent());
                break;
        }
        CameraManagerGDS.Call("cam_active_is", true);
    }

    private Node2D GetPlayer()
    {
        return GetParent().GetParent() as Node2D;
    }

    // private void on_basic_cam()
    // {
    //     CameraManagerGDS.Call("prepare", BasicCam);
    //     CameraManagerGDS.Call("remove_all_dialogue_followed_group");
    //     CameraManagerGDS.Call("add_group", GetBasicOfScene().GroupToFollow);
    //     CameraManagerGDS.Call("cam_active_is", true);
    // }

    // private void on_dialogue_cam(Array<GodotObject> actorsToFollow)
    // {
    //     CameraManagerGDS.Call("prepare", DialogueCam);
    //     CameraManagerGDS.Call("remove_all_followed_group");
    //     CameraManagerGDS.Call("add_group", actorsToFollow);
    //     CameraManagerGDS.Call("cam_active_is", true);
    // }
}