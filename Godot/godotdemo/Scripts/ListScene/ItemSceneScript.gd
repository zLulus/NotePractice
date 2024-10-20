extends Node

@export var Id:int
var detail:MarginContainer

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	#var label=get_node("HBoxContainer/VBoxContainer/Label")
	detail=get_node("DetailMarginContainer")
	detail.visible=false
	pass

func _on_button_pressed() -> void:
	# group method parameters
	get_tree().call_group("RemoveItemFromListGroup","RemoveItemFromList",Id)


func _on_h_box_container_mouse_entered() -> void:
	detail.visible=true


func _on_h_box_container_mouse_exited() -> void:
	detail.visible=false
