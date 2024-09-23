extends Label

var number:int
signal display(number:int)

func _ready() -> void:
	number=1

func _input(event):
	if event is InputEventMouseButton:
		if event.button_index == MOUSE_BUTTON_LEFT and event.pressed:
			display.emit(number)
			number=number+1
