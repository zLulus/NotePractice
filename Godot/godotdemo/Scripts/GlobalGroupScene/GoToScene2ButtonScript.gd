extends Node


func _on_pressed() -> void:
	#var s=load("res://Scene/GlobalGroupScene2.tscn")
	#get_tree().root.add_child(s.instantiate())
	get_tree().change_scene_to_file("res://Scene/GlobalGroupScene2.tscn")
	pass # Replace with function body.
