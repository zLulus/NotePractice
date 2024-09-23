extends Node



func _on_pressed() -> void:
	get_parent().get_node("Label").call("CallFromButton","From Button")
	pass # Replace with function body.
