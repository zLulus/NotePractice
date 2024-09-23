extends Label

var number:int

func _ready() -> void:
	number=0

func CallFromButton(txt:String):
	self.text=txt+str(number);
	number=number+1
