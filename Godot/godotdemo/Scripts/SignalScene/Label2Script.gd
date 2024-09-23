extends Label

var number:int

func _ready() -> void:
	get_parent().OnTextChange.connect(OnTextChange)

func OnTextChange(text:String):
	self.text="From Label2: "+text+str(number)
	number=number+2
