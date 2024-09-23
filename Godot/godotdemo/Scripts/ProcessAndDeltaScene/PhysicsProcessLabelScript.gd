extends Label

var time = 0

func _physics_process(delta):
	time += delta
	text ="From _physics_process:"+str(time) # 'text' is a built-in Label property.
