class_name CameraManager extends Node

static var cam: PhantomCamera2D

##You must call prepare before call any other func

static func prepare(obj: Object) -> void:
	cam = obj
	
static func add_group(actors_array: Array) -> void:
	var arr: Array[Node2D] = []
	arr.append_array(actors_array)
	for element in arr:
		cam.erase_follow_targets(element)
	cam.append_follow_targets_array(arr)

static func remove_in_group(actor: Object):
	cam.erase_follow_targets(actor)

static func remove_all_followed_group():
	cam.erase_follow_target()

static func add_follow_target(target: Node2D) -> void:
	cam.set_follow_target(target)

static func cam_active_is(is_active: bool) -> void:
	if (is_active):
		#dialogue_cam.became_active.emit()
		cam.set_priority(50)
	else:
		#dialogue_cam.became_inactive.emit()
		cam.set_priority(0)
