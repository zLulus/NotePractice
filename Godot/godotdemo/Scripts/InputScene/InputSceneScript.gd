extends Node


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if Input.is_action_just_pressed("SpaceTest"):
		print("press SpaceTest")
	if Input.is_action_pressed("SpaceTest"):
		print("keep pressing SpaceTest")
	if Input.is_action_just_released("SpaceTest"):
		print("release SpaceTest")
	# https://docs.godotengine.org/zh-cn/4.x/classes/class_input.html#class-input-method-get-action-strength
	var strength=Input.get_action_strength("SpaceTest")
	print("strength:"+str(strength))
	# 设置虚拟按键，左/右
	# 根据按下的键返回-1/0/1，之后乘以strength，即可向左向右移动
	var axis=Input.get_axis("Left","Right")
	print("axis:"+str(axis))
	var vector=Input.get_vector("Left","Right","Up","Down")
	print(vector)

func _input(event: InputEvent) -> void:
	if event is InputEventKey:
		var key= event as InputEventKey
		if key.keycode==KEY_C:
			print("press C")
			if key.is_echo():
				print("keep pressing C")
			if key.is_pressed():
				print("press C")
			if key.is_released():
				print("release C")
