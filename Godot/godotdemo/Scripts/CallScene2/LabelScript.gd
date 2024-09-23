extends Label

var number:int

func _ready() -> void:
	number=0

func _on_button_pressed() -> void:
	self.text=str(number)
	number=number+1
