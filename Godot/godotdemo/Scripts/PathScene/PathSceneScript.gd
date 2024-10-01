extends Node


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var path=ProjectSettings.globalize_path("user://")
	print(path)
	var path2=ProjectSettings.localize_path("user://")
	print(path2)
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
