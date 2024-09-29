extends Node


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if Input.is_action_just_pressed("Left"):
		var root=get_tree().current_scene
		var node=root.get_node("Node2D2")
		if node!=null:
			node.queue_free()
