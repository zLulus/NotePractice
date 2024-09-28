extends Node

var number:int=1

func _on_pressed() -> void:
	Global.goto_scene("res://Scene/CustomSceneSwitcherScene2.tscn")
	pass # Replace with function body.


func _on_add_number_button_pressed() -> void:
	(get_parent().get_node("Label") as Label).text=str(number)
	number=number+1
	pass # Replace with function body.
