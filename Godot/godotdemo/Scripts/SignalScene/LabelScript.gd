extends Label

var number:int

func _ready() -> void:
	get_parent().OnTextChange.connect(OnTextChange)

func OnTextChange(text:String):
	self.text="From Label: "+text+str(number)
	number=number+1
