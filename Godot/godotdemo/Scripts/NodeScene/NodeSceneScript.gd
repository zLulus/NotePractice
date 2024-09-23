extends Node

func _ready() -> void:
	var nameLabel=get_node("NameLabel") as Label
	if nameLabel!=null:
		nameLabel.text="Name"

	var ageLabel=get_node("CanvasLayer/AgeLabel") as Label
	if ageLabel!=null:
		ageLabel.text="Age"

	var genderLabel=$CanvasLayer/GenderLabel as Label
	if genderLabel!=null:
		genderLabel.text="Gender"
