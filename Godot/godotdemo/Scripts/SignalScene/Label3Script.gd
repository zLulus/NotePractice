extends Label

func _ready() -> void:
	var top=get_tree().get_root().get_node("SignalScene")
	top.OnTextChange.connect(OnTextChange)
	
func OnTextChange(text:String):
	self.text=text
