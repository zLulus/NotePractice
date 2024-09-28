extends Node

@onready var popup:Window=$Window

func _on_window_close_requested() -> void:
	if popup!=null:
		popup.hide()


func _on_show_window_button_pressed() -> void:
	if popup!=null:
		popup.show()
