extends Node

@export var Id:int

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass

func _on_button_pressed() -> void:
	# group method parameters
	get_tree().call_group("RemoveItemFromListGroup","RemoveItemFromList",Id)
