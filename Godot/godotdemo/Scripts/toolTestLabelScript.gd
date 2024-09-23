@tool
extends Node

func _ready():
	pass

func _process(delta):
	if Engine.is_editor_hint():
		# Code to execute in editor.
		pass

	if not Engine.is_editor_hint():
		# Code to execute in game.
		pass

	# Code to execute both in editor and in game.
	var label=get_node("%toolTestLabel") as Label
	label.text="test from script"
