extends Label

var time = 0

func _process(delta):
	time += delta
	text = "From _process:"+ str(time) # 'text' is a built-in Label property.
