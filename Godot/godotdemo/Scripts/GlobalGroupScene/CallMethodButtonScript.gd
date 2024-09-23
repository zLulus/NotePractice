extends Node


func _on_pressed() -> void:
	get_tree().call_group("GlobalTestGroup1", "CallGlobalTestGroup1")
	pass # Replace with function body.
