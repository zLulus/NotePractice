extends Button


func _on_pressed() -> void:
	get_parent().emit_signal("OnTextChange","text input")
	pass # Replace with function body.
