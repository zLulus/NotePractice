extends Node


func _on_pressed() -> void:
	get_tree().call_group("TestGroup1", "CallTestGroup1")
	pass # Replace with function body.
